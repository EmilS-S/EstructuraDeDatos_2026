using System;

class Program
{
    static void CambiarValor(int x)
    {
        x = 100;
        Console.WriteLine($"  Dentro de CambiarValor: x = {x}");
    }

    static void CambiarReferencia(int[] arr)
    {
        arr[0] = 100;
        Console.WriteLine($"  Dentro de CambiarReferencia: arr[0] = {arr[0]}");
    }

    static void Main()
    {
        int numero = 5;
        Console.WriteLine($"Antes de CambiarValor: numero = {numero}");
        CambiarValor(numero);
        Console.WriteLine($"Después de CambiarValor: numero = {numero}");

        Console.WriteLine();

        int[] arreglo = { 1, 2, 3 };
        Console.WriteLine($"Antes de CambiarReferencia: arreglo[0] = {arreglo[0]}");
        CambiarReferencia(arreglo);
        Console.WriteLine($"Después de CambiarReferencia: arreglo[0] = {arreglo[0]}");
    }
}