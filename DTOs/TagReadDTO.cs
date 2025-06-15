namespace ReadAndVerify.DTOs
{
    public class TagReadDTO
    {
        public string EPC { get; set; } = string.Empty;
        public string TID { get; set; } = string.Empty;
        public int AntennaPort { get; set; }
        public double RSSI { get; set; }
        public string FirstSeen { get; set; } = string.Empty;
        public long LastSeenTicks { get; set; }
        public bool IsActive { get; set; }
        public int RSSIMin { get; set; }
        public int RSSIMax { get; set; }
    }
}
