/*
 * Usuario: Pikachu240
 * Licencia GNU GPL V3
 */
using System;

namespace PokemonGBAFrameWork.ComandosScript
{
 /// <summary>
 /// Description of BufferItems.
 /// </summary>
 public class BufferItems:Comando
 {
  public const byte ID=0xD4;
  public const int SIZE=6;
  Byte buffer;
 short objetoAGuardar;
 short cantidad;
 
  public BufferItems(Byte buffer,short objetoAGuardar,short cantidad) 
  {
   Buffer=buffer;
 ObjetoAGuardar=objetoAGuardar;
 Cantidad=cantidad;
 
  }
   
  public BufferItems(RomGba rom,int offset):base(rom,offset)
  {
  }
  public BufferItems(byte[] bytesScript,int offset):base(bytesScript,offset)
  {}
  public unsafe BufferItems(byte* ptRom,int offset):base(ptRom,offset)
  {}
  public override string Descripcion {
   get {
    return "Stores a plural item name within a specified buffer.";
   }
  }

  public override byte IdComando {
   get {
    return ID;
   }
  }
  public override string Nombre {
   get {
    return "BufferItems";
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
 public short ObjetoAGuardar
{
get{ return objetoAGuardar;}
set{objetoAGuardar=value;}
}
 public short Cantidad
{
get{ return cantidad;}
set{cantidad=value;}
}
 
  protected override System.Collections.Generic.IList<object> GetParams()
  {
   return new Object[]{buffer,objetoAGuardar,cantidad};
  }
  protected unsafe override void CargarCamando(byte* ptrRom, int offsetComando)
  {
   buffer=*(ptrRom+offsetComando);
 offsetComando++;
 objetoAGuardar=Word.GetWord(ptrRom,offsetComando);
 offsetComando+=Word.LENGTH;
 cantidad=Word.GetWord(ptrRom,offsetComando);
 offsetComando+=Word.LENGTH;
 
  }
  protected unsafe override void SetComando(byte* ptrRomPosicionado, params int[] parametrosExtra)
  {
    base.SetComando(ptrRomPosicionado,parametrosExtra);
   *ptrRomPosicionado=buffer;
 ++ptrRomPosicionado; 
 Word.SetWord(ptrRomPosicionado,ObjetoAGuardar);
 ptrRomPosicionado+=Word.LENGTH;
 Word.SetWord(ptrRomPosicionado,Cantidad);
 ptrRomPosicionado+=Word.LENGTH;
 
  }
 }
}
