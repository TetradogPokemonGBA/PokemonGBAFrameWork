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
		public virtual int Size {
			get{ return SIZE; }
		}
        public int ParamsSize
        {
            get
            {
                return Size - Comando.SIZE;
            }
        }
        public  string LineaEjecucionXSE {
			get {
				StringBuilder strLinea = new StringBuilder(Nombre.ToLower());
				IList<object> parametros = GetParams();
				IBloqueConNombre bloque;
				Word auxWord;
				DWord auxDWord;
				OffsetRom auxOffsetRom;
				Hex valor;
				for (int i = 0; i < parametros.Count; i++) {
					strLinea.Append(" ");
					bloque = parametros[i] as IBloqueConNombre;
					if (bloque != null) {
						strLinea.Append('@');
						strLinea.Append(bloque.NombreBloque);
					} else {
						
						strLinea.Append("0x");
						try {
							auxWord = (Word)parametros[i];
							valor = (Hex)auxWord;
						} catch {
							try {
								auxDWord = (DWord)parametros[i];
								valor = (Hex)auxDWord;
							} catch {
								//si es un OffsetRom
								auxOffsetRom = (OffsetRom)parametros[i];
								valor = (Hex)auxOffsetRom.Offset;
							}
						}
						strLinea.Append(valor.ToString());
						
						
					}
					
				}
				return strLinea.ToString();
			}
			
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
		
		public override string ToString()
		{
			return Nombre;
		}
		
        public static Comando LoadXSECommand(string comando)
        {
            comando = NormalizaStringXSE(comando);
            return LoadXSECommand(comando.Contains(" ")?comando.Split(' '):new string[] { comando });
        }
        public static string NormalizaStringXSE(string comando)
        {
            int inicioComentario;

            comando = comando.Trim();
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
            else throw new ComandoMalFormadoExcepcion();
            return comando;
        }
        public static Comando LoadXSECommand(params string[] camposComando)
        {
            Hex aux;
            Comando comando;
            List<Propiedad> propiedades;
            List<Object> parametros = new List<object>();
            Type commandType = DicTypes[camposComando[0].ToLower()];
             propiedades = commandType.GetPropiedades();//mirar de poderlas ordenar con atributos
                for (int j = 0; j < propiedades.Count; j++)
                    if (propiedades[j].Info.Uso.HasFlag(UsoPropiedad.Set)) //uso las propiedades con SET 
                    {
                        aux = camposComando[parametros.Count].Contains("x") ? (Hex)camposComando[parametros.Count].Split('x')[1] : (Hex)int.Parse(camposComando[parametros.Count]);
                        switch (propiedades[j].Info.Tipo.Name)
                        {
                            case "byte":
                            case nameof(Byte):
                                parametros.Add((byte)aux);
                                break;
                            case nameof(OffsetRom):
                                parametros.Add(new OffsetRom((int)aux));
                                break;
                            case nameof(Word):
                                parametros.Add(new Word((ushort)aux));
                                break;
                            case nameof(DWord):
                                parametros.Add(new DWord((uint)aux));
                                break;
                        }
                    }
                //     los atributos se pueden ordenar en un momento dado:)
                comando=(Comando)Activator.CreateInstance(commandType, parametros.ToArray());



                return comando;
        }
      
    }
}
