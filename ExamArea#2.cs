using System;
using System.Collections.Generic;

// 1. Generic Interface
public interface IBinaryOperations<T> {
    T Add(T a, T b);
    T Subtract(T a, T b);
}

// 2. Implementation with int
public class IntMath : IBinaryOperations<int> {
    public int Add(int a, int b) => a + b;
    public int Subtract(int a, int b) => a - b;
}

// 3. Custom Generic Struct
public struct Point<T> {
    public T X { get; set; }
    public T Y { get; set; }
    public override string ToString() => $"[{X}, {Y}]";
}

// 4. Custom Exception
public class PointException : ApplicationException {
    public override string Message => "Invalid point coordinates!";
}

// 5. Program Entry
class Program {
    static void Swap<T>(ref T a, ref T b) {
        T temp = a; a = b; b = temp;
    }

    static void Main() {
        try {
            // Using List<T>
            List<Point<int>> points = new List<Point<int>> {
                new Point<int> { X = 1, Y = 2 },
                new Point<int> { X = 3, Y = 4 }
            };

            // Using generic interface
            IBinaryOperations<int> ops = new IntMath();
            int sum = ops.Add(5, 10);

            // Swap generic method
            Point<int> p1 = points[0], p2 = points[1];
            Swap(ref p1, ref p2); // By passing by references, any changes made to that param will be reflected outside the method
            Console.WriteLine($"Swapped Points: {p1}, {p2}");

            // Trigger custom exception
            if (sum > 10)
                throw new PointException();
        // Can have many catches, but most specific ones first
        } catch (PointException e) when (DateTime.Now.DayOfWeek != DayOfWeek.Friday) {
            Console.WriteLine("Filtered Custom Exception: " + e.Message);
        } catch (Exception e) {
            Console.WriteLine($"Error: {e.Message}");
        } finally {
            Console.WriteLine("Execution finished.");
        }
    }
}
