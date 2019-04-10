using System;
using System.Globalization;

namespace Jovemnf.DateTimeStamp
{

    public enum MyWeek
    {
        SEGUNDA, TERCA, QUARTA, QUINTA, SEXTA, SABADO, DOMINGO
    }

    public class MyDateTime: IComparable
    {

        public DateTime _date_time;

        public MyDateTime( double timestamp )
        {
            _date_time = getDefault();
            _date_time = _date_time.AddSeconds(timestamp);
        }
        
        public int CompareTo(object obj)
        {
            if (obj == null) return 1;

            MyDateTime otherTemperature = obj as MyDateTime;
            if (otherTemperature != null)
                return this._date_time.CompareTo(otherTemperature._date_time);
            else
                throw new ArgumentException("Object is not a Temperature");
        }

        public MyWeek DiaDaSemana
        {
            get {
                switch (_date_time.DayOfWeek)
                {
                    case DayOfWeek.Monday:
                        return MyWeek.SEGUNDA;
                    case DayOfWeek.Tuesday:
                        return MyWeek.TERCA;
                    case DayOfWeek.Wednesday:
                        return MyWeek.QUARTA;
                    case DayOfWeek.Thursday:
                        return MyWeek.QUINTA;
                    case DayOfWeek.Friday:
                        return MyWeek.SEXTA;
                    case DayOfWeek.Saturday:
                        return MyWeek.SABADO;
                    default:
                        return MyWeek.DOMINGO;
                }
            }
        }

        public MyDateTime(DateTime date)
        {
            _date_time = date;
        }

        public MyDateTime()
        {
            _date_time = getDefault();
        }

        protected DateTime getDefault()
        {
            return new DateTime(1970, 1, 1, 0, 0, 0, 0);
        }

        public DateTime getDateTime()
        {
            return _date_time;
        }

        public static MyDateTime fromIsoDateByNode(string data)
        {
            try
            {
                DateTime d;
                DateTime.TryParseExact(data,
                    @"yyyy-MM-dd\THH:mm:ss.fff\Z", CultureInfo.InvariantCulture,
                    DateTimeStyles.AssumeUniversal, out d);
                return new MyDateTime(d);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public int HMin()
        {
            return Convert.ToInt32(_date_time.Hour.ToString().PadLeft(2, '0') + "" + _date_time.Minute.ToString().PadLeft(2, '0'));
        }

        public MyDateTime byStrToTime(string str)
        {
            try
            {
                //Console.WriteLine(str);
                RelativeDateParser r = new RelativeDateParser(_date_time);
                DateTime d = r.Parse(str);
                return new MyDateTime(d);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.StackTrace);
                throw e;
            }
        }

        public double getTimestamp()
        {
            DateTime origin = getDefault();
            TimeSpan diff = _date_time - origin;
            return Math.Floor(diff.TotalSeconds);
        }

        public double getDiference(DateTime date)
        {
            TimeSpan diff = _date_time - date;
            return Math.Floor(diff.TotalSeconds);
        }

        public double getDiference(MyDateTime date)
        {
            return getDiference(date._date_time);
        }

        public TimeSpan GetDiff(DateTime date)
        {
            TimeSpan diff = _date_time - date;
            return diff;
        }

        public double getDiference(double timestamp)
        {
            return Math.Floor( getTimestamp() - timestamp );
        }

        public bool isValid () {
            try {
                DateTime.Parse(this.getDiaMesAnoSomente());
                return true;
            } catch (Exception)
            {
                return false;
            }
        }

        public string getDiaMesAno()
        {
            return String.Format("{0:dd/MM/yyyy HH:mm:ss}", _date_time);
        }

        public string getAnoMesDia()
        {
            return String.Format("{0:yyyy/MM/dd HH:mm:ss}", _date_time);
        }

        public string getAnoMesDiaSomente()
        {
            return String.Format("{0:yyyy/MM/dd}", _date_time);
        }

        public string getDiaMesAnoSomente()
        {
            return String.Format("{0:dd/MM/yyyy}", _date_time);
        }

        public string getHoraMinutoSegundo()
        {
            return String.Format("{0:HH:mm:ss}", _date_time);
        }

        static public MyDateTime Now
        {
            get { return new MyDateTime( DateTime.Now ); }
        }

        public int getDay()
        {
            return this._date_time.Day;
        }

        public int getMonth()
        {
            return this._date_time.Month;
        }

        public int getYear()
        {
            return this._date_time.Year;
        }

        public int getHour()
        {
            return this._date_time.Hour;
        }

        public int getMinute()
        {
            return this._date_time.Minute;
        }

        public int getSecond()
        {
            return this._date_time.Second;
        }

        public override string ToString()
        {
            return getDiaMesAno();
        }

    }
}