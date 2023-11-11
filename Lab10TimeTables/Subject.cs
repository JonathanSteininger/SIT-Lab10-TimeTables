using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab10TimeTables
{
    internal class Subject
    {
        private int _id;
        private string _name;
        public string Name { get { return _name; } }
        public int Id { get { return _id; } }
        public Subject(int id, string name)
        {
            _id = id;
            _name = name;
        }

        public override string ToString() => _name;
    }
}
