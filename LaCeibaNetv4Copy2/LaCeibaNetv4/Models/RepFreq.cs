//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace LaCeibaNetv4.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class RepFreq
    {
        public RepFreq()
        {
            this.LoansTbls = new HashSet<LoansTbl>();
        }
    
        public int Id { get; set; }
        public string Frequency { get; set; }
    
        public virtual ICollection<LoansTbl> LoansTbls { get; set; }
    }
}
