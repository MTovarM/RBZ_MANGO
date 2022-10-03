namespace RiderbikeZone.Modelos
{
    using System.Collections.Generic;

    public class VentaNew
    {
        public ClientNew ClientNew { get; set; }
        public string Fecha { get; set; }
        public string FormaPago { get; set; }
        public List<Producto> ListaProductos { get; set; }
        public string Observaciones { get; set; }
        public int Total { get; set; }
        public int Ganancia { get; set; }
    }
}
