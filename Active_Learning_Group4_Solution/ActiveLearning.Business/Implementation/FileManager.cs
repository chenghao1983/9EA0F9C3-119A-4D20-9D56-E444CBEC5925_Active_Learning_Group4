using ActiveLearning.Business.Interface;
using ActiveLearning.DB;
using ActiveLearning.Repository;
using ActiveLearning.Repository.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ActiveLearning.Business.Implementation
{
    public class FileManager : BaseManager, IFileManager
    {
        public void AddFile(Content fileDetail)
        {
            using (var unitOfWork = new UnitOfWork(new ActiveLearningContext()))
            {
                unitOfWork.Contents.Add(fileDetail);

                unitOfWork.Complete();
            }
        }

        public void DeleteFile()
        {
            throw new NotImplementedException();
        }
    }
}
