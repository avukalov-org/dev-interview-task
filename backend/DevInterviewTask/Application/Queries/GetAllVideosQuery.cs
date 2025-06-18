using DevInterviewTask.Domain.Videos;
using MediatR;

namespace DevInterviewTask.Application.Queries
{
    public class GetAllVideosQuery : IRequest<List<Video>>
    {
        // TODO: Add filter parameters 
    }
}
