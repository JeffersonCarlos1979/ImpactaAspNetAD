using System;
using System.Collections.Generic;
using System.Data.Entity;
using Loja.Dominio;
using System.Linq;

namespace Loja.Repositorios.SqlServer.EF
{
    //Apostila página 191 mostra as possíveis formas de 
    
    internal class LojaDbInitializer : DropCreateDatabaseIfModelChanges<LojaDbContext>
    {
        protected override void Seed(LojaDbContext context)
        {
            context.Categorias.AddRange(ObterCategorias());
            context.SaveChanges();
            context.Produtos.AddRange(ObterProdutos(context));
            context.SaveChanges();
        }

        private IEnumerable<Produto> ObterProdutos(LojaDbContext context)
        {
            return new List<Produto>
            {
                new Produto {
                    Nome = "Grampeador",
                    Estoque=44,
                    Preco=21.44m,
                    Ativo=false,
                    Categoria = context.Categorias
                    .Where(c => c.Nome == "Papelaria")
                    .Single()
                },
                new Produto {
                    Nome = "PenDrive",
                    Estoque = 49,
                    Preco = 21.49m,
                    Ativo = false,
                    Categoria = context.Categorias
                    .Where(c => c.Nome == "Informática")
                    .Single()
                }
            };
        }

        private IEnumerable<Categoria> ObterCategorias()
        {
            return new List<Categoria>
            {
                new Categoria {Nome = "Papelaria"},
                new Categoria {Nome = "Informática"}
            };
        }
    }
}