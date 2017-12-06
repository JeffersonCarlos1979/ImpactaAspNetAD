using Loja.Dominio;
using System.Data.Entity.ModelConfiguration;

namespace Loja.Repositorios.SqlServer.EF.ModelConfiguration
{
    internal class ProdutoConfiguration : EntityTypeConfiguration<Produto>
    {
        public ProdutoConfiguration()
        {
            Property(p => p.Nome)
                .IsRequired()
                .HasMaxLength(200)
                .HasColumnType("nvarchar");

            Property(p => p.Preco)
                .IsRequired()
                .HasPrecision(9, 2) ;

            HasRequired(p => p.Categoria);

            //Criação da chave estrangeira ligando 1x1 produto e imagem 
            HasOptional(p => p.imagem)
                .WithRequired(pi => pi.produto)
                .WillCascadeOnDelete(true);
        }
    }
}