using System;
using System.Threading;
using System.Threading.Tasks;
using System.Reflection;

// Object Contexts is a runtime feature used to manage behavior into certain objects when 
// they are accessed
// Synchronization is one example where onlyone thread can enter the class at a time
[Synchronization]
public class Goat : ContextBoundObject
{
    public Goat()
    {
        Context ctx = Thread.CurrentContext;
        Console.WriteLine("Goat in Context: " + ctx.ContextID);
    }
}

/*
"Imagine a restaurant kitchen. The entire kitchen is like a process — it has its own space, equipment, and ingredients.

Now, the chefs inside that kitchen are like threads. Each chef can work on a different task: one is chopping vegetables, 
another is grilling meat, and a third is plating dishes.

Even though they all work independently, they share the same space and resources, just like threads in a process share memory.

Sometimes, chefs need to coordinate — like not grabbing the same pan at once. That’s where synchronization comes in, 
making sure the work runs smoothly without collisions.
*/

class Program {
    static async Task Main() {
        // AppDomain - A logical container for code. It provides isolation, not concurrency
        // Benefit is that their is crash isolation and dynamic loading/unloading(freeing up memory)
        AppDomain domain = AppDomain.CreateDomain("DemoDomain");
        domain.AssemlyLoad += (sender, args) => {
            console.WriteLine("Assembly Loaded: " + args.LoadAssembly.FullName);
        };

        Assembly asm = domain.Load("RandomFile");
        // After loading, you can inspect, create objects, and invoke methods
        domain.DomainUnload += (sender, args) => {
            console.WriteLine("Domain unloaded");
        }

        AppDomain.Unload(domain);



        // Threads
        // Threads enable concurrent executions, where multiple tasks can be managed withing the same process
        // Each thread maintains its own storage area for local data

        // Thread Synchronization
        // Lock - Simiplifies mutual exclusion
        // Semaphore - Controls how many threads can access a shared resource at the same time 
        private object threadLock = new object();
    public void CriticalSection() {
        lock (threadLock) {
            // Crtical Code
        }
    }

    // Atomic Operations - prevents race conditions as it blocks other threads from interfering during that time
    int count = 0;
    Interlocked.Increment(ref count);
        Interlocked.Exchange(ref count, 10);
        int result = Interlocked.CompareExchange(ref count, 15, 10);

    // Foreground vs Background Threads

    // Thread Suspension 
    Thread.Sleep(5000); // Pauses Execution
        Thread.Suspend(); // Bad because it can lead to deadlocks

        // TPL
        Task t1 = Task.Run(MyWork);
    Task t2 = Task.Run(() => MyWorkWithParam("Hello"));
    Task.WaitAll(t1, t2);
        
        // Parallel.For - Cool example is bring it to gradiance and how we used it
        Parallel.For(0, 10, i => {
 	        Console.WriteLine($"Index {i} processed on thread {Thread.CurrentThread.ManagedThreadId}");
        });

        // ThreadPool - helps run small tasks in the background without the cost of creating and destroying threads manually
        var data = new WorkItemData {nameof = "Ronak", count = 5};
        ThreadPool.QueueUserWorkItem(DoWorkWithParams, data);
        
        // Timer Callbacks
        timer - new Timer(PrintGreeting, param, 0, 1000) // Tick every second

        // Async and Await
        // When an await is encountered, execution of the method is paused until awaited task completes, without blocking 
        // the thread
        private async void Button_Click(object sender, EventArgs e) {
    string result = await DoWorkAsync();
    Console.WriteLine(result);
}

      
    }

    static async Task<string> DoWorkAsync() {
    await Task.Delay(1000);
    return "Async task complete!";
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
