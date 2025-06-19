using DevInterviewTask.Domain.Videos;
using MediatR;

namespace DevInterviewTask.Application.Queries.Videos
{
    public class GetUserVideosQueryHandler : IRequestHandler<GetUserVideosQuery, List<Video>>
    {
        private readonly IVideoService _videoService;

        public GetUserVideosQueryHandler(IVideoService videoService)
        {
            _videoService = videoService;
        }

        public async Task<List<Video>> Handle(GetUserVideosQuery request, CancellationToken cancellationToken)
        {
            return await _videoService.FindByUserIdAsync(request.Id);
        }
    }
}