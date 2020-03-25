using System;
using System.Collections.Generic;

namespace M09
{
    public class Student
    {
        protected bool Equals(Student other)
        {
            return Name == other.Name && Equals(Test, other.Test);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((Student) obj);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Name, Test);
        }

        public string Name { get; set; }
        public List<Test> Test { get; set; }
    }
}
