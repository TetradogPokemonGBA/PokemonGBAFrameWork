/*
 * Creado por SharpDevelop.
 * Usuario: Pikachu240
 * Fecha: 02/06/2017
 * Hora: 7:39
 * Licencia GNU GPL V3
 * Para cambiar esta plantilla use Herramientas | Opciones | Codificación | Editar Encabezados Estándar
 */
using System;

namespace PokemonGBAFrameWork
{
	/// <summary>
	/// Description of CompareBanks.
	/// </summary>
	public class CompareBanks:Comando
	{
		public const byte ID=0x1B;
		public const int SIZE=5;
		
		short bank1;
		short bank2;
		public CompareBanks(RomGba rom,int offset):base(rom,offset)
		{
		}
		public CompareBanks(byte[] bytesScript,int offset):base(bytesScript,offset)
		{}
		public unsafe CompareBanks(byte* ptRom,int offset):base(ptRom,offset)
		{}
		public override string Descripcion {
			get {
				return "Compara dos banks";
			}
		}

		public override byte IdComando {
			get {
				return ID;
			}
		}

		public override string Nombre {
			get {
				return "CompareBanks";
			}
		}

		public override int Size {
			get {
				return SIZE;
			}
		}

		public short Bank1 {
			get {
				return bank1;
			}
			set {
				bank1 = value;
			}
		}

		public short Bank2 {
			get {
				return bank2;
			}
			set {
				bank2 = value;
			}
		}
		protected unsafe override void CargarCamando(byte* ptrRom, int offsetComando)
		{
			bank1=Word.GetWord(ptrRom,offsetComando);
			bank2=Word.GetWord(ptrRom,offsetComando+Word.LENGTH);
		}
		protected unsafe override void SetComando(byte* ptrRomPosicionado, params int[] parametrosExtra)
		{
			base.SetComando(ptrRomPosicionado, parametrosExtra);
			ptrRomPosicionado++;
			Word.SetWord(ptrRomPosicionado,bank1);
			Word.SetWord(ptrRomPosicionado+Word.LENGTH,bank2);
		}
	}
}
