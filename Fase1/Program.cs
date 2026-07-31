using System;

public struct RegistroDatos
{
    public int Id;
    public long HashValidacion;
    public int PesoBytes;

    public RegistroDatos(int id, long hash, int pesoBytes)
    {
        if (pesoBytes <= 0)
            throw new ArgumentException(
                "PesoBytes debe ser mayor a 0. Un registro no puede tener tamaño nulo o negativo.",
                nameof(pesoBytes)); 

        Id = id;
        HashValidacion = hash;
        PesoBytes = pesoBytes;
    }
}

class Program1
{
    static void OrdenarPorSeleccion(RegistroDatos[] arr)
    {
        int comparaciones = 0;
        int intercambios = 0;

        for (int i = 0; i < arr.Length - 1; i++)
        {
            int indiceMinimo = i;

            for (int j = i + 1; j < arr.Length; j++)
            {
                comparaciones++;
                if (arr[j].Id < arr[indiceMinimo].Id)
                    indiceMinimo = j;
            }

            if (indiceMinimo != i)
            {
                (arr[i], arr[indiceMinimo]) = (arr[indiceMinimo], arr[i]); // Tupla moderna C#
                intercambios++;
            }
        }

        Console.WriteLine($"Comparaciones realizadas : {comparaciones}");
        Console.WriteLine($"Intercambios reales      : {intercambios}");
    }

    static void Main()
    {
        var rng = new Random();
        var arreglo = new RegistroDatos[40];

        try
        {
            for (int i = 0; i < arreglo.Length; i++)
                arreglo[i] = new RegistroDatos(
                    id: rng.Next(1, 1001),
                    hash: rng.NextInt64(),
                    pesoBytes: rng.Next(10, 5001));
        }
        catch (ArgumentException ex)
        {
            Console.WriteLine($"Error al crear registro: {ex.Message}");
        }

        Console.WriteLine("=== ESTADO INICIAL ===");
        foreach (var r in arreglo)
            Console.WriteLine($"Id: {r.Id,4} | Hash: {r.HashValidacion,20} | Peso: {r.PesoBytes} bytes");

        OrdenarPorSeleccion(arreglo);

        Console.WriteLine("\n=== ESTADO FINAL ORDENADO ===");
        foreach (var r in arreglo)
            Console.WriteLine($"Id: {r.Id,4} | Hash: {r.HashValidacion,20} | Peso: {r.PesoBytes} bytes");
    }
}
