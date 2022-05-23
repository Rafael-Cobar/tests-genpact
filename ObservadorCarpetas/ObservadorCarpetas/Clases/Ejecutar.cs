using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

using Syncfusion.XlsIO;
using System.Reflection;

namespace ObservadorCarpetas.Clases
{
    internal class Ejecutar
    {
        // Variables -----------------------------------------------------------------
        private Archivo archivo;
        private string pathEntrada;
        private string pathSalida;
        private string pathProcessed;
        private string pathNotApplicable;
        private string nombreArchivoMaestro;

        // Constructor -----------------------------------------------------------------
        public Ejecutar(string pathEntrada, string pathSalida){
            this.pathEntrada = pathEntrada;
            this.pathSalida = pathSalida;
            this.pathProcessed = Path.Combine(pathSalida, "Processed");
            this.pathNotApplicable = Path.Combine(pathSalida, "NotApplicable");
            archivo = new Archivo();
            this.nombreArchivoMaestro = Path.Combine(pathSalida, "ArchivoMaestro.xlsx");
        }

        // Metodos -----------------------------------------------------------------
        public void comenzar(){
            this.archivo.crearCarpeta(pathProcessed);
            this.archivo.crearCarpeta(pathNotApplicable);
            this.realizarAcciones();
        }

        // realizarAcciones = obtiene todos los archivos de la ruta especificada, copias la hojas y mueves los archivos
        private void realizarAcciones(){
            Excel excel = new Excel(this.nombreArchivoMaestro);
            Regex rg = new Regex(".xls*");

            string[] newArrayArchivos = archivo.getArchivos(this.pathEntrada);
            string destino;
            bool bandera; // true = se puede mover el archivo || false = no se puede mover el archivo
            bool banderaMover; // true = Se movió el archivo || false = no se movio el archivo


            foreach (string archivoOrigen in newArrayArchivos){
                destino = this.pathNotApplicable;
                bandera = true;

                if (rg.IsMatch(Path.GetExtension(archivoOrigen))){ // Si Es un archivo .xls*
                    bandera = excel.copiarHojasArchivo(archivoOrigen); // Copiar Hojas
                    destino = this.pathProcessed;
                }

                if (bandera){ // Si no hubieron problemas en el copiado de hojas
                    banderaMover = archivo.moverArchivo(archivoOrigen, destino); // mover las hojas

                    if (banderaMover){
                        Singleton.Instance.agregarMsn("OK: Se Movió el Archivo", archivoOrigen);
                    } else{
                        Singleton.Instance.agregarMsn("Error: No se pudo Mover El archivo", archivoOrigen);
                    }
                }
            }
        }



    }
}
