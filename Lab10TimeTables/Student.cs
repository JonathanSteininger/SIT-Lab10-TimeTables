using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab10TimeTables
{
    internal class Student
    {
        private int _id;
        private string _firstName;
        private string _lastName;
        public string FirstName { get { return _firstName; } }
        public string LastName { get { return _lastName; } }
        public int ID { get { return _id; } }

        public Student(int id, string firstName, string lastName)
        {
            _id = id;
            _firstName = firstName;
            _lastName = lastName;
        }

        public override string ToString() => $"ID: {_id}, firstname: {_firstName}, lastname: {_lastName}. ";
    }
}
