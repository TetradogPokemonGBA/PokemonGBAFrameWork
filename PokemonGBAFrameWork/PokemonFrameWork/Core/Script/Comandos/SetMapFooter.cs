/*
 * Usuario: Pikachu240
 * Licencia GNU GPL V3
 */
using System;

namespace PokemonGBAFrameWork.ComandosScript
{
 /// <summary>
 /// Description of SetMapFooter.
 /// </summary>
 public class SetMapFooter:Comando
 {
  public const byte ID=0xA7;
  public const int SIZE=3;
  short footer;
 
  public SetMapFooter(short footer) 
  {
   Footer=footer;
 
  }
   
  public SetMapFooter(RomGba rom,int offset):base(rom,offset)
  {
  }
  public SetMapFooter(byte[] bytesScript,int offset):base(bytesScript,offset)
  {}
  public unsafe SetMapFooter(byte* ptRom,int offset):base(ptRom,offset)
  {}
  public override string Descripcion {
   get {
    return "Cambia el map footer actual del mapa cargando el nuevo. El mapa debe actualizarse luego para funcionar bien.";
   }
  }

  public override byte IdComando {
   get {
    return ID;
   }
  }
  public override string Nombre {
   get {
    return "SetMapFooter";
   }
  }
  public override int Size {
   get {
    return SIZE;
   }
  }
                         public short Footer
{
get{ return footer;}
set{footer=value;}
}
 
  protected override System.Collections.Generic.IList<object> GetParams()
  {
   return new Object[]{footer};
  }
  protected unsafe override void CargarCamando(byte* ptrRom, int offsetComando)
  {
   footer=Word.GetWord(ptrRom,offsetComando);
 offsetComando+=Word.LENGTH;
 
  }
  protected unsafe override void SetComando(byte* ptrRomPosicionado, params int[] parametrosExtra)
  {
    base.SetComando(ptrRomPosicionado,parametrosExtra);
   Word.SetWord(ptrRomPosicionado,Footer);
 ptrRomPosicionado+=Word.LENGTH;
 
  }
 }
}
