import numpy as np
import math

file = "input7.txt"
input = list(open(file, "r"))


colors = {}
outers = {}

def interior(value):
    contains = {}
    all = value.strip(".").split(", ")
    for each in all:
        this = each.split(" ")
        num = this[0]
        color = " ".join(this[1:3])
        contains[color] = int(num)
    return contains

def exterior(one, two):
    global outers
    current = []
    if one in outers.keys():
        current = outers[one]
    current.append(two)
    outers[one] = current

for line in input:
    rule = line.strip('\n')
    words = rule.split(" ")
    key = " ".join(words[0:2])
    value = " ".join(words[4:])
    if "no other bags" in value:
        pass
    else:
        colors[key] = interior(value)
        for each in colors[key]:
            exterior(each, key)
    #print(key)


def checkOut(color):
    global outers
    final = []
    try:
        wrappings = [checkOut(wrap) for wrap in outers[color]]
        for find in wrappings:
            if type(find) == type(list()):
                for thing in find:
                    final.append(thing)
            else:
                final.append(find)
        final.append(color)
        return final
    except:
        return color

def checkIn(color):
    print("checking for ", color)
    try:
        total = 0
        global colors
        final = []
        current = colors[color]
        print(color + " and they have these subsets ",current)
        for bag in current:
            for i in range(current[bag]):
                total += checkIn(bag)
        print("returning ", total)
        return total + 1

    except Exception as e:
        print(str(e) + " doesn't have a subset")
        return 1

#result = set(np.hstack(checkOut("shiny gold")))
#result.remove("shiny gold")
print(colors)
print(checkIn("shiny gold"))


