using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ClenoviSDSM.Shared.Models;
using Microsoft.Data.Sqlite;
using Dapper;

namespace ClenoviSDSM.Server.Repositories
{
    public class NacionalnostRepository
    {
        private readonly string _connSQLite;

        public NacionalnostRepository(string connSQLite)
        {
            this._connSQLite = connSQLite;
        }

        public async Task<List<Nacionalnost>> GetNacionalnosti()
        {
            using (SqliteConnection conn = new SqliteConnection(_connSQLite))
            {
                await conn.OpenAsync();
                IEnumerable<Nacionalnost> res;
                try
                {
                    res = await conn.QueryAsync<Nacionalnost>(
                        "select * from tNacionalnosti order by Id",
                        commandType: System.Data.CommandType.Text
                        );
                }
                catch (Exception e)
                {
                    throw;
                }

                var lista = res.ToList();
                lista.Add(new Nacionalnost { Id = null, NacOpis = "(Непознато)" });
                return lista;
            }
        }

        public async Task UpdateNacionalnost(Nacionalnost rs)
        {
            using (SqliteConnection conn = new SqliteConnection(_connSQLite))
            {
                DynamicParameters par = new DynamicParameters();
                par.Add("@Id", rs.Id);
                par.Add("@NacOpis", rs.NacOpis);

                await conn.OpenAsync();
                try
                {
                    var res = await conn.ExecuteAsync(
                        "update tNacionalnosti set NacOpis = @NacOpis where Id = @Id",
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

        public async Task InsertNacionalnost(Nacionalnost rs)
        {
            using (SqliteConnection conn = new SqliteConnection(_connSQLite))
            {
                DynamicParameters par = new DynamicParameters();
                par.Add("@Id", rs.Id);
                par.Add("@NacOpis", rs.NacOpis);

                await conn.OpenAsync();
                try
                {
                    var res = await conn.ExecuteAsync(
                        "insert into tNacionalnosti (NacOpis) values (@NacOpis)",
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

        public async Task DeleteNacionalnost(int Id)
        {
            using (SqliteConnection conn = new SqliteConnection(_connSQLite))
            {
                await conn.OpenAsync();
                DynamicParameters par = new DynamicParameters();
                par.Add("@Id", Id, System.Data.DbType.Int32, System.Data.ParameterDirection.Input);
                try
                {
                    var res = await conn.ExecuteAsync(
                        "delete from tNacionalnosti where Id = @Id",
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
