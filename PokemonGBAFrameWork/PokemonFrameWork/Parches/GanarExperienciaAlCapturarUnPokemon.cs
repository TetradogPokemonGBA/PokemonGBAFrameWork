/*
 * Creado por SharpDevelop.
 * Usuario: Pikachu240
 * Fecha: 26/09/2017
 * Hora: 1:20
 * Licencia GNU GPL V3
 * Para cambiar esta plantilla use Herramientas | Opciones | Codificación | Editar Encabezados Estándar
 */
using System;
using Gabriel.Cat.Extension;
//de momento es para la region de Kanto...
namespace PokemonGBAFrameWork
{
	/// <summary>
	/// Description of GanarExperienciaAlCapturarUnPokemon.
	/// </summary>
	public static class GanarExperienciaAlCapturarUnPokemon
	{
		public static readonly Creditos Creditos;
		public static readonly Variable VarOffsetPointerRutina;
		public static readonly Variable VarOffsetRutinaOri;
		public static readonly Variable VarOffsetOffset2;
		public static readonly Variable VarOffset2;//como es un pointer varia en cada edicion

		static readonly Variable VarOffset2Off;

		public static readonly byte[] Rutina=new byte[]{0x2E, 0xE0, 0x3F, 0x02, 0x02, 0x00, 0x2E, 0x0C, 0x3C, 0x02, 0x02, 0x00, 0x2E, 0x0D, 0x3C, 0x02, 0x02, 0x00, 0x23, 0x00, 0xF1, 0x63, 0x9A, 0x1D, 0x08, 0x28, 0x58, 0x9A, 0x1D, 0x08};
		public static readonly Variable VarRutinaOffset1;
		public static readonly Variable VarRutinaOffset2;
		const int OFFSET1RUTINA=21;
		const int OFFSET2RUTINA=25;
		//se pone antes del pointer de la rutina
		const byte RUTINAON=0x41;
		const byte RUTINAOFF=0xF1;
		static GanarExperienciaAlCapturarUnPokemon()
		{
			Creditos=new Creditos();
			Creditos.Add(Creditos.Comunidades[Creditos.POKEMONCOMMUNITY],"Ismash","Tutorial");
			Creditos.Add(Creditos.Comunidades[Creditos.POKEMONCOMMUNITY],"DoesntKnowHowToPlay","Tutorial");
			
			//offset donde poner el pointer de la rutina
			VarOffsetPointerRutina=new Variable("offset rutina ganar experiencia al capturar un pokemon");
			VarOffsetPointerRutina.Add(EdicionPokemon.RojoFuegoUsa,0x1D9A53,0x1D9AC3);
			VarOffsetPointerRutina.Add(EdicionPokemon.VerdeHojaUsa,0x1D9A2F,0x1D9A9F);
			VarOffsetPointerRutina.Add(EdicionPokemon.RojoFuegoEsp,0x1D9587);
			VarOffsetPointerRutina.Add(EdicionPokemon.VerdeHojaEsp,0x1D9563);
			//por mirar por casualidad me he encontrado con los bytes que he buscado antes...no se si estará 100% funcional pero por si acaso los busco
			VarOffsetPointerRutina.Add(EdicionPokemon.EsmeraldaEsp,0x2E3521);
			VarOffsetPointerRutina.Add(EdicionPokemon.EsmeraldaUsa,0x2DBD95);
			VarOffsetPointerRutina.Add(EdicionPokemon.RubiUsa,0x1D9ED3,0x1D9EEB);
			VarOffsetPointerRutina.Add(EdicionPokemon.ZafiroUsa,0x1D9E63,0x1D9E7B);
			
			VarRutinaOffset1=new Variable("offset rutina 1");
			VarRutinaOffset1.Add(EdicionPokemon.RojoFuegoUsa,0x1D9A63,0x1D9AD3);
			VarRutinaOffset1.Add(EdicionPokemon.VerdeHojaUsa,0x1D9A3F,0x1D9AAF);
			VarRutinaOffset1.Add(EdicionPokemon.RojoFuegoEsp,0x1D9597);
			VarRutinaOffset1.Add(EdicionPokemon.VerdeHojaEsp,0x1D9573);
			
			VarRutinaOffset2=new Variable("offset rutina 2");
			VarRutinaOffset2.Add(EdicionPokemon.RojoFuegoUsa,0x1D9A58,0x1D9AC8);
			VarRutinaOffset2.Add(EdicionPokemon.VerdeHojaUsa,0x1D9A34,0x1D9AA4);
			VarRutinaOffset2.Add(EdicionPokemon.RojoFuegoEsp,0x1D958C);
			VarRutinaOffset2.Add(EdicionPokemon.VerdeHojaEsp,0x1D9568);
			
			//desactivar la rutina llama a la rutina de capturar o subir de nivel no tengo ni idea pero se que es lo mismo
			VarOffsetRutinaOri=VarRutinaOffset1;
			
			VarOffsetOffset2=new Variable("offset donde va el offset a poner");
			VarOffsetOffset2.Add(EdicionPokemon.RojoFuegoUsa,0x15A68,0x15A7C);
			VarOffsetOffset2.Add(EdicionPokemon.VerdeHojaUsa,0x15A68,0x15A7C);
			VarOffsetOffset2.Add(EdicionPokemon.RojoFuegoEsp,0x159D8);
			VarOffsetOffset2.Add(EdicionPokemon.VerdeHojaEsp,0x159D8);
			
			VarOffset2=new Variable("Offset2 a poner");
			VarOffset2.Add(EdicionPokemon.RojoFuegoUsa,0x015AA1,0x15AB5);
			VarOffset2.Add(EdicionPokemon.VerdeHojaUsa,0x15AA1,0x15AB5);
			VarOffset2.Add(EdicionPokemon.RojoFuegoEsp,0x15A11);
			VarOffset2.Add(EdicionPokemon.VerdeHojaEsp,0x15A11);
			
			VarOffset2Off=new Variable("Offset2 original");
			VarOffset2Off.Add(EdicionPokemon.RojoFuegoUsa,0x015B59,0x15B6D);
			VarOffset2Off.Add(EdicionPokemon.VerdeHojaUsa,0x15B59,0x15B8D);
			VarOffset2Off.Add(EdicionPokemon.RojoFuegoEsp,0x15AC9);
			VarOffset2Off.Add(EdicionPokemon.VerdeHojaEsp,0x15AC9);	
		}
		public static byte[] GetRutina(EdicionPokemon edicion,Compilacion compilacion)
		{
			byte[] rutina=(byte[])Rutina.Clone();
			
			rutina.SetArray(OFFSET1RUTINA,new OffsetRom(Variable.GetVariable(VarRutinaOffset1,edicion,compilacion)).BytesPointer);
			rutina.SetArray(OFFSET2RUTINA,new OffsetRom(Variable.GetVariable(VarRutinaOffset2,edicion,compilacion)).BytesPointer);
			return rutina;
		}
		public static int OffsetRutina(RomGba rom,EdicionPokemon edicion,Compilacion compilacion)
		{
			return IOffsetRutina(rom,GetRutina(edicion,compilacion));
		}
		static int IOffsetRutina(RomGba rom,byte[] rutina)
		{
			return rom.Data.SearchArray(rutina);
		}
		public static bool EstaActivado(RomGba rom,EdicionPokemon edicion,Compilacion compilacion)
		{
			return OffsetRutina(rom,edicion,compilacion)>0;
		}
		public static void Activar(RomGba rom,EdicionPokemon edicion,Compilacion compilacion)
		{
			byte[] rutina=GetRutina(edicion,compilacion);
			int offsetRutina=IOffsetRutina(rom,rutina);
			int offsetDondePonerElOffsetDeLaRutina;
			//pongo la rutina
			if(offsetRutina<0)
				offsetRutina=rom.Data.SetArray(rutina);
			offsetDondePonerElOffsetDeLaRutina=Variable.GetVariable(VarOffsetPointerRutina,edicion,compilacion);
			rom.Data[offsetDondePonerElOffsetDeLaRutina++]=RUTINAON;
			rom.Data.SetArray(offsetDondePonerElOffsetDeLaRutina,new OffsetRom(offsetRutina+1).BytesPointer);
			//pongo el pointer que toca
			rom.Data.SetArray(Variable.GetVariable(VarOffsetOffset2,edicion,compilacion),new OffsetRom(Variable.GetVariable(VarOffset2,edicion,compilacion)).BytesPointer);
		}
		public static void Desctivar(RomGba rom,EdicionPokemon edicion,Compilacion compilacion)
		{
			byte[] rutina=GetRutina(edicion,compilacion);
			int offsetRutina=IOffsetRutina(rom,rutina);
			int offsetDondePonerElOffsetDeLaRutina;
			//pongo la rutina
			if(offsetRutina>0){
				rom.Data.Remove(offsetRutina,rutina.Length);

			}
			offsetDondePonerElOffsetDeLaRutina=Variable.GetVariable(VarOffsetPointerRutina,edicion,compilacion);
			rom.Data[offsetDondePonerElOffsetDeLaRutina++]=RUTINAOFF;
			rom.Data.SetArray(offsetDondePonerElOffsetDeLaRutina,new OffsetRom(Variable.GetVariable(VarOffsetRutinaOri,edicion,compilacion)).BytesPointer);
			//pongo el pointer que toca
			rom.Data.SetArray(Variable.GetVariable(VarOffsetOffset2,edicion,compilacion),new OffsetRom(Variable.GetVariable(VarOffset2Off,edicion,compilacion)).BytesPointer);

		}

	}
}
