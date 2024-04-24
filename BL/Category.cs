using StationeryStoreManagementSystem.DL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StationeryStoreManagementSystem.BL
{
    internal class Category
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public double? GST { get; set; }

        public List<object> InitialArgs;
        public Category(string? name = null, double? gst = 16)
        {
            Name = name;
            GST = gst;
        }
        public Category(List<object> args)
        {
            Id = (int)args[0];
            Name = (string)args[1];
            GST = (double?)args[2];
            InitialArgs = args;
            InitialArgs.RemoveAt(0);
        }
        public void Save(bool isAdd = false)
        {
            List<(string, object)> args = new List<(string, object)>
            {
                (nameof(Name), Name.ToString()),
            };
            if (isAdd == true)
            {
                DateTime now = DateTime.Now;
                args.Add(("IsDeleted", 0));
                args.Add(("AddedOn", now.ToString("yyyy-MM-dd HH:mm:ss")));
                DataHandler.AddData(args, GetType().Name);
            }
            else
                DataHandler.UpdateData(args, InitialArgs, GetType().Name, (nameof(Id), Id));
        }
    }
}
