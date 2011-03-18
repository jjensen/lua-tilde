using System;
using System.IO;
using System.Reflection;
using System.Windows.Forms;
using System.Xml.Serialization;

namespace Scintilla.Legacy.Configuration
{
    public class ConfigurationUtility
    {
        protected Assembly _assembly;

        protected virtual byte[] LoadFile(string filename, ConfigFile parent)
        {
			System.IO.Stream res;
			byte[] buf;
			// check for local file.
			res = OpenFile( filename, parent );
			if( res != null)
			{
				buf = new byte[res.Length];
				res.Read( buf , 0 , buf.Length );
				return buf;
			}
			return null;
		}

        protected virtual Stream OpenFile(string filename, ConfigFile parent)
        {
			System.IO.Stream res;
			// check for local file.
			if( System.IO.File.Exists( filename ) )
			{	
				String fn = filename.Replace( "$$USERDIR$$" , Application.UserAppDataPath );
				res = new System.IO.FileStream( filename , System.IO.FileMode.Open );
			}
			else
			{
				res = _assembly.GetManifestResourceStream(String.Format( "{0}.{1}" , _assembly.GetName().Name, filename.Replace("\\" , "." ) ));
			}
			if( res==null && parent !=null && parent.filename != null )
			{
				// try by prepending the path from the parent to the path.
				int p = parent.filename.LastIndexOf('\\');
				if( p > 0 )
					return OpenFile( String.Format( "{0}\\{1}" , parent.filename.Substring(0 , p) , filename ) , null );
			}

			return res;
		}

        protected object Deserialize(TextReader reader, Type aType)
        {
            XmlSerializer xmlSerializer = null;
            object local = null;
            xmlSerializer = new XmlSerializer(aType);
            local = xmlSerializer.Deserialize(reader);
            reader.Close();
            return local;
        }

        public ConfigurationUtility(Assembly assembly)
        {
            _assembly = assembly;
        }

        public virtual object LoadConfiguration(ConfigFile parent)
        {
            return LoadConfiguration(typeof(Scintilla), "ScintillaNET.xml", parent);
        }

        public virtual object LoadConfiguration(string filename, ConfigFile parent)
        {
            return LoadConfiguration(typeof(Scintilla), filename, parent);
        }

        public virtual object LoadConfiguration(Type configType, ConfigFile parent)
        {
            return LoadConfiguration(configType, "ScintillaNET.xml", parent);
        }

        public virtual object LoadConfiguration(Type configType, string filename, ConfigFile parent)
        {
            ConfigFile configFile = null;
            Stream stream = null;
            TextReader textReader = null;
            configFile = null;
            if (typeof(ConfigFile).IsAssignableFrom(configType))
            {
                stream = OpenFile(filename, parent);
                if (stream != null)
                {
                    textReader = new StreamReader(stream);
                    configFile = Deserialize(textReader, configType) as ConfigFile;
                    configFile.filename = filename;
                    configFile.init(this, parent);
                }
            }
            return configFile;
        }

        public virtual object LoadConfiguration()
        {
            return LoadConfiguration(typeof(Scintilla), "ScintillaNET.xml", null);
        }

        public virtual object LoadConfiguration(string filename)
        {
            return LoadConfiguration(typeof(Scintilla), filename, null);
        }

        public virtual object LoadConfiguration(Type configType)
        {
            return LoadConfiguration( configType , "ScintillaNET.xml", null );
        }

        public virtual object LoadConfiguration(Type configType, string filename)
        {
            return LoadConfiguration( configType , filename, null );
        }
    }

}
