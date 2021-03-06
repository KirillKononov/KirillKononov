﻿using System.Collections.Generic;
using BLL.DTO;
using BLL.Interfaces;
using Newtonsoft.Json;

namespace BLL.Services.Report.Serializers
{
    public class JsonAttendanceSerializer : ISerializer
    {
        public string Serialize(IEnumerable<Attendance> attendance)
        {
            return JsonConvert.SerializeObject(attendance, Formatting.Indented);
        }
    }
}
