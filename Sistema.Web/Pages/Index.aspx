<%@ Page Title="" Language="C#" MasterPageFile="~/MP.Master" AutoEventWireup="true" CodeBehind="Index.aspx.cs" Inherits="Sistema.Web.Pages.Index" %>
<asp:Content ID="Content1" ContentPlaceHolderID="title" runat="server">
    Inicio
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="head" runat="server">
    <style>
        .noprint{ display: none;}
    </style>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="body" runat="server">
    <form runat="server">
        <br />
        <div class="mx-auto" style="width:300px">
            <h2>Lista de Usúarios</h2>
        </div>
        <br />
        <div class="container">
            <div class="row">
                <div class="col align-self-end">
                    <asp:Button  runat="server" ID="BtnCreate" CssClass="btn btn-outline-success form-control" Text="Cadastrar novo usúario" OnClick="BtnCreate_Click"/>
                </div>
            </div>
        </div>
        <br />
        <div class="container">
            <div class="table small">
                <asp:GridView autogeneratecolumns="false" runat="server" ID="gvusuarios" class="table table-striped table-hover">                    
                     <Columns>
                          <asp:BoundField datafield="Id" HeaderStyle-CssClass="noprint" ItemStyle-CssClass="noprint" headertext="ID"/>
                          <asp:BoundField datafield="Nome" headertext="Nome"/>
                          <asp:BoundField datafield="Rg" headertext="RG"/>
                          <asp:BoundField datafield="CPF" headertext="CPF"/>
                          <asp:BoundField datafield="OrgaoExpedicao" headertext="Orgão Expedição"/>
                          <asp:BoundField datafield="EstadoCivil" headertext="Estado Civil"/>
                   </Columns>
                    <Columns>
                        <asp:TemplateField HeaderText="Ações">
                            <ItemTemplate>
                                 <asp:LinkButton ID="BtnRead" runat="server" CssClass="btn btn-primary form-control-sm" OnClick="BtnRead_Click">
                                 <i class="bi bi-eye-fill"></i>Visualizar</asp:LinkButton>
                                  <asp:LinkButton ID="BtnUpdate" runat="server" CssClass="btn btn-warning form-control-sm" OnClick="BtnUpdate_Click">
                                  <i class="bi bi-pencil-square"></i>Alterar</asp:LinkButton>
                                   <asp:LinkButton ID="BtnDelete" runat="server" CssClass="btn btn-danger form-control-sm" OnClick="BtnDelete_Click">
                                  <i class="bi bi-trash-fill"></i>Excluir</asp:LinkButton>
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>
            </div>
        </div>
    </form>
</asp:Content>
