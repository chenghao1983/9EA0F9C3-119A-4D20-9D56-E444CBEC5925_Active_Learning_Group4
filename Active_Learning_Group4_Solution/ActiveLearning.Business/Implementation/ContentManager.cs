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
    public class ContentManager : BaseManager, IContentManager
    {
        public Content AddContent(Content content, int courseSid, out string message)
        {
            message = string.Empty;
            using (var unitOfWork = new UnitOfWork(new ActiveLearningContext()))
            {
                // Same course cannot have same file name
                //var content = unitOfWork.Contents.GetAll().Where(c => c.CourseSid == content.CourseSid && c.DeleteDT == null && c.OriginalFileName == content.OriginalFileName).FirstOrDefault();


                if (content == null)
                {
                    unitOfWork.Contents.Add(content);

                    unitOfWork.Complete();
                    return content;
                }
                else
                {
                    throw new Exception("Same filename exits.");
                }
            }
        }

        public bool DeleteContent(Content content, out string message)
        {
            message = string.Empty;
            throw new NotImplementedException();
        }

        public IEnumerable<Content> GetContentsByCourseSid(int courseID, out string message)
        {
            message = string.Empty;
            using (var unitOfWork = new UnitOfWork(new ActiveLearningContext()))
            {
                return unitOfWork.Contents.GetAll().Where(c => c.CourseSid == courseID && c.DeleteDT == null);
            }
        }



        public string GetGUIDFile(string originalFilename, int courseID, out string message)
        {
            message = string.Empty;
            using (var unitOfWork = new UnitOfWork(new ActiveLearningContext()))
            {
                var file = unitOfWork.Contents.GetAll().Where(c => c.CourseSid == courseID && c.DeleteDT == null && c.OriginalFileName == originalFilename).FirstOrDefault();

                return file.FileName;
            }
        }
    }
}
