/*
 * Creado por SharpDevelop.
 * Usuario: Pikachu240
 * Fecha: 06/09/2017
 * Hora: 1:46
 * Licencia GNU GPL V3
 * Para cambiar esta plantilla use Herramientas | Opciones | Codificación | Editar Encabezados Estándar
 * Créditos a Darthatron por la investigación y a karatekid552 por la rutina acortada, ambos de Pokecommunity
 * Créditos a Ωmega por el tutorial :D https://wahackforo.com/t-47450/fr-animar-portada-al-estilo-liquid-crystal
 */
using System;
using System.Collections.Generic;
using Gabriel.Cat.Extension;
/*
 Falta probar
 */
namespace PokemonGBAFrameWork
{
	/// <summary>
	/// Description of AnimaciónPortada.
	/// </summary>
	public class AnimaciónPortada
	{
		public struct FrameAnimacion
		{
			OffsetRom offsetImgData;
			byte tiempoPausa;
			byte posicionSiguienteFrame;
			public static readonly byte[] Buffer={0xFF,0xFF};
			public const int LENGHT=OffsetRom.LENGTH+1+1+2;//imgData,tiempoPausa,siguienteFrame,Buffer
			public OffsetRom OffsetImgData
			{
				get{return offsetImgData;}
				set{offsetImgData=value;}
			}
			
			public byte TiempoPausa {
				get {
					return tiempoPausa;
				}
				set {
					tiempoPausa = value;
				}
			}

			public byte PosicionSiguienteFrame {
				get {
					return posicionSiguienteFrame;
				}
				set {
					posicionSiguienteFrame = value;
				}
			}
			public FrameAnimacion(OffsetRom offsetImgData,byte tiempoPausa,byte posicionSiguienteFrame)
			{
				this.offsetImgData=offsetImgData;
				this.tiempoPausa=tiempoPausa;
				this.posicionSiguienteFrame=posicionSiguienteFrame;
			}
			private unsafe FrameAnimacion(byte* ptrDataPosicionada)
			{
				this.offsetImgData=new OffsetRom(ptrDataPosicionada);
				ptrDataPosicionada+=OffsetRom.LENGTH;
				this.tiempoPausa=*ptrDataPosicionada;
				ptrDataPosicionada++;
				this.posicionSiguienteFrame=*ptrDataPosicionada;
				
			}
			public static FrameAnimacion[] GetFrames(RomGba rom,int offsetTabla)
			{
				if(rom==null||offsetTabla<0)
					throw new ArgumentException();
				
				List<FrameAnimacion> frames=new List<FrameAnimacion>();
				bool continuar=true;
				FrameAnimacion frameAct;
				unsafe{
					byte* ptrDataPosicionada;
					fixed(byte* ptData=rom.Data.Bytes)
					{
						ptrDataPosicionada=ptData+offsetTabla;
						while(continuar)
						{
							frameAct=new FrameAnimacion(ptrDataPosicionada);
							continuar=frameAct.OffsetImgData.IsAPointer;
							if(continuar){
								frames.Add(frameAct);
								ptrDataPosicionada+=LENGHT;
							}
							
						}
					}
				}
				return frames.ToArray();
			}
			public static int SetFrames(RomGba rom,int offsetTablaOld,IList<FrameAnimacion> frames)
			{
				if(rom==null||offsetTablaOld<0|frames==null)
					throw new ArgumentException();
				int offsetTablaNew;
				//borro los datos antiguos
				rom.Data.Remove(offsetTablaOld,GetFrames(rom,offsetTablaOld).Length*LENGHT);
				//pongo los nuevos
				offsetTablaNew=rom.Data.SearchEmptyBytes(frames.Count*LENGHT);
				unsafe
				{
					byte* ptrDatosPosicionados;
					fixed(byte* ptDatos=rom.Data.Bytes)
					{
						ptrDatosPosicionados=ptDatos+offsetTablaNew;
						for(int i=0;i<frames.Count;i++)
						{
							OffsetRom.SetOffset(ptrDatosPosicionados,frames[i].OffsetImgData);
							ptrDatosPosicionados+=OffsetRom.LENGTH;
							
							*ptrDatosPosicionados=frames[i].TiempoPausa;
							ptrDatosPosicionados++;
							
							*ptrDatosPosicionados=frames[i].PosicionSiguienteFrame;
							ptrDatosPosicionados++;
							
							*ptrDatosPosicionados=Buffer[0];
							ptrDatosPosicionados++;
							*ptrDatosPosicionados=Buffer[1];
							ptrDatosPosicionados++;
						}
					}
				}
				return offsetTablaNew;
			}
		}
		public static readonly ASM Rutina;
		public static readonly Variable OffsetBytesAPoner;
		public static readonly Variable OffsetPointerRutina;
		public static readonly byte[] RutinaOn={0x07, 0x49, 0x08, 0x47, 0x08, 0xBC};
		public static readonly byte[] RutinaOff={0x0, 0x23, 0xC1, 0x5E, 0x06, 0x48};
		public static readonly byte[] OffsetRutinaOff={0x8B, 0x0A, 0x00, 0x00};
		public static readonly Variable[] OffsetsRutina;
		const int POSICIONOFFSETTABLA=52;
		
