using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sqlite2403
{
    class Employee
    {

        /*
 * CREATE TABLE "COMPANY" (
"ID"	INTEGER NOT NULL,
"NAME"	TEXT NOT NULL UNIQUE,
"AGE"	INT NOT NULL,
"ADDRESS"	CHAR(50),
"SALARY"	REAL,
PRIMARY KEY("ID")
*/
        public Int64 Id { get; set; }
        public string Name { get; set; }
        public int Age { get; set; }
        public string Address { get; set; }
        public double Salary { get; set; }

        public override string ToString()
        {
            return $"Employee {Id} {Name} {Age} {Address} {Salary}";
        }
    }
    class Program
    {
        static void Main(string[] args)
        {
            
            // creating conenction to the Sqlite database
            SQLiteConnection  connection = new SQLiteConnection($"Data Source = c:\\itay\\start.db; Version=3;");

            List<Employee> employees = new List<Employee>();

            connection.Open();

            using (SQLiteCommand cmd = new SQLiteCommand("SELECT * FROM COMPANY", connection))
            {

                using (SQLiteDataReader reader = cmd.ExecuteReader())
                {

                    while (reader.Read() == true)
                    {
                        Employee e = new Employee
                        {
                            Id = (Int64)reader["ID"],
                            Name = (string)reader["NAME"],
                            Age = (int)reader["AGE"],
                            Address = (string)reader["ADDRESS"],
                            Salary = (double)reader["SALARY"]
                        };

                        employees.Add(e);

                        Console.WriteLine($" {reader["ID"]} {reader["NAME"]} {reader["AGE"]} {reader["ADDRESS"]}"
                            + $" {reader["SALARY"]}");
                    }
                 
                }
            }

            connection.Close();

            foreach(Employee s in employees)
            {
                if (s.Age > 20 && s.Age < 26)
                {
                    // ..... old style
                }
            }

            // ......... now new style
            // (1) SQL format
            List<Employee> resultLinqSqlFormat = (from s in employees
                                       where s.Age > 20 && s.Age < 26
                                       orderby s.Age
                                       select new Employee { Name = s.Name, Id = s.Id }).ToList();

            // (2) C# format
            List<Employee> result = employees.Where(e => e.Age > 20 && e.Age < 26).ToList();

            Console.WriteLine();


        }
    }
}
