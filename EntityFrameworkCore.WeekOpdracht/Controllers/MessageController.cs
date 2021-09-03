﻿using EntityFrameworkCore.WeekOpdracht.Business.Entities;
using EntityFrameworkCore.WeekOpdracht.Business.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System;

namespace EntityFrameworkCore.WeekOpdracht.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MessageController : ControllerBase
    {
        private readonly IMessageService messageService;
        private readonly ILogger logger;

        public MessageController(IMessageService messageService, ILogger logger)
        {
            this.messageService = messageService;
            this.logger = logger;
        }

        [HttpPost]
        public IActionResult Create(Message message)
        {
            try
            {
                logger.LogInformation("Create Message called");
                return Ok(messageService.Add(message));
            }
            catch (Exception ex)
            {
                logger.LogException(ex);
                return BadRequest(new
                {
                    Message = ex.Message
                });
            }
        }

        [HttpGet]
        [Route("{id}")]
        public IActionResult GetById(int id)
        {
            try
            {
                logger.LogInformation("Message by Id called");
                return Ok(messageService.GetById(id));
            }
            catch (Exception ex)
            {
                logger.LogException(ex);
                return BadRequest(new
                {
                    Message = ex.Message
                });
            }
        }

        [HttpGet]
        [Route("user/{userId}")]
        public IActionResult GetByUserId(int userId)
        {
            try
            {
                logger.LogInformation("Messages by UserId called");
                return Ok(messageService.Get(userId));
            }
            catch (Exception ex)
            {
                logger.LogException(ex);
                return BadRequest(new
                {
                    Message = ex.Message
                });
            }
        }
    }
}
