/*
 * Usuario: Pikachu240
 * Licencia GNU GPL V3
 */
using System;

namespace PokemonGBAFrameWork.ComandosScript
{
 /// <summary>
 /// Description of ExecuteRam.
 /// </summary>
 public class ExecuteRam:Comando
 {
  public const byte ID=0xCF;
  public const int SIZE=1;
  
  public ExecuteRam() 
  {
   
  }
   
  public ExecuteRam(RomGba rom,int offset):base(rom,offset)
  {
  }
  public ExecuteRam(byte[] bytesScript,int offset):base(bytesScript,offset)
  {}
  public unsafe ExecuteRam(byte* ptRom,int offset):base(ptRom,offset)
  {}
  public override string Descripcion {
   get {
    return "Calculates the current location of the RAM script area and passes the execution to that offset.";
   }
  }

  public override byte IdComando {
   get {
    return ID;
   }
  }
  public override string Nombre {
   get {
    return "ExecuteRam";
   }
  }
  public override int Size {
   get {
    return SIZE;
   }
  }
                         
  
 }
}
