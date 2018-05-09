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
using Gabriel.Cat.S.Utilitats;

namespace PokemonGBAFrameWork
{
	/// <summary>
	/// Description of Comando.
	/// </summary>
	public abstract class Comando
	{
		public const int SIZE = 1;
		
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
		public bool CheckCompatibilidad(PokemonGBAFrameWork.AbreviacionCanon abreviacion)
		{
			return (GetCompatibilidad() & abreviacion) == abreviacion;
		}
		protected virtual PokemonGBAFrameWork.AbreviacionCanon GetCompatibilidad()
		{
			AbreviacionCanon[] abreviaciones = (AbreviacionCanon[])Enum.GetValues(typeof(AbreviacionCanon));
			AbreviacionCanon compatibilidad = abreviaciones[0];
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
