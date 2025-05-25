// EF Core enables developers to interact with databases with .NET objects instead of writing raw SQL queries. 
// It is a translator between your C# classes and the database tables
// Cros Platform and Lightweight/modular

// Comparison - EF6 is lazy loading by default while EF Core requires explicit configuration
// Batch Operations - combine multiple data base commands in one trip
// Modular and supports multiple database providers

// Steps 
// 1. Install NuGet Packages

// Defining the Db
// Responsible for interacting with the database using EF Core
public class AppDbContext : DbContext
{
    public DbSet<Customer> Customers { get; set; }
    public DbSet<Order> Orders { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer("your_connection_string_here");
    }
}

// Defining Tables 
// Each class refers to a specfic table 
// Each property maps to a column; uses convetion to infer mapping, but you can customize them
public class Customer {
    public int CustomerId { get; set; }
    public string Name { get; set; }
    public string Email { get; set; }
}

public class Order {
    public int OrderId { get; set; }
    public int CustomerId { get; set; }
    public DateTime OrderDate { get; set; }
}

// Usage
var context = new AppDbContext();



// Add
var customer = new Customer { Name = "Bob", Email = "bob@example.com" };
context.Customers.Add(customer);
context.SaveChanges();

// Update
var customer = context.Customers.First(c => c.Id == 1);
customer.Email = "newemail@example.com";
context.SaveChanges();

// Delete
 var customer = context.Customers.Find(1);
 context.Customers.Remove(customer);
 context.SaveChanges();

// Read with LINQ
var orders = context.Orders.Where(o => o.OrderDate >= DateTime.Today.AddDays(-7)).ToList();

// Repository Pattern

// A checklist of special tools for inventory
public interface IInventoryRepo
{
    Inventory GetByName(string name);
}


// A generic class that provides common database operations for all tables
 public class BaseRepo<T> where T : class
 {
 	protected readonly AppDbContext context; // Declares a varible for DB
 	public BaseRepo(AppDbContext ctx) => context = ctx; // Assigns an already exiting db to a variable
 	public void Add(T entity) => context.Set<T>().Add(entity);
 	public void Save() => context.SaveChanges();
 }


// A usage of the generic class and the tools
 public class InventoryRepo : BaseRepo<Inventory>, IInventoryRepo
 {
 	public InventoryRepo(AppDbContext ctx) : base(ctx) { }

 	public Inventory GetByName(string name)
 	{
     	return context.Inventories.FirstOrDefault(i => i.Name.Contains(name));
 	}
 }

 // Will have to base a datbase when creating an instance of these classes
 // If you use the same database, then they will affect the same database

