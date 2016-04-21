using ActiveLearning.Business.Interface;
using ActiveLearning.DB;
using ActiveLearning.Repository;
using ActiveLearning.Repository.CustomExcepetion;
using ActiveLearning.Repository.Interface;
using ActiveLearning.Repository.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ActiveLearning.Repository.Context;
using ActiveLearning.Business.Common;
using System.Transactions;

namespace ActiveLearning.Business.Implementation
{
    public class ChatManager : BaseManager, IChatManager
    {
        public Chat GetChatByChatSid(int chatSid, out string message)
        {
            message = string.Empty;
            try
            {
                using (var unitOfWork = new UnitOfWork(new ActiveLearningContext()))
                {
                    var chat = unitOfWork.Chats.Find(c => c.Sid == chatSid && !c.DeleteDT.HasValue).FirstOrDefault();

                    if (chat == null)
                    {
                        message = Constants.Chat_str + Constants.Not_Found;
                        return null;
                    }
                    message = string.Empty;
                    return chat;
                }
            }
            catch (Exception ex)
            {
                ExceptionLog(ex);
                message = Constants.Operation_Failed_Duing + Constants.Retrieving + Constants.Chat_str + Constants.Contact_System_Admin;
                return null;
            }
        }
        public IEnumerable<Chat> GetChatsByCourseSid(int courseSid, out string message)
        {
            message = string.Empty;
            try
            {
                using (var unitOfWork = new UnitOfWork(new ActiveLearningContext()))
                {
                    var chats = unitOfWork.Chats.Find(c => c.CourseSid == courseSid && !c.DeleteDT.HasValue);

                    if (chats == null || chats.Count() == 0)
                    {
                        message = Constants.Chat_str + Constants.Not_Found;
                        return null;
                    }
                    message = string.Empty;
                    return chats;
                }
            }
            catch (Exception ex)
            {
                ExceptionLog(ex);
                message = Constants.Operation_Failed_Duing + Constants.Retrieving + Constants.Chat_str + Constants.Contact_System_Admin;
                return null;
            }
        }
        public IEnumerable<int> GetChatSidsByCourseSid(int courseSid, out string message)
        {
            var chats = GetChatsByCourseSid(courseSid, out message);
            if (chats == null || chats.Count() == 0)
            {
                return null;
            }
            message = string.Empty;
            return chats.Select(c => c.Sid);
        }
        public IEnumerable<Chat> GetChatsByStudentSidAndCourseSid(int studentSid, int courseSid, out string message)
        {
            message = string.Empty;
            try
            {
                using (var unitOfWork = new UnitOfWork(new ActiveLearningContext()))
                {
                    var chats = unitOfWork.Chats.Find(c => c.StudentSid == studentSid && c.CourseSid == courseSid && !c.DeleteDT.HasValue);

                    if (chats == null || chats.Count() == 0)
                    {
                        message = Constants.Chat_str + Constants.Not_Found;
                        return null;
                    }
                    message = string.Empty;
                    return chats;
                }
            }
            catch (Exception ex)
            {
                ExceptionLog(ex);
                message = Constants.Operation_Failed_Duing + Constants.Retrieving + Constants.Chat_str + Constants.Contact_System_Admin;
                return null;
            }
        }
        public IEnumerable<int> GetChatSidsByStudentSidAndCourseSid(int studentSid, int courseSid, out string message)
        {
            var chats = GetChatsByStudentSidAndCourseSid(studentSid, courseSid, out message);
            if (chats == null || chats.Count() == 0)
            {
                return null;
            }
            message = string.Empty;
            return chats.Select(c => c.Sid);
        }
        public IEnumerable<Chat> GetChatsByInstructorSidAndCourseSid(int instructorSid, int courseSid, out string message)
        {
            message = string.Empty;
            try
            {
                using (var unitOfWork = new UnitOfWork(new ActiveLearningContext()))
                {
                    var chats = unitOfWork.Chats.Find(c => c.InstructorSid == instructorSid && c.CourseSid == courseSid && !c.DeleteDT.HasValue);

                    if (chats == null || chats.Count() == 0)
                    {
                        message = Constants.Chat_str + Constants.Not_Found;
                        return null;
                    }
                    message = string.Empty;
                    return chats;
                }
            }
            catch (Exception ex)
            {
                ExceptionLog(ex);
                message = Constants.Operation_Failed_Duing + Constants.Retrieving + Constants.Chat_str + Constants.Contact_System_Admin;
                return null;
            }
        }
        public IEnumerable<int> GetChatSidsByInstructorSidAndCourseSid(int instructorSid, int courseSid, out string message)
        {
            var chats = GetChatsByInstructorSidAndCourseSid(instructorSid, courseSid, out message);
            if (chats == null || chats.Count() == 0)
            {
                return null;
            }
            message = string.Empty;
            return chats.Select(c => c.Sid);
        }
        public Chat AddStudentChatToCourse(Chat chat, int studentSid, int courseSid, out string message)
        {
            message = string.Empty;
            if (chat == null)
            {
                message = Constants.Empty + Constants.Chat_str;
                return null;
            }
            if (studentSid == 0)
            {
                message = Constants.Empty + Constants.Student_str;
                return null;
            }
            if (courseSid == 0)
            {
                message = Constants.Empty + Constants.Course_str;
                return null;
            }
            try
            {
                using (var unitOfWork = new UnitOfWork(new ActiveLearningContext()))
                {
                    using (TransactionScope scope = new TransactionScope())
                    {
                        chat.CreateDT = DateTime.Now;
                        chat.StudentSid = studentSid;
                        chat.CourseSid = courseSid;
                        unitOfWork.Chats.Add(chat);
                        unitOfWork.Complete();
                        scope.Complete();
                    }
                    message = string.Empty;
                    return chat;
                }
            }
            catch (Exception ex)
            {
                ExceptionLog(ex);
                message = Constants.Operation_Failed_Duing + Constants.Adding + Constants.Chat_str + Constants.Contact_System_Admin;
                return null;
            }
        }
        public Chat AddInstructorChatToCourse(Chat chat, int instructorSid, int courseSid, out string message)
        {
            message = string.Empty;
            if (chat == null)
            {
                message = Constants.Empty + Constants.Chat_str;
                return null;
            }
            if (instructorSid == 0)
            {
                message = Constants.Empty + Constants.Student_str;
                return null;
            }
            if (courseSid == 0)
            {
                message = Constants.Empty + Constants.Course_str;
                return null;
            }
            try
            {
                using (var unitOfWork = new UnitOfWork(new ActiveLearningContext()))
                {
                    using (TransactionScope scope = new TransactionScope())
                    {
                        chat.CreateDT = DateTime.Now;
                        chat.InstructorSid = instructorSid;
                        chat.CourseSid = courseSid;
                        unitOfWork.Chats.Add(chat);
                        unitOfWork.Complete();
                        scope.Complete();
                    }
                    message = string.Empty;
                    return chat;
                }
            }
            catch (Exception ex)
            {
                ExceptionLog(ex);
                message = Constants.Operation_Failed_Duing + Constants.Adding + Constants.Chat_str + Constants.Contact_System_Admin;
                return null;
            }
        }
        public bool UpdateChat(Chat chat, out string message)
        {
            message = string.Empty;
            if (chat == null)
            {
                message = Constants.Empty + Constants.Chat_str;
                return false;
            }
            try
            {
                using (var unitOfWork = new UnitOfWork(new ActiveLearningContext()))
                {
                    using (TransactionScope scope = new TransactionScope())
                    {
                        chat.UpdateDT = DateTime.Now;
                        Util.CopyNonNullProperty(chat, unitOfWork.Chats.Get(chat.Sid));
                        unitOfWork.Complete();
                        scope.Complete();
                    }
                }
                message = string.Empty;
                return true;
            }
            catch (Exception ex)
            {
                ExceptionLog(ex);
                message = Constants.Operation_Failed_Duing + Constants.Updating + Constants.Chat_str + Constants.Contact_System_Admin;
                return false;
            }
        }
        public bool DeleteChat(Chat chat, out string message)
        {
            if (chat == null || chat.Sid == 0)
            {
                message = message = Constants.Empty + Constants.Chat_str;
                return false;
            }
            return DeleteChat(chat.Sid, out message);
        }
        public bool DeleteChat(int chatSid, out string message)
        {
            message = string.Empty;
            if (chatSid == 0)
            {
                message = Constants.Empty + Constants.Chat_str;
                return false;
            }
            var chat = GetChatByChatSid(chatSid, out message);
            if (chat == null)
            {
                return false;
            }
            try
            {
                using (var unitOfWork = new UnitOfWork(new ActiveLearningContext()))
                {
                    using (TransactionScope scope = new TransactionScope())
                    {
                        unitOfWork.Chats.Get(chatSid).DeleteDT = DateTime.Now;
                        unitOfWork.Complete();
                        scope.Complete();
                    }
                    message = string.Empty;
                    return true;
                }
            }
            catch (Exception ex)
            {
                ExceptionLog(ex);
                message = Constants.Operation_Failed_Duing + Constants.Deleting + Constants.Chat_str + Constants.Contact_System_Admin;
                return false;
            }
        }
        public bool SendMessageToGroup(string groupName, string messge, out string message)
        {
            throw new NotImplementedException();
        }
        public bool GetMessageByGroup(string groupName, out string message)
        {
            throw new NotImplementedException();
        }
    }
}
