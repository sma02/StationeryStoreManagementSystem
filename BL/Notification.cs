using StationeryStoreManagementSystem.DL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StationeryStoreManagementSystem.BL
{
    class Notification
    {
        public int Id { get; set; }
        public string? Content { get; set; }
        public Employee? Sender { get; set; }
        public Employee? Receiver { get; set; }

        public List<object> InitialArgs;
        public Notification(string? Content = null, Employee? sender = null, Employee? receiver = null)
        {
            this.Content = Content;
            this.Sender = sender;
            this.Receiver = receiver;
        }
        public Notification(List<object> args)
        {
            Id = (int)args[0];
            Content = (string)args[1];
            Sender = (Employee)args[2];
            Receiver = (Employee)args[3];
            InitialArgs = args;
            InitialArgs.RemoveAt(0);
        }
        public void Save(bool isAdd = false)
        {
            NotificationDL.SaveNotification(this, isAdd);
        }
    }
}
