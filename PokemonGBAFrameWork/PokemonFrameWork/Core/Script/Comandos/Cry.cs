/*
 * Usuario: Pikachu240
 * Licencia GNU GPL V3
 */
using System;

namespace PokemonGBAFrameWork.ComandosScript
{
 /// <summary>
 /// Description of Cry.
 /// </summary>
 public class Cry:Comando
 {
  public const byte ID=0xA1;
  public const int SIZE=5;
  short pokemon;
 short efecto;
 
  public Cry(short pokemon,short efecto) 
  {
   Pokemon=pokemon;
 Efecto=efecto;
 
  }
   
  public Cry(RomGba rom,int offset):base(rom,offset)
  {
  }
  public Cry(byte[] bytesScript,int offset):base(bytesScript,offset)
  {}
  public unsafe Cry(byte* ptRom,int offset):base(ptRom,offset)
  {}
  public override string Descripcion {
   get {
    return "Reproduce el grito del pokemon.";
   }
  }

  public override byte IdComando {
   get {
    return ID;
   }
  }
  public override string Nombre {
   get {
    return "Cry";
   }
  }
  public override int Size {
   get {
    return SIZE;
   }
  }
                         public short Pokemon
{
get{ return pokemon;}
set{pokemon=value;}
}
 public short Efecto
{
get{ return efecto;}
set{efecto=value;}
}
 
  protected override System.Collections.Generic.IList<object> GetParams()
  {
   return new Object[]{pokemon,efecto};
  }
  protected unsafe override void CargarCamando(byte* ptrRom, int offsetComando)
  {
   pokemon=Word.GetWord(ptrRom,offsetComando);
 offsetComando+=Word.LENGTH;
 efecto=Word.GetWord(ptrRom,offsetComando);
 offsetComando+=Word.LENGTH;
 
  }
  protected unsafe override void SetComando(byte* ptrRomPosicionado, params int[] parametrosExtra)
  {
    base.SetComando(ptrRomPosicionado,parametrosExtra);
   Word.SetWord(ptrRomPosicionado,Pokemon);
 ptrRomPosicionado+=Word.LENGTH;
 Word.SetWord(ptrRomPosicionado,Efecto);
 ptrRomPosicionado+=Word.LENGTH;
 
  }
 }
}
