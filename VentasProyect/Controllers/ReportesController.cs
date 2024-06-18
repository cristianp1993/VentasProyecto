using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.SqlClient;
using ClosedXML.Excel;
using System.IO;
using iTextSharp.text;
using iTextSharp.text.pdf;
using VentasProyect.Models.Reportes;
using VentasProyect.Repository;
using System.Dynamic;
using System.Configuration;

namespace VentasProyect.Controllers
{
    public class ReportesController : Controller
    {
        ReportesRepository _reportesRepository = new ReportesRepository();

        // GET: Reportes
        public ActionResult Index()
        {
            // Crear una lista de SelectListItem con la información proporcionada
            List<SelectListItem> items = new List<SelectListItem>
            {
                new SelectListItem { Text = "Categoría de producto", Value = "1" },
                new SelectListItem { Text = "Venta por producto", Value = "2" },
                new SelectListItem { Text = "Producto más vendido", Value = "3" },
                new SelectListItem { Text = "Producto menos vendido", Value = "4" },
                new SelectListItem { Text = "Producto agotado", Value = "5" },
                new SelectListItem { Text = "Fecha de las ventas", Value = "6" },
                new SelectListItem { Text = "Ventas por Clientes", Value = "7" },
                new SelectListItem { Text = "Enumera los clientes de la tienda", Value = "8" },
                new SelectListItem { Text = "El producto más comprado el cliente", Value = "9" },
                new SelectListItem { Text = "Agrupa los clientes por la categoría de los productos", Value = "10" },
                new SelectListItem { Text = "Lista de órdenes", Value = "11" },
                new SelectListItem { Text = "Ventas por categoría de producto", Value = "12" },
                new SelectListItem { Text = "Ventas Mensuales", Value = "13" },
                new SelectListItem { Text = "Productos en stock", Value = "14" },
                new SelectListItem { Text = "Cantidad de clientes de la tienda", Value = "15" },
                new SelectListItem { Text = "Frecuencia de compra", Value = "16" },
                new SelectListItem { Text = "Ventas por cliente y método de pago", Value = "17" },
                new SelectListItem { Text = "Clientes sin compras recientes", Value = "18" },
                new SelectListItem { Text = "Productos con bajo stock (menos de 10 unidades)", Value = "19" },
                new SelectListItem { Text = "Clientes que más han gastado en total", Value = "20" },
                new SelectListItem { Text = "Ventas por ciudad y departamento", Value = "21" },
                new SelectListItem { Text = "Productos más populares (más veces vendidos)", Value = "22" },
                new SelectListItem { Text = "Total de ventas por tipo de cliente", Value = "23" },
                new SelectListItem { Text = "Pedidos pendientes de entrega", Value = "24" },
                new SelectListItem { Text = "Usuarios y sus roles", Value = "25" },

            };

            // Crear una lista de SelectListItem para las opciones de formato
            List<SelectListItem> formatos = new List<SelectListItem>
            {
                new SelectListItem { Text = "Excel", Value = "Excel" },
                new SelectListItem { Text = "PDF", Value = "PDF" },
            };

            // Pasar las listas a la vista usando ViewBag
            ViewBag.Items = items;
            ViewBag.Formatos = formatos;

            return View();
        }

