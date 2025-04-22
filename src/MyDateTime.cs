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

        protected DateTime _date_time;

        public DateTime DateTime => _date_time;

        public MyDateTime( double timestamp )
        {
            _date_time = GetDefault();
            _date_time = _date_time.AddSeconds(timestamp);
        }

        public MyDateTime(int year, int month, int day, int hour, int minute, int second)
        {
            _date_time = new DateTime(year, month, day, hour, minute, second);
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
            DateTime d;
            DateTime.TryParseExact(data,
                @"yyyy-MM-dd\THH:mm:ss.fff\Z", CultureInfo.InvariantCulture,
                DateTimeStyles.AssumeUniversal, out d);
            return new MyDateTime(d);
        }

        public static MyDateTime FromDateTime(string data, string format = @"yyyy-MM-dd HH:mm:ss")
        {
            DateTime.TryParseExact(data, format, CultureInfo.InvariantCulture, DateTimeStyles.AssumeUniversal, out var d);
            return new MyDateTime(d);
        }

        public int HMin()
        {
            return Convert.ToInt32(_date_time.Hour.ToString().PadLeft(2, '0') + "" + _date_time.Minute.ToString().PadLeft(2, '0'));
        }

        public MyDateTime ByStrToTime(string str)
        {
            //Console.WriteLine(str);
            var r = new RelativeDateParser(_date_time);
            var d = r.Parse(str);
            return new MyDateTime(d);
        }

        public double GetTimestamp()
        {
            var origin = GetDefault();
            var diff = _date_time - origin;
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
            var diff = _date_time - date;
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

        public string GetHoraMinuto()
        {
            return String.Format("{0:HH:mm}", _date_time);
        }

        static public MyDateTime Now
        {
            get { return new MyDateTime( DateTime.Now ); }
        }
        public override string ToString()
        {
            return GetDiaMesAno();
        }

    }
}