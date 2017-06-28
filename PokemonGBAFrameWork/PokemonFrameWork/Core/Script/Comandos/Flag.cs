/*
 * Creado por SharpDevelop.
 * Usuario: Pikachu240
 * Fecha: 02/06/2017
 * Hora: 13:03
 * Licencia GNU GPL V3
 * Para cambiar esta plantilla use Herramientas | Opciones | Codificación | Editar Encabezados Estándar
 */
using System;

namespace PokemonGBAFrameWork.Script
{
	/// <summary>
	/// Description of Flag.
	/// </summary>
	public class SetFlag:Comando
	{
		public const byte ID=0x29;
		public const int SIZE=1+Word.LENGTH;
		
		short flag;
		public SetFlag(short flag)
		{
			Flag=flag;
		}
		public SetFlag(RomGba rom,int offset):base(rom,offset)
		{
		}
		public SetFlag(byte[] bytesScript,int offset):base(bytesScript,offset)
		{}
		public unsafe SetFlag(byte* ptRom,int offset):base(ptRom,offset)
		{}
		public override string Descripcion {
			get {
				return "Activa el flag";
			}
		}

		public override byte IdComando {
			get {
				return ID;
			}
		}

		public override string Nombre {
			get {
				return "SetFlag";
			}
		}

		public override int Size {
			get {
				return SIZE;
			}
		}
		
		
		public short Flag {
			get {
				return flag;
			}
			set {
				flag = value;
			}
		}
		protected override System.Collections.Generic.IList<object> GetParams()
		{
			return new Object[]{Flag};
		}
		protected unsafe override void CargarCamando(byte* ptrRom, int offsetComando)
		{
			flag=Word.GetWord(ptrRom,offsetComando);
		}
		protected unsafe override void SetComando(byte* ptrRomPosicionado, params int[] parametrosExtra)
		{
			base.SetComando(ptrRomPosicionado, parametrosExtra);
			ptrRomPosicionado++;
			Word.SetWord(ptrRomPosicionado,flag);
		}
	}
	public class ClearFlag:SetFlag
	{
		public const byte ID=0x2A;

		public ClearFlag(RomGba rom,int offset):base(rom,offset)
		{
		}
		public ClearFlag(byte[] bytesScript,int offset):base(bytesScript,offset)
		{}
		public unsafe ClearFlag(byte* ptRom,int offset):base(ptRom,offset)
		{}
		public override string Descripcion {
			get {
				return "Desactiva el flag";
			}
		}

		public override byte IdComando {
			get {
				return ID;
			}
		}
		public override string Nombre {
			get {
				return "ClearFlag";
			}
		}
	}
	
}
