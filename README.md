# Staff Management API

> En desarrollo activo.

## Descripción general

Staff Management API es un backend desarrollado con ASP.NET Core para gestionar la información del personal de una organización. Su objetivo es centralizar la administración de empleados, departamentos, categorías laborales y usuarios autenticados con una API REST modular y segura.

Actualmente la solución permite:

- gestionar empleados
- gestionar departamentos
- consultar categorías laborales
- registrar e iniciar sesión de usuarios
- proteger endpoints con autenticación y autorización
- ejecutar un seeding inicial de datos para entorno local

## Stack tecnológico

- .NET 10
- ASP.NET Core Web API
- Entity Framework Core
- ASP.NET Core Identity
- JWT Bearer Authentication
- Oracle Database
- Docker Compose
- MediatR
- Mapster
- Swagger

## Arquitectura

El proyecto está organizado por capas para mantener una separación clara entre dominio, aplicación e infraestructura:

- `StaffManagement.Api`: controladores, middleware, autenticación y punto de entrada HTTP
- `StaffManagement.Application`: comandos, queries, DTOs, validaciones y comportamiento de aplicación
- `StaffManagement.Domain`: entidades, errores del dominio y contratos de repositorio
- `StaffManagement.Infrastructure`: DbContext, repositorios, Identity, JWT y seeding de datos

## Patrones y buenas prácticas aplicados

El proyecto incorpora varios patrones que ayudan a mantener el código ordenado y escalable:

- `CQRS` + `MediatR`: lógica de lectura y escritura separada en queries y commands
- `Result Pattern`: manejo consistente de éxito y errores
- `Repository Pattern`: encapsulamiento del acceso a datos
- `Validation Behavior`: validaciones transversales
- `Clean Architecture`: separación por capas para mejorar mantenibilidad
- `Identity + JWT`: autenticación segura para la API

## Funcionalidades actuales

### Departamentos

La API permite:

- listar todos los departamentos
- consultar un departamento por ID
- crear un departamento
- actualizar un departamento
- eliminar un departamento

### Empleados

La API permite:

- listar todos los empleados
- consultar un empleado por ID
- crear un empleado
- actualizar un empleado
- eliminar un empleado

### Categorías laborales

La API permite:

- listar todas las categorías laborales
- consultar una categoría por ID

### Usuarios

La API permite:

- registrar un usuario nuevo
- iniciar sesión con credenciales
- obtener un token JWT para acceder a endpoints protegidos

### Seed inicial de datos

Se configuró un inicializador de base de datos que:

- crea roles por defecto (Admin y User)
- crea usuarios administrativos de ejemplo
- carga departamentos iniciales
- carga categorías de trabajo
- crea empleados de prueba

Esto permite levantar una instancia local con datos útiles para pruebas y desarrollo.

## Base de datos

El proyecto está preparado para usar Oracle Database y cuenta con un contenedor Docker para levantar una instancia local.

El archivo `docker-compose.yaml` configura un servicio Oracle 23c Free para facilitar el desarrollo local. La conexión se define mediante una cadena de conexión configurable, y se puede usar un archivo `.env` para mantener credenciales y variables de entorno fuera del repositorio.

## Información adicional

Este proyecto lo desarrollé como una forma de mejorar mi lógica en backend y complementar lo aprendido en la universidad, especialmente en el uso de Oracle Database. La idea es combinar una API REST con una base de datos relacional robusta, aplicando conceptos de arquitectura backend, validaciones de negocio y diseño de soluciones escalables.

Además, me interesa seguir llevando este proyecto hacia un enfoque más realista del desarrollo profesional, donde la capa de aplicación no sea la única responsable de validar los datos que entran desde la API. En ese sentido, Oracle DB también juega un papel importante como capa defensiva, con posibles triggers, stored procedures y funciones para validar información, reforzar integridad y centralizar reglas del negocio en la base de datos.

La intención es continuar ampliando este proyecto para ir conectando más conceptos de base de datos con lógica de backend, integrando buenas prácticas de validación, diseño de modelos y desarrollo orientado a sistemas reales.