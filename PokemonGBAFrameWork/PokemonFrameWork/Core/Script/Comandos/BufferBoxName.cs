/*
 * Usuario: Pikachu240
 * Licencia GNU GPL V3
 */
using System;

namespace PokemonGBAFrameWork.ComandosScript
{
 /// <summary>
 /// Description of BufferBoxName.
 /// </summary>
 public class BufferBoxName:Comando
 {
  public const byte ID=0xC6;
  public const int SIZE=4;
  Byte buffer;
 ushort cajaPcAGuardar;
 
  public BufferBoxName(Byte buffer,ushort cajaPcAGuardar) 
  {
   Buffer=buffer;
 CajaPcAGuardar=cajaPcAGuardar;
 
  }
   
  public BufferBoxName(RomGba rom,int offset):base(rom,offset)
  {
  }
  public BufferBoxName(byte[] bytesScript,int offset):base(bytesScript,offset)
  {}
  public unsafe BufferBoxName(byte* ptRom,int offset):base(ptRom,offset)
  {}
  public override string Descripcion {
   get {
    return "Guarda el nombre de la caja especificada en el buffer especificado";
   }
  }

  public override byte IdComando {
   get {
    return ID;
   }
  }
  public override string Nombre {
   get {
    return "BufferBoxName";
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
 public ushort CajaPcAGuardar
{
get{ return cajaPcAGuardar;}
set{cajaPcAGuardar=value;}
}
 
  protected override System.Collections.Generic.IList<object> GetParams()
  {
   return new Object[]{buffer,cajaPcAGuardar};
  }
  protected unsafe override void CargarCamando(byte* ptrRom, int offsetComando)
  {
   buffer=*(ptrRom+offsetComando);
 offsetComando++;
 cajaPcAGuardar=Word.GetWord(ptrRom,offsetComando);
 offsetComando+=Word.LENGTH;
 
  }
  protected unsafe override void SetComando(byte* ptrRomPosicionado, params int[] parametrosExtra)
  {
    base.SetComando(ptrRomPosicionado,parametrosExtra);
   *ptrRomPosicionado=buffer;
 ++ptrRomPosicionado; 
 Word.SetWord(ptrRomPosicionado,CajaPcAGuardar);
 ptrRomPosicionado+=Word.LENGTH;
 
  }
 }
}
