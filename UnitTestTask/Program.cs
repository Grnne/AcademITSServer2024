namespace FindPreviousSqrt;

public class Program
{   
    static void Main(string[] args)
    {
        Console.WriteLine("Hello, World!");
    }

    public static int MySqrt(int x)
    {
        switch (x)
        {
            case 0:
                return 0;
            case 1:
                return 1;
        }

        var left = 1;
        var right = x / 2;

        while (left <= right)
        {
            var mid = left + (right - left) / 2;
            var sqrt = x / mid;

            if (sqrt == mid)
            {
                return mid;
            }

            if (sqrt < mid)
            {
                right = mid - 1;
            }
            else
            {
                left = mid + 1;
            }
        }

        return right;
    }
}