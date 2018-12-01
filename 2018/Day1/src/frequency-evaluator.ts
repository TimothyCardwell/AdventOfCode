export class FrequencyEvaluator {
  public frequency: number = 0;
  public calibration: number;
  private _frequencyChanges: number[];

  constructor(frequencyChanges: number[]) {
    this._frequencyChanges = frequencyChanges;

    //Part One
    //this.CalculateFrequency();

    // Part Two
    this.CalculateCalibration();
  }

  private CalculateFrequency(): void {
    this._frequencyChanges.forEach(x => {
      // Update the frequency
      this.frequency += x
    });
  }

  private CalculateCalibration(): void {
    const frequencySet: Set<number> = new Set<number>();
    for (let i = 0; i < this._frequencyChanges.length; i++) {

      // Calculate new frequency
      this.frequency += this._frequencyChanges[i];

      // Check if we've seen this frequency before
      if (frequencySet.has(this.frequency)) {
        this.calibration = this.frequency;
        return;
      }

      // Add the new frequency to the set
      frequencySet.add(this.frequency);

      // Ideally this solution uses a circularly linked list, but we live in a
      // Typscript world and this will have to do
      if (i == this._frequencyChanges.length - 1) {
        i = -1 // -1 because the loop will increase it to 0 at the end of this iteration
      }
    }
  }
}
