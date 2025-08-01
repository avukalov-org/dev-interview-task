﻿using Dapper;
using DevInterviewTask.Domain.Users;
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

        public async Task<UserEntity?> FindByEmailAsync(string email)
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
                _logger.LogError(ex, "Database error occurred while getting user by email.");
                throw new ApplicationException("There was a problem accessing the database.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Unexpected error occurred while getting user by email.");
                throw;
            }
        }

        public async Task<UserEntity?> FindByIdAsync(Guid id)
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
                _logger.LogError(ex, "Database error occurred while getting user by id.");
                throw new ApplicationException("There was a problem accessing the database.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Unexpected error occurred while getting user by id.");
                throw;
            }
        }

        public async Task<Guid> AddAsync(UserEntity entity)
        {
            var query = @"
                    INSERT INTO Users (Id, FirstName, LastName, Email, PasswordHash, IsExternal, ExternalProvider)
                    VALUES (@Id, @FirstName, @LastName, @Email, @PasswordHash, @IsExternal, @ExternalProvider);
                    ";

            try
            {
                entity.Id = Guid.NewGuid();
                using var conn = _context.CreateConnection();
                await conn.ExecuteAsync(query, entity);
            }
            catch (SqlException ex)
            {
                _logger.LogError(ex, "Database error occurred while creating user.");
                throw new ApplicationException("There was a problem accessing the database.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Unexpected error occurred while creating user.");
                throw;
            }

            return entity.Id;
        }
    }
}