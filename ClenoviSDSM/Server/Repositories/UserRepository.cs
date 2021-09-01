using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ClenoviSDSM.Shared.Models;
using Microsoft.Data.Sqlite;
using Dapper;

namespace ClenoviSDSM.Server.Repositories
{
    public class UserRepository
    {
        private readonly string _connSQLite;

        public UserRepository(string connSQLite)
        {
            _connSQLite = connSQLite;
        }

        public async Task<IEnumerable<User>> GetKorisnici()
        {
            using (SqliteConnection conn = new SqliteConnection(_connSQLite))
            {
                await conn.OpenAsync();
                IEnumerable<User> res;
                try
                {
                    res = await conn.QueryAsync<User>(
                        "select * from tUsers where IsDisabled = false",
                        commandType: System.Data.CommandType.Text
                        );
                }
                catch (Exception e)
                {
                    throw;
                }
                return res;
            }
        }

        public async Task<User> GetKorisnik(string username)
        {
            using (SqliteConnection conn = new SqliteConnection(_connSQLite))
            {
                await conn.OpenAsync();

                DynamicParameters par = new DynamicParameters();
                par.Add("@Username", username);

                User res = await conn.QueryFirstOrDefaultAsync<User>(
                    "select * from tUsers where Username = @Username and IsDisabled = false",
                    par,
                    commandType: System.Data.CommandType.Text
                    );

                return res;
            }
        }

        public async Task InsertKorisnik(User user)
        {
            using (SqliteConnection conn = new SqliteConnection(_connSQLite))
            {
                DynamicParameters par = new DynamicParameters();
                par.Add("@Username", user.Username);
                par.Add("@Password", user.Password);
                par.Add("@FirstName", user.FirstName);
                par.Add("@LastName", user.LastName);
                par.Add("@RoleName", user.RoleName);
                par.Add("@IsDisabled", user.IsDisabled);


                await conn.OpenAsync();

                var res = await conn.ExecuteAsync(
                    "INSERT INTO tUsers (Username, Password, FirstName, LastName, RoleName, IsDisabled) " +
                    "VALUES (@Username, @Password, @FirstName, @LastName, @RoleName, @IsDisabled)",
                    par,
                    commandType: System.Data.CommandType.Text
                    );
            }
        }

        public async Task UpdateKorisnik(User user)
        {
            using (SqliteConnection conn = new SqliteConnection(_connSQLite))
            {
                DynamicParameters par = new DynamicParameters();
                par.Add("@Id", user.Id);
                par.Add("@Username", user.Username);
                par.Add("@Password", user.Password);
                par.Add("@FirstName", user.FirstName);
                par.Add("@LastName", user.LastName);
                par.Add("@RoleName", user.RoleName);
                par.Add("@IsDisabled", user.IsDisabled);


                await conn.OpenAsync();

                var res = await conn.ExecuteAsync(
                    "UPDATE tUsers SET  Username = @Username, Password = @Password, FirstName = @FirstName, LastName = @LastName, RoleName = @RoleName, " +
                    "IsDisabled = @IsDisabled WHERE Id = @Id",
                    par,
                    commandType: System.Data.CommandType.Text
                );
            }
        }

        public async Task ChangePassword(User user)
        {
            using (SqliteConnection conn = new SqliteConnection(_connSQLite))
            {
                DynamicParameters par = new DynamicParameters();
                par.Add("@Id", user.Id);
                par.Add("@Password", user.Password);



                await conn.OpenAsync();

                var res = await conn.ExecuteAsync(
                    "UPDATE tUsers SET  Password = @Password, WHERE Id = @Id",
                    par,
                    commandType: System.Data.CommandType.Text
                );
            }
        }

        public async Task DeleteKorisnik(int Id)
        {
            using (SqliteConnection conn = new SqliteConnection(_connSQLite))
            {
                await conn.OpenAsync();
                DynamicParameters par = new DynamicParameters();
                par.Add("@Id", Id);
                try
                {
                    var res = await conn.ExecuteAsync(
                        "update tUsers set IsDisabled = true where Id = @Id",
                        par,
                        commandType: System.Data.CommandType.Text
                        );
                }
                catch (Exception e)
                {
                    throw;
                }
            }
        }
    }
}
