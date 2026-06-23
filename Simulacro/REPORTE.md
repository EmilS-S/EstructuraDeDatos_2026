REPORTE DE AUDITORÍA

Proyecto: Sistema de Monitoreo de Red

Hallazgo 1 – Violación de Encapsulamiento

Clase afectada: Servidor

Severidad: Alta

Problema:
Todos los atributos son públicos y pueden modificarse libremente desde cualquier parte del programa.

Principio violado:
Encapsulamiento.

Corrección propuesta:
Utilizar propiedades con get/set y validaciones.

Hallazgo 2 – Posible NullReferenceException

Clase afectada: Servidor

Severidad: Alta

Problema:
La colección codigosRespuesta puede ser null.

Impacto:
El programa puede finalizar inesperadamente durante la ejecución.

Corrección propuesta:
Inicializar siempre la colección en el constructor.


Hallazgo 3 – Recursión Ineficiente

Método afectado: CalcularFibonacci()

Severidad: Media

Problema:
La implementación utiliza recursión pura.

Impacto:
Complejidad exponencial O(2^n).

Corrección propuesta:
Implementar Memoization.


Hallazgo 4 – Falta de Validación de Coordenadas

Clase afectada: Servidor

Severidad: Alta

Problema:
Se aceptan latitudes fuera del rango válido [-90,90].

Corrección propuesta:
Validar mediante excepciones ArgumentOutOfRangeException.


Hallazgo 5 – Manejo Incorrecto de Entrada

Método afectado: Main()

Severidad: Media

Problema:
Uso de double.Parse() sin validación.

Corrección propuesta:
Reemplazar por double.TryParse().


Hallazgo 6 – Ausencia de LINQ

Método afectado: Main()

Severidad: Baja

Problema:
Uso excesivo de ciclos anidados.

Corrección propuesta:
Utilizar LINQ con Where() y Select() para mejorar legibilidad.


Conclusión

El proyecto presenta problemas de encapsulamiento, robustez, validación de datos y eficiencia algorítmica. Se recomienda aplicar principios SOLID, programación defensiva y estructuras de datos adecuadas para garantizar mantenibilidad y estabilidad.