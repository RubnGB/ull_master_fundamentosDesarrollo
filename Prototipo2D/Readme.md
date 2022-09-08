# Prototipo 2D
Este juego es un prototipo de metroidvania en el que, a lo largo de tres fases, tendrás que ir recuperando tus poderes perdidos para poder avanzar de nivel.
A continuación se irán detallando los aspectos técnicos de cada una de las escenas.


## Menú de inicio y Scene Loader
En el menú hay tres botones que llevarán al inicio del juego, a cargar la última partida (en una versión futura) y a salir del juego.
También se ha incluido un audio de fondo que suena mientras estamos en el menú.
Se ha programado un script en el canvas, MainMenu, para controlar el comportamiento de los botones. Como el cambio entre escenas es algo común a realizar en todas las escenas, se ha realizado un script específico (SceneLoader) que será adjuntado a aquellos objetos que lancen el cambio de escena. En el caso específico del menú de inicio, se ha tenido que tener en cuenta que tomará los valores de las variables de MainMenu según el botón que se pulse.

En el script SceneLoader es imprescindible usar la librería SceneManagement y la función SceneManager.LoadScene(“nombreEscena”). 
Además, si queremos cerrar el juego al pulsar Quit tendremos que llamar a la función Application.Quit(), pero esto solo funcionará cuando el juego esté compilado. Para simular esto, lo que podemos hacer es usar la librería UnityEditor y la poner a false la variable EditorApplication.isPlaying.
También se ha añadido el sonido de un láser cuando pulsamos cualquiera de los tres botones. Al probarlo podemos ver que las distintas acciones las realiza demasiado rápido, tanto que no da tiempo ni de que suene el sonido completo del láser. 
La mejor forma de solucionarlo es a través de transiciones entre escenas y haciendo uso de corrutinas.

### Transiciones y corrutinas
Podríamos hacerlo directamente en el canvas que tenemos ya creado para el menú principal. Sin embargo, como esta transición es algo que va a ocurrir siempre que se cambie de escena (y los botones del menú no queremos que aparezcan siempre), vamos a crear un canvas específico para la transición dentro de este otro canvas y lo convertiremos en un prefab que reutilizaremos en otras escenas.
Dentro de este canvas creamos una imagen que haremos que cubra todo el espacio del canvas y le daremos el color negro.
Al tener este canvas por delante del canvas del menú, nos impediría interactuar con el menú. Para que esto no ocurra, deshabilitamos o eliminamos el componente Graphic Raycaster del canvas con el fondo negro.
Después, tenemos que añadir una animación al canvas para que cambie de negro a transparente (fade out) y de transparente a negro (fade in).
A continuación, para controlar la transición iremos al Animator Controller, nos crearemos un parámetro de tipo trigger y lo asociaremos a la transición entre FadeOut y FadeIn.

Ya podemos controlarlo a través del código, pero para que no ocurra de forma instantánea, usaremos corrutinas en SceneLoader para que espere un segundo. 
Como he dicho previamente, tendremos dos formas de cambiar de escena. Primero, si la escena actual es el menú principal, en cuyo caso se comprueba qué hacer en función del botón pulsado. Mientras que, en cualquier otra escena, lo que haremos siempre será asociar este script a un determinado collider para que cuando el jugador lo toque, cambie a la escena correspondiente.

## Nivel 1.
En este nivel se ha implementado un scroll background con efecto parallax de dos formas:
- Desde que inicia el nivel hasta que el personaje se acerca al power up, lo que se moverá será la cámara. 
- Cuando el jugador toque el icono de PowerUp, la cámara estará fija, se moverá el fondo y el personaje tendrá que saltar para esquivar a 5 enemigos que empezarán a aparecer a través de un pool de objetos

Cuando acabe este reto, la cámara y el scroll se detendrán para que el personaje avance hacia el final del nivel. Además, el jugador no podrá saltar hasta obtener el PowerUp, y una vez que lo consiga, dicha habilidad se mantendrá activa para el resto del juego. 
Se han hecho pequeñas modificaciones en el script BackgroundScrolling respecto a prácticas anteriores para que se pudiera cambiar entre un modo de scroll y otro en tiempo de ejecución (previamente solo era posible al inicio del juego desde el GameManager) y también se ha ajustado un poco la distancia de traslación de los fondos para que no se notara el efecto en la cámara del jugador.

