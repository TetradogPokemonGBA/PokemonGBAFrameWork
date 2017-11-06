/*
 * Usuario: Pikachu240
 * Licencia GNU GPL V3
 */
using System;

namespace PokemonGBAFrameWork.ComandosScript
{
 /// <summary>
 /// Description of SetOvedience.
 /// </summary>
 public class SetOvedience:Comando
 {
  public const byte ID=0xCD;
  public const int SIZE=3;
  Word pokemon;
 
  public SetOvedience(Word pokemon) 
  {
   Pokemon=pokemon;
 
  }
   
  public SetOvedience(RomGba rom,int offset):base(rom,offset)
  {
  }
  public SetOvedience(byte[] bytesScript,int offset):base(bytesScript,offset)
  {}
  public unsafe SetOvedience(byte* ptRom,int offset):base(ptRom,offset)
  {}
  public override string Descripcion {
   get {
    return "Hace que el pokemon seleccionado del equipo obedezca.";
   }
  }

  public override byte IdComando {
   get {
    return ID;
   }
  }
  public override string Nombre {
   get {
    return "SetOvedience";
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
