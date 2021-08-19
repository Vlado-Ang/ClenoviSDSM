using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ClenoviSDSM.Shared.Models;
using Microsoft.Data.Sqlite;
using Dapper;

namespace ClenoviSDSM.Server.Repositories
{
    public class RabotenStatusRepository
    {
        private readonly string _connSQLite;

        public RabotenStatusRepository(string connSQLite)
        {
            this._connSQLite = connSQLite;
        }

        public async Task<List<RabotenStatus>> GetRabotniStatusi()
        {
            using (SqliteConnection conn = new SqliteConnection(_connSQLite))
            {
                await conn.OpenAsync();
                IEnumerable<RabotenStatus> res;
                try
                {
                    res = await conn.QueryAsync<RabotenStatus>(
                        "select * from tRabotniStatusi order by OpisStatus desc",
                        commandType: System.Data.CommandType.Text
                        );
                }
                catch (Exception e)
                {
                    throw;
                }

                var lista = res.ToList();
                lista.Add(new RabotenStatus { Id = null, OpisStatus = "(Непознато)" });
                return lista;
            }
        }

        public async Task UpdateRabotenStatus(RabotenStatus rs)
        {
            using (SqliteConnection conn = new SqliteConnection(_connSQLite))
            {
                DynamicParameters par = new DynamicParameters();
                par.Add("@Id", rs.Id);
                par.Add("@OpisStatus", rs.OpisStatus);

                await conn.OpenAsync();
                try
                {
                    var res = await conn.ExecuteAsync(
                        "update tRabotniStatusi set OpisStatus = @OpisStatus where Id = @Id",
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

        public async Task InsertRabotenStatus(RabotenStatus rs)
        {
            using (SqliteConnection conn = new SqliteConnection(_connSQLite))
            {
                DynamicParameters par = new DynamicParameters();
                par.Add("@Id", rs.Id);
                par.Add("@OpisStatus", rs.OpisStatus);

                await conn.OpenAsync();
                try
                {
                    var res = await conn.ExecuteAsync(
                        "insert into tRabotniStatusi (OpisStatus) values (@OpisStatus)",
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

        public async Task DeleteRabotenStatus(int Id)
        {
            using (SqliteConnection conn = new SqliteConnection(_connSQLite))
            {
                await conn.OpenAsync();
                DynamicParameters par = new DynamicParameters();
                par.Add("@Id", Id, System.Data.DbType.Int32, System.Data.ParameterDirection.Input);
                try
                {
                    var res = await conn.ExecuteAsync(
                        "delete from tRabotniStatusi where Id = @Id",
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
