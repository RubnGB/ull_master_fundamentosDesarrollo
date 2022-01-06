# Entrega 3
## Eventos y movimiento rectilineo

En esta práctica se ha implementado una aplicación en Unity3D que cumple los siguientes requisitos:
- Implementar una UI que permita configurar con qué poder jugarás: turbo y restas una vida o normal.
- Agregar a tu escena un objeto que al ser recolectado por el jugador desplace del juego un tipo de objetos que puedan representar obstáculos, a modo de barreras que abren caminos.
- Agrega un objeto que te teletransporte a otra zona de la escena.
- Agrega un objeto físico que muevas con las teclas wasd.
- Agrega un personaje que se dirija hacia un objetivo estático en la escena.
- Agrega un personaje que se dirija al objeto del apartado 4.

He creado un menú inicial en el que si elijes el modo normal, el parámetro power tendrá como valor 5 y el valor health tendrá valor 3, mientras que si elijes el modo turbo, power será 8 y health será 2.

En la escena de juego hay distribuidos tres cubos que no se pueden mover hasta que sus respectivas "llaves" sean recogidas.
Estas llaves son las esferas, cada una tiene un color que permite al jugador mover el cubo correspondiente. De forma similar, se puede conseguir que en vez de mover los cubos fueran destruídos.

Hay dos portales que permiten al jugador teletransportarse entre ellos.

El objeto que se mueve con las teclas wasd es el PlayerController y hay dos enemigos.
El enemigo del fondo perseguirá sin descanso el cubo azul y el otro enemigo perseguirá al personaje en movimiento.
El script que tienen asociados ambos enemigos es el mismo, lo único que cambia es el objetivo de destino que se puede modificar desde el inspector.

![alt text](https://github.com/RubnGB/ull_master_fundamentosDesarrollo/blob/main/Practica3/gif_animation_03.gif)

## Descripción de los scripts

### MainMenu.cs
En este script asociado a un GameObject vacío que colocaremos en la escena del menú, hay dos métodos que son llamados en función del botón que se pulsa. Ambos llevan a la escena principal del juego, pero si el elegido es el modo turbo, se modifican los valores de poder y salud.
*Hay un problema que no he conseguido resolver y es que al cambiar de una escena a otra desde el menú, la escena principal se ve con colores más oscuros de lo normal. Si el juego se inicia desde la escena principal esto no ocurre.

### PlayerController.cs
He creado desde 0 el movimiento del personaje para entender mejor su funcionamiento. 
Desde el inspector se puede modificar la velocidad de desplazamiento del jugador, la fuerza de la gravedad y el impulso al saltar.
Además, el movimiento se realiza respecto a la cámara.

### PushRigidBody.cs
En este script vinculado al jugador, controlamos el comportamiento de los objetos al colisionar con el personaje. Solamente desplazaremos aquellos objetos que tengan Rigidbody y además que no sean kinemáticos.
Desplazaremos los objetos con una fuerza inicial de 5 (salvo en el modo turbo que la fuerza inicial es de 8), esta fuerza será incrementada cada vez que la puntuación obtenida por empujar objetos suba 100 puntos.
Cada vez que choquemos con un objeto se sumarán 10 puntos (esta cantidad se puede modificar desde el inspector).
Se ha añadido en este script la limitación de que para mover determinados objetos es necesario haber obtenido previamente un objeto y el código necesario para que el personaje se teletransporte a través de los portales.

### FollowPlayerEvent.cs
En este script que tendremos que asociar a cada enemigo, haremos que miren continuamente hacia su objetivo (se elige en el inspector, y pueden ser tanto objetos dinámicos como el jugador u objetos estáticos) y que se muevan hacia ellos con una velocidad que se puede modificar.
Se ha añadido un límite que también se puede cambiar en el inspector para que no se produzca jittering y que el enemigo se pare cuando esté a una determinada distancia del objetivo.
Debido a que se ha usado un prefab con animaciones, cuando el jugador se mueve cambiando la dirección a la que tiene que mirar el enemigo, se queda parado unos segundos. Si se usan enemigos sin animaciones no aparece este problema, pero lo he dejado sin modificar en el vídeo
porque así me daba tiempo de mostrar el resto de funcionalidades sin interrupciones. 

### ScoreTextScript.cs, PowerTextValue.cs y HealthTextScript.cs
Aquí simplemente actualizamos el valor del Canvas para que se muestren los valores de puntuación, fuerza y salud. Estos scripts estarán asociados a los textos correspondientes del canvas.
