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

            var list = db.ClientsTbls.Where(x => x.Active == true);
            return list.Count();
        
        }
        public static decimal averageLoanSize(this LaCeibaDbv4Entities db)
        {

            var list = db.LoansTbls.Where(x => x.Active == true);
            //decimal sum = 0;
            //foreach (var item in list)
            //{
            //    sum += item.AmtLoan;
            //}

            return list.Average(x => x.AmtLoan);
        
        }
        public static decimal grossLoanPortfolio(this LaCeibaDbv4Entities db)
        {

            var list = db.LoansTbls.Where(x => x.Active == true && x != null);
            var PaymentList = db.PaymentTbls.Where(x => x.LoansTbl.Active == true && x != null);
            decimal sum = 0;
            foreach (var item in list)
            {
                sum += item.AmtLoan;
            }
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

    }
}