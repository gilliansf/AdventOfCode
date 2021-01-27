import numpy as np

file = "input4.txt"
input = list(open(file, "r"))
#135 is too high

info = []
data = []

must = ["byr", "iyr", "eyr", "hgt", "hcl", "ecl", "pid"]
reqs = {"byr":(1920, 2002), "iyr":(2010,2020), "eyr":(2020,2030),
        "hgt":{"cm":(150, 193), "in":(59, 76)},
        "ecl":("amb", "blu", "brn", "gry", "grn", "hzl", "oth")}

def check_ppt(data):
    info = format(data)
    global must
    for thing in must:
        if thing not in info.keys():
            #print(thing, info)
            return 0
        elif check(thing, info) == 0:
            return 0
        elif check(thing, info) == 1:
            print(thing, info)
    return 1

def num_check(thing, input):
    try:
        input = int(input)
        global reqs
        limits = reqs[thing]
        return [1 if ((input >= limits[0]) and (input <= limits[1])) else 0][0]
    except:
        return 0

def height_check(thing, input):
    if "in" in input: msm = "in"
    if "cm" in input: msm = "cm"
    global reqs
    try:
        input = int(input.strip(msm))
        limits = reqs[thing][msm]
        return [1 if ((input >= limits[0]) and (input <= limits[1])) else 0][0]
    except:
        return 0

def hcl_check(thing, input):
    try:
        if input[0] != "#":
            return 0
        input = input.strip("#")
        return [1 if ((input.isalnum()) and (len(input) == 6)) else 0][0]
    except:
        return 0

#driver function for all checking
def check(thing, data):
    global must, reqs
    if thing in must[0:3]:
        return num_check(thing, data[thing])
    elif thing == must[3]:
        return height_check(thing, data[thing])
    elif thing == must[4]:
        return hcl_check(thing, data[thing])
    elif thing == must[5]:
        print(thing)
        return [1 if data[thing] in reqs[thing] else 0][0]
    else:
        return [1 if ((len(data[thing]) == 9) and (data[thing].isnumeric())) else 0][0]



def format(data):
    outc = {}
    info = ' '.join(data).split(" ")
    pairs = [(thing.split(":")) for thing in info]
    for pair in pairs:
        outc[pair[0]] = pair[1]
    return outc

total = 0
for line in input:
    if line == "\n":
        total += check_ppt(data)
        data = []
    else:
        data.append(line.strip('\n'))
total += check_ppt(data)

print(total)



