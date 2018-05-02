/*
 * Creado por SharpDevelop.
 * Usuario: pikachu240
 * Fecha: 20/05/2017
 * Hora: 16:11
 * Licencia GNU GPL V3
 * Para cambiar esta plantilla use Herramientas | Opciones | Codificación | Editar Encabezados Estándar
 */
using Gabriel.Cat.S.Utilitats;
using System;
using System.Text;


namespace PokemonGBAFrameWork
{
	/// <summary>
	/// Description of PokemonEditorScripting.
	/// </summary>
	public class PokemonEditorScripting
	{
		const int OFFSETPOKEMONDESENCRIPTADO= 0x203F500;
		public static readonly LlistaOrdenada<EdicionPokemon,ASM> ASMDecrypt;
		public static readonly LlistaOrdenada<EdicionPokemon, ASM> ASMEncrypt;
		public static readonly Creditos Creditos;
		public const string DESCRIPCION="Permite editar un pokemon del equipo mediante scripting";
		
		#region Atributos
		DWord personalidad;
		DWord idEntrenador;
		BloqueString nombrePokemon;//max 10
		Word idioma;
		BloqueString nombreEntrenadorOriginal;//max 7
		byte? marcas;
		Word cheksum;
		byte? relleno;//???
		Word especie;
		Word objeto;
		DWord experiencia;
		byte? masPP;
		byte? amistad;
		Word desconocido;
		Word ataque1;
		Word ataque2;
		Word ataque3;
		Word ataque4;
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
		Word origen;
		//cuando esten separdos los pongo
		DWord ivsHueboHabilidad;
		DWord cintaYObediencia;
		#endregion

		static PokemonEditorScripting()
		{

			ASMDecrypt = new LlistaOrdenada<EdicionPokemon, ASM>();
			ASMEncrypt = new LlistaOrdenada<EdicionPokemon, ASM>();

            ASMDecrypt.Add(EdicionPokemon.RojoFuegoEsp, ASM.Compilar(Recursos.RecursosStrings.ASMEditorPokemonFRDencrypt));
            ASMEncrypt.Add(EdicionPokemon.RojoFuegoEsp, ASM.Compilar(Recursos.RecursosStrings.ASMEditorPokemonFREncrypt));
            Creditos =new Creditos();
			Creditos.Add(Creditos.Comunidades[Creditos.WAHACKFORO],"Javi4315♪","Ha hecho la rutina y lo ha explicado en un tutorial");
			//pongo el codigo compilado para cada edición
		}
		public PokemonEditorScripting()
		{
			nombreEntrenadorOriginal=new BloqueString(7);
			nombrePokemon=new BloqueString(10);
		}
		#region Propiedades
		public DWord Personalidad {
			get {
				return personalidad;
			}
			set {
				personalidad = value;
			}
		}

