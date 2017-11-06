/*
 * Usuario: Pikachu240
 * Licencia GNU GPL V3
 */
using System;

namespace PokemonGBAFrameWork.ComandosScript
{
 /// <summary>
 /// Description of CheckObedience.
 /// </summary>
 public class CheckObedience:Comando
 {
  public const byte ID=0xCE;
  public const int SIZE=3;
  Word pokemon;
 
  public CheckObedience(Word pokemon) 
  {
   Pokemon=pokemon;
 
  }
   
  public CheckObedience(RomGba rom,int offset):base(rom,offset)
  {
  }
  public CheckObedience(byte[] bytesScript,int offset):base(bytesScript,offset)
  {}
  public unsafe CheckObedience(byte* ptRom,int offset):base(ptRom,offset)
  {}
  public override string Descripcion {
   get {
    return "Comprueba si el pokemon del equipo especificado obedece o no y guarda el valor en LASTRESULT.";
   }
  }

  public override byte IdComando {
   get {
    return ID;
   }
  }
  public override string Nombre {
   get {
    return "CheckObedience";
   }
  }
  public override int Size {
   get {
    return SIZE;
   }
  }
                         public Word Pokemon
{
get{ return pokemon;}
set{pokemon=value;}
}
 
  protected override System.Collections.Generic.IList<object> GetParams()
  {
   return new Object[]{pokemon};
  }
  protected unsafe override void CargarCamando(byte* ptrRom, int offsetComando)
  {
   pokemon=new Word(ptrRom,offsetComando);
 offsetComando+=Word.LENGTH;
 
  }
  protected unsafe override void SetComando(byte* ptrRomPosicionado, params int[] parametrosExtra)
  {
    base.SetComando(ptrRomPosicionado,parametrosExtra);
   Word.SetWord(ptrRomPosicionado,Pokemon);
 ptrRomPosicionado+=Word.LENGTH;
 
  }
 }
}
