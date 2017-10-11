/*
 * Usuario: Pikachu240
 * Licencia GNU GPL V3
 */
using System;

namespace PokemonGBAFrameWork.ComandosScript
{
 /// <summary>
 /// Description of BufferDecoration.
 /// </summary>
 public class BufferDecoration:Comando
 {
  public const byte ID=0x81;
  public const int SIZE=4;
  Byte buffer;
 short decoracion;
 
  public BufferDecoration(Byte buffer,short decoracion) 
  {
   Buffer=buffer;
 Decoracion=decoracion;
 
  }
   
  public BufferDecoration(RomGba rom,int offset):base(rom,offset)
  {
  }
  public BufferDecoration(byte[] bytesScript,int offset):base(bytesScript,offset)
  {}
  public unsafe BufferDecoration(byte* ptRom,int offset):base(ptRom,offset)
  {}
  public override string Descripcion {
   get {
    return "Guarda el nombre del item decorativo en el Buffer especificado.";
   }
  }

  public override byte IdComando {
   get {
    return ID;
   }
  }
  public override string Nombre {
   get {
    return "BufferDecoration";
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
 public short Decoracion
{
get{ return decoracion;}
set{decoracion=value;}
}
 
  protected override System.Collections.Generic.IList<object> GetParams()
  {
   return new Object[]{buffer,decoracion};
  }
  protected unsafe override void CargarCamando(byte* ptrRom, int offsetComando)
  {
   buffer=*(ptrRom+offsetComando);
 offsetComando++;
 decoracion=Word.GetWord(ptrRom,offsetComando);
 offsetComando+=Word.LENGTH;
 
  }
  protected unsafe override void SetComando(byte* ptrRomPosicionado, params int[] parametrosExtra)
  {
    base.SetComando(ptrRomPosicionado,parametrosExtra);
   *ptrRomPosicionado=buffer;
 ++ptrRomPosicionado; 
 Word.SetWord(ptrRomPosicionado,Decoracion);
 ptrRomPosicionado+=Word.LENGTH;
 
  }
 }
}
