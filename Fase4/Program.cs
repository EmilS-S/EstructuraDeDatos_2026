using System;

public static class BusquedaBinariaModulo
{
    public static (RegistroDatos? registro, int comparaciones) BuscarRegistroIndexado(
        RegistroDatos[] arrOrdenado, int idBuscado)
    {
        if (arrOrdenado == null || arrOrdenado.Length == 0)
            return (null, 0);

        int izq = 0;
        int der = arrOrdenado.Length - 1;
        int comparaciones = 0;

        while (izq <= der)
        {
            int medio = izq + (der - izq) / 2;
            comparaciones++;

            if (arrOrdenado[medio].Id == idBuscado)
                return (arrOrdenado[medio], comparaciones);
            else if (arrOrdenado[medio].Id < idBuscado)
                izq = medio + 1;
            else
                der = medio - 1;
        }

        return (null, comparaciones);
    }
}

public class ProgramFase4
{
    private static readonly TablaDinamica dataCore = new TablaDinamica();
    private static RegistroDatos[]? indiceOrdenado = null;

    public static void Main()
    {
        int opcion;

        do
        {
            MostrarMenu();
            string input = Console.ReadLine() ?? string.Empty;

            try
            {
                if (!int.TryParse(input.Trim(), out opcion))
                {
                    Console.WriteLine("\n  ERROR: Ingresa un numero valido (0-5).");
                    Pausar();
                    continue;
                }

                switch (opcion)
                {
                    case 1: EjecutarInsercion(); break;
                    case 2: EjecutarEliminacion(); break;
                    case 3: EjecutarMostrar(); break;
                    case 4: EjecutarIndexado(); break;
                    case 5: EjecutarBusqueda(); break;
                    case 0:
                        if (ConfirmarSalida())
                            Console.WriteLine("\n  Cerrando DataCore v4.0. Hasta pronto.");
                        else
                            opcion = -1;
                        break;
                    default:
                        Console.WriteLine("\n  Opcion invalida. Elige un valor entre 0 y 5.");
                        break;
                }
            }
            catch (ArgumentException ex)
            {
                Console.WriteLine($"\n  ERROR DE VALIDACION: {ex.Message}");
                opcion = -1;
            }
            catch (InvalidOperationException ex)
            {
                Console.WriteLine($"\n  ERROR DE OPERACION: {ex.Message}");
                opcion = -1;
            }
            catch (FormatException)
            {
                Console.WriteLine("\n  ERROR: Formato de entrada invalido.");
                opcion = -1;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"\n  Error inesperado: {ex.Message}");
                opcion = -1;
            }

            if (opcion != 0)
                Pausar();

        } while (opcion != 0);
    }

    private static void MostrarMenu()
    {
        Console.WriteLine();
        Console.WriteLine("===========================================");
        Console.WriteLine(" DATACORE v4.0 -- MENU MAESTRO");
        Console.WriteLine("===========================================");
        Console.WriteLine($" Registros actuales en memoria: {dataCore.Cantidad}");
        Console.WriteLine("-------------------------------------------");
        Console.WriteLine(" [1] Insertar nuevo registro");
        Console.WriteLine(" [2] Eliminar registro por Id");
        Console.WriteLine(" [3] Mostrar todos los registros");
        Console.WriteLine(" [4] Indexar y ordenar (QuickSort)");
        Console.WriteLine(" [5] Busqueda binaria indexada O(log n)");
        Console.WriteLine(" [0] Salir del sistema");
        Console.WriteLine("===========================================");
        Console.Write(" Seleccione una opcion: ");
    }

    private static void EjecutarInsercion()
    {
        Console.Write("\n  Id (numero entero): ");
        if (!int.TryParse((Console.ReadLine() ?? string.Empty).Trim(), out int id))
        {
            Console.WriteLine("  ERROR: El Id debe ser un numero entero. Operacion cancelada.");
            return;
        }

        if (ExisteId(id))
        {
            Console.WriteLine($"  ERROR: Ya existe un registro con Id {id}.");
            return;
        }

        Console.Write("  Peso en bytes (entero > 0): ");
        if (!int.TryParse((Console.ReadLine() ?? string.Empty).Trim(), out int pesoBytes))
        {
            Console.WriteLine("  ERROR: El peso debe ser un numero entero. Operacion cancelada.");
            return;
        }

        long hash = DateTime.Now.Ticks + id;

        RegistroDatos nuevo = new RegistroDatos(id, hash, pesoBytes);
        dataCore.InsertarFinal(nuevo);
        indiceOrdenado = null;

        Console.WriteLine($"\n  Registro insertado correctamente (Id {id}).");
    }

    private static bool ExisteId(int id)
    {
        RegistroDatos[] actuales = dataCore.ObtenerComoArreglo();
        for (int i = 0; i < actuales.Length; i++)
        {
            if (actuales[i].Id == id)
                return true;
        }
        return false;
    }

    private static void EjecutarEliminacion()
    {
        if (dataCore.Cantidad == 0)
        {
            Console.WriteLine("\n  La tabla esta vacia. No hay nada que eliminar.");
            return;
        }

        Console.Write("\n  Id a eliminar: ");
        if (!int.TryParse((Console.ReadLine() ?? string.Empty).Trim(), out int id))
        {
            Console.WriteLine("  ERROR: El Id debe ser un numero entero.");
            return;
        }

        if (!ExisteId(id))
        {
            Console.WriteLine($"\n  No se encontro ningun registro con Id {id}.");
            return;
        }

        dataCore.EliminarPorId(id);
        indiceOrdenado = null;
        Console.WriteLine($"\n  Registro {id} eliminado correctamente.");
    }

    private static void EjecutarMostrar()
    {
        RegistroDatos[] actuales = dataCore.ObtenerComoArreglo();

        Console.WriteLine("\n  ---- Registros en TablaDinamica ----");
        if (actuales.Length == 0)
        {
            Console.WriteLine("  (La tabla no contiene registros).");
            return;
        }

        foreach (var r in actuales)
            Console.WriteLine($"  Id: {r.Id,4} | Hash: {r.HashValidacion,20} | Peso: {r.PesoBytes} bytes");
    }

    private static void EjecutarIndexado()
    {
        RegistroDatos[] arreglo = dataCore.ObtenerComoArreglo();

        if (arreglo.Length == 0)
            throw new InvalidOperationException("No se puede indexar una tabla vacia.");

        QuickSortModulo.contadorLlamadas = 0;
        QuickSortModulo.QuickSort(arreglo, 0, arreglo.Length - 1);
        indiceOrdenado = arreglo;

        bool ordenadoOk = QuickSortModulo.EstaOrdenado(arreglo);

        Console.WriteLine($"\n  Indice construido y ordenado ({indiceOrdenado.Length} registros).");
        Console.WriteLine($"  Llamadas recursivas de QuickSort: {QuickSortModulo.contadorLlamadas}");
        Console.WriteLine($"  Verificacion de orden: {(ordenadoOk ? "OK" : "ERROR")}");
        Console.WriteLine("  Listo para busqueda binaria (opcion 5).");
    }

    private static void EjecutarBusqueda()
    {
        if (indiceOrdenado == null)
            throw new InvalidOperationException(
                "Aun no se ha construido el indice. Ejecuta la opcion 4 primero.");

        Console.Write("\n  Id a buscar: ");
        if (!int.TryParse((Console.ReadLine() ?? string.Empty).Trim(), out int idBuscado))
        {
            Console.WriteLine("  ERROR: El Id debe ser un numero entero.");
            return;
        }

        var (registro, comparaciones) =
            BusquedaBinariaModulo.BuscarRegistroIndexado(indiceOrdenado, idBuscado);

        if (registro.HasValue)
        {
            var r = registro.Value;
            Console.WriteLine("\n  Registro encontrado:");
            Console.WriteLine($"    Id: {r.Id,4} | Hash: {r.HashValidacion,20} | Peso: {r.PesoBytes} bytes");
        }
        else
        {
            Console.WriteLine($"\n  Id {idBuscado} no encontrado en el indice.");
        }
        Console.WriteLine($"  Comparaciones realizadas: {comparaciones}");
    }

    private static bool ConfirmarSalida()
    {
        Console.Write("\n  ¿Seguro que deseas salir? (s/n): ");
        string respuesta = (Console.ReadLine() ?? string.Empty).Trim().ToLower();
        return respuesta == "s" || respuesta == "si";
    }

    private static void Pausar()
    {
        Console.Write("\n  Presiona ENTER para continuar...");
        Console.ReadLine();
    }
}