using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace LaCeibaNetv4.Models
{
    [MetadataType(typeof(UserMetaData1))]
    public partial class ProgramClientTbl
    {
    }
    public class UserMetaData1
    {

        public int Id { get; set; }
        [Display(Name="Client")]
        public int ClientId { get; set; }
        [Display(Name = "Program")]
        public int ProgramId { get; set; }

    }
}