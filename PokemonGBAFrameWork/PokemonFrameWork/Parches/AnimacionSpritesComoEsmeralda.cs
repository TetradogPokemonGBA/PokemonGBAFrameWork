/*
 * Creado por SharpDevelop.
 * Usuario: Pikachu240
 * Fecha: 19/09/2017
 * Hora: 4:09
 * Licencia GNU GPL V3
 * Para cambiar esta plantilla use Herramientas | Opciones | Codificación | Editar Encabezados Estándar
 */
using System;
using System.Collections.Generic;
//se tiene que probar...falta acabar el SetPokedex...para hacerlo bien...
namespace PokemonGBAFrameWork
{
	/// <summary>
	/// Description of Animacion Sprites Como Esmeralda.
	/// </summary>
	public class AnimacionSprites
	{
		public static readonly Creditos Creditos;
		public static readonly Zona ZonaAnimacionSprites;
		public const byte SPRITELARGEST=0x0F;
		const byte EMPTY=0x0;
		public static readonly byte[] MarcaFin={0xFF,0xFF,0x0,0x0};
		
		List<byte> frames;
		static AnimacionSprites()
		{
			ZonaAnimacionSprites=new Zona("Zona animacion sprites");
			
			ZonaAnimacionSprites.Add(EdicionPokemon.RojoFuegoUsa,0x2349BC,0x234A2C);
			ZonaAnimacionSprites.Add(EdicionPokemon.VerdeHojaUsa,0x234998,0x234A08);
			ZonaAnimacionSprites.Add(EdicionPokemon.RojoFuegoEsp,0x230128);
			ZonaAnimacionSprites.Add(EdicionPokemon.RojoFuegoEsp,0x230104);
			//Zafiro y Rubi
			ZonaAnimacionSprites.Add(EdicionPokemon.RubiEsp,0x1EC984);
			ZonaAnimacionSprites.Add(EdicionPokemon.ZafiroEsp,0x1EC914);
			ZonaAnimacionSprites.Add(EdicionPokemon.RubiUsa,0x1E7C64,0x1E7C7C);
			ZonaAnimacionSprites.Add(EdicionPokemon.ZafiroUsa,0x1E7BF4,0x1E7C0C);
			//Esmeralda
			ZonaAnimacionSprites.Add(EdicionPokemon.EsmeraldaEsp,0x305970);
			ZonaAnimacionSprites.Add(EdicionPokemon.EsmeraldaUsa,0x2FF70C);
			//creditos
			Creditos=new Creditos();
			Creditos.Add(Creditos.Comunidades[Creditos.POKEMONCOMMUNITY],"altariaking","investigación y tutorial -> https://www.pokecommunity.com/showthread.php?t=223078");
		}
		
		public AnimacionSprites()
		{
			frames=new List<byte>();
		}
		/// <summary>
		/// Cada frame es la posición de la imagen a mostrar, el último es el que se quedará
		/// </summary>
		public List<byte> Frames {
			get {
				return frames;
			}
		}
		public byte[] ToBytes()
		{
			const int BYTESFRAME=4;
			byte[] bytesAnimacion=new byte[BYTESFRAME*Frames.Count+MarcaFin.Length];
			unsafe{
				byte* ptrBytes;
				byte* ptrMarcaFin;
				fixed(byte* ptBytes=bytesAnimacion)
				{
					ptrBytes=ptBytes;
					//frame,emtpy,spritelargest,empty
					for(int i=0;i<frames.Count;i++)
					{
						*ptrBytes=frames[i];
						ptrBytes++;
						//empty
						ptrBytes++;
						//largest
						*ptrBytes=SPRITELARGEST;
						ptrBytes++;
						//empty
						ptrBytes++;
					}
					//MarcaFin
				}
				fixed(byte* ptMarcaFin=MarcaFin)
				{
					ptrMarcaFin=ptMarcaFin;
					for(int i=0;i<MarcaFin.Length;i++)
					{
						*ptrBytes=*ptrMarcaFin;
						ptrBytes++;
						ptrMarcaFin++;
					}
				}
			}
			return bytesAnimacion;
			
		}
		
