using Newtonsoft.Json;
using System;
using System.ComponentModel.DataAnnotations;
using System.Web.Script.Serialization;

namespace ActiveLearning.DB
{
    [MetadataType(typeof(QuizQuestionMetadata))]
    public partial class QuizQuestion
    {
        private class QuizQuestionMetadata
        {
            [JsonIgnore]
            [ScriptIgnore]
            [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}")]
            public System.DateTime CreateDT { get; set; }

            [JsonIgnore]
            [ScriptIgnore]
            [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", NullDisplayText = "-")]
            public Nullable<System.DateTime> UpdateDT { get; set; }

            [JsonIgnore]
            [ScriptIgnore]
            [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", NullDisplayText = "-")]
            public Nullable<System.DateTime> DeleteDT { get; set; }

            [JsonIgnore]
            [ScriptIgnore]
            public int CourseSid { get; set; }

            [JsonIgnore]
            [ScriptIgnore]
            public virtual Course Course { get; set; }
        }
    }
}
