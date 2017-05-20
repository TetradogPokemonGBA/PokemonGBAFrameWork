/*
 * Creado por SharpDevelop.
 * Usuario: tetra
 * Fecha: 20/05/2017
 * Hora: 16:11
 * Licencia GNU GPL V3
 * Para cambiar esta plantilla use Herramientas | Opciones | Codificación | Editar Encabezados Estándar
 *Creditos a Javi4315♪
 */
using System;
using System.Text;
using Gabriel.Cat;
using Gabriel.Cat.Extension;

namespace PokemonGBAFrameWork
{
	/// <summary>
	/// Description of PokemonEditorScripting.
	/// </summary>
	public class PokemonEditorScripting
	{
		const int OFFSETPOKEMONDESENCRIPTADO= 0x203F500;
		
		int? personalidad;
		int? idEntrenador;
		BloqueString nombrePokemon;//max 10
		short? idioma;
		BloqueString nombreEntrenadorOriginal;//max 7
		byte? marcas;
		short? cheksum;
		byte? relleno;//???
		short? especie;
		short? objeto;
		int? experiencia;
		byte? masPP;
		byte? amistad;
		short? desconocido;
		short? ataque1;
		short? ataque2;
		short? ataque3;
		short? ataque4;
		byte? ppAtaque1;
		byte? ppAtaque2;
		byte? ppAtaque3;
		byte? ppAtaque4;
		byte? evSalud;
		byte? evDefensa;
		byte? evVelocidad;
		byte? evAtaqueEspecial;
		byte? evDefensaEspecial;
		byte? carisma;
		byte? belleza;
		byte? dulzura;
		byte? ingenio;
		byte? dureza;
		byte? feel;
		byte? estadoPokerus;
		byte? localizacion;
		short? origen;
		//cuando esten separdos los pongo
		int? ivsHueboHabilidad;
		int? cintaYObediencia;
		
		public PokemonEditorScripting()
		{
			nombreEntrenadorOriginal=new BloqueString(7);
			nombrePokemon=new BloqueString(10);
		}

		public int? Personalidad {
			get {
				return personalidad;
			}
			set {
				personalidad = value;
			}
		}

		public int? IdEntrenador {
			get {
				return idEntrenador;
			}
			set {
				idEntrenador = value;
			}
		}

		public BloqueString NombrePokemon {
			get {
				return nombrePokemon;
			}
			set {
				nombrePokemon = value;
			}
		}

		public short? Idioma {
			get {
				return idioma;
			}
			set {
				idioma = value;
			}
		}

		public byte? Marcas {
			get {
				return marcas;
			}
			set {
				marcas = value;
			}
		}

		public short? Cheksum {
			get {
				return cheksum;
			}
			set {
				cheksum = value;
			}
		}

		public byte? Relleno {
			get {
				return relleno;
			}
			set {
				relleno = value;
			}
		}
		public short? Especie {
			get {
				return especie;
			}
			set {
				especie = value;
			}
		}

		public short? Objeto {
			get {
				return objeto;
			}
			set {
				objeto = value;
			}
		}

		public int? Experiencia {
			get {
				return experiencia;
			}
			set {
				experiencia = value;
			}
		}

		public byte? MasPP {
			get {
				return masPP;
			}
			set {
				masPP = value;
			}
		}

		public byte? Amistad {
			get {
				return amistad;
			}
			set {
				amistad = value;
			}
		}

		public short? Desconocido {
			get {
				return desconocido;
			}
			set {
				desconocido = value;
			}
		}

		public short? Ataque1 {
			get {
				return ataque1;
			}
			set {
				ataque1 = value;
			}
		}

		public short? Ataque2 {
			get {
				return ataque2;
			}
			set {
				ataque2 = value;
			}
		}

		public short? Ataque3 {
			get {
				return ataque3;
			}
			set {
				ataque3 = value;
			}
		}

		public short? Ataque4 {
			get {
				return ataque4;
			}
			set {
				ataque4 = value;
			}
		}

		public byte? PpAtaque1 {
			get {
				return ppAtaque1;
			}
			set {
				ppAtaque1 = value;
			}
		}

		public byte? PpAtaque2 {
			get {
				return ppAtaque2;
			}
			set {
				ppAtaque2 = value;
			}
		}

		public byte? PpAtaque3 {
			get {
				return ppAtaque3;
			}
			set {
				ppAtaque3 = value;
			}
		}

		public byte? PpAtaque4 {
			get {
				return ppAtaque4;
			}
			set {
				ppAtaque4 = value;
			}
		}

