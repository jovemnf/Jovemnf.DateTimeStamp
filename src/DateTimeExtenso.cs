using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jovemnf.DateTimeStamp
{
    public class DateTimeExtenso
    {

        public static string ByHoras(int horas)
        {
            DataValue dv = checarHoras(horas, new DataValue());
            return getTexto(dv);
        }

        public static string ByMinutos(int minutos)
        {
            DataValue dv = checarMinutos(minutos, new DataValue());
            return getTexto(dv);
        }

        private static string getTexto(DataValue dv)
        {
            string str = "";

            if (dv.Dias > 0)
            {
                str += dv.Dias + " Dias ";
            }

            if (dv.Horas > 0)
            {
                str += dv.Horas + " Horas ";
            }

            if (dv.Minutos > 0)
            {
                str += dv.Minutos + " Minutos ";
            }

            if (dv.Segundos > 0)
            {
                str += dv.Segundos + " Segundos ";
            }

            return str;
        }

        private static DataValue checarSegundos( int value, DataValue dv )
        {
            if ( value > 60 ){
                dv.Segundos = value % 60;
                checarMinutos( value / 60, dv );
            } else {
                dv.Segundos = value;
            }
            return dv;
        }

        private static DataValue checarMinutos(int value, DataValue dv)
        {
            if ( value > 60 ){
                dv.Minutos = value % 60;
                checarHoras(value / 60, dv);
            } else {
                dv.Minutos = value;
            }
            return dv;
        }

        private static DataValue checarHoras(int value, DataValue dv)
        {
            if ( value > 24 ){
                dv.Horas = value % 24;
                dv.Dias = value / 24;                
            } else {
                dv.Horas = value;
            }
            return dv;
        }

    }

    class DataValue
    {
        public int Segundos = 0;
        public int Minutos = 0;
        public int Horas = 0;
        public int Dias = 0;
    }
}