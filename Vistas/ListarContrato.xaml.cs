using Clases;
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
using MahApps.Metro.Controls;
using MahApps.Metro.Controls.Dialogs;
using MahApps.Metro.Behaviours;
using MahApps.Metro.Actions;

namespace BeLifeV2
{
    /// <summary>
    /// Lógica de interacción para ListarContrato.xaml
    /// </summary>
    public partial class ListarContrato : MetroWindow
    {
        Conexion conec = new Conexion();

        //private List<Contrato> contratos = new List<Contrato>();

        private int _largo = 0;

        public int Largo
        {
            get
            {
                return _largo;
            }

            set
            {
                _largo = value;
            }
        }

        public ListarContrato()
        {
            InitializeComponent();

            listCombo();
            /*contratos = conec.ListarContratos();
            dtgContrato.ItemsSource = contratos;
            Largo = contratos.Count();*/
            WindowStartupLocation = System.Windows.WindowStartupLocation.CenterScreen;
        }

        public void listCombo()
        {
            string[] listRut = conec.listRut();

            //string[] listPlan = conec.listPlan();

            string[] numeroCon = conec.getListCont();
            cmbNumContrato.SelectedIndex = 0;
            cmbNumContrato.Items.Add("Seleccione");
            for (int i = 0; i < numeroCon.Length; i++)
            {
                cmbNumContrato.Items.Add(numeroCon[i]);
            }

            string[] polizas = conec.getlistPoliza();
            cmbPoliza.SelectedIndex = 0;
            cmbPoliza.Items.Add("Seleccione");
            for (int z = 0; z < polizas.Length; z++)
            {
                cmbPoliza.Items.Add(polizas[z]);
            }
        }

        private async void dtgContrato_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            int index = (dtgContrato.SelectedIndex) + 1;
            int largoDG = dtgContrato.Items.Count;

            if (index > Largo)
            {
                await this.ShowMessageAsync("Advertencia!", "No se pueden mostrar datos de una fila vacía");
            }
            else
            {
                try
                {
                    /*Contrato objCont = dtgContrato.SelectedItem as Contrato;
                    string num = objCont.NumeroContrato;
                    string rut = objCont.RutCliente;
                    string plan = objCont.CodigoPlan;
                    string salud = objCont.DeclaracionSalud;
                    string primaAn = objCont.PrimaAnual;
                    string primaMe = objCont.PrimaMensual;
                    string obs = objCont.Observaciones;
                    string fecIni = objCont.FechaInicioVigencia;

                    editarContrato edCont = new editarContrato(num, rut, plan, salud, primaAn, primaMe, obs);
                    this.Hide();
                    edCont.Owner = this;
                    edCont.Show();*/
                }
                catch (Exception)
                {
                    await this.ShowMessageAsync("Error!", "");
                }
            }
        }

        private void btnBuscar_Click(object sender, RoutedEventArgs e)
        {
            string rut = txtRut.Text;
            string filtro = "RutCliente = '" + rut + "';";
            dtgContrato.ItemsSource = null;

            /*contratos = conec.filtroContratos(filtro);

            dtgContrato.ItemsSource = contratos;
            Largo = contratos.Count();*/
        }

        private void cmbNumContrato_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            dtgContrato.ItemsSource = null;
            string numeroC = cmbNumContrato.SelectedItem.ToString();
            string filtro = " Numero = '" + numeroC + "';";

            /*contratos = conec.filtroContratos(filtro);
            dtgContrato.ItemsSource = contratos;
            Largo = contratos.Count();*/
        }

        private void cmbPoliza_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            dtgContrato.ItemsSource = null;
            string poliza = cmbPoliza.SelectedItem.ToString();
            string filtro = "CodigoPlan = (SELECT idPlan FROM [Plan] WHERE PolizaActual = '" + poliza + "')";

            /*contratos = conec.filtroContratos(filtro);
            dtgContrato.ItemsSource = contratos;
            Largo = contratos.Count();*/
        }

        private async void BtnCerrar_Click(object sender, RoutedEventArgs e)
        {
            MessageDialogResult m = await this.ShowMessageAsync("Salir", "¿Seguro desea salir del listado de contratos?", MessageDialogStyle.AffirmativeAndNegative);
            if (m == MessageDialogResult.Affirmative)
            {
                this.Hide();
            }
            if (m == MessageDialogResult.Negative)
            {
                txtRut.Clear();
            }
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
                    btnCerrar.Background = PinkColor;
                    btnBuscar.Background = PinkColor;
                    btnCerrar.Background = PinkColor;
                    dtgContrato.Background = PinkColor;


                }
            }
            else
            {
                ListarContrato listarContrato = new ListarContrato();
                listarContrato.Show();
                this.Hide();


            }
        }
     }
}
