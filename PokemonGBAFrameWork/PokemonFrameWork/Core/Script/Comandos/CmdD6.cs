/*
 * Usuario: Pikachu240
 * Licencia GNU GPL V3
 */
using System;

namespace PokemonGBAFrameWork.ComandosScript
{
 /// <summary>
 /// Description of CmdD6.
 /// </summary>
 public class CmdD6:Comando
 {
  public const byte ID=0xD6;
  public const int SIZE=1;
  
  public CmdD6() 
  {
   
  }
   
  public CmdD6(RomGba rom,int offset):base(rom,offset)
  {
  }
  public CmdD6(byte[] bytesScript,int offset):base(bytesScript,offset)
  {}
  public unsafe CmdD6(byte* ptRom,int offset):base(ptRom,offset)
  {}
  public override string Descripcion {
   get {
    return "Bajo investigación.";
   }
  }

  public override byte IdComando {
   get {
    return ID;
   }
  }
  public override string Nombre {
   get {
    return "CmdD6";
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
