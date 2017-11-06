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
  Word personaje;
 Word coordenadaX;
 Word coordenadaY;
 
  public MoveSprite2(Word personaje,Word coordenadaX,Word coordenadaY) 
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
                         public Word Personaje
{
get{ return personaje;}
set{personaje=value;}
}
 public Word CoordenadaX
{
get{ return coordenadaX;}
set{coordenadaX=value;}
}
 public Word CoordenadaY
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
   personaje=new Word(ptrRom,offsetComando);
 offsetComando+=Word.LENGTH;
 coordenadaX=new Word(ptrRom,offsetComando);
 offsetComando+=Word.LENGTH;
 coordenadaY=new Word(ptrRom,offsetComando);
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
