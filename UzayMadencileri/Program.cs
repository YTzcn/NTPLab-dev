using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UzayMadencileri
{
    class Program
    {
        static void Main(string[] args)
        {
            // Enerji maliyetini temsil eden 2D matris
            int[,] energyCost = {
            { 1, 3, 5, 8 },
            { 4, 2, 1, 7 },
            { 6, 1, 3, 2 },
            { 5, 0, 2, 3 }
        };

            int n = energyCost.GetLength(0);
            int minEnergy = FindMinimumEnergyPath(energyCost, n);
            Console.WriteLine($"En az enerji harcayarak hedefe ulaşmak için gereken enerji: {minEnergy}");
            Console.ReadLine();
        }

        static int FindMinimumEnergyPath(int[,] energyCost, int n)
        {
            // Enerji maliyetlerini saklayacak bir 2D matris oluştur
            int[,] dp = new int[n, n];

            // İlk hücre için enerji maliyeti
            dp[0, 0] = energyCost[0, 0];

            // İlk satırı doldur
            for (int j = 1; j < n; j++)
            {
                dp[0, j] = dp[0, j - 1] + energyCost[0, j];
            }

            // İlk sütunu doldur
            for (int i = 1; i < n; i++)
            {
                dp[i, 0] = dp[i - 1, 0] + energyCost[i, 0];
            }

            // Diğer hücreleri doldur
            for (int i = 1; i < n; i++)
            {
                for (int j = 1; j < n; j++)
                {
                    // Üstteki, soldaki ve çapraz soldaki değerlerin minimumunu al
                    int minCost = Math.Min(dp[i - 1, j], Math.Min(dp[i, j - 1], dp[i - 1, j - 1]));
                    dp[i, j] = minCost + energyCost[i, j];
                }
            }

            // Hedef hücredeki en az enerji harcaması
            return dp[n - 1, n - 1];
        }
    }
}
