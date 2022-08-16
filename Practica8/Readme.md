# Entrega 8
## Técnicas. Scroll, pool de objetos y parallax

## Inicio
En esta práctica se ha implementado una aplicación en Unity2D en la que he reutilizado los assets de Warped Caves. 
Sin embargo, tienen el inconveniente de que el background y el middleground tienen tamaños distintos y podría ocurrir que al moverse con efecto parallax se vieran los huecos que quedan entre los espacios del background (que es más pequeño que el middleground). Para solucionar este problema he aumentado la escala del background a 1,1. 
Aunque al no tener exactamente el mismo tamaño hace que el scrolling de un pequeño salto (esto se solucionaría haciendo que los fondos tuviesen exactamente el mismo ancho o insertándolos como texturas en quads). 
También he creado una imagen para que ejerza de suelo al que le he añadido un collider y un rigidBody dinámico con gravedad 0 para que el personaje no atraviese el suelo. Además, para que el suelo no tenga comportamientos extraños he congelado sus constraint en los tres ejes.
A continuación, he hecho dos copias de los fondos y el suelo y los he colocado a la izquierda y derecha del fondo central.
Por último, he añadido al personaje con las animaciones y el script de movimiento de prácticas anteriores.

## Scroll con movimiento del personaje. El fondo se repite hasta que pare el juego.
Para que la cámara se mueva junto al personaje de forma infinita hasta que se pare el juego, he añadido el paquete Cinemachine para crear una cámara virtual 2D que se encargue de seguir al player. Además, para que veamos claramente qué es lo que está ocurriendo mientras el juego se está ejecutando, es una buena práctica que tengamos visible al mismo tiempo la pantalla de juego y la de la escena. También he puesto la relación de aspecto a 16:9, pero se puede poner el valor que queramos.
Ahora vamos a crear un script llamado backgroundScrolling.cs en el que vamos a controlar este scroll infinito y que asociaremos a todos los fondos. El fondo no se mueve, lo que se hace es que cuando la cámara se salga del límite del fondo reubicaremos el origen del fondo que esté más alejado de la cámara a esa posición.
Para ello es necesario conocer el ancho del Sprite, usando la función bounds.size nos devuelve el vector con el ancho y el largo del Sprite. Como lo que necesitamos ahora mismo es solamente el ancho porque el scroll va a ser solamente horizontal, nos quedaremos con el eje X de la siguiente forma: GetComponent<SpriteRenderer>().bounds.size.x; y lo almacenaremos en una variable de tipo float que hemos llamado spriteWidth.
Si la posición del fondo (transform.position.x) más el ancho del fondo (spriteWidth) es menor a la posición de la cámara (cameraTransform.position.x), habrá que desplazar el fondo una cantidad igual a su ancho, es decir, transform.Translate(new Vector3(spriteWidth, 0,0)).
En el caso de que la cámara llegue al borde izquierdo en lugar de al derecho, habría que comprobar si la posición del fondo menos el ancho es mayor a la posición de la cámara. En tal caso, se haría un transform.Translate(new Vector3(-spriteWidth, 0,0)).

