using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ClenoviSDSM.Shared.Models;
using Microsoft.Data.Sqlite;
using Dapper;

namespace ClenoviSDSM.Server.Repositories
{
    public class StepenObrazovanieRepository
    {
        private readonly string _connSQLite;

        public StepenObrazovanieRepository(string connSQLite)
        {
            this._connSQLite = connSQLite;
        }

        public async Task<List<StepenObrazovanie>> GetStepeniObrazovanie()
        {
            using (SqliteConnection conn = new SqliteConnection(_connSQLite))
            {
                await conn.OpenAsync();
                IEnumerable<StepenObrazovanie> res;
                try
                {
                    res = await conn.QueryAsync<StepenObrazovanie>(
                        "select * from tStepeniObrazovanie order by Id asc",
                        commandType: System.Data.CommandType.Text
                        );
                }
                catch (Exception e)
                {
                    throw;
                }

                var lista = res.ToList();
                lista.Add(new StepenObrazovanie { Id = null, StepenObrOpis = "(Непознато)" });
                return lista;
            }
        }

        public async Task UpdateStepenObrazovanie(StepenObrazovanie rs)
        {
            using (SqliteConnection conn = new SqliteConnection(_connSQLite))
            {
                DynamicParameters par = new DynamicParameters();
                par.Add("@Id", rs.Id);
                par.Add("@StepenObrOpis", rs.StepenObrOpis);

                await conn.OpenAsync();
                try
                {
                    var res = await conn.ExecuteAsync(
                        "update tStepeniObrazovanie set StepenObrOpis = @StepenObrOpis where Id = @Id",
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

        public async Task InsertStepenObrazovanie(StepenObrazovanie rs)
        {
            using (SqliteConnection conn = new SqliteConnection(_connSQLite))
            {
                DynamicParameters par = new DynamicParameters();
                par.Add("@Id", rs.Id);
                par.Add("@StepenObrOpis", rs.StepenObrOpis);

                await conn.OpenAsync();
                try
                {
                    var res = await conn.ExecuteAsync(
                        "insert into tStepeniObrazovanie (StepenObrOpis) values (@StepenObrOpis)",
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

        public async Task DeleteStepenObrazovanie(int Id)
        {
            using (SqliteConnection conn = new SqliteConnection(_connSQLite))
            {
                await conn.OpenAsync();
                DynamicParameters par = new DynamicParameters();
                par.Add("@Id", Id, System.Data.DbType.Int32, System.Data.ParameterDirection.Input);
                try
                {
                    var res = await conn.ExecuteAsync(
                        "delete from tStepeniObrazovanie where Id = @Id",
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
