/*
 * Usuario: Pikachu240
 * Licencia GNU GPL V3
 */
using System;

namespace PokemonGBAFrameWork.ComandosScript
{
 /// <summary>
 /// Description of Darken.
 /// </summary>
 public class Darken:Comando
 {
  public const byte ID=0x99;
  public const int SIZE=3;
  Word tamañoDestello;
 
  public Darken(Word tamañoDestello) 
  {
   TamañoDestello=tamañoDestello;
 
  }
   
  public Darken(RomGba rom,int offset):base(rom,offset)
  {
  }
  public Darken(byte[] bytesScript,int offset):base(bytesScript,offset)
  {}
  public unsafe Darken(byte* ptRom,int offset):base(ptRom,offset)
  {}
  public override string Descripcion {
   get {
    return "Llama a la animación destello que oscurece el área, deberia ser llamado desde un script de nivel.";
   }
  }

  public override byte IdComando {
   get {
    return ID;
   }
  }
  public override string Nombre {
   get {
    return "Darken";
   }
  }
  public override int Size {
   get {
    return SIZE;
   }
  }
                         public Word TamañoDestello
{
get{ return tamañoDestello;}
set{tamañoDestello=value;}
}
 
  protected override System.Collections.Generic.IList<object> GetParams()
  {
   return new Object[]{tamañoDestello};
  }
  protected unsafe override void CargarCamando(byte* ptrRom, int offsetComando)
  {
   tamañoDestello=new Word(ptrRom,offsetComando);
 offsetComando+=Word.LENGTH;
 
  }
  protected unsafe override void SetComando(byte* ptrRomPosicionado, params int[] parametrosExtra)
  {
    base.SetComando(ptrRomPosicionado,parametrosExtra);
   Word.SetWord(ptrRomPosicionado,TamañoDestello);
 ptrRomPosicionado+=Word.LENGTH;
 
  }
 }
}
