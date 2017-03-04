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
        ushort posicion;//una posicion...no es unica porque los ????? tienen todos la posicion 0//2 bytes que son la poscion
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
            zonaImagenesObjetos.AddOrReplaceZonaOffset(Edicion.EsmeraldaUsa, 0x1B0034);
          //  zonaImagenesObjetos.AddOrReplaceZonaOffset(Edicion.EsmeraldaUsa, 0x1294bc);//objetos base secreta

            zonaImagenesObjetos.AddOrReplaceZonaOffset(Edicion.EsmeraldaEsp, 0x1AFC54);
            //zonaImagenesObjetos.AddOrReplaceZonaOffset(Edicion.EsmeraldaEsp, 0x1290d4);//objetos base secreta
            zonaImagenesObjetos.AddOrReplaceZonaOffset(Edicion.RojoFuegoEsp, 0x98B74);
            zonaImagenesObjetos.AddOrReplaceZonaOffset(Edicion.VerdeHojaEsp, 0x98b48);
            //añado las zonas al diccionario
            Zona.DiccionarioOffsetsZonas.Add(zonaObjeto);
            Zona.DiccionarioOffsetsZonas.Add(zonaImagenesObjetos);
		}
        public Objeto(ushort posicion,BloqueString nombre,BloqueString descripcion,BloqueImagen icono) : this(nombre,0x0,posicion,null,descripcion,null,icono) { }

        public Objeto( BloqueString nombre,byte byteDesconocido,ushort posicion,byte[] bytesAntesDeDescripcion ,BloqueString descripcion,byte[] bytesDespuesDeDescripcion, BloqueImagen icono)
		{
            this.byteNoInterpretado = byteDesconocido;
			this.nombre = nombre;
			nombre.MaxCaracteres=(int)LongitudCampos.Nombre;
            this.posicion = posicion;
            this.descripcion = descripcion;
            this.imagenObjeto = icono;
            this.bytesNoInterpretadosAntesDescripcion = new byte[(int)LongitudCampos.BytesDesconocidosAntesDescripcion];
            this.bytesNoInterpretadosDespuesDescripcion = new byte[
                                                                    (int)LongitudCampos.BytesDesconocidosDespuesDescripcion+
                                                                    (int)LongitudCampos.PointerDatosDesconocidos+
                                                                    (int)LongitudCampos.BytesDesconocidosDespuesDelPointerDesconocido +
                                                                    (int)LongitudCampos.PointerDatosDesconocidos2+
                                                                    (int)LongitudCampos.BytesDesconocidosDespuesDelPointerDesconocido2 
                                                                    ];
            if (bytesAntesDeDescripcion != null)
            {
                for (int i = 0; i < this.bytesNoInterpretadosAntesDescripcion.Length && i < bytesAntesDeDescripcion.Length; i++)
                    this.bytesNoInterpretadosAntesDescripcion[i] = bytesAntesDeDescripcion[i];

            }
            if (bytesDespuesDeDescripcion != null)
            {
                for (int i = 0; i < this.bytesNoInterpretadosDespuesDescripcion.Length && i < bytesDespuesDeDescripcion.Length; i++)
                    this.bytesNoInterpretadosDespuesDescripcion[i] = bytesDespuesDeDescripcion[i];
               
            }
		}

		public BloqueString Nombre {
			get {
				return nombre;
			}
			private set {
				nombre = value;
			}
		}

        public BloqueImagen ImagenObjeto
        {
            get
            {
                return imagenObjeto;
            }

            set
            {
                imagenObjeto = value;
            }
        }

        public override string ToString()
        {
            return Nombre;
        }
        public static int TotalObjetos(RomGBA rom)
		{
            Edicion edicion = Edicion.GetEdicion(rom);
            return TotalObjetos(rom, edicion,CompilacionRom.GetCompilacion(rom,edicion));
		}
		public static int TotalObjetos(RomGBA rom, Edicion edicion,CompilacionRom.Compilacion compilacion)
		{
            Hex posicionDesripcionObjeto = (int)LongitudCampos.NombreCompilado +(int)LongitudCampos.ByteDesconocido +(int)LongitudCampos.Posicion +(int)LongitudCampos.BytesDesconocidosAntesDescripcion;
            const byte MARCAFINNOMBRE = 0xFF,EMPTYBYTENAME=0x0;
			int totalItems = 0;
            Hex offsetInicio = Zona.GetOffset(rom, Variables.DatosObjeto, edicion, compilacion);
            Hex offsetActual = offsetInicio;
            BloqueBytes datosItem;
            //cada objeto como minimo tiene un pointer si no lo tiene es que no tiene el formato bien :) ademas el nombre si no llega al final acaba en FF :D
            bool acabado = false;
            bool nombreComprobadoCorrectamente;
            do
            {
                //mirar de actualizarlo para validar los pointers en otro lado...
                datosItem = BloqueBytes.GetBytes(rom, offsetActual, (int)LongitudCampos.Total);
                //miro que el nombre acaba bien :)
                nombreComprobadoCorrectamente = false;
                acabado = Offset.IsAPointer(rom, offsetActual);//si lo que leo no es un pointer continuo
                for (int i = 0; i < (int)LongitudCampos.NombreCompilado && !acabado; i++)
                {
                    if (datosItem.Bytes[i] == MARCAFINNOMBRE)
                    {
                        if (!nombreComprobadoCorrectamente) nombreComprobadoCorrectamente = true;

                    }
                    
                    else if (nombreComprobadoCorrectamente&& datosItem.Bytes[i] !=EMPTYBYTENAME)
                        acabado = true;//si continua es que esta mal :D
                }
                //miro el pointer
                if (!acabado)
                {
                    if (Offset.GetOffset(datosItem.Bytes, posicionDesripcionObjeto) != -1)
                    {
                        totalItems++;//lo ha leido bien :D
                        offsetActual += (int)LongitudCampos.Total;
                    }
                    else acabado = true;
                }
            } while (!acabado);
            return totalItems;
		}
		public static Objeto GetObjeto(RomGBA rom,Edicion edicion,CompilacionRom.Compilacion compilacion,Hex index)
        {
            Hex offsetObjeto=Zona.GetOffset(rom,Variables.DatosObjeto,edicion,compilacion)+index*(int)LongitudCampos.Total;
            Hex offsetByteDesconocido = offsetObjeto + (int)LongitudCampos.NombreCompilado;
            Hex offsetPosicion = offsetByteDesconocido + (int)LongitudCampos.ByteDesconocido;
            Hex offsetDescripcion = offsetPosicion + (int)LongitudCampos.BytesDesconocidosAntesDescripcion;
            Hex offsetPointersImg;
            BloqueString bloqueNombre = BloqueString.GetString(rom, offsetObjeto,(int)LongitudCampos.NombreCompilado,true);
            BloqueString bloqueDescripcion = BloqueString.GetString(rom, offsetDescripcion);
            ushort posicion = Serializar.ToUShort(BloqueBytes.GetBytes(rom, offsetPosicion, 2).Bytes);
            BloqueImagen bloqueImg=null;
            byte[] bytesDesconocidosAntes=BloqueBytes.GetBytes(rom,offsetDescripcion- (int)LongitudCampos.BytesDesconocidosAntesDescripcion, (int)LongitudCampos.BytesDesconocidosAntesDescripcion).Bytes, bytesDesconocidosDespues= BloqueBytes.GetBytes(rom, offsetDescripcion + (int)LongitudCampos.BytesDesconocidosAntesDescripcion, (int)LongitudCampos.BytesDesconocidosAntesDescripcion).Bytes;
            bloqueNombre.MaxCaracteres = (int)LongitudCampos.Nombre;
            if (edicion.AbreviacionRom != Edicion.ABREVIACIONRUBI && edicion.AbreviacionRom != Edicion.ABREVIACIONZAFIRO) {
                offsetPointersImg = Zona.GetOffset(rom, Variables.ImagenYPaletaObjeto, edicion, compilacion);
                offsetPointersImg += (index * (int)Longitud.Offset * 2);
                //ahora hay pointers y tengo que obtener los offsets
                try
                {
                    
                    bloqueImg = BloqueImagen.GetBloqueImagen(rom, offsetPointersImg,offsetPointersImg+(int)Longitud.Offset);

                }
                catch {
               //si origina una excepcion es que usa la imagen 0 :D
                }
             }


            return new Objeto(bloqueNombre,rom.Datos[offsetByteDesconocido], posicion,bytesDesconocidosAntes,bloqueDescripcion,bytesDesconocidosDespues,bloqueImg);
		}
		public static void SetObjeto(RomGBA rom,Edicion edicion,CompilacionRom.Compilacion compilacion,Objeto objeto, Hex index)
		{

            Hex offsetObjeto = Zona.GetOffset(rom, Variables.DatosObjeto, edicion, compilacion) + index * (int)LongitudCampos.Total;
            Hex offsetByteDesconocido = offsetObjeto + (int)LongitudCampos.NombreCompilado;
            Hex offsetPosicion = offsetByteDesconocido + (int)LongitudCampos.ByteDesconocido;
            Hex offsetDescripcion = offsetPosicion + (int)LongitudCampos.BytesDesconocidosAntesDescripcion;
            Hex offsetImg = Zona.GetOffset(rom, Variables.ImagenYPaletaObjeto, edicion, compilacion) + (index * (int)Longitud.Offset * 2);
            BloqueString.SetString(rom, offsetObjeto,objeto.Nombre.Texto);
            BloqueString.SetString(rom, offsetDescripcion,objeto.descripcion.Texto);
            //pongo la posicion
            BloqueBytes.SetBytes(rom, offsetPosicion, Serializar.GetBytes(objeto.posicion));
            //pongo los bytes que no se interpretar
            rom.Datos[offsetByteDesconocido] = objeto.byteNoInterpretado;
            BloqueBytes.SetBytes(rom, offsetDescripcion - (int)LongitudCampos.BytesDesconocidosAntesDescripcion, objeto.bytesNoInterpretadosAntesDescripcion);
            BloqueBytes.SetBytes(rom, offsetDescripcion + (int)LongitudCampos.BytesDesconocidosAntesDescripcion,objeto.bytesNoInterpretadosDespuesDescripcion);
            if (objeto.imagenObjeto != null && edicion.AbreviacionRom != Edicion.ABREVIACIONRUBI && edicion.AbreviacionRom != Edicion.ABREVIACIONZAFIRO)
            {
                //pongo la imagen y la paleta :D
                BloqueImagen.SetBloqueImagen(rom, offsetImg, objeto.imagenObjeto.DatosDescomprimidos);
                Paleta.SetPaleta(rom, new Paleta(offsetImg + (int)Longitud.Offset, objeto.imagenObjeto.Paletas[0]));
            }
         



        }

        public static void SetObjetos(RomGBA rom, IList<Objeto> objetos)
        {
            if (rom == null || objetos == null) throw new ArgumentNullException();
     
            Edicion edicion = Edicion.GetEdicion(rom);
            CompilacionRom.Compilacion compilacion = CompilacionRom.GetCompilacion(rom, edicion);
            int totalObjetosRom = TotalObjetos(rom, edicion, compilacion);
            if (objetos.Count != totalObjetosRom)
            {
                BloqueBytes.RemoveBytes(rom, Zona.GetOffset(rom, Variables.DatosObjeto, edicion, compilacion), totalObjetosRom * (int)LongitudCampos.Total);
                Zona.SetOffset(rom, Variables.DatosObjeto, edicion, compilacion, BloqueBytes.SearchEmptyBytes(rom, objetos.Count * (int)LongitudCampos.Total));//actualizo el offset
            }
            for (int i = 0; i < objetos.Count; i++)
                SetObjeto(rom, edicion, compilacion, objetos[i], i);
        }
        #region SobreCarga Get
        public static Objeto[] GetObjetos(RomGBA rom)
        {
            return GetObjetos(rom, Edicion.GetEdicion(rom));
        }
        public static Objeto[] GetObjetos(RomGBA rom, Edicion edicion)
        {
            return GetObjetos(rom, edicion, CompilacionRom.GetCompilacion(rom, edicion));
        }
        public static Objeto[] GetObjetos(RomGBA rom, CompilacionRom.Compilacion compilacion)
        {
            return GetObjetos(rom, Edicion.GetEdicion(rom), compilacion);
        }
        #endregion
        public static Objeto[] GetObjetos(RomGBA rom,Edicion edicion,CompilacionRom.Compilacion compilacion)
        {
            Objeto[] objetos = new Objeto[TotalObjetos(rom, edicion,compilacion)];
            for (int i = 0; i < objetos.Length; i++)
                objetos[i] = GetObjeto(rom, edicion, compilacion, i);
            for (int i = 0; i < objetos.Length; i++)
                if (objetos[i].imagenObjeto == null)
                    objetos[i].imagenObjeto = objetos[0].imagenObjeto;
            return objetos;
        }
    }
}
