using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ABM_articulos_netFramework_API.AccesoDatos
{
    public class AccesoDB
    {
        public SqlConnection ConnectionBD = new SqlConnection();
        public string strConDatos;

        public AccesoDB()
        {
            strConDatos = @"Data Source=DESKTOP-CTQ5TEH;
                Initial Catalog=ABMArticulos;
                Persist Security Info=False;
                Integrated Security=True;";

        }

        public void DesConectar()
        {
            try
            {
                ConnectionBD.Close();
            }
            catch (Exception e)
            {

                throw e;
            }
        }

        public void Conectar()
        {
            try
            {
                if (this.ConnectionBD.State == 0)
                {
                    ConnectionBD.ConnectionString = strConDatos;
                    ConnectionBD.Open();
                }
            }
            catch (Exception e)
            {

                throw e;
            }
        }

        public DataTable execDT2(SqlCommand command)
        {
            try
            {
                command.Connection = this.ConnectionBD;
                Conectar();
                SqlDataAdapter da = new SqlDataAdapter(command);

                DataTable dt = new DataTable();
                da.Fill(dt);
                return dt;
            }
            catch (Exception e)
            {

                throw e;
            }
            finally
            {
                DesConectar();
            }
        }

        public DataTable execDT(SqlCommand Command)
        {
            try
            {
                Command.Connection = this.ConnectionBD;
                Conectar();
                SqlDataAdapter da = new SqlDataAdapter(Command);

                DataTable dt = new DataTable();
                da.Fill(dt);

                return dt;
            }
            catch (Exception ex)
            {
                DesConectar();
                throw ex;
            }
            finally
            {
                DesConectar();
            }

        }

        public SqlCommand ejecQuery(SqlCommand cmd)
        {
            int resp = 0;
            try
            {

                cmd.Connection = this.ConnectionBD;
                cmd.CommandTimeout = 60;
                Conectar();
                resp = cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw;
            }
            finally
            {
                DesConectar();
            }
            return cmd;
        }
    }
}
