const fs = require("fs");

const PT2 = false;
let input = fs.readFileSync("Day6/input.txt", { encoding: "utf-8" }).split("\r\n").map(part => PT2 ? part.replace(/\s+/g, "").match(/\d+/g).map(Number) : part.match(/\d+/g).map(Number));

let ans = 1;
for (let i = 0; i < input[0].length; i++)
{
    const time = input[0][i];
    const distance = input[1][i];

    let ranges = FindRange(time, distance);
    ans *= (ranges[1] - ranges[0] + 1);
}

console.log(ans);


function FindRange(time, distance)
{
    // (time - heldTime) * heldTime > distance
    // -heldTime^2 + heldTime*time - distance > 0

    let a = -1, b = time, c = -distance;
    let sqrtDiscriminant = Math.sqrt(Math.pow(b, 2) - 4*a*c);
    return [ Math.floor((-b + sqrtDiscriminant) / (2*a) + 1), Math.ceil((-b - sqrtDiscriminant) / (2*a) - 1) ];
}