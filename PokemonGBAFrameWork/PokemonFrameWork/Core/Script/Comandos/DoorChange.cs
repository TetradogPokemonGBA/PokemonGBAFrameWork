/*
 * Usuario: Pikachu240
 * Licencia GNU GPL V3
 */
using System;

namespace PokemonGBAFrameWork.ComandosScript
{
 /// <summary>
 /// Description of DoorChange.
 /// </summary>
 public class DoorChange:Comando
 {
  public const byte ID=0xAE;
  public const int SIZE=1;
  
  public DoorChange() 
  {
   
  }
   
  public DoorChange(RomGba rom,int offset):base(rom,offset)
  {
  }
  public DoorChange(byte[] bytesScript,int offset):base(bytesScript,offset)
  {}
  public unsafe DoorChange(byte* ptRom,int offset):base(ptRom,offset)
  {}
  public override string Descripcion {
   get {
    return "Cambia el estado de la puerta seleccionada.";
   }
  }

  public override byte IdComando {
   get {
    return ID;
   }
  }
  public override string Nombre {
   get {
    return "DoorChange";
   }
  }
  public override int Size {
   get {
    return SIZE;
   }
  }
                         
  
 }
}
