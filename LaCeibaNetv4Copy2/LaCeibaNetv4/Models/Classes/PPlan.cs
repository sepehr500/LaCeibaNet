using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Collections;

namespace LaCeibaNetv4.Models.Classes
{
    public class PPlan
    {
        public double AmtDue { get; set; }
        public System.DateTime DateDue { get; set; }
        //0 = pending, 1 = paid, 2 = late, 3 = arrears 
        public int status { get; set; }
    }
    public class PPlanHold 
    {
        public ArrayList Plan { get; set; }
        public double TotalOwed { get; set; }



    public void CreatePlan(int Inst, int? Freq, float Princ, string Prog, DateTime time)
    {
        int days = 0;
        double pp = 0;
        bool month = false;
        if (Prog.Equals("PLP")) {
            switch (Freq)
            {
                case 1:
                    pp = .0125 * Princ /(1-(Math.Pow(1+0.0125,Inst * -1)));
                    
                    month = true;
                    break;
                case 2:
                    pp = .003125 * Princ / (1 - (Math.Pow(1 + 0.003125, Inst * -1)));
                    days = 7;
                    break;
                case 3:
                    pp = .00625 * Princ / (1 - (Math.Pow(1 + .00625, Inst * -1)));
                    days = 14;
                    break;
            }
        }
        else
        {
            switch (Freq)
            {
                case 1:
                    pp = .025 * Princ / (1 - (Math.Pow(1 + 0.025, Inst * -1)));

                    month = true;
                    break;
                case 2:
                    pp = .00625 * Princ / (1 - (Math.Pow(1 + 0.00625, Inst * -1)));
                    days = 7;
                    break;
                case 3:
                    pp = .0125 * Princ / (1 - (Math.Pow(1 + .0125, Inst * -1)));
                    days = 14;
                    break;
            }

        }
       double rounded = Math.Round(pp, 2);
        this.Plan = new ArrayList();
        DateTime temp = time;
        for (int i = 0; i < Inst; i++)
        {
            PPlan x = new PPlan();
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

    }

    public void CreatePlan(int Inst, int Freq, float Princ, string Prog, DateTime time, double PaymentTotal) {
        int days = 0;
        double pp = 0;
        bool month = false;
        if (Prog.Equals("PLP"))
        {
            switch (Freq)
            {
                case 1:
                    pp = .0125 * Princ / (1 - (Math.Pow(1 + 0.0125, Inst * -1)));

                    month = true;
                    break;
                case 2:
                    pp = .003125 * Princ / (1 - (Math.Pow(1 + 0.003125, Inst * -1)));
                    days = 7;
                    break;
                case 3:
                    pp = .00625 * Princ / (1 - (Math.Pow(1 + .00625, Inst * -1)));
                    days = 14;
                    break;
            }
        }
        else
        {
            switch (Freq)
            {
                case 1:
                    pp = .025 * Princ / (1 - (Math.Pow(1 + 0.025, Inst * -1)));

                    month = true;
                    break;
                case 2:
                    pp = .00625 * Princ / (1 - (Math.Pow(1 + 0.00625, Inst * -1)));
                    days = 7;
                    break;
                case 3:
                    pp = .0125 * Princ / (1 - (Math.Pow(1 + .0125, Inst * -1)));
                    days = 14;
                    break;
            }

        }
        double rounded = Math.Round(pp, 2);
       
        this.Plan = new ArrayList();
        DateTime temp = time;
        for (int i = 0; i < Inst; i++)
        {
            PaymentTotal = Math.Round(PaymentTotal, 2);   
            PPlan x = new PPlan();
            x.AmtDue = rounded;
            if (PaymentTotal >= x.AmtDue)
            {
                PaymentTotal -= (float)x.AmtDue;
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
		 
	
            if (0 > x.DateDue.AddMonths(1).CompareTo(DateTime.Now))
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