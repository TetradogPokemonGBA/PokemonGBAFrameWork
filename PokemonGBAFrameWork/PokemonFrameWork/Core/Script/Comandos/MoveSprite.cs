/*
 * Usuario: Pikachu240
 * Licencia GNU GPL V3
 */
using System;

namespace PokemonGBAFrameWork.ComandosScript
{
 /// <summary>
 /// Description of MoveSprite.
 /// </summary>
 public class MoveSprite:Comando
 {
  public const byte ID=0x57;
  public const int SIZE=7;
  short personajeAMover;
 short coordenadaX;
 short coordenadaY;
 
  public MoveSprite(short personajeAMover,short coordenadaX,short coordenadaY) 
  {
   PersonajeAMover=personajeAMover;
 CoordenadaX=coordenadaX;
 CoordenadaY=coordenadaY;
 
  }
   
  public MoveSprite(RomGba rom,int offset):base(rom,offset)
  {
  }
  public MoveSprite(byte[] bytesScript,int offset):base(bytesScript,offset)
  {}
  public unsafe MoveSprite(byte* ptRom,int offset):base(ptRom,offset)
  {}
  public override string Descripcion {
   get {
    return "Mueve un sprite a una localización especifica";
   }
  }

  public override byte IdComando {
   get {
    return ID;
   }
  }
  public override string Nombre {
   get {
    return "MoveSprite";
   }
  }
  public override int Size {
   get {
    return SIZE;
   }
  }
                         public short PersonajeAMover
{
get{ return personajeAMover;}
set{personajeAMover=value;}
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
   return new Object[]{personajeAMover,coordenadaX,coordenadaY};
  }
  protected unsafe override void CargarCamando(byte* ptrRom, int offsetComando)
  {
   personajeAMover=Word.GetWord(ptrRom,offsetComando);
 offsetComando+=Word.LENGTH;
 coordenadaX=Word.GetWord(ptrRom,offsetComando);
 offsetComando+=Word.LENGTH;
 coordenadaY=Word.GetWord(ptrRom,offsetComando);
 offsetComando+=Word.LENGTH;
 
  }
  protected unsafe override void SetComando(byte* ptrRomPosicionado, params int[] parametrosExtra)
  {
    base.SetComando(ptrRomPosicionado,parametrosExtra);
   Word.SetWord(ptrRomPosicionado,PersonajeAMover);
 ptrRomPosicionado+=Word.LENGTH;
 Word.SetWord(ptrRomPosicionado,CoordenadaX);
 ptrRomPosicionado+=Word.LENGTH;
 Word.SetWord(ptrRomPosicionado,CoordenadaY);
 ptrRomPosicionado+=Word.LENGTH;
 
  }
 }
}
