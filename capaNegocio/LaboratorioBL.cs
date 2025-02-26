using capaDatos;
using capaEntidad;
using System.Collections.Generic;

namespace capaNegocio
{
    public class LaboratorioBL
    {
        public List<LaboratorioCLS> listarLaboratorio() 
        {
            LaboratorioDAL obj = new LaboratorioDAL();
            return obj.ListarLaboratorio();
        }

        public List<LaboratorioCLS> filtrarLaboratorio(LaboratorioCLS objLaboratorio)
        {
            LaboratorioDAL obj = new LaboratorioDAL();
            return obj.filtrarLaboratorio(objLaboratorio);
        }
    }
}
