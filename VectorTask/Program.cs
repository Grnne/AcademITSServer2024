namespace VectorTask;

internal class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine("Немного примеров работы класса вектор:");
        Console.Write("Создадим новый пустой вектор размером 10 и проверим его размер: ");
        Vector vector1 = new Vector(10);
        Console.WriteLine(vector1.Size);
        Console.WriteLine();

        Console.Write("Поменяем третий компонент вектора на 3 и выведем его, а после весь вектор: ");
        vector1[2] = 3;
        Console.Write(vector1[2] + "; ");
        Console.WriteLine(vector1);
        Console.WriteLine();

        Console.Write("Инициализируем вектор через массив и отобразим его: ");
        double[] doubles = { 10, 11, 12, 13, 14 };
        Vector vector2 = new Vector(doubles);
        Console.WriteLine(vector2);
        Console.WriteLine($"Посмотрим длину вектора: {vector2.GetLength()}");
        Console.WriteLine();

        Console.WriteLine("Сделаем с этим вектором операцию сложения, скалярного произведения и вычитания.");
        vector2.Add(vector1);
        Console.WriteLine(vector2);
        vector2.MultiplyByScalar(3);
        Console.WriteLine(vector2);
        vector2.Subtract(vector1);
        Console.WriteLine(vector2);
        Console.WriteLine();

        Console.WriteLine("Инициализируем вектор через конструктор с указанием длины и массивом и поменяем последний член на 2");
        Vector vector3 = new Vector(12, doubles);
        vector3[11] = 2;
        Console.WriteLine(vector3);
        Console.WriteLine();

        Console.WriteLine("Проверим пару векторов на равенство");
        Vector vector4 = new Vector(vector3);
        Console.WriteLine(vector4.Equals(vector3));
        Console.WriteLine();

        Console.WriteLine("Проверим пару статических методов: сложение и скалярное произведение");
        Vector vector6 = Vector.GetSum(vector3, vector4);
        Console.Write(vector6 + ", ");
        Console.WriteLine(Vector.GetScalarProduct(vector1, vector2));
    }
}