### DialogSystem
En el juego pueden aparecer dos tipos de diálogo, uno que emitan las señales cuando el jugador esté cerca de ellas y pulse la tecla de disparo para leerlas y otro que se activa automáticamente cuando ocurre un determinado evento, como por ejemplo que se inicie el reto de los enemigos.
Para ello se ha creado un GameObject Canvas que contendrá un panel con texto TMP que se encuentra deshabilitado y solo se habilitará cuando el script DialogSystem asociado a los diversos objetos lo indique.

Si el objeto que contiene el script es un letrero (sign) lo que hace es que si el jugador está dentro de la zona de influencia del letrero y pulsa la tecla de disparo (botón izquierdo del ratón) muestra el texto. Se ha añadido una etiqueta al jugador para que este evento solo se lance cuando sea el jugador el que esté cerca del letrero y no cualquier otro elemento.
Si el objeto que contiene el script no es un letrero, tendrá que esperar a que dicho objeto lance una señal para que se active el panel de texto.
Para añadir el texto que queremos que se muestre se ha creado una variable de tipo array de string para poder escribir desde el inspector las líneas de texto que se deseen.
Por otro lado, el texto no aparecerá de forma instantánea, sino que se irá mostrando carácter a carácter, junto con su respectivo sonido para darle mayor realismo. Esto se consigue con un bucle for y con corrutinas, permitiendo al jugador que se muestre todo el texto directamente si pulsa el botón de disparo.

### Audio Manager
También se ha creado un script AudioManager asociado a un GameObject vacío (BackgroundAudio) que permitirá cambiar de música de fondo dinámicamente, dependiendo de la situación. Concretamente, empezará a sonar una música tranquila hasta que se active el reto de saltar los enemigos, y una vez que dicho reto finalice, vuelve la música tranquila.
Es necesario añadirle un component Audio Source con la canción inicial y pasar al script la canción con la que queremos hacer el cambio.

### PowerUp
Tenemos un GameObject con el icono de una estrella y su respectiva animación que se activa al colisionar con el jugador y, al mismo tiempo se inicia la corrutina de combate. 
Esta corrutina lo que hace es introducir una pequeña pausa para que de tiempo a que se cargue la animación del powerUp, luego desactiva el render del icono para que desaparezca y cambia el valor de una variable, dialogSignal, que es consultada por DialogSystem para mostrar un texto en pantalla.
Por otra parte, activa una variable, combat, que es consultada por AudioManager para que se inicie la música de combate. De igual forma, habilita la aparición de enemigos y el modo de scroll 1 para que la cámara esté fija y sea el fondo el que se mueva.
También hace aparecer un contador de enemigos superados y otorga la habilidad de saltar al jugador.
Finalmente, cuando el jugador ha superado a 5 enemigos, desde el GameManager se llama a la función stopBattle(), que lo que hace básicamente es desactivar la mayoría de parámetros que fueron activados en la corrutina startBattle().


## Nivel 2
En este nivel no aparecerán enemigos, ya que el principal enemigo será el propio escenario.
Para ello, he creado tres tilemaps: uno contiene todos los tiles relativos a plataformas, suelo y paredes; otro contiene los pinchos y otro contiene elementos decorativos.
Los suelos, plataformas y paredes tendrán un rigidbody kinematic con composite collider para que el jugador no los atraviese; el tilemap con pinchos también tendrá un rigidbody kinematic con composite collider pero tendrá una etiqueta “spikes” para que si el jugador colisiona con ellos se termine la partida. Y finalmente, el tilemap para elementos decorativos no tendrá rigidbody ni collider porque no es necesario que el jugador interactúe con dichos elementos.  

Por otra parte, se mantiene el background scrolling con parallax para dotar de mayor realismo a la escena y un sistema de cámara que sigue al jugador. 
Se han añadido objetos en la escena como palancas (crank) y cajas. El nivel está diseñado de forma que para poder completarlo tengas que activar la palanca inferior para que caiga la caja de la plataforma superior sobre la que se podrá saltar y luego la palanca superior para que se active el péndulo que dejará el camino libre hacia la puerta del final del nivel.
Además, se han añadido dos cámaras virtuales adicionales que siguen a la caja y al péndulo durante unos segundos cuando estos son activados, de forma que se ve claramente el efecto que produce la activación de las palancas. Para controlar el cambio de cámaras virtuales se ha creado un script asociado a la cámara principal (CameraChange).

