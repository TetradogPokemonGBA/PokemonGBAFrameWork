/*
 * Usuario: Pikachu240
 * Licencia GNU GPL V3
 */
using System;

namespace PokemonGBAFrameWork.ComandosScript
{
 /// <summary>
 /// Description of WarpTeleport2.
 /// </summary>
 public class WarpTeleport2:Comando
 {
  public const byte ID=0xD1;
  public const int SIZE=8;
  Byte banco;
 Byte mapa;
 Byte salida;
 short coordenadaX;
 short coordenadaY;
 
  public WarpTeleport2(Byte banco,Byte mapa,Byte salida,short coordenadaX,short coordenadaY) 
  {
   Banco=banco;
 Mapa=mapa;
 Salida=salida;
 CoordenadaX=coordenadaX;
 CoordenadaY=coordenadaY;
 
  }
   
  public WarpTeleport2(RomGba rom,int offset):base(rom,offset)
  {
  }
  public WarpTeleport2(byte[] bytesScript,int offset):base(bytesScript,offset)
  {}
  public unsafe WarpTeleport2(byte* ptRom,int offset):base(ptRom,offset)
  {}
  public override string Descripcion {
   get {
    return "Transporta al jugador al mapa especificado con el efecto de teletransportación";
   }
  }

  public override byte IdComando {
   get {
    return ID;
   }
  }
  public override string Nombre {
   get {
    return "WarpTeleport2";
   }
  }
  public override int Size {
   get {
    return SIZE;
   }
  }
                         public Byte Banco
{
get{ return banco;}
set{banco=value;}
}
 public Byte Mapa
{
get{ return mapa;}
set{mapa=value;}
}
 public Byte Salida
{
get{ return salida;}
set{salida=value;}
}
 public short CoordenadaX
{
get{ return coordenadaX;}
set{coordenadaX=value;}
}
 public short CoordenadaY
{
get{ return coordenadaY;}
set{coordenadaY=value;}
}
 
  protected override System.Collections.Generic.IList<object> GetParams()
  {
   return new Object[]{banco,mapa,salida,coordenadaX,coordenadaY};
  }
  protected unsafe override void CargarCamando(byte* ptrRom, int offsetComando)
  {
   banco=*(ptrRom+offsetComando);
 offsetComando++;
 mapa=*(ptrRom+offsetComando);
 offsetComando++;
 salida=*(ptrRom+offsetComando);
 offsetComando++;
 coordenadaX=Word.GetWord(ptrRom,offsetComando);
 offsetComando+=Word.LENGTH;
 coordenadaY=Word.GetWord(ptrRom,offsetComando);
 offsetComando+=Word.LENGTH;
 
  }
  protected unsafe override void SetComando(byte* ptrRomPosicionado, params int[] parametrosExtra)
  {
    base.SetComando(ptrRomPosicionado,parametrosExtra);
   *ptrRomPosicionado=banco;
 ++ptrRomPosicionado; 
 *ptrRomPosicionado=mapa;
 ++ptrRomPosicionado; 
 *ptrRomPosicionado=salida;
 ++ptrRomPosicionado; 
 Word.SetWord(ptrRomPosicionado,CoordenadaX);
 ptrRomPosicionado+=Word.LENGTH;
 Word.SetWord(ptrRomPosicionado,CoordenadaY);
 ptrRomPosicionado+=Word.LENGTH;
 
  }
 }
}
