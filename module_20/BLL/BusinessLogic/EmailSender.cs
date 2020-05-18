﻿using BLL.Interfaces;
using DAL.Entities;
using Microsoft.Extensions.Logging;

namespace BLL.BusinessLogic
{
    public class EmailSender : IMessageSender
    {
        public void Send(Student student, ILogger logger = null)
        {
            logger.LogInformation($"Student {student.FirstName} {student.LastName} missed" +
                                  $" {student.MissedLectures} lectures");
        }
    }
}
