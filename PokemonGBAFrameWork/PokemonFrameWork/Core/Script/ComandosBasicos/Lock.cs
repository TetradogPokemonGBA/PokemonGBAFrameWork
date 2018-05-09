/*
 * Usuario: Pikachu240
 * Licencia GNU GPL V3
 */
using System;

namespace PokemonGBAFrameWork.ComandosScript
{
 /// <summary>
 /// Description of Lock.
 /// </summary>
 public class Lock:Comando
 {
  public const byte ID=0x6A;
  public const int SIZE=1;
  
  public Lock() 
  {
   
  }
   
  public Lock(RomGba rom,int offset):base(rom,offset)
  {
  }
  public Lock(byte[] bytesScript,int offset):base(bytesScript,offset)
  {}
  public unsafe Lock(byte* ptRom,int offset):base(ptRom,offset)
  {}
  public override string Descripcion {
   get {
    return "Bloquea los movimientos del PLAYER";
   }
  }

  public override byte IdComando {
   get {
    return ID;
   }
  }
  public override string Nombre {
   get {
    return "Lock";
   }
  }
  public override int Size {
   get {
    return SIZE;
   }
  }
                        
 }
}
