import numpy as np
import time

start = time.time()

input = open('input15.txt').read().split(',')
data = [int(num) for num in input]

locs = {}
last = "X"
turn = 0

def updater(turn, num, guide):
    first = False
    if num in guide.keys():
        exist = guide[num]
        exist.append(turn)
    else:
        first = True
        exist = [turn]
    guide[num] = exist
    return guide, first

for num in data:
    turn += 1
    last = num
    locs, first = updater(turn, num, locs)


for i in range(30000000 - len(input)):
    turn += 1
    #if turn % 500000 == 0:
    #    print(turn)
    if not first:
        indexes = locs[num]
        prev = indexes[-1]
        before = indexes[-2]
        num = prev - before
    else:
        num = 0
    locs, first = updater(turn, int(num), locs)
    last = num


print(last)
print("time: ", time.time()-start)