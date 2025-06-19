namespace DevInterviewTask.Domain.Videos
{
    public interface IVideoService
    {
        Task<Guid> AddAsync(Video video);
        Task<List<Video>> FindAsync(Guid userId);
        Task<List<Video>> FindByUserIdAsync(Guid userId);
        Task<Video?> FindByIdAsync(Guid id);
        Task UpadateAsync(Video video);
        Task DeleteAsync(Guid id);
    }
}
