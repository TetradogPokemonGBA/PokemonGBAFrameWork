/*
 * Usuario: Pikachu240
 * Licencia GNU GPL V3
 */
using System;

namespace PokemonGBAFrameWork.ComandosScript
{
 /// <summary>
 /// Description of DoWeather.
 /// </summary>
 public class DoWeather:Comando
 {
  public const byte ID=0xA5;
  public const int SIZE=1;
  
  public DoWeather() 
  {
   
  }
   
  public DoWeather(RomGba rom,int offset):base(rom,offset)
  {
  }
  public DoWeather(byte[] bytesScript,int offset):base(bytesScript,offset)
  {}
  public unsafe DoWeather(byte* ptRom,int offset):base(ptRom,offset)
  {}
  public override string Descripcion {
   get {
    return "Ejecuta el cambio del tiempo hecho con Set/ Reset Weather";
   }
  }

  public override byte IdComando {
   get {
    return ID;
   }
  }
  public override string Nombre {
   get {
    return "DoWeather";
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
