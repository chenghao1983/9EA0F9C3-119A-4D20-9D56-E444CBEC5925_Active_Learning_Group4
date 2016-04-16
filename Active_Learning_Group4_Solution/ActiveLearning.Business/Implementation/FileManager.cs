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
                // Same course cannot have same file name
                var file = unitOfWork.Contents.GetAll().Where(c => c.CourseSid == fileDetail.CourseSid && c.DeleteDT == null && c.OriginalFileName == fileDetail.OriginalFileName).FirstOrDefault();

                if (file == null)
                {
                    unitOfWork.Contents.Add(fileDetail);

                    unitOfWork.Complete();
                }
                else
                {
                    throw new Exception("Same filename exits.");
                }
            }
        }

        public void DeleteFile()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Content> GetFiles(int courseID)
        {
            using (var unitOfWork = new UnitOfWork(new ActiveLearningContext()))
            {
                return unitOfWork.Contents.GetAll().Where(c => c.CourseSid == courseID && c.DeleteDT == null);
            }
        }

        public string GetGUIDFile(string originalFilename, int courseID)
        {
            using (var unitOfWork = new UnitOfWork(new ActiveLearningContext()))
            {
                var file = unitOfWork.Contents.GetAll().Where(c => c.CourseSid == courseID && c.DeleteDT == null && c.OriginalFileName == originalFilename).FirstOrDefault();

                return file.FileName;
            }
        }
    }
}
