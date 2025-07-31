# API de Inventario

Esta es una API hecha en .NET 8 que permite manejar productos de una tienda. Está pensada para usarse con base de datos en memoria (InMemory), así que no hace falta tener una base real para que funcione.

## ¿Qué se puede hacer?

- Ver todos los productos
- Buscar un producto por ID
- Crear un producto nuevo
- Editar uno existente
- Borrar productos
- Simular una sincronización con el sistema central

## Tecnologias que use

- ASP.NET Core 8
- Entity Framework (InMemory)
- Swagger para probar los endpoints
- C#

## Organización del proyecto

- **Access/**: manejo de acceso a datos
- **Controllers/**: endpoints de la API
- **Models/**: los modelos de los productos
- **Services/**: lógica principal de cada funcionalidad

## ¿Qué hace " SincronizarConCentral() "?

Es un método que simula una sincronización con el sistema central, como si se estuviera trayendo información de otra fuente externa. 
En este caso, se agregan productos para simular una actualización del stock.

## ¿Como correr la API?

1. Abrir el proyecto con Visual Studio.
2. Ejecutar con el perfil **IIS Express** (tecla F5).
3. Se va a abrir el navegador con Swagger:  
   https://localhost:{puerto}/swagger

Ahi se pueden probar todos los endpoints sin tener que usar Postman ni nada más