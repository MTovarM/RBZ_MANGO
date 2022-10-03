namespace RiderbikeZone.Modelos
{
    using System.Collections.Generic;

    public class _Identificacion
    {
        public string Tipo { get; set; }
        public string Numero { get; set; }
        public int DigitoVe { get; set; }
    }

    public class _Product
    {
        public string Codigo { get; set; }
        public string Nombre { get; set; }
        public int Valor { get; set; }
        public int Unidades { get; set; }
    }

    public class _UbicacionNew
    {
        public string Ciudad;
        public string Departamento;
        public string Direccion;
    }

    public class ClientNew
    {
        public string Fecha { get; set; }
        public bool IsDetal { get; set; }
        public bool IsPersonaNatural { get; set; }
        public string Nombre { get; set; }
        public _Identificacion Identificacion { get; set; }
        public bool IsMan { get; set; }
        public _UbicacionNew Ubicacion { get; set; }
        public List<_Product> Productos { get; set; }
        public string Origen { get; set; }
        public string Pago { get; set; }
        public string Contacto { get; set; }
    }
}