		public byte? EvSalud {
			get {
				return evSalud;
			}
			set {
				evSalud = value;
			}
		}

		public byte? EvDefensa {
			get {
				return evDefensa;
			}
			set {
				evDefensa = value;
			}
		}

		public byte? EvVelocidad {
			get {
				return evVelocidad;
			}
			set {
				evVelocidad = value;
			}
		}

		public byte? EvAtaqueEspecial {
			get {
				return evAtaqueEspecial;
			}
			set {
				evAtaqueEspecial = value;
			}
		}

		public byte? EvDefensaEspecial {
			get {
				return evDefensaEspecial;
			}
			set {
				evDefensaEspecial = value;
			}
		}

		public byte? Carisma {
			get {
				return carisma;
			}
			set {
				carisma = value;
			}
		}

		public byte? Belleza {
			get {
				return belleza;
			}
			set {
				belleza = value;
			}
		}

		public byte? Dulzura {
			get {
				return dulzura;
			}
			set {
				dulzura = value;
			}
		}

		public byte? Ingenio {
			get {
				return ingenio;
			}
			set {
				ingenio = value;
			}
		}

		public byte? Dureza {
			get {
				return dureza;
			}
			set {
				dureza = value;
			}
		}

		public byte? Feel {
			get {
				return feel;
			}
			set {
				feel = value;
			}
		}

		public byte? EstadoPokerus {
			get {
				return estadoPokerus;
			}
			set {
				estadoPokerus = value;
			}
		}

		public byte? Localizacion {
			get {
				return localizacion;
			}
			set {
				localizacion = value;
			}
		}

