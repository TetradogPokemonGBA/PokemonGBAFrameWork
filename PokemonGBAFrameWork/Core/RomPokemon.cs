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
	public class RomPokemon
	{
		//hacer un historial con los cambios para poder ir atrás...
		DirectoryInfo dirRom;
		DirectoryInfo dirBackUpRom;
		string nombreRom;
		
		byte[] datosRom;
		public RomPokemon(FileInfo archivo)
		{
			if(archivo==null)throw new ArgumentNullException();
			
			PathRom=archivo.FullName;
			datosRom=File.ReadAllBytes(archivo.FullName);
			
		}
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
