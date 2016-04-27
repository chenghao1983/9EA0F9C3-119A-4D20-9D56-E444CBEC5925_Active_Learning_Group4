using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ActiveLearning.DB
{
    [MetadataType(typeof(QuizOptionMetadata))]
    public partial class QuizOption
    {
        private class QuizOptionMetadata
        {
            [JsonIgnore]
            public bool IsCorrect { get; set; }

            [JsonIgnore]
            [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}")]
            public System.DateTime CreateDT { get; set; }

            [JsonIgnore]
            [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}")]
            public Nullable<System.DateTime> UpdateDT { get; set; }

            [JsonIgnore]
            [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}")]
            public Nullable<System.DateTime> DeleteDT { get; set; }

            [JsonIgnore]
            public virtual ICollection<QuizAnswer> QuizAnswers { get; set; }

            [JsonIgnore]
            public virtual QuizQuestion QuizQuestion { get; set; }

        }
    }
}
