using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ActiveLearning.DB;
using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;
using System.Web.Script.Serialization;

namespace ActiveLearning.DB
{
    [MetadataType(typeof(QuizAnswerMetadata))]
    public partial class QuizAnswer
    {
        public int CourseSid { get; set; }
        public class QuizAnswerMetadata
        {
            [JsonIgnore]
            [ScriptIgnore]
            [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}")]
            public DateTime CreateDT { get; set; }
           
        }
    }
}
