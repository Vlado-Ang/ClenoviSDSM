using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ClenoviSDSM.Shared.Models;
using Microsoft.Data.Sqlite;
using Dapper;

namespace ClenoviSDSM.Server.Repositories
{
    public class ClenRepository
    {
        private readonly string _connSQLite;

        public ClenRepository(string connSQLite)
        {
            this._connSQLite = connSQLite;
        }

        public async Task<Clen> GetClen(int clenId)
        {
            using (SqliteConnection conn = new SqliteConnection(_connSQLite))
            {
                await conn.OpenAsync();

                Clen res = await conn.QueryFirstOrDefaultAsync<Clen>(
                    "select * from tClenovi where Id = @clenId and Deleted = false", new
                    {
                        clenId
                    },
                    commandType: System.Data.CommandType.Text
                    );
                return res;
            }
        }

        public async Task<IEnumerable< Clen>> GetClenovi()
        {
            using (SqliteConnection conn = new SqliteConnection(_connSQLite))
            {
                await conn.OpenAsync();
                IEnumerable<Clen> res;
                try
                { 
                res = await conn.QueryAsync<Clen>(
                    "select c.*, so.StepenObrOpis, rs.OpisStatus from tClenovi c left outer join tStepeniObrazovanie so on c.StepenObrId = so.Id " +
                    "left outer join tRabotniStatusi rs on c.RabotenStatusId = rs.Id where Deleted = false",
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

        public async Task<IEnumerable<ClenExcel>> GetClenoviExcel()
        {
            using (SqliteConnection conn = new SqliteConnection(_connSQLite))
            {
                await conn.OpenAsync();
                IEnumerable<ClenExcel> res;
                try
                {
                    res = await conn.QueryAsync<ClenExcel>(
                        "select Ime,Prezime,EMBG,DataRagjanje,Telefon1,Email,OpisStatus,RabotenStatusOpis,StepenObrOpis,ObrazovnaInstitucija,ObrNasoka,IzbirackoMesto,BrClenskaKarta " +
                        "from tClenovi c left outer join tStepeniObrazovanie so on c.StepenObrId = so.Id " +
                        "left outer join tRabotniStatusi rs on c.RabotenStatusId = rs.Id where c.Deleted = false",
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

        public async Task InsertClen(Clen clen)
        {
            using (SqliteConnection conn = new SqliteConnection(_connSQLite))
            {
                DynamicParameters par = new DynamicParameters();

                par.Add("@Ime", clen.Ime);
                par.Add("@Prezime", clen.Prezime);
                par.Add("@BrClenskaKarta", clen.BrClenskaKarta);
                par.Add("@EMBG", clen.EMBG);
                par.Add("@DataRagjanje", clen.DataRagjanje);
                par.Add("@Telefon1", clen.Telefon1);
                par.Add("@Telefon2", clen.Telefon2);
                par.Add("@eMail", clen.Email);
                par.Add("@Adresa", clen.Adresa);
                par.Add("@Hobi", clen.Hobi);
                par.Add("@StepenObrId", clen.StepenObrId);
                par.Add("@RabotenStatusId", clen.RabotenStatusId);
                par.Add("@RabotenStatusOpis", clen.RabotenStatusOpis);
                par.Add("@FbAccount", clen.FbAccount);
                par.Add("@TwitterAccount", clen.TwitterAccount);
                par.Add("@InstagramAccount", clen.InstagramAccount);
                par.Add("@LinkedInAccount", clen.LinkedInAccount);
                par.Add("@IzbirackoMesto", clen.IzbirackoMesto);
                par.Add("@IzbOpstina", clen.IzbOpstina);

                await conn.OpenAsync();

                var res = await conn.ExecuteAsync(
                    "insert into tClenovi (Ime, Prezime, EMBG, BrClenskaKarta, DataRagjanje, Telefon1, Telefon2, eMail, Adresa, Hobi, RabotenStatusId, StepenObrId, RabotenStatusOpis, FbAccount, TwitterAccount, InstagramAccount, LinkedInAccount, IzbirackoMesto, IzbOpstina) " +
                    "values (@Ime, @Prezime, @EMBG, @BrClenskaKarta, @DataRagjanje, @Telefon1, @Telefon2, @eMail, @Adresa, @Hobi, @RabotenStatusId, @StepenObrId, @RabotenStatusOpis, @FbAccount, @TwitterAccount, @InstagramAccount, @LinkedInAccount, @IzbirackoMesto, @IzbOpstina)", 
                    par,
                    commandType: System.Data.CommandType.Text
                    );
            }
        }

        public async Task UpdateClen(Clen clen)
        {
            using (SqliteConnection conn = new SqliteConnection(_connSQLite))
            {
                if (clen.RabotenStatusId == 0)
                    clen.RabotenStatusId = null;
                DynamicParameters par = new DynamicParameters();
                par.Add("@Id", clen.Id);
                par.Add("@Ime", clen.Ime);
                par.Add("@Prezime", clen.Prezime);
                par.Add("@BrClenskaKarta", clen.BrClenskaKarta);
                par.Add("@EMBG", clen.EMBG);
                par.Add("@DataRagjanje", clen.DataRagjanje);
                par.Add("@Telefon1", clen.Telefon1);
                par.Add("@Telefon2", clen.Telefon2);
                par.Add("@eMail", clen.Email);
                par.Add("@Adresa", clen.Adresa);
                par.Add("@Hobi", clen.Hobi);
                par.Add("@StepenObrId", clen.StepenObrId);
                par.Add("@RabotenStatusId", clen.RabotenStatusId);
                par.Add("@RabotenStatusOpis", clen.RabotenStatusOpis);
                par.Add("@FbAccount", clen.FbAccount);
                par.Add("@TwitterAccount", clen.TwitterAccount);
                par.Add("@InstagramAccount", clen.InstagramAccount);
                par.Add("@LinkedInAccount", clen.LinkedInAccount);
                par.Add("@IzbirackoMesto", clen.IzbirackoMesto);
                par.Add("@IzbOpstina", clen.IzbOpstina);
                par.Add("@ObrazovnaInstitucija", clen.ObrazovnaInstitucija);
                par.Add("@ObrNasoka", clen.ObrNasoka);

                await conn.OpenAsync();
                try
                { 
                var res = await conn.ExecuteAsync(
                    "update tClenovi set Ime = @Ime, Prezime = @Prezime, EMBG = @EMBG, BrClenskaKarta = @BrClenskaKarta, DataRagjanje = @DataRagjanje, Telefon1 = @Telefon1, " +
                    "Telefon2 = @Telefon2, eMail = @eMail, Adresa = @Adresa, Hobi = @Hobi, RabotenStatusId = @RabotenStatusId, StepenObrId = @StepenObrId, RabotenStatusOpis = @RabotenStatusOpis, ObrazovnaInstitucija = @ObrazovnaInstitucija, ObrNasoka = @ObrNasoka, " +
                    "FbAccount = @FbAccount, TwitterAccount = @TwitterAccount, InstagramAccount = @InstagramAccount, LinkedInAccount = @LinkedInAccount, IzbirackoMesto = @IzbirackoMesto, IzbOpstina = @IzbOpstina where Id = @Id",
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
                         
        public async Task DeleteClen(int Id)
        {
            using (SqliteConnection conn = new SqliteConnection(_connSQLite))
            {
                await conn.OpenAsync();
                DynamicParameters par = new DynamicParameters();
                par.Add("@Id", Id, System.Data.DbType.Int32, System.Data.ParameterDirection.Input);
                try
                {
                    var res = await conn.ExecuteAsync(
                        "update tClenovi set Deleted = true where Id = @Id",
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

        public async Task<IEnumerable<string>> GetOpstini()
        {
            using (SqliteConnection conn = new SqliteConnection(_connSQLite))
            {
                await conn.OpenAsync();
                IEnumerable<string> res;
                try
                {
                    res = await conn.QueryAsync<string>(
                        "select ImeOpstina from tOpstini",
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
    }
}
