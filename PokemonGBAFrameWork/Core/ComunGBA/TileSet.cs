/*
 * Creado por SharpDevelop.
 * Usuario: Pikachu240
 * Fecha: 20/09/2017
 * Hora: 16:22
 * Licencia GNU GPL V3
 * Para cambiar esta plantilla use Herramientas | Opciones | Codificación | Editar Encabezados Estándar
 */
using System;
using System.Drawing;
using Gabriel.Cat;
using Gabriel.Cat.Extension;

namespace PokemonGBAFrameWork
{
	/// <summary>
	/// Description of TileSet.
	/// </summary>
	public class TileSet
	{
		GranPaleta paleta;
		Llista<Tile> tiles;
		public TileSet()
		{
			tiles=new Llista<Tile>();
			paleta=new GranPaleta();
		}

		public Llista<Tile> Tiles {
			get {
				return tiles;
			}
		}

		public GranPaleta Paleta {
			get {
				return paleta;
			}
		}
	}
	
}
