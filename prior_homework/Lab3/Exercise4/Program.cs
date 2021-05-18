using System;

namespace Exercise4
{
    
    class Person
    {
        public int Age;
        public virtual void SetAge(int n)
        {
            Age = n;
        }
        public void Greet()
        {
            Console.WriteLine("Hello");
        }

    }

    class Student:Person
    {
        public void GoToClasses()
        {
            Console.WriteLine("I'm going to class.");
        }
        public void ShowAge()
        {
            Console.WriteLine($"My age is: {Age} years old");
        }
    }
    class Teacher:Person
    {
        private string subject;
        public void Explain()
        {
            Console.WriteLine("Explanation begins");
        }
    }
    class StudentAndTeacherTest
    {
        static void Main(string[] args)
        {
            Person p = new Person();
            p.Greet();

            Student s = new Student();
            s.SetAge(21);
            s.Greet();
            s.ShowAge();

            Teacher t = new Teacher();
            t.SetAge(30);
            t.Greet();
            t.Explain();
        }
    }
}
