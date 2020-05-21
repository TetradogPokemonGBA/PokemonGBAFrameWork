/*
 * Usuario: Pikachu240
 * Licencia GNU GPL V3
 */
using System;

namespace PokemonGBAFramework.Core.ComandosScript
{
	/// <summary>
	/// Description of SetHealingPlace.
	/// </summary>
	public class SetHealingPlace:Comando
	{
		//hacer metodo para sacar el mapa del lugar indicado en el script y de un mapa sacar un lugar, RomData o Mapas y HealingPlaces(tiene que existir en algun lado la asociación)
		//Script de nivel de tipo 3 
		//https://www.pokecommunity.com/showthread.php?t=189304 para los lugares mmm mirar si se pueden sacar porque si los obtengo y luego obtengo los mapas obtendré información personalizada de cada rom que es lo ideal :)
		public const byte ID=0x9F;
		public new const int SIZE=Comando.SIZE+Word.LENGTH;
		public const string DESCRIPCION= "Establece el lugar donde el jugador va una vez que está sin pokemon con vida.";
		public const string NOMBRE= "SetHealingPlace";
		public SetHealingPlace() { }
		public SetHealingPlace(Word lugar)
		{
			Lugar=lugar;
			
		}
		
		public SetHealingPlace(ScriptAndASMManager scriptManager,RomGba rom,int offset):base(scriptManager,rom,offset)
		{
		}
		public SetHealingPlace(ScriptAndASMManager scriptManager,byte[] bytesScript,int offset):base(scriptManager,bytesScript,offset)
		{}
		public unsafe SetHealingPlace(ScriptAndASMManager scriptManager,byte* ptRom,int offset):base(scriptManager,ptRom,offset)
		{}
		public override string Descripcion {
			get {
				return DESCRIPCION;
			}
		}

		public override byte IdComando {
			get {
				return ID;
			}
		}
		public override string Nombre {
			get {
				return NOMBRE;
			}
		}
		public override int Size {
			get {
				return SIZE;
			}
		}
		public Word Lugar { get; set; }

		protected override System.Collections.Generic.IList<object> GetParams()
		{
			return new Object[]{Lugar};
		}
		protected unsafe override void CargarCamando(ScriptAndASMManager scriptManager,byte* ptrRom, int offsetComando)
		{
			Lugar=new Word(ptrRom,offsetComando);
		}
		public override byte[] GetBytesTemp()
		{
			byte[] data=new byte[Size];
			
			data[0]=IdComando;
			Word.SetData(data,1,Lugar);
			
			return data;
		}
	}
}
