using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clases
{
    public class Salud:Contrato
    {
        Conexion objConec = new Conexion();
        public string DeclaracionSalud { get; set; }

        public override bool agregarContrato(){

            string sql = "INSERT INTO Contrato VALUES ('" + NumeroContrato + "', GETDATE(), " +
                           "convert(date, '" + FechaFinVigencia + "'), '" + RutCliente +
                        "',(SELECT idPlan FROM [Plan] WHERE Nombre = '" + CodigoPlan + "'), " +TipoContrato
                        + ", convert(date, '" +FechaInicioVigencia + "'), convert(date, '" + FechaFinVigencia + "'), " 
                        + Vigente + ", " + DeclaracionSalud + "," + PrimaAnual.Replace(",", ".") + "," 
                        + PrimaMensual.Replace(",", ".") + ",'" + Observaciones + "');";

            bool guarda = objConec.insertar(sql);

            if (guarda == true){
                return guarda;
            }else{
                return guarda;
            }
        }

    }
}
