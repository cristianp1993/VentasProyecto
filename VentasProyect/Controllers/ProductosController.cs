using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Web.UI.WebControls;
using VentasProyect.Models.Productos;
using VentasProyect.Repository;

namespace VentasProyect.Controllers
{
    public class ProductosController : Controller
    {
        ProductosRepository _productosRepository = new ProductosRepository();
        CategoriaRepository _categoriaRepository = new CategoriaRepository();

        private string _ftpIp;
        private int _ftpPort;
        private string _ftpUsername;
        private string _ftpPassword;

        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            base.OnActionExecuting(filterContext);

            // Llama a LoadFtpConfig aqui
            LoadFtpConfig();
        }

        // Obten la URL base HTTP desde la configuracion
        string httpBaseUrl = "https://rep-file.sagerp.cloud/Pruebas/VentureSales/";

        private void LoadFtpConfig()
        {
            string configPath = Server.MapPath("~/ftpconfig.json");
            var json = System.IO.File.ReadAllText(configPath);
            var config = JObject.Parse(json);

            _ftpIp = (string)config["FtpIp"];
            _ftpPort = (int)config["FtpPort"];
            _ftpUsername = (string)config["FtpUsername"];
            _ftpPassword = (string)config["FtpPassword"];
        }

[HttpPost]
public ActionResult UploadImages(IEnumerable<HttpPostedFileBase> files)
{
    if (files != null && files.Any())
    {
        var imageUrls = new List<string>();
        foreach (var file in files)
        {
            if (file != null && file.ContentLength > 0)
            {
                var fileName = Path.GetFileName(file.FileName);
                var ftpPath = $"ftp://{_ftpIp}:{_ftpPort}/{fileName}";

                // Subir el archivo al FTP
                UploadFileToFtp(file.InputStream, ftpPath);

                // Reemplazar la parte inicial de la URL FTP con la parte inicial de la URL HTTP
                var httpUrl = ftpPath.Replace($"ftp://{_ftpIp}:{_ftpPort}/", "https://rep-file.sagerp.cloud/Pruebas/VentureSales/");

                imageUrls.Add(httpUrl);
            }
        }

        // Guardar las URLs de las imagenes en la sesión
        Session["UploadedImages"] = imageUrls;
        TempData["Message"] = "Imágenes cargadas con éxito.";
    }
    else
    {
        TempData["Error"] = "No se seleccionaron archivos.";
    }

    return RedirectToAction("BulkUpload");
}


        [HttpPost]
        public ActionResult BulkUpload(HttpPostedFileBase file)
        {
            if (file != null && file.ContentLength > 0)
            {
                var productosList = new List<Productos>();
                var imageUrls = Session["UploadedImages"] as List<string> ?? new List<string>();

                using (var reader = new StreamReader(file.InputStream))
                {
                    string line;
                    int lineNumber = 0;

                    while ((line = reader.ReadLine()) != null)
                    {
                        lineNumber++;
                        if (lineNumber == 1)
                        {
                            continue;
                        }

                        var values = line.Split(',');

                        if (values.Length != 7)
                        {
                            ModelState.AddModelError("", $"La línea {lineNumber} no tiene el número correcto de columnas.");
                            continue;
                        }

                        if (imageUrls.Count < lineNumber - 1)
                        {
                            ModelState.AddModelError("", $"No hay suficientes imágenes para el producto en la línea {lineNumber}.");
                            continue;
                        }

                        try
                        {
                            var producto = new Productos
                            {
                                cat_id = values[0],
                                pro_nombre = values[1],
                                pro_descripcion = values[2],
                                pro_valor_unitario = int.Parse(values[3]),
                                pro_stock = int.Parse(values[4]),
                                pro_url_img = imageUrls.ElementAt(lineNumber - 2),
                                pro_estado = values[6]
                            };
                            productosList.Add(producto);
                        }
                        catch (FormatException ex)
                        {
                            ModelState.AddModelError("", $"Error de formato en la línea {lineNumber}: {ex.Message}");
                        }
                        catch (Exception ex)
                        {
                            ModelState.AddModelError("", $"Error en la línea {lineNumber}: {ex.Message}");
                        }
                    }
                }

                if (ModelState.IsValid)
                {
                    foreach (var producto in productosList)
                    {
                        _productosRepository.CreateProduct(producto);
                    }
                    Session.Remove("UploadedImages");
                    return RedirectToAction("Index");
                }
            }

            return View();
        }

