using System;

TablaDinamica dataCore = new TablaDinamica();

for (int i = 1; i <= 15; i++)
{
    RegistroDatos reg = new RegistroDatos(
        i,
        DateTime.Now.Ticks + i,
        i * 100
    );

    dataCore.InsertarFinal(reg);
    Console.WriteLine($"[INSERT] Registro {i} añadido a la cadena.");
}

Console.WriteLine("\n--- Eliminando registros con Id 5 y Id 11 ---");
dataCore.EliminarPorId(5);
dataCore.EliminarPorId(11);
Console.WriteLine("Cadena reestructurada exitosamente.");

RegistroDatos[] arregloDinamico = dataCore.ObtenerComoArreglo();

Console.WriteLine($"\nRegistros en arreglo: {arregloDinamico.Length}");

QuickSortModulo.QuickSort(arregloDinamico, 0, arregloDinamico.Length - 1);

Console.WriteLine("\n--- Arreglo ordenado por Id (QuickSort) ---");

foreach (var r in arregloDinamico)
{
    Console.WriteLine($"Id: {r.Id} | Hash: {r.HashValidacion} | Peso: {r.PesoBytes} bytes");
}

bool ordenadoOk = QuickSortModulo.EstaOrdenado(arregloDinamico);

Console.WriteLine($"\nOrdenamiento correcto: {(ordenadoOk ? "OK" : "ERROR")}");

public class NodoRegistro
{
    public RegistroDatos Dato { get; set; }
    public NodoRegistro? Siguiente { get; set; }

    public NodoRegistro(RegistroDatos dato)
    {
        Dato = dato;
        Siguiente = null;
    }
}

public class TablaDinamica
{
    private NodoRegistro? cabeza;
    private int contadorRegistros;

    public int Cantidad => contadorRegistros;

    public void InsertarInicio(RegistroDatos nuevoRegistro)
    {
        NodoRegistro nuevoNodo = new NodoRegistro(nuevoRegistro);
        nuevoNodo.Siguiente = cabeza;
        cabeza = nuevoNodo;
        contadorRegistros++;
    }

    public void InsertarFinal(RegistroDatos nuevoRegistro)
    {
        NodoRegistro nuevoNodo = new NodoRegistro(nuevoRegistro);

        if (cabeza == null)
        {
            cabeza = nuevoNodo;
        }
        else
        {
            NodoRegistro actual = cabeza;

            while (actual.Siguiente != null)
                actual = actual.Siguiente;

            actual.Siguiente = nuevoNodo;
        }

        contadorRegistros++;
    }

    public void EliminarPorId(int idTarget)
    {
        if (cabeza == null)
            return;

        if (cabeza.Dato.Id == idTarget)
        {
            cabeza = cabeza.Siguiente;
            contadorRegistros--;
            return;
        }

        NodoRegistro anterior = cabeza;
        NodoRegistro? actual = cabeza.Siguiente;

        while (actual != null)
        {
            if (actual.Dato.Id == idTarget)
            {
                anterior.Siguiente = actual.Siguiente;
                contadorRegistros--;
                return;
            }

            anterior = actual;
            actual = actual.Siguiente;
        }
    }

    public RegistroDatos[] ObtenerComoArreglo()
    {
        RegistroDatos[] resultado = new RegistroDatos[contadorRegistros];

        NodoRegistro? actual = cabeza;
        int i = 0;

        while (actual != null)
        {
            resultado[i++] = actual.Dato;
            actual = actual.Siguiente;
        }

        return resultado;
    }
}