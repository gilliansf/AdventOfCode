import numpy as np

file = "input4.txt"
input = list(open(file, "r"))

map = []

for line in input:
    map.append(line.strip('\n'))


#right 3, down 1
reset = len(map[0])

results = []
pairs = [(1,1), (3,1), (5,1), (7,1), (1,2)]

for right, down in pairs:
    trees = 0
    y = 0
    for x in range(len(map)):
        if (x % down) == 0:
            #if not skipping line, then check for tree
            if y >= reset:
                y -= reset
            if map[x][y] == "#":
                trees += 1
            y += right
    results.append(trees)

total = 1
for result in results:
    total = total * result

print(total)


#trees = 0
#y = 0    #(x, y), but x is negative
#for x in range(len(map)):
#    if y >= reset:
#        y -= reset
#    if map[x][y] == "#":
#        trees += 1
    #print(map[x], x, y, map[x][y])
#    y += 3

#print(trees)

