namespace DevInterviewTask.Domain.Videos
{
    public interface IVideoRepository
    {
        Task<Guid> AddAsync(VideoEntity entity);
        Task<VideoEntity?> FindByIdAsync(Guid id);
        Task<List<VideoEntity>> FindByUserIdAsync(Guid userId);
        Task<List<VideoEntity>> FindAsync();
        Task UpdateAsync(VideoEntity entity);
        Task DeleteAsync(Guid id);
    }
}
