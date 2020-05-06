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
   
  public ReleaseAll(RomGba rom,int offset):base(rom,offset)
  {
  }
  public ReleaseAll(byte[] bytesScript,int offset):base(bytesScript,offset)
  {}
  public unsafe ReleaseAll(byte* ptRom,int offset):base(ptRom,offset)
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
