En este proyecto voy a incluir GameObjects del menú, GameObjects de la Asset Store y algún controlador del paquete de los Standard Assets.
Del menú he elegido el GameObject terrain, que será la superficie sobre la que irán el resto de GameObjects y un texto, que colocaré delante de una estructura para indicar al jugador qué es lo que tiene que encontrar.
De la Asset Store he descargado: 
•	Un pack de assets como el Lowpoly environment – Nature Pack: https://assetstore.unity.com/packages/3d/environments/lowpoly-environment-nature-pack-free-187052 
	o	De este pack de assets he elegido algunos árboles.
•	Un pack de objetos de comida: https://assetstore.unity.com/packages/3d/food-pack-3d-microgames-add-ons-163295 
	o	De este pack he cogido la hamburguesa, que es el objeto que tendrá que encontrar el jugador
Para incluir algún controlador del paquete de los Standard Assets, lo primero que hay que tener en cuenta es que los Standard Assets no aparecen en la última versión de Unity, sin embargo, pueden descargarse otros de la Asset Store llamados StarterAssets desde: https://blog.unity.com/games/say-hello-to-the-new-starter-asset-packages?_ga=2.209946325.1745126510.1634419645-1104142834.1634076577 
Concretamente he descargado los de First Person Character Controller, del cual he elegido para mi escena:
•	PlayerCapsule: es el controlador
•	PlayerFollowCamera: para que la cámara se mueva en la dirección en la que apuntamos con el ratón
•	MainCamera: para que la cámara esté incorporada al controlador en vez de en una posición fija
•	Una estructura de los prefabs
Al añadir a mi escenario al controlador (Player), si tenemos problemas con la cámara por haber modificado por error algún parámetro, podemos recuperar los valores por defecto para que pueda moverse por el escenario, le damos a tools, starter assets -> reset first person controller
