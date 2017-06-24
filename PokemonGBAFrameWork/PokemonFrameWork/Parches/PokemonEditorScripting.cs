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
		byte? evAtaque;//faltaba creo que va aqui
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

		public byte? EvAtaque {
			get {
				return evAtaque;
			}
			set {
				evAtaque = value;
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
		public void SetObjeto(Objeto obj)
		{
			if(obj==null)
				throw new ArgumentNullException();
			
			objeto=obj.Index;
			
		}
		public Script.Script Script(int posicionEquipo,RomData rom=null)
		{
			return Script(posicionEquipo,rom!=null?rom.Rom:null,rom!=null?rom.Edicion:null,rom!=null?rom.Compilacion:null);
		}
		public Script.Script Script(int posicionEquipo,RomGba rom,EdicionPokemon edicion,Compilacion compilacion)
		{
			const int VARIABLEPOKEMONEQUIPO=0x8000;
			byte[] aux;
			int auxPos=OFFSETPOKEMONDESENCRIPTADO;
			Script.Script scritpEditorPokemon=new Script.Script();
			
			scritpEditorPokemon.ComandosScript.Add(new Script.SetVar(VARIABLEPOKEMONEQUIPO,posicionEquipo));
			scritpEditorPokemon.ComandosScript.Add(new Script.CallAsm(PosicionEncryptASMScript(rom,edicion,compilacion)));
			//pongo los datos //mirar de poner el nombre de los parametros para asi poder identificar cada linea :)
			if(personalidad.HasValue)
			{
				aux=Serializar.GetBytes(personalidad.Value);
				for(int i=0;i<4;i++)
					scritpEditorPokemon.ComandosScript.Add( new Script.WriteByteToOffset(auxPos++,aux[i]));
			}
			else auxPos+=4;
			if(idEntrenador.HasValue)
			{
				aux=Serializar.GetBytes(idEntrenador.Value);
				for(int i=0;i<4;i++)
					scritpEditorPokemon.ComandosScript.Add( new Script.WriteByteToOffset(auxPos++,aux[i]));
			}else auxPos+=4;
			if(nombrePokemon!=null){
				aux=BloqueString.ToByteArray(nombrePokemon.Texto,false);
				for(int i=0;i<aux.Length&&i<10;i++)
					scritpEditorPokemon.ComandosScript.Add( new Script.WriteByteToOffset(auxPos++,aux[i]));
			}else auxPos+=10;
			if(idioma.HasValue)
			{
				aux=Serializar.GetBytes(idioma.Value);
				for(int i=0;i<aux.Length;i++)
					scritpEditorPokemon.ComandosScript.Add( new Script.WriteByteToOffset(auxPos++,aux[i]));
			}else auxPos+=2;
			if(marcas.HasValue)
				scritpEditorPokemon.ComandosScript.Add( new Script.WriteByteToOffset(auxPos++,Marcas.Value));
			else auxPos++;
			if(cheksum.HasValue)
			{
				aux=Serializar.GetBytes(idioma.Value);
				for(int i=0;i<2;i++)
					scritpEditorPokemon.ComandosScript.Add( new Script.WriteByteToOffset(auxPos++,aux[i]));
			}else auxPos+=2;
			if(relleno.HasValue)
				scritpEditorPokemon.ComandosScript.Add( new Script.WriteByteToOffset(auxPos++,relleno.Value));
			else auxPos++;
			if(especie.HasValue)
			{
				aux=Serializar.GetBytes(especie.Value);
				for(int i=0;i<aux.Length;i++)
					scritpEditorPokemon.ComandosScript.Add( new Script.WriteByteToOffset(auxPos++,aux[i]));

			}else auxPos+=2;
			if(objeto.HasValue)
			{
				aux=Serializar.GetBytes(objeto.Value);
				for(int i=0;i<aux.Length;i++)
					scritpEditorPokemon.ComandosScript.Add( new Script.WriteByteToOffset(auxPos++,aux[i]));

			}else auxPos+=2;
			//poner el resto de valores
			if(experiencia.HasValue)
			{
				aux=Serializar.GetBytes(experiencia.Value);
				for(int i=0;i<aux.Length;i++)
					scritpEditorPokemon.ComandosScript.Add( new Script.WriteByteToOffset(auxPos++,aux[i]));

			}else auxPos+=4;

			if(MasPP.HasValue)
				scritpEditorPokemon.ComandosScript.Add( new Script.WriteByteToOffset(auxPos++,MasPP.Value));
			else auxPos++;
			if(Amistad.HasValue)
				scritpEditorPokemon.ComandosScript.Add( new Script.WriteByteToOffset(auxPos++,Amistad.Value));
			else auxPos++;
			if(desconocido.HasValue)
			{
				aux=Serializar.GetBytes(desconocido.Value);
				for(int i=0;i<aux.Length;i++)
					scritpEditorPokemon.ComandosScript.Add( new Script.WriteByteToOffset(auxPos++,aux[i]));

			}
			else auxPos+=2;
			if(ataque1.HasValue)
			{
				aux=Serializar.GetBytes(ataque1.Value);
				for(int i=0;i<aux.Length;i++)
					scritpEditorPokemon.ComandosScript.Add( new Script.WriteByteToOffset(auxPos++,aux[i]));

			}
			else auxPos+=2;
			if(ataque2.HasValue)
			{
				aux=Serializar.GetBytes(ataque2.Value);
				for(int i=0;i<aux.Length;i++)
					scritpEditorPokemon.ComandosScript.Add( new Script.WriteByteToOffset(auxPos++,aux[i]));

			}
			else auxPos+=2;
			if(ataque3.HasValue)
			{
				aux=Serializar.GetBytes(ataque3.Value);
				for(int i=0;i<aux.Length;i++)
					scritpEditorPokemon.ComandosScript.Add( new Script.WriteByteToOffset(auxPos++,aux[i]));

			}
			else auxPos+=2;
			if(ataque4.HasValue)
			{
				aux=Serializar.GetBytes(ataque4.Value);
				for(int i=0;i<aux.Length;i++)
					scritpEditorPokemon.ComandosScript.Add( new Script.WriteByteToOffset(auxPos++,aux[i]));

			}
			else auxPos+=2;
			if(Amistad.HasValue)
				scritpEditorPokemon.ComandosScript.Add( new Script.WriteByteToOffset(auxPos++,Amistad.Value));
			else auxPos++;
			if(PpAtaque1.HasValue)
				scritpEditorPokemon.ComandosScript.Add( new Script.WriteByteToOffset(auxPos++,PpAtaque1.Value));
			else auxPos++;
			if(PpAtaque2.HasValue)
				scritpEditorPokemon.ComandosScript.Add( new Script.WriteByteToOffset(auxPos++,PpAtaque2.Value));
			else auxPos++;
			if(PpAtaque3.HasValue)
				scritpEditorPokemon.ComandosScript.Add( new Script.WriteByteToOffset(auxPos++,PpAtaque3.Value));
			else auxPos++;
			if(PpAtaque4.HasValue)
				scritpEditorPokemon.ComandosScript.Add( new Script.WriteByteToOffset(auxPos++,PpAtaque4.Value));
			else auxPos++;
			if(EvSalud.HasValue)
				scritpEditorPokemon.ComandosScript.Add( new Script.WriteByteToOffset(auxPos++,EvSalud.Value));
			else auxPos++;
			if(EvAtaque.HasValue)//creo que va aqui
				scritpEditorPokemon.ComandosScript.Add( new Script.WriteByteToOffset(auxPos++,EvAtaque.Value));
			else auxPos++;
			if(EvDefensa.HasValue)
				scritpEditorPokemon.ComandosScript.Add( new Script.WriteByteToOffset(auxPos++,EvDefensa.Value));
			else auxPos++;
			if(EvVelocidad.HasValue)
				scritpEditorPokemon.ComandosScript.Add( new Script.WriteByteToOffset(auxPos++,EvVelocidad.Value));
			else auxPos++;
			if(EvAtaqueEspecial.HasValue)
				scritpEditorPokemon.ComandosScript.Add( new Script.WriteByteToOffset(auxPos++,EvAtaqueEspecial.Value));
			else auxPos++;
			if(EvDefensaEspecial.HasValue)
				scritpEditorPokemon.ComandosScript.Add( new Script.WriteByteToOffset(auxPos++,EvDefensaEspecial.Value));
			else auxPos++;
			if(Carisma.HasValue)
				scritpEditorPokemon.ComandosScript.Add( new Script.WriteByteToOffset(auxPos++,Carisma.Value));
			else auxPos++;
			if(Dulzura.HasValue)
				scritpEditorPokemon.ComandosScript.Add( new Script.WriteByteToOffset(auxPos++,Dulzura.Value));
			else auxPos++;
			if(Ingenio.HasValue)
				scritpEditorPokemon.ComandosScript.Add( new Script.WriteByteToOffset(auxPos++,Ingenio.Value));
			else auxPos++;
			if(Dureza.HasValue)
				scritpEditorPokemon.ComandosScript.Add( new Script.WriteByteToOffset(auxPos++,Dureza.Value));
			else auxPos++;
			if(Feel.HasValue)
				scritpEditorPokemon.ComandosScript.Add( new Script.WriteByteToOffset(auxPos++,Feel.Value));
			else auxPos++;
			if(EstadoPokerus.HasValue)
				scritpEditorPokemon.ComandosScript.Add( new Script.WriteByteToOffset(auxPos++,EstadoPokerus.Value));
			else auxPos++;
			if(Localizacion.HasValue)
				scritpEditorPokemon.ComandosScript.Add( new Script.WriteByteToOffset(auxPos++,Localizacion.Value));
			else auxPos++;
			if(origen.HasValue)
			{
				aux=Serializar.GetBytes(origen.Value);
				for(int i=0;i<aux.Length;i++)
					scritpEditorPokemon.ComandosScript.Add( new Script.WriteByteToOffset(auxPos++,aux[i]));

			}
			else auxPos+=2;
			//cuando entienda como van los demás valores los pongo :)
			scritpEditorPokemon.ComandosScript.Add(new Script.CallAsm(PosicionDecryptASMScript(rom,edicion,compilacion)));

			return scritpEditorPokemon;
		}
		

		public static int PosicionEncryptASMScript(RomGba rom, EdicionPokemon edicion, Compilacion compilacion)
		{
			return -1;
		}
		public static int PosicionDecryptASMScript(RomGba rom, EdicionPokemon edicion, Compilacion compilacion)
		{
			return -1;
		}
		
		public static bool EstaActivado(RomGba rom, EdicionPokemon edicion, Compilacion compilacion)
		{
			return PosicionEncryptASMScript(rom,edicion,compilacion)>-1;
		}
		public static void Activar(RomGba rom, EdicionPokemon edicion, Compilacion compilacion)
		{
		}
		public static void Desactivar(RomGba rom, EdicionPokemon edicion, Compilacion compilacion)
		{
		}
	}
}
