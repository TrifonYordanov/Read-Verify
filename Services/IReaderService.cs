using ReadAndVerify.DTOs;

namespace ReadAndVerify.Services
{
    public interface IReaderService
    {
        Task<List<ReaderDTO>> GetAllAsync();
        Task<ReaderDTO?> GetByIdAsync(int id);
        Task CreateAsync(ReaderDTO readerDto);
        Task DeleteAsync(int id);
        Task UpdateAsync(ReaderDTO readerDto);

    }
}
