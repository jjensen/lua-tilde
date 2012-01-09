Web page structure
==================
- Overview
  * Overview
  * Latest News
  * Downloads
  * Features
  * Requirements
- Getting Started
- Documentation
- Downloads
- News
- Contact

Overview
========

Overview
--------

Tilde is firstly a visual debugger for the Lua programming language, and 
secondly an integrated development environment for Lua. Tilde can connect to a 
Lua virtual machine hosted by another application on almost any platform. 

Like Lua, the Tilde debugger is intended to be integrated into a host 
application. The Tilde IDE is self-contained, however a debuggable application 
requires additional C++ code to be compiled into it.

Unlike other Lua debuggers, Tilde cannot hook into any program, however having 
low-level access to the Lua virtual machine allows Tilde to provide more 
functionality.

Features
--------

### Lua debugger features

- _Breakpoints_:  These can be set on any line of a source file.
- _Stepping_:  Once stopped, the user can step over the next line, into a function 
   call, or out of the current function. 
- _Stack trace_: When the machine is stopped, the _Call Stack_ window displays the 
   Lua stack frames of the current call. Double clicking on an entry in the list 
   makes it the active context for locals and watches. 
- _Local variables and upvalues_: When the machine is stopped, the local variables, 
   upvalues and varargs of the active stack frame are displayed in the _Locals_ window. 
- _Watches_: Arbitrary Lua expressions can be entered into the _Watch_ window. These 
   are evaluated whenever the machine stops. The expression is evaluated in the context 
   of the active stack frame. 
- _Expanding tables and userdata_: A table appears with a `+` or `-` beside it. Clicking 
   on the `+` expands the table, displaying the key/value pairs inside it. Clicking `-` 
   closes it again.
- _Viewing table and userdata metadata_: Metadata attached to tables or userdata 
   (function environments, metatables and upvalues) can be inspected similarly to 
   key/value pairs.
- _Threads_: When the machine is stopped, the currently active threads are displayed 
   in the _Threads_ window. Newly created threads are displayed with a blue background, 
   and recently deleted threads with a red background.
- _Filtering in variable windows_: In the _Locals_ window and _Watch_ windows, values of 
   type `function` can be filtered out using a toolbar button. Similarly metadata can be 
   shown or hidden.
- _Catching Lua errors_: When a Lua error is triggered, a dialog box displaying the 
   error message is displayed and the debugger stops on the offending line. The 
   locals, stack frame and watches can all be inspected, however upon stepping or 
   running the normal error handling behaviour is resumed. 
- _Script downloading and execution_: The _Pending Downloads_ window 
   (View/Debug/Pending Downloads) displays a list of script files that have been 
   saved or externally modified. Right click on a file and select “Download” to 
   download and execute it on the target. 
- _Lua console_: The user can interactively execute Lua code on the target, similarly 
   to the standalone `lua` interpreter. If the target is stopped then the script 
   executes in the context of the active stack frame (with full access to local 
   variables and environment). If the target is running the script is executed in 
   the global environment.
- _Console snippets_: Snippets entered in the Lua console can be saved and retrieved 
   later. Tilde supports shared network drives, so multiple users can share useful 
   code snippets.

### Editor features

- _Source control integration_:  If the user attempts to edit a read-only file 
   under revision control, Tilde will automatically offer to check it out from Perforce.
