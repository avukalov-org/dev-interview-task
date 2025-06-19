using Dapper;
using DevInterviewTask.Domain.Payments;
using DevInterviewTask.Domain.Videos;
using DevInterviewTask.Infrastructure.Videos;
using Microsoft.Data.SqlClient;

namespace DevInterviewTask.Infrastructure.Payments
{
    public class PaymentRepository : IPaymentRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger _logger;

        public PaymentRepository(ApplicationDbContext context, ILogger<VideoRepository> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task AddAsync(UserPaymentEntity entity)
        {
            var query = @"
                    INSERT INTO UserPayments (Id, UserId, VideoId, PurchaseAt)
                    VALUES (@Id, @UserId, @VideoId, @PurchaseAt);
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
        }

        public async Task<List<UserPaymentEntity>> FindByUserIdAsync(Guid userId)
        {
            const string query = @"
                SELECT *
                FROM UserPayments
                WHERE UserId = @UserId
            ";

            try
            {
                using var conn = _context.CreateConnection();
                return (await conn.QueryAsync<UserPaymentEntity>(query, new { UserId = userId })).ToList();
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
    }
}
