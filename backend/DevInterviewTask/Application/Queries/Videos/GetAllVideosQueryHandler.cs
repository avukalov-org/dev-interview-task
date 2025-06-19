using DevInterviewTask.Domain.Videos;
using MediatR;

namespace DevInterviewTask.Application.Queries.Videos
{
    public class GetAllVideosQueryHandler : IRequestHandler<GetAllVideosQuery, List<Video>>
    {
        private readonly IVideoService _videoService;

        public GetAllVideosQueryHandler(IVideoService videoService)
        {
            _videoService = videoService;
        }

        public async Task<List<Video>> Handle(GetAllVideosQuery request, CancellationToken cancellationToken)
        {
            return await _videoService.FindAsync(request.UserId);
        }
    }
}