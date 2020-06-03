using System.Collections.Generic;
using BLL.DTO;
using Newtonsoft.Json;

namespace BLL.BusinessLogic.Serializers
{
    public class JsonAttendanceSerializer
    {
        public string Serialize(IEnumerable<Attendance> attendance)
        {
            return JsonConvert.SerializeObject(attendance, Formatting.Indented);
        }
    }
}
