/*
 * Usuario: Pikachu240
 * Licencia GNU GPL V3
 */
using System;

namespace PokemonGBAFrameWork.ComandosScript
{
 /// <summary>
 /// Description of BufferStd.
 /// </summary>
 public class BufferStd:Comando
 {
  public const byte ID=0x84;
  public const int SIZE=4;
  Byte buffer;
 short standarString;
 
  public BufferStd(Byte buffer,short standarString) 
  {
   Buffer=buffer;
 StandarString=standarString;
 
  }
   
  public BufferStd(RomGba rom,int offset):base(rom,offset)
  {
  }
  public BufferStd(byte[] bytesScript,int offset):base(bytesScript,offset)
  {}
  public unsafe BufferStd(byte* ptRom,int offset):base(ptRom,offset)
  {}
  public override string Descripcion {
   get {
    return "Guarda una string estandar en el buffer especificado.";
   }
  }

  public override byte IdComando {
   get {
    return ID;
   }
  }
  public override string Nombre {
   get {
    return "BufferStd";
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
 public short StandarString
{
get{ return standarString;}
set{standarString=value;}
}
 
  protected override System.Collections.Generic.IList<object> GetParams()
  {
   return new Object[]{buffer,standarString};
  }
  protected unsafe override void CargarCamando(byte* ptrRom, int offsetComando)
  {
   buffer=*(ptrRom+offsetComando);
 offsetComando++;
 standarString=Word.GetWord(ptrRom,offsetComando);
 offsetComando+=Word.LENGTH;
 
  }
  protected unsafe override void SetComando(byte* ptrRomPosicionado, params int[] parametrosExtra)
  {
    base.SetComando(ptrRomPosicionado,parametrosExtra);
   *ptrRomPosicionado=buffer;
 ++ptrRomPosicionado; 
 Word.SetWord(ptrRomPosicionado,StandarString);
 ptrRomPosicionado+=Word.LENGTH;
 
  }
 }
}
