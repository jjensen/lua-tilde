
/****************************************************************************

Tilde

Copyright (c) 2008 Tantalus Media Pty

Permission is hereby granted, free of charge, to any person obtaining a copy
of this software and associated documentation files (the "Software"), to deal
in the Software without restriction, including without limitation the rights
to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
copies of the Software, and to permit persons to whom the Software is
furnished to do so, subject to the following conditions:

The above copyright notice and this permission notice shall be included in
all copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
THE SOFTWARE.

****************************************************************************/

using System;
using System.Collections.Generic;
using System.Text;
using System.Reflection;
using Tilde.Framework.Model;
using System.Drawing;

namespace Tilde.Framework.Controller
{
	public class EnumParser<T>
	{
		public static T Parse(string value, T defaultValue)
		{
			try
			{
				return (T) Enum.Parse(typeof(T), value);
			}
			catch (Exception)
			{
				return defaultValue;
			}
		}
	}

	public enum OptionLocation
	{
		Project,
		User,
		Registry
	}

	public class OptionsManager
	{
		private IManager mManager;
		private ListCollection<IOptions> mOptions;
		private SortedDictionary<OptionLocation, IOptionsDatabase> mDatabases;

		public OptionsManager(IManager manager)
		{
			mManager = manager;
			mManager.ProjectOpened += new ProjectOpenedEventHandler(Manager_ProjectOpened);
			mManager.ProjectClosed += new ProjectClosedEventHandler(Manager_ProjectClosed);

			mOptions = new ListCollection<IOptions>();
			mOptions.ItemAdded += new ListCollection<IOptions>.ItemAddedDelegate(Options_ItemAdded);

			mDatabases = new SortedDictionary<OptionLocation, IOptionsDatabase>();
			mDatabases[OptionLocation.Project] = null ;
			mDatabases[OptionLocation.User] = null;
			mDatabases[OptionLocation.Registry] = new RegistryOptionsDatabase();
		}

		/// <summary>
		/// These IOptions objects are managed by the OptionsManager. When added to the collection,
		/// the options are loaded and then stored back again (so a new option has the default value
		/// saved off).
		/// </summary>
		public ListCollection<IOptions> Options
		{
			get { return mOptions; }
		}

		public IOptionsDatabase RegistryDatabase
		{
			get { return mDatabases[OptionLocation.Registry]; }
		}

		public IOptionsDatabase ProjectDatabase
		{
			get { return mDatabases[OptionLocation.Project]; }
		}

		public IOptionsDatabase UserDatabase
		{
			get { return mDatabases[OptionLocation.User]; }
		}

		void Options_ItemAdded(ListCollection<IOptions> sender, IOptions item)
		{
			Load(item);
			Store(item);
		}

		public void Load(IOptions options)
		{
			foreach (PropertyInfo property in options.GetType().GetProperties())
			{
				object[] attrs = property.GetCustomAttributes(typeof(OptionAttribute), true);
				if(attrs.Length == 1)
				{
					OptionAttribute attr = (OptionAttribute) attrs[0];
					IOptionsDatabase database = mDatabases[attr.Location];
					object value;

					if (database == null)
					{
						value = attr.DefaultValue;
					}
					else
					{
						if (typeof(String).IsAssignableFrom(property.PropertyType))
							value = database.GetStringOption(attr.Path, (string)attr.DefaultValue);
						else if (typeof(Boolean).IsAssignableFrom(property.PropertyType))
							value = database.GetBooleanOption(attr.Path, (bool)attr.DefaultValue);
						else if (typeof(Enum).IsAssignableFrom(property.PropertyType))
						{
							try
							{
								value = Enum.Parse(property.PropertyType, database.GetStringOption(attr.Path, ""));
							}
							catch (Exception)
							{
								value = attr.DefaultValue;
							}
						}
						else if (typeof(Int32).IsAssignableFrom(property.PropertyType))
							value = database.GetIntegerOption(attr.Path, (int)attr.DefaultValue);
						else if (typeof(Color).IsAssignableFrom(property.PropertyType))
						{
							try
							{
								string name = database.GetStringOption(attr.Path, (string)attr.DefaultValue);
								if (name.Contains(","))
								{
									string[] components = name.Split(new char[] { ',' });
									value = Color.FromArgb(Int32.Parse(components[0]), Int32.Parse(components[1]), Int32.Parse(components[2]));
								}
								else
									value = Color.FromName(name);
							}
							catch (System.Exception)
							{
								value = attr.DefaultValue;
							}
						}
						else if (typeof(string[]).IsAssignableFrom(property.PropertyType))
							value = database.GetStringArrayOption(attr.Path, (string[])attr.DefaultValue);

						else
							throw new ApplicationException("Don't know how to load option of type: " + property.PropertyType.ToString());
					}

					property.SetValue(options, value, null);
				}
			}
		}

