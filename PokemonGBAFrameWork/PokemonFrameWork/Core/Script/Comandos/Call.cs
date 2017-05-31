/*
 * Creado por SharpDevelop.
 * Usuario: Pikachu240
 * Fecha: 31/05/2017
 * Hora: 21:20
 * Licencia GNU GPL V3
 * Para cambiar esta plantilla use Herramientas | Opciones | Codificación | Editar Encabezados Estándar
 */
using System;

namespace PokemonGBAFrameWork
{
	/// <summary>
	/// Description of Call.
	/// </summary>
	public class Call:Comando,IDeclaracion
	{
		public const byte ID=0x4;
		public const int SIZE=1+OffsetRom.LENGTH;
		
		Script script;
		public Call(RomGba rom,int offset):base(rom,offset)
		{
		}
		public Call(byte[] bytesScript,int offset):base(bytesScript,offset)
		{}
		public unsafe Call(byte* ptRom,int offset):base(ptRom,offset)
		{}
		public override string Descripcion {
			get {
				return "Continua con la ejecución de otro script que tiene que tener return";
			}
		}

		public override byte IdComando {
			get {
				return ID;
			}
		}

		public override string Nombre {
			get {
				return "Call";
			}
		}

		public override int Size {
			get {
				return SIZE;
			}
		}
		public Script Script
		{
			get{
				return script;
			}
			set{
				if(value==null)
					value=new Script();
				script=value;
				
			}
		}
		#region implemented abstract members of Comando

		protected unsafe override void CargarCamando(byte* ptrRom,int offsetActual)
		{
			byte[] bytesPtr=new byte[OffsetRom.LENGTH];
			byte* ptComando=ptrRom+offsetActual;
			for(int i=0;i<bytesPtr.Length;i++)
			{
				bytesPtr[i]=*ptComando;
				ptComando++;
			}
			Script=new Script(ptrRom,new OffsetRom(bytesPtr).Offset);
		}

		protected unsafe override void SetComando(byte* ptrRom, params int[] parametrosExtra)
		{
			OffsetRom offset;
			try{
				offset=new OffsetRom(parametrosExtra[0]);
				*ptrRom=IdComando;
				ptrRom++;
				for(int i=0;i<OffsetRom.LENGTH;i++)
				{
					*ptrRom=offset.BytesPointer[i];
					ptrRom++;
				}}catch{
				
				throw new ArgumentException("Falta pasar como parametro el offset donde esta la declaracion del script");
			}
		}



		#endregion

		#region IDeclaracion implementation

		public byte[] GetDeclaracion(RomGba rom, params object[] parametrosExtra)
		{
			return script.GetDeclaracion(rom,parametrosExtra);
		}

		#endregion
	}
	public class Goto:Call
	{
		public const byte ID=0x5;
		public Goto(RomGba rom,int offset):base(rom,offset)
		{
		  	
		}
		public Goto(byte[] bytesScript,int offset):base(bytesScript,offset)
		{}
		public unsafe Goto(byte* ptRom,int offset):base(ptRom,offset)
		{}
		public override string Nombre {
			get {
				return "Goto";
			}
		}
		public override byte IdComando {
			get {
				return ID;
			}
		}
		public override string Descripcion {
			get {
				return "Continua con otro script";
			}
		}
		
	
	}
}
