using AutoMapper;
using DevInterviewTask.Domain.Videos;
using MediatR;

namespace DevInterviewTask.Application.Commands.Videos
{
    public class UpdateVideoCommandHandler : IRequestHandler<UpdateVideoCommand, Unit>
    {
        private readonly IVideoService _videoService;
        private readonly IMapper _mapper;

        public UpdateVideoCommandHandler(IVideoService videoService, IMapper mapper)
        {
            _videoService = videoService;
            _mapper = mapper;
        }

        public async Task<Unit> Handle(UpdateVideoCommand request, CancellationToken cancellationToken)
        {
            var video = _mapper.Map<Video>(request);

            await _videoService.UpadateAsync(video);

            return Unit.Value;
        }
    }
}