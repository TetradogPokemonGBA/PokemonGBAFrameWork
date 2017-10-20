/*
 * Usuario: Pikachu240
 * Licencia GNU GPL V3
 */
using System;

namespace PokemonGBAFrameWork.ComandosScript
{
	/// <summary>
	/// Description of AddItem.
	/// </summary>
	public class AddItem:Comando
	{
		public const byte ID=0x44;
		public const int SIZE=5;
		ushort objetoAAñadir;

		ushort cantidad;

		
		public AddItem(ushort objetoAAñadir,ushort cantidad)
		{
			ObjetoAAñadir=objetoAAñadir;

			Cantidad=cantidad;

			
		}
		
		public AddItem(RomGba rom,int offset):base(rom,offset)
		{
		}
		public AddItem(byte[] bytesScript,int offset):base(bytesScript,offset)
		{}
		public unsafe AddItem(byte* ptRom,int offset):base(ptRom,offset)
		{}
		public override string Descripcion {
			get {
				return "Añade la cantidad del objeto especificado";
			}
		}

		public override byte IdComando {
			get {
				return ID;
			}
		}
		public override string Nombre {
			get {
				return "AddItem";
			}
		}
		public override int Size {
			get {
				return SIZE;
			}
		}
		

		public ushort ObjetoAAñadir
		{
			get{ return objetoAAñadir;}
			set{objetoAAñadir=value;}
		}

		public ushort Cantidad
		{
			get{ return cantidad;}
			set{cantidad=value;}
		}
		protected override System.Collections.Generic.IList<object> GetParams()
		{
			return new Object[]{objetoAAñadir,cantidad};
		}
		protected unsafe override void CargarCamando(byte* ptrRom, int offsetComando)
		{
			objetoAAñadir=Word.GetWord(ptrRom,offsetComando);

			offsetComando+=Word.LENGTH;

			cantidad=Word.GetWord(ptrRom,offsetComando);

			offsetComando+=Word.LENGTH;

			
		}
		protected unsafe override void SetComando(byte* ptrRomPosicionado, params int[] parametrosExtra)
		{
			base.SetComando(ptrRomPosicionado,parametrosExtra);
			Word.SetWord(ptrRomPosicionado,ObjetoAAñadir);

			ptrRomPosicionado+=Word.LENGTH;

			Word.SetWord(ptrRomPosicionado,Cantidad);

			ptrRomPosicionado+=Word.LENGTH;

			
		}
	}
}
