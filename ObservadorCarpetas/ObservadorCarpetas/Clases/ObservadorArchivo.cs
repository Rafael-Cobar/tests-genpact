using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ObservadorCarpetas.Clases
{
    internal class ObservadorArchivo
    {
        // Variables -----------------------------------------------------------------
        private string pathOrigen;
        private FileSystemWatcher observer;


        // Constructor -----------------------------------------------------------------
        public ObservadorArchivo(string pathOrigen){
            this.pathOrigen = pathOrigen;
            this.observer = new FileSystemWatcher(this.pathOrigen);
            this.observer.EnableRaisingEvents = false;
        }

        // Metodos -----------------------------------------------------------------


        // iniciarObservador = inicializa el observador
        public void iniciarObservador(){
            // observar todos los cambios
            this.observer.NotifyFilter = NotifyFilters.Attributes
                                 | NotifyFilters.CreationTime
                                 | NotifyFilters.DirectoryName
                                 | NotifyFilters.FileName
                                 | NotifyFilters.LastAccess
                                 | NotifyFilters.LastWrite
                                 | NotifyFilters.Security
                                 | NotifyFilters.Size;


            this.observer.Filter = "*.*"; // Observar todos lo archivos

            // Manejadores de Eventos
            this.observer.Changed += OnChanged;
            this.observer.Created += OnCreated;
            this.observer.Deleted += OnDeleted;
            this.observer.Renamed += OnRenamed;
            this.observer.Error += OnError;

            this.observer.IncludeSubdirectories = true;
            this.observer.EnableRaisingEvents = true;
        }

        // detenerObservador = detiene el observador de la carpeta origen
        public void detenerObservador(){
            this.observer.EnableRaisingEvents = false;
            Singleton.Instance.agregarMsn("Detener: Se detuvo el observador de la carpeta", this.pathOrigen);
        }

        // OnChanged = Verificar cambios en el archivo
        private static void OnChanged(object sender, FileSystemEventArgs e){
            if (e.ChangeType != WatcherChangeTypes.Changed) return;
            Console.WriteLine($"Archivo realizó Cambios: {e.FullPath}");
            Singleton.Instance.verificarObservador();
        }

        // OnCreated = verifica si el archivo fue creado
        private static void OnCreated(object sender, FileSystemEventArgs e){
            string value = $"Archivo Fue Creado: {e.FullPath}";
            Console.WriteLine(value);
            Singleton.Instance.verificarObservador();
        }

        // OnDeleted = verifica si el archivo fue eliminado o movido.
        private static void OnDeleted(object sender, FileSystemEventArgs e) { 
            Console.WriteLine($"Archivo Fue Eliminado: {e.FullPath}");
            Singleton.Instance.verificarObservador();
        }

        // OnRenamed = verifica si el archivo fue renombrado
        private static void OnRenamed(object sender, RenamedEventArgs e){
            Console.WriteLine($"Archivo Fue Renombrado: {e.OldFullPath}");
            Singleton.Instance.verificarObservador();
        }

        private static void OnError(object sender, ErrorEventArgs e){
            if(e.GetException() != null) Console.WriteLine($"Hubó un error = {e.GetException().Message}");
        }

    }
}
