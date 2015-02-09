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
            decimal PaymentTotal = (decimal)loan.PaymentTbls.Sum(x => x.AmtPaid);
            double IR = loan.RoundTbl.ProgramClientTbl.ProgramTbl.InterestRate / 100;
            double PDLength = 0;
            decimal pp = 0m;
            switch (loan.RepFreqId)
            {
                case 1:
                    PDLength = 1 / 12d;
                    break;
                case 2:
                    PDLength = 1 / 52d;
                    break;
                case 3:
                    PDLength = 1 / 26d;

                    break;
            }
            //Interest Rate per Period
            decimal IRPP = (decimal)IR * (decimal)PDLength;
            pp = IRPP * loan.AmtLoan / (decimal)(1 - (Math.Pow(1 + (double)IRPP, (double)loan.Instalments * -1)));
            return decimal.Round(pp, 2) * (int)loan.Instalments - loan.AmtLoan;




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
            return decimal.Round(Total , 2);


        }
        public static decimal InterestRevenue(this LaCeibaDbv4Entities db)
        {
            decimal TotalWithInterest = db.PaymentTbls.ToList().Sum(x => x.AmtPaid ?? 0);
            decimal TotalLoanedOut = db.LoansTbls.ToList().Sum(x => x.AmtLoan);
            return decimal.Round(TotalWithInterest - TotalLoanedOut , 2);

        }

        public static object[] StatusPerLoanLevel(this LaCeibaDbv4Entities db, bool? status)
        {

            var MainListPLP = db.LoansTbls.Where(x => x.RoundTbl.ProgramClientTbl.ClientsTbl.Active == status && x.Active == true && x.RoundTbl.ProgramClientTbl.ProgramTbl.Program == "PLP").GroupBy(x => x.RoundTbl.RoundNum).ToList();
            var MainListEAL = db.LoansTbls.Where(x => x.RoundTbl.ProgramClientTbl.ClientsTbl.Active == status && x.Active == true && x.RoundTbl.ProgramClientTbl.ProgramTbl.Program == "EAL").GroupBy(x => x.RoundTbl.RoundNum).ToList();
            
            var ReturnListPLP = new List<Tuple<int, int>>();
            var ReturnListEAL = new List<Tuple<int, int>>();
            var ReturnListFull = new List<int>();
            var FinalListFull = new List<int>(); 
            var IntListFull = new List<int>(); 
            

            foreach (var item in MainListPLP)
            {
                foreach (var person in item)
                {
                    ReturnListFull.Add(getLoanAmt(person.RoundTbl.RoundNum, 1));
                }
                
            }

            foreach (var item in MainListEAL)
            {
                foreach (var person in item)
                {
                    ReturnListFull.Add(getLoanAmt(person.RoundTbl.RoundNum, 0));
                }

            }
            //create ladder list
            for (int i = 1; i < 7; i++)
            {
                FinalListFull.Add(getLoanAmt(i, 1));
            }
            for (int i = 1; i < 7; i++)
            {
                FinalListFull.Add(getLoanAmt(i, 0));
            }
            foreach (var item in FinalListFull)
            {
                IntListFull.Add(ReturnListFull.Where(x => x.Equals(item)).Count());
            }
            //ReturnListPLP.Concat(ReturnListEAL);
            return IntListFull.Cast<object>().ToArray();

         }

        

        //if plp = 1 it will do plp amounts
        private static int getLoanAmt(int level , int plp)
        {
            if (plp == 1)
            {


                switch (level)
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
                switch (level)
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
}