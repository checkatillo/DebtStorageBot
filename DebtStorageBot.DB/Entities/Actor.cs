using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DebtStorageBot.DB.Entities
{
    public class Actor
    {
        [Key]
        public long ActorID { get; set; }

        [MaxLength(500)]
        public string CreationDescription { get; set; }

        public DateTime CreationDate { get; set; } = DateTime.Now;

        [InverseProperty(nameof(DebtLog.CreditorID))]
        public virtual ICollection<DebtLog> DebtsAsCreditor { get; set; }

        [InverseProperty(nameof(DebtLog.DebtorID))]
        public virtual ICollection<DebtLog> DebtsAsDebtor { get; set; }

        [InverseProperty(nameof(DebtSum.CreditorID))]
        public virtual ICollection<DebtSum> DebtSumAsCreditor { get; set; }

        [InverseProperty(nameof(DebtSum.DebtorID))]
        public virtual ICollection<DebtSum> DebtSumAsDebtor { get; set; }
    }
}
