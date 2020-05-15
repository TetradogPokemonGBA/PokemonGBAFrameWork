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
        public static readonly string[] ComentariosUnaLinea = { "//", "#" };
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
			DicTypes.Add("msgbox", typeof(LoadPointer));
			DicTypes.Add("if", typeof(If1));
        }
        internal Comando()
		{
		}
		internal Comando(RomGba rom, int offsetComando)
			: this(rom.Data.Bytes, offsetComando)
		{
		}
		internal Comando(byte[] bytesComando, int offset)
		{
			unsafe {
				fixed(byte* ptRom=bytesComando)
					CargarCamando(ptRom, offset);
			}
		}
		internal unsafe Comando(byte* ptrRom, int offsetComando)
		{
			CargarCamando(ptrRom, offsetComando);
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
		public  virtual string LineaEjecucionXSE() {
			
			IBloqueConNombre bloque;
			byte auxByte;
			Word auxWord;
			DWord auxDWord;
			OffsetRom auxOffsetRom;
			Hex valor;
			StringBuilder strLinea = new StringBuilder(Nombre.ToLower());
		    IList<object> parametros = GetParams();

				for (int i = 0; i < parametros.Count; i++) {
					strLinea.Append(" ");
					bloque = parametros[i] as IBloqueConNombre;
					if (bloque != null) {
						strLinea.Append('@');
						strLinea.Append(bloque.NombreBloque);
					} else  {
						
						strLinea.Append("0x");
					if (parametros[i] != null)
					{
						try
						{
							auxByte = (byte)parametros[i];
							valor = (Hex)auxByte;
						}
						catch
						{
							try
							{
								auxWord = (Word)parametros[i];
								valor = (Hex)auxWord;
							}
							catch
							{
								try
								{
									auxDWord = (DWord)parametros[i];
									valor = (Hex)auxDWord;
								}
								catch
								{
									//si es un OffsetRom
									auxOffsetRom = (OffsetRom)parametros[i];
									valor = (Hex)auxOffsetRom.Offset;
								}
							}
						}
					}
					else valor = "0";

			    	strLinea.Append(valor.ToString());
						
						
					}
					
				}
				return strLinea.ToString();
			
			
		}

		protected virtual IList<object> GetParams()
		{
			return new object[]{ };
		}

		protected virtual unsafe  void CargarCamando(byte* ptrRom, int offsetComando)
		{
		}
		public void SetComando(RomGba rom, int offsetActualComando, params int[] parametrosExtra)
		{
			unsafe {
				fixed(byte* ptRom=rom.Data.Bytes)
					SetComando(ptRom + offsetActualComando, parametrosExtra);
			}
		}
		
		public byte[] GetComandoArray(params int[] parametrosExtra)
		{
			byte[] bytesComando = new byte[Size];
			unsafe {
				fixed(byte* ptComando=bytesComando)
					SetComando(ptComando, parametrosExtra);
				
			}
			return bytesComando;
		}
		protected virtual unsafe void SetComando(byte* ptrRomPosicionado, params int[] parametrosExtra)
		{
			*ptrRomPosicionado = IdComando;
		}
		public bool CheckCompatibilidad(PokemonGBAFramework.Core.Edicion.Pokemon abreviacion)
		{
			return (GetCompatibilidad() & abreviacion) == abreviacion;
		}
		protected virtual PokemonGBAFramework.Core.Edicion.Pokemon GetCompatibilidad()
		{
			Edicion.Pokemon[] abreviaciones = (Edicion.Pokemon[])Enum.GetValues(typeof(Edicion.Pokemon));
			Edicion.Pokemon compatibilidad = abreviaciones[0];
			for (int i = 1; i < abreviaciones.Length; i++)
				compatibilidad |= abreviaciones[i];
			return compatibilidad;
			
		}
		protected virtual void LoadFromXSE(string[] camposComando)
		{
			//falta testing			
			Hex aux;
			object obj;
			int pos = 0;
			List<Propiedad> propiedades=this.GetPropiedades();
			for (int j = 0; j < propiedades.Count; j++)
				if (propiedades[j].Info.Uso.HasFlag(UsoPropiedad.Set)) //uso las propiedades con SET 
				{
					aux = camposComando[pos].Contains("x") ? (Hex)camposComando[pos].Split('x')[1] : (Hex)int.Parse(camposComando[pos]);
					switch (propiedades[j].Info.Tipo.Name)
					{
						case "byte":
						case nameof(Byte):
							obj=(byte)aux;
							break;
						case nameof(OffsetRom):
							obj=(int)aux;
							break;
						case nameof(Word):
							obj= new Word((ushort)aux);
							break;
						case nameof(DWord):
							obj= new DWord((uint)aux);
							break;
						default:
							obj = default;
							break;
					}
					this.SetProperty(propiedades[j].Info.Nombre, obj);
					pos++;
				}
		}
		
		public override string ToString()
		{
			return Nombre;
		}
		

        public static string NormalizaStringXSE(string comando)
        {
            int inicioComentario;

			comando = comando.Trim();

			while(comando.Contains("  "))
				comando= comando.Replace("  "," ");

			comando = comando.Trim('\r','\n','\t');
			

			if (comando.Length > 0 && !comando.StartsWith(ComentariosUnaLinea))
			{
				inicioComentario = comando.IndexOf("/*");
				if (inicioComentario >= 0)
				{
					comando = comando.Remove(inicioComentario, inicioComentario - comando.IndexOf("*/"));
				} 
                for (int k = 0; k < ComentariosUnaLinea.Length; k++)
                {
                    inicioComentario = comando.IndexOf(ComentariosUnaLinea[k]);
                    if (inicioComentario >= 0)
                    {
                        comando = comando.Remove(inicioComentario);
                    }
                }
            }
            return comando;
		}

		public static Comando LoadXSECommand(params string[] camposComando)
        {
			Comando comando;
			Type commandType=DicTypes[camposComando[0]];
			
			comando= (Comando)Activator.CreateInstance(commandType);
			comando.LoadFromXSE(camposComando);
			return comando;

		}
      
    }
}
