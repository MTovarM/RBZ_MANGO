namespace RiderbikeZone.Servicios
{
    using Firebase.Auth;
    using Firebase.Database;
    using Firebase.Database.Query;
    using System;
    using System.Threading.Tasks;
    using System.Collections.Generic;
    using RiderbikeZone.Modelos;

    public class SFirebase : FirebaseAuth
    {
        #region Propiedades
        public static FirebaseAuthLink auth = null;
        public static string _userId = null;
        public string Email;
        public string Clave;
        public List<FirebaseObject<VentaNew>> Ventas { get; set; }
        public List<FirebaseObject<Compra>> Compras { get; set; }
        #endregion

        #region Métodos
        public async Task<string> LoginWithEmail(bool createUser)
        {
            var authProvider = new FirebaseAuthProvider(new FirebaseConfig("API_KEY"));
            try
            {
                if (createUser)
                {
                    auth = await authProvider.CreateUserWithEmailAndPasswordAsync(Email, Clave);
                }
                else
                {
                    auth = await authProvider.SignInWithEmailAndPasswordAsync(Email, Clave);
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex);
            }
            if (auth != null)
            {
                System.Diagnostics.Debug.WriteLine(auth.FirebaseToken);
                _userId = auth.User.LocalId;
                return auth.FirebaseToken;
            }
            else return null;
        }

        public async Task BorrarVentaNew(VentaNew _venta)
        {
            var firebase = ClienteAutenticadoConEmail();

            var lipro = await firebase
                .Child("UVenta")
                .OnceAsync<VentaNew>();

            FirebaseObject<VentaNew> ob = null;
            foreach (var p in lipro)
            {
                if (p.Object.Fecha == _venta.Fecha &&
                    p.Object.ClientNew.Nombre == _venta.ClientNew.Nombre &&
                    p.Object.Total == _venta.Total)
                {
                    ob = p;
                    break;
                }
            }
            await firebase
                .Child("UVenta")
                .Child(ob?.Key)
                .DeleteAsync();
        }

        public async Task DeleteClientNew(string _venta)
        {
            var firebase = ClienteAutenticadoConEmail();

            var lipro = await firebase
                .Child("UCliente")
                .OnceAsync<ClientNew>();

            FirebaseObject<ClientNew> ob = null;
            foreach (var p in lipro)
            {
                if (p.Object.Nombre.ToUpper() == _venta)
                {
                    ob = p;
                    break;
                }
            }
            await firebase
                .Child("UCliente")
                .Child(ob?.Key)
                .DeleteAsync();
        }

        public async Task<List<ClientNew>> ObtenerClientNew()
        {
            var firebase = ClienteAutenticadoConEmail();

            var Products = await firebase
                .Child("UCliente")
                .OnceAsync<ClientNew>();

            List<ClientNew> VentasFB = new List<ClientNew>();

            foreach (var PR in Products)
            {
                VentasFB.Add(PR.Object);
            }
            return VentasFB;
        }

        public async Task<List<VentaNew>> ObtenerVentasNew()
        {
            var firebase = ClienteAutenticadoConEmail();

            var Products = await firebase
                .Child("UVenta")
                .OnceAsync<VentaNew>();

            List<VentaNew> VentasFB = new List<VentaNew>();

            Ventas = new List<FirebaseObject<VentaNew>>();

            foreach (var PR in Products)
            {
                Ventas.Add(PR);
                VentasFB.Add(PR.Object);
            }
            return VentasFB;
        }

        public FirebaseClient ClienteAutenticadoConEmail()
        {
            var firebaseClient = new FirebaseClient(
                  "https://riderbikezone.firebaseio.com/",
                  new FirebaseOptions
                  {
                      AuthTokenAsyncFactory = () => LoginWithEmail(false)
                  });
            return firebaseClient;
        }

        public async Task BorrarProducto(string code)
        {
            var firebase = ClienteAutenticadoConEmail();

            var lipro = await firebase
                .Child("Inventario")
                .OnceAsync<Producto>();

            FirebaseObject<Producto> ob = null;
            foreach (var p in lipro)
            {
                if (p.Object.Codigo == code)
                {
                    ob = p;
                    break;
                }
            }
            await firebase
                .Child("Inventario")
                .Child(ob?.Key)
                .DeleteAsync();
        }

        //public async Task BorrarVenta(Venta _venta)
        //{
        //    var firebase = ClienteAutenticadoConEmail();

        //    var lipro = await firebase
        //        .Child("Ventas")
        //        .OnceAsync<Venta>();

        //    FirebaseObject<Venta> ob = null;
        //    foreach (var p in lipro)
        //    {
        //        if (p.Object.Fecha == _venta.Fecha &&
        //            p.Object.Cliente.Nombre == _venta.Cliente.Nombre &&
        //            p.Object.Total == _venta.Total)
        //        {
        //            ob = p;
        //            break;
        //        }
        //    }
        //    await firebase
        //        .Child("Ventas")
        //        .Child(ob?.Key)
        //        .DeleteAsync();
        //}

        public async Task BorrarVentaKey(FirebaseObject<VentaNew> _venta)
        {
            var firebase = ClienteAutenticadoConEmail();

            FirebaseObject<VentaNew> ob = _venta;
            
            await firebase
                .Child("UVenta")
                .Child(ob?.Key)
                .DeleteAsync();
        }

        public async Task BorrarCompraKey(FirebaseObject<Compra> _compra)
        {
            var firebase = ClienteAutenticadoConEmail();

            FirebaseObject<Compra> ob = _compra;

            await firebase
                .Child("Compras")
                .Child(ob?.Key)
                .DeleteAsync();
        }

        //public async Task BorrarCliente(string code)
        //{
        //    var firebase = ClienteAutenticadoConEmail();

        //    var lipro = await firebase
        //        .Child("Clientes")
        //        .OnceAsync<Modelos.Cliente>();

        //    FirebaseObject<Modelos.Cliente> ob = null;
        //    foreach (var p in lipro)
        //    {
        //        if (p.Object.Nombre == code)
        //        {
        //            ob = p;
        //            break;
        //        }
        //    }
        //    await firebase
        //        .Child("Clientes")
        //        .Child(ob?.Key)
        //        .DeleteAsync();
        //}

        //public async Task BorrarNCliente(string code)
        //{
        //    var firebase = ClienteAutenticadoConEmail();

        //    var lipro = await firebase
        //        .Child("NClientes")
        //        .OnceAsync<NewClient>();

        //    FirebaseObject<NewClient> ob = null;
        //    foreach (var p in lipro)
        //    {
        //        if (p.Object.Nombre == code)
        //        {
        //            ob = p;
        //            break;
        //        }
        //    }
        //    await firebase
        //        .Child("NClientes")
        //        .Child(ob?.Key)
        //        .DeleteAsync();
        //}
        
        public async Task BorrarProveedor(string code)
        {
            var firebase = ClienteAutenticadoConEmail();

            var lipro = await firebase
                .Child("Proveedores")
                .OnceAsync<Proveedor>();

            FirebaseObject<Proveedor> ob = null;
            foreach (var p in lipro)
            {
                if (p.Object.Nombre == code)
                {
                    ob = p;
                    break;
                }
            }
            await firebase
                .Child("Proveedores")
                .Child(ob?.Key)
                .DeleteAsync();
        }

        public async Task<List<Producto>> ObtenerProductos(string tabla)
        {
            var firebase = ClienteAutenticadoConEmail();

            var Products = await firebase
                .Child(tabla)
                .OnceAsync<Producto>();

            List<Producto> ProductosFB = new List<Producto>();

            foreach (var PR in Products)
            {
                ProductosFB.Add(PR.Object);
            }
            return ProductosFB;
        }

        public async Task DeleteAllString(string _tabla)
        {
            var firebase = ClienteAutenticadoConEmail();

            await firebase
                .Child(_tabla)
                .DeleteAsync();
        }
         
        public async Task Guardar(string _tabla, Object _objeto)
        {
            var firebase = ClienteAutenticadoConEmail();
            try
            {
                var item = await firebase
                  .Child(_tabla)
                  .PostAsync(_objeto);
            }
            catch (Exception)
            {
            }
        }

        public async Task<List<Compra>> ObtenerCompras()
        {
            var firebase = ClienteAutenticadoConEmail();

            var Products = await firebase
                .Child("Compras")
                .OnceAsync<Compra>();

            List<Compra> ComprasFB = new List<Compra>();

            Compras = new List<FirebaseObject<Compra>>();

            foreach (var PR in Products)
            {
                Compras.Add(PR);
                ComprasFB.Add(PR.Object);
            }
            return ComprasFB;
        }

        //public async Task<List<Venta>> ObtenerVentas()
        //{
        //    var firebase = ClienteAutenticadoConEmail();

        //    var Products = await firebase
        //        .Child("Ventas")
        //        .OnceAsync<Venta>();

        //    List<Venta> VentasFB = new List<Venta>();

        //    Ventas = new List<FirebaseObject<Venta>>();

        //    foreach (var PR in Products)
        //    {
        //        Ventas.Add(PR);
        //        VentasFB.Add(PR.Object);
        //    }
        //    return VentasFB;
        //}

        //public async Task<List<Cliente>> ObtenerClientes()
        //{
        //    var firebase = ClienteAutenticadoConEmail();

        //    var Products = await firebase
        //        .Child("Clientes")
        //        .OnceAsync<Cliente>();

        //    List<Cliente> VentasFB = new List<Cliente>();

        //    foreach (var PR in Products)
        //    {
        //        VentasFB.Add(PR.Object);
        //    }
        //    return VentasFB;
        //}

        //public async Task<List<NewClient>> ObtenerNClientes()
        //{
        //    var firebase = ClienteAutenticadoConEmail();

        //    var Products = await firebase
        //        .Child("NClientes")
        //        .OnceAsync<NewClient>();

        //    List<NewClient> VentasFB = new List<NewClient>();

        //    foreach (var PR in Products)
        //    {
        //        VentasFB.Add(PR.Object);
        //    }
        //    return VentasFB;
        //}

        public async Task<List<Proveedor>> ObtenerProveedores()
        {
            var firebase = ClienteAutenticadoConEmail();

            var Products = await firebase
                .Child("Proveedores")
                .OnceAsync<Proveedor>();

            List<Proveedor> VentasFB = new List<Proveedor>();

            foreach (var PR in Products)
            {
                VentasFB.Add(PR.Object);
            }
            return VentasFB;
        }

        public async Task<List<string>> ObtenerMarcasTipos(string _tabla)
        {
            var firebase = ClienteAutenticadoConEmail();

            var Products = await firebase
                .Child(_tabla)
                .OnceAsync<List<string>>();

            List<string> Strings = new List<string>();

            foreach (var PR in Products)
            {
                var u = PR.Object;
                foreach (var item in u)
                {
                    Strings.Add(item);
                }
            }
            return Strings;
        }

        public async Task SaveString(string _tabla, List<string> _list)
        {
            var firebase = ClienteAutenticadoConEmail();

            try
            {
                await firebase
                .Child(_tabla)
                .DeleteAsync();

                var item = await firebase
                  .Child(_tabla)
                  .PostAsync(_list);
            }
            catch (Exception)
            {
            }
        }
        #endregion
    }
}
