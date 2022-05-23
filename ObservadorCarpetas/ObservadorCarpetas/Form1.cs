using ObservadorCarpetas.Clases;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ObservadorCarpetas
{
    public partial class ObservadorCarpetas : Form
    {
        // Variables -----------------------------------------------------------------
        private Archivo archivo;
        private Timer timer;

        // Constructor -----------------------------------------------------------------------
        public ObservadorCarpetas(){
            InitializeComponent();
            archivo = new Archivo();
            this.btnDetener.Enabled = false;

            this.timer = new Timer();
            this.timer.Interval = 5000;
            this.timer.Tick += new EventHandler(this.Timer1_Tick);
        }


        // Eventos -----------------------------------------------------------------------
        private void btnCarpetaEntrada_Click(object sender, EventArgs e){
            var path = archivo.seleccionarCarpeta();
            if (path != txtCarpetaEntrada.Text && !string.IsNullOrEmpty(path)) txtCarpetaEntrada.Text = path;

        }

        private void btnCarpetaSalida_Click(object sender, EventArgs e){
             txtCarpetaSalida.Text = archivo.seleccionarCarpeta();
        }

        private void btnEmpezar_Click(object sender, EventArgs e){
            if (String.IsNullOrEmpty(txtCarpetaEntrada.Text) || String.IsNullOrEmpty(txtCarpetaSalida.Text)){
                this.MessageError("No se ha seleccionado la carpeta de entrada y salida");
                return;
            }

            if (txtCarpetaEntrada.Text == txtCarpetaSalida.Text){
                this.MessageError("La Ruta de entrada y salida no puede ser la misma");
                return;
            }

            this.btnEmpezar.Enabled = false;

            this.timer.Enabled = true; // habilitar timer

            Singleton.Instance.inicializarEjecutar(txtCarpetaEntrada.Text, txtCarpetaSalida.Text);
            Singleton.Instance.iniciarObservador(txtCarpetaEntrada.Text);
            Singleton.Instance.ejecutarAcciones();

            this.btnDetener.Enabled = true;           
        }

        private void btnDetener_Click(object sender, EventArgs e){
            Singleton.Instance.detenerObservador();
            this.btnEmpezar.Enabled = true;
            this.btnDetener.Enabled = false;
            this.timer.Enabled = false;
        }

        private void btnActualizar_Click(object sender, EventArgs e){
            this.richTextBox1.Text = Singleton.Instance.getContenido();
        }

        private void Timer1_Tick(object Sender, EventArgs e){
            if(this.richTextBox1.Text != Singleton.Instance.getContenido()) this.richTextBox1.Text = Singleton.Instance.getContenido();
        }

        private void MessageError(string msn){
            MessageBox.Show(msn, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);          
        }
    }
}
