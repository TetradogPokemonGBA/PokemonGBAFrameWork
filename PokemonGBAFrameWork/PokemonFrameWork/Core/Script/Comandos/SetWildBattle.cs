/*
 * Usuario: Pikachu240
 * Licencia GNU GPL V3
 */
using System;

namespace PokemonGBAFrameWork.ComandosScript
{
 /// <summary>
 /// Description of SetWildBattle.
 /// </summary>
 public class SetWildBattle:Comando
 {
  public const byte ID=0xB6;
  public const int SIZE=6;
  Word pokemon;
 Byte nivel;
 Word objeto;
 
  public SetWildBattle(Word pokemon,Byte nivel,Word objeto) 
  {
   Pokemon=pokemon;
 Nivel=nivel;
 Objeto=objeto;
 
  }
   
  public SetWildBattle(RomGba rom,int offset):base(rom,offset)
  {
  }
  public SetWildBattle(byte[] bytesScript,int offset):base(bytesScript,offset)
  {}
  public unsafe SetWildBattle(byte* ptRom,int offset):base(ptRom,offset)
  {}
  public override string Descripcion {
   get {
    return "Prepara la batalla contra un pokemon salvaje.";
   }
  }

  public override byte IdComando {
   get {
    return ID;
   }
  }
  public override string Nombre {
   get {
    return "SetWildBattle";
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
 public Byte Nivel
{
get{ return nivel;}
set{nivel=value;}
}
 public Word Objeto
{
get{ return objeto;}
set{objeto=value;}
}
 
  protected override System.Collections.Generic.IList<object> GetParams()
  {
   return new Object[]{pokemon,nivel,objeto};
  }
  protected unsafe override void CargarCamando(byte* ptrRom, int offsetComando)
  {
   pokemon=new Word(ptrRom,offsetComando);
 offsetComando+=Word.LENGTH;
 nivel=*(ptrRom+offsetComando);
 offsetComando++;
 objeto=new Word(ptrRom,offsetComando);
 offsetComando+=Word.LENGTH;
 
  }
  protected unsafe override void SetComando(byte* ptrRomPosicionado, params int[] parametrosExtra)
  {
    base.SetComando(ptrRomPosicionado,parametrosExtra);
   Word.SetWord(ptrRomPosicionado,Pokemon);
 ptrRomPosicionado+=Word.LENGTH;
 *ptrRomPosicionado=nivel;
 ++ptrRomPosicionado; 
 Word.SetWord(ptrRomPosicionado,Objeto);
 ptrRomPosicionado+=Word.LENGTH;
 
  }
 }
}
