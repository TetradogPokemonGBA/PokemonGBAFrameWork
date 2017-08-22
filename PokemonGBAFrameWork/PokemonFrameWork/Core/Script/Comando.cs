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
using System.Text;
using Gabriel.Cat;
using Gabriel.Cat.Extension;
namespace PokemonGBAFrameWork
{
	/// <summary>
	/// Description of Comando.
	/// </summary>
	public abstract class Comando
	{
		public const int SIZE=1;
		
		internal Comando()
		{}
		internal Comando(RomGba rom,int offsetComando):this(rom.Data.Bytes,offsetComando)
		{}
		internal Comando(byte[] bytesComando,int offset)
		{
			unsafe{
				fixed(byte* ptRom=bytesComando)
					CargarCamando(ptRom,offset);
			}
		}
		internal unsafe Comando(byte* ptrRom,int offsetComando)
		{
			CargarCamando(ptrRom,offsetComando);
		}
		public abstract string Descripcion
		{
			get;
		}
		public abstract byte IdComando
		{
			get;
		}
		public abstract string Nombre
		{
			get;
		}
		public virtual int Size
		{
			get{return SIZE;}
		}

		public  string LineaEjecucionXSE {
			get{
				StringBuilder strLinea=new StringBuilder(Nombre.ToLower());
				IList<object> parametros=GetParams();
				IBloqueConNombre bloque;
				byte[] bytesAux;
				int aux;
				for(int i=0;i<parametros.Count;i++)
				{
					strLinea.Append(" ");
					bloque=parametros[i] as IBloqueConNombre;
					if(bloque!=null){
						strLinea.Append('@');
						strLinea.Append(bloque.NombreBloque);
					}else
					{
						bytesAux=Serializar.GetBytes(parametros[i]);
						
						if(bytesAux.Length<4)
							bytesAux=bytesAux.AddArray(new byte[4-bytesAux.Length]);
						
						aux=Serializar.ToInt(bytesAux);
						
						if(aux<=byte.MaxValue&&aux>=byte.MinValue)
						{
							strLinea.Append(((Hex)(byte)aux).ByteString);
						}
						else if(aux<=short.MaxValue&&aux>=byte.MinValue)
						{
							strLinea.Append(((Hex)(short)aux).ByteString);
						}else{
							strLinea.Append(((Hex)aux).ByteString);
						}
					}
					
				}
				return strLinea.ToString();
			}
			
		}
		protected virtual IList<object> GetParams()
		{
			return new object[]{};
		}

		protected virtual unsafe  void CargarCamando(byte* ptrRom,int offsetComando)
		{}
		public void SetComando(RomGba rom,int offsetActualComando,params int[] parametrosExtra)
		{
			unsafe{
				fixed(byte* ptRom=rom.Data.Bytes)
					SetComando(ptRom+offsetActualComando,parametrosExtra);
			}
		}
		
		public byte[] GetComandoArray(params int[] parametrosExtra)
		{
			byte[] bytesComando=new byte[Size];
			unsafe{
				fixed(byte* ptComando=bytesComando)
					SetComando(ptComando,parametrosExtra);
				
			}
			return bytesComando;
		}
		protected virtual unsafe void SetComando(byte* ptrRomPosicionado,params int[] parametrosExtra)
		{
			*ptrRomPosicionado=IdComando;
			ptrRomPosicionado++;
		}
		public bool CheckCompatibilidad(PokemonGBAFrameWork.AbreviacionCanon abreviacion)
		{
			return (GetCompatibilidad()&abreviacion)==abreviacion;
		}
		protected virtual PokemonGBAFrameWork.AbreviacionCanon GetCompatibilidad()
		{
			AbreviacionCanon[] abreviaciones=(AbreviacionCanon[])Enum.GetValues(typeof(AbreviacionCanon));
			AbreviacionCanon compatibilidad=abreviaciones[0];
			for(int i=1;i<abreviaciones.Length;i++)
				compatibilidad|=abreviaciones[i];
			return compatibilidad;
			
		}
		
		public override string ToString()
		{
			return Nombre;
		}
		
	}
}
