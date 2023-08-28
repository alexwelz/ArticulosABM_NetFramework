using ABM_articulos_netFramework_API.AccesoDatos;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetFramework_ArticulosABM_Api.Modelo
{
    public class Log
    {
        public static void EscribirLogSql(int usuario, string tipo, string descripcion)
        {
            AccesoDB ac = new AccesoDB();

            try
            {
                SqlCommand command = new SqlCommand();
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "coop_AppLog_Insert";

                SqlParameter param1 = new SqlParameter();
                param1.ParameterName = "@usuario";
                param1.Direction = ParameterDirection.Input;
                param1.SqlDbType = SqlDbType.Int;
                param1.Value = usuario;
                command.Parameters.Add(param1);

                SqlParameter param2 = new SqlParameter();
                param2.ParameterName = "@tipo";
                param2.Direction = ParameterDirection.Input;
                param2.SqlDbType = SqlDbType.VarChar;
                param2.Value = tipo;
                command.Parameters.Add(param2);

                SqlParameter param3 = new SqlParameter();
                param3.ParameterName = "@descripcion";
                param3.Direction = ParameterDirection.Input;
                param3.SqlDbType = SqlDbType.VarChar;
                param3.Value = descripcion;
                command.Parameters.Add(param3);

                ac.ejecQuery(command);
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}
