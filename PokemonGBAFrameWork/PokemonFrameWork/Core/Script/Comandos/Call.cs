/*
 * Creado por SharpDevelop.
 * Usuario: Pikachu240
 * Fecha: 31/05/2017
 * Hora: 21:20
 * Licencia GNU GPL V3
 * Para cambiar esta plantilla use Herramientas | Opciones | Codificación | Editar Encabezados Estándar
 */
using System;

namespace PokemonGBAFrameWork.ComandosScript
{//mirar si es obligatorio que acabe en return si no es asi se tendrá que leer el script y devolver ese valor...
	/// <summary>
	/// Description of Call.
	/// </summary>
	public class Call:Comando,IEndScript//,IDeclaracion
	{
		public const byte ID=0x4;
		public const int SIZE=1+OffsetRom.LENGTH;
		
		public const string NOMBRE="Call";
		public const string DESCRIPCION="Continua con la ejecución de otro script que tiene que tener return";
		/*
		Script script;
		
		public Call(Script script)
		{
			Script=script;
		}*/
		
			OffsetRom script;
		
		public Call(OffsetRom script)
		{
			Script=script;
		}
		public Call(RomGba rom,int offset):base(rom,offset)
		{
		}
		public Call(byte[] bytesScript,int offset):base(bytesScript,offset)
		{}
		public unsafe Call(byte* ptRom,int offset):base(ptRom,offset)
		{}
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

		public override string Nombre {
			get {
				return NOMBRE;
			}
		}

		public override int Size {
			get {
				return SIZE;
			}
		}
	/*	public Script Script
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
	*/
		public OffsetRom Script
		{
			get{
				return script;
			}
			set{
				if(value==null)
					value=new OffsetRom();
				script=value;
				
			}
		}	
		public virtual bool IsEnd
		{
			get{return false;}
		}
		#region implemented abstract members of Comando
		protected override System.Collections.Generic.IList<object> GetParams()
		{
			return new object[]{Script};
		}
		protected unsafe override void CargarCamando(byte* ptrRom,int offsetActual)
		{
			//podria ser que llamara a otra cosa que no fuese un script???
			//Script=new Script(ptrRom,new OffsetRom(ptrRom,offsetActual).Offset);
			 Script=new OffsetRom(ptrRom,offsetActual);
		}

		protected unsafe override void SetComando(byte* ptrRom, params int[] parametrosExtra)
		{
			OffsetRom offset;
			base.SetComando(ptrRom,parametrosExtra);
			ptrRom++;
			try{
			//	offset=new OffsetRom(parametrosExtra[0]);
			//	OffsetRom.SetOffset(ptrRom,offset);
				OffsetRom.SetOffset(ptrRom,Script);
			}catch{
				
				throw new ArgumentException("Falta pasar como parametro el offset donde esta la declaracion del script");
			}
		}



		#endregion

		#region IDeclaracion implementation

		/*public byte[] GetDeclaracion(RomGba rom, params object[] parametrosExtra)
		{
			return script.GetDeclaracion(rom,parametrosExtra);
		}
*/
		#endregion
	}
	
}
