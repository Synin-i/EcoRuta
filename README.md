
<div align="center">

# ♻️ Eco Ruta

### Eco Ruta: Sistema de Gestión de Logística de Recolección de Residuos 

<p align="center">
  <img src=https://raw.githubusercontent.com/Hyuukai/Readme/refs/heads/main/Isologo%20(1).png>
</p>

![C#](https://img.shields.io/badge/C%23-.NET-512BD4?style=for-the-badge&logo=csharp&logoColor=white)
![Visual Studio](https://img.shields.io/badge/Visual%20Studio-2026-5C2D91?style=for-the-badge&logo=visualstudio&logoColor=white)
![GitHub](https://img.shields.io/badge/GitHub-Repository-181717?style=for-the-badge&logo=github)
![CSV](https://img.shields.io/badge/Data-CSV%20%26%20TXT-success?style=for-the-badge)

---
## DATOS DEL PROYECTO
| Campo  | Informacion
| ------------- |:-------------:|
| Modulo      |Programación II    |
| Docente     | Lic. Andrés Grover Albino Chambi     |
|Período      |             Julio 2026  |
| Estudiantes |   Jennifer Alexa Lezama Gonzalez
|              |  Limberth Alfredo Ilaluque Diaz 
|Tecnologías  |    C# (.NET)          |

<div align="center">


## ESTRUCTURA DEL DOCUMENTO DE PROYECTO 

<div align="left">

## Índice
- [1. Introducción](#1-introducción)
  - [1.1 Contexto General](#11-contexto-general)
  - [1.2 Problemática](#12-problemática)
  - [1.3 Justificación](#13-justificación)
  - [1.4 Alcance](#14-alcance)
  - [1.5 Limitaciones](#15-limitaciones)
  - [2. Objetivos](#2-objetivos)
  - [2.1 Objetivo General](#21-objetivo-general)
  - [2.2 Objetivos Específicos](#22-objetivos-específicos)

- [3. Marco Teórico](#3-marco-teórico)
  - [3.1 Programación Orientada a Objetos (POO)](#31-programación-orientada-a-objetos-poo)
  - [3.2 Estructuras (Structs)](#32-estructuras-structs)
  - [3.3 Persistencia de Datos en Archivos Planos (CSV y TXT)](#33-persistencia-de-datos-en-archivos-planos-csv-y-txt)
  - [3.4 Matrices Bidimensionales](#34-matrices-bidimensionales)
  - [3.5 Manejo de Excepciones y Validación de Datos](#35-manejo-de-excepciones-y-validación-de-datos)

- [4. Desarrollo del Proyecto](#4-desarrollo-del-proyecto)
  - [4.1 Descripción General del Sistema](#41-descripción-general-del-sistema)
  - [4.2 Estructura del Sistema](#42-estructura-del-sistema)
  - [4.3 Gestión de Puntos de Recolección](#43-gestión-de-puntos-de-recolección)
  - [4.4 Creación y Validación de Rutas Logísticas](#44-creación-y-validación-de-rutas-logísticas)
  - [4.5 Matriz de Cobertura y Control Semanal](#45-matriz-de-cobertura-y-control-semanal)
  - [4.6 Reportes y Estadísticas](#46-reportes-y-estadísticas)

- [5. Tecnologías Utilizadas](#5-tecnologías-utilizadas)

- [6. Conclusiones y Recomendaciones](#6-conclusiones-y-recomendaciones)
  - [6.1 Conclusiones](#61-conclusiones)
  - [6.2 Recomendaciones](#62-recomendaciones)
  
<a id="introduccion"></a>

# 1. Introducción


## 1.1 Contexto General

La gestión de residuos sólidos urbanos en entornos de alta complejidad geográfica como la ciudad de La Paz requiere herramientas de optimización logística precisas. El control de rutas de recolección y la medición del impacto ambiental son pilares fundamentales para garantizar ciudades sostenibles.

## 1.2 Problemática

En la ciudad de La Paz, la planificación tradicional de rutas de recolección de basura se realiza frecuentemente sin un control estricto de distancias ni capacidades operativas reales, lo que genera un consumo excesivo de combustible, mayores emisiones de gases contaminantes y dificultades para supervisar la cobertura real por zonas y días de la semana. 

## 1.3 Justificación

El desarrollo de una herramienta tecnológica especializada como Eco Ruta se justifica en la necesidad de automatizar el control logístico, garantizar el cumplimiento de umbrales eco-amigables (máximo 40 km por ruta) y facilitar la toma de decisiones gerenciales mediante reportes estadísticos estructurados.

## 1.4 Alcance

El sistema abarca la administración modular de puntos de recolección en zonas clave de La Paz (Sopocachi, San Pedro, Miraflores, San Jorge y Obrajes), la estructuración de rutas logísticas, el control mediante matriz de cobertura semanal (5x7) y la persistencia de datos en archivos planos CSV y TXT.

## 1.5 Limitaciones

El software opera como una aplicación de consola en entorno local, limitando su interacción concurrente en red y dependiendo de la correcta estructuración manual o automática de los archivos planos en el directorio base configurado.

## 2. Objetivos
## 2.1 Objetivo General

Desarrollar una aplicación de consola que permita gestionar, monitorear y optimizar rutas de recolección de residuos para juntas vecinales y cooperativas locales, utilizando estructuras unidimensionales y bidimensionales para el almacenamiento y procesamiento de métricas operativas, junto con mecanismos de persistencia en archivos para generar reportes históricos de eficiencia. El sistema busca reducir la huella operativa, evitar rutas superpuestas y fomentar la trazabilidad en la frecuencia del servicio, alineándose con principios de sostenibilidad digital y eficiencia algorítmica.

## 2.2 Objetivos Específicos

-	Registrar y validar puntos de recolección considerando ID, zonas de La Paz, capacidad en kilogramos y frecuencia semanal. 
-	Implementar un sistema robusto de validación de entradas por consola mediante bucles y tipos de datos seguros (TryParse). 
-	Establecer y validar un umbral eco-amigable estricto de 40.0 kilómetros por ruta para reducir la huella de carbono. 
-	Controlar el uso único de los puntos de recolección para evitar asignaciones duplicadas en múltiples rutas. 
-	Gestionar una matriz bidimensional de cobertura (5 zonas por 7 días de la semana) para el seguimiento operativo. 
-	Generar y exportar reportes estadísticos generales y detallados a archivos de texto formateados (.txt). 
-	Mantener la persistencia de los datos mediante archivos estructurados en formato CSV. 

## 3. Marco Teórico
## 3.1 Programación Orientada a Objetos (POO)
Paradigma de programación basado en la estructuración del código en clases, structs, atributos y métodos que modelan entidades del mundo real. 
## 3.2 Estructuras (Structs)
Tipos de valor en C# utilizados para agrupar datos relacionados (como PuntoRecoleccion y Ruta), garantizando un diseño eficiente e inmutable mediante constructores especializados. 
## 3.3 Persistencia de Datos en Archivos Planos (CSV y TXT)
Técnica de almacenamiento estructurado mediante texto delimitado por comas (CSV) para registros tabulares y archivos de texto plano (TXT) para la emisión de reportes institucionales. 
## 3.4 Matrices Bidimensionales
Estructuras de datos matriciales en memoria utilizadas para representar relaciones cruzadas, como el control de cobertura de 5 zonas frente a los 7 días de la semana. 
## 3.5 Manejo de Excepciones y Validación de Datos
Mecanismos de control de errores de ejecución (como FileNotFoundException o UnauthorizedAccessException) y métodos de reintento con TryParse para garantizar la robustez de la interfaz de consola. 

## 4. Desarrollo del Proyecto
## 4.1 Descripción general del sistema
El sistema desarrollado en C# es una aplicación de consola modular orientada a resolver problemas logísticos reales en la ciudad de La Paz. Permite administrar puntos de recolección de residuos, estructurar rutas operativas considerando restricciones ecológicas de distancia, y llevar un registro matricial del servicio brindado a lo largo de la semana. La arquitectura del software prioriza la separación de responsabilidades, aislando los modelos de datos, la lógica de negocio, la persistencia en archivos y la interfaz de usuario. 
##  4.2 Estructura del sistema
El sistema está organizado mediante clases y estructuras especializadas que representan las entidades del dominio: 
- PuntoRecoleccion (Struct): Almacena los atributos fundamentales de cada punto, tales como ID, Zona, CapacidadKg, FrecuenciaSemanal y Activo, incorporando validaciones internas en su constructor. 
-	Ruta (Struct): Define las características de las rutas logísticas, incluyendo ID, Nombre, Arreglo de Puntos Asignados, DistanciaEstimadaKm y Fecha de Creación. 
-	MenuConsola: Orquesta la interfaz de usuario interactiva en la consola con menús visuales basados en códigos de color ANSI. 
-	AlgoritmosRutas: Gestiona la matriz bidimensional de cobertura (zonas por días de la semana) y procesa los estados del servicio. 
-	Configuracion: Centraliza las rutas de acceso a los archivos de datos y garantiza la existencia de la estructura de directorios base de manera portable. 
-	Validadores: Proporciona métodos estáticos seguros para la captura de entradas por consola mediante bucles de validación. 

##  4.3 Gestión de puntos de recolección
El sistema permite al operador registrar nuevos puntos de recolección ingresando identificadores numéricos, nombres de zonas de La Paz (tales como Sopocachi, San Pedro, Miraflores, San Jorge u Obrajes), capacidades de carga en kilogramos, frecuencias semanales y estados de activación, los cuales se almacenan de manera persistente en archivos CSV. 
<a id="Creación y validación de rutas logísticas"></a>
##  4.4 Creación y validación de rutas logísticas
Para la creación de rutas, el sistema verifica la existencia previa de puntos activos y evalúa que los puntos seleccionados no hayan sido asignados previamente a otras rutas. Incorpora una validación estricta del umbral eco-amigable de 40.0 Km; si una ruta supera este límite, el sistema emite una alerta visual en rojo y solicita confirmación explícita al operador para evaluar su viabilidad ambiental. 
##  4.5 Matriz de cobertura y control semanal
A través de una matriz de dimensiones 5x7 (5 zonas principales y 7 días de la semana), el sistema permite registrar de forma interactiva el paso de los vehículos recolectores, actualizando el estado de la cobertura y permitiendo identificar rápidamente qué días o zonas aún tienen servicios pendientes o si se ha alcanzado el 100% de la cobertura semanal. 
##  4.6 Reportes y estadísticas
El generador de reportes calcula métricas globales de infraestructura, tales como el total de puntos registrados, puntos activos, capacidad máxima de recolección en kilogramos, zonas únicas cubiertas, total de visitas realizadas y la basura total recolectada desglosada por cada ruta operativa. Adicionalmente, permite exportar toda esta información en un archivo de texto formateado. 
##  5. Tecnologías Utilizadas
-	Lenguaje: C# (.NET) 
-	Paradigma: Programación Orientada a Objetos y Estructurada 
-	Entorno de Desarrollo (IDE): Visual Studio 
-	Control de versiones: Git y GitHub 
-	Persistencia de datos: Archivos planos CSV y TXT 

##  6. Conclusiones y Recomendaciones
## 6.1 Conclusiones
El desarrollo de Eco Ruta demostró la viabilidad de aplicar principios de ingeniería de software modular en consola para resolver problemáticas logísticas locales en La Paz. La inclusión del umbral de 40 km y la matriz de cobertura 5x7 aporta un enfoque ecológico y de control estricto que optimiza las operaciones de recolección de residuos.
##  6.2 Recomendaciones
Se recomienda a futuro migrar la persistencia de archivos planos a una base de datos relacional (como SQLite o SQL Server) para mejorar la escalabilidad, así como desarrollar una interfaz gráfica de usuario (GUI) o web que facilite la visualización geoespacial de las rutas en la topografía paceña.
