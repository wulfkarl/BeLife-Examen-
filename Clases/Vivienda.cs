using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clases
{
    public class Vivienda:Contrato{

        Conexion objConec = new Conexion();
        public string CodigoPostal {get;set;}
        public int Anio { get; set; }
        public string Direccion { get; set; }
        public int ValorInmu { get; set; }
        public int ValorConte { get; set; }
        public int Region { get; set; }
        public string Comuna { get; set; }

        public override bool agregarContrato(){

            string sql = "INSERT INTO Contrato VALUES ('" + NumeroContrato + "', GETDATE(), " +
                            "convert(date, '" + FechaFinVigencia + "'), '" + RutCliente +
                        "',(SELECT idPlan FROM [Plan] WHERE Nombre = '" + CodigoPlan + "'), " + TipoContrato
                        + ", convert(date, '" + FechaInicioVigencia + "'), convert(date, '" + FechaFinVigencia + "'), "
                        + Vigente + ", 0," + PrimaAnual.Replace(",", ".") + "," + PrimaMensual.Replace(",", ".") +
                        ",'" + Observaciones + "');";

            string sql1 = "INSERT INTO Vivienda VALUES ('"+CodigoPostal+"', "+Anio+",'"+Direccion+"', "+ValorInmu+","
                            +ValorConte+","+Region+ ",(SELECT idComuna FROM Comuna WHERE NombreComuna = '" + Comuna+"'));";
            string sql2 = "INSERT INTO Contrato_vivienda VALUES ('" + NumeroContrato + "', '" + CodigoPostal+ "');";

            bool guarda = objConec.insertar(sql);

            bool guarda1 = objConec.insertar(sql1);
            
            bool guarda2 = objConec.insertar(sql2);

            if (guarda == true)
            {
                return guarda;
            }
            else
            {
                return guarda;
            }
        }
    }
}
