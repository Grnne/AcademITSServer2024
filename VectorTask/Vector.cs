using System.Text;

namespace VectorTask;

public class Vector
{
    private double[] _components;

    public double this[int index]
    {
        get => _components[index];
        set => _components[index] = value;
    }

    public int Size => _components.Length;

    public Vector(int size)
    {
        CheckSize(size);

        _components = new double[size];
    }

    public Vector(Vector vector) => _components = (double[])vector._components.Clone();

    public Vector(int size, Vector vector)
    {
        CheckSize(size);

        _components = new double[size];
        Array.Copy(vector._components, _components, Math.Min(size, vector.Size));
    }

    public Vector(double[] components)
    {
        CheckSize(components.Length);

        _components = new double[components.Length];
        components.CopyTo(_components, 0);
    }

    public Vector(int size, double[] components)
    {
        CheckSize(size);

        _components = new double[size];
        Array.Copy(components, _components, Math.Min(size, components.Length));
    }

    public static Vector GetSum(Vector vector1, Vector vector2)
    {
        Vector result = new Vector(vector1);
        result.Add(vector2);
        return result;
    }

    public static Vector GetDifference(Vector vector1, Vector vector2)
    {
        Vector result = new Vector(vector1);
        result.Subtract(vector2);
        return result;
    }

    public double GetLength()
    {
        double componentsSquaresSum = 0;

        foreach (double component in _components)
        {
            componentsSquaresSum += component * component;
        }

        return Math.Sqrt(componentsSquaresSum);
    }

    public static double GetScalarProduct(Vector vector1, Vector vector2)
    {
        double result = 0;
        int minVectorSize = Math.Min(vector1._components.Length, vector2._components.Length);

        for (int i = 0; i < minVectorSize; i++)
        {
            result += vector1._components[i] * vector2._components[i];
        }

        return result;
    }

    public void Add(Vector vector)
    {
        if (vector._components.Length > _components.Length)
        {
            Array.Resize(ref _components, vector._components.Length);
        }

        for (int i = 0; i < vector._components.Length; i++)
        {
            _components[i] += vector._components[i];
        }
    }

    public void Subtract(Vector vector)
    {
        if (vector._components.Length > _components.Length)
        {
            Array.Resize(ref _components, vector._components.Length);
        }

        for (int i = 0; i < vector._components.Length; i++)
        {
            _components[i] -= vector._components[i];
        }
    }

    public void MultiplyByScalar(double number)
    {
        for (int i = 0; i < _components.Length; i++)
        {
            _components[i] *= number;
        }
    }

    public void Reverse()
    {
        MultiplyByScalar(-1);
    }

    private static void CheckSize(int size)
    {
        if (size <= 0)
        {
            throw new ArgumentOutOfRangeException(nameof(size), $"Size must be greater than zero. Size: {size}");
        }
    }

    public override string ToString()
    {
        StringBuilder stringBuilder = new();

        stringBuilder.Append('{');
        stringBuilder.AppendJoin(", ", _components);
        stringBuilder.Append('}');

        return stringBuilder.ToString();
    }

    public override bool Equals(object? obj)
    {
        if (ReferenceEquals(obj, this))
        {
            return true;
        }

        if (obj is null || obj.GetType() != GetType())
        {
            return false;
        }

        Vector vector = (Vector)obj;

        if (_components.Length != vector._components.Length)
        {
            return false;
        }

        for (int i = 0; i < _components.Length; i++)
        {
            if (_components[i] != vector._components[i])
            {
                return false;
            }
        }

        return true;
    }

    public override int GetHashCode()
    {
        int prime = 23;
        int hash = 1;

        foreach (double component in _components)
        {
            hash = prime * hash + component.GetHashCode();
        }

        return hash;
    }
}