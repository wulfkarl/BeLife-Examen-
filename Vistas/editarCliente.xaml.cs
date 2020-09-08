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
using Clases;

namespace BeLifeV2
{
    /// <summary>
    /// Lógica de interacción para editarCliente.xaml
    /// </summary>
    public partial class editarCliente : MetroWindow
    {

        public Conexion conec = new Conexion();
        public Cliente objCli = new Cliente();
        public Selecciones objSelec = new Selecciones();
        private List<Cliente> clientes = new List<Cliente>();

        public editarCliente()
        {
            InitializeComponent();
            limpiar();
            LlenarCombo();
            WindowStartupLocation = System.Windows.WindowStartupLocation.CenterScreen;
        }

        public editarCliente(string nombre, string apellido, string rut, string sexo, string estado, string fecha)
        {
            InitializeComponent();
            LlenarCombo();
            txtNombCli.Text = nombre;
            txtRutCli.Text = rut;
            txtApCli.Text = apellido;

            if (sexo == "Masculino")
            {
                cbbSexo.SelectedIndex = 1;
            }
            else if (sexo == "Femenino")
            {
                cbbSexo.SelectedIndex = 2;
            }

            if (estado == "Soltero")
            {
                cbbEC.SelectedIndex = 1;
            }
            else if (estado == "Casado")
            {
                cbbEC.SelectedIndex = 2;
            }
            else if (estado == "Divorciado")
            {
                cbbEC.SelectedIndex = 3;
            }
            else if (estado == "Viudo")
            {
                cbbEC.SelectedIndex = 4;
            }

            dtpFechaNacCli.DisplayDate = Convert.ToDateTime(fecha);
            dtpFechaNacCli.SelectedDate = Convert.ToDateTime(fecha);
            //limpiar();
            activarOpciones();
            WindowStartupLocation = System.Windows.WindowStartupLocation.CenterScreen;
        }

        public void activarOpciones()
        {
            txtRutCli.IsEnabled = false;
            btnEditarCli.IsEnabled = true;
            btnEliminarCli.IsEnabled = true;
            cbbEC.IsEnabled = true;
            cbbSexo.IsEnabled = true;
            dtpFechaNacCli.IsEnabled = true;
            txtNombCli.IsEnabled = true;
            txtApCli.IsEnabled = true;
        }

        public void desactivarOpciones()
        {
            btnEditarCli.IsEnabled = false;
            btnEliminarCli.IsEnabled = false;
            cbbEC.IsEnabled = false;
            cbbSexo.IsEnabled = false;
            dtpFechaNacCli.IsEnabled = false;
            txtNombCli.IsEnabled = false;
            txtApCli.IsEnabled = false;
            txtRutCli.IsEnabled = true;
        }

        public void limpiar()
        {
            txtApCli.Clear();
            txtNombCli.Clear();
            txtRutCli.Clear();
            dtpFechaNacCli.DisplayDate = DateTime.Today;
            cbbSexo.SelectedIndex = 0;
            cbbEC.SelectedIndex = 0;
        }

        public void LlenarCombo()
        {

            string[] listSex = objSelec.listSexo();

            cbbSexo.SelectedIndex = 0;
            cbbSexo.Items.Add("Seleccione");
            for (int i = 0; i < listSex.Length; i++)
            {
                cbbSexo.Items.Add(listSex[i]);
            }

            string[] listEC = objSelec.listEstadoCivil();

            cbbEC.SelectedIndex = 0;
            cbbEC.Items.Add("Seleccione");
            for (int x = 0; x < listEC.Length; x++)
            {
                cbbEC.Items.Add(listEC[x]);
            }

        }

        public async Task editarClienteAsync()
        {
            try
            {
                bool edita = false;
                string nombre = txtNombCli.Text;
                string apellido = txtApCli.Text;
                string rut = txtRutCli.Text;
                DateTime fechaC = dtpFechaNacCli.SelectedDate.Value;
                string fecNac = fechaC.Year.ToString() + "-" + fechaC.Month.ToString() + "-" + fechaC.Day.ToString();
                string sexo = cbbSexo.SelectedIndex.ToString();
                string estadoCi = cbbEC.SelectedIndex.ToString();
                objCli.Rut = rut;
                objCli.Nombre = nombre;
                objCli.Apellido = apellido;
                objCli.FechaNacimiento = fecNac;
                objCli.EstadoCivil = estadoCi;
                objCli.Sexo = sexo;

                edita = objCli.editarCliente(rut);
                if (edita == true)
                {
                    await this.ShowMessageAsync("Cliente Editado", "Confirmacion!");
                    limpiar();
                    desactivarOpciones();
                }
                else
                {
                    await this.ShowMessageAsync("Editar Cliente", "Error!");
                }
            }
            catch (Exception error)
            {
                await this.ShowMessageAsync("Editar Cliente", error.Message);
            }
        }

        public async Task buscarClienteAsync()
        {
            string rut = txtRutCli.Text;
            string[] datos;
            bool valida = objCli.validar("Cliente", rut);
            if (valida == false)
            {
                datos = conec.getDatosCliente(rut);
                txtNombCli.Text = datos[0];
                txtApCli.Text = datos[1];
                dtpFechaNacCli.DisplayDate = Convert.ToDateTime(datos[2]);
                dtpFechaNacCli.SelectedDate = Convert.ToDateTime(datos[2]);
                cbbSexo.SelectedIndex = int.Parse(datos[3]);
                cbbEC.SelectedIndex = int.Parse(datos[4]);
                activarOpciones();
            }
            else
            {
                await this.ShowMessageAsync("Advertencia!", "El RUT " + rut + " no ha sido ingresado");
            }
            //await this.ShowMessageAsync("Advertencia!", "El RUT " + rut + " no ha sido ingresado");
        }

        public async Task eliminarClienteAsync()
        {
            bool elimina = false;
            string rut = txtRutCli.Text;
            objCli.Rut = rut;

            if (objCli.clienteContrato(rut) == true)
            {
                elimina = objCli.eliminarCliente(rut);
                await this.ShowMessageAsync("Confirmación!", "Cliente Elimnado");
                limpiar();
                desactivarOpciones();
            }
            else
            {
                await this.ShowMessageAsync("Advetencia!", "El cliente tiene un contrato en vigencia, no se puede eliminar");
            }
        }


        private void txtNombCli_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void txtRutCli_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void txtApCli_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void btnBuscarCli_Click(object sender, RoutedEventArgs e)
        {
            buscarClienteAsync();
        }

        private void cbbEC_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void cbbSexo_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void btnEditarCli_Click(object sender, RoutedEventArgs e)
        {
            editarClienteAsync();
        }

        private void btnEliminarCli_Click(object sender, RoutedEventArgs e)
        {
            eliminarClienteAsync();
        }

        private void btnLimpiarCli_Click(object sender, RoutedEventArgs e)
        {
            limpiar();
        }

        private void btnListarCli_Click(object sender, RoutedEventArgs e)
        {
            ListarCliente listCli = new ListarCliente();

            //listCli.Owner = this;
            this.Hide();
            listCli.ShowDialog();
            this.Close();
        }

        private void btnCerrar_Click(object sender, RoutedEventArgs e)
        {
            this.Hide();
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
                    btnEditarCli.Background = PinkColor;
                    btnEliminarCli.Background = PinkColor;
                    btnLimpiarCli.Background = PinkColor;
                    btnListarCli.Background = PinkColor;
                    btnBuscarCli.Background = PinkColor;

                }
            }
            else
            {
                editarCliente editarCliente = new editarCliente();
                editarCliente.Show();
                this.Hide();


            }
        }
    }
}
