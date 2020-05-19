/*
 * Usuario: Pikachu240
 * Licencia GNU GPL V3
 */
using System;

namespace PokemonGBAFramework.Core.ComandosScript
{
 /// <summary>
 /// Description of ReleaseAll.
 /// </summary>
 public class ReleaseAll:Comando
 {
  public const byte ID=0x6B;
  public const int SIZE=1;
  
  public ReleaseAll() 
  {
   
  }
   
  public ReleaseAll(ScriptAndASMManager scriptManager,RomGba rom,int offset):base(scriptManager,rom,offset)
  {
  }
  public ReleaseAll(ScriptAndASMManager scriptManager,byte[] bytesScript,int offset):base(scriptManager,bytesScript,offset)
  {}
  public unsafe ReleaseAll(ScriptAndASMManager scriptManager,byte* ptRom,int offset):base(scriptManager,ptRom,offset)
  {}
  public override string Descripcion {
   get {
    return "Deveulve a todos los personajes de la pantalla el movimiento y cierra cualquier mensaje abierto también";
   }
  }

  public override byte IdComando {
   get {
    return ID;
   }
  }
  public override string Nombre {
   get {
    return "ReleaseAll";
   }
  }
  public override int Size {
   get {
    return SIZE;
   }
  }
                         
  
 }
}
