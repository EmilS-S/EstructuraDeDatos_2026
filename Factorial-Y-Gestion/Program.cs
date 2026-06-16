using System;
using System.Numerics; 

namespace EB8;

class Program
{
    static int FactorialInt(int n)
    {
        if (n == 0 || n == 1)
            return 1;
        return n * FactorialInt(n - 1);
    }

    static int FactorialIterativo(int n)
    {
        int resultado = 1;
        for (int i = 2; i <= n; i++)  
            resultado *= i;
        return resultado;
    }

    static BigInteger FactorialProfesional(BigInteger n) 
    {
        if (n == 0 || n == 1)
            return BigInteger.One;
        return n * FactorialProfesional(n - 1);
    }

    static void Imprimir() 
    {
        for (int i = 1; i <= 20; i++)
        {
            Console.WriteLine($"n={i:D2}|Recursivo:{FactorialInt(i),25}|Iterativo:{FactorialIterativo(i),25}");
        }

        BigInteger resultado = FactorialProfesional(100);
        Console.WriteLine($"100! = {resultado}");
    }

    static void Main(string[] args)
    {
        Imprimir();
    }
}