/*
 * Usuario: Pikachu240
 * Licencia GNU GPL V3
 */
using System;

namespace PokemonGBAFrameWork.ComandosScript
{
 /// <summary>
 /// Description of Faceplayer.
 /// </summary>
 public class Faceplayer:Comando
 {
  public const byte ID=0x5A;
  public const int SIZE=1;
  
  public Faceplayer() 
  {
   
  }
   
  public Faceplayer(RomGba rom,int offset):base(rom,offset)
  {
  }
  public Faceplayer(byte[] bytesScript,int offset):base(bytesScript,offset)
  {}
  public unsafe Faceplayer(byte* ptRom,int offset):base(ptRom,offset)
  {}
  public override string Descripcion {
   get {
    return "Mueve el que ha sido llamado hacia el PLAYER";
   }
  }

  public override byte IdComando {
   get {
    return ID;
   }
  }
  public override string Nombre {
   get {
    return "Faceplayer";
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
