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
	public class If1:Comando,IDeclaracion
	{
		public const byte ID=0x6;
		public const int SIZE=6;
		
		byte condicion;
		Script script;
		public If1(RomGba rom,int offset):base(rom,offset)
		{}
		public If1(byte[] bytesScript,int offset):base(bytesScript,offset)
		{}
		public unsafe If1(byte* ptRom,int offset):base(ptRom,offset)
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
		
		protected unsafe override void CargarCamando(byte* ptrRom, int offsetComando)
		{
			byte[] bytesPtr=new byte[OffsetRom.LENGTH];
			//byte condicion ptr script
			Condicion=ptrRom[offsetComando++];
			for(int i=0;i<bytesPtr.Length;i++)
				bytesPtr[i]=ptrRom[offsetComando++];
			Script=new Script(ptrRom,new OffsetRom(bytesPtr).Offset);
			
		}

		protected unsafe override void SetComando(byte* ptrRomPosicionado, params int[] parametrosExtra)
		{
			
			OffsetRom offset;
			base.SetComando(ptrRomPosicionado,parametrosExtra);
			try{
				offset=new OffsetRom(parametrosExtra[0]);
				*ptrRomPosicionado=Condicion;
				ptrRomPosicionado++;
				for(int i=0;i<OffsetRom.LENGTH;i++)
				{
					*ptrRomPosicionado=offset.BytesPointer[i];
					ptrRomPosicionado++;
				}}catch{
				
				throw new ArgumentException("Falta pasar como parametro el offset donde esta la declaracion del script");
			}
		}

		public override string Descripcion {
			get {
				return "Comprueba que la condicion sea true con el 'lastresult'";
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

		#region IDeclaracion implementation

		public byte[] GetDeclaracion(RomGba rom, params object[] parametrosExtra)
		{
			return Script.GetDeclaracion(rom,parametrosExtra);
		}

		#endregion
	}
	public class If2:If1{
		public const byte ID=0x7;
		public If2(RomGba rom,int offset):base(rom,offset)
		{}
		public If2(byte[] bytesScript,int offset):base(bytesScript,offset)
		{}
		public unsafe If2(byte* ptRom,int offset):base(ptRom,offset)
		{}
		public override string Nombre {
			get {
				return "If2";
			}
		}
		public override byte IdComando {
			get {
				return ID;
			}
		}
	}
}
