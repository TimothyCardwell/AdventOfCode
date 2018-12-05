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
 *
 * This LineByLineReader function is dumb, where is C# when I need it?
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

/**
 * Note to self, stop using Typescript
 */
function processClaims(claims: Collections.LinkedList<Claim>) {
  const santaSuit = generateSantaSuit();

  const overlappedClaims: Set<number> = new Set<number>();
  claims.forEach(c => {
    for (let i = c.topPadding; i < c.topPadding + c.height; i++) {
      for (let j = c.leftPadding; j < c.leftPadding + c.width; j++) {

        // Part 1
        //santaSuit[i][j] += 1;

        // Square has never been visited yet, update with the id
        if (santaSuit[i][j] === 0) {
          santaSuit[i][j] = c.id;
        }

        // Square has been visited before, need to add the existing claim
        // to the overlap set and the current claim to the overlap set
        else if (santaSuit[i][j] !== -1) {
          overlappedClaims.add(santaSuit[i][j]);
          overlappedClaims.add(c.id);
          santaSuit[i][j] = -1; // -1 to indicate this square has been visited multiple times
        }

        // Multiple claims target this square, add the current to the overlap set
        else {
          overlappedClaims.add(c.id);
        }
      }
    }
  });

  // Part 1
  /*
  let overlappedSquareCount = 0;
  for (let i = 0; i < 1000; i++) {
    for (let j = 0; j < 1000; j++) {
      if (santaSuit[i][j] > 1) {
        overlappedSquareCount++;
      }
    }
  }
  */

  claims.forEach(c => {
    if (!overlappedClaims.has(c.id)) {
      console.log(c.id);
      return;
    }
  });
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
