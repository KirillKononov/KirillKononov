using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace M09
{
    public class DataRetriever
    {
        public DataRetriever(string path)
        {
            _path = path;
        }

        private static Dictionary<string, string> CriteriaParser(string criteria)
        {
            var allCriteria = new Dictionary<string, string>
            {
                ["-name"] = null,
                ["-surname"] = null,
                ["-minmark"] = null,
                ["-maxmark"] = null,
                ["-datefrom"] = null,
                ["-dateto"] = null,
                ["-test"] = null,
                ["-sort"] = null
            };

            var parsedCriteria = criteria.Split(" ");

            for (var i = 0; i < parsedCriteria.Length; ++i)
            {
                switch (parsedCriteria[i])
                {
                    case "-name":
                        ++i;
                        allCriteria["-name"] = parsedCriteria[i];
                        break;
                    case "-surname":
                        ++i;
                        allCriteria["-surname"] = parsedCriteria[i];
                        break;
                    case "-minmark":
                        ++i;
                        allCriteria["-minmark"] = parsedCriteria[i];
                        break;
                    case "-maxmark":
                        ++i;
                        allCriteria["-maxmark"] = parsedCriteria[i];
                        break;
                    case "-datefrom":
                        ++i;
                        allCriteria["-datefrom"] = parsedCriteria[i];
                        break;
                    case "-dateto":
                        ++i;
                        allCriteria["-dateto"] = parsedCriteria[i];
                        break;
                    case "-test":
                        ++i;
                        allCriteria["-test"] = parsedCriteria[i];
                        break;
                    case "-sort":
                        if (i + 2 >= parsedCriteria.Length)
                        {
                            throw new FormatException($"Was entered invalid criteria: {parsedCriteria[i]}");
                        }

                        ++i;
                        allCriteria["-sort"] = parsedCriteria[i] + " " + parsedCriteria[++i];
                        ++i;
                        break;
                    default:
                        throw new FormatException($"Was entered invalid criteria: {parsedCriteria[i]}");
                }
            }

            return allCriteria;
        }

        public IEnumerable<StudentAndTest> Filter(string criteria)
        {
            var file = File.ReadAllText(_path);

            var students = JsonConvert.DeserializeObject<List<Student>>(file, 
                new IsoDateTimeConverter {DateTimeFormat = "dd/MM/yyyy"});

            var allCriteria = CriteriaParser(criteria);
            
            var result = students
                .Where(student => FilterForName(student, allCriteria))
                .SelectMany(student => student.Test, ((student, test) => new StudentAndTest
                {
                    Name = student.Name,
                    Subject = test.Subject,
                    Mark = test.Mark,
                    Date = test.Date
                }))
                .Where(studentAndTest => FilterForTest(studentAndTest, allCriteria));



            if (!string.IsNullOrEmpty(allCriteria["-sort"]))
            {
                var type = allCriteria["-sort"].Split(" ");

                result = type[0] switch
                {
                    "name" => result.OrderBy(studentAndTest => studentAndTest.Name.Split(' ')[0]),
                    "surname" => result.OrderBy(studentAndTest => studentAndTest.Name.Split(' ')[1]),
                    "subject" => result.OrderBy(studentAndTest => studentAndTest.Subject),
                    "mark" => result.OrderBy(studentAndTest => studentAndTest.Mark),
                    "date" => result.OrderBy(studentAndTest => studentAndTest.Date),
                    _ => throw new FormatException($"Was entered invalid sorting criteria: {type[0]}")
                };


                result = type[1] switch
                {
                    "asc" => result,
                    "dsc" => result.Reverse(),
                    _ => throw new FormatException($"Was entered invalid sorting criteria: {type[1]}'")
                };

            }

            return result;
        }

        private static bool FilterForName(Student student, IReadOnlyDictionary<string, string> allCriteria)
        {
            return
                (string.IsNullOrEmpty(allCriteria["-name"])) || (student.Name.Split(' ')[0] == allCriteria["-name"])
                &&
                (string.IsNullOrEmpty(allCriteria["-surname"])) || (student.Name.Split(' ')[1] == allCriteria["-surname"]);
        }

        private static bool FilterForTest(StudentAndTest studentAndTest, IReadOnlyDictionary<string, string> allCriteria)
        {
            return
                (string.IsNullOrEmpty(allCriteria["-test"]) || studentAndTest.Subject == allCriteria["-test"])
                &&
                (string.IsNullOrEmpty(allCriteria["-datefrom"]) || 
                 studentAndTest.Date >= DateTime.ParseExact(allCriteria["-datefrom"], "dd/MM/yyyy", null))
                &&
                (string.IsNullOrEmpty(allCriteria["-dateto"]) || 
                 studentAndTest.Date <= DateTime.ParseExact(allCriteria["-dateto"], "dd/MM/yyyy", null))
                &&
                (string.IsNullOrEmpty(allCriteria["-minmark"]) ||
                 studentAndTest.Mark >= Convert.ToInt32(allCriteria["-minmark"]))
                &&
                (string.IsNullOrEmpty(allCriteria["-maxmark"]) ||
                 studentAndTest.Mark <= Convert.ToInt32(allCriteria["-maxmark"]));
        }

        private readonly string _path;
    }
}
