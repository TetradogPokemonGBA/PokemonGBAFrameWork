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
		public Script(RomGba rom,int offsetScript):this(rom.Data.Bytes,offsetScript)
		{
			Script script=new Script();
			
		}
		public Script(byte[] bytesScript,int offset=0):this()
		{
			//obtengo los comandos hasta encontrar return o end
			byte byteComandoActual;
			try{
				do{
					byteComandoActual=bytesScript[offset++];
					switch(byteComandoActual)
					{
							//añado el comando sin contar end y return
					}
					if(comandosScript.Count>0)
						offset+=comandosScript[comandosScript.Count-1].Size;
					
				}while(byteComandoActual!=END&&byteComandoActual!=RETURN);
			}catch{
				throw new FormatoRomNoReconocidoException();
			}
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
			bool isEnd=parametros.Length==0?false:(bool)parametros[0];
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
				{//si se tiene que insertar los bytes en la rom para obtener el offset para la declaracion la inserto y listo
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
