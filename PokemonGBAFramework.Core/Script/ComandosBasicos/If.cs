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
	public class If1:Comando,IDeclaracion, IOffsetScript
	{
		public const byte ID=0x6;
		public new const int SIZE=Comando.SIZE+1+OffsetRom.LENGTH;
        public const string NOMBRE = "If1";
        public const string DESCRIPCION = "Comprueba que la condicion sea true con el 'lastresult'";

        OffsetRom offsetScript;
		public If1():this(0,new OffsetRom()) { }
		public If1(byte condicion,OffsetRom offsetScript)
        {
            Condicion = condicion;
            Offset = offsetScript;
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

        public OffsetRom Offset {
			get {
				return offsetScript;
			}
			set {
				if(value==null)
					value=new OffsetRom();
				offsetScript = value;
			}
		}
		protected override System.Collections.Generic.IList<object> GetParams()
		{
			return new Object[]{Condicion,Offset};
		}
		#region implemented abstract members of Comando
		
		protected unsafe override void CargarCamando(byte* ptrRom, int offsetComando)
		{
			//byte condicion ptr script
			Condicion=ptrRom[offsetComando++];
			Offset=new OffsetRom(ptrRom,offsetComando);
			
		}

		protected unsafe override void SetComando(byte* ptrRomPosicionado, params int[] parametrosExtra)
		{
			
			base.SetComando(ptrRomPosicionado,parametrosExtra);
			ptrRomPosicionado+=base.Size;
            *ptrRomPosicionado = Condicion;
            ptrRomPosicionado++;
            OffsetRom.SetOffset(ptrRomPosicionado, Offset);
		}

		

		#endregion

		#region IDeclaracion implementation

		public byte[] GetDeclaracion(byte[] rom, params object[] parametrosExtra)
		{
			return new Script(rom,Offset).GetDeclaracion(rom,parametrosExtra);
		}

		#endregion
		public override string LineaEjecucionXSE()
		{
			return $"if {((Hex)Condicion).ByteString} call {((Hex)(int) Offset).ByteString}";
		}
		protected override void LoadFromXSE(string[] camposComando)
		{
			//if 0xCONDICION call 0xOFFSET
			Condicion = camposComando[1].Contains("x") ? (byte)(Hex)camposComando[1].Split('x')[1] : byte.Parse(camposComando[1]);
			Offset = new OffsetRom(camposComando[3].Contains("x")?(int)(Hex)camposComando[3].Split('x')[1]:int.Parse(camposComando[3]));
		}
	}
	public class If2:If1{
		public new const byte ID=0x7;
        public new const string NOMBRE = "If2";
		public If2() : this(0, new OffsetRom()) { }
		public If2(byte condicion,OffsetRom offsetScript):base(condicion, offsetScript)
		{}
		public If2(RomGba rom,int offset):base(rom,offset)
		{}
		public If2(byte[] bytesScript,int offset):base(bytesScript,offset)
		{}
		public unsafe If2(byte* ptRom,int offset):base(ptRom,offset)
		{}
		public override string Nombre => NOMBRE;
		public override byte IdComando => ID;
	}
}
