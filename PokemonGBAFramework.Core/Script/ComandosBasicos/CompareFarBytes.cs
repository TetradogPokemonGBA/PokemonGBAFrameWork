/*
 * Creado por SharpDevelop.
 * Usuario: Pikachu240
 * Fecha: 02/06/2017
 * Hora: 8:03
 * Licencia GNU GPL V3
 * Para cambiar esta plantilla use Herramientas | Opciones | Codificación | Editar Encabezados Estándar
 */
using System;

namespace PokemonGBAFramework.Core.ComandosScript
{
	/// <summary>
	/// Description of CompareFarBytes.
	/// </summary>
	public class CompareFarBytes:Comando
	{
		public const int ID=0x20;
		public new const int SIZE=Comando.SIZE+OffsetRom.LENGTH+OffsetRom.LENGTH;
        public const string NOMBRE = "CompareFarBytes";
        public const string DESCRIPCION= "Compara los bytes aljados en los offsets";
        OffsetRom offsetA;
		OffsetRom offsetB;
		
		public CompareFarBytes() { }
		public CompareFarBytes(int offsetA,int offsetB):this(new OffsetRom(offsetA),new OffsetRom(offsetB))
		{}
		public CompareFarBytes(OffsetRom offsetA,OffsetRom offsetB)
		{
			OffsetA=offsetA;
			OffsetB=offsetB;
		}
		public CompareFarBytes(ScriptAndASMManager scriptManager,RomGba rom,int offset):base(scriptManager,rom,offset)
		{}
		public CompareFarBytes(ScriptAndASMManager scriptManager,byte[] bytesScript,int offset):base(scriptManager,bytesScript,offset)
		{}
		public unsafe CompareFarBytes(ScriptAndASMManager scriptManager,byte* ptRom,int offset):base(scriptManager,ptRom,offset)
		{}
		public override string Nombre {
			get {
				return NOMBRE;
			}
		}
		public override byte IdComando {
			get {
				return ID;
			}
		}
		public override string Descripcion {
			get {
                return DESCRIPCION;
			}
		}
		public override int Size {
			get {
				return SIZE;
			}
		}

		public OffsetRom OffsetA {
			get {
				return offsetA;
			}
			set
            {
                if (value == null)
                    value = new OffsetRom();
                offsetA = value;
			}
		}

		public OffsetRom OffsetB {
			get {
				return offsetB;
			}
			set
            {
                if (value == null)
                    value = new OffsetRom();
                offsetB = value;
			}
		}
		public override System.Collections.Generic.IList<Gabriel.Cat.S.Utilitats.Propiedad> GetParams()
		{
			return new Gabriel.Cat.S.Utilitats.Propiedad[]{ new Gabriel.Cat.S.Utilitats.Propiedad(this, nameof(OffsetA)), new Gabriel.Cat.S.Utilitats.Propiedad(this, nameof(OffsetB))};
		}
		protected unsafe override void CargarCamando(ScriptAndASMManager scriptManager,byte* ptrRom, int offsetComando)
		{
			offsetA=new OffsetRom(ptrRom,offsetComando);
			offsetB=new OffsetRom(ptrRom,offsetComando+OffsetRom.LENGTH);
		}
		public override byte[] GetBytesTemp()
		{
			byte[] data=new byte[Size];
			data[0]=IdComando;
			OffsetRom.Set(data,1,offsetA);
			OffsetRom.Set(data,5,offsetB);
			return data;
		}
	}
}
