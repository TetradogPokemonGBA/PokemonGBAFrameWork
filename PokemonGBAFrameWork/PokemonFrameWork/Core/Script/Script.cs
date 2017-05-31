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
		
		}
		public Script(byte[] bytesScript,int offset=0):this()
		{
			unsafe{
				fixed(byte* ptBytesScirpt=bytesScript)
					Cargar(ptBytesScirpt,offset);
			
			}
			
		}
		public unsafe Script(byte* ptRom,int offsetScript):this()
		{
			Cargar(ptRom,offsetScript);
		}
		unsafe void Cargar(byte* ptrRom,int offsetScript)
		{
			//obtengo los comandos hasta encontrar return o end
			byte* ptrScript=ptrRom+offsetScript;
			byte byteComandoActual;
			Comando comandoActual;
			
			do{
				comandoActual=null;
				byteComandoActual=*ptrScript;
				ptrScript++;
				switch(byteComandoActual)
				{
						//pongo los comandos
						case Nop.ID:comandoActual=new Nop(ptrRom,offsetScript);break;
						case Nop1.ID:comandoActual=new Nop1(ptrRom,offsetScript);break;
						//estos me los salto
					case RETURN:
					case END:
						break;
						//si no esta hago una excepcion
					default:
						throw new ScriptMalFormadoException();
				}
				if(comandoActual!=null)
				{
					comandosScript.Add(comandoActual);
					offsetScript+=comandoActual.Size;
				}
				
			}while(byteComandoActual!=END&&byteComandoActual!=RETURN);
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
