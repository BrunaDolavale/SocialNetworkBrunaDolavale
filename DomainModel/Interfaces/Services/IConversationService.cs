using DomainModel.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainModel.Interfaces.Services
{
    public interface IConversationService
    {
        IEnumerable<Conversation> GetAllConversations(User user);
        IEnumerable<Message> GetAllMessages(Conversation conversation);
        void SendMessage(Conversation conversation, Message message);
        void DeleteMessage(Message message);
        void DeleteConversation(Conversation conversation);
    }
}
