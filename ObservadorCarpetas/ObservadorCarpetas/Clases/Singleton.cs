using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ObservadorCarpetas.Clases
{
    internal sealed class Singleton
    {
        // Variables -----------------------------------------------------------------
        private static readonly Singleton instance = new Singleton();

        private Ejecutar ejecutar; // objeto para ejecutar: copiar y mover archivos
        private int repeticiones = 1; // contador de repeticiones 
        private ObservadorArchivo obs; // objeto observador de la carpeta especifica
        private string contenido;

        // Constructor -----------------------------------------------------------------
        static Singleton() { }
        private Singleton() { }


        // Metodos -----------------------------------------------------------------
        public static Singleton Instance {
            get { return instance; }
        }

        // agregarMsn = agrega mensajes de ok u error al Richtextbox
        public void agregarMsn(string msn, string archivo) {
            this.contenido += $"{archivo} || {msn}\t\n";
        }

        // detenerObservador = detenie el observador de la carpeta
        public void detenerObservador(){
            this.repeticiones = 0;
            this.obs.detenerObservador();
        }

        // ejecutarAcciones = se encarga de ejecutar copias hojas y mover los archivos.
        public void ejecutarAcciones(){
            this.repeticiones = 1;
            while (this.repeticiones > 0){
                this.ejecutar.comenzar();
                this.repeticiones--;
            }
        }

        // inicializarEjecutar = crea el objeto de la clase Ejecutar
        public void inicializarEjecutar(string pathEntrada, string pathSalida) => this.ejecutar = new Ejecutar(pathEntrada, pathSalida);

        // iniciarObservador = crea el objeto del observador de la carpeta
        public void iniciarObservador(string pathEntrada){
            this.obs = new ObservadorArchivo(pathEntrada);
            this.obs.iniciarObservador();
        }


        // verificarObservador = verifica si se estan ejecutando acciones
        public void verificarObservador(){
            if (this.repeticiones > 0) { // Aumentar contador si hay repiticiones activas
                this.repeticiones++;
            } else {
                this.ejecutarAcciones(); // Si no hay repeticiones ejecutar directamente
            }
        }

        // getContenido = retorna el contenido para mostrar en consola los mensajes
        public string getContenido() => this.contenido;


    }
}
