using capaEntidad;
using Microsoft.Extensions.Configuration;
using System.Data.SqlClient;
using System.Net.Http.Headers;

namespace capaDatos
{
    public class TipoMedicamentoDAL
    {
        public List<TipoMedicamentoCLS> listarTipoMedicamento()
        {
            List<TipoMedicamentoCLS> Lista = null;

            IConfigurationBuilder builder = new ConfigurationBuilder();
            builder.AddJsonFile(Path.Combine(Directory.GetCurrentDirectory(), "appsettings.json"));
            var root = builder.Build();
            var cadenaDato = root.GetConnectionString("cn");

            using (SqlConnection cn = new SqlConnection(cadenaDato))
            {
                try
                {
                    cn.Open();
                    using (SqlCommand cmd = new SqlCommand("SELECT IIDTIPOMEDICAMENTO, NOMBRE, DESCRIPCION FROM TipoMedicamento WHERE BHABILITADO = 1", cn))
                    {
                        cmd.CommandType = System.Data.CommandType.Text;
                        SqlDataReader drd = cmd.ExecuteReader();

                        if(drd != null)
                        {
                            TipoMedicamentoCLS oTipoMedicamentoCLS;
                            Lista = new List<TipoMedicamentoCLS>();

                            while (drd.Read())
                            {
                                oTipoMedicamentoCLS = new TipoMedicamentoCLS();
                                oTipoMedicamentoCLS.idTipoMedicamento = drd.GetInt32(0);
                                oTipoMedicamentoCLS.nombre = drd.GetString(1);
                                oTipoMedicamentoCLS.descripcion = drd.GetString(2);
                                Lista.Add(oTipoMedicamentoCLS);
                            }
                        }
                    }
                }
                catch(Exception)
                {
                    cn.Close();
                    Lista = null;
                }
            }

            return Lista;
        }

        public List<TipoMedicamentoCLS> filtrarMed(string desc)
        {
            List<TipoMedicamentoCLS> Lista = new List<TipoMedicamentoCLS>();

            using (SqlConnection cn = new SqlConnection(ConexionBD.getCadenaConexion()))
            {
                try
                {
                    cn.Open();
                    Console.WriteLine("Conexión a la base de datos establecida en DAL.");

                    using (SqlCommand cmd = new SqlCommand("uspFiltrarMedicamento", cn))
                    {
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@descripcion", desc);
                        SqlDataReader drd = cmd.ExecuteReader();

                        if (drd != null)
                        {
                            TipoMedicamentoCLS oTipoMedicamentoCLS;
                            Lista = new List<TipoMedicamentoCLS>();

                            while (drd.Read())
                            {
                                oTipoMedicamentoCLS = new TipoMedicamentoCLS();
                                oTipoMedicamentoCLS.idTipoMedicamento = drd.GetInt32(0);
                                oTipoMedicamentoCLS.nombre = drd.GetString(1);
                                oTipoMedicamentoCLS.descripcion = drd.GetString(2);
                                Lista.Add(oTipoMedicamentoCLS);
                            }
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

        public int GuardarTipoMedicamento(TipoMedicamentoCLS objTipoMedicamentoCLS)
        {
            int rpta = 0;
            using (SqlConnection cn = new SqlConnection(ConexionBD.getCadenaConexion()))
            {
                try
                {
                    cn.Open();
                    Console.WriteLine("Conexión a la base de datos establecida en DAL.");

                    using (SqlCommand cmd = new SqlCommand("uspGuardarTipoMedicamento", cn))
                    {
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@nombre", objTipoMedicamentoCLS.nombre);
                        cmd.Parameters.AddWithValue("@descripcion", objTipoMedicamentoCLS.descripcion);
                        cmd.Parameters.AddWithValue("@idTipoMedicamento", objTipoMedicamentoCLS.idTipoMedicamento);
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

        public TipoMedicamentoCLS RecuperarTipoMedicamento(int idTipoMedicamento)
        {
            TipoMedicamentoCLS oTipoMedicamentoCLS = new TipoMedicamentoCLS();
            using (SqlConnection cn = new SqlConnection(ConexionBD.getCadenaConexion()))
            {
                try
                {
                    cn.Open();
                    Console.WriteLine("Conexión a la base de datos establecida en DAL. Buscando con ID " + idTipoMedicamento);
                    using (SqlCommand cmd = new SqlCommand("SELECT IIDTIPOMEDICAMENTO, NOMBRE, DESCRIPCION FROM TipoMedicamento WHERE IIDTIPOMEDICAMENTO = @idTipoMedicamento", cn))
                    {
                        cmd.CommandType = System.Data.CommandType.Text;
                        cmd.Parameters.AddWithValue("@idTipoMedicamento", idTipoMedicamento);
                        SqlDataReader drd = cmd.ExecuteReader();
                        if (drd != null)
                        {
                            while (drd.Read())
                            {
                                oTipoMedicamentoCLS.idTipoMedicamento = drd.GetInt32(0);
                                oTipoMedicamentoCLS.nombre = drd.GetString(1);
                                oTipoMedicamentoCLS.descripcion = drd.GetString(2);
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error en DAL: {ex.Message}");
                    cn.Close();
                }
            }
            return oTipoMedicamentoCLS;
        }
    }
}