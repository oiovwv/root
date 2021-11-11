using System;
using System.Globalization;
using System.IO;

namespace Biz
{
    public class Cfg
    {
        public static string LogRootPath
        {
            get { return "C:\\TOLL\\EDI\\ServiceLogs\\"; }
        }

        public static string LogPath
        {
            get { return CreateYearPath(LogRootPath, DateTime.Now.Year.ToString()); }
        }

        public static string ConnStr
        {
            //get { return "Provider=MSDAORA.1;Password=jdawms;User ID=jdawms;Data Source=newjdadbcnd011.c4wombbgpman.rds.cn-north-1.amazonaws.com.cn:1521/oracle"; }

            get { return "Provider=MSDAORA.1;Password=STA_ILIS;User ID=STA_ILIS;Data Source=PROD"; }
        }

        public static string CreateYearPath(string RootPath, string year)
        {
            string Year = RootPath + year + @"\";

            if (!Directory.Exists(Year))
            {
                Directory.CreateDirectory(Year);
            }

            return Year;
        }

        public static void WriteLog(string logPath, string logText)
        {
            CText log = new CText();
            if (Directory.Exists(logPath) == false)
            {
                Directory.CreateDirectory(logPath);
            }

            log.Open(logPath + DateTime.Now.ToString("yyyyMMdd") + ".TXT");

            log.WriteLog(logText);

            log.Close();
        }

        public static void WriteDBLog(string logPath, string logText)
        {
            CText log = new CText();
            if (Directory.Exists(logPath) == false)
            {
                Directory.CreateDirectory(logPath);
            }

            log.Open(logPath + DateTime.Now.ToString("yyyyMMdd") + "_DBLogs" + ".TXT");

            log.WriteLog(logText);

            log.Close();
        }


        public static int WeekOfYear(DateTime dt, CultureInfo ci)
        {
            return ci.Calendar.GetWeekOfYear(dt, ci.DateTimeFormat.CalendarWeekRule, ci.DateTimeFormat.FirstDayOfWeek);
        }

        public static int NumberOfWeek(string weekDay)
        {
            switch (weekDay)
            {
                case "Monday":
                    return 1;
                case "Tuesday":
                    return 2;
                case "Wednesday":
                    return 3;
                case "Thursday":
                    return 4;
                case "Friday":
                    return 5;
                case "Saturday":
                    return 6;
                case "Sunday":
                    return 7;
                default:
                    throw new Exception("星期代码错误");
            }
        }

        public static int DayCountOfYear(DateTime dt)
        {
            DateTime dtMax = DateTime.Parse(dt.Year.ToString() + "-12-31");
            return (dtMax.DayOfYear - dt.DayOfYear);
        }

        public static string GetDateWeekFlag(DateTime dt)
        {
            string dayWeek = string.Empty;

            int yearSort = WeekOfYear(dt, new CultureInfo("zh-CN"));
            int weekSort = NumberOfWeek(dt.DayOfWeek.ToString());
            if ((7 - weekSort) > DayCountOfYear(dt))
            {
                dayWeek = (dt.Year + 1).ToString() + "01";
            }
            else
            {
                dayWeek = dt.Year.ToString() + yearSort.ToString("00");
            }

            return dayWeek;
        }
    }
}
