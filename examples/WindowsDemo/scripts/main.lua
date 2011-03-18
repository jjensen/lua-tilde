
print("Running main.lua!\n")

function step()
	for thread, info in pairs(ThreadTable) do
		if coroutine.status(thread) ~= "running" then
			local result = coroutine.resume(thread)
			if not result or coroutine.status(thread) ~= "suspended" then
				ThreadTable[thread] = nil
			end
		end
	end
end

function newthread(name, func)
	local thread = coroutine.create(func)
	ThreadTable[thread] = { name = name }
	return thread
end

