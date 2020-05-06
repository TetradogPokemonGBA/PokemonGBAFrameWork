/*
 * Usuario: Pikachu240
 * Licencia GNU GPL V3
 */
using System;

namespace PokemonGBAFramework.Core.ComandosScript
{
 /// <summary>
 /// Description of PokeCasino.
 /// </summary>
 public class PokeCasino:Comando
 {
  public const byte ID=0x89;
  public const int SIZE=3;
  
  public PokeCasino() 
  {
   
  }
   
  public PokeCasino(RomGba rom,int offset):base(rom,offset)
  {
  }
  public PokeCasino(byte[] bytesScript,int offset):base(bytesScript,offset)
  {}
  public unsafe PokeCasino(byte* ptRom,int offset):base(ptRom,offset)
  {}
  public override string Descripcion {
   get {
    return "Abre el Casino.";
   }
  }

  public override byte IdComando {
   get {
    return ID;
   }
  }
  public override string Nombre {
   get {
    return "PokeCasino";
   }
  }
  public override int Size {
   get {
    return SIZE;
   }
  }
                         
  
 }
}