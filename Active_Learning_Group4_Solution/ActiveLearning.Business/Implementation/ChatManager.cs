using ActiveLearning.Business.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ActiveLearning.DB;
using ActiveLearning.Business.Common;
namespace ActiveLearning.Business.Implementation
{
    public class ChatManager : BaseManager, IChatManager
    {
        public Chat AddChat(Chat chat, out string message)
        {
            throw new NotImplementedException();
        }

        public bool DeleteChat(Chat chat, out string message)
        {
            throw new NotImplementedException();
        }

        public IEnumerator<Chat> GetAllChat(out string message)
        {
            throw new NotImplementedException();
        }

        public Chat GetChatByChatSid(int chatSid, out string message)
        {
            throw new NotImplementedException();
        }

        public bool GetMessageByGroup(string groupName, out string message)
        {
            throw new NotImplementedException();
        }

        public bool SendMessageToGroup(string groupName, string messge, out string message)
        {
            throw new NotImplementedException();
        }

        public bool UpdateChat(Chat chat, out string message)
        {
            throw new NotImplementedException();
        }
    }
}
