/*
 * Usuario: Pikachu240
 * Licencia GNU GPL V3
 */
using System;

namespace PokemonGBAFrameWork.ComandosScript
{
 /// <summary>
 /// Description of SetWorldMapFlag.
 /// </summary>
 public class SetWorldMapFlag:Comando
 {
  public const byte ID=0xD0;
  public const int SIZE=3;
  short flag;
 
  public SetWorldMapFlag(short flag) 
  {
   Flag=flag;
 
  }
   
  public SetWorldMapFlag(RomGba rom,int offset):base(rom,offset)
  {
  }
  public SetWorldMapFlag(byte[] bytesScript,int offset):base(bytesScript,offset)
  {}
  public unsafe SetWorldMapFlag(byte* ptRom,int offset):base(ptRom,offset)
  {}
  public override string Descripcion {
   get {
    return "Activa el flag que permite hacer vuelo.";
   }
  }

  public override byte IdComando {
   get {
    return ID;
   }
  }
  public override string Nombre {
   get {
    return "SetWorldMapFlag";
   }
  }
  public override int Size {
   get {
    return SIZE;
   }
  }
                         public short Flag
{
get{ return flag;}
set{flag=value;}
}
 
  protected override System.Collections.Generic.IList<object> GetParams()
  {
   return new Object[]{flag};
  }
  protected unsafe override void CargarCamando(byte* ptrRom, int offsetComando)
  {
   flag=Word.GetWord(ptrRom,offsetComando);
 offsetComando+=Word.LENGTH;
 
  }
  protected unsafe override void SetComando(byte* ptrRomPosicionado, params int[] parametrosExtra)
  {
    base.SetComando(ptrRomPosicionado,parametrosExtra);
   Word.SetWord(ptrRomPosicionado,Flag);
 ptrRomPosicionado+=Word.LENGTH;
 
  }
 }
}
