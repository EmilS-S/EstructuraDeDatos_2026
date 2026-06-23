using System;
using System.Linq;
 
namespace Simulacro;
 
     public struct PuntoDeRed
    {
        public double Latitud { get; }
        public double Longitud { get; }
 
        public PuntoDeRed(double latitud, double longitud)
        {
            if (latitud < -90.0 || latitud > 90.0)
                throw new ArgumentOutOfRangeException(nameof(latitud),
                    "La latitud debe estar entre -90 y 90 grados.");
            if (longitud < -180.0 || longitud > 180.0)
                throw new ArgumentOutOfRangeException(nameof(longitud),
                    "La longitud debe estar entre -180 y 180 grados.");
            Latitud = latitud;
            Longitud = longitud;
        }
 
        public override string ToString() => $"({Latitud}°, {Longitud}°)";
    }
 
    public class ServidorConexion
    {
        public int ID { get; set; }
        public string Nombre { get; set; }
        public PuntoDeRed Ubicacion { get; set; }
        public List<int> CodigosRespuesta { get; set; }
 
        // Cache de Fibonacci memoizado por instancia
        private readonly long[] _cache = new long[100];
 
        // Constructor
        public ServidorConexion(int id, string nombre,
            PuntoDeRed ubicacion,
            List<int> codigos)
        {
            ID = id;
            Nombre = nombre;
            Ubicacion = ubicacion;
            CodigosRespuesta = codigos ?? new List<int>();
        }
 
        public override string ToString() => $"[{ID}] {Nombre} @ {Ubicacion}";
 
        public long DiagnosticarLatencia(int n, out string alerta)
        {
            if (n < 0 || n >= 100)
                throw new ArgumentOutOfRangeException(nameof(n), "El valor de n debe estar entre 0 y 99.");
 
            if (n <= 1)
            {
                alerta = string.Empty;
                return n;
            }
 
            if (_cache[n] != 0)
            {
                alerta = string.Empty;
                return _cache[n];
            }
 
            _cache[n] = DiagnosticarLatencia(n - 1, out _) +
                        DiagnosticarLatencia(n - 2, out _);
 
            if (_cache[n] > 10_000)
                alerta = $"ALERTA: Índice de estrés crítico en n={n}";
            else
                alerta = string.Empty;
 
            return _cache[n];
        }
    }
 class Program{

 
    public static void Main(string[] args)
    {
        var servidores = new List<ServidorConexion>
        {
            new ServidorConexion(1, "Servidor-CDMX",
                new PuntoDeRed(19.43, -99.13),
                new List<int> { 200, 200, 500 }),
            new ServidorConexion(2, "Servidor-NYC",
                new PuntoDeRed(40.71, -74.01),
                new List<int> { 200, 404 }),
            new ServidorConexion(3, "Servidor-Sydney",
                new PuntoDeRed(-33.87, 151.21),
                new List<int> { 500, 500 }),
            new ServidorConexion(4, "Servidor-Londres",
                new PuntoDeRed(51.51, -0.13),
                new List<int> { 200, 200, 200 })
        };
 
        var servidoresCriticos = servidores
            .Where(s => s.Ubicacion.Latitud > 0
                     && s.CodigosRespuesta.Contains(500))
            .ToList();
 
        Console.WriteLine("=== SERVIDORES CRÍTICOS ===");
        foreach (var servidor in servidoresCriticos)
            Console.WriteLine(servidor);
 
        try
        {
            Console.Write("Ingresa la latitud del nuevo servidor: ");
            string? input = Console.ReadLine();
            if (!double.TryParse(input, out double latitud))
                throw new FormatException($"'{input}' no es un número decimal válido.");
 
            var punto = new PuntoDeRed(latitud, 0.0);
            Console.WriteLine($"Punto creado: {punto}");
 
            var servidorNuevo = new ServidorConexion(
                99, "Servidor-Temporal", punto, new List<int>());
 
            long indiceEstres = servidorNuevo.DiagnosticarLatencia(20, out string alerta);
 
            if (!string.IsNullOrEmpty(alerta))
                Console.WriteLine(alerta);
            else
                Console.WriteLine($"Índice de estrés: {indiceEstres}");
        }
        catch (FormatException fe)
        {
            Console.WriteLine($"[ERROR DE FORMATO] {fe.Message}");
        }
        catch (ArgumentOutOfRangeException aore)
        {
            Console.WriteLine($"[ERROR DE RANGO] {aore.Message}");
        }
        catch (OverflowException oe)
        {
            Console.WriteLine($"[DESBORDAMIENTO] {oe.Message}");
        }
    }
 }