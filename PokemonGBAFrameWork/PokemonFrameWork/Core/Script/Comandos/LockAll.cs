/*
 * Usuario: Pikachu240
 * Licencia GNU GPL V3
 */
using System;

namespace PokemonGBAFrameWork.ComandosScript
{
 /// <summary>
 /// Description of LockAll.
 /// </summary>
 public class LockAll:Comando
 {
  public const byte ID=0x69;
  public const int SIZE=1;
  
  public LockAll() 
  {
   
  }
   
  public LockAll(RomGba rom,int offset):base(rom,offset)
  {
  }
  public LockAll(byte[] bytesScript,int offset):base(bytesScript,offset)
  {}
  public unsafe LockAll(byte* ptRom,int offset):base(ptRom,offset)
  {}
  public override string Descripcion {
   get {
    return "Detiene el movimiento de todos los personajes de la pantalla";
   }
  }

  public override byte IdComando {
   get {
    return ID;
   }
  }
  public override string Nombre {
   get {
    return "LockAll";
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
