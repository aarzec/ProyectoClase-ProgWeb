using capaDatos;
using capaEntidad;
using System.Collections.Generic;

namespace capaNegocio
{
    public class SucursalBL
    {
        public List<SucursalCLS> listarSucursal() 
        {
            SucursalDAL obj = new SucursalDAL();
            return obj.ListarSucursal();
        }

        public List<SucursalCLS> filtrarSucursal(SucursalCLS objSucursal)
        {
            SucursalDAL obj = new SucursalDAL();
            return obj.filtrarSucursal(objSucursal);
        }

        public int GuardarSucursal(SucursalCLS objSucursalCLS)
        {
            SucursalDAL obj = new SucursalDAL();
            return obj.GuardarSucursal(objSucursalCLS);
        }
    }
}
