using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StationeryStoreManagementSystem.DL
{
    static class ShipmentDL
    {
        public static DataTable GetShipmentsView()
        {
            return DataHandler.FillDataTable(@"SELECT AddedOn,Id,Name FROM GetShipments_View");
        }
    }
}
