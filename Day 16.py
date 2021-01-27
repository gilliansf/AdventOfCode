import numpy as np
import time
from copy import deepcopy

start = time.time()
input = open('input16.txt').read().splitlines()

rawrules = input[:input.index("your ticket:") - 1]
rawme = input[input.index("your ticket:") + 1]
rawtickets = input[input.index("nearby tickets:") + 1:]

def makerules(raw):
    rules = {}
    for line in raw:
        vals = []
        #is there something simpler than a double split, maybe?
        type, ranges = line.split(": ")
        ranges = ranges.split(" or ")
        for range in ranges:
            these = range.split("-")
            vals.append((int(these[0]), int(these[1])))
        rules[type] = vals
    return rules

def maketickets(rawyou, raw):
    ticks = [[int(num) for num in line.split(",")] for line in raw]
    return [int(num) for num in rawyou.split(",")], ticks

def impossible(rules, tickets):
    pmin, pmax = min([val[0][0] for val in rules.values()]), max([val[1][1] for val in rules.values()])
    #pmin/max only works here because of my knowledge of the input file. not perfect.
    #only keep tickets where # of valid possible values == number of total values
    possible = [ticket for ticket in tickets if
                (len(ticket) == sum([1 if (pmin < val < pmax) else 0 for val in ticket]))]
    return possible

def errorrate(rules, tickets):
    pmin, pmax = min([val[0][0] for val in rules.values()]), max([val[1][1] for val in rules.values()])
    error = sum([val for ticket in tickets for val in ticket if not (pmin < val < pmax)])
    return error

#this is ungodly and shouldn't exist. can it not be broken up?
def pullmatches(rules, tickets):
    groups = np.flipud(np.rot90(tickets))  # grouped by placement
    save = groups.copy()
    pairs = {}
    options = list(rules.keys())
    remove = (None, None)
#this could probably be a utility function that returns the new lists??
    while True:
        if remove != (None, None):
            options.remove(remove[0])
            groups = np.delete(groups, remove[1], 0)
        if len(options) == 0:
            break
        for key in options:
            lower, upper = rules[key]
            all = [sum([(lower[0] <= val <= lower[1]) | (upper[0] <= val <= upper[1]) for val in group]) == len(group)
                   for group in groups]
            if sum(all) == 1:
                pairs[key] = groups[all.index(True)]
                remove = (key, all.index(True))
                break
    for key in pairs.keys():
        current = pairs[key]
        for i in range(len(save)):
            if (save[i] == current).all():
                pairs[key] = i
                break
    return pairs

def parttwo(pairs, me):
    departs = [pairs[key] for key in pairs.keys() if "departure" in key]
    total = 1
    for val in [me[ind] for ind in departs]:
        total *= val
    return total


def main():
    rules = makerules(rawrules)
    mine, tickets = maketickets(rawme, rawtickets)
    print("part one solution: ", errorrate(rules, tickets))

    possible = impossible(rules, tickets)
    pairs = pullmatches(rules, possible)
    print("part two solution: ", parttwo(pairs, mine))


main()
print("time: ", time.time()-start)