También se han añadido tres plataformas con Rigidbody dinámico pero sin gravedad, que caerán en cuanto el jugador las toque (debido a la fuerza que aplica sobre ellas), por lo que tendrá que saltar con rapidez.
En cuanto a las palancas, se ha creado un script (ObjectSwitch) al que se le pasará el objeto destino que va a ser activado al pulsar la palanca. Además, se ha creado una etiqueta “Switch” para que este script pueda utilizarse tanto si se incorpora a palancas como a cualquier otro tipo de activador como botones o interruptores. La activación se centra en configurar los objetos destino como objetos estáticos y al pulsar la palanca convertirlos en dinámicos para que cumplan con el comportamiento físico deseado, como caer desde una determinada altura (caja) o empezar a moverse (péndulo).

### Péndulo (Hinge Joint)
Para el péndulo (liana), hemos creado un GameObject vacío al que le hemos añadido como hijos la rama sin colliders y la caja con pinchos con colliders. Al GameObject padre se le ha añadido un Rigidbody que inicialmente será estático para que al pulsar la palanca se convierta en dinámico y pueda tener el correspondiente comportamiento físico. Para que actúe como un péndulo, se le ha añadido un componente Hinge Joint 2D. 
El pivote del joint hay que colocarlo en la parte superior del objeto para que cuelgue de dicho punto. Para poder mover este pivote modificaremos los valores de anchor.

Para controlar el movimiento de la liana, creamos un script (Pendulum) que asignaremos a este objeto. En él, aplicamos una velocidad inicial de rotación (angularVelocity) para que empiece el movimiento de balanceo de izquierda a derecha.
Además, mientras la rotación de z sea menor que el límite que hemos establecido para la derecha y la velocidad angular de rotación sea mayor que 0 (es decir, que se esté moviendo a la derecha) sin que supere a la velocidad indicada a través del inspector, le aplicaremos la velocidad indicada. 
Por otro lado, si la velocidad angular de rotación es menor que 0 (se está moviendo hacia la izquierda), la rotación tiene un valor mayor al límite establecido hacia la izquierda y sigue siendo mayor a la velocidad indicada a través del inspector (en negativo), le aplicaremos la velocidad indicada en negativo para que realice el movimiento hacia la izquierda.
Se ha añadido también una variable en el Update para que la velocidad angular inicial solamente se aplique una vez.

## Nivel 3
En este nivel se ha implementado un pool dinámico de disparos que se habilitará después de que el jugador coja el powerUp. El script asociado a los powerUps ha sido modificado para que cuando diferenciar entre los que activan algún evento (como el evento de batalla del nivel 1) y los que solamente activan una habilidad a través de la corutina waitingTimeBeforeDisapear.

Respecto al enemigo, se ha programado un comportamiento diferente para los enemigos de tipo Octopus. Su movimiento es de arriba abajo y cuando sea alcanzado por tres disparos desaparecerá, y en este caso se activará la animación de la puerta para que se pueda avanzar a otro nivel. También se ha incluido una transición de color rojo cada vez que el enemigo sea alcanzado para dar mayor feedback visual.

Al script asociado al jugador se han añadido algunas variables para poder permitir la acción de disparar, además de asociarle una animación específica para ello. Como para crear los disparos se ha creado un pool de objetos, cuando el jugador tenga la habilidad de disparar y pulse el botón de disparo, se llamará al método RequestShot del script asociado al pool para que se activen los disparos que sean necesarios. 
Dependiendo de hacia donde esté mirando el jugador, se aplicará un offset al disparo hacia la derecha o hacia la izquierda.

Respecto a la bala, se ha creado un prefab y un script asociado a él (shot) en el que se le aplica una velocidad hacia la izquierda o hacia la derecha (dependiendo de hacia donde se ejecuta el disparo) cuando la bala es activada por el jugador. Si la bala colisiona con cualquier objeto, queda desactivada.

En cuanto al script asociado al pool de balas (ShotPool), se le asocia el prefab de la bala, un tamaño inicial del pool y se crea una lista que contendrá dichas balas. En este caso se usa una lista en vez de un array para poder aumentar su tamaño en caso de solicitar más balas que el tamaño del pool indicado inicialmente.
Al iniciar el juego se crea el total de balas inicial desactivadas, y cada vez que el jugador dispare, lo que hará será llamar al método RequestShot que se encarga de ir activando una a una las balas solicitadas. Si no encuentra balas inactivas para activarlas, lo que ocurre es que se añade una bala más al final de la lista y se activa dicha bala.

<iframe width="560" height="315" src="https://www.youtube.com/embed/6oEmNrLDTZI" title="YouTube video player" frameborder="0" allow="accelerometer; autoplay; clipboard-write; encrypted-media; gyroscope; picture-in-picture" allowfullscreen></iframe>

