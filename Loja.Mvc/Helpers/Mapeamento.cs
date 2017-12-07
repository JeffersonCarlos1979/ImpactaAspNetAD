﻿using Loja.Dominio;
using Loja.Mvc.Models;
using Loja.Repositorios.SqlServer.EF;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web.Mvc;

namespace Loja.Mvc.Helpers
{
    public static class Mapeamento
    {
        public static List<ProdutoViewModel> Mapear(List<Produto> produtos)
        {
            var produtosViewModel = new List<ProdutoViewModel>();

            foreach (var produto in produtos)
            {
                produtosViewModel.Add(Mapear(produto));
            }

            return produtosViewModel;
        } 

        public static Produto Mapear(ProdutoViewModel viewModel, LojaDbContext dbContext)
        {
            var produto = new Produto();

            if (viewModel.imagem != null && viewModel.imagem.ContentLength > 0)
            {
                using (var reader = new BinaryReader(viewModel.imagem.InputStream))
                {
                    produto.imagem = new ProdutoImagem
                    {
                        Bytes = reader.ReadBytes(viewModel.imagem.ContentLength),
                        ContentType = viewModel.imagem.ContentType
                    };
                }
            }

            produto.Categoria = dbContext.Categorias.Find(viewModel.CategoriaId);
            produto.Ativo = viewModel.Ativo;
            produto.Estoque = viewModel.Estoque;
            produto.Nome = viewModel.Nome;
            produto.Preco = viewModel.Preco;

            return produto;
        }

        public static ProdutoViewModel Mapear(Produto produto, List<Categoria> categorias = null)
        {
            var viewModel = new ProdutoViewModel();

            viewModel.CategoriaId = produto.Categoria?.Id;
            viewModel.CategoriaNome = produto.Categoria?.Nome;

            if (categorias != null)
            {
                foreach (var categoria in categorias)
                {
                    viewModel.Categorias.Add(new SelectListItem { Text = categoria.Nome, Value = categoria.Id.ToString() });
                }
            }

            viewModel.Estoque = produto.Estoque;
            viewModel.Id = produto.Id;
            viewModel.Nome = produto.Nome;
            viewModel.Preco = produto.Preco;
            viewModel.Ativo = produto.Ativo;

            return viewModel;
        }

        public static void Mapear(ProdutoViewModel viewModel, Produto produto, LojaDbContext dbContext)
        {
            dbContext.Entry(produto).CurrentValues.SetValues(viewModel);

            produto.Categoria = dbContext.Categorias.Single(c => c.Id == viewModel.CategoriaId);

            if (viewModel.imagem != null && viewModel.imagem.ContentLength > 0)
            {
                using (var reader = new BinaryReader(viewModel.imagem.InputStream))
                {
                    if (produto.imagem == null)
                    {
                        produto.imagem = new ProdutoImagem
                        {
                            Bytes = reader.ReadBytes(viewModel.imagem.ContentLength),
                            ContentType = viewModel.imagem.ContentType
                        };
                    }
                    else
                    {
                        produto.imagem.Bytes = reader.ReadBytes(viewModel.imagem.ContentLength);
                        produto.imagem.ContentType = viewModel.imagem.ContentType;
                    }
                }
            }
        }
    }
}