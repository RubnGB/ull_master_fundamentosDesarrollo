# Entrega 6
## Introducción a la programación de juegos 2D. Mapa - Físicas

En esta práctica se ha implementado una aplicación en Unity2D con dos partes diferenciadas, mapa y físicas (aunque también añado un apartado respecto al movimiento por ser diferente al de la anterior práctica):
## Mapa
Respecto al mapa de juego, había que trabajar con los siguientes elementos para generar un mapa plano con obstáculos y paredes:
- Grid
- Tilemap
- Tile Palette
- Tilemap collider
- Composite collider

Para generar el mapa, se han usado los recursos de: https://assetstore.unity.com/packages/2d/characters/warped-caves-103250
Los tilesets que incluye vienen seccionados/cortados (sliced) y sin seccionar. Para poder usarlos correctamente tenemos que seccionarlos indicando las medidas en el Sprite editor. Si tuviésemos que cortarlos tendríamos que calcular cuánto ocupan las casillas (aunque lo habitual en la asset store de Unity es que ya vengan cortados o que al menos te indiquen las medidas para que los puedas cortar tú). En mi caso usaré directamente los que ya están seccionados por comodidad.
Un menú muy importante que tenemos que abrir para poder pintar nuestro escenario es el Tile Palette y lo ideal es situar este menú en uno de los laterales del editor para así poder arrastrar los elementos a la escena.
Ahora tenemos que crearnos un GameObject 2D -> Tilemap, que es el lienzo sobre el que dibujaremos los elementos del escenario (al crearlo se añade automáticamente a un objeto grid). Hay distintos tipos de tilemap, en este caso seleccionaremos un tilemap rectangular.
Para poder usar los tiles recortados y dibujar con ellos el escenario, hay que incluirlos en una paleta. He creado una carpeta para incluir las paletas que voy a crear. Para crear la paleta, podemos darle a botón derecho, 2D -> Tile Palette o bien en el propio menú de Tile Palette podemos darle a crear nueva paleta. Podremos elegir el tipo de paleta según sea el GameObject tilemap que hayamos creado.
En este caso he generado tres paletas, una llamada Grounded con pocos tiles destinados exclusivamente para el suelo, Walls con tiles para las paredes y Environment con una buena variedad de tiles para obstáculos y distintos elementos.
De todas formas, la paleta es un elemento que se puede modificar a nuestro gusto, por lo que podemos borrar aquellos tiles que no nos sean de utilidad para simplificar la paleta, o bien si vemos que nos faltan tiles podemos añadirlos posteriormente. Para poder modificar nuestra paleta podemos hacer uso de las mismas herramientas que para dibujar en nuestro lienzo pero para ello hay que dejar marcado el botón de edit que hay justo debajo de estas herramientas.
Haciendo uso de las tres paletas se han dibujado un suelo, dos paredes y un pequeño obstáculo para que el jugador tenga que saltar para superarlo.
También se han incluido en el mapa los fondos background y middleground, haciendo uso del orden de las capas, con background en el orden 0, middleground en el orden 1 y los elementos y el personaje en el orden 2.

Para que el personaje pueda interactuar con el escenario tenemos que añadir un tilemap collider 2D a nuestro tilemap. Sin embargo, esto lo que hace es añadir un collider a cada tile del tilemap, y esto no tiene mucha lógica porque solo se necesitan los colliders de los bordes del escenario.
Para hacer que solamente se añada el collider a los bordes, como si todo formara parte de un mismo conjunto, tendremos que añadir el component Composite Collider 2D y en el Tilemap Collider 2D marcar la casilla Used By Composite. Además, al añadir el Composite Collider se nos incluye un Rigidbody, que vamos a poner como estático porque no necesitamos usarlo.
Ahora tendremos que añadir al personaje un Rigidbody2D con el eje Z congelado, y un Collider, concretamente el Capsule Collider 2D, que es el que se le ajusta mejor.

