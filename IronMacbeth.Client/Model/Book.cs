using IronMacbeth.Client.Model;
using IronMacbeth.Client.VVM;
using System;

namespace IronMacbeth.Client
{
    public class Book : Document, IDisplayable 
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Author { get; set; }

        public string PublishingHouse { get; set; }

        public string City { get; set; }

        public int Year { get; set; }
        public string Topic { get; set; }

        public int Pages { get; set; }

        public int Availiability { get; set; }

        public string Location { get; set; }

        public string TypeOfDocument { get; set; }

        public string RentPrice { get; set; }


        public string NameOfBook => Name;

        public Guid? ImageFileId { get; set; }

        public Image Image { get; set; }

    
    }
}
