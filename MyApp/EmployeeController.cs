using System.Diagnostics;
using Microsoft.Extensions.Logging;
using MySqlConnector;
using Timer = System.Timers.Timer;

namespace MyApp;

public class EmployeeController
{
    public void Run()
    {
        while (true)
        {
            Console.WriteLine("Выберите действие");
            var action = Console.ReadLine();
            switch (action)
            {
                case "myApp 1":
                    CreateDB();
                    break;
                case "myApp 2":
                    AddEmployee();
                    break;
                case "myApp 3":
                    PrintDB();
                    break;
                case "myApp 4":
                    TextDB();
                    break;
                case "myApp 5":
                    PrintPartOfDB();
                    break;
            }
        }
    }

    private void CreateDB()
    {
        using (var db = new ApplicationContext())
        {
            Console.WriteLine("База данных создана");
        }
    }

    private void AddEmployee()
    {
        using (var db = new ApplicationContext())
        {
            var newEmployee = new Employee();

            while (true)
            {
                Console.WriteLine("Введите ФИО сотрудника");
                var fullName = Console.ReadLine();

                if (fullName == null)
                {
                    Console.WriteLine("Вы не ввели имя");
                }
                else
                {
                    newEmployee.FullName = fullName;
                    break;
                }
            }

            while (true)
            {
                Console.WriteLine("Введите дату рождения в формате 'год, месяц, число' через пробел");
                var dateInput = Console.ReadLine();
                try
                {
                    var dateOfBirth = DateOnly.Parse(dateInput);
                    if (DateTime.Now.Year - dateOfBirth.Year <= 16 || DateTime.Now.Year - dateOfBirth.Year >= 120)
                    {
                        throw new Exception();
                    }

                    newEmployee.DateOfBirth = dateOfBirth;
                    break;
                }
                catch
                {
                    Console.WriteLine("Вы ввели дату рождения некорректно");
                }
            }

            while (true)
            {
                Console.WriteLine("Введите ваш пол в формате 'male/female'");
                var gender = Console.ReadLine();
                if (gender.ToLower() == "male")
                {
                    newEmployee.Gender = Employee.Genders.Male;
                    break;
                }

                if (gender.ToLower() == "female")
                {
                    newEmployee.Gender = Employee.Genders.Female;
                    break;
                }
                else
                {
                    Console.WriteLine("Вы ввели неккореткно значение 'пол', попробуйте еще");
                }
            }

            db.Employees.Add(newEmployee);
            Console.WriteLine("Сотрудник добавлен");


            db.SaveChanges();
        }
    }

    private void PrintDB()
    {
        var employees = new List<Employee>();
        using (var db = new ApplicationContext())
        {
            employees = db.Employees.ToArray()
                .OrderBy(p => p.FullName)
                .ToList();
        }

        foreach (var employee in employees)
        {
            var age = employee.FindAge(employee.DateOfBirth);
            Console.WriteLine($"{employee.FullName}  {employee.DateOfBirth} {employee.Gender} {age}");
        }
    }

