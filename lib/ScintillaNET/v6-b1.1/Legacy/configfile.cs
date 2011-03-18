using System;
using System.Collections;
using System.Runtime;
using System.Xml.Serialization;

namespace Scintilla.Legacy.Configuration
{
    [SerializableAttribute()]
    public class ConfigFile : ConfigItem
    {
        [NonSerializedAttribute()]
        protected ConfigFile[] includedFiles;

        [XmlArrayAttribute("includes")]
        [XmlArrayItemAttribute("include")]
        public include[] includes;

        [NonSerializedAttribute()]
        public string filename;


        protected virtual Scintilla ChildScintilla
        {
            get
            {
                return null;
            }
        }

        public virtual Scintilla MasterScintilla
        {
            get
            {
                return (_parent == this) ? ChildScintilla : _parent.MasterScintilla;
            }
        }

        public virtual void addIncludedFile(ConfigFile file)
        {
            ConfigFile[] configFiles = null;
            configFiles = new ConfigFile[includedFiles.Length + 1];
            includedFiles.CopyTo(configFiles, 0);
            configFiles[includedFiles.Length] = file;
            includedFiles = configFiles;
        }

        public override void init(ConfigurationUtility utility, ConfigFile theParent)
        {
            ConfigFile configFile = null;
            includedFiles = new ConfigFile[0];
            base.init(utility, theParent);
            if (includes == null)
            {
                includes = new include[0];
            }
            for (int j = 0; j < includes.Length; j++)
            {
                includes[j].init(utility, _parent);
            }
            for (int i = 0; i < includes.Length; i++)
            {
                configFile = (ConfigFile)utility.LoadConfiguration(base.GetType(), includes[i].file, (ConfigFile)_parent);
                addIncludedFile(configFile);
            }
            CollectScintillaNodes(null);
        }

        public virtual void CollectScintillaNodes(ArrayList list)
        {
			if( _parent == this )
			{
				if( list != null )
					return;
				list = new System.Collections.ArrayList();
				// add my children's includes first!
				if( ChildScintilla != null )
					ChildScintilla.CollectScintillaNodes( list );

			}
			else if( list == null )
				return;

			ConfigFile cf;
			if( includedFiles != null )
				for( int i = 0 ; i< includedFiles.Length ; i++ )
				{
					cf = includedFiles[i];
					// add the included file's scintilla to the list.
					if( cf.ChildScintilla != null )
					{
						list.Add( cf.ChildScintilla );
					}

					// add the child scintilla's children to the list
					if( cf.ChildScintilla != null )
						cf.ChildScintilla.CollectScintillaNodes( list );

					// add the included file's included
					if( cf.includedFiles != null && cf.includedFiles.Length > 0 )
						cf.CollectScintillaNodes(list);

				}

			if( _parent == this )
			{
				ChildScintilla.includedFiles = new ConfigFile [list.Count];
				list.CopyTo( ChildScintilla.includedFiles );
			}
		}
    }

}
