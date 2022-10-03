namespace RiderbikeZone.Servicios
{
    using System.IO;

    public class SEscribirTXT
    {
        public void Escribir(string _direccion, string _contenido)
        {
            if (!File.Exists(_direccion))
            {
                using (StreamWriter sw = File.CreateText(_direccion))
                {
                    sw.WriteLine(_contenido);
                    sw.Close();
                }
            }
            else
            {
                using (StreamWriter sw = File.AppendText(_direccion))
                {
                    sw.WriteLine(_contenido);
                }
            }
        }

        public void Escribirs(string _direccion, string _contenido)
        {
            using (StreamWriter sw = File.CreateText(_direccion))
            {
                sw.Write(_contenido);
                sw.Close();
            }
        }

        public string Leer(string _direccion)
        {
            string _contenido = string.Empty;
            using (StreamReader sr = File.OpenText(_direccion))
            {
                _contenido = sr.ReadToEnd();
                sr.Close();
            }
            return _contenido;
        }

        public void Eliminar(string _direccion)
        {
            if (File.Exists(_direccion)) File.Delete(_direccion);
        }
    }
}
