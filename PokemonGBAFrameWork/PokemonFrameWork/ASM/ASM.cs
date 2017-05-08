/*
 * Creado por SharpDevelop.
 * Usuario: pikachu240
 * Fecha: 07/05/2017
 * Hora: 20:22
 * Licencia GNU GPL V3
 * Para cambiar esta plantilla use Herramientas | Opciones | Codificación | Editar Encabezados Estándar
 */
using System;
using System.Diagnostics;
using System.IO;
using Gabriel.Cat.Extension;
namespace PokemonGBAFrameWork
{
	/// <summary>
	/// Description of ASM.
	/// </summary>
	public  class ASM
	{
		public static readonly string RutaThumb=Environment.CurrentDirectory+System.IO.Path.AltDirectorySeparatorChar+"thumb.bat";
		public const string MESNAJEFINCORRECTO="Assembled successfully.";
		
		string asmCode;
		byte[] asmBinary;
		string errorCompilar;

		private ASM(string asmCode,string errorCompilar)
		{
			AsmCode=asmCode;
			ErrorCompilar=errorCompilar;
		}
		private ASM(string asmCode,byte[] asmBin)
		{
			AsmCode=asmCode;
			AsmBinary=asmBin;
		}
		public string AsmCode {
			get {
				return asmCode;
			}
			private set{
				asmCode=value;
			}
		}
		public byte[] AsmBinary {
			get {
				return asmBinary;
			}
			private set{
				asmBinary=value;
			}
		}

		public string ErrorCompilar {
			get {
				return errorCompilar;
			}
			private set {
				errorCompilar = value;
			}
		}
		public static ASM Compilar(string asmCode)
		{
			//llamo al compilador y si no da el mensaje de compilado correctamente lanza una excepcion con el mensaje
			ASM asmResult;
			string pathAsmCode=System.IO.Path.GetTempFileName();
			string pathAsmCompilado=System.IO.Path.GetTempFileName()+".bin";
			string mensajeFinProceso;
			byte[] codigoCompilado;
			System.Diagnostics.Process proceso;
			System.IO.File.AppendAllText(pathAsmCode,asmCode);
			proceso=new Process();
			proceso.StartInfo=new ProcessStartInfo(RutaThumb,pathAsmCode+" "+pathAsmCompilado);
			proceso.StartInfo.RedirectStandardOutput=true;
			proceso.StartInfo.Hide();
			
			proceso.Start();

		    mensajeFinProceso=  proceso.StandardOutput.ReadLine();
		    if(File.Exists(pathAsmCode))
		    		File.Delete(pathAsmCode);
		    if(mensajeFinProceso!=MESNAJEFINCORRECTO)
		    {
		    	asmResult=new ASM(asmCode,mensajeFinProceso);
		    }
		    else{
		    codigoCompilado=File.ReadAllBytes(pathAsmCompilado);
		    File.Delete(pathAsmCompilado);
		    asmResult=new ASM(asmCode, codigoCompilado);
		    }
		    return asmResult;
			
		}
	}
	public class ASMCompilerException:Exception
	{
		public ASMCompilerException(string error):base(error)
		{}
	}
}
