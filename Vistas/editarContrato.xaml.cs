using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using Clases;
using MahApps.Metro.Controls;
using MahApps.Metro.Controls.Dialogs;
using System.Windows.Threading;


namespace BeLifeV2
{
    /// <summary>
    /// Lógica de interacción para editarContrato.xaml
    /// </summary>
    public partial class editarContrato : MetroWindow
    {
        DispatcherTimer timer;
        System.Windows.Threading.DispatcherTimer dispatcherTimer = new System.Windows.Threading.DispatcherTimer();

        public Conexion conec = new Conexion();
        //public Contrato objCont = new Contrato();
        public Cliente objCli = new Cliente();

        private int _edad;
        private int _estadoC;
        private int _sexo;
        private double _primaBase;

        public int Edad
        {
            get
            {
                return _edad;
            }

            set
            {
                _edad = value;
            }
        }

        public int EstadoC
        {
            get
            {
                return _estadoC;
            }

            set
            {
                _estadoC = value;
            }
        }

        public int Sexo
        {
            get
            {
                return _sexo;
            }

            set
            {
                _sexo = value;
            }
        }

        public double PrimaBase
        {
            get
            {
                return _primaBase;
            }

            set
            {
                _primaBase = value;
            }
        }

        int ctr = 0;
        public editarContrato()
        {
            InitializeComponent();
            timer = new DispatcherTimer();
            timer.Interval = new TimeSpan(0, 0, 2);
            timer.Tick += new EventHandler(timer_Tick);
            timer.Start();

            ctr = 1;
            PlaySlideShow(ctr);

            listCombo();
            WindowStartupLocation = System.Windows.WindowStartupLocation.CenterScreen;
        }

        public editarContrato(string numero, string rut, string plan, string salud, string primaAn, string primaMen, string obs)
        {
            InitializeComponent();

            timer = new DispatcherTimer();
            timer.Interval = new TimeSpan(0, 0, 2);
            timer.Tick += new EventHandler(timer_Tick);
            timer.Start();

            ctr = 1;
            PlaySlideShow(ctr);

            listCombo();

            cbbRutCli.SelectedValue = rut;
            cbbListaContrato.SelectedValue = numero;
            dataCliente();
            buscarContrato();
            WindowStartupLocation = System.Windows.WindowStartupLocation.CenterScreen;
        }

        void timer_Tick(object sender, EventArgs e)
        {
            ctr++;
            if (ctr > 6)
            {
                ctr = 1;
            }
            PlaySlideShow(ctr);
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            ctr = 1;
            PlaySlideShow(ctr);
        }

        private void PlaySlideShow(int ctr)
        {
            BitmapImage image = new BitmapImage();
            image.BeginInit();
            string filename = ((ctr < 10) ? "images/contratos/contr0" + ctr + ".jpg" : "images/contratos/contr" + ctr + ".jpg");
            image.UriSource = new Uri(filename, UriKind.Relative);
            image.EndInit();
            myImage.Source = image;
            myImage.Stretch = Stretch.Uniform;

        }

        public void listCombo()
        {
            string[] listRutC = conec.listRutContrato();
            cbbRutCli.SelectedIndex = 0;
            cbbRutCli.Items.Add("Seleccione");
            for (int z = 0; z < listRutC.Length; z++)
            {
                cbbRutCli.Items.Add(listRutC[z]);
            }

            /*string[] listPlan = conec.listPlan();
            cbbPlan.SelectedIndex = 0;
            cbbPlan.Items.Add("Seleccione");
            for (int x = 0; x < listPlan.Length; x++)
            {
                cbbPlan.Items.Add(listPlan[x]);
            }*/

            cbbSalud.SelectedIndex = 0;
            cbbSalud.Items.Add("Seleccione");
            cbbSalud.Items.Add("No");
            cbbSalud.Items.Add("Si");
        }

        public void buscarContrato()
        {
            string rut = cbbRutCli.SelectedItem.ToString();
            string numero = cbbListaContrato.SelectedItem.ToString();
            string[] datos;

            datos = conec.getDatosContrato(numero, rut);
            if (datos[0] == "VID01")
            {
                cbbPlan.SelectedIndex = 1;
            }
            else if (datos[0] == "VID02")
            {
                cbbPlan.SelectedIndex = 2;
            }
            else if (datos[0] == "VID03")
            {
                cbbPlan.SelectedIndex = 3;
            }
            else if (datos[0] == "VID04")
            {
                cbbPlan.SelectedIndex = 4;
            }
            else if (datos[0] == "VID05")
            {
                cbbPlan.SelectedIndex = 5;
            }

            if (datos[4] == "true")
            {
                cbbSalud.SelectedIndex = 1;
            }
            else
            {
                cbbSalud.SelectedIndex = 2;
            }

            txtPrimaAnu.Text = datos[5];
            txtPrimaMen.Text = datos[6];
            txtObsv.Text = datos[7];
            activarOpciones();
        }

        public void activarOpciones()
        {
            btnEditarCont.IsEnabled = true;
            btnTerminarContrato.IsEnabled = true;
            cbbPlan.IsEnabled = true;
            cbbSalud.IsEnabled = true;
            txtObsv.IsEnabled = true;
        }

        public void desactivarOpciones()
        {
            btnEditarCont.IsEnabled = false;
            btnTerminarContrato.IsEnabled = false;
            cbbListaContrato.IsEnabled = false;
            cbbPlan.IsEnabled = false;
            //dtpFechaInicio.IsEnabled = false;
            cbbSalud.IsEnabled = false;
            txtObsv.IsEnabled = false;
        }

