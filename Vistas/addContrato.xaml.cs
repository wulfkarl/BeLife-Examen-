using Clases;
using MahApps.Metro.Controls;
using MahApps.Metro.Controls.Dialogs;
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

namespace Vistas
{
    /// <summary>
    /// Lógica de interacción para addContrato.xaml
    /// </summary>
    public partial class addContrato : MetroWindow
    {
        public Conexion conec = new Conexion();
        public Cliente objCli = new Cliente();
        public Selecciones objSelec = new Selecciones();
        public Contrato objCont;
        public Vehiculo objVehi;
        public Vivienda objViv;
        public Salud objSalud;
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

        public addContrato()
        {
            InitializeComponent();
            listCombo();
            cnvAuto.Visibility = Visibility.Hidden;
            cnvHogar.Visibility = Visibility.Hidden;
            cnvDatos.Margin = new Thickness(10, 156, 0, 0);
        }

        public void listCombo()
        {
            string[] listRut = conec.listRut();

            string[] listTipCont = objSelec.listTipoCont();
            cbbTipoCont.SelectedIndex = 0;
            cbbTipoCont.Items.Add("Seleccione");
            for (int x = 0; x < listTipCont.Length; x++)
            {
                cbbTipoCont.Items.Add(listTipCont[x]);
            }

            string[] listMarca = objSelec.listMarca();
            cbbMarca.SelectedIndex = 0;
            cbbMarca.Items.Add("Seleccione");
            for (int x = 0; x < listMarca.Length; x++)
            {
                cbbMarca.Items.Add(listMarca[x]);
            }

            string[] listRegion = objSelec.listRegion();
            cbbRegion.SelectedIndex = 0;
            cbbRegion.Items.Add("Seleccione");
            for (int x = 0; x < listRegion.Length; x++)
            {
                cbbRegion.Items.Add(listRegion[x]);
            }

            cbbSalud.SelectedIndex = 0;
            cbbSalud.Items.Add("Seleccione");
            cbbSalud.Items.Add("No");
            cbbSalud.Items.Add("Si");
        }

        public async void TipoContrato()
        {

            int idTipoCont = cbbTipoCont.SelectedIndex * 10;
            /*if (idTipoCont == 10){
                idTipoCont = 30;
            }else if (idTipoCont ==30){
                idTipoCont = 10;
            }*/
            string[] listPlan = conec.listPlan(idTipoCont);
            cbbPlan.Items.Clear();
            cbbPlan.Items.Add("Seleccione");
            cbbPlan.SelectedIndex = 0;

            for (int x = 0; x < listPlan.Length; x++)
            {
                cbbPlan.Items.Add(listPlan[x]);
            }

            if (cbbTipoCont.SelectedIndex == 2)
            {
                cnvAuto.Visibility = Visibility.Visible;
                cnvHogar.Visibility = Visibility.Hidden;
                cnvDatos.Margin = new Thickness(10, 352, 0, 0);
                lblSalud.Visibility = Visibility.Hidden;
                cbbSalud.Visibility = Visibility.Hidden;
            }
            else if (cbbTipoCont.SelectedIndex == 3)
            {
                cnvAuto.Visibility = Visibility.Hidden;
                cnvHogar.Visibility = Visibility.Visible;
                cnvHogar.Margin = new Thickness(10, 156, 0, 0);
                cnvDatos.Margin = new Thickness(10, 392, 0, 0);
                lblSalud.Visibility = Visibility.Hidden;
                cbbSalud.Visibility = Visibility.Hidden;
            }
            else if (cbbTipoCont.SelectedIndex == 1)
            {
                cnvAuto.Visibility = Visibility.Hidden;
                cnvHogar.Visibility = Visibility.Hidden;
                cnvDatos.Margin = new Thickness(10, 156, 0, 0);
                lblSalud.Visibility = Visibility.Visible;
                cbbSalud.Visibility = Visibility.Visible;
                await this.ShowMessageAsync("¡Advertencia!", "Recordar que Declaración de salud es Obligatoria");
            }
        }

        public void limpiar()
        {
            txtNombreCliCon.Clear();
            txtRutCont.Clear();
            cbbPlan.SelectedIndex = 0;
            txtPoliza.Clear();
            dtpFechaInicio.DisplayDate = DateTime.Today;
            dtpFechaInicio.SelectedDate = DateTime.Today;
            cbbSalud.SelectedIndex = 0;
            txtPrimaMen.Clear();
            txtPrimaAnu.Clear();
            txtObsv.Clear();
        }

