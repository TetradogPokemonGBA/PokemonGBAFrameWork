/*
 * Created by SharpDevelop.
 * User: Pikachu240
 * Date: 10/03/2017
 * Time: 11:06
 * 
 * Código bajo licencia GNU
 *
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;


namespace PokemonGBAFrameWork
{
	/// <summary>
	/// Description of Compilacion.
	/// </summary>
	public class Compilacion:IComparable
	{
		public static readonly Compilacion[] Compilaciones=new Compilacion[]{new Compilacion(0),new Compilacion(1),new Compilacion(2)};
	    int compilacion;
		private Compilacion(int compilacion)
		{
			this.compilacion=compilacion;
		}
		public  IComparable Clau{
			get{return compilacion;}
		}
		public  int CompareTo(object obj)
		{
			Compilacion compilacion=obj as Compilacion;
			int compareTo=compilacion!=null?this.compilacion.CompareTo(compilacion.compilacion):(int)Gabriel.Cat.CompareTo.Inferior;
			return compareTo;
		}
		public override string ToString()
		{
			return string.Format("1.{0}", compilacion);
		}
       
		public static Compilacion GetCompilacion(RomData romData)
		{
			return GetCompilacion(romData.Rom,romData.Edicion);
		}
		public static Compilacion GetCompilacion(RomGba rom,EdicionPokemon edicion)
		{
			//ahora tengo la edicion correctamente
			Compilacion compilacionRom=null;
			switch (edicion.AbreviacionRom) {
				case AbreviacionCanon.AXV:
				case AbreviacionCanon.AXP:
					if(edicion.Idioma==Idioma.Español||Zona.GetOffsetRom(rom, DescripcionPokedex.ZonaDescripcion,edicion, Compilacion.Compilaciones[0]).IsAPointer)
						compilacionRom=Compilaciones[0];
					else{
						if(Zona.GetOffsetRom(rom,Ataque.ZonaAnimacion, edicion, Compilacion.Compilaciones[1]).IsAPointer)
				    	   compilacionRom=Compilaciones[1];
						//me falta saber como diferenciar Ruby&Zafiro 1.1 y Ruby&Zafiro 1.2 USA
						
					}
					break;
				case AbreviacionCanon.BPE:
					compilacionRom=Compilaciones[0];
					break;
				case AbreviacionCanon.BPR:
				case AbreviacionCanon.BPG:
					if(edicion.Idioma==Idioma.Español||Zona.GetOffsetRom(rom, DescripcionPokedex.ZonaDescripcion, edicion, Compilacion.Compilaciones[0]).IsAPointer)
						compilacionRom=Compilaciones[0];
					else{
					compilacionRom=Compilaciones[1];
					}
					break;
			}
			return compilacionRom;
		}
	}
}