## Movimiento
Ahora tenemos que programar el movimiento (y animaciones) del personaje, que, a diferencia de la anterior práctica no será a través de su transform, puesto que queremos mover un objeto físico dinámico con RigidBody, y este tipo de acciones en las que se aplican fuerzas del motor de físicas es necesario hacerlas en el FixedUpdate.
Necesitaremos algunas variables, además de instanciar los componentes Rigidbody2D y Animator al iniciar el juego:
- Variable booleana jump: nos permitirá decirle al fixedUpdate si se cumplen las condiciones necesarias para que aplique la fuerza vertical de salto. Estas condiciones serán que el jugador haya pulsado la tecla espacio y que el personaje esté tocando el suelo. A ser lógica de programación lo haremos en el Update.
- Variable de tipo float jumpForce: para controlar la fuerza de salto
- Variable de tipo float horizontalMovement: para indicar la dirección del movimiento horizontal
- Variable de tipo float speedMovement: para especificar la velocidad de desplazamiento
- Variable booleana isFacingLeft: para indicar si el personaje está mirando hacia la izquierda o no (la inicializamos a false porque en este caso el Sprite por defecto está mirando hacia la derecha). 

En el Update vamos a almacenar la dirección del movimiento horizontal en la variable horizontalMovement, y su valor absoluto en el parámetro speed, que será utilizado en el Animator para indicar la transición de Idle a Moving si es mayor a 0.01 y de Moving a Idle si es menor a 0.01.
A continuación, si estamos pulsando la tecla espacio y estamos tocando el suelo, pondremos a true la variable jump, y también a true el parámetro isJumping (en caso contrario pondremos el parámetro a false), que será utilizado en el Animator para indicar la transición desde cualquier estado a Jumping si dicha variable es true. De dicho estado hay que generar una transición hacia Idle si isJumping es false y speed es menor a 0.01 y de Jumping a Moving si isJumping es false y speed es mayor a 0.01. Finalmente llamamos a un método Flip() que explicaré al final de este apartado.

Para comprobar si está tocando el suelo o no, usaremos una variable booleana llamada isGrounded que cambiará su valor a través de los eventos OnCollisionEnter2D y OnCollisionExit2D:
- En el evento OnCollisionEnter2D vamos a hacer uso de etiquetas para que solamente ponga a true la variable isGrounded si el jugador está sobre objetos o superficies etiquetadas como suelo (Ground). 
- Cuando la colisión deje de existir, es decir, cuando se active OnCollisionExit2D, pondremos a false la variable para así evitar que el jugador salte estando en el aire.

En el FixedUpdate modificaremos la velocidad del rigidBody2D. En primer lugar modificaremos su velocidad solamente en el eje X, haciendo uso de horizontalMovement (cuando lo multiplicamos por deltaTime es para que se ejecute siempre a la misma velocidad dependiendo del tiempo en vez de depender de los FPS que sea capaz de ejecutar el ordenador), y en el eje Y dejamos sin modificar la velocidad que tenga en ese momento.
Sin embargo, si se han cumplido las condiciones para que el personaje salte, lo que haremos será dejar la velocidad en el eje X sin modificar, y aplicar una fuerza de salto en el eje Y. Además de poner a false la variable jump para que no esté saltando continuamente.

Finalmente, la función Flip lo que hace es comprobar si el Sprite del personaje está mirando hacia la izquierda o hacia la derecha para que, cuando cambie la dirección del movimiento, cambie también la orientación del Sprite en el eje X. Esto se consigue multiplicando por -1 la escala local en el eje X.

