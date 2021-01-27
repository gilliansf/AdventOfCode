import numpy as np
import time
from copy import deepcopy

start = time.time()
input = open('input17.txt').read().splitlines()

space = {0: list(input)}

def touchcounts(space, loc):
    x, y, z = loc
    me = space[z][x][y]
    for i in range(z-1, z+2):
        print(i)


def expand(space, max):
    z, y = max
    for i in range(-z, z + 1): #for each layer in the 3rd dimension
        if i in space.keys() and max[1] > len(space[i]):
            layer = space[i]
            for j in range(len(layer)):
                layer[j] = '.' + layer[j] + '.'
            layer.insert(0, ''.join(['.' for x in range(y)]))
            layer.append(''.join(['.' for x in range(y)]))
            space[i] = layer
        elif i not in space.keys():
            #print("Making a new layer for", i)
            layer = [''.join(['.' for x in range(y)]) for line in range(y)]
            space[i] = layer
    #for key in space.keys():
    #    print("key   ", key)
    #    for lay in space[key]:
    #        print(lay)
    return space


def iterate(space, rounds, maxes):
    print("iterate rounds: ", rounds)
    if rounds == 0:
        print("keys: ", sorted(space.keys()))
        return space
    else:
        maxes = (maxes[0] + 1, [maxes[1] + 2 if rounds < 6 else maxes[1]][0])
        space = expand(space, maxes)
        return iterate(space, rounds - 1, maxes)

maxes = (max(space.keys()), len(space[0]), len(space[0][0]) )
#iterate(space, 6, maxes)

#touchcounts(space, (1, 1, 0))


