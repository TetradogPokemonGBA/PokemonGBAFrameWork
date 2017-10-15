/*
 * Usuario: Pikachu240
 * Licencia GNU GPL V3
 */
using System;

namespace PokemonGBAFrameWork.ComandosScript
{
 /// <summary>
 /// Description of PayMoney.
 /// </summary>
 public class PayMoney:Comando
 {
  public const byte ID=0x91;
  public const int SIZE=6;
  int dineroACoger;
 Byte comprobarEjecucionComando;
 
  public PayMoney(int dineroACoger,Byte comprobarEjecucionComando) 
  {
   DineroACoger=dineroACoger;
 ComprobarEjecucionComando=comprobarEjecucionComando;
 
  }
   
  public PayMoney(RomGba rom,int offset):base(rom,offset)
  {
  }
  public PayMoney(byte[] bytesScript,int offset):base(bytesScript,offset)
  {}
  public unsafe PayMoney(byte* ptRom,int offset):base(ptRom,offset)
  {}
  public override string Descripcion {
   get {
    return "Coge algo de dinero del jugador";
   }
  }

  public override byte IdComando {
   get {
    return ID;
   }
  }
  public override string Nombre {
   get {
    return "PayMoney";
   }
  }
  public override int Size {
   get {
    return SIZE;
   }
  }
                         public Int DineroACoger
{
get{ return dineroACoger;}
set{dineroACoger=value;}
}
 public Byte ComprobarEjecucionComando
{
get{ return comprobarEjecucionComando;}
set{comprobarEjecucionComando=value;}
}
 
  protected override System.Collections.Generic.IList<object> GetParams()
  {
   return new Object[]{dineroACoger,comprobarEjecucionComando};
  }
  protected unsafe override void CargarCamando(byte* ptrRom, int offsetComando)
  {
   dineroACoger=DWord.GetDWord(ptrRom,offsetComando);
 offsetComando+=DWord.LENGTH;
 comprobarEjecucionComando=*(ptrRom+offsetComando);
 offsetComando++;
 
  }
  protected unsafe override void SetComando(byte* ptrRomPosicionado, params int[] parametrosExtra)
  {
    base.SetComando(ptrRomPosicionado,parametrosExtra);
   DWord.SetDWord(ptrRomPosicionado,DineroACoger);dineroACoger
 ptrRomPosicionado+=DWord.LENGTH;
 *ptrRomPosicionado=comprobarEjecucionComando;
 ++ptrRomPosicionado; 
 
  }
 }
}
