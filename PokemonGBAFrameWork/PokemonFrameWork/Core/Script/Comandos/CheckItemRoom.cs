/*
 * Usuario: Pikachu240
 * Licencia GNU GPL V3
 */
using System;

namespace PokemonGBAFrameWork.ComandosScript
{
 /// <summary>
 /// Description of CheckItemRoom.
 /// </summary>
 public class CheckItemRoom:Comando
 {
  public const byte ID=0x46;
  public const int SIZE=5;
  Word objeto;
 Word cantidad;
 
  public CheckItemRoom(Word objeto,Word cantidad) 
  {
   Objeto=objeto;
 Cantidad=cantidad;
 
  }
   
  public CheckItemRoom(RomGba rom,int offset):base(rom,offset)
  {
  }
  public CheckItemRoom(byte[] bytesScript,int offset):base(bytesScript,offset)
  {}
  public unsafe CheckItemRoom(byte* ptRom,int offset):base(ptRom,offset)
  {}
  public override string Descripcion {
   get {
    return "Comprueba si el player tiene espacio para los objetos en la mochila";
   }
  }

  public override byte IdComando {
   get {
    return ID;
   }
  }
  public override string Nombre {
   get {
    return "CheckItemRoom";
   }
  }
  public override int Size {
   get {
    return SIZE;
   }
  }
                         public Word Objeto
{
get{ return objeto;}
set{objeto=value;}
}
 public Word Cantidad
{
get{ return cantidad;}
set{cantidad=value;}
}
 
  protected override System.Collections.Generic.IList<object> GetParams()
  {
   return new Object[]{objeto,cantidad};
  }
  protected unsafe override void CargarCamando(byte* ptrRom, int offsetComando)
  {
   objeto=new Word(ptrRom,offsetComando);
 offsetComando+=Word.LENGTH;
 cantidad=new Word(ptrRom,offsetComando);
 offsetComando+=Word.LENGTH;
 
  }
  protected unsafe override void SetComando(byte* ptrRomPosicionado, params int[] parametrosExtra)
  {
    base.SetComando(ptrRomPosicionado,parametrosExtra);
   Word.SetWord(ptrRomPosicionado,Objeto);
 ptrRomPosicionado+=Word.LENGTH;
 Word.SetWord(ptrRomPosicionado,Cantidad);
 ptrRomPosicionado+=Word.LENGTH;
 
  }
 }
}
