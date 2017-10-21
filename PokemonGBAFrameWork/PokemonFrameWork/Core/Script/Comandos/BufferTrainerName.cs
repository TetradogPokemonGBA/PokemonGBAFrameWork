/*
 * Usuario: Pikachu240
 * Licencia GNU GPL V3
 */
using System;

namespace PokemonGBAFrameWork.ComandosScript
{
 /// <summary>
 /// Description of BufferTrainerName.
 /// </summary>
 public class BufferTrainerName:Comando
 {
  public const byte ID=0xDE;
  public const int SIZE=4;
  Byte buffer;
 ushort entrenador;
 
  public BufferTrainerName(Byte buffer,ushort entrenador) 
  {
   Buffer=buffer;
 Entrenador=entrenador;
 
  }
   
  public BufferTrainerName(RomGba rom,int offset):base(rom,offset)
  {
  }
  public BufferTrainerName(byte[] bytesScript,int offset):base(bytesScript,offset)
  {}
  public unsafe BufferTrainerName(byte* ptRom,int offset):base(ptRom,offset)
  {}
  public override string Descripcion {
   get {
    return "Guarda en el buffer el nombre del entrenador.";
   }
  }

  public override byte IdComando {
   get {
    return ID;
   }
  }
  public override string Nombre {
   get {
    return "BufferTrainerName";
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
 public ushort Entrenador
{
get{ return entrenador;}
set{entrenador=value;}
}
 
  protected override System.Collections.Generic.IList<object> GetParams()
  {
   return new Object[]{buffer,entrenador};
  }
  protected unsafe override void CargarCamando(byte* ptrRom, int offsetComando)
  {
   buffer=*(ptrRom+offsetComando);
 offsetComando++;
 entrenador=Word.GetWord(ptrRom,offsetComando);
 offsetComando+=Word.LENGTH;
 
  }
  protected unsafe override void SetComando(byte* ptrRomPosicionado, params int[] parametrosExtra)
  {
    base.SetComando(ptrRomPosicionado,parametrosExtra);
   *ptrRomPosicionado=buffer;
 ++ptrRomPosicionado; 
 Word.SetWord(ptrRomPosicionado,Entrenador);
 ptrRomPosicionado+=Word.LENGTH;
 
  }
 }
}