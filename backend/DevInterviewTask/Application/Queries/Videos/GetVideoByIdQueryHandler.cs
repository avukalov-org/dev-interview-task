using DevInterviewTask.Domain.Videos;
using MediatR;

namespace DevInterviewTask.Application.Queries.Videos
{
    public class GetVideoByIdQueryHandler : IRequestHandler<GetVideoByIdQuery, Video?>
    {
        private readonly IVideoService _videoService;

        public GetVideoByIdQueryHandler(IVideoService videoService)
        {
            _videoService = videoService;
        }

        public async Task<Video?> Handle(GetVideoByIdQuery request, CancellationToken cancellationToken)
        {
            return await _videoService.FindByIdAsync(request.Id);
        }
    }
}
