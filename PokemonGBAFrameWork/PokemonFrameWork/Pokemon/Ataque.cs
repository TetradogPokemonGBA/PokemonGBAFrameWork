/*
 * Created by SharpDevelop.
 * User: Pikachu240
 * Date: 11/03/2017
 * Time: 5:52
 * 
 * Código bajo licencia GNU
 *
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Collections.Generic;
using Gabriel.Cat;
using Gabriel.Cat.Extension;
namespace PokemonGBAFrameWork
{
	/// <summary>
	/// Description of Ataque.
	/// </summary>
	public class Ataque:IComparable
	{
		//son 9 bits en total de alli el 511 :) asi en 2 bytes hay ataque y nivel :)
		public const int MAXATAQUESSINASM = 511;//hasta que no sepa como se cambia para poner más se queda este maximo :) //hay un tutorial de como hacerlo pero se necesita insertar una rutina ASM link:http://www.pokecommunity.com/showthread.php?t=263479
		enum LongitudCampos
		{
			
			Nombre = 13,
			
			PointerEfecto = 4,
			ContestData,
			Descripcion = 4,//es un pointer al texto
			ScriptBatalla,
			Animacion,
			DatosConcurso=15
		}
		enum ValoresLimitadoresFin
		{
			Ataque =0x13E0,
			AnimacionesConcurso=0xE0,
			AtaqueConcurso=0
		}
		public static readonly Zona ZonaNombre;
		public static readonly Zona ZonaDescripcion;
		public static readonly Zona ZonaScriptBatalla;
		public static readonly Zona ZonaAnimacion;
		public static readonly Zona ZonaDatosConcursosHoenn;
		public static readonly Variable VariableLimitadoAtaques;
		public static readonly Variable VariableAtaqueConcurso;
		public static readonly Variable VariableAnimacionAtaqueConcurso;
		

		const int LENGTHLIMITADOR = 16;

		static readonly byte[] BytesDesLimitadoAtaques;
		static readonly byte[] BytesDesLimitadoAtaquesConcurso;
		static readonly byte[] BytesDesLimitadoAnimacionAtaques;
		
		BloqueString nombre;
		BloqueString descripcion;
		DatosAtaque datos;
		BloqueBytes datosConcursosHoenn;
		static Ataque()
		{
			byte[] valoresUnLimited = (((Hex)(int)ValoresLimitadoresFin.Ataque));
			byte[] valoresUnLimitedAtaque = { 0 };
			byte[] valoresUnLimitedAnimacion = (((Hex)(int)ValoresLimitadoresFin.AnimacionesConcurso));
			
			ZonaNombre=new Zona("NombreAtaque");
			ZonaDescripcion=new Zona("DescripciónAtaque");
			ZonaScriptBatalla=new Zona("ScriptAtaqueBatalla");
			ZonaAnimacion=new Zona("AnimaciónAtaque");
			ZonaDatosConcursosHoenn=new Zona("Datos concursos hoenn");
			VariableLimitadoAtaques=new Variable("VariableLimitadorAtaque");
			VariableAtaqueConcurso=new Variable("Variable Ataque concurso");
			VariableAnimacionAtaqueConcurso=new Variable("Variable Animacion Ataque concurso");
			//nombres
			ZonaNombre.Add(0x148,EdicionPokemon.EsmeraldaEsp,EdicionPokemon.EsmeraldaUsa,EdicionPokemon.RojoFuegoEsp,EdicionPokemon.RojoFuegoUsa,EdicionPokemon.VerdeHojaEsp,EdicionPokemon.VerdeHojaUsa,EdicionPokemon.RubiEsp,EdicionPokemon.ZafiroEsp);
			ZonaNombre.Add( 0x2e18c,EdicionPokemon.RubiUsa,EdicionPokemon.ZafiroUsa);

			//por investigar!!!
			//efectos el offset tiene que acabar en 0,4,8,C
			//de momento se tiene que investigar...lo que habia antes eran animaciones...

			//descripcion con ellas calculo el total :D
			ZonaDescripcion.Add(EdicionPokemon.RojoFuegoUsa, 0xE5440, 0xE5454);
			ZonaDescripcion.Add(EdicionPokemon.VerdeHojaUsa, 0xE5418, 0xE542C);
			ZonaDescripcion.Add(EdicionPokemon.RubiUsa, 0xA0494, 0xA04B4);
			ZonaDescripcion.Add(EdicionPokemon.ZafiroUsa, 0xA0494, 0xA04B4);
			ZonaDescripcion.Add(EdicionPokemon.EsmeraldaUsa, 0x1C3EFC);
			ZonaDescripcion.Add(EdicionPokemon.EsmeraldaEsp, 0x1C3B1C);
			ZonaDescripcion.Add(EdicionPokemon.RubiEsp, 0xA06C8);
			ZonaDescripcion.Add(EdicionPokemon.ZafiroEsp, 0xA06C8);
			ZonaDescripcion.Add(EdicionPokemon.RojoFuegoEsp, 0xE574C);
			ZonaDescripcion.Add(EdicionPokemon.VerdeHojaEsp, 0XE5724);
			//script batalla
			ZonaScriptBatalla.Add(0x162D4,EdicionPokemon.RojoFuegoEsp,EdicionPokemon.VerdeHojaEsp);
			ZonaScriptBatalla.Add(EdicionPokemon.RojoFuegoUsa, 0x16364, 0x16378);
			ZonaScriptBatalla.Add(EdicionPokemon.VerdeHojaUsa, 0x16364, 0x16378);

			ZonaScriptBatalla.Add(0x148B0,EdicionPokemon.RubiEsp,EdicionPokemon.ZafiroEsp);
			ZonaScriptBatalla.Add(0x3E854,EdicionPokemon.EsmeraldaUsa,EdicionPokemon.EsmeraldaEsp);
			ZonaScriptBatalla.Add(0x146E4,EdicionPokemon.RubiUsa,EdicionPokemon.ZafiroUsa);

			//animacion CON ESTO PUEDO DIFERENCIAR LAS VERSIONES ZAFIRO Y RUBI USA :D
			ZonaAnimacion.Add(0x72608,EdicionPokemon.RojoFuegoEsp,EdicionPokemon.VerdeHojaEsp);
			ZonaAnimacion.Add(EdicionPokemon.RojoFuegoUsa, 0x7250D0, 0x725E4);
			ZonaAnimacion.Add(EdicionPokemon.VerdeHojaUsa, 0x7250D0, 0x725E4);
			ZonaAnimacion.Add(EdicionPokemon.RubiUsa, 0x75734, 0x75754);
			ZonaAnimacion.Add(EdicionPokemon.ZafiroUsa, 0x75738, 0x75758);
			ZonaAnimacion.Add(EdicionPokemon.EsmeraldaUsa, 0xA3A44);
			ZonaAnimacion.Add(EdicionPokemon.EsmeraldaEsp, 0xA3A58);
			ZonaAnimacion.Add(EdicionPokemon.RubiEsp, 0x75BF0);
			ZonaAnimacion.Add(EdicionPokemon.ZafiroEsp, 0x75BF4);

			//añado la variable limitador
			VariableLimitadoAtaques.Add(EdicionPokemon.VerdeHojaUsa, 0xD75D0, 0xD75E4);
			VariableLimitadoAtaques.Add(EdicionPokemon.RojoFuegoUsa, 0xD75FC, 0xD7610);
			VariableLimitadoAtaques.Add(EdicionPokemon.EsmeraldaUsa, 0x14E504);
			VariableLimitadoAtaques.Add(EdicionPokemon.VerdeHojaEsp, 0xD7858);
			VariableLimitadoAtaques.Add(EdicionPokemon.EsmeraldaEsp, 0x14E138);
			VariableLimitadoAtaques.Add(EdicionPokemon.RojoFuegoEsp, 0xD7884);
			VariableLimitadoAtaques.Add(0xAC8C2,EdicionPokemon.RubiEsp,EdicionPokemon.ZafiroEsp);
			VariableLimitadoAtaques.Add(EdicionPokemon.RubiUsa, 0xAC676,0xAC696);
			VariableLimitadoAtaques.Add(EdicionPokemon.ZafiroUsa, 0xAC676, 0xAC696);
			
			//añado la parte de los concursos de hoenn
			ZonaDatosConcursosHoenn.Add(EdicionPokemon.EsmeraldaEsp, 0xD8248);
			ZonaDatosConcursosHoenn.Add(EdicionPokemon.EsmeraldaUsa, 0xD85F0);
			ZonaDatosConcursosHoenn.Add(EdicionPokemon.RubiEsp, 0xA04C4);
			ZonaDatosConcursosHoenn.Add(EdicionPokemon.ZafiroEsp, 0xA04C4);
			ZonaDatosConcursosHoenn.Add(EdicionPokemon.RubiUsa, 0xA0290,0XA02B0);
			ZonaDatosConcursosHoenn.Add(EdicionPokemon.ZafiroUsa, 0xA0290, 0XA02B0);
			//pongo las variables ataques
			VariableAtaqueConcurso.Add(EdicionPokemon.EsmeraldaEsp, 0xD8248);
			VariableAtaqueConcurso.Add(EdicionPokemon.EsmeraldaUsa, 0xD8F0C);//puesta
			VariableAtaqueConcurso.Add(EdicionPokemon.RubiEsp, 0xA04C4);
			VariableAtaqueConcurso.Add(EdicionPokemon.ZafiroEsp, 0xA04C4);
			VariableAtaqueConcurso.Add(EdicionPokemon.RubiUsa, 0xA0290, 0XA02B0);
			VariableAtaqueConcurso.Add(EdicionPokemon.ZafiroUsa, 0xA0290, 0XA02B0);
			//pongo las variables animaciones
			VariableAnimacionAtaqueConcurso.Add(EdicionPokemon.EsmeraldaEsp, 0xD8248);
			VariableAnimacionAtaqueConcurso.Add(EdicionPokemon.EsmeraldaUsa, 0xD8F0C);
			VariableAnimacionAtaqueConcurso.Add(EdicionPokemon.RubiEsp, 0xA04C4);
			VariableAnimacionAtaqueConcurso.Add(EdicionPokemon.ZafiroEsp, 0xA04C4);
			VariableAnimacionAtaqueConcurso.Add(EdicionPokemon.RubiUsa, 0xA0290, 0XA02B0);
			VariableAnimacionAtaqueConcurso.Add(EdicionPokemon.ZafiroUsa, 0xA0290, 0XA02B0);
			
			
			BytesDesLimitadoAtaques = new byte[LENGTHLIMITADOR];
			BytesDesLimitadoAtaques.SetArray(LENGTHLIMITADOR - valoresUnLimited.Length, valoresUnLimited);
			
			BytesDesLimitadoAtaquesConcurso = new byte[LENGTHLIMITADOR];
			BytesDesLimitadoAtaquesConcurso.SetArray(LENGTHLIMITADOR - valoresUnLimitedAtaque.Length, valoresUnLimitedAtaque);
			BytesDesLimitadoAnimacionAtaques = new byte[LENGTHLIMITADOR];
			BytesDesLimitadoAnimacionAtaques.SetArray(LENGTHLIMITADOR - valoresUnLimitedAnimacion.Length, valoresUnLimitedAnimacion);


		}
		public Ataque()
		{
			nombre=new BloqueString((int)LongitudCampos.Nombre);
			descripcion=new BloqueString();
			datos=new DatosAtaque();
			datosConcursosHoenn=new BloqueBytes((int)LongitudCampos.DatosConcurso);
		}
		#region Propiedades
		public BloqueString Nombre {
			get {
				return nombre;
			}
		}

		public BloqueString Descripcion {
			get {
				return descripcion;
			}
		}

		public DatosAtaque Datos {
			get {
				return datos;
			}
		}

		public BloqueBytes DatosConcursosHoenn {
			get {
				return datosConcursosHoenn;
			}
		}
		#region IComparable implementation


		public int CompareTo(object obj)
		{
			return nombre.CompareTo(obj);
		}


		#endregion

		#endregion
		public override string ToString()
		{
			return Nombre;
		}
		
		public static Ataque GetAtaque(RomData rom,int posicionAtaque)
		{
			return GetAtaque(rom.Rom,rom.Edicion,rom.Compilacion,posicionAtaque);
		}
		public static Ataque GetAtaque(RomGba rom,EdicionPokemon edicion,Compilacion compilacion,int posicionAtaque)
		{//por mirar la obtenxion del offset descripcion
			int offsetDescripcion;
			int offsetNombre= Zona.GetOffsetRom(rom,ZonaNombre,edicion,compilacion).Offset+posicionAtaque*(int)LongitudCampos.Nombre;
			
			Ataque ataque=new Ataque();
			
			ataque.nombre=BloqueString.GetString(rom,offsetNombre,(int)LongitudCampos.Nombre);
			if(posicionAtaque!=0)//el primero no tiene
			{
				offsetDescripcion= new OffsetRom(rom,Zona.GetOffsetRom(rom,ZonaDescripcion,edicion,compilacion).Offset+(posicionAtaque-1)*(int)LongitudCampos.Descripcion).Offset;
				ataque.descripcion=BloqueString.GetString(rom,offsetDescripcion);
			}
			else ataque.descripcion=null;
			
			ataque.datos=DatosAtaque.GetDatosAtaque(rom,edicion,compilacion,posicionAtaque);
			
			if(edicion.AbreviacionRom!=AbreviacionCanon.BPG&&edicion.AbreviacionRom!=AbreviacionCanon.BPR)
			{
				//pongo los datos de los concursos de hoenn
				ataque.DatosConcursosHoenn.Bytes = BloqueBytes.GetBytes(rom.Data, Zona.GetOffsetRom(rom, ZonaDatosConcursosHoenn, edicion, compilacion).Offset + posicionAtaque * OffsetRom.LENGTH, (int)LongitudCampos.DatosConcurso).Bytes;
			}
			
			return ataque;
		}
		public static Ataque[] GetAtaques(RomData rom)
		{
			return GetAtaques(rom.Rom,rom.Edicion,rom.Compilacion);
		}
		public static Ataque[] GetAtaques(RomGba rom,EdicionPokemon edicion,Compilacion compilacion)
		{
			Ataque[] ataques=new Ataque[GetTotalAtaques(rom,edicion,compilacion)];
			for(int i=0;i<ataques.Length;i++)
				ataques[i]=GetAtaque(rom,edicion,compilacion,i);
			return ataques;
		}
		public static void SetAtaque(RomData rom,int posicionAtaque,Ataque ataqueAPoner)
		{
			SetAtaque(rom.Rom,rom.Edicion,rom.Compilacion,posicionAtaque,ataqueAPoner);
		}
		public static void SetAtaque(RomGba rom,EdicionPokemon edicion,Compilacion compilacion,int posicionAtaque,Ataque ataqueAPoner)
		{
			OffsetRom offsetDatosConcurso;
			int offsetDescripcion;
			int offsetPointerDatos;
			int offsetNombre= Zona.GetOffsetRom(rom,ZonaNombre,edicion,compilacion).Offset+posicionAtaque*(int)LongitudCampos.Nombre;
			
			BloqueString.Remove(rom,offsetNombre);
			BloqueString.SetString(rom,offsetNombre,ataqueAPoner.Nombre);
			if(posicionAtaque!=0){
				
				offsetDescripcion=offsetDescripcion=new OffsetRom(rom,Zona.GetOffsetRom(rom,ZonaDescripcion,edicion,compilacion).Offset+(posicionAtaque-1)*(int)LongitudCampos.Descripcion).Offset;
				BloqueString.Remove(rom,offsetDescripcion);
				rom.Data.SetArray(offsetDescripcion,new OffsetRom(BloqueString.SetString(rom,ataqueAPoner.Descripcion)).BytesPointer);	
			}
			//pongo los datos
			DatosAtaque.SetDatosAtaque(rom,edicion,compilacion,posicionAtaque,ataqueAPoner.Datos);
			if(edicion.AbreviacionRom!=AbreviacionCanon.BPG&&edicion.AbreviacionRom!=AbreviacionCanon.BPR)
			{
				//pongo los datos de los concursos de hoenn
				
				offsetPointerDatos = Zona.GetOffsetRom(rom, ZonaDatosConcursosHoenn, edicion, compilacion).Offset + posicionAtaque * (int)LongitudCampos.DatosConcurso;
				offsetDatosConcurso=new OffsetRom(rom, offsetPointerDatos);
				if(new OffsetRom(rom,offsetPointerDatos).IsAPointer)
					rom.Data.Remove(offsetDatosConcurso.Offset, (int)LongitudCampos.DatosConcurso);//quito los viejos
				OffsetRom.SetOffset(rom, offsetDatosConcurso, rom.Data.SetArray(ataqueAPoner.DatosConcursosHoenn.Bytes));//pongo los nuevos
			}
			
			QuitarLimite(rom,edicion,compilacion,posicionAtaque);
		}
		public static void SetAtaques(RomData rom)
		{
			SetAtaques(rom.Rom,rom.Edicion,rom.Compilacion,rom.Ataques);
		}
		public static void SetAtaques(RomGba rom,EdicionPokemon edicion,Compilacion compilacion,IList<Ataque> ataques)
		{
			if(ataques.Count>MAXATAQUESSINASM)//mas adelante adapto el hack de Jambo
				throw new ArgumentOutOfRangeException("ataques");
			
			int offsetTablaPointersDescripcion;
			int offsetDescripcionActual;
			int totalActual=GetTotalAtaques(rom,edicion,compilacion);
			//borro los datos y busco un nuevo sitio donde quepan
			if(ataques.Count!=totalActual)
			{
				//borro los datos anteriores y busco espacio libre y cambio todos los offsets
				rom.Data.Remove(Zona.GetOffsetRom(rom,ZonaNombre,edicion,compilacion).Offset,(int)LongitudCampos.Nombre*totalActual);
				//borro las descripciones y luego los pointers
				offsetTablaPointersDescripcion=Zona.GetOffsetRom(rom,ZonaDescripcion,edicion,compilacion).Offset;

				for(int i=0;i<totalActual;i++)
				{
					offsetDescripcionActual=new OffsetRom(rom,offsetTablaPointersDescripcion+i*OffsetRom.LENGTH).Offset;
					//borro los datos
					BloqueString.Remove(rom,offsetDescripcionActual);
					
					
				}
				if(edicion.AbreviacionRom!=AbreviacionCanon.BPG&&edicion.AbreviacionRom!=AbreviacionCanon.BPR)
				{
					//borro los datos de los concursos
					rom.Data.Remove(Zona.GetOffsetRom(rom,ZonaDatosConcursosHoenn,edicion,compilacion).Offset,totalActual*(int)LongitudCampos.DatosConcurso);
				}
				rom.Data.Remove(Zona.GetOffsetRom(rom,DatosAtaque.ZonaDatosAtaques,edicion,compilacion).Offset,DatosAtaque.Longitud*totalActual,0xFF);
				rom.Data.Remove(offsetTablaPointersDescripcion,OffsetRom.LENGTH*totalActual,0xFF);
				
				//cambio los offsets de las zonas :D//falta los concursos
				if(ataques.Count>totalActual){//asi solo hago sitio si hay mas :)
					OffsetRom.SetOffset(rom,Zona.GetOffsetRom(rom,ZonaNombre,edicion,compilacion),rom.Data.SearchEmptyBytes((int)LongitudCampos.Nombre*ataques.Count));
					OffsetRom.SetOffset(rom,Zona.GetOffsetRom(rom,ZonaDescripcion,edicion,compilacion),rom.Data.SearchEmptyBytes((int)LongitudCampos.Descripcion*ataques.Count-1));//el primero no tiene
					OffsetRom.SetOffset(rom,Zona.GetOffsetRom(rom,DatosAtaque.ZonaDatosAtaques,edicion,compilacion),rom.Data.SearchEmptyBytes(DatosAtaque.Longitud*ataques.Count));
					if(edicion.AbreviacionRom!=AbreviacionCanon.BPG&&edicion.AbreviacionRom!=AbreviacionCanon.BPR)
					{
						//busco espacio para los datos del concurso
						OffsetRom.SetOffset(rom,Zona.GetOffsetRom(rom,ZonaDatosConcursosHoenn,edicion,compilacion),rom.Data.SearchEmptyBytes((int)LongitudCampos.DatosConcurso*ataques.Count));
				
					}
				}
			}
			
			for(int i=0;i<ataques.Count;i++)
				SetAtaque(rom,edicion,compilacion,i,ataques[i]);

		}
		public static int GetTotalAtaques(RomData rom)
		{
			return GetTotalAtaques(rom.Rom, rom.Edicion, rom.Compilacion);
		}
		public static int GetTotalAtaques(RomGba rom, EdicionPokemon edicion, Compilacion compilacion)
		{
			int offsetDescripciones = Zona.GetOffsetRom(rom, ZonaDescripcion, edicion, compilacion).Offset;
			int total =1;//el primero no tiene
			while (new OffsetRom(rom, offsetDescripciones).IsAPointer)
			{
				offsetDescripciones += OffsetRom.LENGTH;//avanzo hasta la proxima descripcion :)
				total++;
			}
			return total;
		}

		static void QuitarLimite(RomGba rom, EdicionPokemon edicion, Compilacion compilacion,int posicion)
		{
			//quito el limite
			if(posicion>GetTotalAtaques(rom,edicion,compilacion)){
				BloqueBytes.SetBytes(rom.Data, Variable.GetVariable(VariableLimitadoAtaques, edicion, compilacion), BytesDesLimitadoAtaques);
				if(edicion.AbreviacionRom!=AbreviacionCanon.BPG&&edicion.AbreviacionRom!=AbreviacionCanon.BPR)
				{
					//quito la limitacion de los concursos de hoenn
					BloqueBytes.SetBytes(rom.Data, Variable.GetVariable(VariableAtaqueConcurso, edicion, compilacion), BytesDesLimitadoAtaquesConcurso);
					BloqueBytes.SetBytes(rom.Data, Variable.GetVariable(VariableAnimacionAtaqueConcurso, edicion, compilacion), BytesDesLimitadoAnimacionAtaques);
					
				}
			}
		}
	}
	
	public class DatosAtaque : IComparable<DatosAtaque>
	{
		
		enum Custom
		{
			MakesContact = 1,
			Protect = 2,
			MagicCoat = 4,
			Snatch = 8,
			MirrorMove = 16,
			KingsRock = 32
		}
		enum CamposDatosAtaque
		{
			Effect,
			BasePower,
			Type,
			Accuracy,
			PP,
			EffectAccuracy,
			Target,
			Priority,
			Custom,
			PadByte1,
			Category,
			PadByte3
		}
		public enum Categoria
		{
			Fisico,Especial,Estatus
		}
		public static readonly Zona ZonaDatosAtaques;
		public const int Longitud = 12;
		static DatosAtaque()
		{
			ZonaDatosAtaques = new Zona("Datos ataque");

			//datos los pp es el offset de los datos+4 si se cambia el offset de los datos hay que cambiar el de los pps tambien!!!
			ZonaDatosAtaques.Add(0x1CC,EdicionPokemon.EsmeraldaEsp,EdicionPokemon.EsmeraldaUsa,EdicionPokemon.RojoFuegoEsp,EdicionPokemon.RojoFuegoUsa,EdicionPokemon.VerdeHojaEsp,EdicionPokemon.VerdeHojaUsa);

			ZonaDatosAtaques.Add(0xCC20,EdicionPokemon.RubiEsp,EdicionPokemon.ZafiroEsp);
			ZonaDatosAtaques.Add(0xCA54,EdicionPokemon.RubiUsa,EdicionPokemon.ZafiroUsa);

		}
		BloqueBytes blDatosAtaque;
		/* custom
          el primero 1 Makes contact
          el segundo 2 Is affected by Protect
          el tercero 4 is affected by magic coat
          el cuarto 8 is affected by snatch
          el quinto 10 is affected by mirror move
          el sexto 20 is affected by king's rock
		 */
		//los ultimos bytes puede ser la parte "Extra" de PGE -Attack Editor

		public DatosAtaque()
		{
			blDatosAtaque = new BloqueBytes(Longitud);
		}
		public DatosAtaque(byte effect, byte basePower, byte type, byte accuracy, byte pp, byte effectAccuracy, byte target, byte priority,bool makeContact,bool isAffectedByProtect,bool isAffectedByMagicCoat,bool isAffectedBySnatch,bool isAffectedByMirrorMove,bool isAffectedByKingsRock, byte padByte1, Categoria category, byte padByte3):this()
		{
			Effect = effect;
			BasePower = basePower;
			Type = type;
			Accuracy = accuracy;
			PP = pp;
			EffectAccuracy = effectAccuracy;
			Target = target;
			Priority = priority;

			MakesContact = makeContact;
			IsAffectedByProtect = isAffectedByProtect;
			IsAffectedBySnatch = isAffectedBySnatch;
			IsAffectedByMagicCoat = isAffectedByMagicCoat;
			IsAffectedByMirrorMove = isAffectedByMirrorMove;
			IsAffectedByKingsRock = isAffectedByKingsRock;

			PadByte1 = padByte1;
			Category = category;
			PadByte3 = padByte3;
		}
		#region Propiedades
		public bool MakesContact
		{
			get
			{
				return blDatosAtaque.Bytes[(int)CamposDatosAtaque.Custom] % 2 != 0;//si es impar es que hay el 1 de make contact
			}
			set
			{
				if (value)
				{
					if (!MakesContact)
						blDatosAtaque.Bytes[(int)CamposDatosAtaque.Custom]++;

				}
				else
				{
					if (MakesContact)
						blDatosAtaque.Bytes[(int)CamposDatosAtaque.Custom]--;
				}
			}
		}
		public bool IsAffectedByProtect
		{
			get
			{
				return IsCustomEnabled(1);
			}
			set
			{
				if (value)
				{
					if (!IsAffectedByProtect)
						blDatosAtaque.Bytes[(int)CamposDatosAtaque.Custom] += (int)Custom.Protect;
				}
				else
				{
					if (IsAffectedByProtect)
						blDatosAtaque.Bytes[(int)CamposDatosAtaque.Custom] -= (int)Custom.Protect;
				}

			}
		}
		public bool IsAffectedByMagicCoat
		{
			get
			{
				return IsCustomEnabled(2);
			}
			set
			{
				if (value)
				{
					if (!IsAffectedByMagicCoat)
						blDatosAtaque.Bytes[(int)CamposDatosAtaque.Custom] += (int)Custom.MagicCoat;
				}
				else
				{
					if (IsAffectedByMagicCoat)
						blDatosAtaque.Bytes[(int)CamposDatosAtaque.Custom] -= (int)Custom.MagicCoat;
				}

			}
		}
		public bool IsAffectedBySnatch
		{
			get
			{
				return IsCustomEnabled(3);
			}
			set
			{
				if (value)
				{
					if (!IsAffectedBySnatch)
						blDatosAtaque.Bytes[(int)CamposDatosAtaque.Custom] += (int)Custom.Snatch;
				}
				else
				{
					if (IsAffectedBySnatch)
						blDatosAtaque.Bytes[(int)CamposDatosAtaque.Custom] -= (int)Custom.Snatch;
				}

			}
		}
		public bool IsAffectedByMirrorMove
		{
			get
			{
				return IsCustomEnabled(4);
			}
			set
			{
				if (value)
				{
					if (!IsAffectedByMirrorMove)
						blDatosAtaque.Bytes[(int)CamposDatosAtaque.Custom] += (int)Custom.MirrorMove;
				}
				else
				{
					if (IsAffectedByMirrorMove)
						blDatosAtaque.Bytes[(int)CamposDatosAtaque.Custom] -= (int)Custom.MirrorMove;
				}

			}
		}
		public bool IsAffectedByKingsRock
		{
			get
			{
				return IsCustomEnabled(5);
			}
			set
			{
				if (value)
				{
					if (!IsAffectedByKingsRock)
						blDatosAtaque.Bytes[(int)CamposDatosAtaque.Custom] += (int)Custom.KingsRock;
				}
				else
				{
					if (IsAffectedByKingsRock)
						blDatosAtaque.Bytes[(int)CamposDatosAtaque.Custom] -= (int)Custom.KingsRock;
				}

			}
		}

		public byte Effect
		{
			get
			{
				return blDatosAtaque.Bytes[(int)CamposDatosAtaque.Effect];
			}

			set
			{
				blDatosAtaque.Bytes[(int)CamposDatosAtaque.Effect] = value;
			}
		}

		public byte BasePower
		{
			get
			{
				return blDatosAtaque.Bytes[(int)CamposDatosAtaque.BasePower];
			}

			set
			{
				blDatosAtaque.Bytes[(int)CamposDatosAtaque.BasePower] = value;
			}
		}

		public byte Type
		{
			get
			{
				return blDatosAtaque.Bytes[(int)CamposDatosAtaque.Type];
			}

			set
			{
				blDatosAtaque.Bytes[(int)CamposDatosAtaque.Type] = value;
			}
		}

		public byte Accuracy
		{
			get
			{
				return blDatosAtaque.Bytes[(int)CamposDatosAtaque.Accuracy];
			}

			set
			{
				blDatosAtaque.Bytes[(int)CamposDatosAtaque.Accuracy] = value;
			}
		}

		public byte PP
		{
			get
			{
				return blDatosAtaque.Bytes[(int)CamposDatosAtaque.PP];
			}

			set
			{
				blDatosAtaque.Bytes[(int)CamposDatosAtaque.PP] = (byte)(value%30);//el maximo es 30
			}
		}

		public byte EffectAccuracy
		{
			get
			{
				return blDatosAtaque.Bytes[(int)CamposDatosAtaque.EffectAccuracy];
			}

			set
			{
				blDatosAtaque.Bytes[(int)CamposDatosAtaque.EffectAccuracy] = value;
			}
		}

		public byte Target
		{
			get
			{
				return blDatosAtaque.Bytes[(int)CamposDatosAtaque.Target];
			}

			set
			{
				blDatosAtaque.Bytes[(int)CamposDatosAtaque.Target] = value;
			}
		}

		public byte Priority
		{
			get
			{
				return blDatosAtaque.Bytes[(int)CamposDatosAtaque.Priority];
			}

			set
			{
				blDatosAtaque.Bytes[(int)CamposDatosAtaque.Priority] = value;
			}
		}

		

		public byte PadByte1
		{
			get
			{
				return blDatosAtaque.Bytes[(int)CamposDatosAtaque.PadByte1];
			}

			set
			{
				blDatosAtaque.Bytes[(int)CamposDatosAtaque.PadByte1] = value;
			}
		}

		public Categoria Category
		{
			get
			{
				return (Categoria)blDatosAtaque.Bytes[(int)CamposDatosAtaque.Category];
			}

			set
			{
				blDatosAtaque.Bytes[(int)CamposDatosAtaque.Category] = (byte)value;
			}
		}

		public byte PadByte3
		{
			get
			{
				return blDatosAtaque.Bytes[(int)CamposDatosAtaque.PadByte3];
			}

			set
			{
				blDatosAtaque.Bytes[(int)CamposDatosAtaque.PadByte3] = value;
			}
		}
		#endregion
		private bool IsCustomEnabled(int indexCustomToCalculate)
		{
			bool isCustomEnabled;
			int aux = blDatosAtaque.Bytes[(int)CamposDatosAtaque.Custom];
			Custom[] customs = Enum.GetValues(typeof(Custom)) as Custom[];
			for (int i = customs.Length - 1; i > indexCustomToCalculate; i--)
				if(aux>= (int)customs[i])
					aux -= (int)customs[i];
			isCustomEnabled = aux >= (int)customs[indexCustomToCalculate];
			return isCustomEnabled;
		}
		public new int CompareTo(object obj)
		{
			return CompareTo(obj as DatosAtaque);
		}
		public int CompareTo(DatosAtaque other)
		{
			int compareTo;
			if (other != null)
			{
				compareTo = (int)Gabriel.Cat.CompareTo.Iguales;
				for (int i = 0; i < blDatosAtaque.Bytes.Length && compareTo == (int)Gabriel.Cat.CompareTo.Iguales; i++)
					compareTo = blDatosAtaque.Bytes[i].CompareTo(other.blDatosAtaque.Bytes[i]);

			}
			else compareTo = (int)Gabriel.Cat.CompareTo.Inferior;
			return compareTo;
		}
		public static DatosAtaque GetDatosAtaque(RomGba rom, EdicionPokemon edicion, Compilacion compilacion, int posicion)
		{
			return new DatosAtaque() { blDatosAtaque = BloqueBytes.GetBytes(rom.Data, Zona.GetOffsetRom(rom,ZonaDatosAtaques, edicion, compilacion).Offset + posicion * Longitud, Longitud) };
		}
		public static void SetDatosAtaque(RomGba rom, EdicionPokemon edicion, Compilacion compilacion, int posicion,DatosAtaque datosAtaque)
		{
			BloqueBytes.SetBytes(rom.Data, Zona.GetOffsetRom(rom,ZonaDatosAtaques, edicion, compilacion).Offset + posicion * Longitud, datosAtaque.blDatosAtaque.Bytes);
		}

		
		
	}
}
