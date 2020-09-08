using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clases
{
    public class Selecciones{
        Conexion conec = new Conexion();

        //llena un arreglo con la infomración de la tabla que se le ingresa por parametros
        public string[] listSexo(){
            string[] listSex = conec.listSelec("Sexo");
            return listSex;
        }

        //llena un arreglo con la infomración de la tabla que se le ingresa por parametros
        public string[] listEstadoCivil(){
            string[] listEC = conec.listSelec("EstadoCivil");
            return listEC;
        }

        public string[] listMarca(){
            string[] listMar = conec.listSelec("MarcaVehiculo");
            return listMar;
        }

        /*public string[] listModelo()
        {
            string[] listMod = conec.listSelec("ModeloVehiculo");
            return listMod;
        }*/

        public string[] listTipoCont()
        {
            string[] listTC = conec.listSelec("TipoContrato");
            return listTC;
        }

        public string[] listRegion()
        {
            string[] listRegion = conec.listSelec("Region");
            return listRegion;
        }

    }
}
