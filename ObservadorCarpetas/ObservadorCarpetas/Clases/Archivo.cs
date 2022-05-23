using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Windows.Forms;
using System.Diagnostics;

namespace ObservadorCarpetas.Clases
{
    internal class Archivo
    {
        // Constructor -----------------------------------------------------------------
        public Archivo() { }

        // Metodos -----------------------------------------------------------------

        // crearCarpeta = se encarga de crear el archivo
        public void crearArchivo(string path){
            if (!File.Exists(path)) File.Create(path) ;
        }

        // crearCarpeta = se encarga de crear la carpeta en al ruta especificada
        public void crearCarpeta(string path){
            if (!Directory.Exists(path))  Directory.CreateDirectory(path);
        }

        // eliminarArchivo = se encarga de eliminar el archivo
        public void eliminarArchivo(string path){
            if(File.Exists(path)){
                try{
                    File.Delete(path);
                }
                catch(Exception e) {
                    Console.WriteLine(e.Message);
                }
            }
        }


        // getArchivos = se encarga de obtener todos los archivos de la carpeta
        public string[] getArchivos(string path){
            string[] archivos = { };
            if (Directory.Exists(path)) archivos = Directory.GetFiles(path, "*.*", SearchOption.AllDirectories);

            return archivos;
        }


        // moverArchivo = mueve el archivo a la ruta especificada
        public bool moverArchivo(string pathArchivo, string pathCarpetaDestino){
            try{
                if (File.Exists(pathArchivo) && Directory.Exists(pathCarpetaDestino)){
                    string pathArchivoDestino = Path.Combine(pathCarpetaDestino, Path.GetFileName(pathArchivo));
                    this.eliminarArchivo(pathArchivoDestino);
                    File.Move(pathArchivo, pathArchivoDestino);
                    return true;
                }
            } catch (Exception e){
                Singleton.Instance.agregarMsn($"Error: {e.Message}", pathArchivo);
            }
            return false;
        }


        // seleccionarCarpeta = se encarga de seleccionar la dirección de la carpeta
        public string seleccionarCarpeta(){
            FolderBrowserDialog fbd = new FolderBrowserDialog();
            DialogResult result = fbd.ShowDialog();

            return result == DialogResult.OK && !string.IsNullOrEmpty(fbd.SelectedPath) ? fbd.SelectedPath : "";
        }


    }
}
