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
    /// Lógica de interacción para ListarCliente.xaml
    /// </summary>
    public partial class ListarCliente : MetroWindow
    {
        public Conexion conec = new Conexion();
        public Cliente objCli = new Cliente();
        public Selecciones objSelec = new Selecciones();
        private List<Cliente> clientes = new List<Cliente>();

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

        public ListarCliente()
        {
            InitializeComponent();
            LlenarCombo();

            clientes = conec.ListarClientes();
            dtgCliente.ItemsSource = clientes;
            Largo = clientes.Count();
            WindowStartupLocation = System.Windows.WindowStartupLocation.CenterScreen;
        }

        public void LlenarCombo()
        {
            string[] listSex = objSelec.listSexo();

            cmbSexo.SelectedIndex = 0;
            cmbSexo.Items.Add("Seleccione");
            for (int i = 0; i < listSex.Length; i++)
            {
                cmbSexo.Items.Add(listSex[i]);
            }

            string[] listEC = objSelec.listEstadoCivil();

            cmbEstadoCivil.SelectedIndex = 0;
            cmbEstadoCivil.Items.Add("Seleccione");
            for (int x = 0; x < listEC.Length; x++)
            {
                cmbEstadoCivil.Items.Add(listEC[x]);
            }
        }

        public bool validar(string tabla, string rut)
        {
            string condicion = "RutCliente = '" + rut + "'";
            return conec.validar(tabla, condicion);
        }

        private async void BtnCerrar_Click(object sender, RoutedEventArgs e)
        {
            MessageDialogResult m = await this.ShowMessageAsync("Salir", "¿Seguro desea salir del listado de clientes?", MessageDialogStyle.AffirmativeAndNegative);
            if (m == MessageDialogResult.Affirmative)
            {
                this.Hide();
            }
            if (m == MessageDialogResult.Negative)
            {
                txtRut.Clear();
            }
        }

        private void btnBuscar_Click(object sender, RoutedEventArgs e)
        {
            string rut = txtRut.Text;
            string filtro = "RutCliente = '" + rut + "';";
            dtgCliente.ItemsSource = null;

            clientes = conec.filtroClientes(filtro);

            dtgCliente.ItemsSource = clientes;
            Largo = clientes.Count();
        }

        private async void dtgCliente_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            int index = (dtgCliente.SelectedIndex) + 1;
            int largoDG = dtgCliente.Items.Count;

            if (index > Largo)
            {
                await this.ShowMessageAsync("Advertencia!", "No se pueden mostrar datos de una fila vacía");
            }
            else
            {
                Cliente objCli = dtgCliente.SelectedItem as Cliente;
                string nom = objCli.Nombre;
                string ap = objCli.Apellido;
                string rut = objCli.Rut;
                string sexo = objCli.Sexo;
                string estC = objCli.EstadoCivil;
                string fecN = objCli.FechaNacimiento;
                editarCliente edCli = new editarCliente(nom, ap, rut, sexo, estC, fecN);
                this.Hide();
                edCli.Owner = this;
                edCli.Show();
            }
        }

        private void cmbEstadoCivil_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            dtgCliente.ItemsSource = null;
            string estadoC = cmbEstadoCivil.SelectedIndex.ToString();
            string filtro = "idEstadoCivil = " + estadoC;

            clientes = conec.filtroClientes(filtro);
            dtgCliente.ItemsSource = clientes;
            Largo = clientes.Count();
        }

        private void cmbSexo_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            dtgCliente.ItemsSource = null;
            string sexo = cmbSexo.SelectedIndex.ToString();
            string filtro = "idSexo = " + sexo;

            clientes = conec.filtroClientes(filtro);

            dtgCliente.ItemsSource = clientes;
            Largo = clientes.Count();
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
                    dtgCliente.Background = PinkColor;


                }
            }
            else
            {
                ListarCliente listarCliente = new ListarCliente();
                listarCliente.Show();
                this.Hide();


            }
        }
    }
}