		public static AnimacionSprites GetDefault()
		{
			AnimacionSprites animacionDefault=new AnimacionSprites();
			
			animacionDefault.frames.Add(0);
			animacionDefault.frames.Add(1);
			animacionDefault.frames.Add(0);
			animacionDefault.frames.Add(1);
			animacionDefault.frames.Add(0);
			
			return animacionDefault;
		}
		public static AnimacionSprites GetAnimacion(RomGba rom,EdicionPokemon edicion,Compilacion compilacion)
		{
			const int LENGHTFRAME=4;
			const byte MARCAFIN=0xFF;
			bool acabado=false;
			AnimacionSprites animacion=new AnimacionSprites();
			int offsetAnimacion=Zona.GetOffsetRom(rom,ZonaAnimacionSprites,edicion,compilacion).Offset;
			unsafe{
				byte* ptrRomPosicionado;
				fixed(byte* ptRom=rom.Data.Bytes)
				{
					ptrRomPosicionado=ptRom+offsetAnimacion;
					while(!acabado)
					{
						animacion.Frames.Add(*ptrRomPosicionado);
						ptrRomPosicionado+=LENGHTFRAME;
						acabado=*ptrRomPosicionado==MARCAFIN;

					}
				}
				
			}
			return animacion;
			
		}
		public static void SetAnimacion(RomGba rom,EdicionPokemon edicion,Compilacion compilacion,AnimacionSprites animacion=null)
		{
			if(animacion==null)
				animacion=GetDefault();
			
			const int LENGHTFRAME=4;
			byte[] bytesAnimacion=animacion.ToBytes();
			int offsetAnimacionAnterior;
			int offsetAnimacion=rom.Data.SetArray(bytesAnimacion);
			AnimacionSprites animacionAnt=GetAnimacion(rom,edicion,compilacion);
			
			if(animacion.Frames.Count>2&&animacion.Frames[2]!=2)
			{
				//quito la animacion porque no es la que hay por defecto
				offsetAnimacionAnterior=Zona.GetOffsetRom(rom,ZonaAnimacionSprites,edicion,compilacion).Offset;
				rom.Data.Remove(offsetAnimacionAnterior,animacion.Frames.Count*LENGHTFRAME+MarcaFin.Length);
			}
			
			Zona.SetOffsetRom(ZonaAnimacionSprites,rom,edicion,compilacion,new OffsetRom(offsetAnimacion));
		}
		public static void DuplicarSpritesSiEsNecesario(RomGba rom,EdicionPokemon edicion,Compilacion compilacion)
		{
			Pokemon[] pokedex;

			pokedex=Pokemon.GetPokedex(rom,edicion,compilacion);
			for(int i=0;i<pokedex.Length;i++)
			{
				if(pokedex[i].Sprites.SpritesFrontales.Count==1)
				pokedex[i].Sprites.SpritesFrontales.Add(pokedex[i].Sprites.SpritesFrontales[0]);
				
				if(pokedex[i].Sprites.SpritesTraseros.Count==1)
				pokedex[i].Sprites.SpritesTraseros.Add(pokedex[i].Sprites.SpritesTraseros[0]);
			}
			
			Pokemon.SetPokedex(rom,edicion,compilacion,pokedex);
			
		}
		public static bool EstaActivado(RomGba rom,EdicionPokemon edicion,Compilacion compilacion)
		{
			const int POSSPRITELARGEST=2;//empieza por 0
			int offsetAnimacion=Zona.GetOffsetRom(rom,ZonaAnimacionSprites,edicion,compilacion).Offset;
			return rom.Data.Bytes[offsetAnimacion+POSSPRITELARGEST]==SPRITELARGEST;
			
		}
	}
}