        public void calcularPlan()
        {

            bool itemsPlan = cbbPlan.HasItems;

            if (itemsPlan == true)
            {
                string plan = cbbPlan.SelectedItem.ToString();
                string[] datosPoliza = conec.datosPoliza(plan);
                txtPoliza.Text = datosPoliza[0];
                PrimaBase = Convert.ToDouble(datosPoliza[1]);
                int idTipoCont = cbbTipoCont.SelectedIndex;

                double total, recargoEdad = 0, recargoSexo = 0, recargoEstadoC = 0, recargoBase = 0,
                    recargoAnioVeh = 0, recargoAnioHog = 0, recargoReg = 0;

                if (idTipoCont == 1)
                {
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
                }
                else if (idTipoCont == 2)
                {
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

                    int anioVehi = int.Parse(txtAnioVe.Text);
                    if (anioVehi == DateTime.Today.Year)
                    {
                        recargoAnioVeh = 1.2;
                    }
                    else if (anioVehi < (DateTime.Today.Year - 5))
                    {
                        recargoAnioVeh = 0.8;
                    }
                    else if (anioVehi > (DateTime.Today.Year - 5))
                    {
                        recargoAnioVeh = 0.4;
                    }
                }
                else if (idTipoCont == 3)
                {
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

                    int anioViv = int.Parse(txtAnioVi.Text);
                    if (anioViv < 5 && anioViv > 0)
                    {
                        recargoAnioHog = 1.0;
                    }
                    else if (anioViv < 10 && anioViv > 5)
                    {
                        recargoAnioHog = 0.8;
                    }
                    else if (anioViv < 30 && anioViv > 10)
                    {
                        recargoAnioHog = 0.4;
                    }
                    else if (anioViv > 30)
                    {
                        recargoAnioHog = 0.2;
                    }

                    int idRegion = cbbRegion.SelectedIndex;
                    if (idRegion == 13)
                    {
                        recargoReg = 3.2;
                    }
                    else
                    {
                        recargoReg = 2.8;
                    }
                }

                recargoBase = PrimaBase;
                total = recargoBase + recargoEdad + recargoSexo + recargoEstadoC + recargoAnioVeh + recargoAnioHog + recargoReg;
                txtPrimaAnu.Text = total.ToString();
                txtPrimaMen.Text = Math.Round((total / 12), 2).ToString();
            }


        }

        public async void buscarCli()
        {
            string rut = txtRutCont.Text;
            bool existe = objCli.validar("Cliente", rut);

            if (existe == false)
            {
                string[] datos = conec.getDatosCliente(rut);
                txtNombreCliCon.Text = datos[0] + " " + datos[1];
                Edad = (DateTime.Now.Subtract(Convert.ToDateTime(datos[2])).Days / 365);
                Sexo = int.Parse(datos[3]);
                EstadoC = int.Parse(datos[4]);
            }
            else
            {
                await this.ShowMessageAsync("Advertencia!", "El Cliente no ha sido ingresado en la Base de Datos");
                txtNombreCliCon.Clear();
            }
        }

