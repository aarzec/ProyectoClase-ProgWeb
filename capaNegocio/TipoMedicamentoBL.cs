using capaDatos;
using capaEntidad;

namespace capaNegocio
{
    public class TipoMedicamentoBL
    {
        public List<TipoMedicamentoCLS> listarMedicamentos()
        {
            TipoMedicamentoDAL obj = new TipoMedicamentoDAL();

            return obj.listarTipoMedicamento();
        }

        public List<TipoMedicamentoCLS> filtrarMed(string desc)
        {
            TipoMedicamentoDAL obj = new TipoMedicamentoDAL();
            return obj.filtrarMed(desc);
        }

        public int GuardarTipoMedicamento(TipoMedicamentoCLS objTipoMedicamentoCLS)
        {
            TipoMedicamentoDAL obj = new TipoMedicamentoDAL();
            return obj.GuardarTipoMedicamento(objTipoMedicamentoCLS);
        }

        public TipoMedicamentoCLS RecuperarTipoMedicamento(int idTipoMedicamento)
        {
            TipoMedicamentoDAL obj = new TipoMedicamentoDAL();
            return obj.RecuperarTipoMedicamento(idTipoMedicamento);
        }

        public int EliminarTipoMedicamento(int idTipoMedicamento)
        {
            TipoMedicamentoDAL obj = new TipoMedicamentoDAL();
            return obj.EliminarTipoMedicamento(idTipoMedicamento);
        }
    }
}
