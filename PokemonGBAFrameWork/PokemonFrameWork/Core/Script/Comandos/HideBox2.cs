/*
 * Usuario: Pikachu240
 * Licencia GNU GPL V3
 */
using System;

namespace PokemonGBAFrameWork.ComandosScript
{
 /// <summary>
 /// Description of HideBox2.
 /// </summary>
 public class HideBox2:Comando
 {
  public const byte ID=0xDA;
  public const int SIZE=1;
  
  public HideBox2() 
  {
   
  }
   
  public HideBox2(RomGba rom,int offset):base(rom,offset)
  {
  }
  public HideBox2(byte[] bytesScript,int offset):base(bytesScript,offset)
  {}
  public unsafe HideBox2(byte* ptRom,int offset):base(ptRom,offset)
  {}
  public override string Descripcion {
   get {
    return "Oculta una caja mostrada.";
   }
  }

  public override byte IdComando {
   get {
    return ID;
   }
  }
  public override string Nombre {
   get {
    return "HideBox2";
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
