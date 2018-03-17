/*
 * Creado por SharpDevelop.
 * Usuario: Pikachu240
 * Fecha: 08/11/2017
 * Hora: 11:26
 * Licencia GNU GPL V3
 * Para cambiar esta plantilla use Herramientas | Opciones | Codificación | Editar Encabezados Estándar
 */
using System;
using Gabriel.Cat;
using Gabriel.Cat.S.Utilitats;

namespace PokemonGBAFrameWork
{
	/// <summary>
	/// Description of Habitat.
	/// </summary>
	public class Habitat
	{
		public class Pagina
		{
			Llista<Word> pokemon;
			
			public Pagina()
			{
				pokemon=new Llista<Word>();
			}

			public Llista<Word> Pokemon {
				get {
					return pokemon;
				}
			}
		}
		
		Llista<Pagina> paginas;
		//buscar donde está la información y como se añaden más habitats :)
		BloqueString nombre;
		BloqueImagen icono;
		public Habitat()
		{
			paginas=new Llista<Pagina>();
		}

		public Llista<Pagina> Paginas {
			get {
				return paginas;
			}
		}
	}
}
