using System;
using System.Collections.Generic;
using BLL.DTO;

namespace BLL.Interfaces
{
    public interface IReportService
    {
        string MakeStudentReport(string firstName, string lastName, Func<IEnumerable<Attendance>, string> serializer = null);
        string MakeLectureReport(string lectureName, Func<IEnumerable<Attendance>, string> serializer = null);
    }
}
