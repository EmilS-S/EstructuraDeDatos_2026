using System;

namespace LogicaREC
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.Write("Ingresa un número para la suma: ");
            string entradaSuma = Console.ReadLine() ?? "";

            if (int.TryParse(entradaSuma, out int numeroSuma) && numeroSuma > 0)
            {
                int resultado = SumarHasta(numeroSuma);
                Console.WriteLine($"La suma de 1 hasta {numeroSuma} es: {resultado}");
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("ERROR: Solo se aceptan enteros positivos");
                Console.ResetColor();
            }

            Console.WriteLine();

            Console.Write("Ingresa un número para la cuenta regresiva: ");
            string entradaConteo = Console.ReadLine() ?? "";

            if (int.TryParse(entradaConteo, out int numeroConteo) && numeroConteo > 0)
            {
                ImprimirCuentaRegresiva(numeroConteo);
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("ERROR: Solo se aceptan enteros positivos");
                Console.ResetColor();
            }
        }

        static int SumarHasta(int n)
        {
            if (n == 1) return 1;
            return n + SumarHasta(n - 1);
        }

        static void ImprimirCuentaRegresiva(int numero)
        {
            if (numero == 0)
            {
                Console.WriteLine(0);
                return;
            }
            Console.WriteLine(numero);
            ImprimirCuentaRegresiva(numero - 1);
        }
    }
}