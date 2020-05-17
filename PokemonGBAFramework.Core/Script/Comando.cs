/*
 * Creado por SharpDevelop.
 * Usuario: Pikachu240
 * Fecha: 31/05/2017
 * Hora: 19:09
 * Licencia GNU GPL V3
 * Para cambiar esta plantilla use Herramientas | Opciones | Codificación | Editar Encabezados Estándar
 */
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using Gabriel.Cat.S.Extension;
using Gabriel.Cat.S.Utilitats;
using PokemonGBAFramework.Core.ComandosScript;
using PokemonGBAFramework.Core.Extension;

namespace PokemonGBAFramework.Core
{
	/// <summary>
	/// Description of Comando.
	/// </summary>
	public abstract class Comando
	{
		public const int SIZE = 1;
        public static readonly LlistaOrdenada<string, Type> DicTypes;

        static Comando()
        {
            Assembly assembly = Assembly.Load("PokemonGBAFramework.Core");
            Type[] types = assembly.GetTypes();
            DicTypes = new LlistaOrdenada<string, Type>();


            for (int i = 0; i < types.Length; i++)
            {
				if (types[i].FullName.Contains("ComandosScript"))
					DicTypes.Add(types[i].Name.ToLower(), types[i]);
            }
			//DicTypes.Add("msgbox", typeof(LoadPointer));
			DicTypes.Add("if", typeof(If1));
        }
        internal Comando()
		{
		}
		internal Comando(ScriptManager scriptManager, RomGba rom, int offsetComando)
			: this(scriptManager,rom.Data.Bytes, offsetComando)
		{
		}
		internal Comando(ScriptManager scriptManager,byte[] bytesComando, int offset)
		{
			unsafe {
				fixed(byte* ptRom=bytesComando)
					CargarCamando(scriptManager,ptRom, offset);
			}
		}
		internal unsafe Comando(ScriptManager scriptManager, byte* ptrRom, int offsetComando)
		{
			CargarCamando(scriptManager,ptrRom, offsetComando);
		}
		public abstract string Descripcion {
			get;
		}
		public abstract byte IdComando {
			get;
		}
		public abstract string Nombre {
			get;
		}
		public virtual int Size => SIZE;
		public int ParamsSize=> Size - Comando.SIZE;

		protected virtual IList<object> GetParams()
		{
			return new object[]{ };
		}

		protected virtual unsafe  void CargarCamando(ScriptManager scriptManager,byte* ptrRom, int offsetComando)
		{
		}
		public virtual byte[] GetBytesTemp()
		{
			return new byte[] { IdComando };
		}
		

		public bool CheckCompatibilidad(Edicion.Pokemon abreviacion)
		{
			return (GetCompatibilidad() & abreviacion) == abreviacion;
		}
		protected virtual Edicion.Pokemon GetCompatibilidad()
		{
			Edicion.Pokemon[] abreviaciones = (Edicion.Pokemon[])Enum.GetValues(typeof(Edicion.Pokemon));
			Edicion.Pokemon compatibilidad = abreviaciones[0];
			for (int i = 1; i < abreviaciones.Length; i++)
				compatibilidad |= abreviaciones[i];
			return compatibilidad;
			
		}

		
		public override string ToString()
		{
			return Nombre;
		}
		


      
    }
}
