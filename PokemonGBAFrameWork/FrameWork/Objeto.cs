/*
 * Creado por SharpDevelop.
 * Usuario: tetradog
 * Fecha: 29/08/2016
 * Hora: 23:26
 * 
 * Para cambiar esta plantilla use Herramientas | Opciones | Codificación | Editar Encabezados Estándar
 */
using System;
using Gabriel.Cat;
using System.Collections.Generic;
using System.Linq;

namespace PokemonGBAFrameWork
{
	/// <summary>
	/// Description of Objeto.
	/// </summary>
	public class Objeto
	{
		public enum Variables
		{
			DatosObjeto,ImagenYPaletaObjeto
		}
		public enum LongitudCampos
		{
			Total=44,
            Nombre=25,
            //explicacion de los 44 bytes 
			NombreCompilado=13,
            ByteDesconocido=1,
            Posicion=2,//esta en la posicion F
            BytesDesconocidosAntesDescripcion = 4,
            Descripcion=4,//es un pointer a una zona que acaba en FF
            BytesDesconocidosDespuesDescripcion = 4,
            PointerDatosDesconocidos=4,
            BytesDesconocidosDespuesDelPointerDesconocido=4,
            PointerDatosDesconocidos2=4,
            BytesDesconocidosDespuesDelPointerDesconocido2 = 4,
        }
		BloqueString nombre;//acaba en FF y como maximo son 13 bytes
        byte byteNoInterpretado;//no se que hace...//1 byte
        short posicion;//una posicion...no es unica porque los ????? tienen todos la posicion 0//2 bytes que son la poscion
        byte[] bytesNoInterpretadosAntesDescripcion;//son 4 bytes
        BloqueString descripcion;//es un puntero a la descripcion que son 4 bytes
        byte[] bytesNoInterpretadosDespuesDescripcion;//4 bytes+1 pointer+4 bytes+pointer+4bytes desconocidos
        //no esta en los datos del objeto es a parte
        BloqueImagen imagenObjeto;//no esta en rubi y zafiro(sera null cuando lo lea) y el formato es 8bytes->PointerImg,PointerPaleta

        /*por saber como va*/
		//efecto
		//uso
		//etc
		static Objeto()
		{
			Zona zonaObjeto = new Zona(Variables.DatosObjeto);
            Zona zonaImagenesObjetos = new Zona(Variables.ImagenYPaletaObjeto);
			
			//datos item
			zonaObjeto.AddOrReplaceZonaOffset(Edicion.ZafiroEsp, 0xA9B3C);
			zonaObjeto.AddOrReplaceZonaOffset(Edicion.RubiEsp, 0xA9B3C);
			zonaObjeto.AddOrReplaceZonaOffset(Edicion.RojoFuegoEsp, 0x1C8);
			zonaObjeto.AddOrReplaceZonaOffset(Edicion.VerdeHojaEsp, 0x1C8);
			zonaObjeto.AddOrReplaceZonaOffset(Edicion.EsmeraldaEsp, 0x1C8);
			
			zonaObjeto.AddOrReplaceZonaOffset(Edicion.ZafiroUsa, 0xA98F0, 0xA9910, 0xA9910);
			zonaObjeto.AddOrReplaceZonaOffset(Edicion.RubiUsa, 0xA98F0, 0xA9910, 0xA9910);
			zonaObjeto.AddOrReplaceZonaOffset(Edicion.EsmeraldaUsa, 0x1C8);
			zonaObjeto.AddOrReplaceZonaOffset(Edicion.RojoFuegoUsa, 0x1C8);
			zonaObjeto.AddOrReplaceZonaOffset(Edicion.VerdeHojaUsa, 0x1C8);
            //datos imagen y paleta
            zonaImagenesObjetos.AddOrReplaceZonaOffset(Edicion.RojoFuegoUsa, 0x9899C, 0x989B0);
            zonaImagenesObjetos.AddOrReplaceZonaOffset(Edicion.VerdeHojaUsa, 0x98970, 0x98984);
            zonaImagenesObjetos.AddOrReplaceZonaOffset(Edicion.EsmeraldaUsa, 0x1294bc);

            zonaImagenesObjetos.AddOrReplaceZonaOffset(Edicion.EsmeraldaUsa, 0x1290d4);
            zonaImagenesObjetos.AddOrReplaceZonaOffset(Edicion.RojoFuegoEsp, 0x98B74);
            zonaImagenesObjetos.AddOrReplaceZonaOffset(Edicion.VerdeHojaEsp, 0x98b48);
            //añado las zonas al diccionario
            Zona.DiccionarioOffsetsZonas.Añadir(zonaObjeto);
            Zona.DiccionarioOffsetsZonas.Añadir(zonaImagenesObjetos);
		}
        public Objeto(BloqueString nombre) : this(nombre, null) { }

