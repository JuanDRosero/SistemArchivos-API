using SistemArchivos_API.Model.Excepciones;
using SistemArchivos_API.Model.Interfaz;

namespace SistemArchivos_API.Model
{

    public class SuperBloque: ISuperBloque, ILectorSuperBloque
    {
        public int CantidadBloques { get; set; }
        public int CantidadInodos { get; set; }
        public int TamañoBloques { get; set; }
        public Espacio<INODO>[] TablaINodos { get; private set; }
        public Espacio<Bloque>[] TablaBloques { get; private set; }

        public SuperBloque(int cantidadBloques, int cantidadInodos, int tamañoBloques)
        {
            CantidadBloques = cantidadBloques;
            CantidadInodos = cantidadInodos;
            TamañoBloques = tamañoBloques;
            TablaINodos = new Espacio<INODO>[cantidadInodos];
            TablaBloques = new Espacio<Bloque>[cantidadBloques];
            for (int i = 0; i < cantidadBloques; i++)
            {
                TablaBloques[i] = new Espacio<Bloque>();
                TablaBloques[i].Id = i;
                TablaBloques[i].tipo = "Bloque";
            }
            for (int i = 0; i < cantidadInodos; i++)
            {
                TablaINodos[i] = new Espacio<INODO>();
                TablaINodos[i].Id = i;
                TablaINodos[i].tipo = "Inodo";
            }

        }

        public bool CrearArchivo(Archivo archivo, int padre)
        {
            //Busca un espacio libre
            bool encontrado = false;
            int i = 0;
            int BloquesReq = 0;
            int[] bloquesLibres;
            int dirInodo=0;
            INODO inodo=null;
            //Busca un espacio libre en la tabla de inoodos
            do
            {
                if (TablaINodos[i].libre)
                {
                    encontrado = true;
                    dirInodo = i;
                    i = 0;
                    if (TablaINodos[i].elemento==null)  //Si lo encuentra y es null lo i inicializa
                    {
                        inodo = new INODO();
                        TablaINodos[i].elemento =inodo;
                    }
                    else
                    {
                        inodo = TablaINodos[i].elemento;    //Si ya existe solo lo pasa por ref
                    }
                }
                i++;
                    
            } while (!encontrado && i < CantidadInodos);
            if (!encontrado)
            {
                throw new NoSpaceException("No hay Inodos libres");
            }
            else
            {
                encontrado = false;
                BloquesReq = (int)Math.Ceiling(Convert.ToDecimal(archivo.Tamaño) /
                    Convert.ToDecimal(TamañoBloques));
                bloquesLibres = new int[BloquesReq];
                int j = 0;
                do
                {
                    if (TablaBloques[i].libre)
                    {
                        BloquesReq--;
                        bloquesLibres[j] = i;
                        j++;
                    }
                    i++;

                } while (BloquesReq>0 && i<CantidadBloques);
                if (BloquesReq!=0)
                {
                    throw new NoSpaceException("No hay Bloques libres para guardar el archivo");
                }
                else
                {
                    inodo.ID = dirInodo;
                    inodo.Padre = padre;
                    inodo.HoraCreacion = (DateTime.Now.ToString("hh:mm:ss tt"));
                    inodo.Nombre = "Archivo: " + archivo.Nombre + "." + archivo.Extencion;
                    int tamRestante = archivo.Tamaño;
                    foreach (var item in bloquesLibres)
                    {
                        if (TablaBloques[item].elemento==null)
                        {
                            TablaBloques[item].elemento = new Bloque();
                        }
                        TablaBloques[item].elemento.NombreArchivo = archivo.Nombre+"."+archivo.Extencion;
                        TablaBloques[item].elemento.TamañoOcupado = tamRestante > TamañoBloques ? TamañoBloques : tamRestante;
                        TablaBloques[item].libre = false;
                        tamRestante -= TamañoBloques;
                        inodo.Punteros.Add(new Puntero()
                        {
                            Dir=item,
                            Tipo="Bloque"
                        });
                        asignarPadre(padre, dirInodo);

                    }
                    return true;

                }
            }

            
        }

        public bool CrearCarpeta(int padre, string nombre)
        {
            //Busca un espacio libre
            bool encontrado = false;
            int i = 0;
            int dirInodo = 0;
            INODO inodo = null;
            //Busca un espacio libre en la tabla de inoodos
            do
            {
                if (TablaINodos[i].libre)
                {
                    encontrado = true;
                    dirInodo = i;
                    i = 0;
                    if (TablaINodos[i].elemento == null)  //Si lo encuentra y es null lo i inicializa
                    {
                        inodo = new INODO();
                        TablaINodos[i].elemento = inodo;
                    }
                    else
                    {
                        inodo = TablaINodos[i].elemento;    //Si ya existe solo lo pasa por ref
                    }
                }
                i++;

            } while (!encontrado && i < CantidadInodos);
            if (!encontrado)
            {
                throw new NoSpaceException("No hay Inodos libres");
            }
            else
            {
                inodo.ID = dirInodo;
                inodo.Padre = padre;
                inodo.HoraCreacion = (DateTime.Now.ToString("hh:mm:ss tt"));
                inodo.Nombre = "Carpeta " + nombre;
                asignarPadre(padre, dirInodo);
                return true;
            }
        }

        private void asignarPadre(int padre, int actual)
        {
            //La raiz se marca como ID=-1
            if (padre!=-1)
            {
                TablaINodos[padre].elemento.Punteros.Add(new Puntero()
                {
                    Tipo = "Inodo",
                    Dir=actual
                }) ;
            }
        }

        public Espacio<Bloque>[] GetBloques()
        {
            return TablaBloques;
        }

        public Espacio<INODO>[] GetNodos()
        {
            return TablaINodos;
        }
    }
}
