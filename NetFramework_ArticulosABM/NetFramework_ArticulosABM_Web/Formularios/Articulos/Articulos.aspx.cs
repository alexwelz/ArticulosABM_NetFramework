using NetFramework_ArticulosABM_Api.Controladores;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace NetFramework_ArticulosABM_Web.Formularios.Articulos
{
    
    public partial class Articulos : System.Web.UI.Page
    {
        ControladorArticulos cArticulos = new ControladorArticulos();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                CargarArticulos();
            }
        }
        public void CargarArticulos()
        {
            try
            {
                //obtenemos los art de la bd
                DataTable dt = cArticulos.GetArticulos();
                //recorremos con un foreach y lo agregamos al placeholder
                if (dt != null)
                {
                    foreach (DataRow dr in dt.Rows)
                    {
                        CargarArticulos_PH(dr);
                    }
                }
            }
            catch (Exception ex)
            {

            }
        }
        public void CargarArticulos_PH(DataRow articulo)
        {
            try
            {
                TableRow tr = new TableRow();

                TableCell celDescripcion = new TableCell();
                celDescripcion.Text = articulo["descripcion"].ToString();
                celDescripcion.VerticalAlign = VerticalAlign.Middle;
                celDescripcion.Width = Unit.Percentage(45);
                tr.Cells.Add(celDescripcion);

                TableCell celStock = new TableCell();
                celStock.Text = articulo["stock"].ToString() == "" ? "0" : articulo["stock"].ToString();
                celStock.VerticalAlign = VerticalAlign.Middle;
                celStock.Width = Unit.Percentage(15);
                tr.Cells.Add(celStock);

                TableCell celFechaAlta = new TableCell();
                celFechaAlta.Text = articulo["fechaAlta"].ToString();
                celFechaAlta.VerticalAlign = VerticalAlign.Middle;
                celFechaAlta.Width = Unit.Percentage(15);
                tr.Cells.Add(celFechaAlta);

                TableCell celAccion = new TableCell();

                HtmlGenericControl btnEditar = new HtmlGenericControl("a");
                btnEditar.Attributes.Add("class", "btn btn-xs");
                btnEditar.Attributes.Add("data-toggle", "tooltip");
                btnEditar.Attributes.Add("title", "Editar");
                btnEditar.ID = articulo["id"].ToString();
                btnEditar.InnerHtml = "<span class='bi bi-pencil-fill'></span>";
                btnEditar.Attributes.Add("OnClick", $"EditarArticulo('{articulo["id"].ToString()}');");
                celAccion.Controls.Add(btnEditar);

                Literal l = new Literal();
                l.Text = "&nbsp";
                celAccion.Controls.Add(l);

                HtmlGenericControl btnEliminar = new HtmlGenericControl();
                btnEliminar.ID = "btnEliminar_" + articulo["id"].ToString();
                btnEliminar.Attributes.Add("class", "btn btn-xs text-danger");
                btnEliminar.Attributes.Add("data-toggle", "tooltip");
                btnEliminar.Attributes.Add("title", "Eliminar");
                btnEliminar.InnerHtml = "<span class='bi bi-trash-fill'></span>";
                btnEliminar.Attributes.Add("OnClick", "EliminarArticulo(" + articulo["id"].ToString() + ");");
                celAccion.Controls.Add(btnEliminar);
                celAccion.VerticalAlign = VerticalAlign.Middle;
                celAccion.HorizontalAlign = HorizontalAlign.Center;
                tr.Cells.Add(celAccion);

                phArticulos.Controls.Add(tr);

            }
            catch (Exception e)
            {

            }
        }
        protected void btnSi_Click(object sender, EventArgs e)
        {
            try
            {
                int id = Convert.ToInt32(hidenIdArt.Value);
                int rta = cArticulos.DeleteArticulo_ById(id);
                if (rta > 0)
                {
                    string script = "toastr.success('Articulo eliminado con exito!. ')";
                    ScriptManager.RegisterStartupScript(this, GetType(), "OpenNewTab", script, true);
                    CargarArticulos();
                }
            }
            catch (Exception ex)
            {

            }
        }
       
    }
   
}