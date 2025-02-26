using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using capaDatos;
using capaEntidad;

namespace capaNegocio
{
    public class DocumentoBL
    {
        public List<DocumentoCLS> listarDocumentos()
        {
            DocumentosDAL obj = new DocumentosDAL();

            return obj.listarDocumentos();
        }
    }
}