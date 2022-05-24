# **Observador de Carpetas**

## Descripción

El **Observador de Carpetas** se encarga de supervisar la carpeta seleccionada como origen y verifica todos los archivos que esten actualmente en la carpeta y en subcarpetas como también los archivos futuros que se vayan agregar a la carpeta.

Todos los archivos se moveran de la carpeta seleccionada como origen a la carpeta destino diviendo los archivos en dos carpetas:

- Carpeta **Processed**: Solo almacena archivos con extensión .xls\*
- Carpeta **NotApplicable**: Almacena los demas archivos que no sean extensión .xls

En la carpeta destino se crean un archivo **ArchivoMaestro.xlsx** el cual se encarga de obtener todas las copias de las hojas de los otros archivos .xls\* de la carpeta Origen.

---

## Librerias Externas

- **[Syncfusion](https://help.syncfusion.com/file-formats/xlsio/overview)**: esta libreria se encarga de crear el archivo de excel como poder copiar las hojas de los archivos internos.