![alt text](https://github.com/RubnGB/ull_master_fundamentosDesarrollo/blob/main/Practica8/gif_animation_08a.gif)

## Scroll con movimiento del fondo. El personaje salta sobre objetos que aparecen en la escena.
Voy a hacerlo en dos partes, en primer lugar haré que se mueva el fondo hasta que el juego se detenga y más adelante, en el apartado del pool, haré que aparezcan los enemigos.

A diferencia del anterior modo, en este, para poder mover los fondos, hemos tenido que añadir a los fondos background y middleground un rigidbody 2D cinemático con las coordenadas Z e Y congeladas y un box collider 2D is Trigger para que no se solape con el jugador.
El desplazamiento del fondo cuando la cámara llega al borde es exactamente igual al anterior ejemplo, lo único que cambia es que en vez de mover la cámara, lo que se mueve es el fondo aplicando una velocidad al Rigidbody en el eje X de -1 para que el fondo se mueva hacia la izquierda (si queremos que se mueva hacia la derecha sería 1).
Para evitar que también se desplace el suelo he hecho uso de los tags para que si es Grounded no se produzca la acción de desplazamiento.
Para cambiar entre un modo de scroll y otro y otro tipo de configuraciones que van a afectar a diversos elementos del juego me he creado un GameObject vacío al que he llamado GameManager. Este GameManager tendrá asociado un script en el que iremos añadiendo diversas variables para controlar aspectos del juego como el modo de scroll, si van a aparecer enemigos o no, la velocidad del scroll, etc.
Por ejemplo, si el modo de scroll es 1 (movimiento de fondos), la cámara virtual será desactivada y en el script de movimiento del player haremos que se deshabilite el movimiento horizontal.

![alt text](https://github.com/RubnGB/ull_master_fundamentosDesarrollo/blob/main/Practica8/gif_animation_08b.gif)

## UI
Antes de crear los enemigos vamos a configurar la UI del juego. Por un lado, tendremos un valor numérico que ejercerá de puntuación y que se irá incrementando cada vez que superemos a un enemigo. Por otra parte, tendremos una pantalla de Game Over si el jugador se choca con un enemigo.
Para crear esta interfaz añadimos un canvas que se renderice en el espacio de la cámara y que se escale según el tamaño de la pantalla. Le añadimos un Text Mesh Pro (importándolo), le damos el tamaño que deseemos y lo colocamos en la esquina superior derecha (con shift+alt al seleccionar el pivote correspondiente desplaza automáticamente el texto a dicha posición).
Vamos a crear otro texto con Game Over para cuando el jugador sea eliminado y lo colocaremos en la parte superior central de la escena y lo desplazaremos para que quede un poco más centrado.
Una vez tengamos este texto lo deshabilitaremos para que solo sea habilitado cuando el jugador sea derrotado. Para ello, en el GameManager vamos a añadir una función con nombre GameOver que pueda ser llamada desde otros scripts para que active el texto y además ponga a true una variable creada en el GameManager llamada isGameOver.
Esta variable nos va a servir para el siguiente paso, que es reiniciar el juego una vez hayamos sido derrotados. Crearemos otra función, llamada RestartGame, que lo que hará será reiniciar la escena en la que nos encontramos actualmente. 
Si tuviésemos varias escenas y quisiéramos que al reiniciar volviera al principio del juego en vez de al principio del nivel actual, lo que tendríamos que hacer es especificar el índice de la escena a la que queremos volver.
Como no es el caso y simplemente queremos reiniciar la escena activa, podemos obtener el índice de la escena actual con SceneManager.GetActiveScene().buildIndex.
Para poder utilizar estas funciones es imprescindible importar SceneManagement con: using UnityEngine.SceneManagement;
Ahora ya podemos decir que si la partida se ha terminado y pulsamos por ejemplo la tecla Enter (Return), que ejecute la función RestartGame.

## Puntuación y GameOver
Cuando el jugador salte por encima del enemigo (o pase por debajo de él si es un enemigo aéreo), sumará un punto al contador que aparece en la esquina superior derecha. Para ello vamos a crear un nuevo box collider 2D en el enemigo (ya tenía uno que servía para colisionar con el jugador) y lo ponemos encima del enemigo haciéndolo is Trigger. De esta forma, el jugador puede atravesar dicho collider y se activará el correspondiente evento de trigger.
Ahora vamos a programar el incremento de la puntuación en el GameManager:
- Creamos una variable de tipo int
- Creamos una función que se encargará de incrementar la variable en 1 cada vez que sea llamada
- Referenciamos TMPro: using TMPro;
- Asignamos en la función el valor de la puntuación al texto (haciendo un casting a string)
- Llamamos a dicha función desde el enemigo a través del evento OnTriggerExit2D(). Usamos Exit en lugar de Enter porque el Enter dependiendo del tamaño de los Collider puede llamar más de una vez a la función de incremento al activarse, mientras que Exit solo va a hacerlo una vez. Por otra parte, para que solamente cuente la puntuación cuando interactúe con el jugador, pondremos una condición en la que el nombre del objeto con el que colisiona tenga que ser Player

Cuando el jugador colisione con un enemigo morirá, y para ello tendremos que hacer varias cosas:
- Crear un tag para asociarlo a todos los enemigos y así gestionar con ellos el evento OnCollisionEnter2D del Player
- Llamar a la función GameOver() del GameManager
- Desactivar al jugador para que desaparezca de la escena (gameObject.SetActive(false))
- Detener el scrolling
- Detener el movimiento del enemigo y su animación

Una vez que hemos podido comprobar el funcionamiento de todo el sistema respecto al enemigo, lo convertimos en Prefab arrastrando el gameObject hacia alguna carpeta del proyecto y una vez creado el prefab eliminamos al enemigo de la escena porque va a ser generado automáticamente a través de la técnica de Object Pooling.

## Pool de objetos para ir creando elementos en el juego sobre los que debe saltar el jugador evitándolos o para adquirir puntos si salta sobre ellos.
- En primer lugar creamos un objeto vacío EnemiesPool con un script asociado
- En el script referenciamos al objeto prefab creado para poder asociarlo desde el editor
- Referenciamos también al GameManager y a la clase EnemyMovementScore para poder pasarle el GameManager en tiempo de ejecución (esto es necesario porque cuando se crea el prefab se elimina la referencia al GameManager).
- Añado una variable que me permita controlar el tamaño del array
- En el Start inicializamos el array y, a través de un for, vamos añadiendo los enemigos hasta el tamaño que hemos indicado
- A continuación, lo que hacemos es desactivar los enemigos instanciados para poder elegir cuándo activarlos y que no se solapen entre ellos
- Para poder hacer que los enemigos se activen cada x tiempo, creamos dos variables, una para indicar cada cuando tiempo queremos que aparezcan enemigos (spawnTime) y otra (timeSinceLastSpawn) que irá desde 0 hasta spawnTime que se actualizará a 0 cada vez que aparezca un enemigo
- En la función SpawnEnemy pondremos a 0 la variable timeSinceLastSpawn e iremos activando uno a uno los distintos enemigos (desde counter == 0 hasta counter == poolSize)
- Ahora nos creamos una variable por si queremos modificar la posición en el eje X donde queremos que aparezca por defecto el enemigo (si quisiéramos jugar con las alturas tendríamos que crearnos variables para el eje Y)
- Por último, vamos a añadir una variable en el GameManager que active/desactive el pool de enemigos (desactivando su GameObject)

Podemos ver cómo se van activando los 5 objetos instanciados en el pool y cómo se van recolocando en la escena.
![alt text](https://github.com/RubnGB/ull_master_fundamentosDesarrollo/blob/main/Practica8/gif_animation_08c.gif)

## Efecto Parallax
Hay muchas formas de implementar el efecto parallax, que no es más que hacer que los distintos fondos se desplacen a velocidades diferentes. 
Por tanto, lo que he hecho es crear una variable booleana en el GameManager que active/desactive este efecto. Esto hace que podamos usar el efecto parallax tanto en el modo en el que se desplaza la cámara y el jugador como en el modo en el que quienes se desplazan son los fondos.

En ambos casos, la implementación se hará en el script BackgroundScrolling, y utilizaremos una variable llamada parallaxMultiplier que tomará distintos valores según el fondo al que esté asociado. La idea es que el fondo que está más alejado de la cámara tenga un desplazamiento más lento a los fondos más cercanos a la cámara, y para ello, parallaxMultiplier tendrá que tomar un valor cercano a 1, mientras que los fondos más cercanos a la cámara, para que se desplacen con mayor velocidad tendrán valores más cercanos a 0. Los valores de parallaxMultiplier siempre deben estar entre 0 y 1.

- Si estamos en el modo en el que se desplaza el personaje, lo que tenemos que hacer es crear una variable adicional para almacenar la posición de la cámara en el frame anterior.  Si restamos la posición actual de la cámara menos la posición de la cámara en el frame anterior obtenemos la cantidad de desplazamiento que se ha producido entre un frame y otro. Esta cantidad la almacenaremos en una variable llamada deltaX, y será el desplazamiento que realizará el fondo cuando nos estemos moviendo. Además, habrá que actualizar la posición del frame anterior cada vez que haya un desplazamiento para poder calcular la próxima cantidad de movimiento. Si parallaxMultiplier fuera 1, los fondos se moverían a la misma velocidad que la cámara, así que con valores cercanos a 1 los fondos se moverán más lentos respecto a la cámara porque realizan casi el mismo desplazamiento que la cámara, mientras que si fueran valores cercanos a 0, como apenas se van a mover y la cámara sí, va a verse como si se desplazaran muy rápido.
- Si estamos en el modo en el que se desplazan los fondos, es muy sencillo, lo único que tenemos que hacer es multiplicar la velocidad de desplazamiento de los fondos por parallaxMultiplier para que cada fondo se desplace a una velocidad distinta. En este caso, si parallaxMultiplier fuera 1, la velocidad de desplazamiento del fondo sería la máxima (al contrario de lo que ocurre en el otro modo). Por este motivo, se hace (1-parallaxMultiplier), para que cuanto mayor sea el valor de parallaxMultiplier, más lento sea el desplazamiento del fondo y de esta forma los valores de parallaxMultiplier tendrán el mismo comportamiento independientemente del modo de scroll.

Aquí podemos ver el efecto parallax sin enemigos y con desplazamiento del jugador
![alt text](https://github.com/RubnGB/ull_master_fundamentosDesarrollo/blob/main/Practica8/gif_animation_08d.gif)

Aquí podemos ver el efecto parallax con enemigos y con desplazamiento de los fondos
![alt text](https://github.com/RubnGB/ull_master_fundamentosDesarrollo/blob/main/Practica8/gif_animation_08e.gif)