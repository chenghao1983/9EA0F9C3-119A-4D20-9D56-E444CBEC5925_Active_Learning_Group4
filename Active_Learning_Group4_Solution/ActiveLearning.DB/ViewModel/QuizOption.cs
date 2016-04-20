using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ActiveLearning.DB
{
    [MetadataType(typeof(TriviaOptionMetadata))]
    public partial class QuizOption
    {
        private class TriviaOptionMetadata
        {
            [JsonIgnore]
            public bool IsCorrect { get; set; }
            [JsonIgnore]
            public System.DateTime CreateDT { get; set; }

            [JsonIgnore]
            public Nullable<System.DateTime> UpdateDT { get; set; }

            [JsonIgnore]
            public Nullable<System.DateTime> DeleteDT { get; set; }

            [JsonIgnore]
            public virtual ICollection<QuizAnswer> QuizAnswers { get; set; }

            [JsonIgnore]
            public virtual QuizQuestion QuizQuestion { get; set; }

        }
    }
}
