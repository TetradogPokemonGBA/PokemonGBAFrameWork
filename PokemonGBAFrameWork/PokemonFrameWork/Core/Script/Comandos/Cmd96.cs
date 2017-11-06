/*
 * Usuario: Pikachu240
 * Licencia GNU GPL V3
 */
using System;

namespace PokemonGBAFrameWork.ComandosScript
{
 /// <summary>
 /// Description of Cmd96.
 /// </summary>
 public class Cmd96:Comando
 {
  public const byte ID=0x96;
  public const int SIZE=3;
  Word unknow;
 
  public Cmd96(Word unknow) 
  {
   Unknow=unknow;
 
  }
   
  public Cmd96(RomGba rom,int offset):base(rom,offset)
  {
  }
  public Cmd96(byte[] bytesScript,int offset):base(bytesScript,offset)
  {}
  public unsafe Cmd96(byte* ptRom,int offset):base(ptRom,offset)
  {}
  public override string Descripcion {
   get {
    return "Aparentemente no hace absolutamente nada";
   }
  }

  public override byte IdComando {
   get {
    return ID;
   }
  }
  public override string Nombre {
   get {
    return "Cmd96";
   }
  }
  public override int Size {
   get {
    return SIZE;
   }
  }
                         public Word Unknow
{
get{ return unknow;}
set{unknow=value;}
}
 
  protected override System.Collections.Generic.IList<object> GetParams()
  {
   return new Object[]{unknow};
  }
  protected unsafe override void CargarCamando(byte* ptrRom, int offsetComando)
  {
   unknow=new Word(ptrRom,offsetComando);
 offsetComando+=Word.LENGTH;
 
  }
  protected unsafe override void SetComando(byte* ptrRomPosicionado, params int[] parametrosExtra)
  {
    base.SetComando(ptrRomPosicionado,parametrosExtra);
   Word.SetWord(ptrRomPosicionado,Unknow);
 ptrRomPosicionado+=Word.LENGTH;
 
  }
 }
}
