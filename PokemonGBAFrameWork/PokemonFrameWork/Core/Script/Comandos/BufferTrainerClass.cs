/*
 * Usuario: Pikachu240
 * Licencia GNU GPL V3
 */
using System;

namespace PokemonGBAFrameWork.ComandosScript
{
 /// <summary>
 /// Description of BufferTrainerClass.
 /// </summary>
 public class BufferTrainerClass:Comando
 {
  public const byte ID=0xDD;
  public const int SIZE=4;
  Byte buffer;
 short claseEntrenador;
 
  public BufferTrainerClass(Byte buffer,short claseEntrenador) 
  {
   Buffer=buffer;
 ClaseEntrenador=claseEntrenador;
 
  }
   
  public BufferTrainerClass(RomGba rom,int offset):base(rom,offset)
  {
  }
  public BufferTrainerClass(byte[] bytesScript,int offset):base(bytesScript,offset)
  {}
  public unsafe BufferTrainerClass(byte* ptRom,int offset):base(ptRom,offset)
  {}
  public override string Descripcion {
   get {
    return "Guarda en el buffer el nombre de la clase de entrenador especificada.";
   }
  }

  public override byte IdComando {
   get {
    return ID;
   }
  }
  public override string Nombre {
   get {
    return "BufferTrainerClass";
   }
  }
  public override int Size {
   get {
    return SIZE;
   }
  }
                         public Byte Buffer
{
get{ return buffer;}
set{buffer=value;}
}
 public short ClaseEntrenador
{
get{ return claseEntrenador;}
set{claseEntrenador=value;}
}
 
  protected override System.Collections.Generic.IList<object> GetParams()
  {
   return new Object[]{buffer,claseEntrenador};
  }
  protected unsafe override void CargarCamando(byte* ptrRom, int offsetComando)
  {
   buffer=*(ptrRom+offsetComando);
 offsetComando++;
 claseEntrenador=Word.GetWord(ptrRom,offsetComando);
 offsetComando+=Word.LENGTH;
 
  }
  protected unsafe override void SetComando(byte* ptrRomPosicionado, params int[] parametrosExtra)
  {
    base.SetComando(ptrRomPosicionado,parametrosExtra);
   *ptrRomPosicionado=buffer;
 ++ptrRomPosicionado; 
 Word.SetWord(ptrRomPosicionado,ClaseEntrenador);
 ptrRomPosicionado+=Word.LENGTH;
 
  }
 }
}
