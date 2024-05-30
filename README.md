# DogWalkApp_Backend

Esta parte esta desarrollada en C# usando Visual Studio y usando .NET Framework 7. Primero cree la solución en blanco y después fui añadiéndole proyectos conforme avanzaba en su desarrollo.
Primero añadí una librería de clases llamada Models. Aquí vamos a implementar la capa de Acceso a Datos usando Entity Framework.

Entity Framework (EF) es un marco de trabajo (framework) de mapeo objeto-relacional (ORM, por sus siglas en inglés). Su principal propósito es facilitar la interacción entre aplicaciones .NET y bases de datos relacionales mediante la abstracción de las operaciones de acceso a datos, permitiendo a los desarrolladores trabajar con datos en forma de objetos en lugar de tener que escribir consultas SQL directamente. Se han de instalar paquetes a través del Administrador de paquetes NuGet para que todo esto funcione.

¿Para qué sirve Entity Framework?
1. Abstracción de Base de Datos: EF permite trabajar con bases de datos utilizando objetos y clases en lugar de consultas SQL. Esto simplifica el desarrollo y el mantenimiento del código.
2. Mapeo Objeto-Relacional: EF mapea automáticamente las clases en C# a las tablas de una base de datos y las propiedades de estas clases a las columnas correspondientes, gestionando las relaciones entre entidades.
3. Consultas Linq: EF permite realizar consultas a la base de datos utilizando LINQ (Language Integrated Query), lo que proporciona una sintaxis más intuitiva y segura para el tipo en comparación con SQL.
4. Migraciones de Base de Datos: EF soporta migraciones, lo que facilita la gestión de cambios en el esquema de la base de datos a lo largo del ciclo de vida de la aplicación.
5. Gestión de Conexiones: EF se encarga de la apertura y cierre de conexiones a la base de datos, así como de la gestión de transacciones, simplificando así el manejo de la infraestructura subyacente.
   
En mi caso, al tener la base de datos hecha antes de empezar el proyecto en VS, con lo cual no he tenido que hacer migraciones hacia la base de datos. He volcado la base de datos al proyecto en VS abriendo la Consola del Administrador de paquetes NuGet y escribiendo el siguiente comando:

Scaffold-DbContext"Server=Albert\SQLEXPRESS;Database=DOG_WALK_PLUS;Trust Server Certificate=true;User Id='xxxxx';Password=xxxxxxx;MultipleActiveResultSets=true" Microsoft.EntityFrameworkCore.SqlServer -ContextDir "Context" -OutputDir "Models"

Al ejecutar este comando me generará una carpeta ‘Context’ que tendrá una clase DbContext. Esta es la clase principal que coordina la funcionalidad de Entity Framework para un modelo de datos. Esta clase es responsable de la creación de conexiones a la base de datos, la construcción de comandos de base de datos, el mapeo de resultados de consultas a objetos y el seguimiento de los cambios en los objetos. En mi caso, DogWalkPlusContext es una instancia de DbContext que representa mi base de datos y contiene DbSet para mis entidades como Opiniones, Perros, Paseadores, etc.

También nos va a generar una carpeta Models que contendrá todas las entidades de nuestra base de datos generado por EF. Ahí cada clase pertenece a una entidad de la Base de Datos.

A partir de aquí ya podemos empezar a construir nuestro proyecto Api REST. Para ello necesitamos crear en nuestra librería de clases Models un directorio llamado DTOs (Data Transfer Objects): Los DTOs son objetos simples que se utilizan para transferir datos entre procesos o capas de la aplicación. En mi caso, tengo OpinionDto y CrearOpinionDto que se utilizan para enviar y recibir datos de las opiniones en mis endpoints HTTP.

Después sobre la solución de nuestro proyecto agregamos un nuevo proyecto: ASP .NET Core Web Api. En este proyecto se desarrollarán los controladores de la Api para crear los endpoints http para que la aplicación nos devuelva los datos que están en nuestra base de datos.

También dispone de un directorio para el manejo de errores en las solicitudes Http y de un directorio Middleware, que es lo que dirige las solicitudes HTTP a mis controladores.
Por otra parte consta de una capa llamada BLL (Business Logic Layer o Capa Lógica de Negocio). Es una parte crucial de la arquitectura de una aplicación. Esta capa se encarga de ejecutar las reglas de negocio y las operaciones de la aplicación. Aquí es donde se toman las decisiones y se realizan los cálculos.

En mi proyecto, la BLL contiene clases de servicio como Servicio, que implementan la lógica de negocio para las operaciones CRUD (crear, leer, actualizar, eliminar) en los servicios de mi base de datos.
Por ejemplo, en mi clase Servicio, el método Agregar() implementa la lógica para agregar un nuevo servicio a la base de datos. Primero, crea una nueva instancia de Servicio con los datos proporcionados, luego utiliza la unidad de trabajo para agregar el nuevo servicio a la base de datos y finalmente guarda los cambios en la base de datos.
8
La ventaja de tener una BLL separada es que mantiene la lógica de negocio centralizada en un solo lugar, lo que facilita el mantenimiento y la prueba del código. Además, al separar la lógica de negocio de la capa de presentación y la capa de acceso a datos, podemos cambiar la implementación de cualquiera de estas capas sin afectar a las demás.