		public short? Origen {
			get {
				return origen;
			}
			set {
				origen = value;
			}
		}
		public void SetPokemon(Pokemon pokemon)
		{
			if(pokemon==null)
				throw new ArgumentNullException();
			
			especie=Convert.ToInt16(pokemon.OrdenNacional);
		}
		public void SetPokemon(Objeto obj)
		{
			if(obj==null)
				throw new ArgumentNullException();
			
			objeto=obj.Index;
		
		}
		public string ScriptXSE(int posicionEquipo,RomData rom=null)
		{
			return ScriptXSE(posicionEquipo,rom!=null?rom.Rom:null,rom!=null?rom.Edicion:null,rom!=null?rom.Compilacion:null);
		}
		public string ScriptXSE(int posicionEquipo,RomGba rom,EdicionPokemon edicion,Compilacion compilacion)
		{
			StringBuilder strScript=new StringBuilder();
			byte[] aux;
			strScript.Append("#dynamic 0x800000\n#org @start\n");
			strScript.Append("//antes\n");
			strScript.Append("setvar 0x8000 0x"+posicionEquipo+"\n");
			strScript.Append("callasm "+PosicionEncryptASMScript(rom,edicion,compilacion)+"\n");
			//pongo los datos
			if(personalidad.HasValue)
			{
				aux=Serializar.GetBytes(personalidad.Value);
				for(int i=0;i<4;i++)
					strScript.Append("writebytooffset "+((Hex)aux[i]).ByteString+" "+((Hex)(OFFSETPOKEMONDESENCRIPTADO)+i).ByteString+"\n");
			}
			if(idEntrenador.HasValue)
			{
				aux=Serializar.GetBytes(idEntrenador.Value);
				for(int i=0;i<4;i++)
					strScript.Append("writebytooffset "+((Hex)aux[i]).ByteString+" "+((Hex)(OFFSETPOKEMONDESENCRIPTADO)+4+i).ByteString+"\n");
			}
			if(nombrePokemon!=null){
				aux=BloqueString.ToByteArray(nombrePokemon.Texto,false);
				for(int i=0;i<aux.Length&&i<10;i++)
					strScript.Append("writebytooffset "+((Hex)aux[i]).ByteString+" "+((Hex)(OFFSETPOKEMONDESENCRIPTADO)+4+4+i).ByteString+"\n");
			}
			if(idioma.HasValue)
			{
				aux=Serializar.GetBytes(idioma.Value);
				for(int i=0;i<2;i++)
					strScript.Append("writebytooffset "+((Hex)aux[i]).ByteString+" "+((Hex)(OFFSETPOKEMONDESENCRIPTADO)+4+4+10+i).ByteString+"\n");
			}
			if(marcas.HasValue)
				strScript.Append("writebytooffset "+((Hex)marcas.Value).ByteString+" "+((Hex)(OFFSETPOKEMONDESENCRIPTADO)+4+4+10+2).ByteString+"\n");
			if(cheksum.HasValue)
			{
				aux=Serializar.GetBytes(idioma.Value);
				for(int i=0;i<2;i++)
					strScript.Append("writebytooffset "+((Hex)aux[i]).ByteString+" "+((Hex)(OFFSETPOKEMONDESENCRIPTADO)+4+4+10+2+1+i).ByteString+"\n");
			}
			if(relleno.HasValue)
				strScript.Append("writebytooffset "+((Hex)relleno.Value).ByteString+" "+((Hex)(OFFSETPOKEMONDESENCRIPTADO)+4+4+10+2+2+1).ByteString+"\n");
			
			if(especie.HasValue)
			{
				aux=Serializar.GetBytes(especie.Value);
				for(int i=0;i<aux.Length;i++)
					strScript.Append("writebytooffset "+((Hex)aux[i]).ByteString+" "+((Hex)(OFFSETPOKEMONDESENCRIPTADO)+4+4+10+2+1+1+i).ByteString+"\n");

			}
			if(objeto.HasValue)
			{
				aux=Serializar.GetBytes(objeto.Value);
				for(int i=0;i<aux.Length;i++)
					strScript.Append("writebytooffset "+((Hex)aux[i]).ByteString+" "+((Hex)(OFFSETPOKEMONDESENCRIPTADO)+4+4+10+2+1+1+2+i).ByteString+"\n");

			}
			//poner el resto de valores
			
			strScript.Append("callasm "+PosicionDecryptASMScript(rom,edicion,compilacion)+"\n");
			strScript.Append("//después\n");
			strScript.Append("end");
			return strScript.ToString();
		}
		public byte[] BytesScript(int posicionEquipo,RomData rom=null)
		{
			return BytesScript(posicionEquipo,rom!=null?rom.Rom:null,rom!=null?rom.Edicion:null,rom!=null?rom.Compilacion:null);
		}
		public byte[] BytesScript(int posicionEquipo,RomGba rom,EdicionPokemon edicion,Compilacion compilacion)
		{//por mirar y probar
			const byte SETVAR=0x16,WRITEBYOOFFSET=0x11,CALLASM=0x23,END=0x2;
			string strScript=ScriptXSE(posicionEquipo,rom,edicion,compilacion);
			string[] partes=strScript.Split('\n');
			string[] aux;
			byte[] bytes=new byte[1+5+5+(partes.Length-7)*6];
			//0x16 wordVariable wordValue x1
			aux=partes[3].Split(' ');
			bytes[0]=SETVAR;
			bytes.SetArray(1,Serializar.GetBytes((byte)((Hex)aux[1].Split('x')[1])));
			bytes.SetArray(3,Serializar.GetBytes((byte)((Hex)aux[2].Split('x')[1])));
			aux=partes[4].Split(' ');
			//0x23 pointer
			bytes[4]=CALLASM;
			bytes.SetArray(5,new OffsetRom( Serializar.GetBytes((int)((Hex)aux[1].Split('x')[1]))).BytesPointer);
			//0x11 byte pointer partes-7
			for(int i=5,f=partes.Length-2;i<f;i++)
			{
				aux=partes[i].Split(' ');
				bytes[9+((i-5)*6)]=WRITEBYOOFFSET;
				bytes.SetArray(10+((i-5)*6),Serializar.GetBytes((byte)((Hex)aux[1].Split('x')[1])));
			bytes.SetArray(11+((i-5)*6),new OffsetRom(Serializar.GetBytes((int)((Hex)aux[2].Split('x')[1]))).BytesPointer);
			}
			//0x23 pointer
			bytes[bytes.Length-7]=CALLASM;
			bytes.SetArray(bytes.Length-6,new OffsetRom( Serializar.GetBytes((int)((Hex)aux[1].Split('x')[1]))).BytesPointer);
			
			//0x02
			bytes[bytes.Length-1]=END;
			
			return bytes;
			
		}

		public static string PosicionEncryptASMScript(RomGba rom, EdicionPokemon edicion, Compilacion compilacion)
		{
			
		}
		public static string PosicionDecryptASMScript(RomGba rom, EdicionPokemon edicion, Compilacion compilacion)
		{
			
		}
		
		public static bool EstaActivado(RomGba rom, EdicionPokemon edicion, Compilacion compilacion)
		{
			return PosicionEncryptASMScript(rom,edicion,compilacion)[0]!='@';
		}
		public static void Activar(RomGba rom, EdicionPokemon edicion, Compilacion compilacion)
		{
		}
		public static void Desactivar(RomGba rom, EdicionPokemon edicion, Compilacion compilacion)
		{
		}
	}
}
