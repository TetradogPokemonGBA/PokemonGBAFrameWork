/*
 * Creado por SharpDevelop.
 * Usuario: pikachu240
 * Fecha: 31/05/2017
 * Hora: 19:08
 * Licencia GNU GPL V3
 * Para cambiar esta plantilla use Herramientas | Opciones | Codificación | Editar Encabezados Estándar
 */
using System;
using Gabriel.Cat;
using Gabriel.Cat.Extension;
namespace PokemonGBAFrameWork
{
	/// <summary>
	/// Description of Script.
	/// </summary>
	public class Script:IDeclaracion
	{
		public const byte RETURN=0x3;
		public const byte END=0x2;
		
		Llista<Comando> comandosScript;
		
		public Script()
		{
			comandosScript=new Llista<Comando>();
		}

		public Llista<Comando> ComandosScript {
			get {
				return comandosScript;
			}
		}

		public void SetScript(RomGba rom,int offset=-1,bool lastComandIsEnd=true)
		{
			byte[] byteDeclaracion=GetDeclaracion(rom,lastComandIsEnd);
			if(offset<0)
				rom.Data.SearchEmptyBytes(byteDeclaracion.Length);
			if(offset<0)
				throw new RomSinEspacioException();
			

		}
		public byte[] GetDeclaracion(RomGba rom,params object[] parametros)
		{
			int sizeTotal=1;//el utimo byte
			int offset=0;
			byte[] bytesDeclaracion;
			bool isEnd=parametros.Length==0?true:(bool)parametros[0];
			IDeclaracion  comandoHaDeclarar;
			int offsetDeclaracion=0;
			byte[] bytesDeclaracionAux;
			for(int i=0;i<comandosScript.Count;i++)
				sizeTotal+=comandosScript[i].Size;
			bytesDeclaracion=new byte[sizeTotal];

			for(int i=0;i<comandosScript.Count;i++)
			{
				comandoHaDeclarar=comandosScript[i] as IDeclaracion;
				if(comandoHaDeclarar!=null)
				{
					bytesDeclaracionAux=comandoHaDeclarar.GetDeclaracion(rom);
					offsetDeclaracion=rom.Data.SearchArray(bytesDeclaracionAux);
					if(offsetDeclaracion<0)
						offsetDeclaracion=rom.Data.SetArray(bytesDeclaracionAux);
				}
				bytesDeclaracion.SetArray(offset,comandosScript[i].GetComandoArray(offsetDeclaracion));
				
				offset+=comandosScript[i].Size;
			}
			
			if(isEnd)
				bytesDeclaracion[offset]=END;
			else bytesDeclaracion[offset]=RETURN;
			
			return bytesDeclaracion;
		}
	}
}
