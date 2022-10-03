namespace RiderbikeZone.Modelos
{
    public class Producto
    {
        public string Codigo { get; set; }
        public int UnidadesVendidas { get; set; }
        public int Inversion { get; set; }
        public string Marca { get; set; }
        public string Nombre { get; set; }
        public string Nota { get; set; }
        public Proveedor Origen { get; set; }
        public int PrecioAntesIVA { get; set; }
        public int PrecioCompra { get; set; }
        public int PrecioVenta { get; set; }
        public string Tipo { get; set; }
        public int Unidades { get; set; }
        public int Ganancia { get; set; }
    }
}
