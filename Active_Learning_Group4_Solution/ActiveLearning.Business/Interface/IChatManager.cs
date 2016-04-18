using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ActiveLearning.DB;
namespace ActiveLearning.Business.Interface
{
    public interface IChatManager : IDisposable
    {
        Chat GetChatByChatSid(int chatSid, out string message);
        //ChatRecord GetChatRecordByChatRecordSid(int chatRecordSid, out string message);
        IEnumerable<Chat> GetAllChat(out string message);
        Chat AddChat(Chat chat, out string message);
        bool UpdateChat(Chat chat, out string message);
        bool DeleteChat(Chat chat, out string message);
        bool SendMessageToGroup(string groupName, string messge, out string message);
        bool GetMessageByGroup(string groupName, out string message);

    }
}
