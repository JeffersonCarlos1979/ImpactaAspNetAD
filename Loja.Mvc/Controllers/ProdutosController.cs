﻿using Loja.Dominio;
using Loja.Mvc.Helpers;
using Loja.Mvc.Models;
using Loja.Repositorios.SqlServer.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Loja.Mvc.Controllers
{
    public class ProdutosController : Controller
    {
        private LojaDbContext _db = new LojaDbContext();

        // GET: Produtos
        public ActionResult Index()
        {
            return View(Mapeamento.Mapear(_db.Produtos.ToList()));
        }

        // GET: Produtos/Details/5
        public ActionResult Details(int id)
        {

            return View(Mapeamento.Mapear(_db.Produtos.Find(id)));
        }

        // GET: Produtos/Create
        public ActionResult Create()
        {
            return View(Mapeamento.Mapear(new Produto(),_db.Categorias.ToList()));
        }

        // POST: Produtos/Create
        [HttpPost]
        public ActionResult Create(ProdutoViewModel viewModel)
        {
            try
            {
                var produto = Mapeamento.Mapear(viewModel, _db);
                _db.Produtos.Add(produto);
                _db.SaveChanges();
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Produtos/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Produtos/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Produtos/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Produtos/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
