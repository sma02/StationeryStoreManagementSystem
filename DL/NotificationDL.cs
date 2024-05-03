using Microsoft.Data.SqlClient;
using StationeryStoreManagementSystem.BL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StationeryStoreManagementSystem.DL
{
    static class NotificationDL
    {
        public static DataTable GetNotifications_View()
        {
            List<object> list = new List<object>();
            return DataHandler.FillDataTable(@"SELECT * FROM GetNotifications_View");
        }
        public static Notification GetNotification(int id)
        {
            SqlDataReader reader = Utils.ReadData(@"SELECT N.Id, N.UserId, AddedBy [From],  
                                                   Content AS [Notification] 
                                                   FROM Notification N 
                                                   WHERE Id=" + id.ToString());
            List<object> args = Utils.GetArgs(reader);
            if (args.Count != 0)
            {
                if (args[1] != null)
                    args[1] = EmployeeDL.GetEmployee((int)args[1]);
                if (args[2] != null)
                    args[2] = EmployeeDL.GetEmployee((int)args[2]);
                return new Notification(args);
            }
            else
                return null;
        }
        public static void SaveNotification(Notification N, bool isAdd = false)
        {
            List<(string, object)> args = new List<(string, object)>
            {
            };
            if (isAdd == true)
            {
            
            }
            else
            {

            }
        }
    }
}
