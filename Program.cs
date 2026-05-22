using System;

struct Poligono
{
    public string Nombre;
    public int NumeroLados;
    public double Lado;
    public double Apotema;
}

class Program
{
    static Poligono SeleccionarPoligono()
    {
        Console.WriteLine ("Selecciona el polígono");
        Console.WriteLine("1. Pentágono");
        Console.WriteLine("2. Hexágono");
        Console.WriteLine("3. Heptágono");
        Console.WriteLine("4. Octógono");

        int[] lados = { 5, 6, 7, 8 };
        string[] nombres = { "Pentágono", "Hexágono", "Heptágono", "Octógono" };

        int opcion;
        while (true)
        {
            Console.Write("Elige una opción: ");
            bool esValido = int.TryParse(Console.ReadLine(), out opcion);

            if (esValido && opcion >= 1 && opcion <= 4)
               break;

            Console.WriteLine("Selecciona una opcion valida");
            
        }

        Poligono figura = new Poligono();
        figura.Nombre = nombres[opcion - 1];
        figura.NumeroLados = lados[opcion - 1];

        return figura;

    }

    static Poligono PedirDatos(Poligono figura)
    {
        Console.WriteLine($"Datos para {figura.Nombre}");

        figura.Lado = PedirDecimalPositivo("Medida del lado:");
        figura.Apotema = PedirDecimalPositivo("Medida de la apotema: ");
        return figura;
    }

    static double PedirDecimalPositivo(string mensaje)
    {
        double numero;

        while(true)
        {
            Console.Write(mensaje);
            bool esValido = double.TryParse(Console.ReadLine(), out numero);

            if (esValido && numero > 0)
                return numero;

            Console.WriteLine("Ingresa un numero valido");
        }
    }

    static double CalcularArea(Poligono figura)
    {
        double perimetro = figura.NumeroLados * figura.Lado;
        return (perimetro * figura.Apotema) / 2;
    }

    static void Main()
    {

        Poligono figura = SeleccionarPoligono();
        figura = PedirDatos(figura);
        double area = CalcularArea(figura);

        Console.WriteLine($"Figura: {figura.Nombre}");
        Console.WriteLine($"Número de lados: {figura.NumeroLados}");
        Console.Write($"Área: {area:F2}");
        Console.WriteLine($"Apotema: {figura.Apotema}");
    }

}