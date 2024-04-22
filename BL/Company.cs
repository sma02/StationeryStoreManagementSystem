using StationeryStoreManagementSystem.DL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StationeryStoreManagementSystem.BL
{
    internal class Company
    {
        public int Id { get; set; }
        public string Name { get; set; }

        private List<object> initialArgs;
        public Company(string name = null)
        {
            Name = name;
        }
        public Company(List<object> args)
        {
            Id = (int)args[0];
            Name = (string)args[1];
            initialArgs = args;
            initialArgs.RemoveAt(0);
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
                DataHandler.UpdateData(args, initialArgs, GetType().Name, (nameof(Id), Id));
        }
    }
}
