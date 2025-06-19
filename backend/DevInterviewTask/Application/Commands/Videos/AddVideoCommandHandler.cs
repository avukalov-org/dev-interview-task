using AutoMapper;
using DevInterviewTask.Domain.Videos;
using MediatR;

namespace DevInterviewTask.Application.Commands.Videos
{
    public class AddVideoCommandHandler : IRequestHandler<AddVideoCommand, Guid>
    {
        private readonly IVideoService _videoService;
        private readonly IMapper _mapper;

        public AddVideoCommandHandler(IVideoService videoService, IMapper mapper)
        {
            _videoService = videoService;
            _mapper = mapper;
        }

        public async Task<Guid> Handle(AddVideoCommand request, CancellationToken cancellationToken)
        {
            var video = _mapper.Map<Video>(request);

            return await _videoService.AddAsync(video);
        }
    }
}