using System;

namespace M09
{
    public class StudentAndTest : IEquatable<StudentAndTest>
    {
        public bool Equals(StudentAndTest other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return Name == other.Name && Subject == other.Subject && Mark == other.Mark && Date.Equals(other.Date);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((StudentAndTest) obj);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Name, Subject, Mark, Date);
        }

        public string Name { get; set; }
        public string Subject { get; set; }
        public int Mark { get; set; }
        public DateTime Date { get; set; }
    }
}
