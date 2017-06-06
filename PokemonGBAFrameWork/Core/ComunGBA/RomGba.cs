/*
 * Created by SharpDevelop.
 * User: Pikachu240
 * Date: 10/03/2017
 * Time: 7:33
 * 
 * Código bajo licencia GNU
 *
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.IO;
using Gabriel.Cat;
using Gabriel.Cat.Extension;

namespace PokemonGBAFrameWork
{
	/// <summary>
	/// Es la rom cargada en la ram
	/// </summary>
	public class RomGba:ObjectAutoId 
	{
		public const string EXTENSION=".gba";
		
		string path;
		Edicion edicion;
		BloqueBytes romData;
		string nombre;
		
		#region Constructores
		public RomGba(string pathRom):this(new FileInfo(pathRom))
		{

		
		}
		public RomGba(FileInfo romFile)
		{
			
			
			if(romFile==null)
				throw new ArgumentNullException();
			
     		nombre=System.IO.Path.GetFileNameWithoutExtension(romFile.FullName);
			path=romFile.FullName.Substring(0,romFile.FullName.Length - System.IO.Path.GetFileName(romFile.FullName).Length); 
			
			Load();
			LoadEdicion();
			
		}
		private RomGba()
		{}
        #endregion
        #region propiedades
		public Edicion Edicion {
			get {
				return edicion;
			}
		}

		public BloqueBytes Data {
			get {
				return romData;
			}
		}

		public string Nombre {
			get {
				return nombre;
			}
			set{
				
				if(String.IsNullOrEmpty(value))//null or ""
					nombre="Hack "+edicion.NombreCompleto;
				else nombre=value;
			
			}
		}

		public string Path {
			get {
				return path;
			}
			set {
        		if(!Directory.Exists(value))
        			throw new ArgumentException();
				path = value;
			}
		}

        public string FullPath{
        	get{
        		string nombreArchivoConExtension=Nombre+EXTENSION;
        		return System.IO.Path.Combine(path,nombreArchivoConExtension);}
        }
        public byte this[int index]
        {
        	get{return romData[index];}
        	set{romData[index]=value;}
        }
        #endregion
        
        #region Metodos
        /// <summary>
        /// Carga la edicion de los datos en memoria
        /// </summary>
		public void LoadEdicion()
		{
			edicion=Edicion.GetEdicion(this);
		}
		/// <summary>
		/// Pone la edicion en los datos en memoria
		/// </summary>
		public void SaveEdicion()
		{
			Edicion.SetEdicion(this,edicion);
		}
		
		public void Save()
		{
			if(File.Exists(FullPath))
				File.Delete(FullPath);
			Data.Bytes.Save(FullPath);
		}
		public void Load()
		{
			FileStream fs=new FileStream(FullPath,FileMode.Open,FileAccess.Read);
			romData=new BloqueBytes(fs.ReadToEnd());
			fs.Close();
		}
		/// <summary>
		/// Crea una backup de los datos en memoria
		/// </summary>
		/// <returns>Ruta backup</returns>
		public string BackUp()
		{
			string path=Path+"BackUp."+DateTime.Now.Ticks+"."+Nombre+EXTENSION; 
			File.WriteAllBytes(path,Data.Bytes);
			return path;
			
		}
		/// <summary>
		/// Devuelve una copia de la rom
		/// </summary>
		/// <returns></returns>
		public RomGba Clone()
		{
			RomGba rom=new RomGba();
			rom.Path=Path;
			rom.Nombre=Nombre;
			rom.edicion=this.edicion.Clone(); 
			rom.romData=rom.Data.Clon();
			return rom;
		}
		#endregion
		
		#region Overrides
		public override string ToString() 
		{
			return string.Format("[RomGba Nombre={0}]", nombre);
		}

		#endregion
	}
}