		public DWord IdEntrenador {
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

		public Word Idioma {
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

		public Word Cheksum {
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
		public Word Especie {
			get {
				return especie;
			}
			set {
				especie = value;
			}
		}

		public Word Objeto {
			get {
				return objeto;
			}
			set {
				objeto = value;
			}
		}

		public DWord Experiencia {
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

		public Word Desconocido {
			get {
				return desconocido;
			}
			set {
				desconocido = value;
			}
		}

		public Word Ataque1 {
			get {
				return ataque1;
			}
			set {
				ataque1 = value;
			}
		}

		public Word Ataque2 {
			get {
				return ataque2;
			}
			set {
				ataque2 = value;
			}
		}

		public Word Ataque3 {
			get {
				return ataque3;
			}
			set {
				ataque3 = value;
			}
		}

		public Word Ataque4 {
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

		public Word Origen {
			get {
				return origen;
			}
			set {
				origen = value;
			}
		}
		#endregion

		public void SetPokemon(Pokemon pokemon)
		{
			if(pokemon==null)
				throw new ArgumentNullException();
			
			especie=pokemon.OrdenNacional;
		}
		public void SetObjeto(Objeto obj)
		{
			if(obj==null)
				throw new ArgumentNullException();
			
			objeto=obj.Index;
			
		}
		public Script Script(int posicionEquipo,RomData rom=null)
		{
			return Script(posicionEquipo,rom!=null?rom.Rom:null,rom!=null?rom.Edicion:null,rom!=null?rom.Compilacion:null);
		}
		public Script Script(int posicionEquipo,RomGba rom,EdicionPokemon edicion,Compilacion compilacion)
		{
			const int VARIABLEPOKEMONEQUIPO=0x8000;
			byte[] aux;
			int auxPos=OFFSETPOKEMONDESENCRIPTADO;
			Script scritpEditorPokemon=new Script();
			
			scritpEditorPokemon.ComandosScript.Add(new ComandosScript.SetVar(VARIABLEPOKEMONEQUIPO,(ushort)posicionEquipo));
			scritpEditorPokemon.ComandosScript.Add(new ComandosScript.CallAsm(PosicionEncryptASMScript(rom,edicion,compilacion)+1));//sumo uno porque las rutinas ASM se les llama así :)
			//pongo los datos //mirar de poner el nombre de los parametros para asi poder identificar cada linea :)
			if(personalidad!=null)
			{
				aux=Serializar.GetBytes(personalidad.Data);
				for(int i=0;i<4;i++)
					scritpEditorPokemon.ComandosScript.Add( new ComandosScript.WriteByteToOffset(auxPos++,aux[i]));
			}
			else auxPos+=4;
			if(idEntrenador!=null)
			{
				aux=Serializar.GetBytes(idEntrenador.Data);
				for(int i=0;i<4;i++)
					scritpEditorPokemon.ComandosScript.Add( new ComandosScript.WriteByteToOffset(auxPos++,aux[i]));
			}else auxPos+=4;
			if(nombrePokemon!=null){
				aux=BloqueString.ToByteArray(nombrePokemon.Texto,false);
				for(int i=0,totalMax=(int)Pokemon.LongitudCampos.NombreCompilado;i<aux.Length&&i<totalMax;i++)
					scritpEditorPokemon.ComandosScript.Add( new ComandosScript.WriteByteToOffset(auxPos++,aux[i]));
			}else auxPos+=10;
			if(idioma!=null)
			{
				aux=idioma.Data;
				for(int i=0;i<aux.Length;i++)
					scritpEditorPokemon.ComandosScript.Add( new ComandosScript.WriteByteToOffset(auxPos++,aux[i]));
			}else auxPos+=2;
			if(marcas.HasValue)
				scritpEditorPokemon.ComandosScript.Add( new ComandosScript.WriteByteToOffset(auxPos++,Marcas.Value));
			else auxPos++;
			if(cheksum!=null)
			{
				aux=idioma.Data;
				for(int i=0;i<2;i++)
					scritpEditorPokemon.ComandosScript.Add( new ComandosScript.WriteByteToOffset(auxPos++,aux[i]));
			}else auxPos+=2;
			if(relleno.HasValue)
				scritpEditorPokemon.ComandosScript.Add( new ComandosScript.WriteByteToOffset(auxPos++,relleno.Value));
			else auxPos++;
			if(especie!=null)
			{
				aux=especie.Data;
				for(int i=0;i<aux.Length;i++)
					scritpEditorPokemon.ComandosScript.Add( new ComandosScript.WriteByteToOffset(auxPos++,aux[i]));

			}else auxPos+=2;
			if(objeto!=null)
			{
				aux=objeto.Data;
				for(int i=0;i<aux.Length;i++)
					scritpEditorPokemon.ComandosScript.Add( new ComandosScript.WriteByteToOffset(auxPos++,aux[i]));

			}else auxPos+=2;
			//poner el resto de valores
			if(experiencia!=null)
			{
				aux=experiencia.Data;
				for(int i=0;i<aux.Length;i++)
					scritpEditorPokemon.ComandosScript.Add( new ComandosScript.WriteByteToOffset(auxPos++,aux[i]));

			}else auxPos+=4;

			if(MasPP.HasValue)
				scritpEditorPokemon.ComandosScript.Add( new ComandosScript.WriteByteToOffset(auxPos++,MasPP.Value));
			else auxPos++;
			if(Amistad.HasValue)
				scritpEditorPokemon.ComandosScript.Add( new ComandosScript.WriteByteToOffset(auxPos++,Amistad.Value));
			else auxPos++;
			if(desconocido!=null)
			{
				aux=desconocido.Data;
				for(int i=0;i<aux.Length;i++)
					scritpEditorPokemon.ComandosScript.Add( new ComandosScript.WriteByteToOffset(auxPos++,aux[i]));

			}
			else auxPos+=2;
			if(ataque1!=null)
			{
				aux=ataque1.Data;
				for(int i=0;i<aux.Length;i++)
					scritpEditorPokemon.ComandosScript.Add( new ComandosScript.WriteByteToOffset(auxPos++,aux[i]));

			}
			else auxPos+=2;
			if(ataque2!=null)
			{
				aux=ataque2.Data;
				for(int i=0;i<aux.Length;i++)
					scritpEditorPokemon.ComandosScript.Add( new ComandosScript.WriteByteToOffset(auxPos++,aux[i]));

			}
			else auxPos+=2;
			if(ataque3!=null)
			{
				aux=ataque3.Data;
				for(int i=0;i<aux.Length;i++)
					scritpEditorPokemon.ComandosScript.Add( new ComandosScript.WriteByteToOffset(auxPos++,aux[i]));

			}
			else auxPos+=2;
			if(ataque4!=null)
			{
				aux=ataque4.Data;
				for(int i=0;i<aux.Length;i++)
					scritpEditorPokemon.ComandosScript.Add( new ComandosScript.WriteByteToOffset(auxPos++,aux[i]));

			}
			else auxPos+=2;
			if(Amistad.HasValue)
				scritpEditorPokemon.ComandosScript.Add( new ComandosScript.WriteByteToOffset(auxPos++,Amistad.Value));
			else auxPos++;
			if(PpAtaque1.HasValue)
				scritpEditorPokemon.ComandosScript.Add( new ComandosScript.WriteByteToOffset(auxPos++,PpAtaque1.Value));
			else auxPos++;
			if(PpAtaque2.HasValue)
				scritpEditorPokemon.ComandosScript.Add( new ComandosScript.WriteByteToOffset(auxPos++,PpAtaque2.Value));
			else auxPos++;
			if(PpAtaque3.HasValue)
				scritpEditorPokemon.ComandosScript.Add( new ComandosScript.WriteByteToOffset(auxPos++,PpAtaque3.Value));
			else auxPos++;
			if(PpAtaque4.HasValue)
				scritpEditorPokemon.ComandosScript.Add( new ComandosScript.WriteByteToOffset(auxPos++,PpAtaque4.Value));
			else auxPos++;
			if(EvSalud.HasValue)
				scritpEditorPokemon.ComandosScript.Add( new ComandosScript.WriteByteToOffset(auxPos++,EvSalud.Value));
			else auxPos++;
			if(EvAtaque.HasValue)//creo que va aqui
				scritpEditorPokemon.ComandosScript.Add( new ComandosScript.WriteByteToOffset(auxPos++,EvAtaque.Value));
			else auxPos++;
			if(EvDefensa.HasValue)
				scritpEditorPokemon.ComandosScript.Add( new ComandosScript.WriteByteToOffset(auxPos++,EvDefensa.Value));
			else auxPos++;
			if(EvVelocidad.HasValue)
				scritpEditorPokemon.ComandosScript.Add( new ComandosScript.WriteByteToOffset(auxPos++,EvVelocidad.Value));
			else auxPos++;
			if(EvAtaqueEspecial.HasValue)
				scritpEditorPokemon.ComandosScript.Add( new ComandosScript.WriteByteToOffset(auxPos++,EvAtaqueEspecial.Value));
			else auxPos++;
			if(EvDefensaEspecial.HasValue)
				scritpEditorPokemon.ComandosScript.Add( new ComandosScript.WriteByteToOffset(auxPos++,EvDefensaEspecial.Value));
			else auxPos++;
			if(Carisma.HasValue)
				scritpEditorPokemon.ComandosScript.Add( new ComandosScript.WriteByteToOffset(auxPos++,Carisma.Value));
			else auxPos++;
			if(Dulzura.HasValue)
				scritpEditorPokemon.ComandosScript.Add( new ComandosScript.WriteByteToOffset(auxPos++,Dulzura.Value));
			else auxPos++;
			if(Ingenio.HasValue)
				scritpEditorPokemon.ComandosScript.Add( new ComandosScript.WriteByteToOffset(auxPos++,Ingenio.Value));
			else auxPos++;
			if(Dureza.HasValue)
				scritpEditorPokemon.ComandosScript.Add( new ComandosScript.WriteByteToOffset(auxPos++,Dureza.Value));
			else auxPos++;
			if(Feel.HasValue)
				scritpEditorPokemon.ComandosScript.Add( new ComandosScript.WriteByteToOffset(auxPos++,Feel.Value));
			else auxPos++;
			if(EstadoPokerus.HasValue)
				scritpEditorPokemon.ComandosScript.Add( new ComandosScript.WriteByteToOffset(auxPos++,EstadoPokerus.Value));
			else auxPos++;
			if(Localizacion.HasValue)
				scritpEditorPokemon.ComandosScript.Add( new ComandosScript.WriteByteToOffset(auxPos++,Localizacion.Value));
			else auxPos++;
			if(origen!=null)
			{
				aux=origen.Data;
				for(int i=0;i<aux.Length;i++)
					scritpEditorPokemon.ComandosScript.Add( new ComandosScript.WriteByteToOffset(auxPos++,aux[i]));

			}
			else auxPos+=2;
			//cuando entienda como van los demás valores los pongo :)
			scritpEditorPokemon.ComandosScript.Add(new ComandosScript.CallAsm(PosicionDecryptASMScript(rom,edicion,compilacion)));

			return scritpEditorPokemon;
		}
		public static bool Compatible(EdicionPokemon edicion,Compilacion compilacion)
		{
			bool compatible=ASMDecrypt.ContainsKey(edicion);
			return compatible;
		}

		public static int PosicionEncryptASMScript(RomGba rom, EdicionPokemon edicion, Compilacion compilacion)
		{
			return Posicion(rom, edicion, compilacion, ASMEncrypt);
		}
		public static int PosicionDecryptASMScript(RomGba rom, EdicionPokemon edicion, Compilacion compilacion)
		{
			return Posicion(rom, edicion, compilacion, ASMDecrypt);
		}

		static int Posicion(RomGba rom, EdicionPokemon edicion, Compilacion compilacion,LlistaOrdenada<EdicionPokemon,ASM> dicASM)
		{
			if(!dicASM.ContainsKey(edicion))
				throw new RomFaltaInvestigacionException();
			return rom.Data.SearchArray(dicASM[edicion].AsmBinary);
		}
		
		public static bool EstaActivado(RomGba rom, EdicionPokemon edicion, Compilacion compilacion)
		{
			return PosicionEncryptASMScript(rom,edicion,compilacion)>-1;
		}
		public static void Activar(RomGba rom, EdicionPokemon edicion, Compilacion compilacion)
		{
			if (!EstaActivado(rom, edicion, compilacion))
			{
				//Pongo el asm encryptar
				rom.Data.SearchEmptySpaceAndSetArray(ASMEncrypt[edicion].AsmBinary);
				//pongo el asm desencryptar
				rom.Data.SearchEmptySpaceAndSetArray(ASMDecrypt[edicion].AsmBinary);
				
			}
		}
		public static void Desactivar(RomGba rom, EdicionPokemon edicion, Compilacion compilacion)
		{
			if (EstaActivado(rom, edicion, compilacion))
			{
				//Quito el asm encryptar
				rom.Data.Remove(PosicionEncryptASMScript(rom, edicion, compilacion), ASMEncrypt[edicion].AsmBinary.Length);
				//Quito el asm desencryptar
				rom.Data.Remove(PosicionDecryptASMScript(rom, edicion, compilacion),ASMDecrypt[edicion].AsmBinary.Length);

			}
		}
	}
}
