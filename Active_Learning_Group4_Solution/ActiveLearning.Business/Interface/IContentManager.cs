using ActiveLearning.DB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ActiveLearning.Business.Interface
{
    public interface IContentManager
    {
        Content AddContent(Content content, int courseSid, out string message);

        bool DeleteContent(Content conten, out string message);

        IEnumerable<Content> GetContentsByCourseSid(int courseSid, out string message);

        string GetGUIDFile(string originalFilename, int courseID, out string message);

    }
}
