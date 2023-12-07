const fs = require("fs");

const PT2 = true;
const labels = PT2 ? ['A', 'K', 'Q', 'T', '9', '8', '7', '6', '5', '4', '3', '2', 'J'] : ['A', 'K', 'Q', 'J', 'T', '9', '8', '7', '6', '5', '4', '3', '2'];
const input = fs.readFileSync("Day7/input.txt", { encoding: "utf-8" }).split("\r\n").map(part => part.split(" ")).sort(SortCards);

let ans = 0;
for (let i = 0; i < input.length; i++)
{
    const bid = input[i][1];
    ans += bid * (i + 1);
}
console.log(ans);


function SortCards(hand1, hand2)
{
    // sort by type then strength
    const aType = GetHandType(hand1[0]);
    const bType = GetHandType(hand2[0]);
    if (aType < bType) return 1;
    else if (aType > bType) return -1;

    return -StrongerHandIfSameType(hand1[0], hand2[0]);
}

function GetHandType(hand)
{
    // 0 = five of a kind, 1 = four of a kind, 2 = full house ....
    let handLabels = Array(labels.length).fill(0);
    let jokers = 0;
    for (let i = 0; i < hand.length; i++)
    {
        if (hand[i] == 'J' && PT2) jokers++;
        else handLabels[labels.indexOf(hand[i])]++;
    }

    let max = Math.max(...handLabels);
    if (PT2) max += jokers;

    switch (max)
    {
        case 5: return 0;
        case 4: return 1;
        case 3:
            handLabels[handLabels.indexOf(max - jokers)] = -1;
            if (Math.max(...handLabels) == 2) return 2;
            else return 3;
        case 2:
            handLabels[handLabels.indexOf(max - jokers)] = -1;
            if (Math.max(...handLabels) == 2) return 4;
            else return 5;
        default: return 6;
    }
}

// -1 = hand1, 1 = hand2, 0 = both equal
function StrongerHandIfSameType(hand1, hand2)
{
    for (let i = 0; i < hand1.length; i++)
    {
        let ind1 = labels.indexOf(hand1[i]);
        let ind2 = labels.indexOf(hand2[i]);
        if (ind1 < ind2) return -1;
        else if (ind1 > ind2) return 1;
    }

    return 0;
}