        public void limpiar()
        {
            cbbRutCli.SelectedIndex = 0;
            txtNombreCliCon.Clear();
            cbbListaContrato.SelectedIndex = 0;
            cbbPlan.SelectedIndex = 0;
            txtPoliza.Clear();
            cbbSalud.SelectedIndex = 0;
            txtPrimaMen.Clear();
            txtPrimaAnu.Clear();
            txtObsv.Clear();
        }

        //funcion se agrega en el onchange del cbbPlan
        public void calcularPlan()
        {
            string plan = cbbPlan.SelectedItem.ToString();
            string[] datosPoliza = conec.datosPoliza(plan);
            txtPoliza.Text = datosPoliza[0];
            //txtPoliza.Text = datosPoliza[1];
            PrimaBase = Convert.ToDouble(datosPoliza[1]);

            double total, recargoEdad = 0, recargoSexo = 0, recargoEstadoC = 0, recargoBase = 0;

            if (Edad < 18 && Edad > 25)
            {
                recargoEdad = 3.6;
            }
            else if (Edad < 26 && Edad > 45)
            {
                recargoEdad = 2.4;
            }
            else if (Edad > 45)
            {
                recargoEdad = 6.0;
            }

            if (Sexo == 1)
            {
                recargoSexo = 2.4;
            }
            else if (Sexo == 2)
            {
                recargoSexo = 1.2;
            }

            if (EstadoC == 1)
            {
                recargoEstadoC = 4.8;
            }
            else if (EstadoC == 2)
            {
                recargoEstadoC = 2.4;
            }
            else if (EstadoC == 3 || EstadoC == 4)
            {
                recargoEstadoC = 3.6;
            }

            recargoBase = PrimaBase;
            total = recargoBase + recargoEdad + recargoSexo + recargoEstadoC;
            txtPrimaAnu.Text = total.ToString();
            txtPrimaMen.Text = Math.Round((total / 12), 2).ToString();
        }

        public async Task editarContAsync()
        {
            bool edita = false;

            string plan = cbbPlan.SelectedValue.ToString();
            string salud = cbbSalud.SelectedValue.ToString();
            string numero = cbbListaContrato.SelectedValue.ToString();
            if (salud == "Si")
            {
                salud = "1";
            }
            else
            {
                salud = "0";
            }

            string primaAnu = txtPrimaAnu.Text;
            string primaMen = txtPrimaMen.Text;
            string observacion = txtObsv.Text;
            /*objCont.CodigoPlan = plan;
            objCont.DeclaracionSalud = salud;
            objCont.PrimaAnual = primaAnu;
            objCont.PrimaMensual = primaMen;
            objCont.Observaciones = observacion;
            objCont.NumeroContrato = numero;
            edita = objCont.editarContrato();
            if (edita == true)
            {
                await this.ShowMessageAsync("Confirmacion!", "Contrato Modificado");
                limpiar();
            }
            else
            {
                await this.ShowMessageAsync("Error!", "Contrato no se pudo modificafr");
            }*/
        }

        public async Task terminarContAsync()
        {
            bool edita = false;
            /*objCont.Vigente = "0";
            objCont.NumeroContrato = cbbListaContrato.SelectedValue.ToString();
            edita = objCont.terminarContrato();
            if (edita == true)
            {
                await this.ShowMessageAsync("Confirmación!", "Contrato Terminado");
                limpiar();
                desactivarOpciones();
                txtNombreCliCon.Clear();
            }
            else
            {
                await this.ShowMessageAsync("Advertencia!", "Contrato no se puede Terminar");
            }*/
        }

        public void dataCliente()
        {
            cbbListaContrato.Items.Clear();
            string rut = cbbRutCli.SelectedValue.ToString();
            string[] listCont = conec.listContratos(rut);
            cbbListaContrato.SelectedIndex = 0;
            //cbbRutCli.Items.Add("Seleccione");
            for (int i = 0; i < listCont.Length; i++)
            {
                cbbListaContrato.Items.Add(listCont[i]);
            }
            string[] datos = conec.getDatosCliente(rut);
            txtNombreCliCon.Text = datos[0] + " " + datos[1];
        }

        private void cbbPlan_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            calcularPlan();
        }

        private void cbbListaContrato_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void cbbSalud_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void cbbRutCli_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            dataCliente();
        }

        private void txtNombreCliCon_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void txtPoliza_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void btnSalir_Click(object sender, RoutedEventArgs e)
        {
            this.Hide();
        }

        private void btnBuscarCto_Click(object sender, RoutedEventArgs e)
        {
            buscarContrato();
        }

        private void btnEditarCont_Click(object sender, RoutedEventArgs e)
        {
            editarContAsync();
        }

        private void btnTerminarContrato_Click(object sender, RoutedEventArgs e)
        {
            terminarContAsync();
        }

        bool Contraste = false;
        private void BtnContraste_Click(object sender, RoutedEventArgs e)
        {
            Contraste = !Contraste;
            if (Contraste)
            {

                {
                    SolidColorBrush PinkColor = new SolidColorBrush(Color.FromRgb(242, 0, 252));



                    gPrincipal.Background = Brushes.Black;
                    btnBuscarCto.Background = PinkColor;
                    btnEditarCont.Background = PinkColor;
                    btnSalir.Background = PinkColor;
                    btnTerminarContrato.Background = PinkColor;


                }
            }
            else
            {
                editarContrato editarContrato = new editarContrato();
                editarContrato.Show();
                this.Hide();


            }
        }
    }
}
