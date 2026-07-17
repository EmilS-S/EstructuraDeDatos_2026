using System;

class Program
{
    static int BusquedaLineal(int[] arr, int objetivo, out int iteraciones)
    {
        iteraciones = 0;
        for (int i = 0; i < arr.Length; i++)
        {
            iteraciones++;
            if (arr[i] == objetivo) return i;
        }
        return -1;
    }

    static int BusquedaBinaria(int[] arr, int objetivo, out int iteraciones)
    {
        iteraciones = 0;
        int izquierda = 0, derecha = arr.Length - 1;

        while (izquierda <= derecha)
        {
            iteraciones++;
            int centro = izquierda + (derecha - izquierda) / 2;

            if (arr[centro] == objetivo)
                return centro;

            if (arr[centro] < objetivo)
                izquierda = centro + 1;
            else
                derecha = centro - 1;
        }

        return -1;
    }

    static void Main()
    {
        int[] matriculas = new int[10000];
        for (int i = 0; i < matriculas.Length; i++)
            matriculas[i] = i + 1;

        Console.Write("Ingresa la matri­cula a buscar: ");
        int objetivo = int.Parse(Console.ReadLine());

        int iterLineal, iterBinaria;

        int idxLineal = BusquedaLineal(matriculas, objetivo, out iterLineal);
        int idxBinaria = BusquedaBinaria(matriculas, objetivo, out iterBinaria);

        Console.WriteLine("\n=== REPORTE DE BÃSQUEDA ===");
        Console.WriteLine($"TamaÃ±o del arreglo: {matriculas.Length}");
        Console.WriteLine($"MatrÃ­cula objetivo: {objetivo}");

        if (idxLineal != -1)
            Console.WriteLine($"[Lineal]  Encontrado en Ã­ndice: {idxLineal}");
        else
            Console.WriteLine("[Lineal]  No encontrado.");
        Console.WriteLine($"[Lineal]  Iteraciones realizadas: {iterLineal}");

        if (idxBinaria != -1)
            Console.WriteLine($"[Binaria] Encontrado en Ã­ndice: {idxBinaria}");
        else
            Console.WriteLine("[Binaria] No encontrado.");
        Console.WriteLine($"[Binaria] Iteraciones realizadas: {iterBinaria}");

        Console.WriteLine("\nObservaciÃ³n:");
        Console.WriteLine("La bÃºsqueda lineal puede revisar casi todo el arreglo.");
        Console.WriteLine("La bÃºsqueda binaria aprovecha que los datos estÃ¡n ordenados.");
    }
}