# Entrega 1.2
## Unity Collab y Plastic SCM

## Inicio
Para realizar esta práctica he usado los assets del proyecto subido a Github en la tarea anterior.
En primer lugar quiero dejar constancia de que Unity Collab es una herramienta obsoleta que ha sido sustituida por Plastic SCM, aún así podremos ver los pasos que había que seguir con ambas.

## Unity Collab
- Para habilitar Unity Collab tenemos que abrir nuestro proyecto de Unity, ir a la pestaña Windows y abrir Collaborate.
- Establecer un identificador para el proyecto.
- Para ello es necesario seleccionar una organización, pero podría ocurrir que no tuvieses ninguna asociada a tu cuenta. Para asociar una organización a tu cuenta, tienes que ir a id.unity.com, hacer login, y en el panel de la izquierda ir a organizaciones y crear una. Si al ir a este apartado aparece una organización (lo normal es que crearas una cuando te hiciste la cuenta de Unity), simplemente cierra tu sesión en el proyecto de Unity y vuelve a iniciarla para que ya te aparezca para seleccionar la organización.
- Tras crear el identificador, a la derecha hay que pulsar en Start Collab o bien en el botón de OFF que está en el menú de Project settings.
- Aparece un mensaje indicando que Unity Collab está obsoleto y que ha sido sustituido por PlasticSMC. Aún así vamos a continuar hasta donde se pueda.
- Una vez que hemos activado Unity Collab podemos ver cómo aparece un símbolo + en un cuadrado azul que indica qué archivos tienen cambios que no han sido subidos al repositorio. 
- Además, en la pestaña Collaborate de la izquierda, podemos seleccionar qué archivos queremos que se publiquen en el repositorio y cuáles no.
- Después de seleccionar los elementos que queremos subir al repositorio le daríamos a publicar. En este caso no funciona porque no puede establecer la conexión con el servidor (como he dicho antes, esto ocurre por estar obsoleto).
- Si hubiera ido todo bien, para añadir personas al proyecto, le daríamos a los 3 puntos que hay al lado de Publish Changes y le daríamos a Invite Teammate.
- Se nos abrirá en el navegador el dashboard de nuestro proyecto (previo inicio de sesión) y desde Project members podremos añadir personas para que colaboren en nuestro proyecto indicando su dirección de correo electrónico.


## Plastic SCM
Lo primero que vamos a hacer es deshabilitar el control de versiones desde Project settings y luego comprobar si en la pestaña Windows aparece Plastic SCM. En el caso de que no aparezca, habrá que actualizar la versión del paquete Version Control, en mi caso sí me aparece en la versión 1.11.2. Si siguen saliendo los errores, hay que reiniciar el proyecto.

A continuación, es imprescindible tener una cuenta de Plastic SCM, hay dos formas de hacerlo. La primera es desde Unity Teams y te obliga a especificar un método de pago aunque selecciones la opción gratuita, y la segunda, es a través de la página de Plastic SCM, y es la que voy a realizar, ya que el método de pago es opcional.
- Tenemos que ir a la web https://www.plasticscm.com/ , darle a try now e iniciar sesión con nuestro ID de Unity.
- Rellenamos nuestros datos incluyendo el nombre de nuestra empresa, si esta empresa existe en VAT pondríamos el CIF de nuestra empresa para que puedan emitirnos facturas. Seleccionamos el Datacenter que esté más cerca de nosotros y le damos a finalizar.
- Ahora podemos irnos a Unity para poder usar Plastic SCM, aunque también tendríamos la posibilidad de usar el software independiente de Plastic SCM, para lo cual nos tendríamos que descargar e instalar el software.
- Para esta práctica no es necesario instalar dicho software, nos vamos al editor de Unity y pulsamos en Plastic SCM. Debe aparecer un tick verde en el login con la cuenta de Plastic.
- Ahora creamos el workspace, nos permite asignarle un nombre al proyecto y en el nombre de repositorio nos añade el nombre de nuestra organización. Al igual que con la aplicación independiente, nos permite seleccionar entre un workspace de plastic que incluye herramientas similares a las que se usan en git o bien una versión simplificada que es gluon en la que podemos subir los archivos y bloquearlos para que no sean modificados por otras personas. Voy a usar gluon porque en la tarea anterior ya usé las herramientas de git.
- Seleccionamos los elementos que queremos subir al repositorio y en mi caso solamente voy a subir la escena que tengo creada. Podemos ver que como todos los elementos son considerados como nuevos por Plastic SCM tienen una barra inclinada dentro de un cuadrado verde.
- Cuando la seleccionemos, en el inspector aparecerá la opción de añadirla al seguimiento para poder subirla al repositorio.
- Cuando la tengamos añadida el icono cambiará a un signo + dentro de un círculo. Una vez tengamos todos los elementos que queremos subir, escribimos un mensaje y le damos a checkin changes. También podemos directamente seleccionar todos los elementos que queremos subir, escribir el mensaje y hacer el checkin.
- En Changesets podemos ver la operación que acabamos de realizar, y si nos vamos al árbol de directorios del proyecto, podemos ver que ha desaparecido el cuadrado verde del elemento que acabamos de subir, indicando que la versión que está subida es la versión más actualizada y si hacemos cualquier cambio volverá a ponerse en verde para que la volvamos a subir.
- Si queremos comprobar si se ha subido el contenido a nuestro repositorio, podemos hacer como con Github, e ir a nuestro Dashboard de Plastic SCM: https://www.plasticscm.com/dashboard 
- Desde ahí entramos en Cloud y le damos a Open WebUI
- Si un usuario tuviese una cuenta creada y le hubiera dado permisos, seguramente podría acceder a través de este enlace: https://www.plasticscm.com/orgs/rbn91/repos/FundamentosP01/branch/main/tree/Assets/Scenes
- Para añadir usuarios con permisos sobre nuestro repositorio, en vez de entrar en la WebUI, en Configure tenemos la opción de usuarios y grupos.

En este gif voy a mostrar brevemente cómo sería el proceso de subir otro archivo y consultar si lo ha hecho correctamente en el dashboard
![alt text](https://github.com/RubnGB/ull_master_fundamentosDesarrollo/blob/main/Practica1/Tarea2_PlasticSCM/gif_animation_01b.gif)