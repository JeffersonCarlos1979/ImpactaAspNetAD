﻿using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace Northwind.Repositorios.SqlServer.Ado
{
    public class RepositorioListBase
    {
        private String _stringConexao =
            ConfigurationManager
                .ConnectionStrings["NorthwindConnectionString"]
                .ConnectionString;

        public void ExecuteNomQuery(String nomeProcedure, params SqlParameter[] parametros)
        {
            using (var conexao = new SqlConnection(_stringConexao))
            {
                conexao.Open();

                using (var comando = new SqlCommand(nomeProcedure, conexao))
                {
                    comando.CommandType = CommandType.StoredProcedure;

                    if (parametros!=null)
                    {
                        comando.Parameters.AddRange(parametros); 
                    }

                    comando.ExecuteNonQuery();
                }
            }
        }
    }
}