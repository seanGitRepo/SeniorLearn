using System.Security.Cryptography;

namespace ConsoleApp1
{
    internal class Program
    {
        static void Main(string[] args)
        {
            
            var hmac1 = new HMACSHA256();
            var key = Convert.ToBase64String(hmac1.Key);
            Console.WriteLine(key);
                
        }
    }
}
