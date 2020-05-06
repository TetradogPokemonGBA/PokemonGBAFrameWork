/*
 * Usuario: Pikachu240
 * Licencia GNU GPL V3
 */
using System;

namespace PokemonGBAFramework.Core.ComandosScript
{
 /// <summary>
 /// Description of Release.
 /// </summary>
 public class Release:Comando
 {
  public const byte ID=0x6C;
  public const int SIZE=1;
  
  public Release() 
  {
   
  }
   
  public Release(RomGba rom,int offset):base(rom,offset)
  {
  }
  public Release(byte[] bytesScript,int offset):base(bytesScript,offset)
  {}
  public unsafe Release(byte* ptRom,int offset):base(ptRom,offset)
  {}
  public override string Descripcion {
   get {
    return "Permite que el player se pueda mover y cierra cualquier msgbox abierto anteriormente también.";
   }
  }

  public override byte IdComando {
   get {
    return ID;
   }
  }
  public override string Nombre {
   get {
    return "Release";
   }
  }
  public override int Size {
   get {
    return SIZE;
   }
  }
                        
 }
}