- _Find and replace_:  Text can be searched for in the current document using a 
   Find and Replace dialog modelled on Visual Studio. The regular expression search 
   is provided by Scintilla, and [documented here](http://scintilla.sourceforge.net/SciTERegEx.html). 
- _Options_:  The “Tools/Options…” menu command can be used to modify project and user options. 
- _Find file in project_:  A searchable list of all the files in the project can 
   be accessed via the menu “Window/Find File in Project” or the shortcut Shift-Alt-O. 
   Enter a substring in the text box to filter the list; this also accepts the wildcards 
   `*` (match anything) and `?` (match any character). 

Requirements
------------

### Host requirements

The Tilde debugger has been tested on Windows XP Service Pack 3. It requires the 
.NET Framework 2.0 runtime with Service Pack 1. Tilde does not currently work on Mono 
(Linux, MacOS), and there are currently no plans to support it.

### Lua target requirements

The minimum requirements for a Lua target program is that you have access to the 
application source code and there is a full duplex communications channel 
available to the host PC.

Tilde has built-in support for connecting to a target over TCP sockets, however 
it is designed to support an arbitrary transport layer. If your target platform 
does not support sockets you can write your own Tilde plugin and target support 
code to use a different protocol.

The debugger supports Lua coroutines but does not support multiple Lua states. 
If your application hosts multiple Lua machines (created via `lua_newstate()`) you 
will only be able to debug one of them. Multithreaded access to a single Lua 
virtual machine is also unsupported.

### Source build requirements

The C++ code uses the STL libraries extensively, so a certain level of standards 
compliance will be required by the compiler on the target platform. There are 
currently no plans to provide non-templated C++ or C versions of the target 
interface code.

Getting Started
===============

User guide
==========

Installation
------------

Projects
--------

Debugging
---------

Examples
--------

Developer guide
===============

Building Tilde
--------------

Integrating the Lua debugger
----------------------------

### Introduction

The target debugger code is written in C++, so you must be able to re-compile 
the application hosting the Lua machine you are debugging. You will also need to 
configure the debugger and provide hooks between your application and the 
debugger code.

The debugger code is contained in nine source files, two of which are intended 
to be heavily customised to the needs of your platform and application 
(`HostConfig.h` and `HostConfig.cpp`). The other seven should not require 
modifications; if you find yourself needing to change things here please report 
any fixes to the forums. All Tilde code is in the tilde namespace, except for a 
small number of macros which are prefixed with `TILDE_`. 

The source files are:

- `tilde/HostConfig.h`, `tilde/HostConfig.cpp` : 
   Platform and application customisable settings such as memory allocators, typedefs, 
   endianness, assertion macros, buffer sizes.
- `tilde/LuaDebugger.h`, `tilde/LuaDebugger.cpp`: 
   Hooks into the Lua machine to retrieve and modify the Lua state.
- `tilde/LuaDebuggerComms.h`, `tilde/LuaDebuggerComms.cpp`: 
   Manages the connection between the LuaDebugger and Tilde.
- `tilde/LuaDebuggerProtocol.h` : 
   Defines the communications protocol between the target and Tilde.
- `tilde/ReceiveMessageBuffer.h` : 
   Helper class for processing messages received from Tilde.
- `tilde/SendMessageBuffer.h` : 
   Helper class for generating messages to send to Tilde.


Procedure
---------

An overview of the integration procedure is:

1. Add the three Tilde source files and six header files to your project build system.
2. Modify `HostConfig.h` and `HostConfig.cpp` as required.
3. Compile your application. If you need to fix build errors please report these to 
   the project forums.
4. Create a class derived from `tilde::LuaDebuggerHost` and implement all the functions in it.
5. Modify the startup code in your application as follows:
   1. Create an instance of your `tilde::LuaDebuggerHost`-derived class.
   2. Create an instance of `tilde::LuaDebuggerComms` and pass it your lua_State and your host object.
6. Modify the main loop of your application to check for incoming Tilde connections 
   and messages, and forward these to the `tilde::LuaDebuggerComms` object.

### Sample application

A sample Windows application has been provided that demonstrates how to configure the 

### Lua debugger transport

Tilde architecture
------------------

### Overview

Tilde is implemented using a simple plugin architecture, split across a number 
of .NET assemblies. Dependencies between Tilde assemblies are shown below.

![View of assemblies hierarchy](doc/assemblies.png)

The assemblies are:

- _ScintillaNET_: A 3rd party .NET wrapper around the scintilla text control.
- _WinFormsUI_: A 3rd party .NET library for MDI docking interfaces in Windows Forms.
- _Framework_: A core Tilde assembly that contains interfaces and utility classes used by every other assembly.
- _TildeApp_: The main Tilde executable; implements high-level application managers.
- _CorePlugins_: Contains Tilde functionality implemented as plugins, as well as utility classes.
- _LuaDebugger_: A non-core plugin that implements the functionality for debugging Lua programs.
- _LuaSocketConnection_: An assembly that provides TCP socket connectivity for the LuaDebugger assembly.

The Framework assembly contains several interfaces which all plugins must 
implement, in order to be recognised as Tilde plugins. 

### Manager

The single most important object in Tilde is the `Manager` which is accessible to 
plugins via the `IManager` interface. The manager object provides functionality 
for:

- adding menus and toolbars
- setting status messages and progress bars in the main window
- opening, viewing and closing documents
- opening and closing projects
- accessing options, current project, open documents
- sending events when projects, documents and views are opened, saved or closed

The manager is effectively a singleton but there is no global variable to access 
it. Most high-level objects are passed a reference to the manager in their 
constructors.

### Plugins

The absolute minimum required for a Tilde plugin is that it contains a class 
derived from `Tilde.Framework.Controller.IPlugin`. The plugin object is intended 
to be the manager object for the functionality provided by the plugin. 

When Tilde starts it searches the folder containing the Tilde executable for 
valid plugin assemblies. If it finds an assembly containing a class derived from 
`IPlugin` it instantiates an object of that type and calls the `Initialise()` member 
function. Plugins are initialised at startup before any projects are loaded and 
remain live for the lifetime of the Tilde application.

The plugin can connect event listeners to the manager, so it could for example 
listen to `IProject.ProjectOpened` to perform further initialisation once a 
project is loaded.

### Windows

### Documents

### Projects

At any time Tilde either has zero or one project open. Opening a new project can 
only occur after the previous has been closed. The project is represented by an 
instance of the abstract base class `Project`. 

The `Project` class is responsible for loading the project file, represented by an 
instance of the abstract class `ProjectDocument`, derived from `Document`. 
Developers can use their own project file format by implementing custom `Project` 
and `ProjectDocument` classes. A project document can recursively reference other 
project files.

A project mostly consists of a tree of folders and files, much like Visual 
Studio and other IDEs. The project hierarchy is represented by a tree of 
`ProjectItem` nodes. Subclasses of `ProjectItem` are used to represent the root of 
the tree (`RootItem`), each project file (`ProjectDocumentItem`), folders 
(`FolderItem`) and document files (`DocumentItem`).

Tilde places few requirements on the project class; all it must provide is a 
project name and the files referenced by the project. It comes with support for 
the Visual Studio C++ Project file format, however other formats should be easy 
to add.

Additional project-specific information can be stored in `ProjectItem.ProjectTag`. 
For example the `VCProjectDocument` stores the XML DOM object representing the 
folder or file in the source document. When Tilde modifies the project tree the 
specialised `Project` class is responsible for making the change in the source 
document. In this case the project retrieves the DOM nodes from the effected 
`ProjectItems` and modifies the XML accordingly. This way Tilde does not need to 
understand the entire contents of the source XML file, just enough to represent 
the file tree structure.

Note that custom projects can be stored in any file format, not just XML.

When loading a project file Tilde iterates through all the registered `Project` 
classes and calls the static method `Project.CanLoad()` with the file name as 
argument. If the Project can load that file format (based on the file name 
extension, or inspection of the file contents) it should return true.

### Options

Options are maintained by the `OptionsManager` class, an instance of which is held 
by the `Manager`. Plugins can register objects derived from `IOptions` with the 
manager so that they are automatically saved and loaded as required, and 
displayed to the user in the options editor window.

An individual option is a property in an `IOptions`-derived class with appropriate 
attributes. The attributes specify various names and labels, as well as where 
the option will be stored. Options can be stored in one of three locations:

- _Project_: These settings are specific to a particular project and are stored within the project's documents.
- _User_: Settings that are also specific to a project, but are stored in a separate user-specific file.
- _Preferences_: Global settings that are applied to all projects in Tilde, but are user-specific, 
  and are stored in the registry.

Writing a plugin
----------------

### Getting Started

1. Use the Visual Studio "Add New Project" wizard to add a Visual C# Class Library.
2. Modify the new project's properties to set the output directory to be the same as the 
   _TildeApp_ output directory.
3. Add a reference to the Framework assembly.
4. Add a class (for example `MyPlugin`) derived from `IPlugin`.
5. Add a member function public void `Initialise(IManager manager)` 
   _see `LuaPlugin.cs` for an example_.

If you set a breakpoint in your `Initialise()` method you should find it being hit 
when you compile and run. You will also be able to see the plugin listed in the 
Tools/Plugins... window.

### Tool Windows
1. Use the Visual Studio _Add New Item_ wizard to add an _Inherited Form_.
2. In the Inheritance Picker browse to the `Framework.dll` assembly in the `bin` directory.
3. Select the `Tilde.Framework.View.ToolWindow` component.
4. Open the form's _Properties_ sheet.
   1. Modify the attributes in the _Docking_ group as required to customise the docking behaviour.
   2. The _Text_ attribute specifies the label that appears in menus and the panel.
5. Open the form's source code.
   1. Add the `ToolWindowAttribute` class attribute to your class.
   2. Add a constructor that takes a single `IManager` argument; this is the one that is 
      invoked when Tilde initialises your plugin.
   3. If you need to respond to Tilde events (such as projects or documents being opened) 
      you should connect event handlers in the constructor.

When you compile and run you should see your tool window listed in the _View/..._ 
menu. When you select it your window should appear and will dock according to 
the properties you specified earlier.

### Documents 

1. Use the Visual Studio _Add New Item_ wizard to add a `Class`.
2. Derive your class from `Tilde.Framework.Model.Document`.
3. Add the `DocumentClassAttribute` to your class and specify the user-visible document 
   type name, the name of the class used to view the document, and the list of file 
   extensions the document type can represent.
4. Add a constructor that takes an `Imanager` and filename string arguments, and 
   passes these to the `base()` constructor.
5. Implement the three virtual functions `New()`, `Load()` and `Save()`.

When you compile and run you should be able to right-click in the project tree 
and select _Add/New My Document_ where the name is the one you specified in the 
`DocumentClassAttribute`. A new document named `New Document.ext` should be added 
to the project, where the extension is the first one in the array specified in 
the `DocumentClassAttribute`. Your document view should have opened and should be 
displaying the contents of the document.

### Document Views

1. Use the Visual Studio _Add New Item_ wizard to add an `Inherited` Form.
2. In the _Inheritance Picker_ browse to the `Framework.dll` assembly in the `bin` directory.
3. Select the `Tilde.Framework.View.DocumentView` component.
4. Open the form's _Properties sheet_.
   1. Modify the attributes in the _Docking_ group as required to customise the 
      docking behaviour.
   2. The _Text_ property is controlled by the Tilde framework so you do not need to set it.
5. Open the form's source code.
   1. Add a constructor that takes two arguments `IManager` and `Document`, and passes 
      these to the `base()` constructor.
   2. If you need to respond to events or extract information from the document you 
      should cast it to the expected document type and connect event handlers.

### Version control

This functionality is not yet plugin-driven.

3rd Party Libraries
===================

DockPanel Suite v2.2
--------------------
[http://sourceforge.net/projects/dockpanelsuite/](http://sourceforge.net/projects/dockpanelsuite/)

Provides docking and MDI framework, for a Visual Studio look and feel.

The MIT License

Copyright (c) 2007 Weifen Luo <weifenluo@yahoo.com>

Permission is hereby granted, free of charge, to any person obtaining a copy of 
this software and associated documentation files (the "Software"), to deal in 
the Software without restriction, including without limitation the rights to 
use, copy, modify, merge, publish, distribute, sublicense, and/or sell copies of 
the Software, and to permit persons to whom the Software is furnished to do so, 
subject to the following conditions:

The above copyright notice and this permission notice shall be included in all 
copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR 
IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY, FITNESS 
FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR 
COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER 
IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN 
CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.

Scintilla control 
-----------------

[http://scintilla.sourceforge.net/index.html](http://scintilla.sourceforge.net/index.html)

The text editing control; a fully featured embedded code editor.

License for Scintilla and SciTE

Copyright 1998-2003 by Neil Hodgson <neilh@scintilla.org>

All Rights Reserved 

Permission to use, copy, modify, and distribute this software and its 
documentation for any purpose and without fee is hereby granted, 
provided that the above copyright notice appear in all copies and that 
both that copyright notice and this permission notice appear in 
supporting documentation. 

NEIL HODGSON DISCLAIMS ALL WARRANTIES WITH REGARD TO THIS 
SOFTWARE, INCLUDING ALL IMPLIED WARRANTIES OF MERCHANTABILITY 
AND FITNESS, IN NO EVENT SHALL NEIL HODGSON BE LIABLE FOR ANY 
SPECIAL, INDIRECT OR CONSEQUENTIAL DAMAGES OR ANY DAMAGES 
WHATSOEVER RESULTING FROM LOSS OF USE, DATA OR PROFITS, 
WHETHER IN AN ACTION OF CONTRACT, NEGLIGENCE OR OTHER 
TORTIOUS ACTION, ARISING OUT OF OR IN CONNECTION WITH THE USE 
OR PERFORMANCE OF THIS SOFTWARE. 

Scintilla.NET
-------------

[http://www.codeplex.com/ScintillaNET](http://www.codeplex.com/ScintillaNET)

.NET wrapper for the Scintilla text editor (needed because Scintilla is a Win32/GTK native control).

ScintillaNET is based on the Scintilla component by Neil Hodgson.

ScintillaNET is released on this same license.

The ScintillaNET bindings are Copyright 2002-2006 by Garrett Serack <gserack@gmail.com>

All Rights Reserved

Permission to use, copy, modify, and distribute this software and its 
documentation for any purpose and without fee is hereby granted, provided that 
the above copyright notice appear in all copies and that both that copyright 
notice and this permission notice appear in supporting documentation.

GARRETT SERACK AND ALL EMPLOYERS PAST AND PRESENT DISCLAIM ALL WARRANTIES WITH 
REGARD TO THIS SOFTWARE, INCLUDING ALL IMPLIED WARRANTIES OF MERCHANTABILITY AND 
FITNESS, IN NO EVENT SHALL GARRETT SERACK AND ALL EMPLOYERS PAST AND PRESENT BE 
LIABLE FOR ANY SPECIAL, INDIRECT OR CONSEQUENTIAL DAMAGES OR ANY DAMAGES 
WHATSOEVER RESULTING FROM LOSS OF USE, DATA OR PROFITS, WHETHER IN AN ACTION OF 
CONTRACT, NEGLIGENCE OR OTHER TORTIOUS ACTION, ARISING OUT OF OR IN CONNECTION 
WITH THE USE OR PERFORMANCE OF THIS SOFTWARE.
