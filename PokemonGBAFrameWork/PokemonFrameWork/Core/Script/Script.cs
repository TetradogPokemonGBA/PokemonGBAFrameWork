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
			byte byteComandoActual;
			Comando comandoActual;
			
			do{
				comandoActual=null;
				byteComandoActual=ptrRom[offsetScript];
				offsetScript++;
				switch(byteComandoActual)
				{
						//pongo los comandos
						case Nop.ID:comandoActual=new Nop(ptrRom,offsetScript);break;
						case Nop1.ID:comandoActual=new Nop1(ptrRom,offsetScript);break;
						case Call.ID:comandoActual=new Call(ptrRom,offsetScript);break;
						case Goto.ID:comandoActual=new Goto(ptrRom,offsetScript);break;
						case If1.ID:comandoActual=new If1(ptrRom,offsetScript);break;
						case If2.ID:comandoActual=new If2(ptrRom,offsetScript);break;
						case Gotostd.ID:comandoActual=new Gotostd(ptrRom,offsetScript);break;
						case Callstd.ID:comandoActual=new Callstd(ptrRom,offsetScript);break;
						case Gotostdif.ID:comandoActual=new Gotostdif(ptrRom,offsetScript);break;
						case Callstdif.ID:comandoActual=new Callstdif(ptrRom,offsetScript);break;
						case Jumpram.ID:comandoActual=new Jumpram(ptrRom,offsetScript);break;
						case Killscript.ID:comandoActual=new Killscript(ptrRom,offsetScript);break;
						case SetByte.ID:comandoActual=new SetByte(ptrRom,offsetScript);break;
						case LoadPointer.ID:comandoActual=new LoadPointer(ptrRom,offsetScript);break;
						case SetByte2.ID:comandoActual=new SetByte2(ptrRom,offsetScript);break;
						case WriteByteToOffset.ID:comandoActual=new WriteByteToOffset(ptrRom,offsetScript);break;
						case LoadByteFromPointer.ID:comandoActual=new LoadByteFromPointer(ptrRom,offsetScript);break;
						case SetFarByte.ID:comandoActual=new SetFarByte(ptrRom,offsetScript);break;
						case Copyscriptbanks.ID:comandoActual=new Copyscriptbanks(ptrRom,offsetScript);break;
						case CopyByte.ID:comandoActual=new CopyByte(ptrRom,offsetScript);break;
						case SetVar.ID:comandoActual=new SetVar(ptrRom,offsetScript);break;
						case AddVar.ID:comandoActual=new AddVar(ptrRom,offsetScript);break;
						case SubVar.ID:comandoActual=new SubVar(ptrRom,offsetScript);break;
						case CopyVar.ID:comandoActual=new CopyVar(ptrRom,offsetScript);break;
						case CopyVarIfNotZero.ID:comandoActual=new CopyVarIfNotZero(ptrRom,offsetScript);break;
						case CompareBanks.ID:comandoActual=new CompareBanks(ptrRom,offsetScript);break;
						case CompareBankToByte.ID:comandoActual=new CompareBankToByte(ptrRom,offsetScript);break;
						case CompareBankToFarByte.ID:comandoActual=new CompareBankToFarByte(ptrRom,offsetScript);break;
						case CompareFarByteToBank.ID:comandoActual=new CompareBankToFarByte(ptrRom,offsetScript);break;
						case CompareFarByteToByte.ID:comandoActual=new CompareFarByteToByte(ptrRom,offsetScript);break;
						case CompareFarBytes.ID:comandoActual=new CompareFarBytes(ptrRom,offsetScript);break;
						case Compare.ID:comandoActual=new Compare(ptrRom,offsetScript);break;
						case CompareVars.ID:comandoActual=new CompareVars(ptrRom,offsetScript);break;
						
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
					offsetScript--;//resto el comando porque ya lo sumo antes
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
