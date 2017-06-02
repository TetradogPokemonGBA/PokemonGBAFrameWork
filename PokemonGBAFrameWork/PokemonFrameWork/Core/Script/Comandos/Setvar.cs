/*
 * Creado por SharpDevelop.
 * Usuario: Pikachu240
 * Fecha: 01/06/2017
 * Hora: 3:12
 * Licencia GNU GPL V3
 * Para cambiar esta plantilla use Herramientas | Opciones | Codificación | Editar Encabezados Estándar
 */
using System;

namespace PokemonGBAFrameWork
{
	/// <summary>
	/// Description of Setvar.
	/// </summary>
	public class SetVar:Comando
	{
		public const byte ID=0x16;
		public const int SIZE=0x5;
		
		short variable;
		short valor;
		
		public SetVar(RomGba rom,int offset):base(rom,offset)
		{}
		public SetVar(byte[] bytesScript,int offset):base(bytesScript,offset)
		{}
		public unsafe SetVar(byte* ptRom,int offset):base(ptRom,offset)
		{}
		
		#region implemented abstract members of Comando
		public override string Descripcion {
			get {
				return "Asigna a la variable el valor especificado";
			}
		}
		public override byte IdComando {
			get {
				return ID;
			}
		}
		public override string Nombre {
			get {
				return "Setvar";
			}
		}
		public override int Size {
			get {
				return SIZE;
			}
		}

		public short Variable {
			get {
				return variable;
			}
			set {
				variable = value;
			}
		}

		public short Valor {
			get {
				return valor;
			}
			set {
				valor = value;
			}
		}
		#endregion

		protected unsafe override void CargarCamando(byte* ptrRom, int offsetComando)
		{
			variable=Word.GetWord(ptrRom,offsetComando);
			valor=Word.GetWord(ptrRom,offsetComando+Word.LENGTH);
		}
		protected unsafe override void SetComando(byte* ptrRomPosicionado, params int[] parametrosExtra)
		{
		
			base.SetComando(ptrRomPosicionado, parametrosExtra);
			ptrRomPosicionado++;
			Word.SetWord(ptrRomPosicionado,variable);
			ptrRomPosicionado+=Word.LENGTH;
			
			Word.SetWord(ptrRomPosicionado,valor);
			ptrRomPosicionado+=Word.LENGTH;
			
		}
	}
	
		public class SubVar:SetVar
	{
		public const byte ID=0x18;
		
		public SubVar(RomGba rom,int offset):base(rom,offset)
		{}
		public SubVar(byte[] bytesScript,int offset):base(bytesScript,offset)
		{}
		public unsafe SubVar(byte* ptRom,int offset):base(ptRom,offset)
		{}
		
		#region implemented abstract members of Comando
		public override string Descripcion {
			get {
				return "Resta cualquier valor a la variable";
			}
		}
		public override byte IdComando {
			get {
				return ID;
			}
		}
		public override string Nombre {
			get {
				return "SubVar";
			}
		}
		#endregion
	}
}
