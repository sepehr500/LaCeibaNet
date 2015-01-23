using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Collections;

namespace LaCeibaNetv4.Models.Classes
{
    public class PPlan
    {
        public decimal AmtDue { get; set; }

        public decimal Principal { get; set; }
        public decimal Interest { get; set; }


        public System.DateTime DateDue { get; set; }
        //0 = pending, 1 = paid, 2 = late, 3 = arrears 
        public int status { get; set; }
    }
    public class PPlanHold 
    {
        public ArrayList Plan { get; set; }

        public decimal TotalInterest { get; set; }
        public decimal TotalOwed { get; set; }
        public int Instalments { get; set; }

        public string Frequency { get; set; }

        public string Term { get; set; }

        public decimal amtEachInstalment { get; set; }




    public void CreatePlan(int Inst, int? Freq, decimal Princ, string Prog, DateTime time)
    {
        int days = 0;
        decimal pp = 0;
        bool month = false;
        if (Prog.Equals("PLP")) {
            switch (Freq)
            {
                case 1:
                    pp = .0125m * Princ / (decimal)(1-(Math.Pow(1+0.0125, (double)Inst * -1)));
                    
                    month = true;
                    break;
                case 2:
                    pp = .003125m * Princ / (decimal)(1 - (Math.Pow(1 + 0.003125, Inst * -1)));
                    days = 7;
                    break;
                case 3:
                    pp = .00625m * Princ / (decimal)(1 - (Math.Pow(1 + .00625, Inst * -1)));
                    days = 14;
                    break;
            }
        }
        else
        {
            switch (Freq)
            {
                case 1:
                    pp = .025m * Princ / (decimal)(1 - (Math.Pow(1 + 0.025, Inst * -1)));

                    month = true;
                    break;
                case 2:
                    pp = .00625m * Princ / (decimal)(1 - (Math.Pow(1 + 0.00625, Inst * -1)));
                    days = 7;
                    break;
                case 3:
                    pp = .0125m * Princ / (decimal)(1 - (Math.Pow(1 + .0125, Inst * -1)));
                    days = 14;
                    break;
            }

        }
  
       decimal rounded = Math.Round(pp, 2);
       this.amtEachInstalment = pp;
        this.Plan = new ArrayList();
        DateTime temp = time;
        //Principal before interest is added per instalment
        decimal InstPrinc = Princ / Inst;
        decimal InstInterest = rounded - InstPrinc;
        for (int i = 0; i < Inst; i++)
        {
            PPlan x = new PPlan();
            x.Principal = InstPrinc;
            x.Interest = InstInterest;
            x.AmtDue = rounded;
            if (month)
            {
                x.DateDue = temp.AddMonths(1);
            }
            else
            {
            x.DateDue = temp.AddDays(days);
            }
            temp = x.DateDue;
            Plan.Add(x);
            
        }

       
        this.Instalments = Inst;
        switch (Freq)
        {
            case 1:
                this.Frequency = "Monthly";
                this.Term = Inst.ToString() + " " + "Months";
                break;
            case 2:

                this.Frequency = "Weekly";
                this.Term = Inst.ToString() + " " + "Weeks";
                break;
            case 3:

                this.Frequency = "BiWeekly";
                var adjInst = Inst * 2;
                this.Term = adjInst.ToString() + " " + "Weeks";
                break;
        }
        foreach (PPlan item in Plan)
        {
            this.TotalOwed += item.AmtDue;

        }
        this.TotalInterest = this.TotalOwed - Princ;
        
        

    }



    public void CreatePlan(int Inst, int Freq, decimal Princ, string Prog, DateTime time, decimal PaymentTotal) {
        int days = 0;
        decimal pp = 0;
        bool month = false;
        if (Prog.Equals("PLP"))
        {
            switch (Freq)
            {
                case 1:
                    pp = .0125m * Princ / (decimal)(1 - (Math.Pow(1 + 0.0125, Inst * -1)));

                    month = true;
                    break;
                case 2:
                    pp = .003125m * Princ / (decimal)(1 - (Math.Pow(1 + 0.003125, Inst * -1)));
                    days = 7;
                    break;
                case 3:
                    pp = .00625m * Princ / (decimal)(1 - (Math.Pow(1 + .00625, Inst * -1)));
                    days = 14;
                    break;
            }
        }
        else
        {
            switch (Freq)
            {
                case 1:
                    pp = .025m * Princ / (decimal)(1 - (Math.Pow(1 + 0.025, Inst * -1)));

                    month = true;
                    break;
                case 2:
                    pp = .00625m * Princ / (decimal)(1 - (Math.Pow(1 + 0.00625, Inst * -1)));
                    days = 7;
                    break;
                case 3:
                    pp = .0125m * Princ / (decimal)(1 - (Math.Pow(1 + .0125, Inst * -1)));
                    days = 14;
                    break;
            }

        }
        decimal rounded = Math.Round(pp, 2);
       
        this.Plan = new ArrayList();
        DateTime temp = time;
        for (int i = 0; i < Inst; i++)
        {
            PaymentTotal = Math.Round(PaymentTotal, 2);   
            PPlan x = new PPlan();
            x.AmtDue = rounded;
            if (PaymentTotal >= x.AmtDue)
            {
                PaymentTotal -= (decimal)x.AmtDue;
                x.AmtDue = 0;
            }
            else
            {
                x.AmtDue -= PaymentTotal;
                PaymentTotal = 0;
            }
            if (month)
            {
                x.DateDue = temp.AddMonths(1);
            }
            else
            {
                x.DateDue = temp.AddDays(days);
            }
            temp = x.DateDue;
            if (x.AmtDue != 0)
	{
		 
	
            if (0 > x.DateDue.AddMonths(3).CompareTo(DateTime.Now))
            {
                x.status = 3;
            }
            else if (0 > x.DateDue.CompareTo(DateTime.Now))
            {
                x.status = 2;
            }
            else
            {
                x.status = 0;
            }

            }
            else
            {
                x.status = 1;
            }
            Plan.Add(x);
            
            
        }
        foreach (PPlan item in Plan)
        {
            this.TotalOwed += item.AmtDue;
            
        }
    
    
    }
    
    }
}