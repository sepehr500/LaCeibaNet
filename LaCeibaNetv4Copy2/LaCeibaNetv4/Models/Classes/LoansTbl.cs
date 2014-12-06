using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace LaCeibaNetv4.Models
{
    [MetadataType(typeof(UserMetaData2))]
    public partial class LoansTbl
    {
        public int LoanAmt()
        {

            if (RoundTbl.ProgramClientTbl.ProgramId == 1)
            {


                switch (RoundTbl.RoundNum)
                {
                    case 1:
                        return 500;

                    case 2:
                        return 750;
                    case 3:
                        return 1000;
                    case 4:
                        return 1250;
                    case 5:
                        return 1500;
                    case 6:
                        return 1750;
                    case 7:
                        return 2000;
                    default:
                        return 0;
                }
            }
            else
            {
                switch (RoundTbl.RoundNum)
                {
                    case 1:
                        return 2500;

                    case 2:
                        return 3000;
                    case 3:
                        return 3500;
                    case 4:
                        return 4000;
                    case 5:
                        return 4500;
                    case 6:
                        return 5000;
                    default:
                        return 0;
                }
            }
        }

    }
    public class UserMetaData2
    {
        [Display(Name="Loan Amount Is")]
        public decimal AmtLoan { get; set; }
        [DataType(DataType.Date)]
        public System.DateTime TransferDate { get; set; }
        public bool Active { get; set; }
        [Required]
        [Display(Name="Frequency")]
        public Nullable<int> RepFreqId { get; set; }
        [Required]
        public Nullable<int> Instalments { get; set; }
        public virtual RoundTbl RoundTbl { get; set; }
        

    }
}