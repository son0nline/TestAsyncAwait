using System;
using System.Threading.Tasks;

namespace TestAsyncAwait{
    
    // WaitAll will block current thread and wait for all task run completed
    public void TestWaitAll()
    {
        Console.WriteLine("TestWaitAll");
        Task task1 = new Task(() =>
        {
            for (int i = 0; i < 5; i++)
            {
                Console.WriteLine("Task 1 - iteration {0}", i);
                Task.Delay(1000);
            }
            Console.WriteLine("Task 1 complete");
        });

        // can create new task by this way and don't need to call task.Start()
        Task task2 = Task.Run(() =>
        {
            Console.WriteLine("Task 2 complete");
        });
        task1.Start();
        //task2.Start();
        Console.WriteLine("Waiting for tasks to complete.");
        Task.WaitAll(task1, task2);
        Console.WriteLine("Tasks Completed.");
        Console.ReadLine();
    }

    // WhenAll create a new task and that task only complete when all task complete
    // WhenAll don't block thread
    public void TestWhenAll()
    {
        Console.WriteLine("TestWhenAll");
        Task task1 = new Task(() =>
        {
            for (int i = 0; i < 5; i++)
            {
                Console.WriteLine("Task 1 - iteration {0}", i);
                Task.Delay(1000);
            }
            Console.WriteLine("Task 1 complete");
        });
        Task task2 = new Task(() =>
        {
            Console.WriteLine("Task 2 complete");
        });
        task1.Start();
        task2.Start();
        Console.WriteLine("Waiting for tasks to complete.");
        Task.WhenAll(task1, task2);
        Console.WriteLine("Tasks Completed.");
        Console.ReadLine();
    }
}