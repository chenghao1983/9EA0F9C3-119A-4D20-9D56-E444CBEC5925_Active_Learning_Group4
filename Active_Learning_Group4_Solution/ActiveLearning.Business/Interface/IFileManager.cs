using ActiveLearning.DB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ActiveLearning.Business.Interface
{
    public interface IFileManager
    {
        void AddFile(Content fileDetail);

        void DeleteFile();

        IEnumerable<Content> GetFiles(int courseID);

        string GetGUIDFile(string originalFilename, int courseID);

    }
}
