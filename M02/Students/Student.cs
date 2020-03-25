using System;
using System.Collections.Generic;
using System.Text;


namespace Students
{
    class Student
    {
        public Student(string name, string email)
        {
            if (name != GetNameFromEmail(email))
            {
                throw new ArgumentException("Email doesn't match the name");
            }

            Name = name;
            Email = email;
        }

        public Student(string email) : this(GetNameFromEmail(email), email) { }

        private static string GetNameFromEmail(string email)
        {
            var parts = email.Split('.', '@');
            var sb = new StringBuilder();
            for (int i = 0; i < 2; ++i)
            {
                var sb2 = new StringBuilder();
                for (int j = 0; j < parts[i].Length; ++j)
                {
                    sb2.Append(j == 0 ? 
                        char.ToUpper(parts[i][0]) : char.ToLower(parts[i][j]));
                }
                sb.Append(sb2);
                if (i == 0)
                {
                    sb.Append(' ');
                }
            }
            return sb.ToString();
        }

        public override int GetHashCode()
        {
            var hashCode = 1666616157;
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(Name);
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(Email);
            return hashCode;
        }

        public override bool Equals(object obj)
        {
            return obj is Student student &&
                   Name == student.Name &&
                   Email == student.Email;
        }

        public string Name
        { get; set; }
        public string Email
        { get; set; }
    }
}