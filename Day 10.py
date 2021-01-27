import numpy as np
import math

file = "input10.txt"
input = list(open(file, "r"))

adapters = [int(line.strip("\n")) for line in input]
adapters.sort()

deviceJolt = max(adapters) + 3

print("Device jolt:", deviceJolt)
counts = [None,0,0,0]
memo = []

currentJolt = 0

#for line in adapters:
#    diff = line - currentJolt
#    counts[diff] += 1
#    currentJolt = line
    #print(line, diff)

#diff = deviceJolt - currentJolt
#counts[diff] += 1

def check(adapters):
    current = 0
    global deviceJolt
    for line in adapters:
        diff = line - current
        if diff > 3:
            return 0
        current = line
    diff = deviceJolt - current
    if diff > 3:
        return 0
    global memo
    memo.append(adapters)
    return 1


def checkRemove(adapters):
    if (1 not in adapters) and (2 not in adapters) and (3 not in adapters):
        return
    global memo
    result = check(adapters)
    if (result == 1) and (adapters not in memo):
        memo.append(adapters)
#removing one number at a time
    for i in range(len(adapters)):
        checking = adapters.copy()
        checking.pop(i)
        result = check(checking)
        if result == 1:
            there = False
            for item in memo:
                if item == checking:
                    there = True
                    break
            if not there:
                memo.append(checking)
            checkRemove(checking)


def subsetsUtil(A, subset, index):
    if (1 not in A) and (2 not in A) and (3 not in A):
        return
    check(subset)
    for i in range(index, len(A)):
        subset.append(A[i])
        subsetsUtil(A, subset, i + 1)
        subset.pop(-1)
    return

def subsets(A):
    global res
    subset = []
    # keeps track of current element in vector A
    index = 0
    subsetsUtil(A, subset, index)



#checkRemove(adapters)
#memo = set(memo)
subsets(adapters)
print(len(memo))
#for line in memo: print(line)

