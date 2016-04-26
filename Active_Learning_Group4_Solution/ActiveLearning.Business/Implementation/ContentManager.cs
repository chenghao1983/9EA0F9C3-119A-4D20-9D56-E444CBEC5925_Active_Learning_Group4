using ActiveLearning.Business.Interface;
using ActiveLearning.DB;
using ActiveLearning.Repository;
using ActiveLearning.Repository.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ActiveLearning.Business.Common;

namespace ActiveLearning.Business.Implementation
{
    public class ContentManager : BaseManager, IContentManager
    {
        public Content GetContentByContentSid(int contentSid, out string message)
        {
            message = string.Empty;
            try
            {
                using (var unitOfWork = new UnitOfWork(new ActiveLearningContext()))
                {
                    var content = unitOfWork.Contents.Find(c => c.Sid == contentSid && !c.DeleteDT.HasValue).FirstOrDefault();
                    if (content == null)
                    {
                        message = Constants.ValueNotFound(Constants.Content);
                        return null;
                    }
                    message = string.Empty;
                    return content;
                }
            }
            catch (Exception ex)
            {
                ExceptionLog(ex);
                message = Constants.OperationFailedDuringRetrievingValue(Constants.Content);
                return null;
            }
        }
        public IEnumerable<Content> GetContentsByCourseSid(int courseSid, out string message)
        {
            message = string.Empty;
            try
            {
                using (var unitOfWork = new UnitOfWork(new ActiveLearningContext()))
                {
                    var contents = unitOfWork.Contents.Find(c => c.CourseSid == courseSid && !c.DeleteDT.HasValue);
                    if (contents == null || contents.Count() == 0)
                    {
                        message = Constants.ThereIsNoValueFound(Constants.Content);
                        return null;
                    }
                    message = string.Empty;
                    return contents.ToList();
                }
            }
            catch (Exception ex)
            {
                ExceptionLog(ex);
                message = Constants.OperationFailedDuringRetrievingValue(Constants.Content);
                return null;
            }
        }

        public IEnumerable<int> GetContentSidsByCounrseSid(int courseSid, out string message)
        {
            var contents = GetContentsByCourseSid(courseSid, out message);
            if (contents == null || contents.Count() == 0)
            {
                return null;
            }
            return contents.Select(c => c.Sid).ToList();
        }
        public Content AddContent(Content content, int courseSid, out string message)
        {
            message = string.Empty;
            if(content ==null)
            {

            }
            try
            {
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
            catch (Exception)
            {

                throw;
            }
        }

        public bool DeleteContent(Content content, out string message)
        {
            message = string.Empty;
            throw new NotImplementedException();
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
