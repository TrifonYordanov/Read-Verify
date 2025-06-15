using ReadAndVerify.Models;

namespace ReadAndVerify.DTOs
{
    public class ReaderDTO
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string IpAddress { get; set; } = string.Empty;
        public string LocationName { get; set; } = string.Empty;
        public string ChipType { get; set; } = string.Empty;
        public ReaderConfiguration Configuration { get; set; } = new ReaderConfiguration();

        public bool IsOnline { get; set; } = false;
    }
}
