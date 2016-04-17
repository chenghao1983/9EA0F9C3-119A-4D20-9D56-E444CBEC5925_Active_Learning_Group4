using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ActiveLearning.DB;
using ActiveLearning.Business.Interface;
using ActiveLearning.Business.Common;

namespace ActiveLearning.Business.Implementation
{
    public class ContentManager : BaseManager, IContentManager
    {
        public Content AddContent(Content content, int CourseSid)
        {
            throw new NotImplementedException();
        }

        public Content GetActiveContentBySid(int Sid)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Content> GetContentByCourseSid(int CourseSid)
        {
            throw new NotImplementedException();
        }

        public bool RemoveContent(Content content)
        {
            throw new NotImplementedException();
        }

        public bool UpdateContent(Content content)
        {
            throw new NotImplementedException();
        }
    }
}
