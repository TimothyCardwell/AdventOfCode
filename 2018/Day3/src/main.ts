import LineByLineReader = require("line-by-line");
import { Claim } from "./claim";
import Collections = require('typescript-collections');

/**
 * Note to self, stop using Typescript
 */
function run() {
  processInput();
}

/**
 * Note to self, stop using Typescript
 */
function processInput(): void {
  const claims = new Collections.LinkedList<Claim>();

  const lineByLineReader = new LineByLineReader("input.txt");
  lineByLineReader.on("error", (err) => {
    throw Error(err);
  });

  lineByLineReader.on("line", (line) => {
    claims.add(new Claim(line));
  });

  lineByLineReader.on("end", (line) => {
    processClaims(claims);
  });
}

function processClaims(claims: Collections.LinkedList<Claim>) {
  const santaSuit = generateSantaSuit();
  claims.forEach(c => {
    //console.log(`Left Padding: ${c.leftPadding}\nTop Padding: ${c.topPadding}\nWidth: ${c.width}\nHeight: ${c.height}`);
    for (let i = c.topPadding; i < c.topPadding + c.height; i++) {
      for (let j = c.leftPadding; j < c.leftPadding + c.width; j++) {
        //console.log(i);
        //console.log(j);
        santaSuit[i][j] += 1;
      }
    }
  });

  let overlappedSquareCount = 0;
  for (let i = 0; i < 1000; i++) {
    //process.stdout.write("\n");
    for (let j = 0; j < 1000; j++) {
      //process.stdout.write(`${santaSuit[i][j]}`);
      if (santaSuit[i][j] > 1) {
        overlappedSquareCount++;
      }
    }
  }

  process.stdout.write(`\n${overlappedSquareCount}`);
}

/**
 * Note to self, stop using Typescript
 */
function generateSantaSuit(): number[][] {
  let santaSuit: number[][];
  santaSuit = [];
  for (let i = 0; i < 1000; i++) {
    santaSuit[i] = [];
    for (let j = 0; j < 1000; j++) {
      santaSuit[i][j] = 0;
    }
  }

  return santaSuit;
}

run();
