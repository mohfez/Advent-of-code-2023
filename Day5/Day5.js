// i thought using js would be a fun challenge on day 4 (for day 5), worst mistake.
const fs = require("fs");

let input = fs.readFileSync("Day5/input.txt", { encoding: "utf-8" }).split("\r\n\r\n").map(part => part.split("\r\n"));
let seeds = input[0][0].match(/\d+/g).map(part => parseInt(part));

input.shift(); // remove "seeds: ..."
for (const line of input) line.shift(); // remove title
input = input.map(part => part.map(nums => nums.split(" ").map(num => parseInt(num)))); // change strings to nums

const pt2 = true;
if (pt2)
{
    for (let i = 0; i < seeds.length; i += 2) seeds[i + 1] = seeds[i] + seeds[i + 1]; // clean up seed ranges

    // brute force time :))), takes 10 years so not much
    let location = 1;
    let found = false;
    while (!found)
    {
        const seed = GetSeed(location);
        for (let i = 0; i < seeds.length; i += 2)
        {
            if (seed >= seeds[i] && seed <= seeds[i + 1])
            {
                console.log(location);
                found = true;
            }
        }
        
        location++;
    }
}
else
{
    let lowest = Number.MAX_VALUE;
    for (const seed of seeds)
    {
        const loc = GetLocation(seed);
        if (loc < lowest) lowest = loc;
    }

    console.log(lowest);
}

function GetSeed(location)
{
    let result = location;
    for (let i = input.length - 1; i >= 0; i--)
    {
        for (let k = 0; k < input[i].length; k++)
        {
            const num = GetNumberFrom(...input[i][k], result);
            if (num != Number.MIN_VALUE)
            {
                result = num;
                break;
            }
        }
    }

    return result;
}

function GetLocation(seed)
{
    let result = seed;
    for (let i = 0; i < input.length; i++)
    {
        for (let k = 0; k < input[i].length; k++)
        {
            const num = GetNumberTo(...input[i][k], result);
            if (num != Number.MIN_VALUE)
            {
                result = num;
                break;
            }
        }
    }

    return result;
}

// destination + (seed - source) => location
function GetNumberTo(destination, source, length, seed)
{
    const diff = seed - source;

    if (diff > length || diff < 0) return Number.MIN_VALUE;
    return destination + diff;
}

// source + (location - destination) => seed
function GetNumberFrom(destination, source, length, location)
{
    const diff = location - destination;

    if (diff > length || diff < 0) return Number.MIN_VALUE;
    return source + diff;
}