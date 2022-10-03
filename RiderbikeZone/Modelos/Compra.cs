namespace RiderbikeZone.Modelos
{
    using System.Collections.Generic;
    public class Compra
    {
        public string Fecha { get; set; }
        public List<Producto> ListaProductos { get; set; }
        public Proveedor Proveedor { get; set; }
        public int Total { get; set; }
    }
}
