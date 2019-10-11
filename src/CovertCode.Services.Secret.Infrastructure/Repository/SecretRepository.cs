using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using CovertCode.Services.Secret.Contracts.DTOs;
using Dapper;

namespace CovertCode.Services.Secret.Infrastructure.Repository
{
    public class SecretRepository : IRepository<SecretDto>
    {
        private SecretRepositoryOptions _options;
        public SecretRepository(SecretRepositoryOptions options)
        {
            _options = options;
        }

        public SqlConnection Connection
        {
            get
            {
                return new SqlConnection(_options.DbConnectionString);
            }
        }

        public SecretDto Add(SecretDto secret)
        {
            using (IDbConnection dbConnection = Connection)
            {
                string query = @"INSERT INTO 
                                    dbo.Secret
                                    (AccessPhrase, Value, ExpireDate, PassPhrase)
                                    OUTPUT INSERTED.*
                                    VALUES
                                    (@AccessPhrase, @Value, @ExpireDate, @PassPhrase);";
                dbConnection.Open();
                SecretDto result = dbConnection.QuerySingle<SecretDto>(query, secret);
                dbConnection.Close();
                return result;
            }
        }

        public IEnumerable<SecretDto> FindAll()
        {
            using (IDbConnection dbConnection = Connection)
            {
                dbConnection.Open();
                return dbConnection.Query<SecretDto>("SELECT * FROM dbo.Secret");
            }
        }

        public SecretDto FindByID(int id)
        {
            using (IDbConnection dbConnection = Connection)
            {
                string query = "SELECT * FROM dbo.Secret WHERE SecretId = @Id";
                dbConnection.Open();
                return dbConnection.Query<SecretDto>(query, new { Id = id }).FirstOrDefault();
            }
        }
        public SecretDto FindByAccessPhrase(string accessPhrase)
        {
            using (IDbConnection dbConnection = Connection)
            {
                string query = "SELECT * FROM dbo.Secret WHERE AccessPhrase = @AccessPhrase and Active = 1 and DelFlag = 0";
                dbConnection.Open();
                return dbConnection.Query<SecretDto>(query, new { AccessPhrase = accessPhrase }).FirstOrDefault();
            }
        }

        public void Remove(int id)
        {
            using (IDbConnection dbConnection = Connection)
            {
                string query = @"UPDATE dbo.Secret 
                                 SET DelFlag = 1, DateUpdated = getutcdate() 
                                 WHERE ProductId = @Id";
                dbConnection.Open();
                dbConnection.Execute(query, new { Id = id });
            }
        }

        public void Update(SecretDto item)
        {
            using (IDbConnection dbConnection = Connection)
            {
                string query = @"UPDATE [dbo].[Secret]
                                 SET [Active] = @Active
                                    ,[Served] = @Served
                                    ,[DateUpdated] = getutcdate()
                                 WHERE SecretId = @SecretId";
                dbConnection.Open();
                dbConnection.Query(query, item);
            }
        }

        public void MarkAsRead(SecretDto item)
        {
            using (IDbConnection dbConnection = Connection)
            {
                string query = @"UPDATE [dbo].[Secret]
                                 SET [Active] = 0
                                    ,[Served] = 1
                                    ,[DateUpdated] = getutcdate()
                                 WHERE SecretId = @SecretId";
                dbConnection.Open();
                dbConnection.Query(query, item);
            }
        }

    }
}
