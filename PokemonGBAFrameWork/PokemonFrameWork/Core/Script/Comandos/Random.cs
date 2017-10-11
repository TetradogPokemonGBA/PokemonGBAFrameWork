/*
 * Usuario: Pikachu240
 * Licencia GNU GPL V3
 */
using System;

namespace PokemonGBAFrameWork.ComandosScript
{
 /// <summary>
 /// Description of Random.
 /// </summary>
 public class Random:Comando
 {
  public const byte ID=0x8F;
  public const int SIZE=3;
  short numeroFin;
 
  public Random(short numeroFin) 
  {
   NumeroFin=numeroFin;
 
  }
   
  public Random(RomGba rom,int offset):base(rom,offset)
  {
  }
  public Random(byte[] bytesScript,int offset):base(bytesScript,offset)
  {}
  public unsafe Random(byte* ptRom,int offset):base(ptRom,offset)
  {}
  public override string Descripcion {
   get {
    return "Genera un numero random entre 0 y NumeroFin";
   }
  }

  public override byte IdComando {
   get {
    return ID;
   }
  }
  public override string Nombre {
   get {
    return "Random";
   }
  }
  public override int Size {
   get {
    return SIZE;
   }
  }
                         public short NumeroFin
{
get{ return numeroFin;}
set{numeroFin=value;}
}
 
  protected override System.Collections.Generic.IList<object> GetParams()
  {
   return new Object[]{numeroFin};
  }
  protected unsafe override void CargarCamando(byte* ptrRom, int offsetComando)
  {
   numeroFin=Word.GetWord(ptrRom,offsetComando);
 offsetComando+=Word.LENGTH;
 
  }
  protected unsafe override void SetComando(byte* ptrRomPosicionado, params int[] parametrosExtra)
  {
    base.SetComando(ptrRomPosicionado,parametrosExtra);
   Word.SetWord(ptrRomPosicionado,NumeroFin);
 ptrRomPosicionado+=Word.LENGTH;
 
  }
 }
}
