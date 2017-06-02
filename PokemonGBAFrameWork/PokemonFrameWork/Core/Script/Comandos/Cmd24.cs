﻿/*
 * Creado por SharpDevelop.
 * Usuario: Pikachu240
 * Fecha: 02/06/2017
 * Hora: 12:44
 * Licencia GNU GPL V3
 * Para cambiar esta plantilla use Herramientas | Opciones | Codificación | Editar Encabezados Estándar
 */
using System;

namespace PokemonGBAFrameWork.Script
{
	/// <summary>
	/// Description of Cmd24.
	/// </summary>
	public class Cmd24:Comando
	{
		public const byte ID=0x24;
		public const int SIZE=1+OffsetRom.LENGTH;
		
		OffsetRom offsetDesconocido;
		public Cmd24(RomGba rom,int offset):base(rom,offset)
		{
		}
		public Cmd24(byte[] bytesScript,int offset):base(bytesScript,offset)
		{}
		public unsafe Cmd24(byte* ptRom,int offset):base(ptRom,offset)
		{}
		public override string Descripcion {
			get {
				return "Se desconoce el uso que tiene";
			}
		}

		public override byte IdComando {
			get {
				return ID;
			}
		}

		public override string Nombre {
			get {
				return "Cmd24";
			}
		}

		public override int Size {
			get {
				return SIZE;
			}
		}

		public OffsetRom OffsetDesconocido {
			get {
				return offsetDesconocido;
			}
			set {
				if(value==null)
					value=new OffsetRom();
				offsetDesconocido = value;
			}
		}

		protected unsafe override void CargarCamando(byte* ptrRom, int offsetComando)
		{
			OffsetRom.SetOffset(ptrRom,offsetComando);
		}
		protected unsafe override void SetComando(byte* ptrRomPosicionado, params int[] parametrosExtra)
		{
			base.SetComando(ptrRomPosicionado, parametrosExtra);
			ptrRomPosicionado++;
			OffsetRom.SetOffset(ptrRomPosicionado,offsetDesconocido);
		}
	}
}