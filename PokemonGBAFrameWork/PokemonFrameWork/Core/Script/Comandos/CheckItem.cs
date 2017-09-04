/*
 * Usuario: Pikachu240
 * Licencia GNU GPL V3
 */
using System;

namespace PokemonGBAFrameWork.ComandosScript
{
 /// <summary>
 /// Description of CheckItem.
 /// </summary>
 public class CheckItem:Comando
 {
  public const byte ID=0x47;
  public const int SIZE=5;
  short objeto;
 short cantidad;
 
  public CheckItem(short objeto,short cantidad) 
  {
   Objeto=objeto;
 Cantidad=cantidad;
 
  }
   
  public CheckItem(RomGba rom,int offset):base(rom,offset)
  {
  }
  public CheckItem(byte[] bytesScript,int offset):base(bytesScript,offset)
  {}
  public unsafe CheckItem(byte* ptRom,int offset):base(ptRom,offset)
  {}
  public override string Descripcion {
   get {
    return "Comprueba si el player lleva la cantidad del objeto especificado";
   }
  }

  public override byte IdComando {
   get {
    return ID;
   }
  }
  public override string Nombre {
   get {
    return "CheckItem";
   }
  }
  public override int Size {
   get {
    return SIZE;
   }
  }
                         public short Objeto
{
get{ return objeto;}
set{objeto=value;}
}
 public short Cantidad
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
   objeto=Word.GetWord(ptrRom,offsetComando);
 offsetComando+=Word.LENGTH;
 cantidad=Word.GetWord(ptrRom,offsetComando);
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