    private void TextDB()
    {
        var nameFemale = new[]
        {
            "Dasha ", "Masha ", "Katya ", "Olya ", "Vika ", "Kristina ", "Anna ", "Evgenia ", "Sasha ", "Olga ",
            "Yulia ", "Oksana "
        };
        var lastNameFemale = new[]
        {
            "Averkieva ", "Burlakova ", "Golikova ", "Mashkova ", "Stepanova ", "Panukaeva ", "Terekhova ", "Pekun ",
            "Sukhova ", "Dubrovina ", "Sokolova ", "Ponomarenko "
        };
        var fatherNameFemale = new[]
        {
            "Sergeevna", "Ivanovna", "Andreevna", "Nikolaevna", "Olegovna", "Antonovna", "Sergeevna", "Igorevna",
            "Leonidovna", "Victorovna", "Alexandrovna ", "Arturovna"
        };

        var lastNameMale = new[]
        {
            "Ivanov ", "Petrov ", "Kot ", "Orlov ", "Terekhov ", "Romanov ", "Sokolov ", "Muhich ", "Sherbak ",
            "Avdeev ", "Sidorov ", "Fomin "
        };
        var nameMale = new[]
        {
            "Max ", "Oleg ", "Roman ", "Artem ", "Sergei ", "Denis ", "Kolya ", "Andrei ", "Alexei ", "Sasha ",
            "Vasya ", "Mishel "
        };
        var fatherNameMale = new[]
        {
            "Sergeevich", "Maximovich", "Ivanovich", "Igorevich", "Sergeevich", "Nikolaevich", "Olegovich",
            "Denisovich", "Artemovich", "Petrovich", "Mihailovich", "Romanovich"
        };

        var familyF = new[]
        {
            "Fedorov ", "Fedotov ", "Fonarev ", "Fiziev ", "Fahrutdinov ", "Frolov ", "Filimonov ", "Filchenko ",
            "Funtikov ", "Fomov ", "Fiatov ", "Filatov "
        };
        var timer = new Stopwatch();
        timer.Start();
        using (var db = new ApplicationContext())
        {
            var count = 0;
            var random = new Random();
            Console.WriteLine("Заполняю таблицу данным, пожалуйста, подождите \n" +
                              "мне понадобится чуть больше 1 минуты для внесения 1000000 уникальных работников");
            while (count < 1000000)
            {
                var indexName = random.Next(0, 12);
                var indexLastName = random.Next(0, 12);
                var indexFatherName = random.Next(0, 12);
                var month = random.Next(1, 13);
                var day = random.Next(1, 29);
                var year = random.Next(1960, 2006);

                if (count % 2 == 0)
                {
                    var fullName = lastNameFemale[indexLastName] + nameFemale[indexName] +
                                   fatherNameFemale[indexFatherName];
                    var newEmployee = new Employee(fullName, new DateOnly(year, month, day), Employee.Genders.Female);
                    db.Employees.Add(newEmployee);
                }
                else
                {
                    var fullName = lastNameMale[indexLastName] + nameMale[indexName] +
                                   fatherNameMale[indexFatherName];
                    var newEmployee = new Employee(fullName, new DateOnly(year, month, day), Employee.Genders.Male);
                    db.Employees.Add(newEmployee);
                }

                count++;
            }

            count = 0;

            while (count < 100)
            {
                var indexName = random.Next(0, 12);
                var indexLastName = random.Next(0, 12);
                var indexFatherName = random.Next(0, 12);
                var month = random.Next(1, 13);
                var day = random.Next(1, 29);
                var year = random.Next(1940, 2006);

                var fullName = familyF[indexLastName] + nameMale[indexName] +
                               fatherNameMale[indexFatherName];
                var newEmployee = new Employee(fullName, new DateOnly(year, month, day), Employee.Genders.Male);
                db.Employees.Add(newEmployee);

                count++;
            }

            db.SaveChanges();
            timer.Stop();
            var path = "test.txt";
            using (var streamWriter = new StreamWriter(path, true))
            {
                streamWriter.WriteLine($"Время выполнения операции myApp 4 {timer.Elapsed.Seconds}");
            }
        }
    }

    private void PrintPartOfDB()
    {
        var stopwatch = new Stopwatch();
        stopwatch.Start();
        var employees = new List<Employee>();
        using (var db = new ApplicationContext())
        {
            employees = db.Employees.ToArray()
                .Where(x => x.Gender == Employee.Genders.Male && x.FullName.ToUpper().StartsWith("F"))
                .ToList();
        }

        foreach (var employee in employees)
        {
            var age = employee.FindAge(employee.DateOfBirth);
            Console.WriteLine($"{employee.FullName}  {employee.DateOfBirth} {employee.Gender} {age}");
        }


        stopwatch.Stop();
        var path = "test.txt";
        using (var streamWriter = new StreamWriter(path, true))
        {
            streamWriter.WriteLine($"Время выполнения операции myApp 5 {stopwatch.Elapsed.Milliseconds} милисекунд");
        }

        Console.WriteLine($"Время выполнения {stopwatch.Elapsed.Milliseconds} милисекунд");
    }
}