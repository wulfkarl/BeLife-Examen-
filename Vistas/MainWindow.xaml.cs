using BeLifeV2;
using MahApps.Metro.Controls;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Vistas
{
    /// <summary>
    /// Lógica de interacción para MainWindow.xaml
    /// </summary>
    public partial class MainWindow : MetroWindow
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void btn_agregar_cliente_Click(object sender, RoutedEventArgs e)
        {
            addCliente addCliente = new addCliente();
            addCliente.Owner = this;
            addCliente.ShowDialog();
        }

        private void btn_agregar_contrato_Click(object sender, RoutedEventArgs e)
        {
            
            addContrato addContrato = new addContrato();
            addContrato.Owner = this;
            addContrato.ShowDialog();
            
        }

        private void btn_editar_cliente_Click(object sender, RoutedEventArgs e)
        {
            editarCliente editarCliente = new editarCliente();
            editarCliente.Show();
            editarCliente.ShowDialog();

        }

        private void btn_listar_cliente_Click(object sender, RoutedEventArgs e)
        {
            ListarCliente listarCliente = new ListarCliente();
            listarCliente.Show();
            listarCliente.ShowDialog();

        }

        private void btn_editar_contrato_Click(object sender, RoutedEventArgs e)
        {
            editarContrato editarContrato = new editarContrato();
            editarContrato.Show();
            editarContrato.ShowDialog();

        }

        private void btn_listar_contrato_Click(object sender, RoutedEventArgs e)
        {
            ListarContrato listarContrato = new ListarContrato();
            listarContrato.Show();
            listarContrato.ShowDialog();

        }

        private void btn_listar_contrato_Click_1(object sender, RoutedEventArgs e)
        {
            ListarContrato listarContrato = new ListarContrato();
            listarContrato.Show();
        }

        private void btn_listar_contrato_Click_3(object sender, RoutedEventArgs e)
        {

            ListarContrato listarContrato = new ListarContrato();
            listarContrato.Show();
        }

        private void btn_editar_contrato_Click_1(object sender, RoutedEventArgs e)
        {
            editarContrato editarContrato = new editarContrato();
            editarContrato.Show();
        }

        private void btn_editar_cliente_Click_1(object sender, RoutedEventArgs e)
        {
            editarCliente editarCliente = new editarCliente();
            editarCliente.Show();
        }

        private void btn_listar_cliente_Click_1(object sender, RoutedEventArgs e)
        {
            ListarCliente listarCliente = new ListarCliente();
            listarCliente.Show();
        
        }
    }
}
