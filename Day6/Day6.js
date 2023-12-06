const fs = require("fs");

let input = fs.readFileSync("Day6/input.txt", { encoding: "utf-8" });

const PT2 = true;
if (PT2)
{
    input = input.split("\r\n").map(part => part.replace(/\s+/g, "")).map(part => part.match(/\d+/g).map(Number));
}
else
{
    input = input.split("\r\n").map(part => part.match(/\d+/g).map(Number));
}

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
    let discriminant = Math.pow(b, 2) - 4*a*c;
    return [ Math.floor((-b + Math.sqrt(discriminant)) / (2*a) + 1), Math.ceil((-b - Math.sqrt(discriminant)) / (2*a) - 1) ];
}