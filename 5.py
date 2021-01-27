import numpy as np
import math

file = "input5.txt"
input = list(open(file, "r"))

#128 rows, 0-127
#8 seats, 0-7

#ID = (row * 8) + seat

ids = []

def row(guide):
    possible = np.zeros((128,))
    rows = (0,128)
    for char in guide:
        half = int(len(possible[rows[0]:rows[1]])/2)
        if char == "F":
            rows = (rows[0], rows[1] - half)
        else:
            rows = (rows[0] + half, rows[1])
    if (rows[1] - rows[0]) < 2:
        return rows[0]
    else:
        print("My row didn't converge.")

def seat(guide):
    possible = np.zeros((8,))
    seats = (0,8)
    for char in guide:
        half = int(len(possible[seats[0]:seats[1]])/2)
        if char == "L":
            seats = (seats[0], seats[1] - half)
        else:
            seats = (seats[0] + half, seats[1])
    if (seats[1] - seats[0]) < 2:
        return seats[0]
    else:
        print("My seat didn't converge.")

grid = np.zeros((128,8))

for line in input:
    guide = line.strip("\n")
    this_row = row(guide[:7])
    this_seat = seat(guide[7:])
    grid[this_row][this_seat] = 1
    ids.append((this_row * 8) + this_seat)

def checkseat(row, col, grid, ids):
    myseat = grid[row][col]
    if myseat != 0:
        return -999
    myid = ((row * 8) + col)
    if ((myid + 1) in ids) and ((myid - 1) in ids):
        return 1
    return 0

#688 is too high

print(np.max(ids))
for r in range(len(grid)):
    for c in range(len(grid[0])):
        if grid[r][c] == 0:
            found = checkseat(r, c, grid, ids)
            if found == 1:
                print("My ID: ", ((r * 8) + c))
                break


