using ABM_articulos_netFramework_API.AccesoDatos;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NetFramework_ArticulosABM_Api.Modelo;
//using NetFramework_ArticulosABM_Api.Entitys;

namespace NetFramework_ArticulosABM_Api.Controladores
{
    public class ControladorArticulos
    {
        AccesoDB ac = new AccesoDB();
        //ABMArtiuclosEntitys artEnt = new ABMArtiuclosEntitys

        public DataTable GetArticulos()
        {
            try
            {
                SqlCommand command = new SqlCommand();
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "Get_Articulos_All";

                DataTable dt = this.ac.execDT2(command);
                return dt;
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        public DataTable GetArticulos_ById(int id)
        {
            try
            {
                SqlCommand command = new SqlCommand();
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "Get_Articulos_ById";

                SqlParameter param = new SqlParameter();
                param.ParameterName = "id";
                param.Direction = ParameterDirection.Input;
                param.SqlDbType = SqlDbType.VarChar;
                param.Value = id;
                command.Parameters.Add(param);

                DataTable dt = this.ac.execDT2(command);
                return dt;
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        public int UpdateArticulo_ById(int idArt, string descripcion)
        {
            try
            {
                SqlCommand command = new SqlCommand();
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "Update_Articulos_ById";

                SqlParameter param = new SqlParameter();
                param.ParameterName = "@Id";
                param.Direction = ParameterDirection.Input;
                param.SqlDbType = SqlDbType.Int;
                param.Value = idArt;
                command.Parameters.Add(param);

                SqlParameter param1 = new SqlParameter();
                param1.ParameterName = "@descripcion";
                param1.Direction = ParameterDirection.Input;
                param1.SqlDbType = SqlDbType.VarChar;
                param1.Value = descripcion;
                command.Parameters.Add(param1);

                this.ac.execDT2(command);

                return 1;
            }
            catch (Exception ex)
            {
                Log.EscribirLogSql(1, "ERROR", "Error: " + ex.Message);
                return -1;
            }
        }
        public int UpdateArticuloStock_ById(int idArt, double stock)
        {
            try
            {
                SqlCommand command = new SqlCommand();
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "Update_ArticulosStock_ById";

                SqlParameter param = new SqlParameter();
                param.ParameterName = "@Id";
                param.Direction = ParameterDirection.Input;
                param.SqlDbType = SqlDbType.Int;
                param.Value = idArt;
                command.Parameters.Add(param);

                SqlParameter param1 = new SqlParameter();
                param1.ParameterName = "@stock";
                param1.Direction = ParameterDirection.Input;
                param1.SqlDbType = SqlDbType.Float;
                param1.Value = stock;
                command.Parameters.Add(param1);

                this.ac.execDT2(command);
                return 1;
            }
            catch (Exception ex)
            {
                Log.EscribirLogSql(1, "ERROR", "Error: " + ex.Message);

                return -1;
            }
        }
        public int DeleteArticulo_ById(int idArt)
        {
            try
            {
                SqlCommand comman = new SqlCommand();
                comman.CommandType = CommandType.Text;
                comman.CommandText = $"UPDATE Articulos set estado=0 WHERE id={idArt}";
                DataTable dt = ac.execDT(comman);

                return 1;
            }
            catch (Exception ex)
            {
                return -1;
            }
        }

        public int InsertArticulo(string descripcion)
        {
            try
            {
                SqlCommand command = new SqlCommand();
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "Insert_Articulos";

                SqlParameter param = new SqlParameter();
                param.ParameterName = "@Id";
                param.Direction = ParameterDirection.Output;
                param.SqlDbType = SqlDbType.Int;
                command.Parameters.Add(param);

                SqlParameter param1 = new SqlParameter();
                param1.ParameterName = "@descripcion";
                param1.Direction = ParameterDirection.Input;
                param1.SqlDbType = SqlDbType.VarChar;
                param1.Value = descripcion;
                command.Parameters.Add(param1);

                SqlParameter param2 = new SqlParameter();
                param2.ParameterName = "@fechaAlta";
                param2.Direction = ParameterDirection.Input;
                param2.SqlDbType = SqlDbType.DateTime;
                param2.Value = DateTime.Now;
                command.Parameters.Add(param2);

                this.ac.execDT2(command);
                int id = Convert.ToInt32(param.Value);

                return id;
            }
            catch (Exception ex)
            {
                Log.EscribirLogSql(1, "ERROR", "Error: " + ex.Message);

                return -1;
            }
        }
        public int InsertArticuloStock(int idArt, float stock)
        {
            try
            {
                SqlCommand command = new SqlCommand();
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "Insert_ArticulosStock";

                SqlParameter param = new SqlParameter();
                param.ParameterName = "@Id";
                param.Direction = ParameterDirection.Output;
                param.SqlDbType = SqlDbType.Int;
                command.Parameters.Add(param);

                SqlParameter param0 = new SqlParameter();
                param0.ParameterName = "@idArt";
                param0.Direction = ParameterDirection.Input;
                param0.SqlDbType = SqlDbType.Int;
                param0.Value = idArt;
                command.Parameters.Add(param0);

                SqlParameter param1 = new SqlParameter();
                param1.ParameterName = "@stock";
                param1.Direction = ParameterDirection.Input;
                param1.SqlDbType = SqlDbType.Float;
                param1.Value = stock;
                command.Parameters.Add(param1);

                this.ac.execDT2(command);
                int id = Convert.ToInt32(param.Value);

                return id;
            }
            catch (Exception ex)
            {
                Log.EscribirLogSql(1, "ERROR", "Error: " + ex.Message);

                return -1;
            }
        }
    }


}
