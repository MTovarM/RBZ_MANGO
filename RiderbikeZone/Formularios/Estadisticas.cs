namespace RiderbikeZone.Formularios
{
    using RiderbikeZone.Modelos;
    using RiderbikeZone.Servicios;
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Drawing;
    using System.Linq;
    using System.Windows.Forms;

    public partial class Estadisticas : Form
    {
        #region Propiedades
        public string Email { get; set; }
        public string Clave { get; set; }
        public List<VentaNew> Ventas { get; set; }
        public List<Compra> Compras { get; set; }
        public List<Producto> Productos { get; set; }
        #endregion

        #region Constructor
        public Estadisticas(string _email, string _clave)
        {
            Email = _email;
            Clave = _clave;
            Ventas = new List<VentaNew>();
            Compras = new List<Compra>();
            Productos = new List<Producto>();
            InitializeComponent();
            Obtener();
        }
        #endregion

        #region Métodos
        public async void Obtener()
        {
            SFirebase FB = new SFirebase
            {
                Email = Email,
                Clave = Clave
            };
            try
            {
                var t = await FB.LoginWithEmail(false);
                if (t == null) throw new Exception();
            }
            catch (Exception)
            {
                MessageBox.Show("Ha ocurrido un error de conexión.\nIntente recargando la página.", "Error de conexión", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            try 
            {
                var _compras = await FB.ObtenerCompras();
                var _ventas = await FB.ObtenerVentasNew();
                var _productos = await FB.ObtenerProductos("Inventario");
                foreach (var c in _compras) Compras.Add(c);
                foreach (var v in _ventas) Ventas.Add(v);
                foreach (var p in _productos) Productos.Add(p);
                /*
                List<VentaNew> uV = new List<VentaNew>();
                List<ClientNew> uC = new List<ClientNew>();
                foreach (var i in Ventas)
                {
                    if (i.Cliente != null)
                    {
                        var ide = new _Identificacion()
                        {
                            Tipo = "CC-Cédula Ciudadanía",
                            Numero = i.Cliente.Cedula,
                            DigitoVe = -1
                        };

                        string iOrigen = "";
                        var _origen = i.Cliente.Origen == "Otro" ? iOrigen = "Punto Fisico" : iOrigen = i.Cliente.Origen;

                        _UbicacionNew ubi = new _UbicacionNew()
                        {
                            Departamento = "",
                            Ciudad = i.Cliente.Ciudad,
                            Direccion = i.Cliente.Direccion
                        };

                        List<_ProductNew> prod = new List<_ProductNew>();

                        foreach (var p in i.ListaProductos)
                        {
                            prod.Add(new _ProductNew() 
                            {
                                Codigo = p.Codigo,
                                Nombre = p.Nombre,
                                Unidades = p.UnidadesVendidas,
                                Valor = p.PrecioVenta
                            });
                        }

                        i.ClientNew = new ClientNew()
                        {
                            Fecha = i.Fecha,
                            IsDetal = true,
                            IsPersonaNatural = true,
                            Nombre = i.Cliente.Nombre,
                            Identificacion = ide,
                            Ubicacion = ubi,
                            Productos = prod,
                            Origen = iOrigen,
                            Contacto = i.Cliente.Contacto,
                            IsMan = true,
                            Pago = "Pagos digitales"
                        };

                        VentaNew VN = new VentaNew()
                        {
                            ClientNew = i.ClientNew,
                            Fecha = i.Fecha,
                            FormaPago = i.FormaPago,
                            ListaProductos = i.ListaProductos,
                            Observaciones = i.Observaciones,
                            Total = i.Total,
                            Ganancia = i.Ganancia
                        };
                        uV.Add(VN);
                        uC.Add(i.ClientNew);
                    }
                }
                var ty = uC.GroupBy(x => x.Nombre).Select(y => y.First()).ToList();
                foreach (var _uv in uV)
                {
                    await FB.Guardar("UVenta", _uv);
                }
                foreach (var _uc in ty)
                {
                    await FB.Guardar("UCliente", _uc);
                }*/
                ////////////////////////////////////////
                LlenarTablas();
            }
            catch(Exception)
            {
                MessageBox.Show("Ha ocurrido un error.\nIntente recargando la página.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
        }

        public void Llenar_ProductosMasVendidosDataGridView(List<Producto> _productos)
        {
            int _puesto = 1;
            ProductosMasVendidos_DataGridView.Rows.Clear();
            foreach (var D in _productos)
            {
                int i = ProductosMasVendidos_DataGridView.Rows.Add();

                ProductosMasVendidos_DataGridView.Rows[i].DefaultCellStyle.Font = new Font("Century Gothic", 11.25F, FontStyle.Regular, GraphicsUnit.Point, 0);
                ProductosMasVendidos_DataGridView.Rows[i].Cells[0].Value = _puesto;
                ProductosMasVendidos_DataGridView.Rows[i].Cells[1].Value = D.Nombre;
                ProductosMasVendidos_DataGridView.Rows[i].Cells[2].Value = D.UnidadesVendidas;
                ProductosMasVendidos_DataGridView.Rows[i].Cells[3].Value = D.Ganancia.ToString("N0");
                _puesto++;
            }
        }

        public void Llenar_ProductosMasVendidosLastMesDataGridView(List<Producto> _productos)
        {
            int _puesto = 1;
            ProductosMasVendidosLastMes_DataGridView.Rows.Clear();
            try
            {
                foreach (var D in _productos)
                {
                    int i = ProductosMasVendidosLastMes_DataGridView.Rows.Add();

                    ProductosMasVendidosLastMes_DataGridView.Rows[i].DefaultCellStyle.Font = new Font("Century Gothic", 11.25F, FontStyle.Regular, GraphicsUnit.Point, 0);
                    ProductosMasVendidosLastMes_DataGridView.Rows[i].Cells[0].Value = _puesto;
                    ProductosMasVendidosLastMes_DataGridView.Rows[i].Cells[1].Value = D.Nombre;
                    ProductosMasVendidosLastMes_DataGridView.Rows[i].Cells[2].Value = D.UnidadesVendidas;
                    ProductosMasVendidosLastMes_DataGridView.Rows[i].Cells[3].Value = D.Ganancia.ToString("N0");
                    _puesto++;
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Recargue de nuevo la página \"Estadisticas\" por favor.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
        }

        public void LlenarTablas()
        {
            DateTime _fecha = DateTime.Now;
            List<string> Mes = new List<string>()
            {"ene","feb", "mar", "abr", "may", "jun", "jul", "ago", "sep", "oct", "nov", "dic"};
            string _mess = Mes[_fecha.Month - 1];
            string _mes = string.Empty; ;
            string _anio = _fecha.Year.ToString();
            if (_mess == "ene")
            {
                _mes = "dic";
                var _year = Convert.ToInt32(_anio) - 1;
                _anio = _year.ToString();
            }
            else _mes = Mes[_fecha.Month - 2];

            //Productos último Mes
            List<Producto> _productosMasVendidosLastMes = new List<Producto>();
            var _productoXmes = Ventas.Where(v => v.Fecha.Contains(_mes));
            var _productoLastMes = _productoXmes.Where(v => v.Fecha.Contains(_anio)).ToList();


            foreach (var v in _productoLastMes) _productosMasVendidosLastMes.AddRange(v.ListaProductos);

            List<Producto> _productosAgrupados = new List<Producto>();
            foreach (var v in _productosMasVendidosLastMes)
            {
                var lis = _productosAgrupados.Where(p => p.Codigo == v.Codigo).ToList();
                if (lis.Count != 0)
                {
                    int _index = _productosAgrupados.FindIndex(p => p.Codigo == v.Codigo);
                    _productosAgrupados[_index].UnidadesVendidas += v.UnidadesVendidas;
                    if (_productosAgrupados[_index].Ganancia < v.Ganancia) _productosAgrupados[_index].Ganancia = v.Ganancia;
                }
                else _productosAgrupados.Add(v);
            }

            var _productosOrganizados = _productosAgrupados.OrderByDescending(p => p.UnidadesVendidas).ToList();
            if (_productosOrganizados.Count <= 21) Llenar_ProductosMasVendidosLastMesDataGridView(_productosOrganizados);
            else Llenar_ProductosMasVendidosLastMesDataGridView(_productosOrganizados.GetRange(0, 20));

            //Todos los productos
            //List<Producto> _productosMasVendidos = new List<Producto>();
            //List<Producto> _proMasVendidosUnidad = new List<Producto>();
            //foreach (var v in Ventas) _productosMasVendidos.AddRange(v.ListaProductos);
            //foreach (var _pro in _productosMasVendidos)
            //{
            //    var lis = _proMasVendidosUnidad.Where(p => p.Codigo == _pro.Codigo).ToList();
            //    if (lis.Count != 0)
            //    {
            //        int _index = _proMasVendidosUnidad.FindIndex(p => p.Codigo == _pro.Codigo);
            //        _proMasVendidosUnidad[_index].UnidadesVendidas += _pro.UnidadesVendidas;
            //        if (_proMasVendidosUnidad[_index].Ganancia < _pro.Ganancia) _proMasVendidosUnidad[_index].Ganancia = _pro.Ganancia;
            //    }
            //    else _proMasVendidosUnidad.Add(_pro);
            //}
            var _TodosProductosOrganizados = Productos.OrderByDescending(p => p.UnidadesVendidas).ToList();
            if (_TodosProductosOrganizados.Count <= 21) Llenar_ProductosMasVendidosDataGridView(_TodosProductosOrganizados);
            else Llenar_ProductosMasVendidosDataGridView(_TodosProductosOrganizados.GetRange(0, 20));

            DibujarGraficas();
        }

        public void LlenarGraficaVentas(List<int> _puntos, List<string> _axisLabel, string _titulo)
        {
            Ventas_Chart.Titles["Titulo"].Text = _titulo;

            for (int i = 0; i <= 5; i++)
            {
                Ventas_Chart.Series["ChartVentas"].Points.Add(_puntos[i]);
                Ventas_Chart.Series["ChartVentas"].Points[i].AxisLabel = _axisLabel[i];
                Ventas_Chart.Series["ChartVentas"].Points[i].Label = _puntos[i].ToString("N0");
            }
        }

        public void LlenarGraficaGanancias(List<int> _puntos, List<string> _axisLabel, string _titulo)
        {
            Ganancias_Chart.Titles["Titulo"].Text = _titulo;

            for (int i = 0; i <= 5; i++)
            {
                Ganancias_Chart.Series["ChartGanancias"].Points.Add(_puntos[i]);
                Ganancias_Chart.Series["ChartGanancias"].Points[i].AxisLabel = _axisLabel[i];
                Ganancias_Chart.Series["ChartGanancias"].Points[i].Label = _puntos[i].ToString("N0");
            }
        }

        public void LlenarGraficaOrigen(List<int[]> _puntos, List<string> _axisLabel, string _titulo)
        {
            Origenes_Chart.Titles["Titulo"].Text = _titulo;

            for (int i = 0; i <= 3; i++)
            {
                Origenes_Chart.Series["MercadoLibre"].Points.Add(_puntos[i][0]);
                Origenes_Chart.Series["MercadoLibre"].Points[i].AxisLabel = _axisLabel[i];
                Origenes_Chart.Series["MercadoLibre"].Points[i].Label = _puntos[i][0].ToString("N0");

                Origenes_Chart.Series["Facebook"].Points.Add(_puntos[i][1]);
                Origenes_Chart.Series["Facebook"].Points[i].AxisLabel = _axisLabel[i];
                Origenes_Chart.Series["Facebook"].Points[i].Label = _puntos[i][1].ToString("N0");

                Origenes_Chart.Series["Instagram"].Points.Add(_puntos[i][2]);
                Origenes_Chart.Series["Instagram"].Points[i].AxisLabel = _axisLabel[i];
                Origenes_Chart.Series["Instagram"].Points[i].Label = _puntos[i][2].ToString("N0");

                Origenes_Chart.Series["Otros"].Points.Add(_puntos[i][3]);
                Origenes_Chart.Series["Otros"].Points[i].AxisLabel = _axisLabel[i];
                Origenes_Chart.Series["Otros"].Points[i].Label = _puntos[i][3].ToString("N0");
            }
        }

        public void DibujarGraficas()
        {
            DateTime _fecha = DateTime.Now;
            List<string> Mes = new List<string>()
            { "ago" ,"sep", "oct", "nov", "dic", "ene", "feb", "mar", "abr", "may", "jun", "jul", "ago", "sep", "oct", "nov", "dic" };
            int _mes =  _fecha.Month + 4;
            int _anio = _fecha.Year;

            List<Producto> _ventasUltimosMeses = new List<Producto>();
            var _productoXmes = Ventas.Where
                (v => v.Fecha.Contains(Mes[_mes]) || v.Fecha.Contains(Mes[_mes - 1]) || v.Fecha.Contains(Mes[_mes - 2])
                || v.Fecha.Contains(Mes[_mes - 3]) || v.Fecha.Contains(Mes[_mes - 4]) || v.Fecha.Contains(Mes[_mes - 5]));

            int _m = _mes - 5;
            ProductosMasVendidosLastMes_GroupBox.Text += " (" + Mes[_mes - 1] + ")";
            List<VentaNew> _ventasG = new List<VentaNew>();
            string _titulo = "PERIODO ";
            if (_m <= 4)
            {
                var _aniom = _anio - 1;
                _titulo += _aniom + "/" + _anio;
                _ventasG = _productoXmes.Where(v => v.Fecha.Contains(_anio.ToString()) || v.Fecha.Contains(_aniom.ToString())).ToList();
            }
            else 
            {
                _titulo += _anio;
                _ventasG = _productoXmes.Where(v => v.Fecha.Contains(_anio.ToString())).ToList(); 
            }

            var _Mes6 = _ventasG.Where(v => v.Fecha.Contains(Mes[_mes])).ToList();
            var _Mes5 = _ventasG.Where(v => v.Fecha.Contains(Mes[_mes - 1])).ToList();
            var _Mes4 = _ventasG.Where(v => v.Fecha.Contains(Mes[_mes - 2])).ToList();
            var _Mes3 = _ventasG.Where(v => v.Fecha.Contains(Mes[_mes - 3])).ToList();
            var _Mes2 = _ventasG.Where(v => v.Fecha.Contains(Mes[_mes - 4])).ToList();
            var _Mes1 = _ventasG.Where(v => v.Fecha.Contains(Mes[_mes - 5])).ToList();

            List<int> _puntosVentas = new List<int>() 
            { 
                _Mes1.Count,
                _Mes2.Count,
                _Mes3.Count,
                _Mes4.Count,
                _Mes5.Count,
                _Mes6.Count
            };
            List<string> _axisLabel = new List<string>()
            {
                Mes[_mes - 5],
                Mes[_mes - 4],
                Mes[_mes - 3],
                Mes[_mes - 2],
                Mes[_mes - 1],
                Mes[_mes]
            };

            int _insta1 = 0, _face1 = 0, _ml1 = 0, _otr1 = 0; 
            foreach (var m6 in _Mes6)
            {
                if (m6.ClientNew != null)
                {
                    switch (m6.ClientNew.Origen)
                    {
                        case "Facebook":
                            _face1++;
                            break;
                        case "Instagram":
                            _insta1++;
                            break;
                        case "Mercado Libre":
                            _ml1++;
                            break;
                        case "Punto Fisico":
                            _otr1++;
                            break;
                    }
                }
            }
            int _insta2 = 0, _face2 = 0, _ml2 = 0, _otr2 = 0;
            foreach (var m6 in _Mes5)
            {
                if (m6.ClientNew != null)
                {
                    switch (m6.ClientNew.Origen)
                    {
                        case "Facebook":
                            _face2++;
                            break;
                        case "Instagram":
                            _insta2++;
                            break;
                        case "Mercado Libre":
                            _ml2++;
                            break;
                        case "Punto Fisico":
                            _otr2++;
                            break;
                    }
                }
            }
            int _insta3 = 0, _face3 = 0, _ml3 = 0, _otr3 = 0;
            foreach (var m6 in _Mes4)
            {
                if (m6.ClientNew != null)
                {
                    switch (m6.ClientNew.Origen)
                    {
                        case "Facebook":
                            _face3++;
                            break;
                        case "Instagram":
                            _insta3++;
                            break;
                        case "Mercado Libre":
                            _ml3++;
                            break;
                        case "Punto Fisico":
                            _otr3++;
                            break;
                    }
                }
            }
            int _insta4 = 0, _face4 = 0, _ml4 = 0, _otr4 = 0;
            foreach (var m6 in _Mes3)
            {
                if (m6.ClientNew != null)
                {
                    switch (m6.ClientNew.Origen)
                    {
                        case "Facebook":
                            _face4++;
                            break;
                        case "Instagram":
                            _insta4++;
                            break;
                        case "Mercado Libre":
                            _ml4++;
                            break;
                        case "Punto Fisico":
                            _otr4++;
                            break;
                    }
                }
            }

            List<int[]> _puntosOrigen = new List<int[]>() 
            {
                new int[4] { _ml4, _face4, _insta4, _otr4},
                new int[4] { _ml3, _face3, _insta3, _otr3},
                new int[4] { _ml2, _face2, _insta2, _otr2},
                new int[4] { _ml1, _face1, _insta1, _otr1}             
            };

            int m6g = 0, m5g = 0, m4g = 0, m3g = 0, m2g = 0, m1g = 0;
            //////////////////////////////////////////////////////////////
            //int u = 0;
            //List<Venta> _ven = new List<Venta>();
            //foreach (var m6 in _Mes6)
            //{
            //    var t = m6.Ganancia;
            //    if (t <= 0)
            //    {
            //        _ven.Add(m6);
            //        int y = 0;
            //    }
            //    m6g += t;
            //    u++;
            //}
            //////////////////////////////////////////////////////////////
            foreach (var m6 in _Mes6)
            {
                if (m6.Ganancia >=0 ) m6g += m6.Ganancia;
            }
            foreach (var m5 in _Mes5)
            {
                if (m5.Ganancia >= 0) m5g += m5.Ganancia;
            }
            foreach (var m4 in _Mes4) 
            {
                if (m4.Ganancia >= 0) m4g += m4.Ganancia;
            }
            foreach (var m3 in _Mes3) 
            {
                if (m3.Ganancia >= 0) m3g += m3.Ganancia;
            }
            foreach (var m2 in _Mes2) 
            {
                if (m2.Ganancia >= 0) m2g += m2.Ganancia;
            }
            foreach (var m1 in _Mes1) 
            {
                if (m1.Ganancia >= 0) m1g += m1.Ganancia; 
            }

            List<int> _puntosGanancias = new List<int>()
            {
                m1g,
                m2g,
                m3g,
                m4g,
                m5g,
                m6g
            };

            LlenarGraficaVentas(_puntosVentas, _axisLabel, "Ventas " + _titulo);
            LlenarGraficaGanancias(_puntosGanancias,_axisLabel,"Ganancias " + _titulo);
            _axisLabel.RemoveRange(0,2);
            LlenarGraficaOrigen(_puntosOrigen, _axisLabel, _titulo);
            Seleccionar_ComboBox.Text = "Ventas";
        }

        private void Seleccionar_ComboBox_TextChanged(object sender, EventArgs e)
        {
            if (Seleccionar_ComboBox.Text != "Ventas") Ganancias_Chart.Visible = true;
            else Ganancias_Chart.Visible = false;
        }
        #endregion
    }
}
