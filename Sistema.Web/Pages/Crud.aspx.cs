using Sistema.Web.ServiceReference1;
using System;
using System.Collections.Generic;
using System.EnterpriseServices;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Sistema.Web.Pages
{
    public partial class Crud : System.Web.UI.Page
    {
        Service1Client sistemaCadastro = new Service1Client();
        
        public static Guid sID;
        public static string sOpc = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                IEnumerable<string> UfList = ConfiguraUfs();
                CarregaDropListSexo();
                CarregaDropListUFExpedicao(UfList);
                CarregaDropListUFEndereco(UfList);
                CarregaDropListEstadoCivil();
                this.CarregaDropListOrgaoEmissor();
                ConfiguraPage();
               
            }

        }

        private void CarregaDropListOrgaoEmissor()
        {
            IEnumerable<string> orgaoEmissor = new List<string>() { "SSP", "SJC", "SJT", "OUTROS"};
            this.dropOrgaoExpedicao.DataSource = orgaoEmissor;
            this.dropOrgaoExpedicao.DataBind();
        }

        private void CarregaDropListUFEndereco(IEnumerable<string> ufList)
        {
            ufEndereco.DataSource = ufList;
            ufEndereco.DataBind();
        }

        private void CarregaDropListUFExpedicao(IEnumerable<string> ufList)
        {
            this.dropUfExpedicao.DataSource = ufList;
            this.dropUfExpedicao.DataBind();

        }

        private IEnumerable<string> ConfiguraUfs()
        {
            return new List<string>() { "AC","AL","AP","AM"
                                        ,"BA","CE","DF","ES","GO",
                                        "MA", "MT","MS","MG","PA","PB",
                                        "PR", "PE","PI", "RJ", "RN", "RS", "RO", "RR"
                                        ,"SC", "SP", "SE", "TO" };
        }

        private void CarregaDropListEstadoCivil()
        {
            IList<string> estadoCivil = new List<string>
            {
                "Solteiro",
                "Casado",
                "Divorciado",
                "Viúvo"
            };
            this.dropEstadoCivil.DataSource = estadoCivil;
            dropEstadoCivil.DataBind();
        }

        private void CarregaDropListSexo()
        {
            IEnumerable<string> sexo = new List<string>
            {
                "Masculino",
                "Feminino"
            };
            this.dropSexo.DataSource = sexo;
            this.dropSexo.DataBind();
        }

        void ConfiguraPage()
        {
            if (Request.QueryString["id"] != null)
            {
                sID = new Guid(Request.QueryString["id"].ToString().Replace("-",""));
            }
            if (Request.QueryString["op"] != null)
            {
                sOpc = Request.QueryString["op"].ToString();
                switch (sOpc)
                {
                    case "C":
                        this.lbltitulo.Text = "Cadastrar Usuario";
                        this.BtnCreate.Visible = true;
                        this.BtnVoltar.Visible = true;                        
                        break;
                    case "R":
                        this.lbltitulo.Text = "Consultar Usuario";
                        this.BtnVoltar.Visible = true;
                        this.btnpesquisa.Enabled = false;
                        CargaDados();
                        DesabilitaCampos();
                        break;
                    case "U":
                        this.lbltitulo.Text = "Alterar Usuario";
                        this.BtnUpdate.Visible = true;
                        this.BtnVoltar.Visible = true;
                        CargaDados();
                        break;
                    case "D":
                        this.lbltitulo.Text = "Deletar Usuario";
                        this.BtnDelete.Visible = true;
                        this.BtnVoltar.Visible = true;
                        CargaDados();
                        break;
                }
            }
        }

        void CargaDados()
        {
            var usuarioEnderecoView = sistemaCadastro.GetUsuarioEnderecoViewById(sID);
            if(usuarioEnderecoView != null)
            {
                this.PreencheForm(usuarioEnderecoView);
            }

        }

        private void PreencheForm(UsuarioEnderecoViewModel usuarioEnderecoView)
        {
            this.txtNome.Text = usuarioEnderecoView.Nome;
            this.lblNomeUser.Text = usuarioEnderecoView.Nome;
            this.txtCpf.Text = usuarioEnderecoView.CPF;
            this.txtRG.Text = usuarioEnderecoView.RG;
            this.txtExpedicao.Text = usuarioEnderecoView.DataExpedicao.ToString("yyyy-MM-dd");
            this.dropOrgaoExpedicao.SelectedValue = usuarioEnderecoView.OrgaoExpedicao;
            this.dropUfExpedicao.SelectedValue = usuarioEnderecoView.UFExpedicao;
            this.txtDtNasc.Text = usuarioEnderecoView.DataNascimento.ToString("yyyy-MM-dd");
            this.dropSexo.SelectedValue = usuarioEnderecoView.Sexo;
            this.dropEstadoCivil.SelectedValue = usuarioEnderecoView.EstadoCivil;
            this.txtCep.Text = usuarioEnderecoView.CEP;
            this.txtRua.Text = usuarioEnderecoView.Logradouro;
            this.txtNumero.Text = usuarioEnderecoView.Numero;
            this.txtComplemento.Text = usuarioEnderecoView.Complemento;
            this.txtBairro.Text = usuarioEnderecoView.Bairro;
            this.txtCidade.Text = usuarioEnderecoView.Cidade;
            this.ufEndereco.SelectedValue = usuarioEnderecoView.UF;      

        }

        public void ValidaCamposForm(TextBox textBox)
        {
            if (string.IsNullOrWhiteSpace(textBox.Text))
                textBox.BorderColor = System.Drawing.Color.Red;
            else
                textBox.BorderColor = System.Drawing.Color.FromArgb(224, 223, 218);  
        }

        public void DesabilitaCampos()
        {
            var list = formCad.Controls.OfType<TextBox>().ToList();
            var listDrop = formCad.Controls.OfType<DropDownList>().ToList();
            list.ForEach(c => c.Enabled = false);
            listDrop.ForEach(c => c.Enabled = false);
            
        }

        protected void BtnCreate_Click(object sender, EventArgs e)
        {    
          
            if (this.ConfirmacaoDadosForm())
            {

                var usuario = CreateUpdateUsuario();

                var cadastrdo = sistemaCadastro.Add(usuario);
                if (!cadastrdo)
                {
                    ModalConfig("Cadastro", "Erro ao realizar cadastro", "error");
                }
                else Response.Redirect("Index.aspx");

            }
            else ModalConfig("Campos obrigatórios", "Preencha os campos obrigatórios", "warning");

            
        }

        private bool ConfirmacaoDadosForm()
        {
            List<TextBox> list = formCad.Controls.OfType<TextBox>().ToList();
            list.ForEach(c => ValidaCamposForm(c));
            bool validaCpfRgCep = this.ValidaCamposCpfRgCep();

            return !(list.Any(c => string.IsNullOrWhiteSpace(c.Text)) ||
                                                            validaCpfRgCep);
        }

        private bool ValidaCamposCpfRgCep()
        {
            bool validaCpfRgCep = false;
            List<TextBox> txtInputMask = new List<TextBox>() { txtRG, txtCpf, txtCep };
            foreach (var item in txtInputMask)
            {
                var text = item.Text.Replace("_", "");
                if (text.Length < item.MaxLength)
                {
                    item.BorderColor = System.Drawing.Color.Red;
                    validaCpfRgCep = true;
                }
                else
                    item.BorderColor = System.Drawing.Color.FromArgb(224, 223, 218);
            }

            return validaCpfRgCep;

        }

        private void ModalConfig(string title, string msg, string tipo)
        {
            ClientScript.RegisterClientScriptBlock(this.GetType(), "alert",
                            $"swal('{title}', '{msg}!', '{tipo}');", true);
        }

        private UsuarioModel CreateUpdateUsuario()
        {
            var usuario = new UsuarioModel();
            usuario.Nome = this.txtNome.Text;
            usuario.CPF = this.txtCpf.Text;
            usuario.RG = this.txtRG.Text;
            usuario.DataExpedicao = DateTime.Parse(this.txtExpedicao.Text);
            usuario.OrGaoExpedicao = this.dropOrgaoExpedicao.SelectedValue;
            usuario.UF = this.dropUfExpedicao.SelectedValue;
            usuario.DataNascimento = DateTime.Parse(this.txtDtNasc.Text);
            usuario.Sexo = this.dropSexo.SelectedValue;
            usuario.EstadoCivil = this.dropEstadoCivil.SelectedValue;
            var endereco = new EnderecoModel();
            endereco.Cep = this.txtCep.Text;
            endereco.Logradouro = this.txtRua.Text;
            endereco.Numero = this.txtNumero.Text;
            endereco.Complemento = this.txtComplemento.Text;
            endereco.Bairro = this.txtBairro.Text;
            endereco.Cidade = this.txtCidade.Text;
            endereco.UF = this.ufEndereco.SelectedValue;

            usuario.EnderecoModel = endereco;

            return usuario;
        }

        protected void BtnUpdate_Click(object sender, EventArgs e)
        {
            

            if(ConfirmacaoDadosForm())
            {
                var usuario = CreateUpdateUsuario();

                var alterado =sistemaCadastro.Update(sID,usuario);
                if (!alterado)
                {
                    ModalConfig("Alteração", "Erro ao alterar", "error");
                }
                else
                    Response.Redirect("Index.aspx");

            }
            else ModalConfig("Campos obrigatórios", "Preencha os campos obrigatórios", "warning");

        }

        protected void BtnDelete_Click(object sender, EventArgs e)
        {
            var deletado = sistemaCadastro.Delete(sID);
            if (!deletado)
            {
                ModalConfig("Exclusão", "Erro ao excluir", "error");
            }
            else
                Response.Redirect("Index.aspx");
        }

        protected void BtnVoltar_Click(object sender, EventArgs e)
        {
            Response.Redirect("Index.aspx");
        }

        protected void btnpesquisa_Click(object sender, EventArgs e)
        {
            var cep = txtCep.Text.Replace("-","");
            EnderecoWS endereco = sistemaCadastro.GetEnderecoWS(cep);
            if (!string.IsNullOrEmpty(endereco?.cep))
                this.PopulaEndereco(endereco);
            else
            {
                this.PopulaEndereco(new EnderecoWS());
                ModalConfig("Endereço","Endereço não encontrado", "warning");
            }

        }

        private void PopulaEndereco(EnderecoWS endereco)
        {
            this.txtBairro.Text = endereco.bairro;
            this.txtCidade.Text = endereco.localidade;
            this.txtRua.Text = endereco.logradouro;
            this.ufEndereco.SelectedValue = endereco.uf;
        }
    }
}