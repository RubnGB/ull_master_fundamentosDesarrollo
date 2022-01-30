# Entrega 4
## Sistema de Waypoints de Unity

En esta práctica se ha implementado una aplicación en Unity3D que cumple los siguientes requisitos:
- Importar el paquete Utility de los Standard Assets de Unity, concretamente los scripts WaypointCircuit.cs y WaypointProgressTracker.cs
- Crear un circuito
- Hacer que un personaje recorra dicho circuito

En primer lugar tuve que solucionar un problema que aparecía en el script WaypointCircuit respecto al tamaño del array. Simplemente hay que cortar la línea 257: var item=items.GetArrayElementAtIndex(i); y pegarla en la línea 273, entre el else y el if(n==0)

Una vez hecho esto, creamos un gameObject vacío, le añadimos como component el script WaypointCircuit.cs y creamos en este objeto tantos objetos hijos como puntos queremos que tenga el circuito
Posteriormente, tenemos que pulsar en el component del script, "assign using all child objects" para que integrarlos en el script.
Al personaje que queremos que recorra el circuito tendremos que añadirle el script WaypointProgressTracker, seleccionando como circuit el objeto vacío creado previamente.
Para finalizar, no se nos puede olvidar asignar como objeto target al propio personaje que queremos que recorra el circuito.

En el caso de que quiera que visualmente se vean los waypoints, puedo asignar a cada uno de los puntos del circuito una figura geométrica

![alt text](https://github.com/RubnGB/ull_master_fundamentosDesarrollo/blob/main/Practica3/gif_animation_04.gif)
