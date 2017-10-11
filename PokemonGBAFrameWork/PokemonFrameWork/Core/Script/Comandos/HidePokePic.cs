/*
 * Usuario: Pikachu240
 * Licencia GNU GPL V3
 */
using System;

namespace PokemonGBAFrameWork.ComandosScript
{
 /// <summary>
 /// Description of HidePokePic.
 /// </summary>
 public class HidePokePic:Comando
 {
  public const byte ID=0x76;
  public const int SIZE=1;
  
  public HidePokePic() 
  {
   
  }
   
  public HidePokePic(RomGba rom,int offset):base(rom,offset)
  {
  }
  public HidePokePic(byte[] bytesScript,int offset):base(bytesScript,offset)
  {}
  public unsafe HidePokePic(byte* ptRom,int offset):base(ptRom,offset)
  {}
  public override string Descripcion {
   get {
    return "Oculta una imagen de un pokemon previamente mostrada";
   }
  }

  public override byte IdComando {
   get {
    return ID;
   }
  }
  public override string Nombre {
   get {
    return "HidePokePic";
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
