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
    public class TrasportadoraRepositorio : RepositorioListBase
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

        public Transportadora Selecionar(int id)
        {
            Transportadora transportadora = null;

            using (var conexao = new SqlConnection(_stringConexao))
            {
                conexao.Open();

                using (var comando = new SqlCommand("TransportadoraSelecionar", conexao))
                {
                    comando.CommandType = CommandType.StoredProcedure;
                    comando.Parameters.Add(new SqlParameter("@ShipperID", id));

                    using (var registro = comando.ExecuteReader())
                    {
                        if (registro.Read())
                        {
                            transportadora = Mapear(registro);
                        }
                    }
                }
            }
            return transportadora;
        }

        public void Inserir(Transportadora transportadora)
        {

            using (var conexao = new SqlConnection(_stringConexao))
            {
                conexao.Open();

                using (var comando = new SqlCommand("TransportadoraInserir", conexao))
                {
                    comando.CommandType = CommandType.StoredProcedure;
                    comando.Parameters.AddRange(Mapear(transportadora));

                    transportadora.Id = Convert.ToInt32(comando.ExecuteScalar());
                }
            }
        }

        public void Atualizar(Transportadora transportadora)
        {

            //using (var conexao = new SqlConnection(_stringConexao))
            //{
            //    conexao.Open();

            //    using (var comando = new SqlCommand("TransportadoraAtualizar", conexao))
            //    {
            //        comando.CommandType = CommandType.StoredProcedure;
            //        comando.Parameters.AddRange(Mapear(transportadora));

            //        comando.ExecuteNonQuery();
            //    }
            //}
            ExecuteNomQuery("TransportadoraAtualizar", Mapear(transportadora));
        }

        public void Excluir(Int32 id)
        {

            //using (var conexao = new SqlConnection(_stringConexao))
            //{
            //    conexao.Open();

            //    using (var comando = new SqlCommand("TransportadoraExcluir", conexao))
            //    {
            //        comando.CommandType = CommandType.StoredProcedure;
            //        //comando.Parameters.Add(new SqlParameter("@ShipperID", id));
            //        comando.Parameters.AddWithValue("@ShipperID", id);
            //        comando.ExecuteNonQuery();
            //    }
            //}
            ExecuteNomQuery("TransportadoraExcluir", new SqlParameter("@ShipperID", id));
        }

        private SqlParameter[] Mapear(Transportadora transportadora)
        {
            //SqlParameter[] parametros = new List<SqlParameter>();
            var parametros = new List<SqlParameter>() {
                new SqlParameter("@companyName", transportadora.Nome),
                new SqlParameter("@phone",transportadora.Telefone)
            };

            if (transportadora.Id>0)
            {
                parametros.Add(new SqlParameter("@ShipperID", transportadora.Id));
            }

            return parametros.ToArray();
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
