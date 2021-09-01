using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ClenoviSDSM.Shared.Models;
using Microsoft.Data.Sqlite;
using Dapper;

namespace ClenoviSDSM.Server.Repositories
{
    public class SlavaRepository
    {
        private readonly string _connSQLite;

        public SlavaRepository(string connSQLite)
        {
            this._connSQLite = connSQLite;
        }

        public async Task<List<Slava>> GetSlavi()
        {
            using (SqliteConnection conn = new SqliteConnection(_connSQLite))
            {
                await conn.OpenAsync();
                IEnumerable<Slava> res;
                try
                {
                    res = await conn.QueryAsync<Slava>(
                        "select * from tSlavi order by SlavaOpis",
                        commandType: System.Data.CommandType.Text
                        );
                }
                catch (Exception e)
                {
                    throw;
                }

                var lista = res.ToList();
                lista.Add(new Slava { Id = null, SlavaOpis = "(Непознато)" });
                return lista;
            }
        }

        public async Task UpdateSlava(Slava rs)
        {
            using (SqliteConnection conn = new SqliteConnection(_connSQLite))
            {
                DynamicParameters par = new DynamicParameters();
                par.Add("@Id", rs.Id);
                par.Add("@SlavaOpis", rs.SlavaOpis);

                await conn.OpenAsync();
                try
                {
                    var res = await conn.ExecuteAsync(
                        "update tSlavi set SlavaOpis = @SlavaOpis where Id = @Id",
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

        public async Task InsertSlava(Slava rs)
        {
            using (SqliteConnection conn = new SqliteConnection(_connSQLite))
            {
                DynamicParameters par = new DynamicParameters();
                par.Add("@Id", rs.Id);
                par.Add("@SlavaOpis", rs.SlavaOpis);

                await conn.OpenAsync();
                try
                {
                    var res = await conn.ExecuteAsync(
                        "insert into tSlavi (SlavaOpis) values (@SlavaOpis)",
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

        public async Task DeleteSlava(int Id)
        {
            using (SqliteConnection conn = new SqliteConnection(_connSQLite))
            {
                await conn.OpenAsync();
                DynamicParameters par = new DynamicParameters();
                par.Add("@Id", Id, System.Data.DbType.Int32, System.Data.ParameterDirection.Input);
                try
                {
                    var res = await conn.ExecuteAsync(
                        "delete from tSlavi where Id = @Id",
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
