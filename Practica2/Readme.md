# Entrega 2
## Programación de Scripts en C#

En esta práctica se ha implementado una aplicación en Unity3D que cumple los siguientes requisitos:
- Todos los objetos pueden ser formas básicas (cubos, esferas, cilindros).
- Los objetos se distribuyen por la escena y se catalogan en dos tipos, en movimiento rectilíneo y estáticos.
- Algunos objetos son estáticos, el jugador cuando colisiona, los desplaza una cantidad proporcional a su poder.
- Otros objetos al colisionar se desplazan por la fuerza que ejerce el jugador sobre ellos y hacen que el jugador sume puntos.
- Cuando el jugador suma puntos, las dimensiones del objeto disminuyen y se atenúa su color, cuando se llega a un umbral desaparece el objeto.

Concretamente he distribuído por la escena cubos, cilindros y esferas con las siguientes diferencias:
- Los cubos son objetos estáticos que disminuyen su tamaño y atenúan su color al ser colisonados. Cuando llegan a un determinado umbral el objeto desaparece
- Las esferas son objetos con movimiento rectilíneo que atenúan su color al ser golpeados
- Los cilindros son objetos estáticos que no disminuyen su tamaño ni atenúan su color. Están colocadas para poder observar con mayor facilidad la diferente fuerza aplicada dependiendo del nivel de poder del jugador.

Todos los objetos suman puntos al ser golpeados y cada 100 puntos conseguidos se aumenta en 1 la fuerza del jugador.

![alt text](https://github.com/RubnGB/ull_master_fundamentosDesarrollo/master/Practica2/gif_animation_02.gif)

##Descripción de los scripts

###PlayerController.cs
He creado desde 0 el movimiento del personaje para entender mejor su funcionamiento. 
Desde el inspector se puede modificar la velocidad de desplazamiento del jugador, la fuerza de la gravedad y el impulso al saltar.
Además, el movimiento se realiza respecto a la cámara.

###PushRigidBody.cs
Aquí controlamos el comportamiento de los objetos al colisionar con el personaje. Solamente desplazaremos aquellos objetos que tengan Rigidbody y además que no sean kinemáticos.
Desplazaremos los objetos con una fuerza inicial de 3, esta fuerza será incrementada cada vez que la puntuación obtenida por empujar objetos suba 100 puntos.
Cada vez que choquemos con un objeto se sumarán 10 puntos (esta cantidad se puede modificar desde el inspector).
Dependiendo del tipo de objeto colisionado este tendrá un comportamiento u otro de los mencionados anteriormente.

###RectilinearMovement.cs
En este script controlamos la velocidad de desplazamiento (speed) y la longitud de desplazamiento (steps) de un objeto con movimiento rectilíneo.
Cuando el objeto alcanza la longitud máxima de desplazamiento aplicamos una rotación para que cambie su trayectoria. Si por ejemplo es de 180º volverá hacia atrás, y si es de 90º hará un recorrido en forma de cuadrado.

###ScoreScript.cs
Aquí simplemente actualizamos el valor del Canvas para que se muestren los valores tanto de la puntuación como de la fuerza