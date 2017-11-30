using Northwind.Dominio;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Northwind.Repositorios.SqlServer.Ado
{
    public class TrasportadoraRepositorio : RepositorioBase
    {
        private String _stringConexao =
            ConfigurationManager
                .ConnectionStrings["NorthwindConnectionString"]
                .ConnectionString;
        
        public List<Transportadora> Selecionar()
        {
            var transportadoras = new List<Transportadora>();

            using (var conexao = new SqlConnection(_stringConexao))
            {
                conexao.Open();

                using (var comando = new SqlCommand("TransportadoraSelecionar", conexao))
                {
                    comando.CommandType = CommandType.StoredProcedure;

                    using (var registro = comando.ExecuteReader())
                    {
                        while (registro.Read())
                        {
                            transportadoras.Add(Mapear(registro));
                        }
                    }


                }

            }

            return transportadoras;
        }

        private Transportadora Mapear(SqlDataReader registro)
        {
            return new Transportadora()
            {
                Id = Convert.ToInt32(registro["ShipperID"]),
                Nome = registro["CompanyName"].ToString(),
                Telefone = Convert.ToString(registro["Phone"])
            };
        }
    }
}
