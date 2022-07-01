using System;
using System.Threading.Tasks;

namespace TestAsyncAwait
{
    class Program
    {
        static void Main(string[] args)
        {
            MyTestAsyncAwait.Run();

            // MyKnowbaseAsyncAwait myKnowbaseAsyncAwait = new MyKnowbaseAsyncAwait();
            // myKnowbaseAsyncAwait.Run();

            //myKnowbaseAsyncAwait.Run2Async();

            //myKnowbaseAsyncAwait.RunNotAsync();

            //MulitpleAsyncRequests.RunAsync()
            //        .Wait(); // wait đê bắt func async chạy đồng bộ 


            //MulitpleAsyncRequests m = new MulitpleAsyncRequests();
            //m.TestJoinStringPerformance();

            Console.WriteLine("Hello World!");
            Console.ReadKey();
        }
    }

    public class MyKnowbaseAsyncAwait {

        public async void Run()        
        {
            // gọi một hàm bất đồng bộ 
            // hàm này sẽ bắt đầu chạy ở từ đây 
            Task<int> rsTaskRunning = LongTimeRunningTaskAsync("LongTimeRunningTaskAsync");
            
            // thực hiện một công việc khác song song với với hàm bất đồng bộ kia 
            for (int i = 0; i < 10; i++)
            {
                Console.WriteLine($"\r Processing {i}");                    
                Task.Delay(100).Wait();
            }

            // await được dùng để đợi cho hàm bất động bộ kia trả về kết quả 
            int Total = await rsTaskRunning;
            
            //whatever
            Console.WriteLine($"\r Total: {Total}");
        }

        public async void Run2Async()
        {

            // gọi 2 hàm bất đồng bộ 
            Task<int> rsTaskRunning = LongTimeRunningTaskAsync("method 1 ",100);
            Task<int> rsTaskRunning2 = LongTimeRunningTaskAsync("method 2 ", 20);
            // cả 2 thằng cùng chạy 
            // tất nhiên là thằng nào gọi trước sẽ chạy trước rồi 

            
            
            // kể cả đảo thứ tự của (await) thì func nào được gọi trước vẫn chạy trước
            // thứ tự của await không ảnh hưởng đến tốc độ chạy của func
            int Total2 = await rsTaskRunning2;
            int Total = await rsTaskRunning;            
            //ở ví dụ trên thì 2 func cùng chạy song song
            // cho đến khi trả về hết kết qủa mới thực hiện tiếp các câu lệnh tiếp theo 

            // whatever
            Console.WriteLine($"\r Total: {Total} {Total2}");
        }

        
        public async void RunNotAsync()
        {
            // cái method này thì nó vẫn bất đồng bộ với tại điểm nó được gọi 
            // nhưng các func bên trong nó thì vẫn chạy đồng bộ  
            

            // gọi 2 hàm bất đồng bộ nhưng chạy đồng bộ 
            // không còn là bất đồng bộ nữa rồi 
            int Total = await LongTimeRunningTaskAsync("method 1 ",100);
            int Total2 = await  LongTimeRunningTaskAsync("method 2 ", 20);
            // tất nhiên là thằng nào gọi trước sẽ chạy trước rồi 
            
            // whatever
            Console.WriteLine($"\r Total: {Total} {Total2}");
        }

        Task<int> LongTimeRunningTaskAsync(string name){
            // demo một funcion bất đồng bộ 
            return Task.Run<int> (()=>{
                int total  = 0 ;
                for (int i = 0; i < 30; i++)
                {
                    Console.WriteLine($" {name} {i}");
                    total+=i;
                    Task.Delay(100).Wait();
                }
                return total;
            });
        }

        Task<int> LongTimeRunningTaskAsync(string name, int jobs){
            // demo một funcion bất đồng bộ 
            return Task.Run<int> (()=>{
                int total  = 0 ;
                for (int i = 0; i < jobs; i++)
                {
                    Console.WriteLine($" {name} {i}");
                    total+=i;
                    Task.Delay(100).Wait();
                }
                return total;
            });
        }
    }    
}
