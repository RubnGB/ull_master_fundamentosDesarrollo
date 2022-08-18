# Entrega 1.3
## Perforce con Unity

## Inicio
Para realizar esta práctica he usado los assets del proyecto subido a Github en la tarea anterior.

## Helix Visual Client (P4V)
En primer lugar tenemos que descargarnos un cliente de Perforce para poder configurar nuestro workspace y conectarnos posteriormente desde Unity. https://www.perforce.com/downloads 
Hay una gran variedad de opciones, pero siempre instalaremos como mínimo Helix Visual Client, que además incluye el intérprete de línea de comandos y el cliente administrativo. Si luego queremos integraciones con otros editores como Visual Studio tendríamos que instalar su correspondiente Plugin.

Realizamos la instalación incluyendo todos los componentes que vienen por defecto y al pedirnos el servidor y nombre de usuario pondremos el servidor de la ULL y nuestro nombre de usuario de la ULL sin @.
Una vez instalado, abrimos la aplicación P4V y elegimos la pestaña de remote server, ya que no es nada común crearnos y usar un servidor personal de Perforce, ya que ni en Unity ni en Unreal Engine hay soporte nativo para usar servidores personales de Perforce.

Ahora tendremos que crear un Workspace, que es una colección de archivos que se crea en el servidor. Para acceder al Workspace siempre tenemos que hacerlo desde un mismo ordenador, sin embargo, podemos tener en nuestro ordenador acceso a distintos workspaces que hayamos creado. Además, no pueden existir en el servidor workspaces con el mismo nombre.
Lo habitual es tener un workspace para cada proyecto, y que en su nomenclatura, para no colisionar con otros usuarios, se ponga el nombre de usuario y la máquina desde la que está accediendo. Se crea un workspace por cada proyecto para no tener que descargar archivos innecesarios cada vez que lo solicitemos.

Si después de introducir nuestro usuario y contraseña de la ULL no nos deja conectarnos es porque no estamos conectados a la red de la ULL. Para ello vamos a tener que usar una VPN siguiendo los pasos indicados por la ULL.
Una vez estemos conectados a través de la VPN lo volvemos a intentar, esta vez con éxito y nos pregunta si confiamos en la huella indicada, a lo que le damos a aceptar.
Para esta práctica crearé un workspace con el nombre alu0101525655.asus.fdvPerforce

Por defecto, en cada workspace se añade el contenido de todos los repositorios que hay en el servidor para que se descargue todo ese contenido en el workspace local. Lo normal es que no queramos prácticamente nada de lo que hay. Para ello vamos marcando con la x (tercer icono) todos los repositorios.
Si hubiese alguno cuyo contenido queremos descargar parcialmente, abrimos el repositorio pulsando en la flecha de la izquierda y seleccionamos con el – rojo cuáles son los elementos de dicho repositorio que no queremos descargarnos.
Si al elegir un repositorio y desmarcar todo lo que tiene dentro para quedarnos solamente con la estructura raíz no nos crea ninguna carpeta en nuestra ruta local es porque en ese nivel no existen archivos. En tal caso podemos crear manualmente esa carpeta para subir ahí el contenido que queramos.
Para esta práctica solamente dejaremos marcado el repositorio FDV2122, pero desmarcando manualmente todo lo que tiene dentro para quedarnos solamente con la estructura.

A continuación se abrirá la interfaz principal de perforce, en la que podemos ver nuestro directorio principal que seguramente estará inicialmente vacío. Podemos refrescar o darle a Get Latest y si había algo en el repositorio que habíamos dejado seleccionado, se cargará en nuestro Workspace. En mi caso se ha creado la carpeta FDV2122 con un archivo de texto dentro.

Para probar que Perforce funciona correctamente voy a modificar el archivo de texto y lo voy a actualizar. En primer lugar hay que seleccionarlo y darle a checkout, porque por defecto los archivos que nos descargamos del servidor son marcados como sólo lectura y nos impide modificarlos. Si hacemos checkout significa que estamos indicando que lo queremos modificar, y de esta forma nos permite hacerlo.
Cuando hacemos esto, se nos pregunta en qué Changelist queremos que se registre esta modificación. Esto nos permite tener distintas Changelists para poder subir al servidor aquella que tenga los cambios que nos interese actualizar en el servidor, mientras tenemos otra Changelist que de momento no queramos actualizar. En este caso el cambio en el archivo de texto lo dejé en la Changelist por defecto.

Cuando queramos subir todos los cambios de una Changelist la seleccionamos y le damos a submit, pudiendo añadir un texto descriptivo indicando qué cambios hemos realizado.

Una vez subido al servidor, podemos ver que el tick rojo desaparece y vuelve a tener el punto verde. Además, si nos fijamos en el número asociado al archivo, podemos ver que ha pasado de la revisión 5/5 a la revisión 6 de 6, indicando que la versión que tenemos en nuestro directorio es la última existente.


## Perforce con Unity
- En primer lugar tenemos que preparar el entorno para que funcione con Perforce, para ello, debemos asegurarnos de que nuestro proyecto de Unity sea hijo del directorio de nuestro workspace. Si no lo hacemos, cuando intentemos conectarnos desde Unity nos saldrá el error: “Not Connected. Couldn’t fstat the Project root directory”
- Una vez que lo tengamos en un directorio que esté contenido en nuestro Workspace, volvemos a la aplicación de P4V y resfrescamos nuestro workspace para que aparezca el proyecto de Unity. 
- A continuación, no debemos marcar todas las carpetas para el seguimiento en Perforce, solamente marcaremos la carpeta Assets, Packages y Project Settings, puesto que el resto de elementos son generados automáticamente por Unity cuando se abre el proyecto y le daremos a Add.
- Esto lo añadirá en la Changelist, y a continuación lo que haremos será el submit para que lo suba al servidor. Una vez subidos al servidor podemos ver que los archivos de las carpetas que hemos añadido para su seguimiento tienen un punto verde.

Ahora ya tenemos preparado el entorno para poder modificar nuestro proyecto desde Unity y subir los cambios al servidor sin necesidad de salir del editor.
- Para usar Perforce en Unity tenemos que abrir nuestro proyecto y entrar en Project Settings, concretamente en Version Control y seleccionar como modo Perforce.
- Si después de introducir nuestro usuario y contraseña de la ULL no nos deja conectarnos hay que comprobar que estamos conectado a la red de la ULL a través de la VPN.
- Una vez conectados, el primer archivo que tenemos que modificar es el de la propia configuración de Perforce. Al conectarnos, podemos ver debajo que ha aparecido el botón de checkout. Le damos y cualquier cambio que hagamos en la configuración será añadida a la Changelist.
- Para poder ver fácilmente qué cambios han sido añadidos a la Changelist, es buena idea abrir la pestaña de Version Control que se encuentra en Windows -> Assets Management.
- Cuando tengamos todos los cambios que queremos subir al servidor, cogemos la Changelist y hacemos el correspondiente submit. 

En este gif simplemente vamos a hacer checkout de la escena para que nos deje añadir una fruta para posteriormente subir los cambios al servidor.
![alt text](https://github.com/RubnGB/ull_master_fundamentosDesarrollo/blob/main/Practica1/Tarea3_Perforce/gif_animation_01c.gif)