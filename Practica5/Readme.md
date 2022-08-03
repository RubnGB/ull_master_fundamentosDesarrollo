# Entrega 5
## Introducción a la programación de juegos 2D. Sprites.

En esta práctica se ha implementado una aplicación en Unity2D que cumple los siguientes requisitos:
- Obtener assets que incorpores a tu proyecto: Sprites individuales y Atlas de Sprites.
- Incorporar los recursos del punto 1 en el proyecto y generar al menos 2 animaciones para uno de los personajes.
- Busca en el inspector de objetos la propiedad Flip y comprueba qué pasa al activarla desactivarla en alguno de los ejes.
- Mover uno de los personajes con el eje horizontal virtual que definen las teclas de flechas.
- Hacer saltar el personaje y cambiar de dirección cuando se pulsa la barra espaciadora.
- Crear una animación para otro personaje, que se active cuando el jugador pulse la tecla x.
- Agregar un objeto que al estar el personaje a una distancia menor que un umbral se active una animación, por ejemplo explosión o cualquier otra que venga en el atlas de sprites.

En primer lugar obtuve assets que incluían sprites individuales y un atlas de sprites de este enlace: https://rvros.itch.io/pixel-art-animated-slime 
Practiqué con los distintos tipos de división de los sprites del atlas, pero finalmente hice uso de los sprites individuales para generar las animaciones porque ya te indicaban con sus nombres qué sprite se correspondía con qué animación (aunque hay que tener presente que usar los sprites obtenidos del atlas es más óptimo).

Después me creé un GameObject vacío al que le asocié el componente Sprite Renderer con el sprite idle_0 y un GameObject 2D Square con un componente Box Collider 2D para que ejerciera de plataforma.
Con los sprites generé varias animaciones: idle, move, die y attack(aunque solo he usado las tres primeras). Al arrastrar los sprites correspondientes para generar la animación, si lo hacemos sobre el GameObject, genera automáticamente el componente Animation y el controlador de animaciones para dicho GameObject.

La propiedad flip lo que hace es girar el sprite 180º sobre el eje X o sobre el eje Y, es decir, si el sprite está mirando hacia la izquierda y marcamos la X se pondrá a mirar hacia la derecha, mientras que, si marcamos la Y, lo que hará será ponerse boca abajo.

Para mover el personaje de izquierda a derecha (sin hacer uso de físicas, con el transform porque así lo indicaba la práctica), lo que tenemos que hacer es aplicar al transform una traslación del eje horizontal (poniendo a 0 el eje Y del Vector2). Sin embargo, como también queremos que el personaje salte tenemos que usar una aceleración y la fuerza de la gravedad en la Y del Vector2. Además, para que no atraviese el suelo, es conveniente que hagamos un Raycast hacia el suelo. De esta forma, cuando llegue a una determinada distancia, el personaje se detendrá y no atravesará el suelo. 

Para que el personaje cambie de dirección solamente al saltar, la condición tendrá que comprobar que la tecla espacio esté pulsada, y cuando esto ocurra, habrá que cambiar el signo de positivo a negativo y viceversa de la escala en el eje X.

Para usar las animaciones hay que ir al controlador de animaciones, crear parámetros y condiciones y luego en el código principal referenciar al controlador para así acceder al parámetro y poder modificar su valor. 
En el caso del slime azul, queremos que cambie de la animación idle (que es la que he puesto por defecto) a la animación de movimiento: crearemos un parámetro de tipo float llamado speed, y pondremos como condición en la transición hacia slimeMove que sea mayor a 0.01 y en la transición de vuelta a slimeIdle que sea menor a 0.01.
En el código cambiaremos el valor del parámetro speed según el movimiento en el eje horizontal (en valor absoluto).

En el caso del slime rojo queremos que se active una animación (slimeHurt) cuando el jugador pulse la tecla X, haremos una transición hacia dicha animación desde la animación por defecto (slimeIdle) y otra de vuelta cuando la tecla X no esté pulsada: esto tenemos que hacerlo en el controlador asociado a este slime rojo. Para ello, nos crearemos un parámetro llamado isXPressed de tipo boolean que será el que nos ayudará a pasar de una animación a otra a través de una condición en el Animator.
En el código haremos que el valor de isXPressed sea modificado en función de si la tecla está pulsada o no, y para hacer esto es necesario referenciar al Animator del slimeRed y así cambiar el valor del parámetro.

Finalmente, haremos que cuando el slime azul esté a una determinada distancia del slime rojo, este último explote.
Lo haremos de forma similar a la animación anterior, pero esta vez usando un parámetro de tipo float llamado isNear. Para que la animación de muerte se realice solo una vez y el slime rojo se quede en dicha animación, desmarcaremos el loop time para que esta animación solo se ejecute una vez y no trazaremos una transición de vuelta hacia ninguna otra animación.
En el código BasicMovement simplemente referenciaremos al otro GameObject (redSlime) para calcular la distancia entre los dos objetos (restamos la posición en el eje x de ambos objetos y le aplicamos el valor absoluto para no obtener valores negativos).

![alt text](https://github.com/RubnGB/ull_master_fundamentosDesarrollo/blob/main/Practica5/gif_animation_05.gif)
