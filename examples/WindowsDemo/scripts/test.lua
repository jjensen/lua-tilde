
function spawn_threads()
	newthread("Hello", function() 
        for count = 1, 10 do
            print("hello\n")
            coroutine.yield() 
        end
    end)

	newthread("doSomething", function() 
        for count = 1, 10 do
            doSomething(1, 2, count)
            coroutine.yield() 
        end
    end)

	newthread("throwError", function() 
        for count = 1, 10 do
            coroutine.yield() 
        end
		error("oops")
    end)
end

local anUpvalue = 20

function doSomething(...)
	print(string.format("anUpvalue = %d\n", anUpvalue))
end

function add(x, y)
	return x + y
end
