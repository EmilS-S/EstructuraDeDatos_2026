using System;
using System.Collections.Generic;

public class Servidor
{
    // Violación de encapsulamiento
    public int id;
    public string nombre;
    public double latitud;
    public double longitud;

    // Puede ser null
    public List<int> codigosRespuesta;

    public Servidor()
    {
    }

    public long CalcularFibonacci(int n)
    {
        // Sin validaciones
        if (n <= 1)
            return n;

        // Recursión ineficiente
        return CalcularFibonacci(n - 1) +
               CalcularFibonacci(n - 2);
    }
}

class Program
{
    static void Main()
    {
        List<Servidor> servidores = new List<Servidor>();

        Servidor s1 = new Servidor();
        s1.id = 1;
        s1.nombre = "CDMX";
        s1.latitud = 19.43;
        s1.longitud = -99.13;
        s1.codigosRespuesta = null;

        servidores.Add(s1);

        // Posible NullReferenceException
        foreach (int codigo in s1.codigosRespuesta)
        {
            Console.WriteLine(codigo);
        }

        // Sin manejo de excepciones
        double latitud = double.Parse(Console.ReadLine());

        // Coordenadas inválidas permitidas
        Servidor s2 = new Servidor();
        s2.latitud = 500;

        servidores.Add(s2);

        // LINQ reemplazado por lógica poco clara
        foreach (var servidor in servidores)
        {
            if (servidor.latitud > 0)
            {
                if (servidor.codigosRespuesta != null)
                {
                    foreach (var codigo in servidor.codigosRespuesta)
                    {
                        if (codigo == 500)
                        {
                            Console.WriteLine(servidor.nombre);
                        }
                    }
                }
            }
        }
    }
}