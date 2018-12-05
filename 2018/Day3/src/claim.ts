export class Claim {
  id: number;
  leftPadding: number;
  topPadding: number;
  width: number;
  height: number;

  constructor(magicString: string) {
    let magicStringPieces: string[] = magicString.split(" "); // ["#1234", "@", "123,456:", "12x34"]
    this.id = +magicStringPieces[0].substring(1); // Remove #

    let leftAndTopPadding: string = magicStringPieces[2]; // "123,456:"
    let leftAndTopPaddingPieces: string[] = leftAndTopPadding.split(","); // ["123", "456:"]
    this.leftPadding = +leftAndTopPaddingPieces[0];
    this.topPadding = +leftAndTopPaddingPieces[1].substring(0, leftAndTopPaddingPieces[1].length - 1); // Remove colon

    let widthAndHeight = magicStringPieces[3]; // "12x34"
    let widthAndHeightPieces = widthAndHeight.split("x"); // ["12", "34"]
    this.width = +widthAndHeightPieces[0];
    this.height = +widthAndHeightPieces[1];
  }
}
