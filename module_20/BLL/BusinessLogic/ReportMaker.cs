namespace BLL.BusinessLogic
{
    public class ReportMaker //: IReportMaker
    {
        //private readonly IUnitOfWork _dataBase;
        //private readonly string _name;
        //private readonly ILogger _logger;

        //public ReportMaker(IUnitOfWork db, string name, ILogger<ReportMaker> logger = null)
        //{
        //    _dataBase = db;
        //    _name = name;
        //    _logger = logger;
        //}

        //public string MakeReport()
        //{
        //    var student = _dataBase.Students.Find(person =>
        //        person.FirstName + " " + person.LastName == _name).ToList();

        //    if (student.Count != 0)
        //    {
        //        var attendance = _dataBase.Homework.Find(homeWork =>
        //            homeWork.Student == student[0]);
        //        return JsonConvert.SerializeObject(attendance, 
        //            new IsoDateTimeConverter { DateTimeFormat = "dd/MM/yyyy" });
        //    }

        //    var lecture = _dataBase.Lectures.Find(subject =>
        //        subject.Name == _name).ToList();

        //    if (lecture.Count != 0)
        //    {
        //        var attendance = _dataBase.Homework.Find(homeWork => 
        //            homeWork.Lecture == lecture[0]);
        //        return JsonConvert.SerializeObject(attendance,
        //            new IsoDateTimeConverter { DateTimeFormat = "dd/MM/yyyy" });
        //    }

        //    _logger.LogError("Entered lecture or student doesn't exist");
        //    throw new ValidationException("Entered lecture or student doesn't exist");
        //}
    }
}
