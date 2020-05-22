/*
 * Creado por SharpDevelop.
 * Usuario: Pikachu240
 * Fecha: 31/05/2017
 * Hora: 21:20
 * Licencia GNU GPL V3
 * Para cambiar esta plantilla use Herramientas | Opciones | Codificación | Editar Encabezados Estándar
 */
using System;

namespace PokemonGBAFramework.Core.ComandosScript
{//mirar si es obligatorio que acabe en return si no es asi se tendrá que leer el script y devolver ese valor...
	/// <summary>
	/// Description of Call.
	/// </summary>
	public class Call:Comando,IEndScript, IScript
	{
		public const byte ID=0x4;
		public new const int SIZE=Comando.SIZE+OffsetRom.LENGTH;
		
		public const string NOMBRE="Call";
		public const string DESCRIPCION="Continua con la ejecución de otro script que tiene que tener return";		

		public Call():this(new Script()) { }
		public Call(Script script)
		{
			Script=script;
		}
		public Call(ScriptAndASMManager scriptManager, RomGba rom,int offset):base(scriptManager,rom,offset)
		{
		}
		public Call(ScriptAndASMManager scriptManager, byte[] bytesScript,int offset):base(scriptManager,bytesScript,offset)
		{}
		public unsafe Call(ScriptAndASMManager scriptManager, byte* ptRom,int offset):base(scriptManager,ptRom,offset)
		{}
		public override string Descripcion => DESCRIPCION;

		public override byte IdComando => ID;

		public override string Nombre => NOMBRE;

		public override int Size => SIZE;
		public Script Script { get; set; }

		public virtual bool IsEnd => false;



		#region implemented abstract members of Comando
		public override System.Collections.Generic.IList<object> GetParams()
		{
			return new object[]{Script};
		}
		protected unsafe override void CargarCamando(ScriptAndASMManager scriptManager, byte* ptrRom,int offsetActual)
		{
			 Script=scriptManager.GetScript(ptrRom,new OffsetRom(ptrRom, offsetActual));
		}

		public override byte[] GetBytesTemp()
		{
			byte[] data = new byte[Size];
			data[0] = IdComando;
			OffsetRom.Set(data, 1, new OffsetRom(Script.IdUnicoTemp));
			return data;
		}



		#endregion

	}
	
}
