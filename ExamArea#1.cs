using System;

public interface IEngine { void Start(); }

public interface IDisplay { void ShowInfo(); }

public abstract class Vehicle
{
    protected string Brand { get; init; }
    public int Year { get; set; }
    // Abstract Method
    public abstract void Drive();
    public Vehicle(string brand, int year)
    {
        Brand = brand;
        Year = year;
    }
}
// Class - Can make it partial so you can have it across multiple files
public sealed class Car : Vehicle, IEngine, IDisplay {
    private readonly string color;
    private string VIN { get; init; }
    public static int Count { get; private set; }
    // Static Constructor
    static Car() => Count = 0;
    // Constructor (calling parent constructor as well)
    public Car(string brand, int year, string color, string VIN) : base(brand, year) {
        this.VIN = VIN; // This keyword 
        this.color = color;
        Count++;
    }

    public override void Drive() => Console.WriteLine($"{Brand} ({Year}) is driving.");

    public void Start() => Console.WriteLine("Engine started.");

    public void ShowInfo() => Console.WriteLine($"VIN: {Reg.VIN}, Count: {Count}");
}

public class Program {
    public static void Main() {

        Car myCar = new Car("Tesla", 2024, "orange", "X12345") {
            Year = 2025 // using object initializer
        };

        myCar.Start();              // Interface method
        myCar.Drive();              // Overridden method
        myCar.ShowInfo();           // Another interface method
        // string myBrand = myCar.Brand;    -- Not Allowed



        Console.WriteLine($"Total Cars: {Car.Count}");

        Vehicle myVehicle = new Vehicle("Mercedes", 2025); // assuming that Vehicle is not an Abstract Class
        myCar = myVehicle as Car; // This will cause an error

        if (myCar is Vehicle) {
            Console.WriteLine("Cool"); // Will reach here
        }

        IDisplay display = myCar as IDisplay;
        display?.ShowInfo();

        // Example with tuples
        (bool success, _) = (true, 42);  // Discard the second item
    }
}

// Member Shadowing Example
public class Parent {
    public void Display() => Console.WriteLine("Parent Display");
}

 public class Child : Parent{
 	public new void Display() => Console.WriteLine("Child Display");
 }
