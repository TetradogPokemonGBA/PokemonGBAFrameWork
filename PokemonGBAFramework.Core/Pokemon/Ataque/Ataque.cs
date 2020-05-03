using Gabriel.Cat.S.Extension;
using System;
using System.Collections.Generic;
using System.Text;

namespace PokemonGBAFramework.Core
{
   public class Ataque
    {
		public const int MAXATAQUESSINASM = 511;//hasta que no sepa como se cambia para poner más se queda este maximo :) //hay un tutorial de como hacerlo pero se necesita insertar una rutina ASM link:http://www.pokecommunity.com/showthread.php?t=263479

		enum LongitudCampos
		{

			PointerEfecto = 4,
			ContestData,
			Descripcion = 4,//es un pointer al texto
			ScriptBatalla,
			Animacion
		}
		enum ValoresLimitadoresFin
		{
			Ataque = 0x13E0,
		}

		static readonly byte[] BytesDesLimitadoAtaques;

		public const int LENGTHLIMITADOR = 16;

		static Ataque()
		{
			byte[] valoresUnLimited = (((Gabriel.Cat.S.Utilitats.Hex)(int)ValoresLimitadoresFin.Ataque));
			BytesDesLimitadoAtaques = new byte[LENGTHLIMITADOR];
			BytesDesLimitadoAtaques.SetArray(LENGTHLIMITADOR - valoresUnLimited.Length, valoresUnLimited);
		}
		public Ataque()
		{
			Descripcion = new DescripcionAtaque();
			Datos = new DatosAtaque();
			Concursos = new ConcursosAtaque();
		}
        #region Propiedades
        public NombreAtaque Nombre { get; set; }

        public DescripcionAtaque Descripcion { get; set; }

        public DatosAtaque Datos { get; set; }

        public ConcursosAtaque Concursos { get; set; }

        #region IComparable implementation


        public int CompareTo(object obj)
		{
			int compareTo;
			Ataque ataque = obj as Ataque;
			if (ataque != null)
			{
				compareTo = Nombre.CompareTo(ataque.Nombre);
			}
			else compareTo = (int)Gabriel.Cat.S.Utilitats.CompareTo.Inferior;
			return compareTo;
		}


		#endregion

		#endregion
		public override string ToString()
		{
			return Nombre.ToString();
		}

		public static Ataque Get(RomGba rom, int posicionAtaque)
		{//por mirar la obtenxion del offset descripcion
			Ataque ataque = new Ataque();

			ataque.Nombre = NombreAtaque.Get(rom, posicionAtaque);
			ataque.Descripcion = DescripcionAtaque.Get(rom, posicionAtaque);

			ataque.Datos = DatosAtaque.Get(rom, posicionAtaque);

			ataque.Concursos = ConcursosAtaque.Get(rom, posicionAtaque);

			return ataque;
		}

		public static Ataque[] Get(RomGba rom) => DescripcionAtaque.GetAll<Ataque>(rom,(r,i,o)=> Get(r,i),default);

	}
}
