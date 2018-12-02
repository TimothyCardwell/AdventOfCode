export class BoxIdChecksum {
  public Checksum: number;
  public PrototypeBoxId: string;
  private _boxIds: string[];

  constructor(boxIds: string[]) {
    this._boxIds = boxIds;

    // Part 1
    this.createChecksum();

    // Part 2
    this.findPrototypeBoxes();
  }

  private createChecksum(): void {
    let twoCount = 0;
    let threeCount = 0;

    this._boxIds.forEach(boxId => {
      let hasTwoCount = false;
      let hasThreeCount = false;

      let characterMap: Map<string, number> = new Map<string, number>();
      boxId.split("").forEach(x => {
        if (characterMap.has(x)) {
          characterMap.set(x, characterMap.get(x) + 1);
        }
        else {
          characterMap.set(x, 1);
        }
      });

      for (let value of characterMap.values()) {
        if (value === 2) hasTwoCount = true;
        else if (value === 3) hasThreeCount = true;
      }

      if (hasTwoCount) twoCount++;
      if (hasThreeCount) threeCount++;
    });

    this.Checksum = twoCount * threeCount;
  }

  private findPrototypeBoxes(): void {
    let matchFound = false;

    // Brute force to beat Tyler
    this._boxIds.forEach(x => {
      if (matchFound) return;

      this._boxIds.forEach(y => {
        if (x === y) return;

        const xChars = x.split("");
        const yChars = y.split("");

        let mismatchedChars = 0;
        let mismatchedIndex = 0;
        for (let i = 0 ; i < xChars.length; i++) {
          if (xChars[i] !== yChars[i]) {
            mismatchedIndex = i;
            mismatchedChars++;
          }

          if (mismatchedChars > 1) {
            return;
          }
        }

        this.PrototypeBoxId = x.slice(0, mismatchedIndex) + x.slice(mismatchedIndex + 1);
        matchFound = true;
      });
    });
  }
}
