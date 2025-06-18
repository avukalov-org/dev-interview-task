using AutoMapper;
using DevInterviewTask.Domain.Videos;
using MediatR;

namespace DevInterviewTask.Application.Commands
{
    public class DeleteVideoCommandHandler : IRequestHandler<DeleteVideoCommand, Unit>
    {
        private readonly IVideoService _videoService;


        public DeleteVideoCommandHandler(IVideoService videoService)
        {
            _videoService = videoService;
        } 

        public async Task<Unit> Handle(DeleteVideoCommand request, CancellationToken cancellationToken)
        {
            await _videoService.DeleteAsync(request.Id);

            return Unit.Value;
        }
    }
}
