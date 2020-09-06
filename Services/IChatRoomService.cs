using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ChatAppTest.Models;

namespace ChatAppTest.Services
{
    public interface IChatRoomService
    {
        Task<List<ChatRoom>> GetChatRoomsAsync();
        Task<bool> AddChatRoomAsync(ChatRoom newChatRoom);
    }
}
