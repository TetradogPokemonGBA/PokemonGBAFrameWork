/*
 * Usuario: Pikachu240
 * Licencia GNU GPL V3
 */
using System;

namespace PokemonGBAFrameWork.ComandosScript
{
 /// <summary>
 /// Description of CmdB2.
 /// </summary>
 public class CmdB2:Comando
 {
  public const byte ID=0xB2;
  public const int SIZE=1;
  
  public CmdB2() 
  {
   
  }
   
  public CmdB2(RomGba rom,int offset):base(rom,offset)
  {
  }
  public CmdB2(byte[] bytesScript,int offset):base(bytesScript,offset)
  {}
  public unsafe CmdB2(byte* ptRom,int offset):base(ptRom,offset)
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
    return "CmdB2";
   }
  }
  public override int Size {
   get {
    return SIZE;
   }
  }
                         
 }
}
