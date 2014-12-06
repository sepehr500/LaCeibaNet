using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LaCeibaNetv4.Models.Classes
{
    public class AGLLU
    {
        public int LoanAmt(int RoundNum)
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
    }
}