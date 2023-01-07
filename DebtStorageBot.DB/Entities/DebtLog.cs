using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DebtStorageBot.DB.Entities
{
    public class DebtLog
    {
        [Key]
        public long DebtLogID { get; set; }

        public decimal Amount { get; set; }

        [ForeignKey(nameof(Creditor))]
        public long CreditorID { get; set; }

        [ForeignKey(nameof(Debtor))]
        public long DebtorID { get; set; }

        [MaxLength(500)]
        public string CreationDescription { get; set; }

        public DateTime CreationDate { get; set; } = DateTime.Now;

        public virtual Actor Creditor { get; set; }

        public virtual Actor Debtor { get; set; }
    }
}
