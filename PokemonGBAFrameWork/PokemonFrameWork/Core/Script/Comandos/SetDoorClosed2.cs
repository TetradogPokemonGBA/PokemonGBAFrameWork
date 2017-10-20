/*
 * Usuario: Pikachu240
 * Licencia GNU GPL V3
 */
using System;

namespace PokemonGBAFrameWork.ComandosScript
{
 /// <summary>
 /// Description of SetDoorClosed2.
 /// </summary>
 public class SetDoorClosed2:Comando
 {
  public const byte ID=0xB0;
  public const int SIZE=5;
  short coordenadaX;
 short coordenadaY;
 
  public SetDoorClosed2(short coordenadaX,short coordenadaY) 
  {
   CoordenadaX=coordenadaX;
 CoordenadaY=coordenadaY;
 
  }
   
  public SetDoorClosed2(RomGba rom,int offset):base(rom,offset)
  {
  }
  public SetDoorClosed2(byte[] bytesScript,int offset):base(bytesScript,offset)
  {}
  public unsafe SetDoorClosed2(byte* ptRom,int offset):base(ptRom,offset)
  {}
  public override string Descripcion {
   get {
    return "Prepara la puerta para ser cerrada. Sin animación.";
   }
  }

  public override byte IdComando {
   get {
    return ID;
   }
  }
  public override string Nombre {
   get {
    return "SetDoorClosed2";
   }
  }
  public override int Size {
   get {
    return SIZE;
   }
  }
                         public short CoordenadaX
{
get{ return coordenadaX;}
set{coordenadaX=value;}
}
 public short CoordenadaY
{
get{ return coordenadaY;}
set{coordenadaY=value;}
}
 
  protected override System.Collections.Generic.IList<object> GetParams()
  {
   return new Object[]{coordenadaX,coordenadaY};
  }
  protected unsafe override void CargarCamando(byte* ptrRom, int offsetComando)
  {
   coordenadaX=Word.GetWord(ptrRom,offsetComando);
 offsetComando+=Word.LENGTH;
 coordenadaY=Word.GetWord(ptrRom,offsetComando);
 offsetComando+=Word.LENGTH;
 
  }
  protected unsafe override void SetComando(byte* ptrRomPosicionado, params int[] parametrosExtra)
  {
    base.SetComando(ptrRomPosicionado,parametrosExtra);
   Word.SetWord(ptrRomPosicionado,CoordenadaX);
 ptrRomPosicionado+=Word.LENGTH;
 Word.SetWord(ptrRomPosicionado,CoordenadaY);
 ptrRomPosicionado+=Word.LENGTH;
 
  }
 }
}
