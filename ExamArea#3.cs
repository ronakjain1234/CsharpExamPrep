using System;
using System.Collections.Generic;

// Multiple functions called with one delegate
// Function pointers in C refer to raw memory addresses and no type check
// In contrast, .NET delegates are type-safe, secure, object-oriented references 
// to methods. Delegates encapsulate not only the method address but also the 
// target object and method signature, enabling managed, crash-safe function invocation.

delegate void NotifyUser(string name, int age);

class Program
{
    static void Greet(string name, int age)
    {
        Console.WriteLine($"Hello {name}, age {age}!");
    }

    static void Congratulate(string name, int age)
    {
        Console.WriteLine($"Congrats {name}, turning {age + 1} soon!");
    }

    static void Main()
    {
        NotifyUser n1 = Greet;
        NotifyUser n2 = Congratulate;

        NotifyUser combined = n1 + n2;

        combined.Invoke("Alice", 24);
    }
}
// Generic Delegate
public delegate void MyGenericDelegate<T>(T val);


// The alarm publishes an event when smoke is detected.

// Anyone (a subscriber) — fire station, sprinklers, emergency text system — can react 
// to that alarm without the alarm needing to know what those systems are.

// Custom EventArgs using EventHandler<T>
// Events in C# allow objects to notify other objects when something happens,
// where event data is passed through a class derived from EventArgs
// Built in generic delegate type for handling events with custom Event Arguments
public class TempEventArgs : EventArgs
{
    public int Temp { get; }
    public TempEventArgs(int t) => Temp = t;
}
public class Thermostat
{
    public event EventHandler<TempEventArgs> TempChanged;
    // Methods that typically invoke the delegate are kept private or protected
    public void ChangeTemp(int t) => TempChanged?.Invoke(this, new TempEventArgs(t));
}

// Extension Method
public static class IntExtensions
{
    public static bool IsEven(this int num) => num % 2 == 0;
}

// Indexer 
public class StringStore
{
    private List<string> data = new();
    public string this[int i] { get => data[i]; set => data.Insert(i, value); }
}

// Operator Overload + Conversion
public class Square
{
    public int Height { get; set; }
    public static implicit operator Rectangle(Square s) => new Rectangle { Height = s.Height };
}
public class Rectangle
{
    public int Height { get; set; }
    public static explicit operator Square(Rectangle r) => new Square { Height = r.Height };
}

class Program
{
    static void Main()
    {
        var thermo = new Thermostat();
        thermo.TempChanged += (s, e) => Console.WriteLine("Temp: " + e.Temp); // Response by subscribers
        thermo.ChangeTemp(30); // Alarm triggered - publishes the event
        // The delegate is like the alarm
        
        // Some good practices include unsubscribing  and checking for null

        // Extension + Indexer
        int val = 10;
        Console.WriteLine($"{val} is even? {val.IsEven()}");

        var store = new StringStore();
        store[0] = "Hello";
        Console.WriteLine(store[0]);

        // Generic Delegate
        MyGenericDelegate<string> del = msg => Console.WriteLine(msg.ToUpper());
        del("generic world");

        // Conversion & Operator
        Square sq = new() { Height = 5 };
        Rectangle rect = sq; // Implicit - conversion is safe and no casting needed
        Square sq2 = (Square)new Rectangle { Height = 10 }; // Explicit - data might be lost and cast is needed
        Console.WriteLine($"Square Height: {sq2.Height}");


    }
}



// Async Example of delegate
class Program
{
    // Define delegate with out parameter
    delegate string AsyncOp(int milliseconds, out int threadId);

    // Long-running operation
    static string LongTask(int ms, out int threadId)
    {
        Thread.Sleep(ms); // Simulate delay
        threadId = Thread.CurrentThread.ManagedThreadId;
        return $"Finished sleeping for {ms}ms on thread {threadId}";
    }

    static void Main()
    {
        // Create delegate instance
        AsyncOp op = LongTask;

        int tid; // Will be assigned in EndInvoke
        Console.WriteLine("Calling BeginInvoke...");

        // Start async call
        IAsyncResult result = op.BeginInvoke(2000, out tid, null, null);

        Console.WriteLine("Main thread continues working...");

        // Do other work here...
        Thread.Sleep(500);
        Console.WriteLine("Main thread is still alive.");

        // Get result and threadId. Important to call so you get results and any exceptions thrown insides the delegate
        string output = op.EndInvoke(out tid, result);
        Console.WriteLine(output);
    }
}


// Callback example - if extra time 
// Think of Notifier as an alarm system. It lets you register what to do when an alarm goes off — you can 
// plug in different "reactions" without modifying the alarm code.
public class Notifier
{
    public delegate void Notify(string message);

    public void RegisterCallback(Notify callback)
    {
        callback("Callback called!");
    }
}

 public class Client
 {
 	public static void StaticCallback(string msg) => Console.WriteLine("Static: " + msg);
 	public void InstanceCallback(string msg) => Console.WriteLine("Instance: " + msg);
 }

 // Usage
 var notifier = new Notifier();
 var client = new Client();

 notifier.RegisterCallback(client.StaticCallback);
 notifier.RegisterCallback(client.InstanceCallback);