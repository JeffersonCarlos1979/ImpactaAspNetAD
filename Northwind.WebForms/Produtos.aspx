<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Produtos.aspx.cs" Inherits="Northwind.WebForms.Produtos" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <h2>Produtos</h2>
    <div class="row">
        <div class="col-lg-12">
            <asp:RadioButtonList runat="server"
                ID="criterioRadioButtonList"
                RepeatDirection="Horizontal"
                RepeatLayout="Flow">
                <asp:ListItem Text="Categoria" Value="0" />
                <asp:ListItem Text="Fornecedor" Value="1" />
            </asp:RadioButtonList>
            <asp:MultiView ActiveViewIndex="0"
                runat="server" ID="criterioMultiView">
                <asp:View runat="server" ID="categoriaView">
                    <asp:DropDownList runat="server" 
                        ID="categoriaDrop"
                        DataTextField="CategoryName"
                        DataValueField="CategoryID"
                        DataSourceID="categoriaDataSource"
                        AppendDataBoundItems="true">

                        <asp:ListItem Text="seleciona uma categoria" value="0"/>
                    </asp:DropDownList>
                    <asp:ObjectDataSource runat="server" 
                        ID="categoriaDataSource"
                        TypeName="Northwind.Repositorios.SqlServer.Ado.CategoriaRepositorio" 
                        SelectMethod="Selecionar"/>
                </asp:View>
                <asp:View runat="server" ID="fornecedorView">
                    <asp:DropDownList runat="server" 
                        ID="fornecedorDrop"
                        DataTextField="CompanyName"
                        DataValueField="SupplierID">
                        <asp:ListItem Text="seleciona um fornecedor" value="0" />
                    </asp:DropDownList>
                </asp:View>
            </asp:MultiView>
        </div>
    </div>
    <div class="row">
        <div class="col-lg-12">
            grid
        </div>
    </div>
</asp:Content>
