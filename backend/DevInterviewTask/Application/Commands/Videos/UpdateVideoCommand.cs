using MediatR;

namespace DevInterviewTask.Application.Commands.Videos
{
    public class UpdateVideoCommand : IRequest<Unit>
    {
        public Guid Id { get; set; }
        public required string Title { get; set; }
        public bool IsPremium { get; set; }
        public double Price { get; set; }
        public required string Currency { get; set; }
        public required string Status { get; set; }
        public Guid UserId { get; set; }
        public string? UploadId { get; set; }
        public string? AssetId { get; set; }
        public string? PlaybackId { get; set; }
    }
}
