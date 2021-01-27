import numpy as np
import math

file = "input8.txt"
input = list(open(file, "r"))

instructs = []


for line in input:
    value = line.strip("\n")
    instructs.append((value, 0))


def check(line):
    return (line[1] == 0)

def do(line, acc):
    act, val = line[0].split(" ")
    amt = int(val[1:])
    if val[0] == "-":
        amt = amt * (-1)
    if act == "nop":
        return 1, acc
    elif act == "acc":
        acc += amt
        return 1, acc
    else:
        return amt, acc

def run(lines):
    acc = 0
    i = 0
    going = True
    while going:
        try:
            line = lines[i]
            if not check(line):
                return False
            else:
                lines[i] = (lines[i][0], lines[i][1] + 1)
            iDelta, acc = do(line, acc)
            i += iDelta
        except Exception as e:
            print("Reached end of instructions")
            return acc

#acc = 0
#i = 0
going = True

lines = []

for i in range(len(instructs)):
    #print(instructs[i])
    acc = 0
    repl = "error"
    job, amt = instructs[i][0].split(" ")
    if job == "acc":
        continue
    elif job == "nop":
        repl = "jmp"
    elif job == "jmp":
        repl = "nop"
    lines = instructs.copy()
    lines[i] = (repl + " " + amt, lines[i][1])
    #print("altered - ", lines[i])
    result = run(lines)
    if result != False:
        print(result)





