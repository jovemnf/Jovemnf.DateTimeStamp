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
            _date_time = GetDefault();
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
            _date_time = GetDefault();
        }

        protected DateTime GetDefault()
        {
            return new DateTime(1970, 1, 1, 0, 0, 0, 0);
        }

        public DateTime GetDateTime()
        {
            return _date_time;
        }

        public static MyDateTime FromIsoDateByNode(string data)
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

        public static MyDateTime FromDateTime(string data, string format = @"yyyy-MM-dd HH:mm:ss")
        {
            try
            {
                DateTime d;
                DateTime.TryParseExact(data, format, CultureInfo.InvariantCulture, DateTimeStyles.AssumeUniversal, out d);
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

        public MyDateTime ByStrToTime(string str)
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

        public double GetTimestamp()
        {
            DateTime origin = GetDefault();
            TimeSpan diff = _date_time - origin;
            return Math.Floor(diff.TotalSeconds);
        }

        public double GetDiference(DateTime date)
        {
            TimeSpan diff = _date_time - date;
            return Math.Floor(diff.TotalSeconds);
        }

        public double GetDiference(MyDateTime date)
        {
            return GetDiference(date._date_time);
        }

        public TimeSpan GetDiff(DateTime date)
        {
            TimeSpan diff = _date_time - date;
            return diff;
        }

        public double GetDiference(double timestamp)
        {
            return Math.Floor( GetTimestamp() - timestamp );
        }

        public bool IsValid () {
            try {
                DateTime.Parse(this.GetDiaMesAnoSomente());
                return true;
            } catch (Exception)
            {
                return false;
            }
        }

        public string GetDiaMesAno()
        {
            return String.Format("{0:dd/MM/yyyy HH:mm:ss}", _date_time);
        }

        public string GetAnoMesDia()
        {
            return String.Format("{0:yyyy/MM/dd HH:mm:ss}", _date_time);
        }

        public string GetAnoMesDiaSomente()
        {
            return String.Format("{0:yyyy/MM/dd}", _date_time);
        }

        public string GetDiaMesAnoSomente()
        {
            return String.Format("{0:dd/MM/yyyy}", _date_time);
        }

        public string GetHoraMinutoSegundo()
        {
            return String.Format("{0:HH:mm:ss}", _date_time);
        }

        static public MyDateTime Now
        {
            get { return new MyDateTime( DateTime.Now ); }
        }

        public int GetDay()
        {
            return this._date_time.Day;
        }

        public int GetMonth()
        {
            return this._date_time.Month;
        }

        public int GetYear()
        {
            return this._date_time.Year;
        }

        public int GetHour()
        {
            return this._date_time.Hour;
        }

        public int GetMinute()
        {
            return this._date_time.Minute;
        }

        public int GetSecond()
        {
            return this._date_time.Second;
        }

        public override string ToString()
        {
            return GetDiaMesAno();
        }

    }
}