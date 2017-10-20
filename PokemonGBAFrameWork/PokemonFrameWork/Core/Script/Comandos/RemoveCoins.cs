/*
 * Usuario: Pikachu240
 * Licencia GNU GPL V3
 */
using System;

namespace PokemonGBAFrameWork.ComandosScript
{
 /// <summary>
 /// Description of RemoveCoins.
 /// </summary>
 public class RemoveCoins:Comando
 {
  public const byte ID=0xB5;
  public const int SIZE=3;
  short numeroDeFichasACoger;
 
  public RemoveCoins(short numeroDeFichasACoger) 
  {
   NumeroDeFichasACoger=numeroDeFichasACoger;
 
  }
   
  public RemoveCoins(RomGba rom,int offset):base(rom,offset)
  {
  }
  public RemoveCoins(byte[] bytesScript,int offset):base(bytesScript,offset)
  {}
  public unsafe RemoveCoins(byte* ptRom,int offset):base(ptRom,offset)
  {}
  public override string Descripcion {
   get {
    return "Coge el numero especificado de fichas del jugador.";
   }
  }

  public override byte IdComando {
   get {
    return ID;
   }
  }
  public override string Nombre {
   get {
    return "RemoveCoins";
   }
  }
  public override int Size {
   get {
    return SIZE;
   }
  }
                         public short NumeroDeFichasACoger
{
get{ return numeroDeFichasACoger;}
set{numeroDeFichasACoger=value;}
}
 
  protected override System.Collections.Generic.IList<object> GetParams()
  {
   return new Object[]{numeroDeFichasACoger};
  }
  protected unsafe override void CargarCamando(byte* ptrRom, int offsetComando)
  {
   numeroDeFichasACoger=Word.GetWord(ptrRom,offsetComando);
 offsetComando+=Word.LENGTH;
 
  }
  protected unsafe override void SetComando(byte* ptrRomPosicionado, params int[] parametrosExtra)
  {
    base.SetComando(ptrRomPosicionado,parametrosExtra);
   Word.SetWord(ptrRomPosicionado,NumeroDeFichasACoger);
 ptrRomPosicionado+=Word.LENGTH;
 
  }
 }
}
