/*
 * Usuario: Pikachu240
 * Licencia GNU GPL V3
 */
using System;

namespace PokemonGBAFrameWork.ComandosScript
{
 /// <summary>
 /// Description of CloseOnKeyPress.
 /// </summary>
 public class CloseOnKeyPress:Comando
 {
  public const byte ID=0x68;
  public const int SIZE=1;
  
  public CloseOnKeyPress() 
  {
   
  }
   
  public CloseOnKeyPress(RomGba rom,int offset):base(rom,offset)
  {
  }
  public CloseOnKeyPress(byte[] bytesScript,int offset):base(bytesScript,offset)
  {}
  public unsafe CloseOnKeyPress(byte* ptRom,int offset):base(ptRom,offset)
  {}
  public override string Descripcion {
   get {
    return "Mantiene abierto un mensaje y lo cierra al pulsar una tecla";
   }
  }

  public override byte IdComando {
   get {
    return ID;
   }
  }
  public override string Nombre {
   get {
    return "CloseOnKeyPress";
   }
  }
  public override int Size {
   get {
    return SIZE;
   }
  }
                         
  protected override System.Collections.Generic.IList<object> GetParams()
  {
   return new Object[]{};
  }
  protected unsafe override void CargarCamando(byte* ptrRom, int offsetComando)
  {
   
  }
  protected unsafe override void SetComando(byte* ptrRomPosicionado, params int[] parametrosExtra)
  {
    base.SetComando(ptrRomPosicionado,parametrosExtra);
   
  }
 }
}
