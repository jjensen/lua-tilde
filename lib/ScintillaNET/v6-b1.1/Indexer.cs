using System;
using System.Collections.Generic;
using System.Text;

namespace Scintilla 
{
    public class Collection<t> where t:struct
    {
        ScintillaControl _instance;
        static int _setter;
        static int _getter;
		public Collection(ScintillaControl instance)
		{
		    _instance = instance;
		}
        internal static void Setup( int setter, int getter )
        {
            _setter = setter;
            _getter = getter;
		}
		public t this[int index]
		{
			get
			{
                return (t)Enum.ToObject(typeof(t), _instance.SendMessageDirect((uint)_getter, index));
			}
			set
			{
                _instance.SendMessageDirect((uint)_setter, index, (int)(value as object));
			}
		}
	}

    public class IntCollection 
    {
        ScintillaControl _instance;
        int _setter;
        int _getter;
        public IntCollection(ScintillaControl instance)
        {
            _instance = instance;
        }
        internal void Setup(int setter, int getter)
        {
            _setter = setter;
            _getter = getter;
        }
        public int this[int index]
        {
            get
            {
                return _instance.SendMessageDirect((uint)_getter, index);
            }
            set
            {
                _instance.SendMessageDirect((uint)_setter, index, value);
            }
        }
    }

    public class ReadOnlyIntCollection
    {
        ScintillaControl _instance;
        int _getter;
        public ReadOnlyIntCollection(ScintillaControl instance)
        {
            _instance = instance;
        }
        internal void Setup(int getter)
        {
            _getter = getter;
        }
        public int this[int index]
        {
            get
            {
                return _instance.SendMessageDirect((uint)_getter, index);
            }
        }
    }
    
    
    /// <summary>
    /// For some properties, Scintilla does not allow us to query the value we set
    /// so, in this case, we cache the value on the .NET side.
    /// </summary>
    public class CachingIntCollection
    {
        ScintillaControl _instance;
        int _setter;
        int _value;
        public CachingIntCollection(ScintillaControl instance)
        {
            _instance = instance;
        }
        internal void Setup(int setter)
        {
            _setter = setter;
        }
        public int this[int index]
        {
            set
            {
                _value = value;
                _instance.SendMessageDirect((uint)_setter, index, value);
            }
            get
            {
                return _value;
            }
        }
    }

    public class StringCollection
    {
        ScintillaControl _instance;
        int _setter;
        int _getter;
        public StringCollection(ScintillaControl instance)
        {
            _instance = instance;
        }
        internal void Setup(int setter, int getter)
        {
            _setter = setter;
            _getter = getter;
        }
        public string this[int index]
        {
            get
            {
                string result;
                _instance.SendMessageDirect((uint)_getter, out result);
                return result;
            }
            set
            {
                _instance.SendMessageDirect((uint)_setter, index, value);
            }
        }
    }

    public class ReadOnlyStringCollection
    {
        ScintillaControl _instance;
        int _getter;
		int _linelen;
        public ReadOnlyStringCollection(ScintillaControl instance)
        {
            _instance = instance;
        }
        internal void Setup(int getter, int linelen)
        {
            _getter = getter;
			_linelen = linelen;
        }
        public string this[int index]
        {
            get
            {
                string result;
				int len = _instance.SendMessageDirect((uint)_linelen, index);
				_instance.SendMessageDirect((uint)_getter, (IntPtr) index, out result, len);
                return result;
            }
        }
    }

    /// <summary>
    /// For some properties, Scintilla does not allow us to query the value we set
    /// so, in this case, we cache the value on the .NET side.
    /// </summary>
    public class CachingStringCollection
    {
        ScintillaControl _instance;
        int _setter;
        string _value;
        public CachingStringCollection(ScintillaControl instance)
        {
            _instance = instance;
        }
        internal void Setup(int setter)
        {
            _setter = setter;
        }
        public string this[int index]
        {
            set
            {
                _value = value;
                _instance.SendMessageDirect((uint)_setter, index, value);
            }
            get
            {
                return _value;
            }
        }
    }

    
}
