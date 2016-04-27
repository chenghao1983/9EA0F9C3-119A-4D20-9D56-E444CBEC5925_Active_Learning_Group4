﻿using Newtonsoft.Json;
using System;
using System.ComponentModel.DataAnnotations;

namespace ActiveLearning.DB
{
    [MetadataType(typeof(QuizQuestionMetadata))]
    public partial class QuizQuestion
    {

        private class QuizQuestionMetadata
        {
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
            public int CourseSid { get; set; }

            [JsonIgnore]
            public virtual Course Course { get; set; }

        }









    }
}
