/*
 * Creado por SharpDevelop.
 * Usuario: Pikachu240
 * Fecha: 31/05/2017
 * Hora: 21:57
 * Licencia GNU GPL V3
 * Para cambiar esta plantilla use Herramientas | Opciones | Codificación | Editar Encabezados Estándar
 */
using System;

namespace PokemonGBAFrameWork
{
	/// <summary>
	/// Description of If.
	/// </summary>
	public class If:Comando
	{
		public const byte ID=0x6;
		public const int SIZE=6;
		
		byte condicion;
		Script script;
		public If(RomGba rom,int offset):base(rom,offset)
		{}
		public If(byte[] bytesScript,int offset):base(bytesScript,offset)
		{}
		public unsafe If(byte* ptRom,int offset):base(ptRom,offset)
		{}

		public byte Condicion {
			get {
				return condicion;
			}
			set {
				condicion = value;
			}
		}

		public Script Script {
			get {
				return script;
			}
			set {
				if(value==null)
					value=new Script();
				script = value;
			}
		}

		#region implemented abstract members of Comando
		
		protected override void CargarCamando(byte* ptrRom, int offsetComando)
		{
			throw new NotImplementedException();
		}

		protected override void SetComando(byte* ptrRomPosicionado, params int[] parametrosExtra)
		{
			throw new NotImplementedException();
		}

		public override string Descripcion {
			get {
				throw new NotImplementedException();
			}
		}

		public override byte IdComando {
			get {
				return ID;
			}
		}

		public override string Nombre {
			get {
				return "If1";
			}
		}

		public override int Size {
			get {
				return SIZE;
			}
		}

		#endregion
	}
}
