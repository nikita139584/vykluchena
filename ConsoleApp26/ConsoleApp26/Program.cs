
class StudentManagementException : Exception
{
    public StudentManagementException(string message) : base(message) { }
}

class InvalidGradeException : StudentManagementException
{
    public InvalidGradeException(string message) : base(message) { }
}

class StudentNotFoundException : StudentManagementException
{
    public StudentNotFoundException(string message) : base(message) { }
}

class InvalidStudentDataException : StudentManagementException
{
    public InvalidStudentDataException(string message) : base(message) { }
}


class GroupManagementException : Exception
{
    public GroupManagementException(string message) : base(message) { }
}

class GroupFullException : GroupManagementException
{
    public GroupFullException(string message) : base(message) { }
}

class InvalidGroupDataException : GroupManagementException
{
    public InvalidGroupDataException(string message) : base(message) { }
}

class TransferFailedException : GroupManagementException
{
    public TransferFailedException(string message) : base(message) { }
}
class Student
{
    private string lastName = "Stashko";
    private string Name = "Nikita";
    private string patronymic = "Sehrijovich";
    private int dateOfBirth = 28092010;
    private string homeAddress = "Sadovaja 3";
    private long phoneNumber = 380123456789;

    private int[] credits;
    private int[] coursework;
    private int[] exams;

    public Student(int creditsCount = 3, int courseworkCount = 3, int examsCount = 3)
    {
        credits = new int[creditsCount];
        coursework = new int[courseworkCount];
        exams = new int[examsCount];
    }

    private void ValidateMarks(int[] marks)
    {
        foreach (var m in marks)
            if (m < 0 || m > 100)
                throw new InvalidGradeException("Оцінка повинна бути від 0 до 100!");
    }

    public void SetCredits(int[] credits)
    {
        ValidateMarks(credits);
        this.credits = credits;
    }
    public int[] GetCredits()
    {
        return credits;
    }


    public void SetCoursework(int[] coursework)
    {
        ValidateMarks(coursework);
        this.coursework = coursework;
    }
    public int[] GetCoursework()
    {
        return coursework;
    }

    public void SetExams(int[] exams)
    {
        ValidateMarks(exams);
        this.exams = exams;
    }
    public int[] GetExams()
    {
        return exams;
    }

    public void SetName(string Name)
    {
        if (string.IsNullOrWhiteSpace(Name))
            throw new InvalidStudentDataException("Ім’я не може бути пустим!");
        this.Name = Name;
    }
    public string GetName()
    {
        return Name;
    }

    public void SetLastName(string lastName)
    {
        if (string.IsNullOrWhiteSpace(lastName))
            throw new InvalidStudentDataException("Прізвище не може бути пустим!");
        this.lastName = lastName;
    }
    public string GetLastName()
    {
        return lastName;
    }


    public void SetPatronymic(string patronymic)
    {
        if (string.IsNullOrWhiteSpace(patronymic))
            throw new InvalidStudentDataException("По батькові не може бути пустим!");
        this.patronymic = patronymic;
    }
    public string GetPatronymic()
    {
        return patronymic;
    }

    public void SetDateOfBirth(int dateOfBirth)
    {
        if (dateOfBirth < 19000101 || dateOfBirth > 20250101)
            throw new InvalidStudentDataException("Невалідна дата народження!");
        this.dateOfBirth = dateOfBirth;
    }
    public int GetDateOfBirth()
    {
        return dateOfBirth;
    }

    public void SetHomeAddress(string homeAddress)
    {
        if (string.IsNullOrWhiteSpace(homeAddress))
            throw new InvalidStudentDataException("Адреса не може бути пустою!");
        this.homeAddress = homeAddress;
    }
    public string GetHomeAddress()
    {
        return homeAddress;
    }


    public void SetPhoneNumber(long phoneNumber)
    {
        if (phoneNumber.ToString().Length < 10)
            throw new InvalidStudentDataException("Невалідний номер телефону!");
        this.phoneNumber = phoneNumber;
    }
    public long GetPhoneNumber()
    {
        return phoneNumber;
    }

    public static void Print(in Student student)
    {

        Console.OutputEncoding = System.Text.Encoding.UTF8;
        Console.WriteLine(student.GetName());
        Console.WriteLine(student.GetLastName());
        Console.WriteLine(student.GetPatronymic());
        Console.WriteLine(student.GetDateOfBirth());
        Console.WriteLine(student.GetHomeAddress());
        Console.WriteLine(student.GetPhoneNumber());

        Console.WriteLine("\nЗаліки:");
        foreach (int mark in student.GetCredits())
            Console.Write(mark + " ");

        Console.WriteLine("\nКурсові:");
        foreach (int mark in student.GetCoursework())
            Console.Write(mark + " ");

        Console.WriteLine("\nЕкзамени:");
        foreach (int mark in student.GetExams())
            Console.Write(mark + " ");



    }

}
class Group
{
    private List<Student> students;
    private int capacity;
    private string groupName;
    private string specialization;
    private int courseNumber;

    public Group(int capacity)
    {
        if (capacity <= 0)
            throw new InvalidGroupDataException("Розмір групи має бути > 0!");

        this.capacity = capacity;
        students = new List<Student>();
    }

    public void AddStudent(Student st)
    {
        if (students.Count >= capacity)
            throw new GroupFullException("Неможливо додати! Група переповнена!");

        students.Add(st); // ✔ правильно
    }

    public Student GetStudent(string name)
    {
        foreach (var s in students)
            if (s != null && s.GetName() == name)
                return s;

        throw new StudentNotFoundException("Студента не знайдено!");
    }

    public void TransferStudent(Group toGroup, Student student)
    {
        if (student == null)
            throw new TransferFailedException("Студент = null! Переведення неможливе.");

        try
        {
            toGroup.AddStudent(student);
        }
        catch (Exception ex)
        {
            throw new TransferFailedException("Не вдалося перевести: " + ex.Message);
        }
    }
    public void ShowAllStudents()
    {
        Console.OutputEncoding = System.Text.Encoding.UTF8;

        Console.WriteLine($"\nГрупа: {groupName}");
        Console.WriteLine($"Спеціалізація: {specialization}");
        Console.WriteLine($"Курс: {courseNumber}\n");

        Console.WriteLine("Студенти групи:");
        foreach (Student st in students)
        {
            Console.WriteLine($"{st.GetLastName()} {st.GetName()}");
        }

        Console.WriteLine();
    }
}

class Program
{
    static void Main()
    {
        try
        {
            Student s = new Student();
            s.SetCredits(new int[] { 90, 85, 100 });
            s.SetCoursework(new int[] { 80, 95, 70 });
            s.SetExams(new int[] { 88, 92, 75 });

            Student.Print(s);

            Group g1 = new Group(1);
            g1.AddStudent(s);

            Group g2 = new Group(1);
            g1.TransferStudent(g2, s);
        }
        catch (StudentManagementException ex)
        {
            Console.WriteLine("ПОМИЛКА СТУДЕНТА: " + ex.Message);
        }
        catch (GroupManagementException ex)
        {
            Console.WriteLine("ПОМИЛКА ГРУПИ: " + ex.Message);
        }
    }
}
