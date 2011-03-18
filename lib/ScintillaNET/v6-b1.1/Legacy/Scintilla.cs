using System;
using System.Runtime;
using System.Xml.Serialization;

namespace Scintilla.Legacy.Configuration
{
    [SerializableAttribute()]
    public class Scintilla : ConfigFile
    {
        [XmlArrayItemAttribute("value")]
        [XmlArrayAttribute("globals")]
        public Value[] globals;

        [XmlArrayAttribute("style-classes")]
        [XmlArrayItemAttribute("style-class")]
        public StyleClass[] styleclasses;

        [XmlArrayItemAttribute("keyword-class")]
        [XmlArrayAttribute("keyword-classes")]
        public KeywordClass[] keywordclass;

        [XmlArrayItemAttribute("language")]
        [XmlArrayAttribute("languages")]
        public Language[] languages;


        protected override Scintilla ChildScintilla
        {
            get
            {
                return this;
            }
        }


        public Value GetValue(string keyName)
        {
			Value result = null;
			if( MasterScintilla == this )
			{
				// check the children first (from the end)
				for( int i = includedFiles.Length-1 ; i > -1; i-- )
				{
					Scintilla child = (Scintilla)(includedFiles[i]);
					result = child.GetValue(keyName);
					if( result != null )
						return result;
				}
			}
			//other wise just check here.
			for( int i = globals.Length -1 ;  i > -1 ; i-- )
			{
				if( globals[i].name.Equals( keyName ) )
					result = globals[i];
			}
			
			return result;	
		}

        public StyleClass GetStyleClass(string styleName)
        {
			StyleClass result = null;
			if( MasterScintilla == this )
			{
				// check the children first (from the end)
				for( int i = includedFiles.Length-1 ; i > -1; i-- )
				{
					Scintilla child = (Scintilla)(includedFiles[i]);
					result = child.GetStyleClass(styleName);
					if( result != null )
						return result;
				}
			}
			//other wise just check here.
			for( int i = styleclasses.Length -1 ;  i > -1 ; i-- )
			{
				if( styleclasses[i].name.Equals( styleName ) )
					result = styleclasses[i];
			}
			
			return result;	
		}

        public KeywordClass GetKeywordClass(string keywordName)
        {
			KeywordClass result = null;
			if( MasterScintilla == this )
			{
				// check the children first (from the end)
				for( int i = includedFiles.Length-1 ; i > -1; i-- )
				{
					Scintilla child = (Scintilla)(includedFiles[i]);
					result = child.GetKeywordClass(keywordName);
					if( result != null )
						return result;
				}
			}
			//other wise just check here.
			for( int i = keywordclass.Length -1 ;  i > -1 ; i-- )
			{
				if( keywordclass[i].name.Equals( keywordName ) )
					result = keywordclass[i];
			}
			
			return result;	
		}

        public Language GetLanguage(string languageName)
        {
			Language result = null;
			if( MasterScintilla == this )
			{
				// check the children first (from the end)
				for( int i = includedFiles.Length-1 ; i > -1; i-- )
				{
					Scintilla child = (Scintilla)(includedFiles[i]);
					result = child.GetLanguage(languageName);
					if( result != null )
						return result;
				}
			}
			//other wise just check here.
			for( int i = languages.Length -1 ;  i > -1 ; i-- )
			{
				if( languages[i].name.Equals( languageName ) )
					result = languages[i];
			}
			return result;	
		}

        public override void init(ConfigurationUtility utility, ConfigFile theParent)
        {
            base.init(utility, theParent);
            if (languages == null)
            {
                languages = new Language[0];
            }
            if (styleclasses == null)
            {
                styleclasses = new StyleClass[0];
            }
            if (keywordclass == null)
            {
                keywordclass = new KeywordClass[0];
            }
            if (globals == null)
            {
                globals = new Value[0];
            }
            for (int i2 = 0; i2 < languages.Length; i2++)
            {
                languages[i2].init(utility, _parent);
            }
            for (int k = 0; k < styleclasses.Length; k++)
            {
                styleclasses[k].init(utility, _parent);
            }
            for (int j = 0; j < keywordclass.Length; j++)
            {
                keywordclass[j].init(utility, _parent);
            }
            for (int i1 = 0; i1 < globals.Length; i1++)
            {
                globals[i1].init(utility, _parent);
            }
        }
    }

}
