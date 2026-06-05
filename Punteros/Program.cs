using System;

     int x=10, y=25;
    Console.WriteLine($"Antes: x={x}, y={y}");
    Intercambiar(ref x, ref y);
    Console.WriteLine($"Despues: x={x}, y={y}");

    
    int cociente=CalcularYValidar(17,5,out int resto);
    Console.WriteLine($"Cociente:{cociente}");
    Console.WriteLine($"Residuo:{resto}");

    Alumno alumno1 = new Alumno {Nombre = "Dany"};
    Alumno alumno2 = alumno1;
    alumno2.Nombre="3Treum";
    Console.WriteLine(alumno1.Nombre);

static void Intercambiar(ref int a, ref int b)
{
    int temp = a;
    a=b;
    b=temp;
}

static int CalcularYValidar(int dividendo, int divisor, out int residuo)
{
    residuo=dividendo%divisor;
    return dividendo/divisor;
}

class Alumno
{
     public string? Nombre {get; set;}

}