		public void Store(IOptions options)
		{
			foreach (PropertyInfo property in options.GetType().GetProperties())
			{
				object[] attrs = property.GetCustomAttributes(typeof(OptionAttribute), true);
				if (attrs.Length == 1)
				{
					StoreOption((OptionAttribute)attrs[0], property, options, false);
				}
			}
		}

		private bool StoreOption(OptionAttribute attr, PropertyInfo property, IOptions options, bool validateOnly)
		{
			IOptionsDatabase database = mDatabases[attr.Location];

			bool success;

			if (database == null)
				success = false;
			else if (typeof(String).IsAssignableFrom(property.PropertyType))
				success = database.SetStringOption(attr.Path, (string)property.GetValue(options, null), validateOnly);
			else if (typeof(bool).IsAssignableFrom(property.PropertyType))
				success = database.SetBooleanOption(attr.Path, (bool)property.GetValue(options, null), validateOnly);
			else if (typeof(Int32).IsAssignableFrom(property.PropertyType))
				success = database.SetIntegerOption(attr.Path, (int)property.GetValue(options, null), validateOnly);
			else if (typeof(Enum).IsAssignableFrom(property.PropertyType))
				success = database.SetStringOption(attr.Path, property.GetValue(options, null).ToString(), validateOnly);
			else if (typeof(string[]).IsAssignableFrom(property.PropertyType))
				success = database.SetStringArrayOption(attr.Path, (string[])property.GetValue(options, null), validateOnly);
			else if (typeof(Color).IsAssignableFrom(property.PropertyType))
			{
				Color color = (Color)property.GetValue(options, null);
				KnownColor knownColor = color.ToKnownColor();
				if (knownColor != 0)
					success = database.SetStringOption(attr.Path, knownColor.ToString(), validateOnly);
				else
					success = database.SetStringOption(attr.Path, String.Join(",", new string[] { color.R.ToString(), color.G.ToString(), color.B.ToString() }), validateOnly);
			}
			else
				success = database.SetStringOption(attr.Path, property.GetValue(options, null).ToString(), validateOnly);

			return success;
		}

		public bool Copy(IOptions from, IOptions to, bool store)
		{
			System.Diagnostics.Debug.Assert(to.GetType().IsAssignableFrom(from.GetType()));
			foreach (PropertyInfo property in from.GetType().GetProperties())
			{
				object[] attrs = property.GetCustomAttributes(typeof(OptionAttribute), true);
				if (attrs.Length == 1)
				{
					OptionAttribute attr = (OptionAttribute)attrs[0];

					if (store && !StoreOption(attr, property, from, false))
						return false;

					object value = property.GetValue(from, null);
					property.SetValue(to, value, null);
				}
			}
			return true;
		}

		public bool Validate(IOptions from, IOptions to)
		{
			System.Diagnostics.Debug.Assert(to.GetType().IsAssignableFrom(from.GetType()));
			foreach (PropertyInfo property in from.GetType().GetProperties())
			{
				object[] attrs = property.GetCustomAttributes(typeof(OptionAttribute), true);
				if (attrs.Length == 1)
				{
					OptionAttribute attr = (OptionAttribute)attrs[0];

					if(!StoreOption(attr, property, from, true))
						return false;
				}
			}
			return true;
		}

		void Manager_ProjectOpened(IManager sender, Project project)
		{
			mDatabases[OptionLocation.Project] = project.ProjectOptions;
			mDatabases[OptionLocation.User] = project.UserOptions;

			foreach (IOptions options in mOptions)
			{
				Load(options);
				Store(options);
			}

			project.ProjectReloaded += new ProjectReloadedHandler(Project_ProjectReloaded);
		}

		void Manager_ProjectClosed(IManager sender)
		{
			mDatabases[OptionLocation.Project] = null;
			mDatabases[OptionLocation.User] = null;
		}

		void Project_ProjectReloaded(Project sender, Tilde.Framework.Model.ProjectHierarchy.ProjectDocumentItem reloadedItem)
		{
			mDatabases[OptionLocation.Project] = sender.ProjectOptions;
			mDatabases[OptionLocation.User] = sender.UserOptions;

			foreach (IOptions options in mOptions)
				Load(options);			
		}

	}
}
