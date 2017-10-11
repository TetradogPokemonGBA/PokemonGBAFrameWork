/*
 * Usuario: Pikachu240
 * Licencia GNU GPL V3
 */
using System;

namespace PokemonGBAFrameWork.ComandosScript
{
 /// <summary>
 /// Description of Multichoice3.
 /// </summary>
 public class Multichoice3:Comando
 {
  public const byte ID=0x71;
  public const int SIZE=6;
  Byte coordenadaX;
 Byte coordenadaY;
 Byte idLista;
 Byte numeroDeOpcionesPorFila;
 Byte botonBCancela;
 
  public Multichoice3(Byte coordenadaX,Byte coordenadaY,Byte idLista,Byte numeroDeOpcionesPorFila,Byte botonBCancela) 
  {
   CoordenadaX=coordenadaX;
 CoordenadaY=coordenadaY;
 IdLista=idLista;
 NumeroDeOpcionesPorFila=numeroDeOpcionesPorFila;
 BotonBCancela=botonBCancela;
 
  }
   
  public Multichoice3(RomGba rom,int offset):base(rom,offset)
  {
  }
  public Multichoice3(byte[] bytesScript,int offset):base(bytesScript,offset)
  {}
  public unsafe Multichoice3(byte* ptRom,int offset):base(ptRom,offset)
  {}
  public override string Descripcion {
   get {
    return "Pone una lista de opciones para que el jugador haga.el número de opciones por fila se puede establecer";
   }
  }

  public override byte IdComando {
   get {
    return ID;
   }
  }
  public override string Nombre {
   get {
    return "Multichoice3";
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
 public Byte NumeroDeOpcionesPorFila
{
get{ return numeroDeOpcionesPorFila;}
set{numeroDeOpcionesPorFila=value;}
}
 public Byte BotonBCancela
{
get{ return botonBCancela;}
set{botonBCancela=value;}
}
 
  protected override System.Collections.Generic.IList<object> GetParams()
  {
   return new Object[]{coordenadaX,coordenadaY,idLista,numeroDeOpcionesPorFila,botonBCancela};
  }
  protected unsafe override void CargarCamando(byte* ptrRom, int offsetComando)
  {
   coordenadaX=*(ptrRom+offsetComando);
 offsetComando++;
 coordenadaY=*(ptrRom+offsetComando);
 offsetComando++;
 idLista=*(ptrRom+offsetComando);
 offsetComando++;
 numeroDeOpcionesPorFila=*(ptrRom+offsetComando);
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
 *ptrRomPosicionado=numeroDeOpcionesPorFila;
 ++ptrRomPosicionado; 
 *ptrRomPosicionado=botonBCancela;
 ++ptrRomPosicionado; 
 
  }
 }
}
