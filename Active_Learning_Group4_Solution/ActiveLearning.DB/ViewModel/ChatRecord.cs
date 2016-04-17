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
    [MetadataType(typeof(ChatRecordMetadata))]
    public partial class ChatRecord
    {
        public class ChatRecordMetadata
        {
            [Required(ErrorMessage = Constants.Please_Enter + "Topic")]
            [Display(Name = "Topic")]
            public string Topic { get; set; }
        }
        
    }
}
