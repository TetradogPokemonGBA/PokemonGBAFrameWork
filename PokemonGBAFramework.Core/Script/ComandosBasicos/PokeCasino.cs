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
   
  public PokeCasino(ScriptAndASMManager scriptManager,RomGba rom,int offset):base(scriptManager,rom,offset)
  {
  }
  public PokeCasino(ScriptAndASMManager scriptManager,byte[] bytesScript,int offset):base(scriptManager,bytesScript,offset)
  {}
  public unsafe PokeCasino(ScriptAndASMManager scriptManager,byte* ptRom,int offset):base(scriptManager,ptRom,offset)
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
