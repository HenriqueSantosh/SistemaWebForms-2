<%@ Page Title="" Language="C#" MasterPageFile="~/MP.Master" AutoEventWireup="true" CodeBehind="Crud.aspx.cs" Inherits="Sistema.Web.Pages.Crud" %>
<asp:Content ID="Content1" ContentPlaceHolderID="title" runat="server">
    Sistema
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="head" runat="server">
    <script src="https://cdnjs.cloudflare.com/ajax/libs/jquery/3.3.1/jquery.min.js"></script>
    <script src="https://rawgit.com/RobinHerbots/Inputmask/5.x/dist/jquery.inputmask.js"></script>
    <script src="../Scripts/inputmask.js"></script>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="body" runat="server">
    <br />
    <div class="mx-auto" style="width:500px">
        <asp:Label runat="server" CssClass="h2" ID="lbltitulo"></asp:Label>
    </div>
    <div class="container">
        <form runat="server" ID="formCad" class="row g-4">
            <div class="col-md-12">
                <p style="color:blue" class="fs-4">Cliente</p>
            </div>
            <div class="col-md-3">
                <label class="form-label"><strong>CPF:</strong></label>
                <asp:TextBox runat="server" MinLength="5" MaxLength="14" CssClass="form-control cpf" ID="txtCpf"></asp:TextBox>
            </div>
            <div class="col-md-9">
                <label class="form-label fw-bold">Nome:</label>
                <asp:TextBox runat="server" CssClass="form-control" MaxLength="50" ID="txtNome"></asp:TextBox>
            </div>
            <div class="col-md-2">
                <label class="form-label fw-bold">RG:</label>
                <asp:TextBox runat="server" CssClass="form-control rg" ID="txtRG"></asp:TextBox>            
            </div>
            <div class="col-md-3">
                 <label class="form-label fw-bold">Data Expedição:</label>
                <asp:TextBox runat="server" TextMode="Date" CssClass="form-control" ID="txtExpedicao"></asp:TextBox>
            </div>
            <div class="col-md-5">
                <label class="form-label fw-bold">Orgão Expedição:</label>
                 <asp:DropDownList ID="dropOrgaoExpedicao" CssClass="form-control" placeholder="Selecione" runat="server"></asp:DropDownList>
            </div>
            <div class="col-md-2">
                <label class="form-label fw-bold">UF:</label>
                  <asp:DropDownList ID="dropUfExpedicao" CssClass="form-control" runat="server"></asp:DropDownList>
            </div>
            <div class="col-md-2">
                <label class="form-label fw-bold">Data Nascimento:</label>
                <asp:TextBox runat="server" TextMode="Date" CssClass="form-control" ID="txtDtNasc"></asp:TextBox>             
            </div>
            <div class="col-md-2">
                <label class="form-label fw-bold">Sexo:</label>
                  <asp:DropDownList ID="dropSexo" CssClass="form-control" runat="server"></asp:DropDownList>
            </div>
            <div class="col-md-2">
                <label class="form-label fw-bold">Estado Civil:</label>
                  <asp:DropDownList ID="dropEstadoCivil" CssClass="form-control" runat="server"></asp:DropDownList>
            </div>
            <div class="md-offset-6 col-md-12">
                <p style="color:blue" class="fs-4">Endereço</p>
            </div>
            <div class="form-row col-md-3">
            <label class="form-label fw-bold">CEP:</label>
           <div class="input-group">
               <asp:TextBox runat="server" MaxLength="9" CssClass="form-control cep" ID="txtCep" placeholder="Search"></asp:TextBox> 
               <div class="input-group-btn">
                   <asp:LinkButton ID="btnpesquisa" runat="server" CssClass="btn btn-primary form-control" OnClick="btnpesquisa_Click">
                  <i class="bi bi-search"></i></asp:LinkButton>
                    </div>
            </div>
         </div>
            <div class="col-md-4">
                  <label class="form-label fw-bold">Rua:</label>
                <asp:TextBox runat="server" MaxLength="100" CssClass="form-control" ID="txtRua"></asp:TextBox>   
            </div>
            <div class="col-md-2">
                  <label class="form-label fw-bold">Numero:</label>
                <asp:TextBox runat="server" CssClass="form-control" MaxLength="6" ID="txtNumero"></asp:TextBox>   
            </div>
            <div class="col-md-3">
                  <label class="form-label fw-bold">Complemento:</label>
                <asp:TextBox runat="server" CssClass="form-control" MaxLength="10" ID="txtComplemento"></asp:TextBox>   
            </div>
            <div class="col-md-5">
                  <label class="form-label fw-bold">Bairro:</label>
                <asp:TextBox runat="server" MaxLength="50" CssClass="form-control" ID="txtBairro"></asp:TextBox>   
            </div>
            <div class="col-md-5">
                  <label class="form-label fw-bold">Cidade:</label>
                <asp:TextBox runat="server" MaxLength="100" CssClass="form-control" ID="txtCidade"></asp:TextBox>   
            </div>
            <div class="col-md-2">
                  <label class="form-label fw-bold">UF:</label>
                  <asp:DropDownList ID="ufEndereco" CssClass="form-control" placeholder="Selecione" runat="server"></asp:DropDownList>
                <br />
            </div>
             <div class="col-md-2">
                 <asp:Button runat="server" CssClass="btn btn-primary btn-dark" ID="BtnVoltar" Text="Voltar" Visible="false" OnClick="BtnVoltar_Click" />
            </div>
            <div class="offset-md-8 col-md-2">
                  <asp:Button runat="server" CssClass="btn btn-success" ID="BtnCreate" Text="Create" Visible="false" OnClick="BtnCreate_Click" />
                 <asp:Button runat="server" CssClass="btn btn-primary" ID="BtnUpdate" Text="Update" Visible="false" OnClick="BtnUpdate_Click" />
                <asp:LinkButton type="button" runat="server" ID="BtnDelete" Visible="false" CssClass="btn btn-danger" data-bs-toggle="modal" data-bs-target="#modalDelete">
                  Delete
                </asp:LinkButton> 
            </div>                         
       <div class="modal fade" id="modalDelete" tabindex="-1" aria-labelledby="modalCrud" aria-hidden="true">
          <div class="modal-dialog">
            <div class="modal-content">
              <div class="modal-header">
                <h5 class="modal-title" id="modalCrud">Exclusão</h5>
                <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close"></button>
              </div>
              <div class="modal-body">
                 <p>Deseja excluir o Usuario </p>
                  <asp:Label runat="server" CssClass="h4" id="lblNomeUser"></asp:Label>

              </div>
              <div class="modal-footer">
                <button type="button" class="btn btn-primary" data-bs-dismiss="modal">Não</button>
                  <asp:Button runat="server" CssClass="btn btn-danger" ID="BtnDelete2" Text="Sim" OnClick="BtnDelete_Click"/>
              </div>
            </div>
          </div>
        </div> 
    </form>
  </div> 
  </asp:Content>
