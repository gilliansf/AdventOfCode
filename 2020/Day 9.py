import numpy as np
import math

file = "input9.txt"
input = list(open(file, "r"))


preamble = 25
previous = []
invalid = -9999

def checkprevious(previous, num):
    for i in range(len(previous)):
        others = previous[i:]
        for j in range(len(others)):
            if (previous[i] + others[j]) == num:
                return True
    return False

for i in range(len(input)):
    num = int(input[i].strip("\n"))
    if (i >= preamble) & (invalid == -9999):
        if not checkprevious(previous[i-preamble:], num):
            invalid = num
    previous.append(num)

print("Invalid: ", invalid)

for i in range(len(previous)):
    for j in range(len(previous)):
        total = sum(previous[i:j])
        if total == invalid:
            encrypt = min(previous[i:j]) + max(previous[i:j])
            print(encrypt)
            exit()
