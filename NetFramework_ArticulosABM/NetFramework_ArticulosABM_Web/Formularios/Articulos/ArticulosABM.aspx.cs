using NetFramework_ArticulosABM_Api.Controladores;
using NetFramework_ArticulosABM_Api.Modelo;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace NetFramework_ArticulosABM_Web.Formularios.Articulos
{
    public partial class ArticulosABM : System.Web.UI.Page
    {

        protected void Page_Load(object sender, EventArgs e)
        {

        }

        [WebMethod]
        public static string GetArticulo_ById(string id)
        {

            try
            {
                ControladorArticulos cArticulo = new ControladorArticulos();
                DataTable dt = cArticulo.GetArticulos_ById(Convert.ToInt32(id));

                string resultadoJSON = JsonConvert.SerializeObject(dt);
                return resultadoJSON;
            }
            catch (Exception ex)
            {
                Log.EscribirLogSql(1, "ERROR", "Error: " + ex.Message);

                return null;
            }
        }

        [WebMethod]
        public static string UpdateArticulo_ById(string id, string detalle, string stock)
        {
            try
            {
                ControladorArticulos cArt = new ControladorArticulos();

                int rta = cArt.UpdateArticulo_ById(Convert.ToInt32(id), detalle);
                if (rta > 0)
                {
                    cArt.UpdateArticuloStock_ById(Convert.ToInt32(id), Convert.ToDouble(stock));
                }

                return rta.ToString();
            }
            catch (Exception ex)
            {
                Log.EscribirLogSql(1, "ERROR", "Error: " + ex.Message);

                return null;
            }
        }

        protected void lbtnInsertArticulo_Click(object sender, EventArgs e)
        {
            try
            {
                ControladorArticulos cArticulo = new ControladorArticulos();
                string msg = "toastr.error('Algo salio mal!)";
                string det = Request.Form["detalleN"].ToString();
                decimal stock = Convert.ToDecimal(Request.Form["stockN"]);

                int rta = cArticulo.InsertArticulo(det);
                if (rta > 0)
                {
                    int rtaStock = cArticulo.InsertArticuloStock(rta, (float)stock);
                    if (rtaStock > 0)
                    {
                        msg = "toastr.success('Articulo agregado correctamente!');setTimeout(() => {window.location.href = 'Articulos.aspx'},3000)";
                    }
                }

                ScriptManager.RegisterStartupScript(this, GetType(), "OpenNewTab", msg, true);
            }
            catch (Exception ex)
            {
                Log.EscribirLogSql(1, "ERROR", "Error: " + ex.Message);

                throw;
            }
        }
    }
}