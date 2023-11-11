using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab10TimeTables
{
    internal class TimeTableField
    {
        public Subject subject { get; set; }
        public int day { get; set; }
        public int Period { get; set; }

        public TimeTableField(Subject subject, int day, int period)
        {
            this.subject = subject;
            this.day = day;
            this.Period = period;
        }

        public override string ToString() => subject.ToString();
    }
}
