/*
 * Usuario: Pikachu240
 * Licencia GNU GPL V3
 */
using System;

namespace PokemonGBAFrameWork.ComandosScript
{
 /// <summary>
 /// Description of PokenavCall.
 /// </summary>
 public class PokenavCall:Comando
 {
  public const byte ID=0xDF;
  public const int SIZE=5;
  OffsetRom text;
 
  public PokenavCall(OffsetRom text) 
  {
   Text=text;
 
  }
   
  public PokenavCall(RomGba rom,int offset):base(rom,offset)
  {
  }
  public PokenavCall(byte[] bytesScript,int offset):base(bytesScript,offset)
  {}
  public unsafe PokenavCall(byte* ptRom,int offset):base(ptRom,offset)
  {}
  public override string Descripcion {
   get {
    return "Muestra una llamada del Pokenav.";
   }
  }

  public override byte IdComando {
   get {
    return ID;
   }
  }
  public override string Nombre {
   get {
    return "PokenavCall";
   }
  }
  public override int Size {
   get {
    return SIZE;
   }
  }
                         public OffsetRom Text
{
get{ return text;}
set{text=value;}
}
 
  protected override System.Collections.Generic.IList<object> GetParams()
  {
   return new Object[]{text.Offset};
  }
  protected unsafe override void CargarCamando(byte* ptrRom, int offsetComando)
  {
   text=new OffsetRom(ptrRom,new OffsetRom(ptrRom,offsetComando).Offset);
 offsetComando+=OffsetRom.LENGTH;
 
  }
  protected unsafe override void SetComando(byte* ptrRomPosicionado, params int[] parametrosExtra)
  {
    base.SetComando(ptrRomPosicionado,parametrosExtra);
   OffsetRom.SetOffset(ptrRomPosicionado,text);
 ptrRomPosicionado+=OffsetRom.LENGTH;
 
  }
 }
}
