using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace LaCeibaNetv4.Models
{
    [MetadataType(typeof(UserMetaData))]
    public partial class ClientsTbl
    {

    }
    public class UserMetaData
    {
    
        public int Id { get; set; }
        [Display(Name = "First Name")]
        public string FirstName { get; set; }

        [Display(Name = "Father Surname")]
        public string LastName { get; set; }

        [Display(Name = "Center")]
        public int CenterId { get; set; }
        [Display(Name = "Middle Name (opt)")]
        public string MiddleName1 { get; set; }
        [Display(Name = "Mother Surname(opt)")]
        public string MiddleName2 { get; set; }
        [Display(Name = "Birth Day")]
        [DataType(DataType.Date)]
        public System.DateTime BirthDay { get; set; }
    }
}