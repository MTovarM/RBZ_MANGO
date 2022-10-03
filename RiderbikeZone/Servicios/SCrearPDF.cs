namespace RiderbikeZone.Servicios
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using iTextSharp;
    using iTextSharp.text;
    using iTextSharp.text.pdf;
    using RiderbikeZone.Modelos;

    public class SCrearPDF
    {
        /*
        public void CrearPDF(string _tipoFactura, string _numeroSerie, string _nombreCliente, string _NIT,
            string _direccion, string _ciudad, string _contacto, string _fechaDeEmision, string _observaciones,
            string _vendedor, string _formaDePago, List<Producto> _productos, int _total, string _telefonoEmpresa,
            string _correoEmpresa, List<int> _unidades, List<int> _valores, double _iva)
        {

            SEscribirTXT TXT = new SEscribirTXT();
            bool IsAcEconomicaCheck;

            var _actividadEconomica = TXT.Leer("ACECONOMICA.txt");
            IsAcEconomicaCheck = Convert.ToBoolean(TXT.Leer("ACECONOMICAC.txt"));
            if (_tipoFactura == "VENTA") TXT.Escribirs("CONSECUTIVO.txt", _numeroSerie);
            string NumeroSerie = string.Empty;
            if (_numeroSerie.Length == 1) NumeroSerie += "000" + _numeroSerie;
            else if (_numeroSerie.Length == 2) NumeroSerie += "00" + _numeroSerie;
            else if (_numeroSerie.Length == 3) NumeroSerie += "0" + _numeroSerie;
            else NumeroSerie = _numeroSerie;

            List<int> _valoresSinIVA = new List<int>();
            List<int> IVAxProducto = new List<int>();
            List<int> ValorTotalSinIVA = new List<int>();
            int Subtotal = 0;
            int TotalIVA = 0;
            var ty = _iva * 100;
            string _IVA = ty.ToString() + "%";
            for (int ii = 0; ii <= _valores.Count - 1; ii++)
            {
                var v1 = _valores[ii] / _unidades[ii];
                var _productoSinIVA = v1 / (1 + _iva);
                _valoresSinIVA.Add(Convert.ToInt32(_productoSinIVA));
                var v2 = _productoSinIVA * _iva;
                IVAxProducto.Add(Convert.ToInt32(v2));
                var v3 = _productoSinIVA * _unidades[ii];
                ValorTotalSinIVA.Add(Convert.ToInt32(v3));
                TotalIVA += Convert.ToInt32(v2) * _unidades[ii];
                Subtotal += Convert.ToInt32(_productoSinIVA) * _unidades[ii];
            }

            Document doc = new Document(PageSize.LETTER, 25.34F, 28.34F, 30.68F, 56.68F);
            string Dir = @"C:\RBZ";
            string _NombreCliente = string.Empty;
            var randomNumber = new Random().Next(0, 1000);
            if (string.IsNullOrEmpty(_nombreCliente)) _NombreCliente = "Cliente" + randomNumber;
            else _NombreCliente = _nombreCliente;
            Random rnd = new Random();
            int num = rnd.Next(1000, 9999);
            if (_tipoFactura == "VENTA") Dir += @"\Archivos\Ventas\" + _fechaDeEmision.Replace('/', '_') + "_" + _NombreCliente + NumeroSerie + ".pdf";
            else Dir += @"\Archivos\Cotizaciones\" + _fechaDeEmision.Replace('/', '_') + "_" + _NombreCliente + num + ".pdf";
            PdfWriter writer = PdfWriter.GetInstance(doc, new FileStream(Dir, FileMode.Create));
            //PdfWriter writer = PdfWriter.GetInstance(doc,
            //                            new FileStream(@"Factura.pdf", FileMode.Create));
            doc.AddTitle(_fechaDeEmision + "_" + _NombreCliente);
            doc.AddCreator("MAV Table TEAM");
            doc.Open();
            doc.Add(Chunk.NEWLINE);

            // FUENTES
            BaseFont _separacion = BaseFont.CreateFont(BaseFont.HELVETICA, BaseFont.CP1250, true);
            Font Separación = new Font(_separacion, 3f, iTextSharp.text.Font.NORMAL, new BaseColor(0, 0, 0));
            BaseFont _datosActividadEconomica = BaseFont.CreateFont(BaseFont.HELVETICA, BaseFont.CP1250, true);
            Font DatosActividadEconomica = new Font(_datosActividadEconomica, 9F, iTextSharp.text.Font.NORMAL, new BaseColor(50, 50, 50));
            BaseFont _fuenteNormalGrande = BaseFont.CreateFont(BaseFont.HELVETICA, BaseFont.CP1250, true);
            Font FuenteNormalGrande = new Font(_fuenteNormalGrande, 12F, iTextSharp.text.Font.NORMAL, new BaseColor(0, 0, 0));
            BaseFont _fuenteDatos = BaseFont.CreateFont(BaseFont.HELVETICA, BaseFont.CP1250, true);
            Font FuenteDatos = new Font(_fuenteDatos, 10.5F, iTextSharp.text.Font.BOLD, new BaseColor(0, 0, 0));
            BaseFont _fuenteDatosFill = BaseFont.CreateFont(BaseFont.HELVETICA, BaseFont.CP1250, true);
            Font FuenteDatosFill = new Font(_fuenteDatosFill, 9.5F, iTextSharp.text.Font.NORMAL, new BaseColor(0, 0, 0));
            BaseFont _fuenteSubtitulo = BaseFont.CreateFont(BaseFont.HELVETICA, BaseFont.CP1250, true);
            Font FuenteSubtitulo = new Font(_fuenteSubtitulo, 12F, iTextSharp.text.Font.BOLD, new BaseColor(0, 0, 0));
            BaseFont _titulosFont = BaseFont.CreateFont(BaseFont.HELVETICA, BaseFont.CP1250, true);
            Font FuenteTitulo = new Font(_titulosFont, 23F, iTextSharp.text.Font.BOLD, new BaseColor(0, 0, 0));
            BaseFont _totalFont = BaseFont.CreateFont(BaseFont.HELVETICA, BaseFont.CP1250, true);
            Font FuenteTotal = new Font(_totalFont, 11F, iTextSharp.text.Font.BOLD, new BaseColor(0, 0, 0));
            BaseFont _fuentePiePag = BaseFont.CreateFont(BaseFont.HELVETICA, BaseFont.CP1250, true);
            Font FuentePiePag = new Font(_fuentePiePag, 9F, iTextSharp.text.Font.BOLD, new BaseColor(0, 0, 0));

            // IMAGEN
            //iTextSharp.text.Image Logo = iTextSharp.text.Image.GetInstance(@"Recursos\logo_png.png");
            iTextSharp.text.Image Logo = iTextSharp.text.Image.GetInstance("logo_png.png");
            Logo.SetAbsolutePosition(15f, 492f);
            Logo.ScalePercent(11);
            doc.Add(Logo);

            //iTextSharp.text.Image Name = iTextSharp.text.Image.GetInstance(@"Recursos\Name.png");
            iTextSharp.text.Image Name = iTextSharp.text.Image.GetInstance("Name.png");
            Name.SetAbsolutePosition(120f, 715f);
            Name.ScalePercent(25);
            doc.Add(Name);

            // TABLAS
            PdfPTable TablaEncabezado = new PdfPTable(new float[] { 100f }) { WidthPercentage = 103f };
            TablaEncabezado.AddCell(new PdfPCell(new Phrase(_tipoFactura + " No.", FuenteSubtitulo))
            { BorderColor = new BaseColor(0, 0, 0), Padding = 3, HorizontalAlignment = Element.ALIGN_CENTER, BackgroundColor = new BaseColor(200, 200, 200) });
            TablaEncabezado.AddCell(new PdfPCell(new Phrase(NumeroSerie, FuenteNormalGrande)) { BorderColor = new BaseColor(0, 0, 0), Padding = 3, HorizontalAlignment = Element.ALIGN_CENTER });

            doc.Add(new Phrase(" "));

            PdfPTable TablaDatosEmpresa = new PdfPTable(new float[] { 70f, 30f }) { WidthPercentage = 103f };
            TablaDatosEmpresa.AddCell(new PdfPCell(new Phrase(" ", FuenteTitulo)) { Border = 0, Rowspan = 2, VerticalAlignment = Element.ALIGN_MIDDLE, PaddingLeft = 90f });
            if (IsAcEconomicaCheck)
            {
                TablaDatosEmpresa.AddCell(new PdfPCell(new Phrase("Actividad económica", DatosActividadEconomica)) { Border = 0, HorizontalAlignment = Element.ALIGN_RIGHT });
                TablaDatosEmpresa.AddCell(new PdfPCell(new Phrase(_actividadEconomica, DatosActividadEconomica)) { Border = 0, HorizontalAlignment = Element.ALIGN_RIGHT });
            }
            else
            {
                TablaDatosEmpresa.AddCell(new PdfPCell(new Phrase(" ", DatosActividadEconomica)) { Border = 0, HorizontalAlignment = Element.ALIGN_RIGHT });
                TablaDatosEmpresa.AddCell(new PdfPCell(new Phrase(" ", DatosActividadEconomica)) { Border = 0, HorizontalAlignment = Element.ALIGN_RIGHT });
            }
            doc.Add(TablaDatosEmpresa);

            doc.Add(new Phrase(" "));

            PdfPTable TablaDatosFactura = new PdfPTable(new float[] { 50f, 50f }) { WidthPercentage = 103f };
            var TNombreNIT = new PdfPTable(new float[] { 20f, 80f });
            var TOtrosDatos = new PdfPTable(new float[] { 40f, 30f, 30f });
            var TFecha = new PdfPTable(new float[] { 100f });
            var TVendedor = new PdfPTable(new float[] { 65f, 35f });
            TNombreNIT.AddCell(new PdfPCell(new Phrase("Cliente", FuenteDatos)) { Padding = 2, BackgroundColor = new BaseColor(200, 200, 200), HorizontalAlignment = Element.ALIGN_LEFT });
            TNombreNIT.AddCell(new PdfPCell(new Phrase(_nombreCliente, FuenteDatosFill)) { Padding = 2, HorizontalAlignment = Element.ALIGN_CENTER });
            TNombreNIT.AddCell(new PdfPCell(new Phrase("NIT", FuenteDatos)) { Padding = 2, BackgroundColor = new BaseColor(200, 200, 200), HorizontalAlignment = Element.ALIGN_LEFT });
            TNombreNIT.AddCell(new PdfPCell(new Phrase(_NIT, FuenteDatosFill)) { Padding = 2, HorizontalAlignment = Element.ALIGN_CENTER });
            TOtrosDatos.AddCell(new PdfPCell(new Phrase("Dirección", FuenteDatos)) { Padding = 2, BackgroundColor = new BaseColor(200, 200, 200), HorizontalAlignment = Element.ALIGN_CENTER });
            TOtrosDatos.AddCell(new PdfPCell(new Phrase("Ciudad", FuenteDatos)) { Padding = 2, BackgroundColor = new BaseColor(200, 200, 200), HorizontalAlignment = Element.ALIGN_CENTER });
            TOtrosDatos.AddCell(new PdfPCell(new Phrase("Teléfono", FuenteDatos)) { Padding = 2, BackgroundColor = new BaseColor(200, 200, 200), HorizontalAlignment = Element.ALIGN_CENTER });
            TOtrosDatos.AddCell(new PdfPCell(new Phrase(_direccion, FuenteDatosFill)) { Padding = 2, HorizontalAlignment = Element.ALIGN_CENTER });
            TOtrosDatos.AddCell(new PdfPCell(new Phrase(_ciudad, FuenteDatosFill)) { Padding = 2, HorizontalAlignment = Element.ALIGN_CENTER });
            TOtrosDatos.AddCell(new PdfPCell(new Phrase(_contacto, FuenteDatosFill)) { Padding = 2, HorizontalAlignment = Element.ALIGN_CENTER });
            TFecha.AddCell(new PdfPCell(new Phrase("Fecha de emisión", FuenteDatos)) { Padding = 2, BackgroundColor = new BaseColor(200, 200, 200), HorizontalAlignment = Element.ALIGN_CENTER });
            TFecha.AddCell(new PdfPCell(new Phrase(_fechaDeEmision, FuenteDatosFill)) { Padding = 2, HorizontalAlignment = Element.ALIGN_CENTER });
            TVendedor.AddCell(new PdfPCell(new Phrase("Vendedor", FuenteDatos)) { Padding = 2, BackgroundColor = new BaseColor(200, 200, 200), HorizontalAlignment = Element.ALIGN_CENTER });
            TVendedor.AddCell(new PdfPCell(new Phrase("Forma de pago", FuenteDatos)) { Padding = 2, BackgroundColor = new BaseColor(200, 200, 200), HorizontalAlignment = Element.ALIGN_CENTER });
            TVendedor.AddCell(new PdfPCell(new Phrase(_vendedor, FuenteDatosFill)) { Padding = 2, HorizontalAlignment = Element.ALIGN_CENTER });
            TVendedor.AddCell(new PdfPCell(new Phrase(_formaDePago, FuenteDatosFill)) { Padding = 2, HorizontalAlignment = Element.ALIGN_CENTER });
            TablaDatosFactura.AddCell(new PdfPCell(TNombreNIT) { Border = 0, Padding = 1 });
            TablaDatosFactura.AddCell(new PdfPCell(TablaEncabezado) { Rowspan = 2, Border = 0, Padding = 1 });
            TablaDatosFactura.AddCell(new PdfPCell(TOtrosDatos) { Border = 0, Padding = 1 });
            TablaDatosFactura.AddCell(new PdfPCell(TFecha) { Border = 0, Padding = 1 });
            TablaDatosFactura.AddCell(new PdfPCell(TVendedor) { Border = 0, Padding = 1 });
            TablaDatosFactura.AddCell(new PdfPCell(new Phrase(" ", Separación)) { Border = 0, Padding = 1 });
            TablaDatosFactura.AddCell(new PdfPCell(new Phrase(" ", Separación)) { Border = 0, Padding = 1 });
            doc.Add(TablaDatosFactura);

            // CODIGO  |  NOMBRE  |  CANTIDAD  |  VALORUNITARIO  |  IVA  |  VALOR IVA  |  TOTAL 
            //   17    |    34    |     7      |       11        |   7   |      11     |   13 
            PdfPTable TablaProductos = new PdfPTable(new float[] { 15f, 34f, 10f, 11f, 7f, 10f, 13f }) { WidthPercentage = 103f };
            TablaProductos.AddCell(new PdfPCell(new Phrase("Código", FuenteDatos)) { Padding = 3, BackgroundColor = new BaseColor(200, 200, 200), HorizontalAlignment = Element.ALIGN_CENTER });
            TablaProductos.AddCell(new PdfPCell(new Phrase("Nombre", FuenteDatos)) { Padding = 3, BackgroundColor = new BaseColor(200, 200, 200), HorizontalAlignment = Element.ALIGN_CENTER });
            TablaProductos.AddCell(new PdfPCell(new Phrase("Cantidad", FuenteDatos)) { Padding = 3, BackgroundColor = new BaseColor(200, 200, 200), HorizontalAlignment = Element.ALIGN_CENTER });
            TablaProductos.AddCell(new PdfPCell(new Phrase("Valor c/u", FuenteDatos)) { Padding = 3, BackgroundColor = new BaseColor(200, 200, 200), HorizontalAlignment = Element.ALIGN_CENTER });
            TablaProductos.AddCell(new PdfPCell(new Phrase("IVA", FuenteDatos)) { Padding = 3, BackgroundColor = new BaseColor(200, 200, 200), HorizontalAlignment = Element.ALIGN_CENTER });
            TablaProductos.AddCell(new PdfPCell(new Phrase("Valor IVA c/u", FuenteDatos)) { Padding = 3, BackgroundColor = new BaseColor(200, 200, 200), HorizontalAlignment = Element.ALIGN_CENTER });
            TablaProductos.AddCell(new PdfPCell(new Phrase("Total", FuenteDatos)) { Padding = 3, BackgroundColor = new BaseColor(200, 200, 200), HorizontalAlignment = Element.ALIGN_CENTER });
            int i = 0;
            for (int NumeroFilas = 0; NumeroFilas < 23; NumeroFilas++)
            {
                if (i == 22)
                {
                    if (i <= _productos.Count - 1)
                    {
                        //codigo
                        TablaProductos.AddCell(new PdfPCell(new Phrase(_productos[i].Codigo, FuenteDatosFill))
                        { Border = PdfPCell.RIGHT_BORDER + PdfPCell.LEFT_BORDER + PdfPCell.BOTTOM_BORDER, Padding = 3, HorizontalAlignment = Element.ALIGN_LEFT });
                        if (_productos[i].Nombre.Length > 43)
                        {
                            //nombre
                            TablaProductos.AddCell(new PdfPCell(new Phrase(_productos[i].Nombre.Substring(0, 40), FuenteDatosFill))
                            { Border = PdfPCell.RIGHT_BORDER + PdfPCell.BOTTOM_BORDER, Padding = 2, HorizontalAlignment = Element.ALIGN_LEFT });
                            //cantidad
                            TablaProductos.AddCell(new PdfPCell(new Phrase(_unidades[i].ToString(), FuenteDatosFill))
                            { Border = PdfPCell.RIGHT_BORDER + PdfPCell.BOTTOM_BORDER, Padding = 2, HorizontalAlignment = Element.ALIGN_CENTER });
                            //valor c/u
                            TablaProductos.AddCell(new PdfPCell(new Phrase("$ " + _valoresSinIVA[i].ToString("N0"), FuenteDatosFill))
                            { Border = PdfPCell.RIGHT_BORDER + PdfPCell.BOTTOM_BORDER, Padding = 2, HorizontalAlignment = Element.ALIGN_RIGHT });
                            //iva
                            TablaProductos.AddCell(new PdfPCell(new Phrase(_IVA, FuenteDatosFill))
                            { Border = PdfPCell.RIGHT_BORDER + PdfPCell.BOTTOM_BORDER, Padding = 2, HorizontalAlignment = Element.ALIGN_CENTER });
                            //valorIVA
                            TablaProductos.AddCell(new PdfPCell(new Phrase("$ " + IVAxProducto[i].ToString("N0"), FuenteDatosFill))
                            { Border = PdfPCell.RIGHT_BORDER + PdfPCell.BOTTOM_BORDER, Padding = 2, HorizontalAlignment = Element.ALIGN_RIGHT });
                            //Total
                            TablaProductos.AddCell(new PdfPCell(new Phrase("$ " + ValorTotalSinIVA[i].ToString("N0"), FuenteDatosFill))
                            { Border = PdfPCell.RIGHT_BORDER + PdfPCell.BOTTOM_BORDER, Padding = 2, HorizontalAlignment = Element.ALIGN_RIGHT });
                        }
                        else
                        {
                            //nombre
                            TablaProductos.AddCell(new PdfPCell(new Phrase(_productos[i].Nombre, FuenteDatosFill))
                            { Border = PdfPCell.RIGHT_BORDER + PdfPCell.BOTTOM_BORDER, Padding = 2, HorizontalAlignment = Element.ALIGN_LEFT });
                            //cantidad
                            TablaProductos.AddCell(new PdfPCell(new Phrase(_unidades[i].ToString(), FuenteDatosFill))
                            { Border = PdfPCell.RIGHT_BORDER + PdfPCell.BOTTOM_BORDER, Padding = 2, HorizontalAlignment = Element.ALIGN_CENTER });
                            //valor c/u
                            TablaProductos.AddCell(new PdfPCell(new Phrase("$ " + _valoresSinIVA[i].ToString("N0"), FuenteDatosFill))
                            { Border = PdfPCell.RIGHT_BORDER + PdfPCell.BOTTOM_BORDER, Padding = 2, HorizontalAlignment = Element.ALIGN_RIGHT });
                            //iva
                            TablaProductos.AddCell(new PdfPCell(new Phrase(_IVA, FuenteDatosFill))
                            { Border = PdfPCell.RIGHT_BORDER + PdfPCell.BOTTOM_BORDER, Padding = 2, HorizontalAlignment = Element.ALIGN_CENTER });
                            //valorIVA
                            TablaProductos.AddCell(new PdfPCell(new Phrase("$ " + IVAxProducto[i].ToString("N0"), FuenteDatosFill))
                            { Border = PdfPCell.RIGHT_BORDER + PdfPCell.BOTTOM_BORDER, Padding = 2, HorizontalAlignment = Element.ALIGN_RIGHT });
                            //Total
                            TablaProductos.AddCell(new PdfPCell(new Phrase("$ " + ValorTotalSinIVA[i].ToString("N0"), FuenteDatosFill))
                            { Border = PdfPCell.RIGHT_BORDER + PdfPCell.BOTTOM_BORDER, Padding = 2, HorizontalAlignment = Element.ALIGN_RIGHT });
                            NumeroFilas++;
                        }
                    }
                    else
                    {
                        TablaProductos.AddCell(new PdfPCell(new Phrase(" ", FuenteDatosFill))
                        { Border = PdfPCell.RIGHT_BORDER, Padding = 2 });
                        //nombre
                        TablaProductos.AddCell(new PdfPCell(new Phrase(" ", FuenteDatosFill))
                        { Border = PdfPCell.RIGHT_BORDER, Padding = 2 });
                        //cantidad
                        TablaProductos.AddCell(new PdfPCell(new Phrase(" ", FuenteDatosFill))
                        { Border = PdfPCell.RIGHT_BORDER, Padding = 2 });
                        //valor c/u
                        TablaProductos.AddCell(new PdfPCell(new Phrase(" ", FuenteDatosFill))
                        { Border = PdfPCell.RIGHT_BORDER, Padding = 2 });
                        //iva
                        TablaProductos.AddCell(new PdfPCell(new Phrase(" ", FuenteDatosFill))
                        { Border = PdfPCell.RIGHT_BORDER, Padding = 2 });
                        //valorIVA
                        TablaProductos.AddCell(new PdfPCell(new Phrase(" ", FuenteDatosFill))
                        { Border = PdfPCell.RIGHT_BORDER, Padding = 2, });
                        //Total
                        TablaProductos.AddCell(new PdfPCell(new Phrase(" ", FuenteDatosFill))
                        { Border = PdfPCell.RIGHT_BORDER, Padding = 2 });
                    }
                }
                else
                {
                    if (i <= _productos.Count - 1)
                    {
                        TablaProductos.AddCell(new PdfPCell(new Phrase(_productos[i].Codigo, FuenteDatosFill))
                        { Border = PdfPCell.RIGHT_BORDER + PdfPCell.LEFT_BORDER, Padding = 3, HorizontalAlignment = Element.ALIGN_LEFT });
                        if (_productos[i].Nombre.Length > 43)
                        {
                            //nombre
                            TablaProductos.AddCell(new PdfPCell(new Phrase(_productos[i].Nombre.Substring(0, 40), FuenteDatosFill))
                            { Border = PdfPCell.RIGHT_BORDER, Padding = 2, HorizontalAlignment = Element.ALIGN_LEFT });
                            //cantidad
                            TablaProductos.AddCell(new PdfPCell(new Phrase(_unidades[i].ToString(), FuenteDatosFill))
                            { Border = PdfPCell.RIGHT_BORDER, Padding = 2, HorizontalAlignment = Element.ALIGN_CENTER });
                            //valor c/u
                            TablaProductos.AddCell(new PdfPCell(new Phrase("$ " + _valoresSinIVA[i].ToString("N0"), FuenteDatosFill))
                            { Border = PdfPCell.RIGHT_BORDER, Padding = 2, HorizontalAlignment = Element.ALIGN_RIGHT });
                            //iva
                            TablaProductos.AddCell(new PdfPCell(new Phrase(_IVA, FuenteDatosFill))
                            { Border = PdfPCell.RIGHT_BORDER, Padding = 2, HorizontalAlignment = Element.ALIGN_CENTER });
                            //valorIVA
                            TablaProductos.AddCell(new PdfPCell(new Phrase("$ " + IVAxProducto[i].ToString("N0"), FuenteDatosFill))
                            { Border = PdfPCell.RIGHT_BORDER, Padding = 2, HorizontalAlignment = Element.ALIGN_RIGHT });
                            //Total
                            TablaProductos.AddCell(new PdfPCell(new Phrase("$ " + ValorTotalSinIVA[i].ToString("N0"), FuenteDatosFill))
                            { Border = PdfPCell.RIGHT_BORDER, Padding = 2, HorizontalAlignment = Element.ALIGN_RIGHT });
                        }
                        else
                        {
                            //nombre
                            TablaProductos.AddCell(new PdfPCell(new Phrase(_productos[i].Nombre, FuenteDatosFill))
                            { Border = PdfPCell.RIGHT_BORDER, Padding = 2, HorizontalAlignment = Element.ALIGN_LEFT });
                            //cantidad
                            TablaProductos.AddCell(new PdfPCell(new Phrase(_unidades[i].ToString(), FuenteDatosFill))
                            { Border = PdfPCell.RIGHT_BORDER, Padding = 2, HorizontalAlignment = Element.ALIGN_CENTER });
                            //valor c/u
                            TablaProductos.AddCell(new PdfPCell(new Phrase("$ " + _valoresSinIVA[i].ToString("N0"), FuenteDatosFill))
                            { Border = PdfPCell.RIGHT_BORDER, Padding = 2, HorizontalAlignment = Element.ALIGN_RIGHT });
                            //iva
                            TablaProductos.AddCell(new PdfPCell(new Phrase(_IVA, FuenteDatosFill))
                            { Border = PdfPCell.RIGHT_BORDER, Padding = 2, HorizontalAlignment = Element.ALIGN_CENTER });
                            //valorIVA
                            TablaProductos.AddCell(new PdfPCell(new Phrase("$ " + IVAxProducto[i].ToString("N0"), FuenteDatosFill))
                            { Border = PdfPCell.RIGHT_BORDER, Padding = 2, HorizontalAlignment = Element.ALIGN_RIGHT });
                            //Total
                            TablaProductos.AddCell(new PdfPCell(new Phrase("$ " + ValorTotalSinIVA[i].ToString("N0"), FuenteDatosFill))
                            { Border = PdfPCell.RIGHT_BORDER, Padding = 2, HorizontalAlignment = Element.ALIGN_RIGHT });
                            NumeroFilas++;
                        }
                    }
                    else
                    {
                        TablaProductos.AddCell(new PdfPCell(new Phrase(" ", FuenteDatosFill))
                        { Border = PdfPCell.RIGHT_BORDER + PdfPCell.LEFT_BORDER, Padding = 3 });
                        TablaProductos.AddCell(new PdfPCell(new Phrase(" ", FuenteDatosFill))
                        { Border = PdfPCell.RIGHT_BORDER, Padding = 2 });
                        TablaProductos.AddCell(new PdfPCell(new Phrase(" ", FuenteDatosFill))
                        { Border = PdfPCell.RIGHT_BORDER, Padding = 2 });
                        TablaProductos.AddCell(new PdfPCell(new Phrase(" ", FuenteDatosFill))
                        { Border = PdfPCell.RIGHT_BORDER, Padding = 2 });
                        TablaProductos.AddCell(new PdfPCell(new Phrase(" ", FuenteDatosFill))
                        { Border = PdfPCell.RIGHT_BORDER, Padding = 2 });
                        TablaProductos.AddCell(new PdfPCell(new Phrase(" ", FuenteDatosFill))
                        { Border = PdfPCell.RIGHT_BORDER, Padding = 2 });
                        TablaProductos.AddCell(new PdfPCell(new Phrase(" ", FuenteDatosFill))
                        { Border = PdfPCell.RIGHT_BORDER, Padding = 2 });
                    }
                }
                i++;
            }
            doc.Add(TablaProductos);

            PdfPTable TablaPiePagina = new PdfPTable(new float[] { 70f, 30f }) { WidthPercentage = 103f };

            PdfPTable TObservaciones = new PdfPTable(new float[] { 100f }) { WidthPercentage = 100f };
            TObservaciones.AddCell(new PdfPCell(new Phrase("Observaciones", FuenteDatos))
            { BorderColor = new BaseColor(0, 0, 0), Padding = 3, HorizontalAlignment = Element.ALIGN_CENTER, BackgroundColor = new BaseColor(200, 200, 200) });
            TObservaciones.AddCell(new PdfPCell(new Phrase(_observaciones, FuenteDatosFill)) { BorderColor = new BaseColor(0, 0, 0), Padding = 3, HorizontalAlignment = Element.ALIGN_CENTER });

            PdfPTable TablaTotales = new PdfPTable(new float[] { 53f, 47f }) { WidthPercentage = 100f };
            TablaTotales.AddCell(new PdfPCell(new Phrase("SUBTOTAL", FuenteDatos)) { Padding = 2, BackgroundColor = new BaseColor(200, 200, 200), HorizontalAlignment = Element.ALIGN_LEFT });
            TablaTotales.AddCell(new PdfPCell(new Phrase("$ " + Subtotal.ToString("N0"), FuenteTotal)) { Padding = 2, HorizontalAlignment = Element.ALIGN_RIGHT });
            TablaTotales.AddCell(new PdfPCell(new Phrase("IVA", FuenteDatos)) { Padding = 2, BackgroundColor = new BaseColor(200, 200, 200), HorizontalAlignment = Element.ALIGN_LEFT });
            TablaTotales.AddCell(new PdfPCell(new Phrase("$ " + TotalIVA.ToString("N0"), FuenteTotal)) { Padding = 2, HorizontalAlignment = Element.ALIGN_RIGHT });
            PdfPTable TablaTotales1 = new PdfPTable(new float[] { 53f, 47f }) { WidthPercentage = 100f };
            TablaTotales1.AddCell(new PdfPCell(new Phrase("TOTAL", FuenteDatos)) { Padding = 2, BackgroundColor = new BaseColor(200, 200, 200), HorizontalAlignment = Element.ALIGN_LEFT });
            TablaTotales1.AddCell(new PdfPCell(new Phrase("$ " + _total.ToString("N0"), FuenteTotal)) { Padding = 2, HorizontalAlignment = Element.ALIGN_RIGHT });

            TablaPiePagina.AddCell(new PdfPCell(TObservaciones) { Border = 0, Padding = 1 });
            TablaPiePagina.AddCell(new PdfPCell(TablaTotales) { Border = 0, Padding = 1 });
            TablaPiePagina.AddCell(new PdfPCell(new Paragraph(" ")) { Border = 0, Padding = 1 });
            TablaPiePagina.AddCell(new PdfPCell(TablaTotales1) { Border = 0, Padding = 1 });

            doc.Add(TablaPiePagina);

            doc.Add(new Paragraph("Facebook: Rider Bike Zone   Insta: @riderbike_zone   Riderbike Zone   Teléfono: " + _telefonoEmpresa, FuentePiePag) { Alignment = Element.ALIGN_CENTER });
            doc.Add(new Paragraph("Correo: " + _correoEmpresa, FuentePiePag) { Alignment = Element.ALIGN_CENTER });

            doc.Close();
            writer.Close();
        }
        */


        public void CrearPDF(string _tipoFactura, string _numeroSerie, string _nombreCliente, string _NIT,
            string _direccion, string _ciudad, string _contacto, string _fechaDeEmision, string _observaciones,
            string _vendedor, string _formaDePago, List<Producto> _productos, int _total, string _telefonoEmpresa,
            string _correoEmpresa, List<int> _unidades, List<int> _valores, double _iva)
        {
            if (_productos.Count <= 7)
            {
                WritePDF(_tipoFactura, _numeroSerie, _nombreCliente, _NIT, _direccion, _ciudad,_contacto, _fechaDeEmision, _observaciones,
                    _vendedor, _formaDePago, _productos, _total,_telefonoEmpresa, _correoEmpresa, _unidades, _valores, _iva);
            }
            else
            {
                List<List<Producto>> _ListPro = new List<List<Producto>>();
                List<List<int>> _ListUni = new List<List<int>>();
                List<List<int>> _ListVal = new List<List<int>>();
                for (int i = 0; i < _productos.Count; i += 7)
                {
                    _ListPro.Add(_productos.GetRange(i, Math.Min(7, _productos.Count - i)));
                    _ListUni.Add(_unidades.GetRange(i, Math.Min(7, _unidades.Count - i)));
                    _ListVal.Add(_valores.GetRange(i, Math.Min(7, _valores.Count - i)));
                }
                for (int i = 0; i < _ListPro.Count; i++)
                {
                    if (i == _ListPro.Count - 1)
                    {
                        WritePDF(_tipoFactura, _numeroSerie, _nombreCliente, _NIT, _direccion, _ciudad, _contacto, _fechaDeEmision, _observaciones,
                            _vendedor, _formaDePago, _ListPro[i], _total, (i + 1).ToString(), "-10", _ListUni[i], _ListVal[i], _iva);
                    }
                    else
                    {
                        WritePDF(_tipoFactura, _numeroSerie, _nombreCliente, _NIT, _direccion, _ciudad, _contacto, _fechaDeEmision, _observaciones,
                            _vendedor, _formaDePago, _ListPro[i], -1, (i + 1).ToString() , _ListPro.Count.ToString(), _ListUni[i], _ListVal[i], _iva);
                    }
                }
            }
        }        

        public void ColocarTexto(PdfWriter _writer, string _text, Font _fuente, int _x, int _y)
        {
            PdfContentByte canvas = _writer.DirectContent;
            var _frase = new Phrase(_text, _fuente);
            ColumnText.ShowTextAligned(canvas, Element.ALIGN_LEFT, _frase, _x, _y, 0);
        }

        public void WritePDF(string _tipoFactura, string _numeroSerie, string _nombreCliente, string _NIT,
            string _direccion, string _ciudad, string _contacto, string _fechaDeEmision, string _observaciones,
            string _vendedor, string _formaDePago, List<Producto> _productos, int _total, string _telefonoEmpresa,
            string _correoEmpresa, List<int> _unidades, List<int> _valores, double _iva)
        {
            SEscribirTXT TXT = new SEscribirTXT();
            bool IsAcEconomicaCheck;

            var _actividadEconomica = TXT.Leer("ACECONOMICA.txt");
            IsAcEconomicaCheck = Convert.ToBoolean(TXT.Leer("ACECONOMICAC.txt"));
            string NumeroSerie = string.Empty;
            if (_total != -1)
            {
                if (_tipoFactura == "VENTA") TXT.Escribirs("CONSECUTIVO.txt", _numeroSerie);
                if (_numeroSerie.Length == 1) NumeroSerie += "000" + _numeroSerie;
                else if (_numeroSerie.Length == 2) NumeroSerie += "00" + _numeroSerie;
                else if (_numeroSerie.Length == 3) NumeroSerie += "0" + _numeroSerie;
                else NumeroSerie = _numeroSerie;
            }

            List<int> Valorunidad = new List<int>();
            int po = 0;
            foreach (var v in _valores)
            {
                Valorunidad.Add(v / _unidades[po]);
            }

            List<int> _valoresSinIVA = new List<int>();
            List<int> IVAxProducto = new List<int>();
            List<int> ValorTotalSinIVA = new List<int>();
            int Subtotal = 0;
            int TotalIVA = 0;
            var ty = _iva * 100;
            string _IVA = ty.ToString() + "%";
            for (int ii = 0; ii <= _valores.Count - 1; ii++)
            {
                var v1 = _valores[ii] / _unidades[ii];
                var _productoSinIVA = v1 / (1 + _iva);
                _valoresSinIVA.Add(Convert.ToInt32(_productoSinIVA));
                var v2 = _productoSinIVA * _iva;
                IVAxProducto.Add(Convert.ToInt32(v2));
                var v3 = _productoSinIVA * _unidades[ii];
                ValorTotalSinIVA.Add(Convert.ToInt32(v3));
                TotalIVA += Convert.ToInt32(v2) * _unidades[ii];
                Subtotal += Convert.ToInt32(_productoSinIVA) * _unidades[ii];
            }
            //###############ESCRIBIR FACTURA
            Document doc = new Document(PageSize.LETTER, 25.34F, 28.34F, 30.68F, 56.68F);
            string Dir = @"C:\MANGO";
            string _NombreCliente = string.Empty;
            var randomNumber = new Random().Next(0, 1000);
            if (string.IsNullOrEmpty(_nombreCliente)) _NombreCliente = "Cliente" + randomNumber;
            else _NombreCliente = _nombreCliente;
            Random rnd = new Random();
            int num = rnd.Next(1000, 9999);
            if (_tipoFactura == "VENTA") Dir += @"\Archivos\Ventas\" + _fechaDeEmision.Replace('/', '_') + "_" + _NombreCliente + NumeroSerie + ".pdf";
            else Dir += @"\Archivos\Cotizaciones\" + _fechaDeEmision.Replace('/', '_') + "_" + _NombreCliente + num + ".pdf";
            PdfWriter writer = PdfWriter.GetInstance(doc, new FileStream(Dir, FileMode.Create));
            //PdfWriter writer = PdfWriter.GetInstance(doc,
            //                            new FileStream(@"Factura.pdf", FileMode.Create));
            doc.AddTitle(_fechaDeEmision + "_" + _NombreCliente);
            doc.AddCreator("MAV Table TEAM");
            doc.Open();

            //FUENTES
            BaseFont _fuente1 = BaseFont.CreateFont(BaseFont.TIMES_BOLD, BaseFont.CP1250, true);
            Font Fuente1 = new Font(_fuente1, 9.9F, iTextSharp.text.Font.NORMAL, new BaseColor(0, 0, 0));
            BaseFont _fuente2 = BaseFont.CreateFont(BaseFont.TIMES_BOLD, BaseFont.CP1250, true);
            Font Fuente2 = new Font(_fuente2, 11.6F, iTextSharp.text.Font.NORMAL, new BaseColor(0, 0, 0));
            BaseFont _fuente3 = BaseFont.CreateFont(BaseFont.TIMES_ROMAN, BaseFont.CP1250, true);
            Font Fuente3 = new Font(_fuente3, 9.9F, iTextSharp.text.Font.NORMAL, new BaseColor(0, 0, 0));
            BaseFont _fuente4 = BaseFont.CreateFont(BaseFont.TIMES_ROMAN, BaseFont.CP1250, true);
            Font Fuente4 = new Font(_fuente4, 10.8F, iTextSharp.text.Font.NORMAL, new BaseColor(0, 0, 0));
            BaseFont _fuente5 = BaseFont.CreateFont(BaseFont.TIMES_ROMAN, BaseFont.CP1250, true);
            Font Fuente5 = new Font(_fuente5, 11.6F, iTextSharp.text.Font.NORMAL, new BaseColor(0, 0, 0));
            BaseFont _fuente6 = BaseFont.CreateFont(BaseFont.TIMES_ROMAN, BaseFont.CP1250, true);
            Font Fuente6 = new Font(_fuente5, 13F, iTextSharp.text.Font.NORMAL, new BaseColor(0, 0, 0));

            //FACTURA BASE
            iTextSharp.text.Image FacturaBase;
            try
            {
                FacturaBase = iTextSharp.text.Image.GetInstance(@"Recursos\facturaBase.png");
            }
            catch (Exception)
            {
                FacturaBase = iTextSharp.text.Image.GetInstance("facturaBase.png");
            }
            
            
            FacturaBase.SetAbsolutePosition(12f, 12f);
            FacturaBase.ScaleAbsoluteWidth(588);
            FacturaBase.ScaleAbsoluteHeight(770);
            doc.Add(FacturaBase);

            //DATOS CLIENTE
            if (_nombreCliente.Length > 29) ColocarTexto(writer, _nombreCliente.Substring(0, 28), Fuente1, 110, 637);
            else ColocarTexto(writer, _nombreCliente, Fuente1, 103, 637);
            ColocarTexto(writer, _NIT, Fuente3, 420, 637);
            if (_direccion.Length > 27) ColocarTexto(writer, _direccion.Substring(0, 26).ToUpper(), Fuente3, 120, 621);
            else ColocarTexto(writer, _direccion.ToUpper(), Fuente3, 120, 621);
            ColocarTexto(writer, _ciudad.ToUpper(), Fuente3, 110, 606);
            if (_vendedor.Length > 29) ColocarTexto(writer, _vendedor.Substring(0, 28).ToUpper(), Fuente3, 390, 606);
            else ColocarTexto(writer, _vendedor.ToUpper(), Fuente3, 390, 606);
            ColocarTexto(writer, _contacto, Fuente1, 140, 590);
            ColocarTexto(writer, _fechaDeEmision.ToUpper(), Fuente4, 290, 546);
            if (_tipoFactura == "VENTA")
            {
                ColocarTexto(writer, _numeroSerie, Fuente4, 495, 545);
            }
            else
            {
                ColocarTexto(writer, "COTIZACIÓN", Fuente4, 495, 545);
            }

            //LLENAR PRODUCTOS
            int _index = 0, Posicion = 460, Diferencia = 0;
            foreach (var p in _productos)
            {
                iTextSharp.text.Image Linea;
                try
                {
                    Linea = iTextSharp.text.Image.GetInstance(@"Recursos\line.png");
                }
                catch (Exception)
                {
                    Linea = iTextSharp.text.Image.GetInstance("line.png");
                }

                var Nombre = p.Nombre.Trim(' ');
                bool IsDoubleLine = false;
                if (p.Nombre.Length > 38)
                {
                    var nom1 = Nombre.Substring(0, 38);
                    var nom2 = Nombre.Substring(39);
                    ColocarTexto(writer, nom1, Fuente4, 65, Posicion - Diferencia);
                    ColocarTexto(writer, nom2, Fuente4, 65, Posicion - Diferencia - 12);
                    ColocarTexto(writer, p.Marca, Fuente3, 65, Posicion - Diferencia - 22);
                    Linea.SetAbsolutePosition(60, Posicion - Diferencia - 32);
                    IsDoubleLine = true;
                }
                else
                {
                    ColocarTexto(writer, Nombre, Fuente4, 65, Posicion - Diferencia);
                    ColocarTexto(writer, p.Marca, Fuente3, 65, Posicion - Diferencia - 9);
                    Linea.SetAbsolutePosition(60, Posicion - Diferencia - 17);
                }
                ColocarTexto(writer, _unidades[_index].ToString(), Fuente4, 363, Posicion - Diferencia);
                ColocarTexto(writer, "$ " + _valoresSinIVA[_index].ToString("N0"), Fuente4, 403, Posicion - Diferencia);
                ColocarTexto(writer, "$ " + ValorTotalSinIVA[_index].ToString("N0"), Fuente4, 470, Posicion - Diferencia);
                Linea.ScaleAbsoluteWidth(465);
                Linea.ScaleAbsoluteHeight(7);
                doc.Add(Linea);

                if (_index == 0) Diferencia = 30;
                else Diferencia += 30;
                if (IsDoubleLine) Diferencia += 15;
                _index++;
            }
            if (_total == -1)
            {
                ColocarTexto(writer, "Página " + _telefonoEmpresa + " de " + _correoEmpresa + "   ------>", 
                    Fuente6, 200, Posicion - Diferencia - 10);
                ColocarTexto(writer, "$ " + "-----------", Fuente5, 468, 149);
                ColocarTexto(writer, "$ " + "-----------", Fuente5, 474, 128);
                ColocarTexto(writer, "$ " + "-----------", Fuente6, 463, 108);
            }
            else
            {
                if (_correoEmpresa == "-10")
                {
                    int _IvaTotal = Convert.ToInt32( _total / (1 + _iva));
                    int subTotal1 = _total - _IvaTotal;
                    ColocarTexto(writer, "Página " + _telefonoEmpresa + " de " + _telefonoEmpresa + "   ------>", Fuente6, 200, Posicion - Diferencia - 10);
                    ColocarTexto(writer, "$ " + _IvaTotal.ToString("N0"), Fuente5, 468, 149);
                    ColocarTexto(writer, "$ " + subTotal1.ToString("N0"), Fuente5, 474, 128);
                    ColocarTexto(writer, "$ " + _total.ToString("N0"), Fuente6, 463, 108);
                }
                else 
                {
                    ColocarTexto(writer, "$ " + Subtotal.ToString("N0"), Fuente5, 468, 149);
                    ColocarTexto(writer, "$ " + TotalIVA.ToString("N0"), Fuente5, 474, 128);
                    ColocarTexto(writer, "$ " + _total.ToString("N0"), Fuente6, 463, 108);
                }
            }

            doc.Close();
            writer.Close();
        }
    }
}
