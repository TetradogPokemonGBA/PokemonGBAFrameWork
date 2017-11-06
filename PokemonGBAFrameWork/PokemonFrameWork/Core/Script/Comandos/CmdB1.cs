/*
 * Usuario: Pikachu240
 * Licencia GNU GPL V3
 */
using System;

namespace PokemonGBAFrameWork.ComandosScript
{
 /// <summary>
 /// Description of CmdB1.
 /// </summary>
 public class CmdB1:Comando
 {
  public const byte ID=0xB1;
  public const int SIZE=7;
  Byte unknow1;
 Word unknow2;
 Byte unknow3;
 Word unknow4;
 
  public CmdB1(Byte unknow1,Word unknow2,Byte unknow3,Word unknow4) 
  {
   Unknow1=unknow1;
 Unknow2=unknow2;
 Unknow3=unknow3;
 Unknow4=unknow4;
 
  }
   
  public CmdB1(RomGba rom,int offset):base(rom,offset)
  {
  }
  public CmdB1(byte[] bytesScript,int offset):base(bytesScript,offset)
  {}
  public unsafe CmdB1(byte* ptRom,int offset):base(ptRom,offset)
  {}
  public override string Descripcion {
   get {
    return "Bajo investigación.";
   }
  }

  public override byte IdComando {
   get {
    return ID;
   }
  }
  public override string Nombre {
   get {
    return "CmdB1";
   }
  }
  public override int Size {
   get {
    return SIZE;
   }
  }
                         public Byte Unknow1
{
get{ return unknow1;}
set{unknow1=value;}
}
 public Word Unknow2
{
get{ return unknow2;}
set{unknow2=value;}
}
 public Byte Unknow3
{
get{ return unknow3;}
set{unknow3=value;}
}
 public Word Unknow4
{
get{ return unknow4;}
set{unknow4=value;}
}
 
  protected override System.Collections.Generic.IList<object> GetParams()
  {
   return new Object[]{unknow1,unknow2,unknow3,unknow4};
  }
  protected unsafe override void CargarCamando(byte* ptrRom, int offsetComando)
  {
   unknow1=*(ptrRom+offsetComando);
 offsetComando++;
 unknow2=new Word(ptrRom,offsetComando);
 offsetComando+=Word.LENGTH;
 unknow3=*(ptrRom+offsetComando);
 offsetComando++;
 unknow4=new Word(ptrRom,offsetComando);
 offsetComando+=Word.LENGTH;
 
  }
  protected unsafe override void SetComando(byte* ptrRomPosicionado, params int[] parametrosExtra)
  {
    base.SetComando(ptrRomPosicionado,parametrosExtra);
   *ptrRomPosicionado=unknow1;
 ++ptrRomPosicionado; 
 Word.SetWord(ptrRomPosicionado,Unknow2);
 ptrRomPosicionado+=Word.LENGTH;
 *ptrRomPosicionado=unknow3;
 ++ptrRomPosicionado; 
 Word.SetWord(ptrRomPosicionado,Unknow4);
 ptrRomPosicionado+=Word.LENGTH;
 
  }
 }
}
