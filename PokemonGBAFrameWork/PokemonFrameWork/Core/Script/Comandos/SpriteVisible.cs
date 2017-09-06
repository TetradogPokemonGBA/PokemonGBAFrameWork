/*
 * Usuario: Pikachu240
 * Licencia GNU GPL V3
 */
using System;

namespace PokemonGBAFrameWork.ComandosScript
{
 /// <summary>
 /// Description of SpriteVisible.
 /// </summary>
 public class SpriteVisible:Comando
 {
  public const byte ID=0x58;
  public const int SIZE=5;
  short personaje;
 short bank;
 short mapa;
 
  public SpriteVisible(short personaje,short bank,short mapa) 
  {
   Personaje=personaje;
 Bank=bank;
 Mapa=mapa;
 
  }
   
  public SpriteVisible(RomGba rom,int offset):base(rom,offset)
  {
  }
  public SpriteVisible(byte[] bytesScript,int offset):base(bytesScript,offset)
  {}
  public unsafe SpriteVisible(byte* ptRom,int offset):base(ptRom,offset)
  {}
  public override string Descripcion {
   get {
    return "Hace visible un sprite del mapa y bank seleccionado";
   }
  }

  public override byte IdComando {
   get {
    return ID;
   }
  }
  public override string Nombre {
   get {
    return "SpriteVisible";
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
 public short Bank
{
get{ return bank;}
set{bank=value;}
}
 public short Mapa
{
get{ return mapa;}
set{mapa=value;}
}
 
  protected override System.Collections.Generic.IList<object> GetParams()
  {
   return new Object[]{personaje,bank,mapa};
  }
  protected unsafe override void CargarCamando(byte* ptrRom, int offsetComando)
  {
   personaje=Word.GetWord(ptrRom,offsetComando);
 offsetComando+=Word.LENGTH;
 bank=Word.GetWord(ptrRom,offsetComando);
 offsetComando+=Word.LENGTH;
 mapa=Word.GetWord(ptrRom,offsetComando);
 offsetComando+=Word.LENGTH;
 
  }
  protected unsafe override void SetComando(byte* ptrRomPosicionado, params int[] parametrosExtra)
  {
    base.SetComando(ptrRomPosicionado,parametrosExtra);
   Word.SetWord(ptrRomPosicionado,Personaje);
 ptrRomPosicionado+=Word.LENGTH;
 Word.SetWord(ptrRomPosicionado,Bank);
 ptrRomPosicionado+=Word.LENGTH;
 Word.SetWord(ptrRomPosicionado,Mapa);
 ptrRomPosicionado+=Word.LENGTH;
 
  }
 }
}