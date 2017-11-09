/*
 * Usuario: Pikachu240
 * Licencia GNU GPL V3
 */
using System;

namespace PokemonGBAFrameWork.ComandosScript
{
 /// <summary>
 /// Description of cmdD9.
 /// </summary>
 public class CmdD9:Comando
 {
  public const byte ID=0xD9;
  public const int SIZE=1;
  
  public CmdD9() 
  {
   
  }
   
  public CmdD9(RomGba rom,int offset):base(rom,offset)
  {
  }
  public CmdD9(byte[] bytesScript,int offset):base(bytesScript,offset)
  {}
  public unsafe CmdD9(byte* ptRom,int offset):base(ptRom,offset)
  {}
  public override string Descripcion {
   get {
    return "Bajo investigación";
   }
  }

  public override byte IdComando {
   get {
    return ID;
   }
  }
  public override string Nombre {
   get {
    return "cmdD9";
   }
  }
  public override int Size {
   get {
    return SIZE;
   }
  }
                        
 }
}
