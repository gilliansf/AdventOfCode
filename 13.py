import numpy as np

input = open('input13.txt').read().splitlines()



def partone(input):
    goal = int(input[0])
    busses = list(filter(lambda x: x != "x", input[1].split(",")))
    waits = [int(x) - (goal % int(x)) for x in busses]
    loc = np.argmin(waits)
    return (int(busses[loc]) * waits[loc])

def parttwo(input):
    sched = input[1].split(",")
    busses = [int(x) for x in list(filter(lambda x: x != "x", sched))]
    #delays = [sched.index(str(bus)) for bus in busses]
    mult = max(busses)
    i = 1
    while True:
        good = 0
        id = (i * mult) - sched.index(str(mult))
        if id % 100000 == 0:
            print(id)
        if (id % busses[0] == 0):
            good += 1
            good += sum([1 if (id % (bus + sched.index(str(bus))) == 0) else 0 for bus in busses[1:] ])
        #for bus in busses[1:]:
        #    if id % (bus + sched.index(str(bus))) == 0:
        #        good += 1
        #    else: break
        if good == len(busses):
            return id
        i += 1

print("Part one solution: ", partone(input))
#print("Part two solution: ", parttwo(input))

#this is using the chinese remainder theorum, sourced
    #https://www.reddit.com/r/adventofcode/comments/kc4njx/2020_day_13_solutions/gfth69h?utm_source=share&utm_medium=web2x&context=3
    
data = open('input13.txt', 'r').read().split('\n')
data = data[1].split(',')
B = [(int(data[k]), k) for k in range(len(data)) if data[k] != 'x']

lcm = 1
time = 0
for i in range(len(B)-1):
	bus_id = B[i+1][0]
	idx = B[i+1][1]
	lcm *= B[i][0]
	while (time + idx) % bus_id != 0:
		time += lcm
print("Part two solution: ", time)