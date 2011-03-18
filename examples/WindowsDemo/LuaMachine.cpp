
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

#include "LuaMachine.h"

#include <malloc.h>

#include "lualib.h"
#include "lauxlib.h"

#include "LuaHostWindows.h"


void * LuaCallback_Alloc(void *ud, void *ptr, size_t osize, size_t nsize)
{
	if (nsize == 0) 
	{
		free(ptr);
		return NULL;
	}
	else
		return realloc(ptr, nsize);
}

int LuaCallback_Print(lua_State * lvm)
{
	for(int i = 1; i <= lua_gettop(lvm); i++)
	{
		print(lua_tostring(lvm, i));
	}

	return 0;
}


LuaMachine::LuaMachine(void)
	:
		m_lvm(NULL)
{
	m_lvm = lua_newstate(&LuaCallback_Alloc, NULL);

	// Add some of the standard libraries
	luaopen_base(m_lvm);
	luaopen_string(m_lvm);
	luaopen_math(m_lvm);
	luaopen_table(m_lvm);
	luaopen_debug(m_lvm);

	InitialiseThreadTable();

	SetLuaFunction("print", &LuaCallback_Print);

	LoadScript("scripts/main.lua");
	LoadScript("scripts/test.lua");
}

LuaMachine::~LuaMachine(void)
{
}

void LuaMachine::InitialiseThreadTable()
{
	// Create the cache
	lua_newtable(m_lvm);
	int cacheIndex = lua_gettop(m_lvm);

// 	// Assign the metatable
// 	lua_pushvalue(m_lvm, metatableIndex);
// 	lua_setmetatable(m_lvm, cacheIndex);

	// Create an entry for the main thread
	lua_pushthread(m_lvm);
	lua_newtable(m_lvm);
	int mainTableIndex = lua_gettop(m_lvm);

	lua_pushstring(m_lvm, "name");
	lua_pushstring(m_lvm, "<<main>>");
	lua_settable(m_lvm, mainTableIndex);

	lua_pushstring(m_lvm, "threadid");
	lua_pushnumber(m_lvm, 0);
	lua_settable(m_lvm, mainTableIndex);

	lua_pushstring(m_lvm, "state");
	lua_pushnumber(m_lvm, 0);	// Ready
	lua_settable(m_lvm, mainTableIndex);

	// Register the main thread in the ThreadTable
	lua_settable(m_lvm, cacheIndex);

	// Put the ThreadTable into the registry
	lua_pushstring(m_lvm, "ThreadTable");
	lua_pushvalue(m_lvm, cacheIndex);
	lua_settable(m_lvm, LUA_REGISTRYINDEX);

	// Put the ThreadTable into the globals as well
	lua_pushstring(m_lvm, "ThreadTable");
	lua_pushvalue(m_lvm, cacheIndex);
	lua_settable(m_lvm, LUA_GLOBALSINDEX);

	// Pop the cache and metatable from the stack
	lua_pop(m_lvm, 1);
//	lua_pop(m_lvm, 1);
}

void LuaMachine::Step()
{
	lua_pushstring(m_lvm, "step");
	lua_gettable(m_lvm, LUA_GLOBALSINDEX);
	if(lua_pcall(m_lvm, 0, 0, 0) != 0)
		error("step() failed!");
}

void LuaMachine::SetLuaFunction(const char * name, LuaFunctionCallback func)
{
	lua_pushstring(m_lvm, name);
	lua_pushcfunction(m_lvm, func);
	lua_settable(m_lvm, LUA_GLOBALSINDEX);
}

void LuaMachine::LoadScript( const char * name )
{
	if(luaL_dofile(m_lvm, name) != 0)
	{
		error("Fatal error while loading %s: %s\n", name, lua_tostring(m_lvm, -1));
	}
}
