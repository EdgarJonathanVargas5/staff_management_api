# Staff Management API

> Proyecto en construcción.
>
> Esta API ya cuenta con una base funcional para administrar empleados y departamentos, pero sigue en fase de desarrollo. La intención es seguir ampliando validaciones, reglas de negocio, autenticación, pruebas y mejoras de infraestructura.

## Descripción general

Staff Management API es un backend desarrollado con ASP.NET Core para gestionar la información de personal de una organización. Su objetivo principal es centralizar el manejo de empleados y departamentos mediante una API REST con estructura modular y separación de responsabilidades.

Actualmente se pueden realizar operaciones básicas de alta, consulta, actualización y eliminación de:

- Empleados
- Departamentos

## Estado del proyecto

El proyecto se encuentra en desarrollo activo.

Se planean incorporar las siguientes mejoras:

- validaciones más robustas
- autenticación y autorización
- paginación y filtros
- pruebas automatizadas
- refinamiento de la persistencia y configuración de entorno

## Stack tecnológico

- .NET 10
- ASP.NET Core Web API
- Entity Framework Core
- Oracle Database
- Docker Compose
- MediatR
- Swagger 

## Arquitectura

El proyecto está organizado por capas para mantener una separación entre dominio, aplicación e infraestructura:

- `StaffManagement.Api`: controladores, middleware y punto de entrada HTTP
- `StaffManagement.Application`: comandos, queries, DTOs y validaciones
- `StaffManagement.Domain`: entidades, contratos de repositorio y reglas del dominio
- `StaffManagement.Infrastructure`: acceso a datos, DbContext, repositorios y configuración de dependencias

## Patrones y buenas prácticas aplicados

El proyecto ya incorpora varios patrones que ayudan a mantener el código ordenado y escalable:

- `CQRS` + `MediatR`: la lógica de lectura y escritura se organiza en queries y commands, desacoplados de los controladores.
- `Result Pattern`: centraliza el resultado de las operaciones para manejar éxito, errores y validaciones de forma consistente.
- `Repository Pattern`: la capa de infraestructura encapsula el acceso a datos mediante repositorios.
- `Validation Behavior`: las validaciones se aplican de forma transversal mediante comportamientos de MediatR.
- `Clean Architecture`: se separan dominio, aplicación e infraestructura para favorecer el mantenimiento.

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

## Base de datos

El proyecto está preparado para usar Oracle Database y se incluye un contenedor Docker para levantar una instancia local. 

El archivo `docker-compose.yaml` configura un servicio Oracle 23c Free para facilitar el desarrollo local.

Configurando una clave mediante un archivo .env.

## Información adicional

Este proyecto lo desarrollé como una forma de mejorar mi lógica en backend y complementar lo aprendido en la universidad, especialmente en el uso de Oracle Database. La idea es combinar una API REST con una base de datos relacional robusta, aplicando conceptos de arquitectura backend, validaciones de negocio y diseño de soluciones escalables.

Además, me interesa seguir llevando este proyecto hacia un enfoque más realista del desarrollo profesional, donde la capa de aplicación no sea la única responsable de validar los datos que entran desde la API. En ese sentido, Oracle DB también juega un papel importante como capa defensiva, con posibles triggers, stored procedures y funciones para validar información, reforzar integridad y centralizar reglas del negocio en la base de datos.

La intención es continuar ampliando este proyecto para ir conectando más conceptos de base de datos con lógica de backend, integrando buenas prácticas de validación, diseño de modelos y desarrollo orientado a sistemas reales.