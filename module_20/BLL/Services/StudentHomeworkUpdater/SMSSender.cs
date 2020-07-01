﻿using System;
using BLL.Interfaces;
using DAL.Entities;
using Microsoft.Extensions.Logging;

namespace BLL.Services.StudentHomeworkUpdater
{
    public class SMSSender : IMessageSender
    {
        public void Send(Student student, ILogger logger = null)
        {
            logger?.LogInformation($"{DateTime.UtcNow} Student {student.FirstName} {student.LastName} " +
                                  $"with id {student.Id} has an average mark {student.AverageMark}." +
                                  $" Start doing something!");
        }
    }
}
