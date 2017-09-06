/*
 * Usuario: Pikachu240
 * Licencia GNU GPL V3
 */
using System;

namespace PokemonGBAFrameWork.ComandosScript
{
 /// <summary>
 /// Description of EndTrainterBattle2.
 /// </summary>
 public class EndTrainerBattle2:Comando
 {
  public const byte ID=0x5F;
  public const int SIZE=1;
  
  public EndTrainerBattle2() 
  {
   
  }
   
  public EndTrainerBattle2(RomGba rom,int offset):base(rom,offset)
  {
  }
  public EndTrainerBattle2(byte[] bytesScript,int offset):base(bytesScript,offset)
  {}
  public unsafe EndTrainerBattle2(byte* ptRom,int offset):base(ptRom,offset)
  {}
  public override string Descripcion {
   get {
    return "Vuelve desde la batalla contra el entrenador sin acabar el mensaje";
   }
  }

  public override byte IdComando {
   get {
    return ID;
   }
  }
  public override string Nombre {
   get {
    return "EndTrainterBattle2";
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