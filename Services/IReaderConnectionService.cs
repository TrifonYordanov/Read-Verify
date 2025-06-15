using Impinj.OctaneSdk;
using ReadAndVerify.Models;
namespace ReadAndVerify.Services
{
    public interface IReaderConnectionService
    {
        Task<bool> ConnectAsync(Reader reader);
        Task DisconnectAsync(string ipAddress);
        Task<bool> IsConnectedAsync(string ipAddress);
        Task StartReadingAsync(string ipAddress);
        Task StopReadingAsync(string ipAddress);
        List<TagData> GetReadTags(string ipAddress);
    }
}
