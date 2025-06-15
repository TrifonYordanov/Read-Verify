using Impinj.OctaneSdk;
using ReadAndVerify.Models;
using System.Collections.Concurrent;

namespace ReadAndVerify.Services
{
    public class ReaderConnectionService : IReaderConnectionService
    {
        private readonly ConcurrentDictionary<string, ImpinjReader> _connectedReaders = new();
        private readonly ConcurrentDictionary<string, List<TagData>> _readTags = new();

        public async Task<bool> ConnectAsync(Reader reader)
        {
            if (_connectedReaders.ContainsKey(reader.IpAddress))
                return true;

            var impinjReader = new ImpinjReader();
            try
            {
                await Task.Run(() => impinjReader.Connect(reader.IpAddress));
                impinjReader.TagsReported += (sender, args) =>
                {
                    var tags = args.Tags.Select(t => new TagData
                    {
                        EPC = t.Epc.ToString(),
                        TID = t.Tid.ToHexWordString(),
                        RSSI = t.PeakRssiInDbm
                    }).ToList();

                    _readTags.AddOrUpdate(reader.IpAddress, tags, (k, existing) =>
                    {
                        existing.AddRange(tags);
                        return existing;
                    });
                };

                _connectedReaders.TryAdd(reader.IpAddress, impinjReader);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public async Task DisconnectAsync(string ipAddress)
        {
            if (_connectedReaders.TryRemove(ipAddress, out var reader))
                await Task.Run(() => reader.Disconnect());
        }

        public Task<bool> IsConnectedAsync(string ipAddress)
        {
            return Task.FromResult(_connectedReaders.ContainsKey(ipAddress));
        }

        public async Task StartReadingAsync(string ipAddress)
        {
            if (_connectedReaders.TryGetValue(ipAddress, out var reader))
            {
                var settings = reader.QueryDefaultSettings();
                settings.Report.Mode = ReportMode.Individual;
                settings.Report.IncludeFastId = true;
                settings.Report.IncludePeakRssi = true;
                await Task.Run(() => reader.ApplySettings(settings));
                await Task.Run(() => reader.Start());
            }
        }

        public async Task StopReadingAsync(string ipAddress)
        {
            if (_connectedReaders.TryGetValue(ipAddress, out var reader))
                await Task.Run(() => reader.Stop());
        }

        public List<TagData> GetReadTags(string ipAddress)
        {
            return _readTags.TryGetValue(ipAddress, out var tags) ? tags : new List<TagData>();
        }
    }

    public class TagData
    {
        public string EPC { get; set; } = string.Empty;
        public string TID { get; set; } = string.Empty;
        public double RSSI { get; set; }
    }
}

