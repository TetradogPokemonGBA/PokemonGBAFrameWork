/*
 * Creado por SharpDevelop.
 * Usuario: Pikachu240
 * Fecha: 31/05/2017
 * Hora: 21:57
 * Licencia GNU GPL V3
 * Para cambiar esta plantilla use Herramientas | Opciones | Codificación | Editar Encabezados Estándar
 */
using System;

namespace PokemonGBAFrameWork.ComandosScript
{
	/// <summary>
	/// Description of If.
	/// </summary>
	public class If1:Comando,IDeclaracion
	{
		public const byte ID=0x6;
		public new const int SIZE=6;
        public const string NOMBRE = "If1";
        public const string DESCRIPCION = "Comprueba que la condicion sea true con el 'lastresult'";

        Script script;
		
		public If1(byte condicion,Script script)
		{
			Condicion=condicion;
			Script=script;
		}
		public If1(RomGba rom,int offset):base(rom,offset)
		{}
		public If1(byte[] bytesScript,int offset):base(bytesScript,offset)
		{}
		public unsafe If1(byte* ptRom,int offset):base(ptRom,offset)
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
		protected override System.Collections.Generic.IList<object> GetParams()
		{
			return new Object[]{Condicion,Script};
		}
		#region implemented abstract members of Comando
		
		protected unsafe override void CargarCamando(byte* ptrRom, int offsetComando)
		{
			//byte condicion ptr script
			Condicion=ptrRom[offsetComando++];
			Script=new Script(ptrRom,new OffsetRom(ptrRom,offsetComando).Offset);
			
		}

		protected unsafe override void SetComando(byte* ptrRomPosicionado, params int[] parametrosExtra)
		{
			
			OffsetRom offset;
			base.SetComando(ptrRomPosicionado,parametrosExtra);
			ptrRomPosicionado++;
			try{
				offset=new OffsetRom(parametrosExtra[0]);
				*ptrRomPosicionado=Condicion;
				ptrRomPosicionado++;
				OffsetRom.SetOffset(ptrRomPosicionado,offset);
				
			}catch{
				
				throw new ArgumentException("Falta pasar como parametro el offset donde esta la declaracion del script");
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
		public new const byte ID=0x7;
        public new const string NOMBRE = "If2";
		public If2(byte condicion,Script script):base(condicion,script)
		{}
		public If2(RomGba rom,int offset):base(rom,offset)
		{}
		public If2(byte[] bytesScript,int offset):base(bytesScript,offset)
		{}
		public unsafe If2(byte* ptRom,int offset):base(ptRom,offset)
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
	}
}
