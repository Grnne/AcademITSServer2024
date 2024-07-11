namespace ExcelTask;

public class Person(string FirstName, string LastName, int Age, string PhoneNumber)
{
    public string FirstName { get; set; } = FirstName;

    public string LastName { get; set; } = LastName;

    public int Age { get; set; } = Age;

    public string PhoneNumber { get; set; } = PhoneNumber;
}