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
    public partial class User
    {
        private class UserMetadata
        {
            [Required(ErrorMessage = "*")]
            [Display(Name = "Login Name")]
            public string Username;


            [Required(ErrorMessage = "*")]
            [Display(Name = "Password")]
            public string Password { get; set; }
        }
    }
}
