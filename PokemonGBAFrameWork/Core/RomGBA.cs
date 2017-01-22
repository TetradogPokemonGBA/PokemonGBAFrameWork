/*
 * Created by SharpDevelop.
 * User: pc
 * Date: 12/08/2016
 * Time: 18:06
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.IO;
using Gabriel.Cat.Extension;
namespace PokemonGBAFrameWork
{
	/// <summary>
	/// Es una clase para tener el archivo cargado y poderlo modificar comodamente por las demas clases
	/// </summary>
	public class RomGBA
	{
		//hacer un historial con los cambios para poder ir atrás...
		DirectoryInfo dirRom;
		DirectoryInfo dirBackUpRom;
		string nombreRom;
		//en un futuro se podrá crear una rom desde 0 y gestionar las partes desde aqui y no se usará byte[] pero de momento irá asi :)
//no se pondrán cosas calculadas porque se da acceso libre a los datos y pueden cambiar ademas que se duplicaria codigo.	
		//para las zonas tiene que ser algo de clase para que no se este cargando en cada rom.
		//la version viene aqui y solo se cambia con un metodo que primero mira si con la version y el idioma puede saber la compilacion y la edicion si puede porque las zonas cargan bien entonces ya se tiene esos datos y si no funciona se busca cual es la que coincide sin dar error y si no esta da error!!
		byte[] datosRom;
		public RomGBA(FileInfo archivo)
		{
			if(archivo==null)throw new ArgumentNullException();
			
			PathRom=archivo.FullName;
			datosRom=File.ReadAllBytes(archivo.FullName);
			
		}
        public RomGBA(string path):this(new FileInfo(path))
        { }
		public byte[] Datos{
			get{
				return datosRom;
			}
			set{
				if(value==null)throw new ArgumentNullException();
				datosRom=value;
			
			}
		}
		public string PathRom{
			get{
				return Path.Combine(dirRom.FullName,nombreRom+".gba");
			}
			set{
			nombreRom=Path.GetFileNameWithoutExtension(value);
			dirRom=new DirectoryInfo(Path.GetDirectoryName(value));
			}
		}

		public DirectoryInfo DirRom {
			get {
				return dirRom;
			}
			set {
				dirRom = value;
			}
		}

		public DirectoryInfo DirBackUpRom {
			get {
				if(dirBackUpRom==null){
					dirBackUpRom=new DirectoryInfo(Path.Combine(DirRom.FullName,"BackUps -"+NombreRom+"-"));
					if(!dirBackUpRom.Exists)
						dirBackUpRom.Create();
				}
				
				return dirBackUpRom;
			}
			set {
				dirBackUpRom = value;
			}
		}

		public string NombreRom {
			get {
				return nombreRom;
			}
			set {
				//se tiene que validar!!!
				nombreRom = value;
			}
		}
        public bool SePuedeModificar
        {
            get
            {
                bool sePuede = false;
                try
                {
                    new FileInfo(PathRom).OpenWrite().Close();
                    sePuede = true;
                }
                catch { }
                return sePuede;
            }
        }
		public void Guardar(bool sobreEscribirExistente=true,bool actualizarPathSiNoSobreEscribe=false)
		{
			string path;
			if(sobreEscribirExistente)
			{
				Datos.Save(PathRom);
			}else{
				
				path=dirRom.DamePathSinUsar(PathRom);
				Datos.Save(path);
				if(actualizarPathSiNoSobreEscribe)
					PathRom=path;
			}
		}
		/// <summary>
		/// Crea un BackUp del rom como esta en memoria
		/// </summary>
		/// <returns>devuelve la ruta de guardado del backUp</returns>
		public string BackUp()
		{
			string path=Path.Combine(DirBackUpRom.FullName,NombreRom+" -BackUp "+DateTime.Now.ToString().Replace('/','-').Replace(':','·')+"- .gba");
			Datos.Save(path);
			return path;
		}
		
	}
}
