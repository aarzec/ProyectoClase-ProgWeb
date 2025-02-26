using capaEntidad;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;

namespace capaDatos
{
    public class SucursalDAL
    {
        public List<SucursalCLS> ListarSucursal() 
        {
            List<SucursalCLS> Lista = new List<SucursalCLS>();

            IConfigurationBuilder builder = new ConfigurationBuilder();
            builder.AddJsonFile(Path.Combine(Directory.GetCurrentDirectory(), "appsettings.json"));
            var root = builder.Build();
            var cadenaDato = root.GetConnectionString("cn");

            using (SqlConnection cn = new SqlConnection(cadenaDato))
            {
                try
                {
                    cn.Open();
                    Console.WriteLine("Conexión a la base de datos establecida en DAL.");

                    using (SqlCommand cmd = new SqlCommand("uspListarSucursal", cn))
                    {
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;
                        SqlDataReader drd = cmd.ExecuteReader();

                        while (drd.Read())
                        {
                            SucursalCLS oSucursalCLS = new SucursalCLS()
                            {
                                IdSucursal = drd.GetInt32(0),
                                Nombre = drd.GetString(1),
                                Direccion = drd.GetString(2)
                            };
                            Lista.Add(oSucursalCLS);
                            Console.WriteLine($"Sucursal encontrada: {oSucursalCLS.IdSucursal} - {oSucursalCLS.Nombre}");
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error en DAL: {ex.Message}");
                }
            }

            Console.WriteLine($"Total sucursales recuperadas en DAL: {Lista.Count}");
            return Lista;
        }

        public List<SucursalCLS> filtrarSucursal(SucursalCLS sucursalObj)
        {
            List<SucursalCLS> Lista = new List<SucursalCLS>();

            using (SqlConnection cn = new SqlConnection(ConexionBD.getCadenaConexion()))
            {
                try
                {
                    cn.Open();
                    Console.WriteLine("Conexión a la base de datos establecida en DAL.");

                    using (SqlCommand cmd = new SqlCommand("uspFiltrarSucursal", cn))
                    {
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@nombresucursal", sucursalObj.Nombre == null ? "" : sucursalObj.Nombre);
                        cmd.Parameters.AddWithValue("@direccion", sucursalObj.Direccion == null ? "" : sucursalObj.Direccion);
                        SqlDataReader drd = cmd.ExecuteReader(CommandBehavior.SingleResult);

                        while (drd.Read())
                        {
                            SucursalCLS oSucursalCLS = new SucursalCLS()
                            {
                                IdSucursal = drd.GetInt32(0),
                                Nombre = drd.GetString(1),
                                Direccion = drd.GetString(2)
                            };
                            Lista.Add(oSucursalCLS);
                            Console.WriteLine($"Sucursal encontrada: {oSucursalCLS.IdSucursal} - {oSucursalCLS.Nombre}");
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error en DAL: {ex.Message}");
                }
            }

            Console.WriteLine($"Total sucursales recuperadas en DAL: {Lista.Count}");
            return Lista;
        }

        public int GuardarSucursal(SucursalCLS objSucursalCLS)
        {
            int rpta = 0;
            using (SqlConnection cn = new SqlConnection(ConexionBD.getCadenaConexion()))
            {
                try
                {
                    cn.Open();
                    Console.WriteLine("Conexión a la base de datos establecida en DAL.");

                    using (SqlCommand cmd = new SqlCommand("insert into Sucursal(NOMBRE, DIRECCION, BHABILITADO)\r\nvalues(@nombre,@direccion,1)", cn))
                    {
                        cmd.CommandType = System.Data.CommandType.Text;
                        cmd.Parameters.AddWithValue("@nombre", objSucursalCLS.Nombre);
                        cmd.Parameters.AddWithValue("@direccion", objSucursalCLS.Direccion);

                        // Insertar, Update, Delete
                        rpta = cmd.ExecuteNonQuery();
                        // Si es 1, es que se almacenó, si es 0 no se realizó el ingreso
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error en DAL: {ex.Message}");
                    cn.Close();
                }
            }
            return rpta;
        }
    }
}
