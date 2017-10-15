/*
 * Usuario: Pikachu240
 * Licencia GNU GPL V3
 */
using System;

namespace PokemonGBAFrameWork.ComandosScript
{
 /// <summary>
 /// Description of HideMoney.
 /// </summary>
 public class HideMoney:Comando
 {
  public const byte ID=0x94;
  public const int SIZE=3;
  Byte coordenadaX;
 Byte coordenadaY;
 
  public HideMoney(Byte coordenadaX,Byte coordenadaY) 
  {
   CoordenadaX=coordenadaX;
 CoordenadaY=coordenadaY;
 
  }
   
  public HideMoney(RomGba rom,int offset):base(rom,offset)
  {
  }
  public HideMoney(byte[] bytesScript,int offset):base(bytesScript,offset)
  {}
  public unsafe HideMoney(byte* ptRom,int offset):base(ptRom,offset)
  {}
  public override string Descripcion {
   get {
    return "Oculta la ventana del dinero";
   }
  }

  public override byte IdComando {
   get {
    return ID;
   }
  }
  public override string Nombre {
   get {
    return "HideMoney";
   }
  }
  public override int Size {
   get {
    return SIZE;
   }
  }
                         public Byte CoordenadaX
{
get{ return coordenadaX;}
set{coordenadaX=value;}
}
 public Byte CoordenadaY
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
   coordenadaX=*(ptrRom+offsetComando);
 offsetComando++;
 coordenadaY=*(ptrRom+offsetComando);
 offsetComando++;
 
  }
  protected unsafe override void SetComando(byte* ptrRomPosicionado, params int[] parametrosExtra)
  {
    base.SetComando(ptrRomPosicionado,parametrosExtra);
   *ptrRomPosicionado=coordenadaX;
 ++ptrRomPosicionado; 
 *ptrRomPosicionado=coordenadaY;
 ++ptrRomPosicionado; 
 
  }
 }
}
