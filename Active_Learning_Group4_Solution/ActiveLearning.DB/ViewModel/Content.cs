using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ActiveLearning.DB;
using System.ComponentModel.DataAnnotations;

namespace ActiveLearning.DB
{
    [MetadataType(typeof(ContentMetadata))]
    public partial class Content
    {
        public class ContentMetadata
        {
            [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}")]
            public DateTime CreateDT { get; set; }
        }
    }
}
