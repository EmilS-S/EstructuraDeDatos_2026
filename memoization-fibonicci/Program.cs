using System;
using System.Diagnostics;

namespace FibonicciEB9;
class Program
{
    public static long FibonacciInseguro(int n)
    {
    if (n == 0) return 0;
    if (n == 1) return 1;
    return FibonacciInseguro(n - 1)
    + FibonacciInseguro(n - 2);
    }
    public static long FibonacciPro(
        int n, long[] cache)
        {
        if (n == 0) return 0;
        if (n == 1) return 1;
        
        if (cache[n] != -1)
        return cache[n]; 
      
        cache[n] = FibonacciPro(n - 1, cache)
        + FibonacciPro(n - 2, cache);
        return cache[n];
        }

static void Main(string[] args)
 {
    Console.Write(
    "Ingresa un número (35-43): ");
    string? input = Console.ReadLine();

    if (!int.TryParse(input, out int n)
    || n < 0)
    {
    Console.WriteLine(
    "Error: ingresa un número positivo.");
    return;
    }
    Stopwatch sw = new Stopwatch();

    sw.Restart();
    long r1 = FibonacciInseguro(n);
    sw.Stop();
    Console.WriteLine(
    $"Inseguro: F({n}) = {r1}");
    Console.WriteLine(
    $"Tiempo: {sw.ElapsedMilliseconds} ms");
   
    long[] cache = new long[n + 1];
    for (int i = 0; i <= n; i++)
    cache[i] = -1;
    sw.Restart();
    long r2 = FibonacciPro(n, cache);
    sw.Stop();
    Console.WriteLine(
    $"Pro: F({n}) = {r2}");
    Console.WriteLine(
    $"Tiempo: {sw.ElapsedMilliseconds} ms");
 }

static void Imprimir()
    {
        int n = 40; 
        long[] cache = new long[n + 1];
        for (int i = 0; i <= n; i++)
        cache[i] = -1;
    }

}