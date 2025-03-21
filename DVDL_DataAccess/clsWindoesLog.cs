using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;

namespace DVDL_DataAccess
{
    public class clsErrorEventLog
    {
        private static string _SourceName = "DVLD";

        public  enum enEventLog { Informition =0, warning =1, Error =2};
        public enEventLog Log = enEventLog.Informition;

        //public  void WindoesLog(string soursName)
        //{
        //    if (!EventLog.SourceExists(SourseName))
        //    {
        //        EventLog.CreateEventSource(SourseName, "Application");
        //    }

        //    switch(Log)
        //    {
        //        case enEventLog.Informition:
        //            EventLog.WriteEntry(SourseName, "This My Informition :-)", EventLogEntryType.Information);
        //            break;
        //        case enEventLog.warning:
        //            EventLog.WriteEntry(SourseName, "This My Waning :-)", EventLogEntryType.Warning);
        //            break;
        //            case enEventLog.Error:
        //            EventLog.WriteEntry(SourseName, "This My Error :-)", EventLogEntryType.Error);
        //            break;
        //        default:
        //            break;
        //    }

        //}

        public static void LogExseptionsToLogerViewr(string Message, EventLogEntryType type)
        {
            if (!EventLog.SourceExists(_SourceName))
            {
                EventLog.CreateEventSource(_SourceName, "Application");
            }


            EventLog.WriteEntry(_SourceName, Message, type);
        }
    }
}

