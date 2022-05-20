using System;

namespace IronMacbeth.BFF.Contract
{
    public class Book
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

        public Guid? ElectronicVersionFileId { get; set; }


        public Guid? ImageFileId { get; set; }
    }
}