        // Acción para manejar la descarga
        [HttpPost]
        public ActionResult DownloadReport(string Items, string Formatos)
        {

            try
            {
                List<Dictionary<string, object>> data;
                string nombreConsulta = string.Empty;

                switch (Items)
                {

                    case "1":
                        data = _reportesRepository.ProductCategory();
                        nombreConsulta = "CategoriaDeProducto";
                        break;
                    case "2":
                        data = _reportesRepository.SalesByProduct();
                        nombreConsulta = "VentaPorProducto";
                        break;
                    case "3":
                        data = _reportesRepository.BestSellingProduct();
                        nombreConsulta = "ProductoMasVendido";
                        break;
                    case "4":
                        data = _reportesRepository.LeastSoldProduct();
                        nombreConsulta = "ProductoMenosVendido";
                        break;
                    case "5":
                        data = _reportesRepository.ProductsOutOfStock();
                        nombreConsulta = "ProductoAgotado";
                        break;
                    case "6":
                        data = _reportesRepository.SalesByDate();
                        nombreConsulta = "FechaVentas";
                        break;
                    case "7":
                        data = _reportesRepository.SalesForClient();
                        nombreConsulta = "VentasPorCliente";
                        break;
                    case "8":
                        data = _reportesRepository.StoreCustomers();
                        nombreConsulta = "ClientesTienda";
                        break;
                    case "9":
                        data = _reportesRepository.PurchasedByTheCust();
                        nombreConsulta = "ProductoMasCompradoXcliente";
                        break;
                    case "10":
                        data = _reportesRepository.CustomersByProductCategory();
                        nombreConsulta = "ClientesXcategoríaDeProductos";
                        break;
                    case "11":
                        data = _reportesRepository.SalesList();
                        nombreConsulta = "ListaVentas";
                        break;
                    // Agregar más casos para otras opciones
                    default:
                        return new HttpStatusCodeResult(400, "Reporte no soportado");
                }

                if (Formatos == "Excel")
                {
                    return GenerarExcel(data, nombreConsulta);
                }
                else if (Formatos == "PDF")
                {
                    return GenerarPDF(data, nombreConsulta);
                }

                return RedirectToAction("Index");
            }
           
            

             catch (Exception ex)
            {

                return RedirectToAction("Index","Error");
            }
        }


        private ActionResult GenerarExcel(List<Dictionary<string, object>> datos, string nombreConsulta)
        {
            using (var workbook = new XLWorkbook())
            {
                var worksheet = workbook.Worksheets.Add("Reporte");
                var currentRow = 1;

                // Agregar encabezados
                var headers = datos[0].Keys;
                int column = 1;
                foreach (var header in headers)
                {
                    worksheet.Cell(currentRow, column).Value = header;
                    column++;
                }

                // Agregar datos
                foreach (var dato in datos)
                {
                    currentRow++;
                    column = 1;
                    foreach (var header in headers)
                    {
                        worksheet.Cell(currentRow, column).Value = dato[header]?.ToString();
                        column++;
                    }
                }

                using (var stream = new MemoryStream())
                {
                    workbook.SaveAs(stream);
                    var content = stream.ToArray();
                    return File(content, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", $"{nombreConsulta}.xlsx");
                }
            }
        }

        private ActionResult GenerarPDF(List<Dictionary<string, object>> datos, string nombreConsulta)
        {
            using (var stream = new MemoryStream())
            {
                var document = new Document();
                PdfWriter.GetInstance(document, stream).CloseStream = false;
                document.Open();

                // Agregar contenido al PDF
                foreach (var dato in datos)
                {
                    foreach (var kvp in dato)
                    {
                        document.Add(new Paragraph($"{kvp.Key}: {kvp.Value}"));
                    }
                    document.Add(new Paragraph(" "));
                }

                document.Close();
                var content = stream.ToArray();
                return File(content, "application/pdf", $"{nombreConsulta}.pdf");
            }
        }

        // Acción para mostrar gráficos
        public ActionResult Graficos()
        {
            var viewModel = new GraficosViewModel
            {
                ProductCategory = _reportesRepository.ProductCategory(),
                SalesByProduct = _reportesRepository.SalesByProduct(),
                BestSellingProduct = _reportesRepository.BestSellingProduct(),
                LeastSoldProduct = _reportesRepository.LeastSoldProduct(),
                ProductsOutOfStock = _reportesRepository.ProductsOutOfStock(),
                SalesByDate = _reportesRepository.SalesByDate(),
                SalesForClient = _reportesRepository.SalesForClient(),
                StoreCustomers = _reportesRepository.StoreCustomers(),
                PurchasedByTheCust = _reportesRepository.PurchasedByTheCust(),
                CustomersByProductCategory = _reportesRepository.CustomersByProductCategory(),
                SalesList = _reportesRepository.SalesList()
            };

            return View(viewModel);
        }

    }
}
