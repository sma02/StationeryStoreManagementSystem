﻿using StationeryStoreManagementSystem.DL;
using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StationeryStoreManagementSystem.BL
{
    class Supplier
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Contact { get; set; }
        public string? Email { get; set; }
        public string? StreetAddress { get; set; }
        public string? Town { get; set; }
        public string? City { get; set; }
        public string? Country { get; set; }
        public string? PostalCode { get; set; }
        private List<object> initialArgs;
        public Supplier(string? name = null
                       , string? contact = null
                       , string? email = null
                       , string? streetAddress = null
                       , string? town = null
                       , string? city = null
                       , string? country = null
                       , string? postalCode = null)

        {
            Name = name;
            Contact = contact;
            Email = email;
            StreetAddress = streetAddress;
            Town = town;
            City = city;
            Country = country;
            PostalCode = postalCode;

        }
        public Supplier(List<object> args)
        {
            Id = (int)args[0];
            Name = (string?)args[1];
            Contact = (string?)args[2];
            Email = (string?)args[3];
            StreetAddress = (string?)args[4];
            Town = (string?)args[5];
            City = (string?)args[6];
            Country = (string?)args[7];
            PostalCode = (string?)args[8];
            initialArgs = args;
            initialArgs.RemoveAt(0);
        }
        public void Save(bool isAdd = false)
        {
            List<(string, object)> args = new List<(string, object)>
            {
                (nameof(Name), Name.ToString()),
                (nameof(Contact), Contact),
                (nameof(Email),Email),
                (nameof(StreetAddress),StreetAddress),
                (nameof(Town),Town),
                (nameof(City),City),
                (nameof(Country),Country),
                (nameof(PostalCode),PostalCode)
            };
            if (isAdd == true)
            {
                DateTime now = DateTime.Now;
                args.Add(("IsDeleted", 0));
                args.Add(("AddedOn", now.ToString()));
                DataHandler.AddData(args, GetType().Name);
            }
            else
                DataHandler.UpdateData(args, initialArgs, GetType().Name, (nameof(Id), Id));
        }
    }
}