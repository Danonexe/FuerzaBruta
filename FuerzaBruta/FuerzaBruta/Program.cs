// See https://aka.ms/new-console-template for more information

namespace FuerzaBruta
{

    class Program
    {
        static void Main()
        {
            string filePath = @"C:\Users\danie\RiderProjects\FuerzaBruta\FuerzaBruta\bin\Debug\net8.0\resource\2151220-passwords.txt";
            string targetHash = FuerzaBruta.ComputeHash("iloveyou"); 
            int totalLines = File.ReadAllLines(filePath).Length;
            int chunkSize = totalLines / 3;

            string foundPassword = null;
            object lockObject = new object();

            Thread thread1 = new Thread(() =>
            {
                string result = FuerzaBruta.encontrarPorFuerzaBruta(targetHash, filePath, 1);
                if (result != null)
                {
                    lock (lockObject)
                    {
                        foundPassword = result;
                    }
                }
            });

            Thread thread2 = new Thread(() =>
            {
                string result = FuerzaBruta.encontrarPorFuerzaBruta(targetHash, filePath, chunkSize + 1);
                if (result != null)
                {
                    lock (lockObject)
                    {
                        foundPassword = result;
                    }
                }
            });

            Thread thread3 = new Thread(() =>
            {
                string result = FuerzaBruta.encontrarPorFuerzaBruta(targetHash, filePath, chunkSize * 2 + 1);
                if (result != null)
                {
                    lock (lockObject)
                    {
                        foundPassword = result;
                    }
                }
            });

            thread1.Start();
            thread2.Start();
            thread3.Start();

            thread1.Join();
            thread2.Join();
            thread3.Join();

            if (foundPassword != null)
            {
                Console.WriteLine("Contraseña encontrada: " + foundPassword);
            }
            else
            {
                Console.WriteLine("Contraseña no encontrada");
            }
        }
    }
}