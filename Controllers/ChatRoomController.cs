﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ChatAppTest.Models;
using ChatAppTest.Services;
using Microsoft.AspNetCore.Cors;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ChatAppTest.Controllers
{
    [EnableCors("CorsPolicy")]
    [Route("api/[controller]")]
    public class ChatRoomController : Controller
    {
        private readonly IChatRoomService _chatRoomService;

        public ChatRoomController(IChatRoomService chatRoomService)
        {
            _chatRoomService = chatRoomService;
        }

        // GET: api/values
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var chatRooms = await _chatRoomService.GetChatRoomsAsync();

            return Ok(chatRooms);
        }

        // POST api/values
        [HttpPost]
        public async void Post([FromBody]ChatRoom chatRoom)
        {
            await _chatRoomService.AddChatRoomAsync(chatRoom);
        }
    }
}