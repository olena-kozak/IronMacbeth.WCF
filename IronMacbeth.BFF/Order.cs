using IronMacbeth.BFF.Contract;
using System;

namespace IronMacbeth.BFF
{
    public class Order
    {
        public int Id { get; set; }

        public string UserName { get; set; }

        public string UserSurname { get; set; }

        public int PhoneNumber { get; set; }


        public string UserLogin { get; set; }

        public int? BookId { get; set; }

        public Book Book { get; set; }

        public int? ArticleId { get; set; }

        public Article Article { get; set; }

        public int? PeriodicalId { get; set; }

        public Periodical Periodical { get; set; }

        public int? NewspaperId { get; set; }

        public Newspaper Newspaper { get; set; }

        public int? ThesesId { get; set; }

        public Thesis Theses { get; set; }

        public string StatusOfOrder { get; set; }


        public DateTime DateOfOrder { get; set; }

        public DateTime DateOfReturn { get; set; }


        public DateTime ReceiveDate { get; set; }


        public string TypeOfOrder { get; set; }
    }
}
