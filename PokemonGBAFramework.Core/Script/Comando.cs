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
        public static readonly LlistaOrdenada<string, Comando> DicTypes;

        static Comando()
        {
            Assembly assembly = Assembly.Load("PokemonGBAFramework.Core");
            Type[] types = assembly.GetTypes();
			Comando aux;
			DicTypes = new LlistaOrdenada<string, Comando>();


            for (int i = 0; i < types.Length; i++)
            {
				if (types[i].FullName.Contains("ComandosScript"))
				{
					aux = Activator.CreateInstance(types[i]) as Comando;
					if(aux!=default)
					DicTypes.Add(types[i].Name.ToLower(), aux);
				}
            }
			DicTypes.Add("if", new If1());
        }
        internal Comando()
		{
		}
		internal Comando(ScriptAndASMManager scriptManager, RomGba rom, int offsetComando)
			: this(scriptManager,rom.Data.Bytes, offsetComando)
		{
		}
		internal Comando(ScriptAndASMManager scriptManager,byte[] bytesComando, int offset)
		{
			unsafe {
				fixed(byte* ptRom=bytesComando)
					CargarCamando(scriptManager,ptRom, offset);
			}
		}
		internal unsafe Comando(ScriptAndASMManager scriptManager, byte* ptrRom, int offsetComando)
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

		public virtual System.Collections.Generic.IList<Gabriel.Cat.S.Utilitats.Propiedad> GetParams()
		{
			return new Gabriel.Cat.S.Utilitats.Propiedad[]{ };
		}

		protected virtual unsafe  void CargarCamando(ScriptAndASMManager scriptManager,byte* ptrRom, int offsetComando)
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