        public Objeto(BloqueString nombre,byte[] bytesNoInterpretados)
		{
			this.nombre = nombre;
			nombre.MaxCaracteres=(int)LongitudCampos.Nombre;
           // this.bytesNoInterpretados = new byte[(int)LongitudCampos.BytesNoInterpretados];
            //BytesNoInterpretados = bytesNoInterpretados;
		}

		public BloqueString Nombre {
			get {
				return nombre;
			}
			private set {
				nombre = value;
			}
		}


        public override string ToString()
        {
            return Nombre;
        }
        public static int TotalObjetos(RomPokemon rom)
		{
			return TotalObjetos(rom, Edicion.GetEdicion(rom));
		}
		public static int TotalObjetos(RomPokemon rom, Edicion edicion)
		{
			//en  un futuro sacarlos de la rom!!
			int totalItems = 0;
			switch (edicion.Abreviacion) {
				case Edicion.ABREVIACIONESMERALDA:
					totalItems = 0x179;
					break;
				case Edicion.ABREVIACIONRUBI:
				case Edicion.ABREVIACIONZAFIRO:
					totalItems = 0x15D;
					break;
				case Edicion.ABREVIACIONROJOFUEGO:
				case Edicion.ABREVIACIONVERDEHOJA:
					totalItems = 0x177;
					break;
			} 
			return totalItems;
		}
		//de momento solo se cargar el nombre
		public static Objeto GetObjeto(RomPokemon rom,Edicion edicion,CompilacionRom.Compilacion compilacion,Hex index)
        {
            Hex offsetObjeto=Zona.GetOffset(rom,Variables.DatosObjeto,edicion,compilacion)+index*(int)LongitudCampos.Total;
            BloqueString bloqueNombre = BloqueString.GetString(rom, offsetObjeto);
            return new Objeto(bloqueNombre);
		}
		//de momento solo guarda el nombre
		public static void SetObjeto(RomPokemon rom,Edicion edicion,CompilacionRom.Compilacion compilacion,Objeto objeto, Hex index)
		{
			objeto.Nombre.Texto=objeto.Nombre.Texto;
            if (objeto.Nombre.Texto.Length > (int)LongitudCampos.NombreCompilado)
                objeto.Nombre.Texto = objeto.Nombre.Texto.Substring(0, (int)LongitudCampos.NombreCompilado);
			objeto.Nombre.OffsetInicio=Zona.GetOffset(rom,Variables.DatosObjeto,edicion,compilacion)+index*(int)LongitudCampos.Total;
			BloqueString.SetString(rom,objeto.Nombre.OffsetInicio,objeto.Nombre.Texto.Length+1== (int)LongitudCampos.NombreCompilado? objeto.Nombre.Texto: objeto.Nombre.Texto);
          //  BloqueBytes.SetBytes(rom, objeto.Nombre.OffsetFin + 1, objeto.BytesNoInterpretados);
		}

        public static void SetObjetos(RomPokemon rom, IEnumerable<Objeto> objetos)
        {
            if (rom == null || objetos == null) throw new ArgumentNullException();
            Objeto[] objetosArray = objetos.ToArray();
            Edicion edicion = Edicion.GetEdicion(rom);
            CompilacionRom.Compilacion compilacion = CompilacionRom.GetCompilacion(rom, edicion);
            if (objetosArray.Length != TotalObjetos(rom, edicion))
            {
                BloqueBytes.RemoveBytes(rom, Zona.GetOffset(rom, Variables.DatosObjeto, edicion, compilacion), TotalObjetos(rom, edicion) * (int)LongitudCampos.Total);
                Zona.SetOffset(rom, Variables.DatosObjeto, edicion, compilacion, BloqueBytes.SearchEmptyBytes(rom, objetosArray.Length * (int)LongitudCampos.Total));//actualizo el offset
            }
            for (int i = 0; i < objetosArray.Length; i++)
                SetObjeto(rom, edicion, compilacion, objetosArray[i], i);
        }

        public static Objeto[] GetObjetos(RomPokemon rom)
        {
            return GetObjetos(rom, Edicion.GetEdicion(rom));
        }
        public static Objeto[] GetObjetos(RomPokemon rom, Edicion edicion)
        {
            return GetObjetos(rom, edicion, CompilacionRom.GetCompilacion(rom, edicion));
        }
        public static Objeto[] GetObjetos(RomPokemon rom, CompilacionRom.Compilacion compilacion)
        {
            return GetObjetos(rom, Edicion.GetEdicion(rom), compilacion);
        }
        public static Objeto[] GetObjetos(RomPokemon rom,Edicion edicion,CompilacionRom.Compilacion compilacion)
        {
            Objeto[] objetos = new Objeto[TotalObjetos(rom, edicion)];
            for (int i = 0; i < objetos.Length; i++)
                objetos[i] = GetObjeto(rom, edicion, compilacion, i);
            return objetos;
        }
    }
}
