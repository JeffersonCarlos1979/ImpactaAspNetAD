using Microsoft.VisualStudio.TestTools.UnitTesting;
using Loja.Repositorios.SqlServer.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Loja.Dominio;
using System.Diagnostics;

namespace Loja.Repositorios.SqlServer.EF.Tests
{
    [TestClass()]
    public class LojaDbContextTests
    {
        private static LojaDbContext _db = new LojaDbContext();


        [ClassInitialize]
        public static void InicializarTestes(TestContext contexto)
        {
            _db.Database.Log = LogarQueries;
        }

        private static void LogarQueries(string query)
        {
            Debug.WriteLine(query);
        }

        [TestMethod()]
        public void InserirPapelariaTest()
        {
            using (var db = new LojaDbContext())
            {
                var papelaria = new Categoria() {
                    Nome="Papelaria"
                };

                db.Categorias.Add(papelaria);
                db.SaveChanges();
            }

        }

        [TestMethod]
        public void InsererProdutoteste()
        {
            var caneta = new Produto()
            {
                Nome = "Caneta",
                Estoque = 5,
                Preco = 22.06m,
                Categoria = _db.Categorias
                    .Where(c => c.Nome=="Papelaria")
                    .Single()
            };

            _db.Produtos.Add(caneta);
            _db.SaveChanges();

        }


        [TestMethod]
        public void InsererProdutoComNovaCategoria()
        {
            var barbeador = new Produto()
            {
                Nome = "Barbeador",
                Estoque = 20,
                Preco = 35.45m,
                Categoria = new Categoria{Nome="Perfumaria"}
            };

            _db.Produtos.Add(barbeador);
            _db.SaveChanges();

        }

        [TestMethod]
        public void EditarProdutoTeste()
        {
            var caneta = _db.Produtos.Single(p => p.Nome == "Caneta");
            caneta.Preco = 44;

            _db.SaveChanges();

        }


        [ClassCleanup]
        public static void FinalizarTestes()
        {
            _db.Dispose();
        }
    }
}