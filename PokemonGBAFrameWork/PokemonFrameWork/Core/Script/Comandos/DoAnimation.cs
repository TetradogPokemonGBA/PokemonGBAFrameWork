/*
 * Usuario: Pikachu240
 * Licencia GNU GPL V3
 */
using System;

namespace PokemonGBAFrameWork.ComandosScript
{
 /// <summary>
 /// Description of DoAnimation.
 /// </summary>
 public class DoAnimation:Comando
 {
  public const byte ID=0x9C;
  public const int SIZE=3;
  Word animacion;
 
  public DoAnimation(Word animacion) 
  {
   Animacion=animacion;
 
  }
   
  public DoAnimation(RomGba rom,int offset):base(rom,offset)
  {
  }
  public DoAnimation(byte[] bytesScript,int offset):base(bytesScript,offset)
  {}
  public unsafe DoAnimation(byte* ptRom,int offset):base(ptRom,offset)
  {}
  public override string Descripcion {
   get {
    return "Ejecuta la animación de movimiento especificada.";
   }
  }

  public override byte IdComando {
   get {
    return ID;
   }
  }
  public override string Nombre {
   get {
    return "DoAnimation";
   }
  }
  public override int Size {
   get {
    return SIZE;
   }
  }
                         public Word Animacion
{
get{ return animacion;}
set{animacion=value;}
}
 
  protected override System.Collections.Generic.IList<object> GetParams()
  {
   return new Object[]{animacion};
  }
  protected unsafe override void CargarCamando(byte* ptrRom, int offsetComando)
  {
   animacion=new Word(ptrRom,offsetComando);
 offsetComando+=Word.LENGTH;
 
  }
  protected unsafe override void SetComando(byte* ptrRomPosicionado, params int[] parametrosExtra)
  {
    base.SetComando(ptrRomPosicionado,parametrosExtra);
   Word.SetWord(ptrRomPosicionado,Animacion);
 ptrRomPosicionado+=Word.LENGTH;
 
  }
 }
}
