using capaEntidad;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;

namespace capaDatos
{
    public class LaboratorioDAL
    {
        public List<LaboratorioCLS> ListarLaboratorio() 
        {
            List<LaboratorioCLS> Lista = new List<LaboratorioCLS>();

            using (SqlConnection cn = new SqlConnection(ConexionBD.getCadenaConexion()))
            {
                try
                {
                    cn.Open();
                    Console.WriteLine("Conexión a la base de datos establecida en DAL.");

                    using (SqlCommand cmd = new SqlCommand("uspListarLaboratorio", cn))
                    {
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;
                        SqlDataReader drd = cmd.ExecuteReader();

                        while (drd.Read())
                        {
                            LaboratorioCLS oLaboratorioCLS = new LaboratorioCLS()
                            {
                                IdLaboratorio = drd.GetInt32(0),
                                Nombre = drd.GetString(1),
                                Direccion = drd.GetString(2),
                                PersonaContacto = drd.IsDBNull(3) ? "" : drd.GetString(3),
                                //NumeroContacto = drd.IsDBNull(4) ? "" : drd.GetString(4),
                            };
                            Lista.Add(oLaboratorioCLS);
                            Console.WriteLine($"Laboratorio encontrada: {oLaboratorioCLS.IdLaboratorio} - {oLaboratorioCLS.Nombre}");
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error en DAL: {ex.Message}");
                }
            }

            Console.WriteLine($"Total Laboratorioes recuperadas en DAL: {Lista.Count}");
            return Lista;
        }

        public List<LaboratorioCLS> filtrarLaboratorio(LaboratorioCLS obj)
        {
            List<LaboratorioCLS> Lista = new List<LaboratorioCLS>();

            using (SqlConnection cn = new SqlConnection(ConexionBD.getCadenaConexion()))
            {
                try
                {
                    cn.Open();
                    Console.WriteLine("Conexión a la base de datos establecida en DAL.");

                    using (SqlCommand cmd = new SqlCommand("uspFiltrarLaboratorio", cn))
                    {
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@nombre", obj.Nombre == null ? "" : obj.Nombre);
                        cmd.Parameters.AddWithValue("@direccion", obj.Direccion == null ? "" : obj.Direccion);
                        cmd.Parameters.AddWithValue("@personacontacto", obj.PersonaContacto == null ? "" : obj.PersonaContacto);
                        SqlDataReader drd = cmd.ExecuteReader(CommandBehavior.SingleResult);

                        while (drd.Read())
                        {
                            LaboratorioCLS oLaboratorioCLS = new LaboratorioCLS()
                            {
                                IdLaboratorio = drd.GetOrdinal("IIDLABORATORIO"),
                                Nombre = drd.GetString(1),
                                Direccion = drd.GetString(2),
                                PersonaContacto = drd.IsDBNull(3) ? "" : drd.GetString(3),
                                //NumeroContacto = drd.IsDBNull(4) ? "" : drd.GetString(4),
                            };
                            Lista.Add(oLaboratorioCLS);
                            Console.WriteLine($"Laboratorio encontrada: {oLaboratorioCLS.IdLaboratorio} - {oLaboratorioCLS.Nombre}");
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error en DAL: {ex.Message}");
                }
            }

            Console.WriteLine($"Total Laboratorioes recuperadas en DAL: {Lista.Count}");
            return Lista;
        }
    }
}
