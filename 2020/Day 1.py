import numpy as np

file = "input1.txt"
input = list(open(file, "r"))
numbers = []
#with open(file, "r") as mine:
#    numbers = list(mine)

for thing in input:
    numbers.append(int(thing.strip('\n')))

numbers.sort()
#print(numbers)
#for low in numbers:
#    for high in numbers[::-1]:
#        total = low + high
#        if total == 2020:
#            print(low, high, low*high)
#        elif total < 2020:
#            pass

for i in range(len(numbers)):
    for j in range(len(numbers[i:])):
        jLoc = j + i
        for k in range(len(numbers[j:])):
            kLoc = k + jLoc
            sum = numbers[i] + numbers[jLoc] + numbers[kLoc]
            if sum > 2020:
                break
            elif sum == 2020:
                print(numbers[i], numbers[jLoc], numbers[kLoc], numbers[i] * numbers[jLoc] * numbers[kLoc])