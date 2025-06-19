using Dapper;
using DevInterviewTask.Domain.Videos;
using Microsoft.Data.SqlClient;

namespace DevInterviewTask.Infrastructure.Videos
{
    public class VideoRepository : IVideoRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger _logger;

        public VideoRepository(ApplicationDbContext context, ILogger<VideoRepository> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<Guid> AddAsync(VideoEntity entity)
        {
            var query = @"
                    INSERT INTO Videos (Id, Title, IsPremium, Price, Currency, Status, UserId, UploadId, AssetId, PlaybackId)
                    VALUES (@Id, @Title, @IsPremium, @Price, @Currency, @Status, @UserId, @UploadId, @AssetId, @PlaybackId);
                    ";

            try
            {
                using var conn = _context.CreateConnection();
                await conn.ExecuteAsync(query, entity);
            }
            catch (SqlException ex)
            {
                _logger.LogError(ex, "Database error occurred while creating video.");
                throw new ApplicationException("There was a problem accessing the database.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Unexpected error occurred while creating video.");
                throw;
            }

            return entity.Id;
        }

        public async Task<VideoEntity?> FindByIdAsync(Guid id)
        {
            const string query = @"
                SELECT *
                FROM Videos
                WHERE Id = @Id
            ";

            try
            {
                using var conn = _context.CreateConnection();
                return await conn.QuerySingleOrDefaultAsync<VideoEntity>(query, new { Id = id });
            }
            catch (SqlException ex)
            {
                _logger.LogError(ex, "Database error occurred while getting video by id.");
                throw new ApplicationException("There was a problem accessing the database.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Unexpected error occurred while getting video by id.");
                throw;
            }
        }

        public async Task<List<VideoEntity>> FindByUserIdAsync(Guid userId)
        {
            const string query = @"
                SELECT *
                FROM Videos
                WHERE UserId = @UserId
            ";

            try
            {
                using var conn = _context.CreateConnection();
                return (await conn.QueryAsync<VideoEntity>(query, new { UserId = userId })).ToList();
            }
            catch (SqlException ex)
            {
                _logger.LogError(ex, "Database error occurred while getting video by id.");
                throw new ApplicationException("There was a problem accessing the database.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Unexpected error occurred while getting video by id.");
                throw;
            }
        }

        public async Task<List<VideoEntity>> FindAsync()
        {
            const string query = "SELECT * FROM Videos";

            try
            {
                using var conn = _context.CreateConnection();
                return (await conn.QueryAsync<VideoEntity>(query)).ToList();
            }
            catch (SqlException ex)
            {
                _logger.LogError(ex, "Database error occurred while getting videos");
                throw new ApplicationException("There was a problem accessing the database.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Unexpected error occurred while getting videos");
                throw;
            }
        }

        public async Task UpdateAsync(VideoEntity entity)
        {
            const string query = @"
                UPDATE Videos SET
                    Title = @Title,
                    IsPremium = @IsPremium,
                    Price = @Price,
                    Currency = @Currency,
                    Status = @Status,
                    UserId = @UserId,
                    UploadId = @UploadId,
                    AssetId = @AssetId,
                    PlaybackId = @PlaybackId
                WHERE Id = @Id;";

            try
            {
                using var conn = _context.CreateConnection();
                var affected = await conn.ExecuteAsync(query, entity);
                _logger.LogInformation("Update affected {Count} rows for video ID {Id}", affected, entity.Id);
            }
            catch (SqlException ex)
            {
                _logger.LogError(ex, "Database error occurred while updating video.");
                throw new ApplicationException("There was a problem accessing the database.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Unexpected error occurred while updating video.");
                throw;
            }
        }

        public async Task DeleteAsync(Guid id)
        {
            const string query = @"
                    DELETE FROM Videos
                    WHERE Id = @Id;
                ";

            try
            {
                using var conn = _context.CreateConnection();
                await conn.ExecuteAsync(query, new { Id = id });
            }
            catch (SqlException ex)
            {
                _logger.LogError(ex, "Database error occurred while deleting video.");
                throw new ApplicationException("There was a problem accessing the database.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Unexpected error occurred while deleting video.");
                throw;
            }
        }
    }
}