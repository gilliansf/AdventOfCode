import numpy as np

file = "input2.txt"
input = list(open(file, "r"))
data = []
#with open(file, "r") as mine:
#    numbers = list(mine)

def splits(thing):
    line = thing.strip('\n')
    rule, pwd = line.split(': ')
    lims, query = rule.split(' ')
    min, max = lims.split('-')
    return int(min), int(max), query, pwd

def rule_one(min, max, query, pwd):
    total = sum([1 if char == query else 0 for char in pwd])
    if (min <= total) and (max >= total):
        return 1
    return 0

def rule_two(one, two, query, pwd):
    if (pwd[one - 1] == query) ^ (pwd[two - 1] == query):
        return 1
    return 0

valid = 0
for thing in input:
    one, two, query, pwd = splits(thing)
    valid += rule_two(one, two, query, pwd)


print(valid)
