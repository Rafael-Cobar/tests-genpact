using Syncfusion.XlsIO;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ObservadorCarpetas.Clases
{
    internal class Excel
    {
        // Variables -----------------------------------------------------------------
        private string archivoMaestro; // ruta del archivo donde se agregan todas las demas hojas

        // Constructor -----------------------------------------------------------------
        public Excel(string archivoMaestro){
            this.archivoMaestro = archivoMaestro;
            this.crearArchivoExcel();
        }


        // Metodos -----------------------------------------------------------------

        // CrearArchivoExcel = se encarga de crear el archivo maestro de excel
        private void crearArchivoExcel(){
            if(!File.Exists(this.archivoMaestro)){
                using (ExcelEngine excelEngine = new ExcelEngine()){
                    IApplication application = excelEngine.Excel;
                    application.DefaultVersion = ExcelVersion.Xlsx;
                    IWorkbook workbook = application.Workbooks.Create(1); // Se crea una hoja nueva
                    workbook.SaveAs(this.archivoMaestro);
                }
            }
        }

        // copiarHojasArchivo = se encarga de obtener todas las hojas del archivo origen para copiarlas al archivo destino
        public bool copiarHojasArchivo(string archivoOrigen){
            using (ExcelEngine excelEngine = new ExcelEngine()){
                IApplication application = excelEngine.Excel;
                application.DefaultVersion = ExcelVersion.Xlsx;

                try{
                    IWorkbook origenLibro = application.Workbooks.Open(archivoOrigen); // Abrir el archivo de origen
                    IWorkbook maestroLibro = application.Workbooks.Open(this.archivoMaestro); // Abrir el archivo maestro

                    foreach (IWorksheet hoja in origenLibro.Worksheets){
                        maestroLibro.Worksheets.AddCopy(hoja); // Copia la hoja del origen al maestro
                    }

                    maestroLibro.Save(); // Guardar Cambios en el archivo Maestro
                    Singleton.Instance.agregarMsn("OK: Hojas del Archivo Copiadas", archivoOrigen);
                    origenLibro.Close();
                    maestroLibro.Close();
                }
                catch (Exception e){
                    Singleton.Instance.agregarMsn($"Error: Al Mover las hojas, {e.Message}", archivoOrigen);
                    return false;
                }
            }
            return true;
        }


    }
}
