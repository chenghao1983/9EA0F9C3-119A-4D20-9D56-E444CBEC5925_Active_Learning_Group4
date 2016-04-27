using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ActiveLearning.DB;
using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;


namespace ActiveLearning.DB
{
    [MetadataType(typeof(QuizAnswerMetadata))]
    public partial class QuizAnswer
    {
        public class QuizAnswerMetadata
        {

            [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}")]
            public DateTime CreateDT { get; set; }

        }
    }
}
