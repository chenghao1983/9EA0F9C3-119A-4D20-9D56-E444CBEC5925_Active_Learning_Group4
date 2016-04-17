using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ActiveLearning.DB;
using System.ComponentModel.DataAnnotations;
using ActiveLearning.DB.Common;

namespace ActiveLearning.DB
{
    [MetadataType(typeof(ChatDetailMetadata))]
    public partial class ChatDetail
    {
        public class ChatDetailMetadata
        {
            [Required(ErrorMessage = Constants.Please_Enter + "message")]
            [Display(Name = "Message")]
            public string Message { get; set; }
        }
    }
}
