/*
 * Usuario: Pikachu240
 * Licencia GNU GPL V3
 */
using System;

namespace PokemonGBAFrameWork.ComandosScript
{
 /// <summary>
 /// Description of MoveSprite2.
 /// </summary>
 public class MoveSprite2:Comando
 {
  public const byte ID=0x63;
  public const int SIZE=7;
  short personaje;
 short coordenadaX;
 short coordenadaY;
 
  public MoveSprite2(short personaje,short coordenadaX,short coordenadaY) 
  {
   Personaje=personaje;
 CoordenadaX=coordenadaX;
 CoordenadaY=coordenadaY;
 
  }
   
  public MoveSprite2(RomGba rom,int offset):base(rom,offset)
  {
  }
  public MoveSprite2(byte[] bytesScript,int offset):base(bytesScript,offset)
  {}
  public unsafe MoveSprite2(byte* ptRom,int offset):base(ptRom,offset)
  {}
  public override string Descripcion {
   get {
    return "Mueve un sprite a una localización especifica (de forma permanente)";
   }
  }

  public override byte IdComando {
   get {
    return ID;
   }
  }
  public override string Nombre {
   get {
    return "MoveSprite2";
   }
  }
  public override int Size {
   get {
    return SIZE;
   }
  }
                         public short Personaje
{
get{ return personaje;}
set{personaje=value;}
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
   return new Object[]{personaje,coordenadaX,coordenadaY};
  }
  protected unsafe override void CargarCamando(byte* ptrRom, int offsetComando)
  {
   personaje=Word.GetWord(ptrRom,offsetComando);
 offsetComando+=Word.LENGTH;
 coordenadaX=Word.GetWord(ptrRom,offsetComando);
 offsetComando+=Word.LENGTH;
 coordenadaY=Word.GetWord(ptrRom,offsetComando);
 offsetComando+=Word.LENGTH;
 
  }
  protected unsafe override void SetComando(byte* ptrRomPosicionado, params int[] parametrosExtra)
  {
    base.SetComando(ptrRomPosicionado,parametrosExtra);
   Word.SetWord(ptrRomPosicionado,Personaje);
 ptrRomPosicionado+=Word.LENGTH;
 Word.SetWord(ptrRomPosicionado,CoordenadaX);
 ptrRomPosicionado+=Word.LENGTH;
 Word.SetWord(ptrRomPosicionado,CoordenadaY);
 ptrRomPosicionado+=Word.LENGTH;
 
  }
 }
}
