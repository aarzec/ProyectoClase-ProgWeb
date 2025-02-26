using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using capaEntidad;
using Microsoft.Extensions.Configuration;

namespace capaDatos
{
    public class DocumentosDAL
    {
        public List<DocumentoCLS> listarDocumentos()
        {
            List<DocumentoCLS> Lista = null;

            /*IConfigurationBuilder builder = new ConfigurationBuilder();
            builder.AddJsonFile(Path.Combine(Directory.GetCurrentDirectory(), "appsettings.json"));
            var root = builder.Build();
            var cadenaDato = root.GetConnectionString("cn");*/

            string cadenaDato = ConexionBD.getCadenaConexion();

            using (SqlConnection cn = new SqlConnection(cadenaDato))
            {
                try
                {
                    cn.Open();
                    using (SqlCommand cmd = new SqlCommand("uspRecuperarDocumento", cn))
                    {
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;
                        SqlDataReader drd = cmd.ExecuteReader(CommandBehavior.SingleResult);

                        if (drd != null)
                        {
                            DocumentoCLS oDocumentoCLS;
                            Lista = new List<DocumentoCLS>();

                            int posidTipoDoc = drd.GetOrdinal("IIDTIPODOCUMENTO");
                            int posnombreDoc = drd.GetOrdinal("NOMBRE");
                            int posbhabilitado = drd.GetOrdinal("BHABILITADO");

                            while (drd.Read())
                            {
                                oDocumentoCLS = new DocumentoCLS();
                                oDocumentoCLS.idTipoDocumento = drd.IsDBNull(posidTipoDoc) ? 0 : drd.GetInt32(0);
                                oDocumentoCLS.nombre = drd.IsDBNull(posnombreDoc) ? " " : drd.GetString(1);
                                oDocumentoCLS.bhabilitado = drd.IsDBNull(posbhabilitado) ? 0 : drd.GetInt32(2);

                                Lista.Add(oDocumentoCLS);
                            }
                        }
                    }
                }
                catch (Exception)
                {
                    cn.Close();
                    Lista = null;
                }
            }

            return Lista;
        }
    }
}