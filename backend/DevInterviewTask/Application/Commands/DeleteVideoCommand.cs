using MediatR;

namespace DevInterviewTask.Application.Commands
{
    public class DeleteVideoCommand : IRequest<Unit>
    {
        public DeleteVideoCommand(Guid id)
        {
            Id = id;
        }

        public Guid Id { get; set; }
    }
}
