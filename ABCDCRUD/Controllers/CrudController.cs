using Microsoft.AspNetCore.Mvc;

using ABCDCRUD.Util;
using ABCDCRUD.Data;
using ABCDCRUD.Models;
using ExcelDataReader;
using System.Data;
using Microsoft.EntityFrameworkCore;
using System.Data.SqlClient;

namespace ABCDCRUD.Controllers
{
    public class CrudController : Controller
    {
        AseguradoData aseguradoData = new AseguradoData();

        public IActionResult Admin()
        {
            //VISTA ADMINISTRAR ASEGURADOS
            var oLista = aseguradoData.Listar();

            return View(oLista);
        }

        public IActionResult Asegurado()
        {
            //DEVUELVE VISTA CREAR ASEGURADO
            return View();
        }

        [HttpPost]
        public IActionResult Asegurado(Asegurado oAsegurado)
        {
            //RECIBE OBJETO Y LO ALMACENA EN BD
            var respuesta = aseguradoData.Guardar(oAsegurado);

            if (respuesta)
                return RedirectToAction("Admin");
            else
                return View();
        }

        public IActionResult Editar(int Identificacion)
        {
            //METODO SOLO DEVUELVE LA VISTA
            var oAsegurado = aseguradoData.Obtener(Identificacion);
            return View(oAsegurado);
        }

        [HttpPost]
        public IActionResult Editar(Asegurado oAsegurado)
        {
            if (!ModelState.IsValid)
                return View();


            var respuesta = aseguradoData.Editar(oAsegurado);

            if (respuesta)
                return RedirectToAction("Admin");
            else
                return View();
        }


        public IActionResult Eliminar(int Identificacion)
        {
            //METODO SOLO DEVUELVE LA VISTA
            var oAsegurado = aseguradoData.Obtener(Identificacion);
            return View(oAsegurado);
        }

        [HttpPost]
        public IActionResult Eliminar(Asegurado oAsegurado)
        {

            var respuesta = aseguradoData.Eliminar(oAsegurado.Identificacion);

            if (respuesta)
                return RedirectToAction("Admin");
            else
                return View();
        }

        public IActionResult Masivo()
        {
            //DEVUELVE VISTA CARGUE MASIVO ASEGURADOS
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> ImportFile(IFormFile importFile)
        {
            if (importFile == null) return Json(new { Status = 0, Message = "Ningún archivo seleccionado" });

            try
            {
                var stream = new MemoryStream();
                importFile.CopyTo(stream);
                var fileData = GetDataFromCSVFile(stream);

                var dtAsegurado = fileData.ToDataTable();
                var tblAseguradoParameter = new SqlParameter("tblAseguradoTableType", SqlDbType.Structured)
                {
                    TypeName = "dbo.tblTypeAsegurados",
                    Value = dtAsegurado
                };
                //await _dbContext.Database.ExecuteSqlCommandAsync("EXEC sp_ImportAsegurado @tblAseguradoTableType", tblAseguradoParameter);
                var respuesta =  aseguradoData.Importar(tblAseguradoParameter);
                if (respuesta)
                    return Json(new {Status=1, Message="Archivo importado satisfactoriamente."});
                else
                    return Json(new {Status=0, Message="Error al importar archibo"});
            }
            catch (Exception ex)
            {
                return Json(new {Status=0, Message=ex.Message });
            }
        }

        private List<Asegurado> GetDataFromCSVFile(Stream stream)
        {
            var aList = new List<Asegurado>();
            try
            {
                using (var reader = ExcelReaderFactory.CreateCsvReader(stream))
                {
                    var dataSet = reader.AsDataSet(new ExcelDataSetConfiguration
                    {
                        ConfigureDataTable = _ => new ExcelDataTableConfiguration
                        {
                            UseHeaderRow = true // To set First Row As Column Names    
                        }
                    });

                    if (dataSet.Tables.Count > 0)
                    {
                        var dataTable = dataSet.Tables[0];
                        foreach (DataRow objDataRow in dataTable.Rows)
                        {
                            if (objDataRow.ItemArray.All(x => string.IsNullOrEmpty(x?.ToString()))) continue;
                            aList.Add(new Asegurado()
                            {
                                Identificacion = Convert.ToInt32(objDataRow["identificacion"].ToString()),
                                Nombre1 = objDataRow["nombre1"].ToString(),
                                Nombre2 = objDataRow["nombre2"].ToString(),
                                Apellido1 = objDataRow["apellido1"].ToString(),
                                Apellido2 = objDataRow["apellido2"].ToString(),
                                Celular = objDataRow["celular"].ToString(),
                                Email = objDataRow["email"].ToString(),
                                FechaNacimiento = Convert.ToDateTime(objDataRow["fecha_nacimiento"].ToString()),
                                ValorSeguro = Convert.ToDecimal(objDataRow["valor_seguro"].ToString()),
                            });
                        }
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
            return aList;
        }

    }
}
