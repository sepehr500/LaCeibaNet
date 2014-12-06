using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace LaCeibaNetv4.Models
{
    [MetadataType(typeof(UserMetaData3))]
    public partial class RoundTbl
    {
        public int LoanAmt()
        {

            if (ProgramClientTbl.ProgramId == 1)
            {


                switch (RoundNum)
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
                switch (RoundNum)
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
    public class UserMetaData3
    {

        public int Id { get; set; }
        public int PClientNameId { get; set; }
        public int RoundNum { get; set; }

        public virtual ICollection<LoansTbl> LoansTbls { get; set; }
        public virtual ProgramClientTbl ProgramClientTbl { get; set; }
    }
}