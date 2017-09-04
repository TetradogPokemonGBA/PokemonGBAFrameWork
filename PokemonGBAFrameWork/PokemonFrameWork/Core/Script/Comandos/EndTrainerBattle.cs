/*
 * Usuario: Pikachu240
 * Licencia GNU GPL V3
 */
using System;

namespace PokemonGBAFrameWork.ComandosScript
{
 /// <summary>
 /// Description of EndTrainerBattle.
 /// </summary>
 public class EndTrainerBattle:Comando
 {
  public const byte ID=0x5E;
  public const int SIZE=1;
  
  public EndTrainerBattle() 
  {
   
  }
   
  public EndTrainerBattle(RomGba rom,int offset):base(rom,offset)
  {
  }
  public EndTrainerBattle(byte[] bytesScript,int offset):base(bytesScript,offset)
  {}
  public unsafe EndTrainerBattle(byte* ptRom,int offset):base(ptRom,offset)
  {}
  public override string Descripcion {
   get {
    return "Vuelve desde la batalla contra el entrenador sin empezar el mensaje";
   }
  }

  public override byte IdComando {
   get {
    return ID;
   }
  }
  public override string Nombre {
   get {
    return "EndTrainerBattle";
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
