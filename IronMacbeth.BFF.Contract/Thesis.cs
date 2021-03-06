using System;

namespace IronMacbeth.BFF.Contract
{
    public class Thesis
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Author { get; set; }

        public string Responsible { get; set; }

        public string City { get; set; }

        public string Topic { get; set; }

        public int Year { get; set; }

        public int Pages { get; set; }

        public string TypeOfDocument { get; set; }

        public Guid? ElectronicVersionFileId { get; set; }

    }
}
