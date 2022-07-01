using System;
using System.Threading.Tasks;

namespace TestAsyncAwait{
    public class MyTestAsyncAwait{

        public static async void Run(){
            
            // 1 và 2 chạy bất đồng bộ 
            Method1();
            await Method2();
            // nhưng có await ở 2 nên nó sẽ chờ 2 chạy xong mới chạy 3
            
            // Method4 chạy bất đồng bộ cùng với Method3            
            Method4();
            Method3();
        }

        public static void Method1()
        {
            // vẫn bất đồng bộ nhé mặc dù không có từ khóa async await
            Task.Run(() =>
            {
                for (int i = 0; i < 30; i++)
                {
                    Console.WriteLine($" Method 1 {i}");
                    // Do something
                    Task.Delay(100).Wait();
                }
            });
        }

        // bất đồng bộ chăc rồi  
        public static async Task Method2()
        {   
            await Task.Run(() =>
            {
                for (int i = 0; i < 100; i++)
                {
                    Console.WriteLine($" Method 2 {i}");
                    // Do something
                    Task.Delay(100).Wait();
                }
            });
        }

        //chắc chắn là tuần tự đồng bộ rồi
        public static void Method3()
        {
            for (int i = 0; i < 25; i++)
            {
                Console.WriteLine($" Method 3 {i}");
                // Do something
                Task.Delay(100).Wait();
            }
        }

        // tương tư như thằng 2
        public static async Task Method4()
        {   
            await Task.Run(() =>
            {
                for (int i = 0; i < 100; i++)
                {
                    Console.WriteLine($" Method 4 {i}");
                    // Do something
                    Task.Delay(100).Wait();
                }
            });
        }

    }
}