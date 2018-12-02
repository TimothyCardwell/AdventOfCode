export class BoxIdChecksum {
  public Checksum: number;
  private _boxIds: string[];

  constructor(boxIds: string[]) {
    this._boxIds = boxIds;
    this.createChecksum();
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
}
