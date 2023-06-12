using Sistema.Web.ServiceReference1;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Sistema.Web.Pages
{
    public partial class Index : System.Web.UI.Page
    {
        Service1Client sistemaCadastro = new Service1Client();
       
        protected void Page_Load(object sender, EventArgs e)
        {
            CargarTabla();
        }

        void CargarTabla()
        {
            var userList = sistemaCadastro.GetUserList();
            gvusuarios.DataSource = userList;

            gvusuarios.DataBind();

        }

        protected void BtnCreate_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Pages/CRUD.aspx?op=C");
        }

        protected void BtnRead_Click(object sender, EventArgs e)
        {
            string id;
            LinkButton btnConsultar = (LinkButton)sender;
            GridViewRow selectedrow = (GridViewRow)btnConsultar.NamingContainer;
            id = selectedrow.Cells[0].Text;
            Response.Redirect($"~/Pages/CRUD.aspx?id={id}&op=R");

        }

        protected void BtnUpdate_Click(object sender, EventArgs e)
        {
            string id;
            LinkButton btnConsultar = (LinkButton)sender;
            GridViewRow selectedrow = (GridViewRow)btnConsultar.NamingContainer;
            id = selectedrow.Cells[0].Text;
            Response.Redirect($"~/Pages/CRUD.aspx?id={id}&op=U");
        }

        protected void BtnDelete_Click(object sender, EventArgs e)
        {
            string id;
            LinkButton btnConsultar = (LinkButton)sender;
            GridViewRow selectedrow = (GridViewRow)btnConsultar.NamingContainer;
            id = selectedrow.Cells[0].Text;
            Response.Redirect($"~/Pages/CRUD.aspx?id={id}&op=D");
        }

       
    }
}