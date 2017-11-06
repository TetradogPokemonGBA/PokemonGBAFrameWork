/*
 * Usuario: Pikachu240
 * Licencia GNU GPL V3
 */
using System;

namespace PokemonGBAFrameWork.ComandosScript
{
 /// <summary>
 /// Description of BufferContestType.
 /// </summary>
 public class BufferContestType:Comando
 {
  public const byte ID=0xE1;
  public const int SIZE=4;
  Byte buffer;
 Word tipoConcurso;
 
  public BufferContestType(Byte buffer,Word tipoConcurso) 
  {
   Buffer=buffer;
 TipoConcurso=tipoConcurso;
 
  }
   
  public BufferContestType(RomGba rom,int offset):base(rom,offset)
  {
  }
  public BufferContestType(byte[] bytesScript,int offset):base(bytesScript,offset)
  {}
  public unsafe BufferContestType(byte* ptRom,int offset):base(ptRom,offset)
  {}
  public override string Descripcion {
   get {
    return "Guarda el nombre del concurso seleccionado en el buffer especificado.";
   }
  }

  public override byte IdComando {
   get {
    return ID;
   }
  }
  public override string Nombre {
   get {
    return "BufferContestType";
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
 public Word TipoConcurso
{
get{ return tipoConcurso;}
set{tipoConcurso=value;}
}
 
  protected override System.Collections.Generic.IList<object> GetParams()
  {
   return new Object[]{buffer,tipoConcurso};
  }
  protected unsafe override void CargarCamando(byte* ptrRom, int offsetComando)
  {
   buffer=*(ptrRom+offsetComando);
 offsetComando++;
 tipoConcurso=new Word(ptrRom,offsetComando);
 offsetComando+=Word.LENGTH;
 
  }
  protected unsafe override void SetComando(byte* ptrRomPosicionado, params int[] parametrosExtra)
  {
    base.SetComando(ptrRomPosicionado,parametrosExtra);
   *ptrRomPosicionado=buffer;
 ++ptrRomPosicionado; 
 Word.SetWord(ptrRomPosicionado,TipoConcurso);
 ptrRomPosicionado+=Word.LENGTH;
 
  }
 }
}
