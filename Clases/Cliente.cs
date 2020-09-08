using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clases
{
    public class Cliente{
        Conexion conec = new Conexion();
        private string _rut;
        private string _nombre;
        private string _apellido;
        private string _fechaNacimiento;
        private string _sexo;
        private string _estadoCivil;


        public string Rut{
            get
            {
                return _rut;
            }

            set
            {
                if (value.Length == 10)
                {
                    _rut = value;
                }
                else
                {
                    throw new Exception("Largo rut 10");
                }
            }
        }

        public string Nombre
        {
            get
            {
                return _nombre;
            }

            set{
                if (value != "")
                {
                    _nombre = value;
                }
                else
                {
                    throw new Exception("Nombre no puede estar Vacio");
                }
            }
        }

        public string Apellido
        {
            get
            {
                return _apellido;
            }

            set
            {
                if (value != "")
                {
                    _apellido = value;
                }
                else
                {
                    throw new Exception("Apellido no puede estar Vacio");
                }
            }
        }

        public string FechaNacimiento
        {
            get
            {
                return _fechaNacimiento;
            }

            set
            {
                if (value != ""){
                    DateTime fecha_nac = Convert.ToDateTime(value);
                    int result = DateTime.Compare(fecha_nac, DateTime.Today);
                    int edad = DateTime.Today.Year - fecha_nac.Year;
                    if (result <= 0){
                        if (edad >18){
                            _fechaNacimiento = value;
                        }else{
                            throw new Exception("Cliente debe tener mas de 18 años");
                        }
                    }else{
                        throw new Exception("Fecha de nacimiento no puede ser Superior a la fecha Actual");
                    }
                }else{
                    throw new Exception("Fecha de nacimiento no puede ser vacia");
                }                
            }
        }

        public string Sexo
        {
            get
            {
                return _sexo;
            }

            set
            {
                if (value != "0")
                {
                    _sexo = value;
                }
                else
                {
                    throw new Exception("Debe seleccionar un Sexo");
                }
            }
        }

        public string EstadoCivil
        {
            get
            {
                return _estadoCivil;
            }

            set
            {
                if (value != "0")
                {
                    _estadoCivil = value;
                }
                else
                {
                    throw new Exception("Debe seleccionar un Estado Civil");
                }
            }
        }

        //guarda el cliente en la BD
        public bool guardarCliente(){
            //conec.abrirConexion();
            if (this.validar("Cliente",Rut)==true){
                string sql = "INSERT INTO Cliente VALUES ('" + Rut + "','" + Nombre + "','" + Apellido + "',convert(date,'" + FechaNacimiento + "'),"+Sexo+","+EstadoCivil+")";
                bool guarda = conec.insertar(sql);
                if (guarda == true){
                    return guarda;
                }else{
                    return guarda;
                }
            }else{
                return false;
            }
            
        }

        //valida que el cliente no exista en la BD
        public bool validar(string tabla, string rut){
            string condicion = "RutCliente = '" + rut + "'";
            return conec.validar(tabla,condicion);
        }

        public bool editarCliente(string rutB){
            if (validar("Cliente", rutB) == false){
                string campos = " Nombres = '"+Nombre+"', Apellidos ='"+Apellido+"', FechaNacimiento = convert(date,'"+FechaNacimiento+"'), idSexo = "+Sexo+", idEstadoCivil ="+EstadoCivil;
                string condicion = " RutCliente = '" + Rut+"';";
                bool edita = conec.actualizar("Cliente", campos, condicion);
                if (edita == true){
                    return edita;
                }else{
                    return edita;
                }
            }else{
                return false;
            }
            //Retorna true si el cliente fue editado, de lo contrario retorna false
        }

        public bool eliminarCliente(string rutE){
            string condicion = " RutCliente = '" + Rut + "';";

            bool elimina = conec.eliminar("Cliente", condicion);
            
            return elimina;
        }

        public bool clienteContrato(string rut){
            string condicion = "RutCliente = '" + rut + "'";
            string tabla = "Contrato";
            return conec.validar(tabla, condicion);
        }
    }
}
