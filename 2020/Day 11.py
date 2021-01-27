import numpy as np
from itertools import product
from collections import Counter
from copy import deepcopy

file = "input11.txt"
input = [[char for char in line.strip("\n")] for line in list(open(file, "r"))]


#[print(line) for line in input]

def getseat(grid, row, col, direction = False):
    #print(row, col, "inside getseat")
    if (0 <= row < len(grid)) and (0 <= col < len(grid[0])):
        return grid[row][col]
    elif direction != False:
        return "999"
    else:
        return "." #if off grid, it's the floor

def nearbyseats(grid, row, col):
    a = [-1,0,1]
    seats = [getseat(grid,row + pair[0],col + pair[1]) for pair in product(a,a)]
    seats.remove(grid[row][col])
    counts = Counter(seats)
    return counts["#"], counts["L"] #occupied, unoccupied


def diagonalseats(grid, row, col):
    directs = list(product([-1,0,1], [-1,0,1]))
    directs.remove((0,0))
    seats = []
    for direct in directs:
        for i in range(1, 999):
            seat = getseat(grid, row + (i * direct[0]), col + (i * direct[1]), True)
            if (seat == "L") or (seat == "#"):
                seats.append(seat)
                break
            elif seat == "999":
                break
    counts = Counter(seats)
    return counts["#"], counts["L"]

def changer(grid, row, col):
    counts = diagonalseats(grid, row, col)
    mine = grid[row][col]
    if (mine == "L") and (counts[0] == 0):
        return "#"
    elif (mine == "#") and (counts[0] >= 5):
        return "L"
    else:
        return mine

def cycle(input, count):
    if count > 250:
        return -999
    after = deepcopy(input)
    for i in range(len(input)):
        for j in range(len(input[0])):
            if input[i][j] != ".":
                after[i][j] = changer(input, i , j)
    if input == after:
        final = ("".join(np.hstack(input)))
        return Counter(final)["#"]
    else:
        return cycle(after, count + 1)

print(cycle(input, 1))