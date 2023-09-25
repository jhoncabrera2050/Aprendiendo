using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Listado
{
    public class DetallePedido
    {
        public int IdPedido { get; set; }
        public int IdProducto { get; set; }
        public decimal PrecioUnitario { get; set; }
        public int Cantidad { get; set; }
        public decimal Descuento { get; set; }
    }

    public class Pedidos
    {
        public DateTime FechaInicio { get; set; }
        public DateTime FechaFin { get; set; }
        public List<DetallePedido> Detalles { get; set; }

        public Pedidos()
        {
            Detalles = new List<DetallePedido>();
        }
    }
}
