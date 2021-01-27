import numpy as np
from itertools import product
from collections import Counter
from copy import deepcopy

input = open('input12.txt').read().splitlines()


moves = ['L', 'R']
compass = {'N':0, 'S':180, 'E':90, 'W':270, 0:'N', 180:'S', 90:'E', 270:'W'}
location = {'N':0, 'S':0, 'E':0, 'W':0}
#turns = {'N':('W', 'E'), 'S':('E', 'W'), 'E':('N', 'S'), 'W':('S', 'N')}

direc = "E"
waypoint = {'N':1, 'S':0, 'E':10, 'W':0}

#is there maybe a simpler way than case by case?
def turn(current, change, amount, compass):
    if change == "R":
        new = compass[current] + amount
    elif change == "L":
        new = compass[current] - amount
    if new < 0:
        new += 360
    elif new >= 360:
        new -= 360
    return compass[new]

def rotate(waypoint, change, amount, compass):
    newpoint = dict()
    moves = ["N", "E", "S", "W"]
    if change == "L":
        moves.reverse()
    newmoves = moves[int(amount / 90):] + moves[:int(amount / 90)]
    for i in range(len(moves)):
        newpoint[newmoves[i]] = waypoint[moves[i]]
    return newpoint



def partone(input, compass, location, moves, direc = "E"):
    for comm in input:
        where, how = comm[0], int(comm[1:])
        if where in moves:
            direc = turn(direc, where, how, compass)
        elif where in compass.keys():
            location[where] += how
        elif where == "F":
            location[direc] += how
    return abs(location["N"] - location["S"]) + abs(location["E"] - location["W"])

def parttwo(input, compass, waypoint, location, moves, direc = "E"):
    print(input)
    for comm in input:
        print(comm)
        where, how = comm[0], int(comm[1:])
        if where in moves:
            waypoint = rotate(waypoint, where, how, compass)
            #this doesn't actually change the direction, it changes
                #the waypoint by that degree
            #direc = turn(direc, where, how, compass)
            #print('new direction, ', direc)
        elif where in compass.keys():
            #move the waypoint by how
            waypoint[where] += how
            print("new waypoint, ", waypoint)
        elif where == "F":
            for key in waypoint.keys():
                location[key] += (how * waypoint[key])
            print("new location, ", location)
    return abs(location["N"] - location["S"]) + abs(location["E"] - location["W"])

#print("Part one solution: ", partone(input, compass, location.copy(), moves))
#print(location)

print(parttwo(input, compass, waypoint.copy(), location.copy(), moves))