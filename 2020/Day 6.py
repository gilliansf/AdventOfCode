import numpy as np
import math

file = "input6.txt"
input = list(open(file, "r"))

#6852 is too low


def anyone(data):
    info = ''.join(data)
    each = []
    for char in info:
        each.append(char)
    print(each)
    print(np.unique(each))
    return len(np.unique(each))

def everyone(data):
    all = []
    for thing in data:
        this = []
        for char in thing:
            this.append(char)
        all.append(set(this))
    if len(all) == 1:
        return len(all[0])
    else:
        total = len(all[0].intersection(*all[1:]))
    return total



data = []
total = 0


for line in input:
    if line == "\n":
        total += everyone(data)
        data = []
    else:
        data.append(line.strip("\n"))
total += everyone(data)
print(total)