        private void UploadFileToFtp(Stream fileStream, string ftpPath)
        {
            var request = (FtpWebRequest)WebRequest.Create(ftpPath);
            request.Method = WebRequestMethods.Ftp.UploadFile;

            // Usar las credenciales del FTP
            request.Credentials = new NetworkCredential(_ftpUsername, _ftpPassword);

            using (var ftpStream = request.GetRequestStream())
            {
                fileStream.CopyTo(ftpStream);
            }
        }

        public ActionResult Index()
        {
            IEnumerable<Models.Productos.Productos> data = _productosRepository.GetAllProductos();
            return View(data);
        }


        public ActionResult Create()
        {
            ViewBag.Categorias = _categoriaRepository.GetSelectCategorias();
            return View();
        }

        [HttpPost]
        public ActionResult Create(Productos model)
        {
            if (ModelState.IsValid) 
            {
                _productosRepository.CreateProduct(model);

                return RedirectToAction("Index");
            }
            return View(model);
        }

        public ActionResult Edit(int id) 
        {
            ViewBag.Categorias = _categoriaRepository.GetSelectCategorias();
            var data = _productosRepository.GetDataById(id);
            if (data == null)
            {
                return HttpNotFound();
            }

            ViewBag.Estados = new List<SelectListItem>
            {
                new SelectListItem { Text = "Activo", Value = "Activo" },
                new SelectListItem { Text = "Inactivo", Value = "Inactivo" },
                new SelectListItem { Text = "Disponible", Value = "Disponible" },
                new SelectListItem { Text = "Agotado", Value = "Agotado" }
            };

            return View(data);
        }

        [HttpPost]
        public ActionResult Edit(Productos data)
        {
            if (ModelState.IsValid)
            {
                if (data.pro_stock == 0 && data.pro_estado == "Disponible")
                {
                    ModelState.AddModelError("pro_estado", "No puedes establecer el estado como 'Disponible' si el stock es cero.");
                    ViewBag.Categorias = _categoriaRepository.GetSelectCategorias();
                    ViewBag.Estados = new List<SelectListItem>
            {
                new SelectListItem { Text = "Activo", Value = "Activo" },
                new SelectListItem { Text = "Inactivo", Value = "Inactivo" },
                new SelectListItem { Text = "Disponible", Value = "Disponible", Disabled = true },
                new SelectListItem { Text = "Agotado", Value = "Agotado" }
            };
                    return View(data);
                }

                if (data.pro_stock > 0 && (data.pro_estado == "Agotado" || data.pro_estado == "Inactivo"))
                {
                    ModelState.AddModelError("pro_estado", "No puedes establecer el estado como 'Agotado' o 'Inactivo' si el stock es mayor que cero.");
                    ViewBag.Categorias = _categoriaRepository.GetSelectCategorias();
                    ViewBag.Estados = new List<SelectListItem>
            {
                new SelectListItem { Text = "Activo", Value = "Activo" },
                new SelectListItem { Text = "Inactivo", Value = "Inactivo" },
                new SelectListItem { Text = "Disponible", Value = "Disponible" },
                new SelectListItem { Text = "Agotado", Value = "Agotado", Disabled = true }
            };
                    return View(data);
                }

                _productosRepository.UpdateProduct(data);
                return RedirectToAction("Index");
            }

            ViewBag.Categorias = _categoriaRepository.GetSelectCategorias();

            ViewBag.Estados = new List<SelectListItem>
            {
                new SelectListItem { Text = "Activo", Value = "Activo" },
                new SelectListItem { Text = "Inactivo", Value = "Inactivo" },
                new SelectListItem { Text = "Disponible", Value = "Disponible" },
                new SelectListItem { Text = "Agotado", Value = "Agotado" }
            };
            return View(data);
        }


        public ActionResult Details(int id)
        {
            var producto = _productosRepository.GetDataById(id);
            if (producto == null)
            {
                return HttpNotFound();
            }

            // Obtener el nombre de la categoria
            var categoria = _categoriaRepository.GetDataById(Convert.ToInt32(producto.cat_id));
            if (categoria != null)
            {
                producto.cat_nombre = categoria.cat_nombre;
            }

            return View(producto);
        }

        public ActionResult DownloadTemplate()
        {
            var csvContent = "cat_id,pro_nombre,pro_descripcion,pro_valor_unitario,pro_stock,pro_url_img,pro_estado\n" +
                             "1,Producto A,Descripcion del Producto A,100,50,https://rep-file.sagerp.cloud/Pruebas/nombre_img.jpeg,Activo\n";

            byte[] buffer = System.Text.Encoding.UTF8.GetBytes(csvContent);
            return File(buffer, "text/csv", "Plantilla_Productos.csv");
        }

        public ActionResult BulkUpload()
        {
            return View();
        }
    }
}