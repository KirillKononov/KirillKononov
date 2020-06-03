using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml.Serialization;
using BLL.DTO;

namespace BLL.BusinessLogic.Serializers
{
    public class XmlAttendanceSerializer
    {
        public string Serialize(IEnumerable<Attendance> attendance)
        {
            attendance = attendance.ToList();
            var memoryStream = new MemoryStream();
            var serializer = new XmlSerializer(typeof(List<Attendance>));
            serializer.Serialize(memoryStream, attendance);

            return System.Text.Encoding.UTF8.GetString(memoryStream.ToArray());
        }
    }
}
