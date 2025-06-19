namespace DevInterviewTask.Domain.Videos
{
    public class VideoEntity
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public bool IsPremium { get; set; }
        public double Price { get; set; }
        public string Currency { get; set; }
        public string Status { get; set; }
        public Guid UserId { get; set; }
        public string? UploadId { get; set; }
        public string? AssetId { get; set; }
        public string? PlaybackId { get; set; }
    }
}