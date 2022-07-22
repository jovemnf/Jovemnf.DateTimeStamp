using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jovemnf.DateTimeStamp
{
    public class MyDate : MyDateTime
    {

        public override string ToString()
        {
            return GetDiaMesAnoSomente();
        }

        public MyDate( double timestamp )
        {
            _date_time = GetDefault();
            _date_time = _date_time.AddSeconds(timestamp);
        }

        public MyDate(DateTime date)
        {
            _date_time = date;
        }

        public MyDate()
        {
            _date_time = GetDefault();
        }

        public static new MyDate Now
        {
            get { return new MyDate(DateTime.Now); }
        }

        public bool IsLess(MyDate now)
        {
            try
            {
                int aux = Convert.ToInt32(now.GetYear() + "" + now.GetMonth().ToString().PadLeft(2, '0') + "" + now.GetDay().ToString().PadLeft(2, '0'));
                int thisis = Convert.ToInt32(this.GetYear() + "" + this.GetMonth().ToString().PadLeft(2, '0') + "" + this.GetDay().ToString().PadLeft(2, '0'));
                return (aux > thisis) ? true : false;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public bool IsLess(DateTime now)
        {
            return IsLess(new MyDate(now));
        }

        public bool IsLess(double now)
        {
            return IsLess(new MyDate(now));
        }

    }
}