		List<FrameAnimacion> frames;
		
		static AnimaciónPortada()
		{
			const int OFFSETSRUTINA=4;
			Rutina=ASM.Compilar(System.Text.ASCIIEncoding.ASCII.GetString(Resources.ASMAnimarPortada));
			OffsetBytesAPoner=new Variable("Offset donde se tienen que poner los bytes");
			OffsetPointerRutina=new Variable("Offset donde va el offset+1 de la rutina");
			OffsetsRutina=new Variable[OFFSETSRUTINA];
			for(int i=0;i<OFFSETSRUTINA;i++)
				OffsetsRutina[i]=new Variable("Offset rutina "+i);
			
			//Bytes a poner
			OffsetBytesAPoner.Add(EdicionPokemon.RojoFuegoUsa,0x78BFC,0x78C10);
			OffsetBytesAPoner.Add(EdicionPokemon.VerdeHojaUsa,0x78BFC,0x78C10);
			OffsetPointerRutina.Add(EdicionPokemon.VerdeHojaEsp,0x78C34);
			OffsetPointerRutina.Add(EdicionPokemon.RojoFuegoEsp,0x78C34);
			//Donde pongo el offset de la rutina
			OffsetPointerRutina.Add(EdicionPokemon.RojoFuegoUsa,0x78C1C,0x78C30);
			OffsetPointerRutina.Add(EdicionPokemon.VerdeHojaUsa,0x78C1C,0x78C30);
			OffsetPointerRutina.Add(EdicionPokemon.VerdeHojaEsp,0x78C54);
			OffsetPointerRutina.Add(EdicionPokemon.RojoFuegoEsp,0x78C54);
			//pongo los offsets de la rutina
			//primero es un texto que no se para que se usa..."El guardia no te dejará pasar!"
			OffsetsRutina[0].Add(EdicionPokemon.RojoFuegoUsa,0x188900,0x188978);
			OffsetsRutina[0].Add(EdicionPokemon.VerdeHojaUsa,0x1888DC,0x188954);
			OffsetsRutina[0].Add(EdicionPokemon.RojoFuegoEsp,0x188D2E);
			OffsetsRutina[0].Add(EdicionPokemon.VerdeHojaEsp,0x188D0A);
			//segundo
			OffsetsRutina[1].Add(EdicionPokemon.RojoFuegoUsa,0x705A79);
			OffsetsRutina[1].Add(EdicionPokemon.VerdeHojaUsa,0x705355,0x7053C5);
			OffsetsRutina[1].Add(EdicionPokemon.RojoFuegoEsp,0x6FC621);
			OffsetsRutina[1].Add(EdicionPokemon.VerdeHojaEsp,0x6FBD19);
			//tercero
			OffsetsRutina[2].Add(EdicionPokemon.RojoFuegoUsa,0x480468,0x4804C8);
			OffsetsRutina[2].Add(EdicionPokemon.VerdeHojaUsa,0x47FD44,0x47FDB4);
			OffsetsRutina[2].Add(EdicionPokemon.RojoFuegoEsp,0x478CB0);
			OffsetsRutina[2].Add(EdicionPokemon.VerdeHojaEsp,0x4783A8);
			//cuarto
			OffsetsRutina[2].Add(EdicionPokemon.RojoFuegoUsa,0x78C01,0x78C15);
			OffsetsRutina[2].Add(EdicionPokemon.VerdeHojaUsa,0x78C01,0x78C15);
			OffsetsRutina[2].Add(EdicionPokemon.RojoFuegoEsp,0x78C39);
			OffsetsRutina[2].Add(EdicionPokemon.VerdeHojaEsp,0x78C39);
		}
		
		public AnimaciónPortada()
		{
			frames=new List<FrameAnimacion>();
		}

		public List<FrameAnimacion> Frames {
			get {
				return frames;
			}
		}
		public FrameAnimacion this[int indexFrame]{
			
			get{return frames[indexFrame];}
			set{
				frames[indexFrame]=value;}
		}

