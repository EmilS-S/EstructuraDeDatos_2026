using System;

// NOTA: Este archivo asume que ya tienes en tu proyecto (Fase 1):
//   - El struct RegistroDatos (con Id, HashValidacion, PesoBytes)
//   - El método OrdenarPorSeleccion(RegistroDatos[] arr)
//
// Si tu clase de algoritmos se llama distinto, ajusta el "class Algoritmos"
// de abajo para que coincida (o pega estos métodos dentro de tu clase existente).

public static class QuickSortModulo
{
    // Contador de llamadas recursivas (instrumentación del Call Stack)
    public static long contadorLlamadas = 0;

    // ============================================================
    // MÉTODO DE CONTROL RECURSIVO
    // ============================================================
    public static void QuickSort(RegistroDatos[] arr, int bajo, int alto)
    {
        contadorLlamadas++; // Instrumentación del Call Stack

        if (bajo < alto) // Caso base: solo procede si hay más de un elemento
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

    public static bool EstaOrdenado(RegistroDatos[] arr)
    {
        for (int i = 0; i < arr.Length - 1; i++)
            if (arr[i].Id > arr[i + 1].Id)
                return false;
        return true;
    }
}