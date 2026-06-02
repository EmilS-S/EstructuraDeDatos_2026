using System;

namespace ArbolesBinarios
{
    class Program
    {
        static void Main(string[] args)
        {
            var raiz = new Nodo(5, "Raíz");
            raiz = InsertarNodo(raiz, new Nodo(3, "Izquierda"));
            raiz = InsertarNodo(raiz, new Nodo(7, "Derecha"));
            string? resultado = BuscarNodo(raiz, 3);
            Console.WriteLine(resultado ?? "No encontrado");
        }

        static Nodo InsertarNodo(Nodo? raiz, Nodo nuevoNodo)
        {
            if (raiz == null) return nuevoNodo;

            if (nuevoNodo.ID < raiz.ID)
                raiz.HijoIzquierdo = InsertarNodo(raiz.HijoIzquierdo, nuevoNodo);
            else if (nuevoNodo.ID > raiz.ID)
                raiz.HijoDerecho = InsertarNodo(raiz.HijoDerecho, nuevoNodo);

            return raiz;
        }

        static string? BuscarNodo(Nodo? raiz, int idTarget)
        {
            if (raiz == null) return null;
            if (idTarget == raiz.ID) return raiz.Dato;

            return idTarget < raiz.ID
                ? BuscarNodo(raiz.HijoIzquierdo, idTarget)
                : BuscarNodo(raiz.HijoDerecho, idTarget);
        }
    }

    public class Nodo
    {
        public int ID { get; set; }
        public string Dato { get; set; } = string.Empty;
        public Nodo? HijoIzquierdo { get; set; }
        public Nodo? HijoDerecho { get; set; }

        public Nodo(int id, string dato)
        {
            ID = id;
            Dato = dato;
        }
    }
}