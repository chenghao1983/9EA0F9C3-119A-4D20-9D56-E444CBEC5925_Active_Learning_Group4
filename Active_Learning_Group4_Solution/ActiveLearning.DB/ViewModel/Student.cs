using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ActiveLearning.DB;
using System.ComponentModel.DataAnnotations;

namespace ActiveLearning.DB
{
    [MetadataType(typeof(UserMetadata))]
    public partial class Student
    {
        private class UserMetadata
        {
            [Required(ErrorMessage = Common.Constants.Please_Enter + "Batch Number")]
            [Display(Name = "Batch Number")]
            public string BatchNo { get; set; }

            [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}")]
            public DateTime CreateDT { get; set; }
        }

        [Display(Name = "Enrolled")]
        public bool HasEnrolled { get; set; }

    }
}
