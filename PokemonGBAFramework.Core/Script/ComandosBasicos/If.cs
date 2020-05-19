/*
 * Creado por SharpDevelop.
 * Usuario: Pikachu240
 * Fecha: 31/05/2017
 * Hora: 21:57
 * Licencia GNU GPL V3
 * Para cambiar esta plantilla use Herramientas | Opciones | Codificación | Editar Encabezados Estándar
 */
using Gabriel.Cat.S.Utilitats;
using System;

namespace PokemonGBAFramework.Core.ComandosScript
{
	/// <summary>
	/// Description of If.
	/// </summary>
	public class If1:Comando,IDeclaracion
	{
		public const byte ID=0x6;
		public new const int SIZE=Comando.SIZE+1+OffsetRom.LENGTH;
        public const string NOMBRE = "If1";
        public const string DESCRIPCION = "Comprueba que la condicion sea true con el 'lastresult'";

		public If1():this(0,new Script()) { }
		public If1(byte condicion,Script script)
        {
            Condicion = condicion;
            Script=script;
        }

		public If1(ScriptAndASMManager scriptManager,RomGba rom,int offset):base(scriptManager, rom,offset)
		{}
		public If1(ScriptAndASMManager scriptManager, byte[] bytesScript,int offset):base(scriptManager, bytesScript,offset)
		{}
		public unsafe If1(ScriptAndASMManager scriptManager, byte* ptRom,int offset):base(scriptManager,ptRom,offset)
		{}
        public override string Descripcion
        {
            get
            {
                return DESCRIPCION;
            }
        }

        public override byte IdComando
        {
            get
            {
                return ID;
            }
        }

        public override string Nombre
        {
            get
            {
                return NOMBRE;
            }
        }

        public override int Size
        {
            get
            {
                return SIZE;
            }
        }
        public byte Condicion { get; set; }

        public Script Script { get; set; }
		protected override System.Collections.Generic.IList<object> GetParams()
		{
			return new Object[]{Condicion,Script};
		}

		
		protected unsafe override void CargarCamando(ScriptAndASMManager scriptManager, byte* ptrRom, int offsetComando)
		{
			Condicion=ptrRom[offsetComando++];
			Script=scriptManager.GetScript(ptrRom,new OffsetRom(ptrRom,offsetComando));
			
		}
		public override byte[] GetBytesTemp()
		{
			byte[] declaracion = new byte[Size];
			declaracion[0] = ID;
			declaracion[1] = Condicion;
			OffsetRom.Set(declaracion,2, new OffsetRom(Script.IdUnicoTemp));
			return declaracion;
		}




	}
	public class If2:If1{
		public new const byte ID=0x7;
        public new const string NOMBRE = "If2";
		public If2() : this(0, new Script()) { }
		public If2(byte condicion,Script script):base(condicion, script)
		{}
		public If2(ScriptAndASMManager scriptManager, RomGba rom,int offset):base(scriptManager, rom,offset)
		{}
		public If2(ScriptAndASMManager scriptManager, byte[] bytesScript,int offset):base(scriptManager, bytesScript,offset)
		{}
		public unsafe If2(ScriptAndASMManager scriptManager, byte* ptRom,int offset):base(scriptManager, ptRom,offset)
		{}
		public override string Nombre => NOMBRE;
		public override byte IdComando => ID;
	}
}
