using System;
using System.Collections.Generic;
using System.Linq;

// Use var to tell compiler to figure out the type
//In LINQ, var is used because queries often return complex or anonymous types whose exact names are 
//long, unreadable, or unavailable.
public class Car
{
    public string Brand { get; set; }
    public string Color { get; set; }
    public int Speed { get; set; }
    public int Year { get; set; }

    public override string ToString() =>
        $"{Year} {Brand} ({Color}) - {Speed} km/h";
}

public class Program
{
    public static void Main()
    {
        List<Car> cars = new List<Car>
        {
            new Car { Brand = "BMW", Color = "Red", Speed = 250, Year = 2021 },
            new Car { Brand = "Audi", Color = "Black", Speed = 220, Year = 2020 },
            new Car { Brand = "Tesla", Color = "White", Speed = 240, Year = 2022 },
            new Car { Brand = "Toyota", Color = "Blue", Speed = 180, Year = 2019 },
            new Car { Brand = "BMW", Color = "Red", Speed = 200, Year = 2023 }
        };

        // 1. Filter (Where) - only red cars
        // IEnumerable<Car>
        var redCars = cars.Where(c => c.Color == "Red");

        // 2. Projection (Select) - anonymous type
        // IEnumerable<AnonymousType>
        // IEnumerable<{ string Brand, int Speed }>
        var summaries = redCars.Select(c => new { c.Brand, c.Speed });

        // 3. Sorting (OrderBy, ThenBy)
        // IOrderedEnumerable<Car>
        var sorted = cars.OrderBy(c => c.Brand).ThenByDescending(c => c.Speed);

        // 4. Aggregation
        var averageSpeed = cars.Average(c => c.Speed);
        var maxSpeed = cars.Max(c => c.Speed);

        // 5. Grouping
        // IEnumerable<IGrouping<string, Car>>
        var groupedByBrand = cars.GroupBy(c => c.Brand);

        // 6. Set Operations
        // IEnumerable<Car>
        var fastCars = cars.Where(c => c.Speed > 200);
        var bmwCars = cars.Where(c => c.Brand == "BMW");
        var fastBmw = fastCars.Intersect(bmwCars);

        // 7. Immediate Execution
        // List <Car>
        var topSpeeds = cars.Where(c => c.Speed > 200).ToList();

        // 8. Func<> as filter
        // IEnumerable<Car>
        Func<Car, bool> isRecent = c => c.Year >= 2021;
        var recentCars = cars.Where(isRecent);

    }
}
