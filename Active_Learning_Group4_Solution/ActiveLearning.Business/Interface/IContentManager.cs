using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ActiveLearning.DB;

namespace ActiveLearning.Business.Interface
{
    public interface IContentManager
    {
        IEnumerable<Content> GetContentByCourseSid(int courseSid);
        Content GetActiveContentBySid(int sid);
        Content AddContent(Content content, int courseSid);
        bool RemoveContent(Content content);
        bool UpdateContent(Content content);


    }
}
