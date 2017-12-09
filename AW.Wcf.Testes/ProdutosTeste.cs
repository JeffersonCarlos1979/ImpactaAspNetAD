using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using AW.Wcf.Testes.ProdutosServiceReference;

namespace AW.Wcf.Testes
{
    [TestClass]
    public class ProdutosTeste
    {
        [TestMethod]
        public void SelecionarTest()
        {

            using (var produtosClient = new ProdutosClient())
            {
                var produto = produtosClient.Selecionar(316);

                Assert.AreEqual(produto.Name,"Blade");
            }
        }

        [TestMethod]
        public void SelecionarPorNomeTest()
        {

            using (var produtosClient = new ProdutosClient())
            {
                var produtos = produtosClient.SelecionarPorNome("mountain");

                Assert.AreEqual(produtos.Length,94);
            }
        }


    }
}
