import numpy as np
from itertools import product

input = open('input14.txt').read().splitlines()

def bitlist(mask, val):
    bitvals = [(2 ** (len(mask) - (i + 1))) for i in range(len(mask))]
    new = np.zeros((len(mask), ))
    for i in range(len(bitvals)):
        if val >= bitvals[i]:
            new[i] = 1
            val -= bitvals[i]
    return new, bitvals

def valuemask(mask, val):
    new, bitvals = bitlist(mask, val)
    new = [int(new[i]) if mask[i] == "X" else int(mask[i])
           for i in range(len(mask))]
    masked = sum([bitvals[i] if new[i] == 1 else 0 for i in range(len(new))])
    return masked

#applymask('XXXXXXXXXXXXXXXXXXXXXXXXXXXXX1XXXX0X', 11)

def memorymask(mask, location):
    new, bitvals = bitlist(mask, location)
    new = [str(int(new[i])) if mask[i] == '0' else mask[i] for i in range(len(new))]
    exes = list(product(range(2), repeat = new.count('X')))
    locs = []
    for opt in exes:
        opt = list(opt)
        loc = [new[i] if new[i] != "X" else opt.pop() for i in range(len(new))]
        loc = sum([bitvals[i] if int(loc[i]) == 1 else 0 for i in range(len(new))])
        locs.append(loc)
    return locs

def partone(input):
    mask = ''
    memory = {}
    for line in input:
        struc, val = line.split(" = ")
        if struc == 'mask':
            mask = val
        else:
            memory[struc[4:-1]] = valuemask(mask, int(val))
    return sum(memory.values())

def parttwo(input):
    mask = ''
    memory = {}
    for line in input:
        struc, val = line.split(" = ")
        if struc == 'mask':
            mask = val
        else:
            locs = memorymask(mask, int(struc[4:-1]))
            for loc in locs:
                memory[loc] = int(val)
    return sum(memory.values())

print("part one solution: ", partone(input))
print("part two solution: ", parttwo(input))

