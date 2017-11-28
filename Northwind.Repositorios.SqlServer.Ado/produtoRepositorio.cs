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
    public class ProdutoRepositorio
    {
        public DataTable SelecionarPorCategoria(int categoriaId)
        {
            var produtoDataTable = new DataTable();
            var stringConexao = 
                ConfigurationManager
                .ConnectionStrings["NorthwindConnectionString"]
                .ConnectionString;
                //"Server=.\\SQLEXPRESS;Database=Northwind;Trusted_Connection=True";

            using (var conexao = new SqlConnection(stringConexao))
            {


                conexao.Open();

                var instrucao = $@"SELECT [ProductID]
                                  ,[ProductName]
                                  ,[UnitPrice]
                                  ,[UnitsInStock]
                                FROM [Northwind].[dbo].[Products]
                                WHERE CategoryID = @CategoryID";

                using (var comando = new SqlCommand(instrucao, conexao))
                {
                    comando.Parameters.AddWithValue("@CategoryID", categoriaId);

                    using (var dataAdapter = new SqlDataAdapter(comando))
                    {
                        dataAdapter.Fill(produtoDataTable);
                    }
                }

            }

            return produtoDataTable;
        }

    }
}
