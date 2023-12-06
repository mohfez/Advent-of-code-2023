// i thought using js would be a fun challenge on day 4 (for day 5), worst mistake.
const fs = require("fs");

let input = fs.readFileSync("Day5/input.txt", { encoding: "utf-8" }).split("\r\n\r\n").map(part => part.split("\r\n"));
let seeds = input[0][0].match(/\d+/g).map(part => parseInt(part));

input.shift(); // remove "seeds: ..."
for (const line of input) line.shift(); // remove title
input = input.map(part => part.map(nums => nums.split(" ").map(num => parseInt(num)))); // change strings to nums

const PT2 = true;
const PT2_MULTIPLIER = 1000; // depends on input, small input can get away with 1 but bigger inputs should have a bigger multiplier
if (PT2)
{
    for (let i = 0; i < seeds.length; i += 2) seeds[i + 1] = seeds[i] + seeds[i + 1]; // clean up seed ranges

    // faster brute force using estimates
    function bruteForce(add, start)
    {
        let location = start;
        while (true)
        {
            const seed = GetSeed(location);
            for (let i = 0; i < seeds.length; i += 2)
            {
                if (seed >= seeds[i] && seed <= seeds[i + 1])
                {
                    return location;
                }
            }
            
            location += add;
        }
    }

    const firstEstimate = bruteForce(PT2_MULTIPLIER, 1).toString();
    const secondEstimate = bruteForce(PT2_MULTIPLIER * 10, 1).toString();

    let start = 0;
    for (let i = 0; i < firstEstimate.length; i++)
    {
        if (firstEstimate[i] == secondEstimate[i]) start = start * 10 + parseInt(firstEstimate[i]);
        else break;
    }

    start = start * Math.pow(10, firstEstimate.length - start.toString().length);
    console.log(bruteForce(1, start));
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