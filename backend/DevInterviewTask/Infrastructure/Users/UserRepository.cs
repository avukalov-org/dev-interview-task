using Azure.Core;
using Dapper;
using DevInterviewTask.Application.DTOs;
using DevInterviewTask.Domain.Users;
using MediatR;
using Microsoft.Data.SqlClient;
using static Dapper.SqlMapper;

namespace DevInterviewTask.Infrastructure.Users
{
    public class UserRepository : IUserRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger _logger;

        public UserRepository(ApplicationDbContext context, ILogger<UserRepository> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<UserEntity?> FindByEmail(string email)
        {
            const string query = @"
                SELECT *
                FROM Users
                WHERE Email = @Email
            ";

            try
            {
                using var conn = _context.CreateConnection();
                return await conn.QuerySingleOrDefaultAsync<UserEntity>(query, new { Email = email });
            }
            catch (SqlException ex)
            {
                _logger.LogError(ex, "Database error occurred while registering user.");
                throw new ApplicationException("There was a problem accessing the database.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Unexpected error occurred while registering user.");
                throw;
            }
        }

        public async Task<UserEntity?> FindById(Guid id)
        {
            const string query = @"
                SELECT *
                FROM Users
                WHERE Id = @Id
            ";

            try
            {
                using var conn = _context.CreateConnection();
                return await conn.QuerySingleOrDefaultAsync<UserEntity>(query, new { Id = id });
            }
            catch (SqlException ex)
            {
                _logger.LogError(ex, "Database error occurred while registering user.");
                throw new ApplicationException("There was a problem accessing the database.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Unexpected error occurred while registering user.");
                throw;
            }
        }

        public async Task<Guid> Create(UserEntity entity)
        {
            var query = @"
                    INSERT INTO Users (Id, FirstName, LastName, Email, PasswordHash)
                    VALUES (@Id, @FirstName, @LastName, @Email, @PasswordHash);
                    ";

            try
            {
                entity.Id = Guid.NewGuid();
                using var conn = _context.CreateConnection();
                await conn.ExecuteAsync(query, entity);
            }
            catch (SqlException ex)
            {
                _logger.LogError(ex, "Database error occurred while registering user.");
                throw new ApplicationException("There was a problem accessing the database.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Unexpected error occurred while registering user.");
                throw;
            }

            return entity.Id;
        }
    }
}