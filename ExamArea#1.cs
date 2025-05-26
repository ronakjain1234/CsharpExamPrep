using System;

// Analogy
/*

Class is like a blueprint for a car. Just like a car blueprint defines properties like
number of wheels, engine type, and color, a class defines properties and behaviors that the object
have

*/


// A set of rules or promises that a class inheriting the interface has to follow
// Enable to have consistency among classes
public interface IEngine { void Start(); }

public interface IDisplay { void ShowInfo(); }

// Inheritance is the parent blueprint. A Car is a vehicle, but it might have more specific features 
// like Trunk Space

// Abstact - methods that a derived class must implement
// Abstracts allows memebers, have constructors, but interfaces can have multiple inheritance 
public abstract class Vehicle
{
    // They encapsulate access to class fields using get and set accessors
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
// Sealed Class - Prevents further derivation 
public sealed class Car : Vehicle, IEngine, IDisplay
{
    // Can only be assigned during declaration or in constructor
    private readonly string color;
    // Can only be set during object creation
    private string VIN { get; init; }

    // The member belongs to the type and static members can only access static data
    public static int Count { get; protected set; }
    // Static Constructor
    static Car() => Count = 0;
    // Constructor (calling parent constructor as well) - Initializes objects
    public Car(string brand, int year, string color, string VIN) : base(brand, year)
    {
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

        // An object is an instance of a class
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

 // Interface as return type and parameters
 public interface ITegnBar
 {
 	void Tegn();
 }

 public interface ISCalerBar
 {
 	void Scaler();
 }

 public class Firkant : ITegnBar, ISCalerBar
 {
 	public void Tegn() => Console.WriteLine("Drawing Firkant");
 	public void Scaler() => Console.WriteLine("Scaling Firkant");
 }

 public class Program
 {
 	public static void Main()
 	{
     	ITegnBar fig = GetTegnBar();
     	fig.Tegn();

     	AcceptScalerBar(new Firkant());
 	}

    // As paramter
    public static void AcceptScalerBar(ISCalerBar s)
    {
        s.Scaler();
    }

    // As return type
 	public static ITegnBar GetTegnBar()
    {
        return new Firkant();
    }
 }