![alt text](https://github.com/RubnGB/ull_master_fundamentosDesarrollo/blob/main/Practica6/gif_animation_06a.gif)

## Físicas
Ahora vamos a ver los comportamientos de dos objetos en Unity, uno será el personaje que hemos creado y otro será un GameObject 2D correspondiente a un cuadrado. Ambos objetos tendrán su correspondientes collider y estarán en la misma capa para que puedan producirse interacciones entre ellos.
Para entender correctamente las interacciones de distintos tipos de objetos en Unity respecto a sus físicas, crearemos un script checkPhysics.cs que asociaremos a los dos objetos para ver por consola qué generan los eventos OnCollisionEnter, OnCollisionExit, OnTriggerEnter y OnTriggerExit.

Las situaciones que vamos a probar son:
- Ninguno de los objetos es físico: no se recibe ningún evento ni hay interacción entre ellos. Además, ninguno es afectado por la gravedad, por lo que se quedan en la misma posición donde fueron colocados. Para considerar como objeto no físico a un objeto que tiene Rigidbody (como el player) es necesario poner el componente como estático.
- Un objeto tiene físicas (player) y el otro no: el objeto con físicas (Rigidbody dinámico) es afectado por la gravedad, y al colisionar con el cuadrado se genera el evento OnCollisionEnter tanto en el player como en el cuadrado, impidiendo que podamos atravesar el cuadrado. Cuando dejan de colisionar (porque movemos a cualquiera de los objetos, no porque dejen de hacerlo tras la colisión) se activa en ambos el evento OnCollisionExit. Además, podemos ver que el objeto con físicas no puede mover al objeto sin físicas, sin embargo, si movemos (a través de las posiciones del transform) el cuadrado hacia el jugador sí podemos ver cómo el jugador es desplazado por el cuadrado.
- Ambos objetos tienen físicas: los dos objetos son afectados por la gravedad, reciben el evento OnCollisionEnter al colisionar y el evento OnCollisionExit al dejar de colisionar. En este caso el player puede mover al cuadrado y el cuadrado puede desplazar también al player. Además, al ejercer un desplazamiento sobre el otro objeto al colisionar, el evento OnCollisionExit se produce de forma “automática” justo después del evento OnCollisionEnter.
- Ambos objetos tienen físicas y uno de ellos tiene 10 veces más masa que el otro: el objeto que tiene mayor masa es el que ofrece más resistencia al desplazamiento. De hecho, si subimos a 100 la masa del objeto, prácticamente es imposible de ser desplazado por el objeto que tiene menor masa. En cuanto a los eventos, ambos reciben tanto el OnCollisionEnter al colisionar como el OnCollisionExit al dejar de hacerlo.
- Un objeto tiene físicas (player) y el otro es IsTrigger: el player atraviesa el cuadrado sin que este se vea afectado, es decir, ni lo desplaza ni se ve afectado por la gravedad. Sin embargo, ambos reciben el evento OnTriggerEnter cuando entraron en contacto y el evento OnTriggerExit cuando dejaron de estar en contacto.
- Ambos objetos son físicos y uno de ellos está marcado como IsTrigger (cuadrado): al igual que antes, el player atraviesa el cuadrado sin que este se vea afectado, es decir, no lo desplaza. Sin embargo, ahora el cuadrado sí se ve afectado por la gravedad. Ambos reciben el evento OnTriggerEnter cuando entraron en contacto y el evento OnTriggerExit cuando dejaron de estar en contacto. 
- Ambos son físicos y uno de ellos es cinemático: ocurre lo mismo que al colisionar un objeto físico con un no físico, es decir, solo el objeto con físicas es afectado por la gravedad, y al colisionar con el cuadrado se genera el evento OnCollisionEnter y OnCollisionExit tanto en el player como en el cuadrado, impidiendo que podamos atravesar el cuadrado. Además, podemos ver que el objeto con físicas no puede mover al objeto cinemático, pero sí al revés. Hacer que un objeto sea cinemático en lugar de no físico tiene como ventaja que podemos delegar en el motor de físicas su movimiento en vez de desplazarlo a través del transform (por ejemplo, para objetos que tengan un desplazamiento continuo como un ascensor o un transportador).


Ahora incorporaremos elementos físicos en la escena que respondan a las siguientes restricciones:
- Objeto estático que ejerce de barrera infranqueable: tanto el suelo como las paredes son Rigidbody estáticos que ejercen de barrera infranqueable
- Zona en la que los objetos que caen en ella son impulsados hacia adelante: he creado una plataforma que transporta cualquier objeto que caiga sobre ella hacia la derecha
- Dos capas asignadas a diferentes tipos de objetos para evitar colisiones entre ellos: he creado un tilemap específico para el agua y le he asignado un layer que está creado por defecto llamado Water. Tanto el player como el resto de objetos se encuentran en el layer default, por tanto, para que, no haya colisiones con los objetos a pesar de tener collider,  y caiga hacia abajo todo objeto que se pose sobre el agua, en Project Settings -> Physics 2D desmarcamos la casilla que hay entre Default y Water para que no haya colisiones entre los objetos de estas capas. 
- Objeto que es arrastrado por otro a una distancia fija: he creado un enemigo físico con su respectivo collider y una copia del mismo enemigo en color rojo, que lo seguirá a cualquier parte a una distancia fija. Para ello, en el enemigo rojo he colocado el component Distance Joint 2D a una distancia de 1.5.
- Objeto que al colisionar con otros sigue un comportamiento totalmente físico: he recolocado el cuadrado de las pruebas anteriores con Rididbody dinámico y sin congelar ninguno de sus ejes para que tenga un comportamiento físico y pueda ser empujada por el enemigo.

![alt text](https://github.com/RubnGB/ull_master_fundamentosDesarrollo/blob/main/Practica6/gif_animation_06b.gif)