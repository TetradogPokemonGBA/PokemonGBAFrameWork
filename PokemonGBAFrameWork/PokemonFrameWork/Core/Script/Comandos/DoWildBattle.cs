/*
 * Usuario: Pikachu240
 * Licencia GNU GPL V3
 */
using System;

namespace PokemonGBAFrameWork.ComandosScript
{
 /// <summary>
 /// Description of DoWildBattle.
 /// </summary>
 public class DoWildBattle:Comando
 {
  public const byte ID=0xB7;
  public const int SIZE=1;
  
  public DoWildBattle() 
  {
   
  }
   
  public DoWildBattle(RomGba rom,int offset):base(rom,offset)
  {
  }
  public DoWildBattle(byte[] bytesScript,int offset):base(bytesScript,offset)
  {}
  public unsafe DoWildBattle(byte* ptRom,int offset):base(ptRom,offset)
  {}
  public override string Descripcion {
   get {
    return "Ejecuta la batalla preparada con el SetWildBattle.";
   }
  }

  public override byte IdComando {
   get {
    return ID;
   }
  }
  public override string Nombre {
   get {
    return "DoWildBattle";
   }
  }
  public override int Size {
   get {
    return SIZE;
   }
  }
                         
  protected override System.Collections.Generic.IList<object> GetParams()
  {
   return new Object[]{};
  }
  protected unsafe override void CargarCamando(byte* ptrRom, int offsetComando)
  {
   
  }
  protected unsafe override void SetComando(byte* ptrRomPosicionado, params int[] parametrosExtra)
  {
    base.SetComando(ptrRomPosicionado,parametrosExtra);
   
  }
 }
}
