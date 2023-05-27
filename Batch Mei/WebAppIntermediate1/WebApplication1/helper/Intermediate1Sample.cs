using System.Diagnostics;

namespace WebApplication1.helper {
    public class Intermediate1Sample {
        public string SampleStringPublic { get; set; }  
        public void SamplePublic() {

        }

        private string SampleStringPrivate { get; set; }
        private void SamplePPrivate() {

        }

        public void SampleProses(string processName, int count) {
            Console.WriteLine($"Proses dimulai {processName} pada {DateTime.Now}");

            var sw = Stopwatch.StartNew();
            Thread.Sleep(count * 1000);
            sw.Stop();

            Console.WriteLine($"Proses {processName} selesai, dengan durasi {sw.ElapsedMilliseconds} pada {DateTime.Now} ");
        }

        public async Task SampleProsesAsync(string processName, int count) {
            Console.WriteLine($"Proses dimulai {processName} pada {DateTime.Now}");

            var sw = Stopwatch.StartNew();
            await Task.Delay(count * 1000);
            sw.Stop();

            Console.WriteLine($"Proses {processName} selesai, dengan durasi {sw.ElapsedMilliseconds} pada {DateTime.Now} ");
        }

        public void SampleLooping(int count) {
            // sample for loop
            for (int i = 0; i < count; i++) {
                Console.WriteLine($"Value {i}");
            }

            // sample while
            var init = 0;
            while(init < count) {
                Console.WriteLine($"Value {init}");
                init++;
            }

            // sample do while
            init = 0;
            do {
                Console.WriteLine($"Value {init}");
                init++;
            } while(init < count);

            // sample foreach
            var listInt = Enumerable.Range(0, count).ToList();
            foreach(int i in listInt) {
                Console.WriteLine($"Value {init}");
            }

        }

        public void SampleLoopingDenganBreak(int count) {
            // sample for loop
            for (int i = 0; i < count; i++) {
                Console.WriteLine($"Value {i}");

                if (i == 10)
                    break;
            }

            // sample while
            var init = 0;
            while (init < count) {
                Console.WriteLine($"Value {init}");

                if (init == 10)
                    break;

                init++;
            }

            // sample do while
            init = 0;
            do {
                Console.WriteLine($"Value {init}");

                if (init == 10)
                    break;

                init++;
            } while (init < count);

            // sample foreach
            var listInt = Enumerable.Range(0, count).ToList();
            foreach (int i in listInt) {
                Console.WriteLine($"Value {init}");

                if (init == 10)
                    break;
            }
        }
    }
}
