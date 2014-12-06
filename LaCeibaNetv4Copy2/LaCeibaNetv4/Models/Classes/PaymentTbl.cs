using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace LaCeibaNetv4.Models
{
    [MetadataType(typeof(UserMetaData4))]
    public partial class PaymentTbl
    {

    }
    public class UserMetaData4
    {
        public int Id { get; set; }
        [Display(Name = "Date Payment Due")]
        [DataType(DataType.Date)]
        public System.DateTime DatePmtDue { get; set; }
        [Display(Name = "Date Paid")]
        [DataType(DataType.Date)]
        [Required]
        public Nullable<System.DateTime> DatePmtPaid { get; set; }
        [Display(Name="Amount Paid")]
        public Nullable<decimal> AmtPaid { get; set; }
        public int LoanId { get; set; }

        public virtual LoansTbl LoansTbl { get; set; }
        
    }
}