namespace RiderbikeZone.Formularios
{
    using RiderbikeZone.Modelos;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Data;
    using System.Drawing;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using System.Windows.Forms;

    public partial class AgregarUnidadesPorGrupo : Form
    {
        #region Propiedades
        public string Email { get; set; }
        public string Clave { get; set; }
        public List<Producto> Productos { get; set; }
        public List<int> UnidadesExistentes { get; set; }
        #endregion

        #region Constructor
        public AgregarUnidadesPorGrupo(string _email, string _clave, List<int> _unidadesExistentes, List<Producto> _productos)
        {
            Email = _email;
            Clave = _clave;
            Productos = _productos;
            UnidadesExistentes = _unidadesExistentes;
            InitializeComponent();
        }
        #endregion

        #region Métodos

        #endregion
    }
}