        //funciona
        public async void guardarVida()
        {
            //try{
            bool guarda = false;
            string rutCliente = txtRutCont.Text;

            DateTime localDate = DateTime.Now;
            string mes = "", dia = "", minu = "", seg = "";
            string anio = localDate.Year.ToString();
            mes = localDate.Month.ToString();
            dia = localDate.Day.ToString();
            string hora = localDate.Hour.ToString();
            minu = localDate.Minute.ToString();
            seg = localDate.Second.ToString();

            if (mes.Length == 1)
            {
                mes = "0" + mes;
            }
            if (dia.Length == 1)
            {
                dia = "0" + dia;
            }
            if (minu.Length == 1)
            {
                minu = "0" + minu;
            }
            if (seg.Length == 1)
            {
                seg = "0" + seg;
            }
            string numero = anio + mes + dia + hora + minu + seg;

            string plan = cbbPlan.SelectedValue.ToString();
            DateTime fechaIniVig = dtpFechaInicio.SelectedDate.Value;
            DateTime fechaFinVig = fechaIniVig.AddYears(1);
            string fechaVigencia = fechaIniVig.Year.ToString() + "-" + fechaIniVig.Month.ToString() + "-" + fechaIniVig.Day.ToString();
            string fechaFinVigencia = fechaFinVig.Year.ToString() + "-" + fechaFinVig.Month.ToString() + "-" + fechaFinVig.Day.ToString();
            string salud = cbbSalud.SelectedValue.ToString();

            if (salud == "Si")
            {
                salud = "1";
            }
            else if (salud == "No")
            {
                salud = "0";
            }
            else
            {
                salud = "";
            }
            string primaAnu = txtPrimaAnu.Text;
            string primaMen = txtPrimaMen.Text;
            string observacion = txtObsv.Text;
            int idTipoCont = cbbTipoCont.SelectedIndex * 10;
            /*if (idTipoCont == 10)
            {
                idTipoCont = 30;
            }
            else if (idTipoCont == 30)
            {
                idTipoCont = 10;
            }*/

            objSalud = new Salud();
            objSalud.RutCliente = rutCliente;
            objSalud.NumeroContrato = numero;
            objSalud.CodigoPlan = plan;
            objSalud.FechaInicioVigencia = fechaVigencia;
            objSalud.FechaFinVigencia = fechaFinVigencia;
            objSalud.DeclaracionSalud = salud;
            objSalud.PrimaAnual = primaAnu;
            objSalud.PrimaMensual = primaMen;
            objSalud.Vigente = "1";
            objSalud.Observaciones = observacion;
            objSalud.TipoContrato = idTipoCont;


            //DateTime fecha_iniv = Convert.ToDateTime(value);
            int result = DateTime.Compare(fechaIniVig, DateTime.Today);
            int mesV = fechaIniVig.Month - DateTime.Today.Month;
            //if (result >= 0){
            if (mesV < 1)
            {
                guarda = objSalud.agregarContrato();
                if (guarda == true)
                {

                    MessageBox.Show("Contrato de Salud Ingresado", "Confirmacion!", MessageBoxButton.OK, MessageBoxImage.Information);
                    limpiar();
                }
                else
                {
                    MessageBox.Show("Contrato Ya ha Ingresado", "Advertencia!", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                }
            }
            else
            {
                await this.ShowMessageAsync("Advertencia!", "Mes de inicio de vigencia no puede ser superior a un mes");
            }
            /*}
            catch (Exception error)
            {
                MessageBox.Show(error.Message, "Error!", MessageBoxButton.OK, MessageBoxImage.Error);
            }*/
        }

        public async void guardarVehiculo()
        {
            //try{
            bool guarda = false;
            string rutCliente = txtRutCont.Text;

            DateTime localDate = DateTime.Now;
            string mes = "", dia = "", minu = "", seg = "";
            string anio = localDate.Year.ToString();
            mes = localDate.Month.ToString();
            dia = localDate.Day.ToString();
            string hora = localDate.Hour.ToString();
            minu = localDate.Minute.ToString();
            seg = localDate.Second.ToString();

            if (mes.Length == 1)
            {
                mes = "0" + mes;
            }
            if (dia.Length == 1)
            {
                dia = "0" + dia;
            }
            if (minu.Length == 1)
            {
                minu = "0" + minu;
            }
            if (seg.Length == 1)
            {
                seg = "0" + seg;
            }
            string numero = anio + mes + dia + hora + minu + seg;

            string plan = cbbPlan.SelectedValue.ToString();
            DateTime fechaIniVig = dtpFechaInicio.SelectedDate.Value;
            DateTime fechaFinVig = fechaIniVig.AddYears(1);
            string fechaVigencia = fechaIniVig.Year.ToString() + "-" + fechaIniVig.Month.ToString() + "-" + fechaIniVig.Day.ToString();
            string fechaFinVigencia = fechaFinVig.Year.ToString() + "-" + fechaFinVig.Month.ToString() + "-" + fechaFinVig.Day.ToString();
            string salud = cbbSalud.SelectedValue.ToString();

            if (salud == "Si")
            {
                salud = "1";
            }
            else if (salud == "No")
            {
                salud = "0";
            }
            else
            {
                salud = "";
            }
            string primaAnu = txtPrimaAnu.Text;
            string primaMen = txtPrimaMen.Text;
            string observacion = txtObsv.Text;
            int idTipoCont = cbbTipoCont.SelectedIndex * 10;
            /*if (idTipoCont == 10)
            {
                idTipoCont = 30;
            }
            else if (idTipoCont == 30)
            {
                idTipoCont = 10;
            }*/

            objVehi = new Vehiculo();
            objVehi.RutCliente = rutCliente;
            objVehi.NumeroContrato = numero;
            objVehi.CodigoPlan = plan;
            objVehi.FechaInicioVigencia = fechaVigencia;
            objVehi.FechaFinVigencia = fechaFinVigencia;
            //objVehi.DeclaracionSalud = salud;
            objVehi.PrimaAnual = primaAnu;
            objVehi.PrimaMensual = primaMen;
            objVehi.Vigente = "1";
            objVehi.Observaciones = observacion;
            objVehi.TipoContrato = idTipoCont;

            string patente = txtPatente.Text;
            string idMarca = cbbMarca.SelectedIndex.ToString();
            string idModelo = cbbModelo.SelectedItem.ToString();
            string anioVeh = txtAnioVe.Text;
            objVehi.Patente = patente;
            objVehi.Marca = int.Parse(idMarca);
            objVehi.Modelo = idModelo;
            objVehi.Anio = int.Parse(anioVeh);

            int result = DateTime.Compare(fechaIniVig, DateTime.Today);
            int mesV = fechaIniVig.Month - DateTime.Today.Month;
            if (mesV < 1)
            {
                guarda = objVehi.agregarContrato();
                if (guarda == true)
                {
                    await this.ShowMessageAsync("Confirmación!", "Contrato de Vehículo Ingresado");
                    limpiar();
                }
                else
                {
                    await this.ShowMessageAsync("Advertencia!", "Contrato ya se ingresó");
                }
            }
            else
            {
                await this.ShowMessageAsync("Advertencia!", "Mes de inicio de vigencia no puede ser superior a un mes");
            }
            /*}
            catch (Exception error)
            {
                MessageBox.Show(error.Message, "Error!", MessageBoxButton.OK, MessageBoxImage.Error);
            }*/
        }

        public async void guardarHogar()
        {
            //try{
            bool guarda = false;
            string rutCliente = txtRutCont.Text;

            DateTime localDate = DateTime.Now;
            string mes = "", dia = "", minu = "", seg = "";
            string anio = localDate.Year.ToString();
            mes = localDate.Month.ToString();
            dia = localDate.Day.ToString();
            string hora = localDate.Hour.ToString();
            minu = localDate.Minute.ToString();
            seg = localDate.Second.ToString();

            if (mes.Length == 1)
            {
                mes = "0" + mes;
            }
            if (dia.Length == 1)
            {
                dia = "0" + dia;
            }
            if (minu.Length == 1)
            {
                minu = "0" + minu;
            }
            if (seg.Length == 1)
            {
                seg = "0" + seg;
            }
            string numero = anio + mes + dia + hora + minu + seg;

            string plan = cbbPlan.SelectedValue.ToString();
            DateTime fechaIniVig = dtpFechaInicio.SelectedDate.Value;
            DateTime fechaFinVig = fechaIniVig.AddYears(1);
            string fechaVigencia = fechaIniVig.Year.ToString() + "-" + fechaIniVig.Month.ToString() + "-" + fechaIniVig.Day.ToString();
            string fechaFinVigencia = fechaFinVig.Year.ToString() + "-" + fechaFinVig.Month.ToString() + "-" + fechaFinVig.Day.ToString();
            string salud = cbbSalud.SelectedValue.ToString();

            if (salud == "Si")
            {
                salud = "1";
            }
            else if (salud == "No")
            {
                salud = "0";
            }
            else
            {
                salud = "";
            }
            string primaAnu = txtPrimaAnu.Text;
            string primaMen = txtPrimaMen.Text;
            string observacion = txtObsv.Text;
            int idTipoCont = cbbTipoCont.SelectedIndex * 10;
            /*if (idTipoCont == 10)
            {
                idTipoCont = 30;
            }
            else if (idTipoCont == 30)
            {
                idTipoCont = 10;
            }*/

            objViv = new Vivienda();
            objViv.RutCliente = rutCliente;
            objViv.NumeroContrato = numero;
            objViv.CodigoPlan = plan;
            objViv.FechaInicioVigencia = fechaVigencia;
            objViv.FechaFinVigencia = fechaFinVigencia;
            //objViv.DeclaracionSalud = salud;
            objViv.PrimaAnual = primaAnu;
            objViv.PrimaMensual = primaMen;
            objViv.Vigente = "1";
            objViv.Observaciones = observacion;
            objViv.TipoContrato = idTipoCont;

            string codigoPost = txtCodigoPost.Text;
            string anioVi = txtAnioVi.Text;
            string direc = txtDirec.Text;
            string valorInm = txtValorIn.Text;
            string valorCont = txtValorCont.Text;
            int idRegion = cbbRegion.SelectedIndex;
            string idComuna = cbbComuna.SelectedItem.ToString();
            objViv.CodigoPostal = codigoPost;
            objViv.Anio = int.Parse(anioVi);
            objViv.Direccion = direc;
            objViv.ValorInmu = int.Parse(valorInm);
            objViv.ValorConte = int.Parse(valorCont);
            objViv.Region = idRegion;
            objViv.Comuna = idComuna;

            int result = DateTime.Compare(fechaIniVig, DateTime.Today);
            int mesV = fechaIniVig.Month - DateTime.Today.Month;
            if (mesV < 1)
            {
                guarda = objViv.agregarContrato();
                if (guarda == true)
                {
                    await this.ShowMessageAsync("Confirmación!", "Contrato de Vivienda Ingresado");
                    limpiar();
                }
                else
                {
                    await this.ShowMessageAsync("Advertencia!", "Contrato ya se ha ingresado");
                }
            }
            else
            {
                await this.ShowMessageAsync("Advertencia!", "Mes de inicio de vigencia no puede ser superior a un mes");
            }
            /*}
            catch (Exception error)
            {
                MessageBox.Show(error.Message, "Error!", MessageBoxButton.OK, MessageBoxImage.Error);
            }*/
        }

        private void cbbTipoCont_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            TipoContrato();
        }

