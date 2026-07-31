using System;
using System.Diagnostics;
public struct RegistroDatos
{
    public int Id { get; }

    public string HashValidacion { get; }


    public double PesoBytes { get; }

  
    public RegistroDatos(int id, string hashValidacion, double pesoBytes)
    {
        if (id <= 0)
            throw new ArgumentException(
                "El Id debe ser un entero positivo mayor que cero.", nameof(id));

        if (string.IsNullOrEmpty(hashValidacion))
            throw new ArgumentNullException(
                nameof(hashValidacion),
                "HashValidacion no puede ser null ni una cadena vacía.");

        if (pesoBytes <= 0.0)
            throw new ArgumentOutOfRangeException(
                nameof(pesoBytes),
                "PesoBytes debe ser un valor numérico positivo mayor que cero.");

        Id = id;
        HashValidacion = hashValidacion;
        PesoBytes = pesoBytes;
    }

    public override string ToString() =>
        $"[Id={Id}, Hash={HashValidacion[..8]}..., Peso={PesoBytes:F2}B]";
}

class Program1
{
    static int contadorComparaciones = 0;
    static int contadorIntercambios = 0;

    static void OrdenarPorSeleccion(RegistroDatos[] arr)
    {
        for (int i = 0; i < arr.Length - 1; i++)
        {
            int indiceMinimo = i;

            for (int j = i + 1; j < arr.Length; j++)
            {
                contadorComparaciones++;
                if (arr[j].Id < arr[indiceMinimo].Id)
                    indiceMinimo = j;
            }

            if (indiceMinimo != i)
            {
                (arr[i], arr[indiceMinimo]) = (arr[indiceMinimo], arr[i]);
                contadorIntercambios++;
            }
        }
    }

    static int contadorLlamadas = 0;

    public static void QuickSort(RegistroDatos[] arr, int bajo, int alto)
    {
        contadorLlamadas++; 

        if (bajo < alto) 
        {
            int indicePivote = Particionar(arr, bajo, alto);

            QuickSort(arr, bajo, indicePivote - 1);

            QuickSort(arr, indicePivote + 1, alto);
        }
    }

    private static int Particionar(RegistroDatos[] arr, int bajo, int alto)
    {
        RegistroDatos pivote = arr[alto]; 
        int i = bajo - 1;                 

        for (int j = bajo; j < alto; j++)
        {
            if (arr[j].Id <= pivote.Id)
            {
                i++;
                (arr[i], arr[j]) = (arr[j], arr[i]);
            }
        }

        (arr[i + 1], arr[alto]) = (arr[alto], arr[i + 1]);

        return i + 1; 
    }

     static bool EstaOrdenado(RegistroDatos[] arr)
    {
        for (int i = 0; i < arr.Length - 1; i++)
            if (arr[i].Id > arr[i + 1].Id)
                return false;
        return true;
    }

    static RegistroDatos[] GenerarArregloAleatorio(int cantidad)
    {
        Random rnd = new Random(42); // Semilla fija para reproducibilidad
        RegistroDatos[] arreglo = new RegistroDatos[cantidad];

        for (int i = 0; i < cantidad; i++)
        {
            arreglo[i] = new RegistroDatos(
                id: rnd.Next(1, 100_001),                  // Id: 1 a 100000
                hashValidacion: Guid.NewGuid().ToString(),  // Hash como GUID string
                pesoBytes: 1.0 + rnd.NextDouble() * 9999    // Peso: 1.0 a 10000.0
            );
        }

        return arreglo;
    }

    static void Main(string[] args)
    {
        int tamaño = 10_000;
        RegistroDatos[] arregloOriginal = GenerarArregloAleatorio(tamaño);

        RegistroDatos[] copiaSeleccion = (RegistroDatos[])arregloOriginal.Clone();
        RegistroDatos[] copiaQuickSort = (RegistroDatos[])arregloOriginal.Clone();

        contadorComparaciones = 0;
        contadorIntercambios = 0;
        Stopwatch swSeleccion = Stopwatch.StartNew();
        OrdenarPorSeleccion(copiaSeleccion);
        swSeleccion.Stop();

        contadorLlamadas = 0;
        Stopwatch swQuickSort = Stopwatch.StartNew();
        QuickSort(copiaQuickSort, 0, copiaQuickSort.Length - 1);
        swQuickSort.Stop();

        Console.WriteLine(EstaOrdenado(copiaSeleccion)
            ? "OK: Selección ordenó correctamente"
            : "ERROR: Selección NO ordenó correctamente");

        Console.WriteLine(EstaOrdenado(copiaQuickSort)
            ? "OK: QuickSort ordenó correctamente"
            : "ERROR: QuickSort NO ordenó correctamente");

        Console.WriteLine("============================================================");
        Console.WriteLine($"   REPORTE COMPARATIVO DE ORDENAMIENTO (n = {tamaño:N0})");
        Console.WriteLine("============================================================");
        Console.WriteLine("Algoritmo            : Selección Directa");
        Console.WriteLine($"Registros procesados : {tamaño:N0}");
        Console.WriteLine($"Comparaciones        : {contadorComparaciones:N0}");
        Console.WriteLine($"Intercambios         : {contadorIntercambios:N0}");
        Console.WriteLine($"Tiempo de ejecución  : {swSeleccion.ElapsedMilliseconds} ms");
        Console.WriteLine("------------------------------------------------------------");
        Console.WriteLine("Algoritmo            : QuickSort");
        Console.WriteLine($"Registros procesados : {tamaño:N0}");
        Console.WriteLine($"Llamadas recursivas  : {contadorLlamadas:N0}");
        Console.WriteLine($"Tiempo de ejecución  : {swQuickSort.ElapsedMilliseconds} ms");
        Console.WriteLine("------------------------------------------------------------");

        double ratio = swQuickSort.ElapsedMilliseconds > 0
            ? (double)swSeleccion.ElapsedMilliseconds / swQuickSort.ElapsedMilliseconds
            : 0;
        Console.WriteLine(ratio > 0
            ? $"Ratio de velocidad   : QuickSort fue {ratio:F0}x más rápido"
            : "Ratio de velocidad   : QuickSort demasiado rápido para medir (< 1 ms)");
        Console.WriteLine("============================================================");
    }
}