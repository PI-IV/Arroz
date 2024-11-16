using ARROZ.Interface;
using Microsoft.AspNetCore.WebUtilities;
using System.Data.SqlClient;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Formats;
using SixLabors.ImageSharp.Processing;
using SixLabors.ImageSharp.Processing.Processors.Transforms;
using SixLabors.ImageSharp.Web;
using SixLabors.ImageSharp.Web.Processors;
using SixLabors.ImageSharp.Metadata.Profiles.Exif;
using SixLabors.ImageSharp.Formats.Webp;
using SixLabors.ImageSharp.Formats.Jpeg;
using System.IO;
using Microsoft.AspNetCore.Http;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Formats;
using SixLabors.ImageSharp.Processing;
using SixLabors.ImageSharp.Processing.Processors;
using Microsoft.Web.Helpers;
using Microsoft.Extensions.Configuration;
using ARROZ.Models;
using System.Net.Mail;
using System.Net;
using Microsoft.Extensions.Hosting;
using static ARROZ.Repository.CommonRepository;
using MimeKit;
using MailKit.Net;
using System;

namespace ARROZ.Repository
{
    public class CommonRepository : ICommon
    {
        private string con_ARROZ;
        private string con_Krypt;
        private readonly IConfiguration _configuration;
        public CommonRepository(IConfiguration configuration)
        {
            _configuration = configuration;
            con_ARROZ = _configuration.GetConnectionString("DefaultConnection");
            con_Krypt = _configuration.GetConnectionString("KryptConnection");
        }

       
        public List<Common> GetConsumoMedio()
        {
            List<Common> list = new List<Common>();
            Common mod = new Common();
            using (SqlConnection con = new SqlConnection(con_ARROZ))
            {
                string queryString = "SELECT ANO_MES, VALOR FROM CONSUMO_MEDIO  ORDER BY ANO_MES ASC";
                SqlCommand cmd = new SqlCommand(queryString, con);
                con.Open();
                SqlDataReader rdr = cmd.ExecuteReader();

                while (rdr.Read())
                {
                    mod = new Common();
                    mod.ID = 1;
                    mod.ANOMES = Convert.ToString(rdr[0]);
                    mod.VALOR = Convert.ToDouble(rdr[1]);
                    list.Add(mod);
                }

                rdr.Close();
            }

            return list;
        }

        public List<Common> GetPrecoArroz()
        {
            List<Common> list = new List<Common>();
            Common mod = new Common();
            using (SqlConnection con = new SqlConnection(con_ARROZ))
            {
                //string queryString = "SELECT ANO_MES, VALOR_BR FROM PREÇO_ARROZ_50KG  ORDER BY ANO_MES ASC";
                string queryString = @"SELECT ANO_MES, AVG(VALOR_BR) FROM PREÇO_ARROZ_5KG
                                        WHERE VALOR_BR > 0
                                        GROUP BY ANO_MES";
                SqlCommand cmd = new SqlCommand(queryString, con);
                con.Open();
                SqlDataReader rdr = cmd.ExecuteReader();

                while (rdr.Read())
                {
                    mod = new Common();
                    mod.ID = 2;
                    mod.ANOMES = Convert.ToString(rdr[0]);
                    mod.VALOR = Convert.ToDouble(rdr[1]);
                    list.Add(mod);
                }

                rdr.Close();
            }

            return list;
        }
        public List<Common> GetSalarioMinimo()
        {
            List<Common> list = new List<Common>();
            Common mod = new Common();
            using (SqlConnection con = new SqlConnection(con_ARROZ))
            {
                string queryString = "SELECT ANO_MES, VALOR FROM SALARIO_MINIMO ORDER BY ANO_MES ASC";
                SqlCommand cmd = new SqlCommand(queryString, con);
                con.Open();
                SqlDataReader rdr = cmd.ExecuteReader();

                while (rdr.Read())
                {
                    mod = new Common();
                    mod.ID = 3;
                    mod.ANOMES = Convert.ToString(rdr[0]);
                    mod.VALOR = Convert.ToDouble(rdr[1]);
                    list.Add(mod);
                }

                rdr.Close();
            }

            return list;
        }
        public List<Common> GetPrecoDolar()
        {
            List<Common> list = new List<Common>();
            Common mod = new Common();
            using (SqlConnection con = new SqlConnection(con_ARROZ))
            {
                string queryString = @"SELECT CONVERT(VARCHAR,DATEPART(YYYY,[DATA]))+'-'+ RIGHT('00'+ CAST(DATEPART(MM,[DATA]) AS VARCHAR(2)),2), AVG(VALOR) FROM PREÇO_DOLAR
GROUP BY (CONVERT(VARCHAR,DATEPART(YYYY,[DATA]))+'-'+RIGHT('00'+ CAST(DATEPART(MM,[DATA]) AS VARCHAR(2)),2))
ORDER BY (CONVERT(VARCHAR,DATEPART(YYYY,[DATA]))+'-'+RIGHT('00'+ CAST(DATEPART(MM,[DATA]) AS VARCHAR(2)),2)) ASC";
                SqlCommand cmd = new SqlCommand(queryString, con);
                con.Open();
                SqlDataReader rdr = cmd.ExecuteReader();

                while (rdr.Read())
                {
                    mod = new Common();
                    mod.ID = 4;
                    mod.ANOMES = Convert.ToString(rdr[0]);
                    mod.VALOR = Convert.ToDouble(rdr[1]);
                    list.Add(mod);
                }

                rdr.Close();
            }

            return list;
        }

    }

}
