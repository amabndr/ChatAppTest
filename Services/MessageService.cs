using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ChatAppTest.Models;
using ChatAppTest.Data;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.SignalR;
using ChatAppTest.Hubs;

namespace ChatAppTest.Services
{
    public class MessageService : IMessageService
    {
        private readonly ApplicationDbContext _context;
        private IHubContext<ChatHub> _hubContext;
        public MessageService(ApplicationDbContext context, IHubContext<ChatHub> hubContext)
        {
            _context = context;
            _hubContext = hubContext;
        }

        public async Task<List<Message>> GetMessagesAsync()
        {
            var messages = await _context.Messages.ToListAsync<Message>();

            return messages;
        }

        public async Task<List<Message>> DeleteMsgAsync(Guid roomId, Guid messageId)
        {
            var msg = new Message { Id = messageId };
            _context.Messages.Attach(msg);
            _context.Messages.Remove(msg);
            await _context.SaveChangesAsync();
            var messagesForRoom = await _context.Messages
                      .Where(m => m.RoomId == roomId)
                               .ToListAsync<Message>();
            await _hubContext.Clients.All.SendAsync("RetrieveMessage", roomId);
            return messagesForRoom;

        }
        public async Task<List<Message>> GetMessagesForChatRoomAsync(Guid roomId)
        {
            var messagesForRoom = await _context.Messages
                                      .Where(m => m.RoomId == roomId)
                                               .ToListAsync<Message>();

            return messagesForRoom;
        }

        public async Task<bool> AddMessageToRoomAsync(Guid roomId, Message message)
        {
            message.Id = Guid.NewGuid();
            message.RoomId = roomId;
            message.PostedAt = DateTimeOffset.Now;
            _context.Messages.Add(message);

            var saveResults = await _context.SaveChangesAsync();

            return saveResults > 0;
        }
    }
}