        private void cbbMarca_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            cbbModelo.Items.Clear();
            int idMarca = cbbMarca.SelectedIndex;
            if (idMarca != 0)
            {
                string[] listMarca = conec.listModelo(idMarca);
                cbbModelo.SelectedIndex = 0;
                cbbModelo.Items.Add("Seleccione");
                for (int x = 0; x < listMarca.Length; x++)
                {
                    cbbModelo.Items.Add(listMarca[x]);
                }
            }
        }

        private void cbbRegion_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            cbbComuna.Items.Clear();
            string[] listComuna = conec.listComuna(cbbRegion.SelectedIndex);
            cbbComuna.SelectedIndex = 0;
            cbbComuna.Items.Add("Seleccione");
            for (int x = 0; x < listComuna.Length; x++)
            {
                cbbComuna.Items.Add(listComuna[x]);
            }
        }

        private void cbbPlan_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            calcularPlan();
        }

        private void txtAnioVi_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key >= Key.D0 && e.Key <= Key.D9 || e.Key >= Key.NumPad0 && e.Key <= Key.NumPad9)
                e.Handled = false;
            else
                e.Handled = true;
        }

        private void buscarCli(object sender, RoutedEventArgs e)
        {
            buscarCli();
        }

        private void txtValorIn_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key >= Key.D0 && e.Key <= Key.D9 || e.Key >= Key.NumPad0 && e.Key <= Key.NumPad9)
                e.Handled = false;
            else
                e.Handled = true;
        }

        private void txtValorCont_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key >= Key.D0 && e.Key <= Key.D9 || e.Key >= Key.NumPad0 && e.Key <= Key.NumPad9)
                e.Handled = false;
            else
                e.Handled = true;
        }

        private void txtAnioVe_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key >= Key.D0 && e.Key <= Key.D9 || e.Key >= Key.NumPad0 && e.Key <= Key.NumPad9)
                e.Handled = false;
            else
                e.Handled = true;
        }

        private void btnGuardar_Click(object sender, RoutedEventArgs e)
        {
            if (cbbTipoCont.SelectedIndex == 2)
            {
                guardarVehiculo();
            }
            else if (cbbTipoCont.SelectedIndex == 3)
            {
                guardarHogar();
            }
            else if (cbbTipoCont.SelectedIndex == 1)
            {
                guardarVida();
            }
        }

        private void txtCodigoPost_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key >= Key.D0 && e.Key <= Key.D9 || e.Key >= Key.NumPad0 && e.Key <= Key.NumPad9)
                e.Handled = false;
            else
                e.Handled = true;
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
                    btnGuardar.Background = PinkColor;
                    btnLimpiar.Background = PinkColor;



                }
            }
            else
            {
                addContrato addContrato = new addContrato();
                addContrato.Show();
                this.Hide();


            }
        }
    }
}
