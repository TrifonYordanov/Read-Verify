using Impinj.OctaneSdk;
using ReadAndVerify.DTOs;
using System.Collections.Concurrent;

namespace ReadAndVerify.Services
{
    public class ReaderSdkService
    {
        private ImpinjReader? _reader;
        private readonly ConcurrentDictionary<string, TagReadDTO> _tagReads = new();
        private ReaderConfigurationDTO? _configuration;
        private bool _isReading;
        private System.Timers.Timer? _cleanupTimer;

        public async Task<bool> ConnectAsync(string ipAddress)
        {
            try
            {
                if (_reader?.IsConnected == true)
                    _reader.Disconnect();

                _reader = new ImpinjReader();
                await Task.Run(() => _reader.Connect(ipAddress));
                return _reader.IsConnected;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[ReaderSdkService] ConnectAsync ERROR: {ex.Message}");
                return false;
            }
        }

        public async Task ConfigureAsync(ReaderConfigurationDTO config)
        {
            if (_reader == null || !_reader.IsConnected)
            {
                Console.WriteLine("[ReaderSdkService] Cannot configure: Reader not connected.");
                return;
            }

            _configuration = config;
            var settings = _reader.QueryDefaultSettings();

            settings.Report.IncludeAntennaPortNumber = true;
            settings.Report.IncludeFirstSeenTime = true;
            settings.Report.IncludePeakRssi = true;
            settings.Report.IncludeSeenCount = true;
            settings.Report.IncludeFastId = config.IncludeTid;

            settings.Antennas.DisableAll();

            foreach (var antenna in config.Antennas)
            {
                if (antenna.IsEnabled)
                {
                    ushort port = (ushort)antenna.PortNumber;
                    var antSettings = settings.Antennas.GetAntenna(port);
                    antSettings.IsEnabled = true;
                    antSettings.TxPowerInDbm = antenna.PowerDbm;
                    antSettings.RxSensitivityInDbm = antenna.SensitivityDbm;
                }
            }

            _reader.ApplySettings(settings);
            _reader.TagsReported -= OnTagsReported;
            _reader.TagsReported += OnTagsReported;

            await Task.CompletedTask;
        }

        private void OnTagsReported(ImpinjReader sender, TagReport report)
        {
            var nowTicks = DateTime.UtcNow.Ticks;

            foreach (Tag tag in report.Tags)
            {
                var epc = tag.Epc.ToString();
                var tid = tag.Tid?.ToString();
                var key = $"{epc}-{tid}";
                var rssi = (int)tag.PeakRssiInDbm;

                _tagReads.AddOrUpdate(key,
                    _ => new TagReadDTO
                    {
                        EPC = epc,
                        TID = tid,
                        AntennaPort = tag.AntennaPortNumber,
                        RSSI = rssi,
                        RSSIMin = rssi,
                        RSSIMax = rssi,
                        FirstSeen = DateTime.Now.ToString("HH:mm:ss"),
                        LastSeenTicks = nowTicks,
                        IsActive = true
                    },
                    (_, existing) =>
                    {
                        existing.RSSI = rssi;
                        existing.RSSIMin = Math.Min(existing.RSSIMin, rssi);
                        existing.RSSIMax = Math.Max(existing.RSSIMax, rssi);
                        existing.LastSeenTicks = nowTicks;
                        existing.IsActive = true;
                        return existing;
                    });
            }
        }

        public async Task StartReadingAsync()
        {
            if (_reader == null || !_reader.IsConnected)
            {
                Console.WriteLine("[ReaderSdkService] StartReadingAsync: Reader not connected.");
                return;
            }

            _tagReads.Clear();
            _isReading = true;
            await Task.Run(() => _reader.Start());

            _cleanupTimer = new System.Timers.Timer(200);
            _cleanupTimer.Elapsed += (_, _) =>
            {
                var now = DateTime.UtcNow.Ticks;
                foreach (var key in _tagReads.Keys)
                {
                    if (_tagReads.TryGetValue(key, out var tag))
                    {
                        tag.IsActive = (now - tag.LastSeenTicks) < TimeSpan.FromMilliseconds(200).Ticks;
                    }
                }
            };
            _cleanupTimer.Start();
        }

        public async Task StopReadingAsync()
        {
            if (_reader == null || !_reader.IsConnected) return;

            _isReading = false;
            _cleanupTimer?.Stop();
            await Task.Run(() => _reader.Stop());
        }

        public IEnumerable<TagReadDTO> GetReadTags()
        {
            return _tagReads.Values.OrderByDescending(t => t.LastSeenTicks).ToList();
        }

        public bool IsConnected => _reader?.IsConnected == true;
        public bool IsReading => _isReading;

        public void Disconnect()
        {
            if (_reader != null && _reader.IsConnected)
            {
                _reader.Disconnect();
                _reader = null;
            }
        }
    }
}
