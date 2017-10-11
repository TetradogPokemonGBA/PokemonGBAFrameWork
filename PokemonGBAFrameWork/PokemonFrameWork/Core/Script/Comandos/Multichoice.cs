/*
 * Usuario: Pikachu240
 * Licencia GNU GPL V3
 */
using System;

namespace PokemonGBAFrameWork.ComandosScript
{
 /// <summary>
 /// Description of Multichoice.
 /// </summary>
 public class Multichoice:Comando
 {
  public const byte ID=0x6F;
  public const int SIZE=5;
  Byte coordenadaX;
 Byte coordenadaY;
 Byte idLista;
 Byte botonBCancela;
 
  public Multichoice(Byte coordenadaX,Byte coordenadaY,Byte idLista,Byte botonBCancela) 
  {
   CoordenadaX=coordenadaX;
 CoordenadaY=coordenadaY;
 IdLista=idLista;
 BotonBCancela=botonBCancela;
 
  }
   
  public Multichoice(RomGba rom,int offset):base(rom,offset)
  {
  }
  public Multichoice(byte[] bytesScript,int offset):base(bytesScript,offset)
  {}
  public unsafe Multichoice(byte* ptRom,int offset):base(ptRom,offset)
  {}
  public override string Descripcion {
   get {
    return "Pone una lista de opciones que el Jugador haga";
   }
  }

  public override byte IdComando {
   get {
    return ID;
   }
  }
  public override string Nombre {
   get {
    return "Multichoice";
   }
  }
  public override int Size {
   get {
    return SIZE;
   }
  }
                         public Byte CoordenadaX
{
get{ return coordenadaX;}
set{coordenadaX=value;}
}
 public Byte CoordenadaY
{
get{ return coordenadaY;}
set{coordenadaY=value;}
}
 public Byte IdLista
{
get{ return idLista;}
set{idLista=value;}
}
 public Byte BotonBCancela
{
get{ return botonBCancela;}
set{botonBCancela=value;}
}
 
  protected override System.Collections.Generic.IList<object> GetParams()
  {
   return new Object[]{coordenadaX,coordenadaY,idLista,botonBCancela};
  }
  protected unsafe override void CargarCamando(byte* ptrRom, int offsetComando)
  {
   coordenadaX=*(ptrRom+offsetComando);
 offsetComando++;
 coordenadaY=*(ptrRom+offsetComando);
 offsetComando++;
 idLista=*(ptrRom+offsetComando);
 offsetComando++;
 botonBCancela=*(ptrRom+offsetComando);
 offsetComando++;
 
  }
  protected unsafe override void SetComando(byte* ptrRomPosicionado, params int[] parametrosExtra)
  {
    base.SetComando(ptrRomPosicionado,parametrosExtra);
   *ptrRomPosicionado=coordenadaX;
 ++ptrRomPosicionado; 
 *ptrRomPosicionado=coordenadaY;
 ++ptrRomPosicionado; 
 *ptrRomPosicionado=idLista;
 ++ptrRomPosicionado; 
 *ptrRomPosicionado=botonBCancela;
 ++ptrRomPosicionado; 
 
  }
 }
}