		public int[] OffsetsImgData()
		{
			int aux;
			SortedList<int,int> dic=new SortedList<int, int>();
			for(int i=0;i<frames.Count;i++){
				aux=frames[i].OffsetImgData.Offset;
				if(!dic.ContainsKey(aux))
					dic.Add(aux,aux);}
			return dic.GetValues().ToArray();
		}
		public static bool EstaActivado(RomGba rom)
		{
			return rom.Data.SearchArray(RutinaOn)>0;
		}
		public static int GetOffsetTabla(RomGba rom,EdicionPokemon edicion,Compilacion compilacion)
		{
			int pos;
			if(EstaActivado(rom))
			{
				//obtengo la posicion de la rutina le resto 1 y le sumo 52
				pos=new OffsetRom(rom,new OffsetRom(rom,Variable.GetVariable(OffsetPointerRutina,edicion,compilacion)).Offset-1+POSICIONOFFSETTABLA).Offset;
			}else pos=-1;
			return pos;
		}
		public static void SetOffsetTabla(RomGba rom,EdicionPokemon edicion,Compilacion compilacion,int offsetNew)
		{
			if(EstaActivado(rom))
			{
				//obtengo la posicion de la rutina le resto 1 y le sumo 52
				rom.Data.SetArray(new OffsetRom(rom,Variable.GetVariable(OffsetPointerRutina,edicion,compilacion)).Offset-1+POSICIONOFFSETTABLA,new OffsetRom(offsetNew).BytesPointer);
			}
		}
		public static void SetAnimacionPortada(RomGba rom,EdicionPokemon edicion,Compilacion compilacion,AnimaciónPortada animacion)
		{
			if(!EstaActivado(rom))
				Activar(rom,edicion,compilacion);
			SetOffsetTabla(rom,edicion,compilacion,	FrameAnimacion.SetFrames(rom,GetOffsetTabla(rom,edicion,compilacion),animacion.Frames));
		}
		public static AnimaciónPortada GetAnimacionPortada(RomGba rom,EdicionPokemon edicion,Compilacion compilacion)
		{
			AnimaciónPortada animacion;
			
			if(EstaActivado(rom))
			{
				animacion=new AnimaciónPortada();
				animacion.Frames.AddRange(FrameAnimacion.GetFrames(rom,GetOffsetTabla(rom,edicion,compilacion)));
			}else animacion=null;
			
			return animacion;
		}
		public static byte[] GetRutinaCompilada(EdicionPokemon edicion,Compilacion compilacion)
		{
			byte[] rutinaCompilada=(byte[])Rutina.AsmBinary.Clone();
			//sustituyo los offsets :)
			rutinaCompilada.SetArray(0x9,new OffsetRom(Variable.GetVariable(OffsetsRutina[0],edicion,compilacion)).BytesPointer);
			rutinaCompilada.SetArray(0x1F,new OffsetRom(Variable.GetVariable(OffsetsRutina[1],edicion,compilacion)).BytesPointer);
			rutinaCompilada.SetArray(0x2B,new OffsetRom(Variable.GetVariable(OffsetsRutina[2],edicion,compilacion)).BytesPointer);
			rutinaCompilada.SetArray(0x44,new OffsetRom(Variable.GetVariable(OffsetsRutina[3],edicion,compilacion)).BytesPointer);
			return rutinaCompilada;
		}
		public static void Activar(RomGba rom,EdicionPokemon edicion,Compilacion compilacion,int offsetTablaFrames=-1)
		{
			const int DEFAULTLENGTHTABLAFRAMES=10;
			byte[] rutinaCompilada;
			int offsetTablaAux;
			if(!EstaActivado(rom))
			{
				//inserto la rutina
				//en la posicion 52 va el offset de la tabla
				rutinaCompilada=GetRutinaCompilada(edicion,compilacion);
				if(offsetTablaFrames>0)
				{
					offsetTablaAux=offsetTablaFrames;
				}else{
					
					offsetTablaAux=rom.Data.SearchEmptyBytes(FrameAnimacion.LENGHT*	DEFAULTLENGTHTABLAFRAMES);
				}
				
				rutinaCompilada.SetArray(POSICIONOFFSETTABLA,new OffsetRom(offsetTablaAux).BytesPointer);
				
				rom.Data.SetArray(Variable.GetVariable(OffsetPointerRutina,edicion,compilacion),new OffsetRom(rom.Data.SetArray(rutinaCompilada)+1).BytesPointer);//le sumo uno porque es el offset de una rutina
				rom.Data.SetArray(Variable.GetVariable(OffsetBytesAPoner,edicion,compilacion),RutinaOn);
				
			}
		}
		public static void Desctivar(RomGba rom,EdicionPokemon edicion,Compilacion compilacion,bool borrarTabla=false)
		{//mas adelante borrar las imagenes también
			if(EstaActivado(rom))
			{
				if(borrarTabla)
					FrameAnimacion.SetFrames(rom,GetOffsetTabla(rom,edicion,compilacion),new FrameAnimacion[0]);
				//Quito la rutina
				rom.Data.Remove(new OffsetRom(rom,Variable.GetVariable(OffsetPointerRutina,edicion,compilacion)).Offset-1,Rutina.AsmBinary.Length);
				//restauro los bytes anteriores
				rom.Data.SetArray(Variable.GetVariable(OffsetPointerRutina,edicion,compilacion),OffsetRutinaOff);
				rom.Data.SetArray(Variable.GetVariable(OffsetBytesAPoner,edicion,compilacion),RutinaOff);
				
			}
		}
		
	}
	
}
