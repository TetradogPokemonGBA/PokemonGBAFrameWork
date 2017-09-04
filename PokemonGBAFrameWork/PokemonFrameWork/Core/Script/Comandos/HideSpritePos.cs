/*
 * Usuario: Pikachu240
 * Licencia GNU GPL V3
 */
using System;

namespace PokemonGBAFrameWork.ComandosScript
{
 /// <summary>
 /// Description of HideSpritePos.
 /// </summary>
 public class HideSpritePos:Comando
 {
  public const byte ID=0x54;
  public const int SIZE=5;
  short personajeAOcultar;
 Byte coordenadaX;
 Byte coordenadaY;
 
  public HideSpritePos(short personajeAOcultar,Byte coordenadaX,Byte coordenadaY) 
  {
   PersonajeAOcultar=personajeAOcultar;
 CoordenadaX=coordenadaX;
 CoordenadaY=coordenadaY;
 
  }
   
  public HideSpritePos(RomGba rom,int offset):base(rom,offset)
  {
  }
  public HideSpritePos(byte[] bytesScript,int offset):base(bytesScript,offset)
  {}
  public unsafe HideSpritePos(byte* ptRom,int offset):base(ptRom,offset)
  {}
  public override string Descripcion {
   get {
    return "Oculta un sprite y luego aplica la posición X/Y";
   }
  }

  public override byte IdComando {
   get {
    return ID;
   }
  }
  public override string Nombre {
   get {
    return "HideSpritePos";
   }
  }
  public override int Size {
   get {
    return SIZE;
   }
  }
                         public short PersonajeAOcultar
{
get{ return personajeAOcultar;}
set{personajeAOcultar=value;}
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
   return new Object[]{personajeAOcultar,coordenadaX,coordenadaY};
  }
  protected unsafe override void CargarCamando(byte* ptrRom, int offsetComando)
  {
   personajeAOcultar=Word.GetWord(ptrRom,offsetComando);
 offsetComando+=Word.LENGTH;
 coordenadaX=*(ptrRom+offsetComando);
 offsetComando++;
 coordenadaY=*(ptrRom+offsetComando);
 offsetComando++;
 
  }
  protected unsafe override void SetComando(byte* ptrRomPosicionado, params int[] parametrosExtra)
  {
    base.SetComando(ptrRomPosicionado,parametrosExtra);
   Word.SetWord(ptrRomPosicionado,PersonajeAOcultar);
 ptrRomPosicionado+=Word.LENGTH;
 *ptrRomPosicionado=coordenadaX;
 ++ptrRomPosicionado; 
 *ptrRomPosicionado=coordenadaY;
 ++ptrRomPosicionado; 
 
  }
 }
}
