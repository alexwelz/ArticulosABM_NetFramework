using Microsoft.Reporting.WebForms;
using NetFramework_ArticulosABM_Api.Controladores;
using NetFramework_ArticulosABM_Api.Modelo;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace NetFramework_ArticulosABM_Web.Formularios.Articulos
{
    public partial class ImprimirArticulos : System.Web.UI.Page
    {

        private int accion;
        protected void Page_Load(object sender, EventArgs e)
        {
            this.accion = Convert.ToInt32(Request.QueryString["a"]);

            generarReporte1();
        }

        private void generarReporte1()
        {
            try
            {
                ControladorArticulos cArticulo = new ControladorArticulos();

                DataTable dtArticulos = cArticulo.GetArticulos();

                this.ReportViewer1.ProcessingMode = ProcessingMode.Local;
                this.ReportViewer1.LocalReport.ReportPath = Server.MapPath("ReporteArticulos.rdlc");

                ReportDataSource rds = new ReportDataSource("dsArticulos", dtArticulos);

                this.ReportViewer1.LocalReport.DataSources.Clear();

                this.ReportViewer1.LocalReport.DataSources.Add(rds);

                this.ReportViewer1.LocalReport.Refresh();

                Warning[] warnings;

                string mimeType, encoding, fileNameExtension;

                string[] streams;

                if (accion == 1)
                {
                    Byte[] xlsContent = this.ReportViewer1.LocalReport.Render("Excel", null, out mimeType, out encoding, out fileNameExtension, out streams, out warnings);

                    String filename = string.Format("{0}.{1}", "Impagas", "xls");

                    this.Response.Clear();
                    this.Response.Buffer = true;
                    this.Response.ContentType = "application/ms-excel";
                    this.Response.AddHeader("Content-Disposition", "attachment;filename=" + filename);
                    this.Response.BinaryWrite(xlsContent);

                    this.Response.End();
                }
                else
                {
                    //get pdf content
                    Byte[] pdfContent = this.ReportViewer1.LocalReport.Render("PDF", null, out mimeType, out encoding, out fileNameExtension, out streams, out warnings);

                    this.Response.Clear();
                    this.Response.Buffer = true;
                    this.Response.ContentType = "application/pdf";
                    this.Response.AddHeader("content-length", pdfContent.Length.ToString());
                    this.Response.BinaryWrite(pdfContent);

                    this.Response.End();
                }

            }
            catch (Exception ex)
            {
                Log.EscribirLogSql(1, "ERROR", "Error generando reporte: " + ex.Message);

            }
        }
    }
}