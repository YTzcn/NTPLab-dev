using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Labirent
{
    class Program
    {
        static void Main()
        {
            int M = 5; // Labirentin satır sayısı
            int N = 5; // Labirentin sütun sayısı

            List<string> path = FindPath(M, N);

            if (path.Count > 0)
            {
                Console.WriteLine("Yol bulundu:");
                foreach (var step in path)
                {
                    Console.WriteLine(step);
                }
            }
            else
            {
                Console.WriteLine("Şehir kayboldu!");
            }
            Console.ReadLine();
        }

        static List<string> FindPath(int M, int N)
        {
            // Yönler: aşağı, yukarı, sağ, sol
            int[,] directions = new int[,] { { 1, 0 }, { -1, 0 }, { 0, 1 }, { 0, -1 } };
            bool[,] visited = new bool[M, N];
            Queue<Tuple<int, int, List<string>>> queue = new Queue<Tuple<int, int, List<string>>>();

            // Başlangıç noktasını ekle
            queue.Enqueue(Tuple.Create(0, 0, new List<string> { "(0, 0)" }));

            while (queue.Count > 0)
            {
                var current = queue.Dequeue();
                int x = current.Item1;
                int y = current.Item2;
                List<string> currentPath = current.Item3;

                // Hedef nokta kontrolü
                if (x == M - 1 && y == N - 1)
                {
                    return currentPath; // Yol bulundu
                }

                // Yönler arasında döngü
                for (int i = 0; i < directions.GetLength(0); i++)
                {
                    int newX = x + directions[i, 0];
                    int newY = y + directions[i, 1];

                    // Sınır kontrolü
                    if (IsInBounds(newX, newY, M, N) && !visited[newX, newY] && CanEnter(newX, newY))
                    {
                        visited[newX, newY] = true;
                        List<string> newPath = new List<string>(currentPath) { $"({newX}, {newY})" };
                        queue.Enqueue(Tuple.Create(newX, newY, newPath));
                    }
                }
            }

            return new List<string>(); // Yol bulunamadı
        }

        static bool CanEnter(int x, int y)
        {
            return IsPrime(x) && IsPrime(y) && (x + y) % (x * y) == 0;
        }

        static bool IsInBounds(int x, int y, int M, int N)
        {
            return x >= 0 && x < M && y >= 0 && y < N;
        }

        static bool IsPrime(int number)
        {
            if (number <= 1) return false;
            for (int i = 2; i <= Math.Sqrt(number); i++)
            {
                if (number % i == 0) return false;
            }
            return true;
        }
    }
}
