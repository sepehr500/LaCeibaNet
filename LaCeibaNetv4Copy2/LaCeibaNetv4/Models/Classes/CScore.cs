using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LaCeibaNetv4.Models.Classes
{
    public static class CScore
    {
        //gets the numerator
        public static int getNum(ClientsTbl x) {
            int Num = 0;
            foreach (var item in x.ProgramClientTbls)
            {
                foreach (var item1 in item.RoundTbls)
                {
                    foreach (var item2 in item1.LoansTbls)
                    {


                            foreach (var y in item2.PaymentTbls)
                            {
                                DateTime a = y.DatePmtPaid ?? DateTime.Now;
                                TimeSpan z = a - y.DatePmtDue;
                                if (z.Days <= 9)
                                {
                                    Num += 10;
                                }
                                if (z.Days >= 10 && z.Days <= 19)
                                {
                                    Num += 9;
                                }
                                if (z.Days >= 20 && z.Days <= 29)
                                {
                                    Num += 8;
                                }
                                if (z.Days >= 30 && z.Days <= 39)
                                {
                                    Num += 7;
                                }
                                if (z.Days >= 40 && z.Days <= 49)
                                {
                                    Num += 6;
                                }
                                if (z.Days >= 50 && z.Days <= 59)
                                {
                                    Num += 5;
                                }
                                if (z.Days >= 60 && z.Days <= 69)
                                {
                                    Num += 4;
                                }
                                if (z.Days >= 70 && z.Days <= 79)
                                {
                                    Num += 3;
                                }
                                if (z.Days >= 80 && z.Days <= 89)
                                {
                                    Num += 2;
                                }
                                if (z.Days >= 90)
                                {
                                    Num += 1;
                                }

                            
                        }
                    }
                }
            }
            return Num;
        }
        //gets number of payments multiplied by 100
        public static int paymentCount(ClientsTbl x)
        {

            int Num = 0;
            foreach (var item in x.ProgramClientTbls)
            {
                foreach (var item1 in item.RoundTbls)
                {
                    foreach (var item2 in item1.LoansTbls)
                    {
                       Num += item2.PaymentTbls.Count;
                    }
                }
            }
            return Num * 10;
        }
        public static string getCScore(this ClientsTbl client)
        {
            decimal score = 0;
            if (CScore.paymentCount(client) != 0)
            {
            score = (decimal)CScore.getNum(client) / (decimal)CScore.paymentCount(client) * 100;
            }
            else
            {
            score = 0;
            }
        
           return  String.Format("{0:f2}", score) + "%";
        }
    }
}