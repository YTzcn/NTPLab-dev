using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Casus
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.Write("Şifreli mesajı girin: ");
            string encryptedMessage = Console.ReadLine();
            string originalMessage = DecryptMessage(encryptedMessage);
            Console.WriteLine($"Orijinal Mesaj: {originalMessage}");
            Console.ReadLine();
        }

        static string DecryptMessage(string encryptedMessage)
        {
            // Fibonacci sayılarını saklamak için bir liste
            List<int> fibonacci = GenerateFibonacci(encryptedMessage.Length);
            char[] originalChars = new char[encryptedMessage.Length];

            for (int i = 0; i < encryptedMessage.Length; i++)
            {
                // ASCII değerini al
                int encryptedValue = (int)encryptedMessage[i];
                int position = i + 1; // Pozisyon 1'den başlar

                // Modüler çözümleme ile orijinal değeri bulma
                int asciiValue;

                if (IsPrime(position))
                {
                    // Pozisyon asal ise 100'e göre mod
                    asciiValue = GetOriginalValue(encryptedValue, 100);
                }
                else
                {
                    // Pozisyon asal değilse 256'ya göre mod
                    asciiValue = GetOriginalValue(encryptedValue, 256);
                }

                // Fibonacci ile böl
                // Fibonacci sayısının 0 olduğu durumları kontrol et
                int fibValue = fibonacci[i];
                if (fibValue > 0)
                {
                    originalChars[i] = (char)(asciiValue / fibValue);
                }
                else
                {
                    originalChars[i] = '?'; // Geçersiz durum için bir işaret koy
                }
            }

            return new string(originalChars);
        }

        static int GetOriginalValue(int encryptedValue, int mod)
        {
            for (int i = 0; i < mod; i++)
            {
                if (i % mod == encryptedValue)
                {
                    return i;
                }
            }
            return 0; // Bu durumda hiç bir değer bulunamazsa 0 döner
        }

        static List<int> GenerateFibonacci(int length)
        {
            List<int> fibonacci = new List<int> { 1, 1 };
            for (int i = 2; i < length; i++)
            {
                int nextFibonacci = fibonacci[i - 1] + fibonacci[i - 2];
                fibonacci.Add(nextFibonacci);
            }
            return fibonacci;
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
