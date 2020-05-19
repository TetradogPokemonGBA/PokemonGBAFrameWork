/*
 * Creado por SharpDevelop.
 * Usuario: Pikachu240
 * Fecha: 01/06/2017
 * Hora: 1:49
 * Licencia GNU GPL V3
 * Para cambiar esta plantilla use Herramientas | Opciones | Codificación | Editar Encabezados Estándar
 */
using Gabriel.Cat.S.Extension;
using System;
using System.Collections.Generic;
namespace PokemonGBAFramework.Core.ComandosScript
{
	public class Gotostdif : Gotostd
	{
		public new const byte ID = 0xA;

		public new const int SIZE = Gotostd.SIZE+1;
        public new const string NOMBRE= "Gotostdif";
        public new const string DESCRIPCION= Gotostd.DESCRIPCION + " si se cumple la condición";

        public Gotostdif(byte funcion,byte condicion):base(funcion)
		{Condicion=condicion;}
		public Gotostdif(ScriptManager scriptManager,RomGba rom, int offset)  : base(scriptManager,rom, offset)
		{
		}

		public Gotostdif(ScriptManager scriptManager,byte[] bytesScript, int offset) : base(scriptManager,bytesScript, offset)
		{
		}

		public unsafe Gotostdif(ScriptManager scriptManager,byte* ptRom, int offset) : base(scriptManager,ptRom, offset)
		{
		}

		public override string Nombre {
			get {
                return NOMBRE;
			}
		}

		public override string Descripcion {
			get {
                return DESCRIPCION;
			}
		}

		public override byte IdComando {
			get {
				return ID;
			}
		}

        public byte Condicion { get; set; }

        public override int Size {
			get {
				return SIZE;
			}
		}
		protected override IList<object> GetParams()
		{
			return base.GetParams().AfegirValor(Condicion);
		}
		protected unsafe override void CargarCamando(ScriptManager scriptManager,byte* ptrRom, int offsetComando)
		{
			base.CargarCamando(ptrRom, offsetComando);
            offsetComando += base.ParamsSize;
			Condicion = ptrRom[offsetComando];
		}

		public override byte[] GetBytesTemp()
		{
			byte[] data=new byte[Size];
			ptrRomPosicionado+=Gotostd.SIZE;
			*ptrRomPosicionado = Condicion;
		}
	}
}


