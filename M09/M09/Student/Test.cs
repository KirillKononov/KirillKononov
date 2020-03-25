using System;

namespace M09
{
    public class Test : IEquatable<Test>
    {
        public bool Equals(Test other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return Subject == other.Subject && Mark == other.Mark && Equals(Date, other.Date);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((Test) obj);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Subject, Mark, Date);
        }

        public string Subject { get; set; }
        public int Mark { get; set; }
        public DateTime Date { get; set; }
    }
}
