using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IronMacbeth.BFF.Contract
{
    public class CreateOrder
    {
        public int Id { get; set; }

        public string UserName { get; set; }

        public string UserSurname { get; set; }

        public int PhoneNumber { get; set; }

        public string UserLogin { get; set; }

        public int? BookId { get; set; }

        public int? ArticleId { get; set; }

        public int? PeriodicalId { get; set; }

        public int? NewspaperId { get; set; }

        public int? ThesesId { get; set; }

        public string StatusOfOrder { get; set; }

        public DateTime DateOfOrer { get; set; }

        public DateTime DateOfReturn { get; set; }

        public DateTime ReceiveDate { get; set; }

        public string TypeOfOrder { get; set; }
    }
}
