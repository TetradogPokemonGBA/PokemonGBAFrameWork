/*
 * Creado por SharpDevelop.
 * Usuario: Pikachu240
 * Fecha: 01/06/2017
 * Hora: 1:49
 * Licencia GNU GPL V3
 * Para cambiar esta plantilla use Herramientas | Opciones | Codificación | Editar Encabezados Estándar
 */
using System;
using System.Collections.Generic;
using Gabriel.Cat.Extension;
namespace PokemonGBAFrameWork.ComandosScript
{
	public class Gotostdif : Gotostd
	{
		public const byte ID = 0xA;

		public const int SIZE = 3;

		byte condicion;

		public Gotostdif(byte funcion,byte condicion):base(funcion)
		{Condicion=condicion;}
		public Gotostdif(RomGba rom, int offset) : base(rom, offset)
		{
		}

		public Gotostdif(byte[] bytesScript, int offset) : base(bytesScript, offset)
		{
		}

		public unsafe Gotostdif(byte* ptRom, int offset) : base(ptRom, offset)
		{
		}

		public override string Nombre {
			get {
				return "Gotostdif";
			}
		}

		public override string Descripcion {
			get {
				return base.Descripcion + " si se cumple la condición";
			}
		}

		public override byte IdComando {
			get {
				return ID;
			}
		}

		public byte Condicion {
			get {
				return condicion;
			}
			set {
				condicion = value;
			}
		}

		public override int Size {
			get {
				return SIZE;
			}
		}
		protected override IList<object> GetParams()
		{
			return base.GetParams().AfegirValor(Condicion);
		}
		protected unsafe override void CargarCamando(byte* ptrRom, int offsetComando)
		{
			base.CargarCamando(ptrRom, offsetComando);
			Condicion = ptrRom[++offsetComando];
		}

		protected unsafe override void SetComando(byte* ptrRomPosicionado, params int[] parametrosExtra)
		{
			base.SetComando(ptrRomPosicionado, parametrosExtra);
			*ptrRomPosicionado = Condicion;
			ptrRomPosicionado++;
		}
	}
}


