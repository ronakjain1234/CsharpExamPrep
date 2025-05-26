using System;
using System.Reflection;

// EnRegner.dll library
// Assemblies is a complied output of your C# code
// DLLs are examples of Assemblies. Primary used for creating reusable libraries
// of code that can be shared acorss different applications. Language independent
// They contain CIL code, metadata(info about types,members and attributes), and manifest (identity card including version, assembly name and file list)
// They have a self-describing nature
namespace EnRegner {
    public class EnRegner {
        public double Plus(double a, double b) => a + b;
    }
}

var calc = new EnRegner.EnRegner();
double result = calc.Plus(5, 7);


// Custom Attribute 
// Attributes in C# provide extra information to the compiler about how a piece of code should be handled
// [Obsolete] - Mark code as outdated
// Inherit from Attribute base class for consistent processing
[AttributeUsage(AttributeTargets.Class)]
public class ClassDescriptionAttribute : Attribute {
    public string Description { get; }
    public ClassDescriptionAttribute(string desc) => Description = desc;
}

// Class in "library"
[ClassDescription("Simple Calculator Class")]
public class Calculator {
    public double Add(double a, double b) => a + b;
    public double Subtract(double a, double b) => a - b;
}

public class Program {
    public static void Main() {
        // Early binding- Known at compile time - leading to better performance
        Calculator calc = new Calculator();
        Console.WriteLine("Early Binding Add: " + calc.Add(10, 5));

        // Late binding - Type and Members are resolved at runtime, providing flexibility but it is slower
        // The system.Type in combination with system.reflection namespace enable
        // developers to inspect fields, properties, and methods of a type

        Assembly assembly = assembly.Load("CalcAssembly"); // Assuming Calculator has a different assembly
        Type type = assembly.GetType("CalculatorNS.Calculator")
        object obj = Activator.CreateInstance(type);
        MethodInfo method = type.GetMethod("Add"); // Will be array if multiple methods
        double result = (double)method.Invoke(obj, new object[] { 20.0, 15.0 });
        Console.WriteLine("Late Binding Add: " + result);
        // You can also do it with Fields, Properties, Interfaces, access modifiers and hierarchy


        // Static Binding - when the type is known at compile time
        Type t = typeof(Calculator);
        ClassDescriptionAttribute attr = (ClassDescriptionAttribute)Attribute.GetCustomAttribute(t, typeof(ClassDescriptionAttribute));
        Console.WriteLine(attr.Description);

        // Dynamic Binding - load from assemblies at runtime
        Assembly a = Assembly.Load("MyLibrary");
        Type t = a.GetType("MyLibrary.Calculator");
        Type attrType = a.GetType("MyLibrary.ClassDescriptionAttribute");
        object[] attrs = t.GetCustomAttributes(attrType, false); // only gets from that attribute class; flase means if you look at base class
        PropertyInfo descProp = attrType.GetProperty("Description");
        Console.WriteLine(descProp.GetValue(attrs[0]));
    }
}
