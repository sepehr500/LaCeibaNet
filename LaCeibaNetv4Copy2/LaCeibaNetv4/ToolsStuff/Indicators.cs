using LaCeibaNetv4.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LaCeibaNetv4.ToolsStuff
{
    public static class Indicators
    {
        
        public static int activeClients(this LaCeibaDbv4Entities db) {

            var list = db.ClientsTbls.Where(x => x.Active == true).ToList();
            return list.Count();
        
        }
        public static decimal averageLoanSize(this LaCeibaDbv4Entities db)
        {

            var list = db.LoansTbls.Where(x => x.Active == true).ToList();
            //decimal sum = 0;
            //foreach (var item in list)
            //{
            //    sum += item.AmtLoan;
            //}

            return list.Average(x => x.AmtLoan);
        
        }
        //Total HNL's outstanding
        public static decimal grossLoanPortfolio(this LaCeibaDbv4Entities db)
        {

            var list = db.LoansTbls.Where(x => x.Active == true && x != null);
            var PaymentList = db.PaymentTbls.Where(x => x.LoansTbl.Active == true && x != null).ToList();
            decimal sum = 0;
            //Total amount given out
            foreach (var item in list)
            {
                sum += item.AmtLoan;
            }
            //Subtract total paid back
            foreach (var item in PaymentList)
            {
                sum -= (decimal)item.AmtPaid;
            }

            return sum; //- (decimal)PaymentList;
        
        }

        public static decimal repaymentRate(this LaCeibaDbv4Entities db)
        {

            var query = from c in db.PaymentTbls
                        where c.LoansTbl.Active == true
                        select c;
            var LoansPaid = query.Count();

            var query2 = from c in db.LoansTbls
                        where c.Active == true
                        select c;
            var TotalInstalments = query2.Sum(x => x.Instalments);

            return (decimal)LoansPaid / (decimal)TotalInstalments * 100;
                    

        }

        public static decimal interestClac(this LoansTbl loan)
        {
            int Inst = loan.Instalments ?? 0;
            var Princ = loan.AmtLoan;
            decimal pp = 0;
            var Freq = loan.RepFreqId;
            if (loan.RoundTbl.ProgramClientTbl.ProgramTbl.Program.Equals("PLP"))
            {
                switch (Freq)
                {
                    case 1:
                      pp = .0125m * Princ / (decimal)(1 - (Math.Pow(1 + 0.0125, Inst * -1)));

                        
                        break;
                    case 2:
                       pp = .003125m * Princ / (decimal)(1 - (Math.Pow(1 + 0.003125, Inst * -1)));
                        
                        break;
                    case 3:
                        pp = .00625m * Princ / (decimal)(1 - (Math.Pow(1 + .00625, Inst * -1)));
                        
                        break;
                    default: 
                        return 0;
                        
                }
               ;
            }
            else
            {
                switch (Freq)
                {
                    case 1:
                     pp = .025m * Princ / (decimal)(1 - (Math.Pow(1 + 0.025, Inst * -1)));

                        
                        break;
                    case 2:
                       pp = .00625m * Princ / (decimal)(1 - (Math.Pow(1 + 0.00625, Inst * -1)));
                        
                        break;
                    case 3:
                      pp = .0125m * Princ / (decimal)(1 - (Math.Pow(1 + .0125, Inst * -1)));
                        
                        break;
                    default:
                        return 0;
                }
                
            }
            return Math.Round(pp, 2) * (int)loan.Instalments - loan.AmtLoan;




        }

        //porfolio at risk
        public static decimal PAR ( this LaCeibaDbv4Entities db , int days){
            decimal AtRisk = 0;
            int NumAtRisk = 0;

            var ActiveLoans = db.LoansTbls.Where(x => x.Active == true).ToList();
            foreach (var item in ActiveLoans)
            {
                DateTime DueDate = getDueDate(item);

                if (0 > DueDate.AddDays(days).CompareTo(DateTime.Now))
                {

                    NumAtRisk++;
                } 
                

            }
            decimal totalClients = ActiveLoans.Count();
            return NumAtRisk / totalClients * 100;
            }

        private static DateTime getDueDate(this LoansTbl item)
        {
            DateTime DueDate = DateTime.Now;
            DateTime temp = DateTime.Now;
            temp = item.TransferDate;
            int DaysToAdd = item.RepFreqId == 2 ? 7 : 14;
            List<DateTime> DatesDue = new List<DateTime>();
            for (int i = 0; i < item.Instalments; i++)
            {

                if (item.RepFreqId == 1)
                {
                    temp = temp.AddMonths(1);
                    DatesDue.Add(temp);
                }
                else
                {
                    temp = temp.AddDays(DaysToAdd);
                    DatesDue.Add(temp);
                }


            }
            var ElementAt = item.PaymentTbls == null || item.PaymentTbls.Count() == 0 ? 0 : item.PaymentTbls.Count(); 
            DueDate = DatesDue.ElementAt(ElementAt);
            return DueDate;
        }
        public static decimal VAR(this LaCeibaDbv4Entities db)
        {
            decimal AtRisk = 0;
            
            decimal TotalPaid = 0;
            var ActiveLoans = db.LoansTbls.Where(x => x.Active == true).ToList();
            foreach (var item in ActiveLoans)
            {
                DateTime DueDate = getDueDate(item);

                if (0 > DueDate.AddDays(30).CompareTo(DateTime.Now))
                {
                    TotalPaid = item.PaymentTbls == null ? 0 : (decimal)item.PaymentTbls.Sum(x => x.AmtPaid);
                    AtRisk += item.AmtLoan - TotalPaid;
                    
                }


            }
            return Decimal.Round(AtRisk, 2);
        }

        public static decimal InterestReceivable(this LaCeibaDbv4Entities db)
        {
            decimal Total = 0;
            var ActiveLoans = db.LoansTbls.Where(x => x.Active == true).ToList();
            foreach (var item in ActiveLoans)
            {
                Total += item.interestClac();
            }
            return Decimal.Round(Total , 2);


        }
        public static decimal InterestRevenue(this LaCeibaDbv4Entities db)
        {
            decimal TotalWithInterest = db.PaymentTbls.ToList().Sum(x => x.AmtPaid ?? 0);
            decimal TotalLoanedOut = db.LoansTbls.ToList().Sum(x => x.AmtLoan);
            return Decimal.Round(TotalWithInterest - TotalLoanedOut , 2);

        }
    }
}