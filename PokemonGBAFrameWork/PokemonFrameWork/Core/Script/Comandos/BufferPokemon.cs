/*
 * Usuario: Pikachu240
 * Licencia GNU GPL V3
 */
using System;

namespace PokemonGBAFrameWork.ComandosScript
{
 /// <summary>
 /// Description of BufferPokemon.
 /// </summary>
 public class BufferPokemon:Comando
 {
  public const byte ID=0x7D;
  public const int SIZE=4;
  Byte buffer;
 Word pokemon;
 
  public BufferPokemon(Byte buffer,Word pokemon) 
  {
   Buffer=buffer;
 Pokemon=pokemon;
 
  }
   
  public BufferPokemon(RomGba rom,int offset):base(rom,offset)
  {
  }
  public BufferPokemon(byte[] bytesScript,int offset):base(bytesScript,offset)
  {}
  public unsafe BufferPokemon(byte* ptRom,int offset):base(ptRom,offset)
  {}
  public override string Descripcion {
   get {
    return "Guarda el nombre de un pokemon en el Buffer especificado";
   }
  }

  public override byte IdComando {
   get {
    return ID;
   }
  }
  public override string Nombre {
   get {
    return "BufferPokemon";
   }
  }
  public override int Size {
   get {
    return SIZE;
   }
  }
                         public Byte Buffer
{
get{ return buffer;}
set{buffer=value;}
}
 public Word Pokemon
{
get{ return pokemon;}
set{pokemon=value;}
}
 
  protected override System.Collections.Generic.IList<object> GetParams()
  {
   return new Object[]{buffer,pokemon};
  }
  protected unsafe override void CargarCamando(byte* ptrRom, int offsetComando)
  {
   buffer=*(ptrRom+offsetComando);
 offsetComando++;
 pokemon=new Word(ptrRom,offsetComando);
 offsetComando+=Word.LENGTH;
 
  }
  protected unsafe override void SetComando(byte* ptrRomPosicionado, params int[] parametrosExtra)
  {
    base.SetComando(ptrRomPosicionado,parametrosExtra);
   *ptrRomPosicionado=buffer;
 ++ptrRomPosicionado; 
 Word.SetWord(ptrRomPosicionado,Pokemon);
 ptrRomPosicionado+=Word.LENGTH;
 
  }
 }
}
