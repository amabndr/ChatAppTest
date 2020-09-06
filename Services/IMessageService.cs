using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ChatAppTest.Models;

namespace ChatAppTest.Services
{
    public interface IMessageService
    {
        Task<List<Message>> GetMessagesAsync();
        Task<List<Message>> GetMessagesForChatRoomAsync(Guid roomId);
        Task<bool> AddMessageToRoomAsync(Guid roomId, Message message);
        Task<List<Message>> DeleteMsgAsync(Guid roomId, Guid messageId);
    }
}
