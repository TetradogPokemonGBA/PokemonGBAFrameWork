/*
 * Usuario: Pikachu240
 * Licencia GNU GPL V3
 */
using System;

namespace PokemonGBAFrameWork.ComandosScript
{
 /// <summary>
 /// Description of CheckCoins.
 /// </summary>
 public class CheckCoins:Comando
 {
  public const byte ID=0xB3;
  public const int SIZE=3;
  short variableAUsar;
 
  public CheckCoins(short variableAUsar) 
  {
   VariableAUsar=variableAUsar;
 
  }
   
  public CheckCoins(RomGba rom,int offset):base(rom,offset)
  {
  }
  public CheckCoins(byte[] bytesScript,int offset):base(bytesScript,offset)
  {}
  public unsafe CheckCoins(byte* ptRom,int offset):base(ptRom,offset)
  {}
  public override string Descripcion {
   get {
    return "Guarda el numero de monedas en la variable.";
   }
  }

  public override byte IdComando {
   get {
    return ID;
   }
  }
  public override string Nombre {
   get {
    return "CheckCoins";
   }
  }
  public override int Size {
   get {
    return SIZE;
   }
  }
                         public short VariableAUsar
{
get{ return variableAUsar;}
set{variableAUsar=value;}
}
 
  protected override System.Collections.Generic.IList<object> GetParams()
  {
   return new Object[]{variableAUsar};
  }
  protected unsafe override void CargarCamando(byte* ptrRom, int offsetComando)
  {
   variableAUsar=Word.GetWord(ptrRom,offsetComando);
 offsetComando+=Word.LENGTH;
 
  }
  protected unsafe override void SetComando(byte* ptrRomPosicionado, params int[] parametrosExtra)
  {
    base.SetComando(ptrRomPosicionado,parametrosExtra);
   Word.SetWord(ptrRomPosicionado,VariableAUsar);
 ptrRomPosicionado+=Word.LENGTH;
 
  }
 }
}
