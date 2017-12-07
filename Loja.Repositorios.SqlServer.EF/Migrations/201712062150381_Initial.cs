namespace Loja.Repositorios.SqlServer.EF.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Categoria",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Nome = c.String(nullable: false, maxLength: 50),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Produto",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Nome = c.String(nullable: false, maxLength: 200),
                        Preco = c.Decimal(nullable: false, precision: 9, scale: 2),
                        Estoque = c.Int(nullable: false),
                        Ativo = c.Boolean(nullable: false),
                        Categoria_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Categoria", t => t.Categoria_Id, cascadeDelete: true)
                .Index(t => t.Categoria_Id);
            
            CreateTable(
                "dbo.ProdutoImagem",
                c => new
                    {
                        ProdutoId = c.Int(nullable: false),
                        Bytes = c.Binary(),
                        ContentType = c.String(nullable: false, maxLength: 50),
                    })
                .PrimaryKey(t => t.ProdutoId)
                .ForeignKey("dbo.Produto", t => t.ProdutoId, cascadeDelete: true)
                .Index(t => t.ProdutoId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ProdutoImagem", "ProdutoId", "dbo.Produto");
            DropForeignKey("dbo.Produto", "Categoria_Id", "dbo.Categoria");
            DropIndex("dbo.ProdutoImagem", new[] { "ProdutoId" });
            DropIndex("dbo.Produto", new[] { "Categoria_Id" });
            DropTable("dbo.ProdutoImagem");
            DropTable("dbo.Produto");
            DropTable("dbo.Categoria");
        }
    }
}
