use std::collections::HashSet;
use std::fs::File;
use std::io::BufRead;
use std::io::BufReader;

fn main() {
    let (mut bingo_boards, numbers) = parse_input();
    let board_count = bingo_boards.len();

    println!("Board Count: {}", board_count);

    let mut winning_boards: HashSet<usize> = HashSet::with_capacity(board_count);

    for number in numbers {
        for (i, board) in bingo_boards.iter_mut().enumerate() {
            board.mark_number(number);

            if board.is_winner {
                winning_boards.insert(i);

                // Final board
                if winning_boards.len() == board_count {
                    let unmarked_sum = board.sum_unmarked_numbers();
                    let final_number = number;
                    println!(
                        "Umarked Sum: {}, Final Number: {}, Part One Answer: {}",
                        unmarked_sum,
                        final_number,
                        unmarked_sum * final_number
                    );
                    return;
                }
            }
        }
    }
}

fn parse_input() -> (Vec<BingoBoard>, Vec<u32>) {
    let mut file = BufReader::new(File::open("input.txt").unwrap());

    // Load numbers
    let mut numbers_string = String::new();
    file.read_line(&mut numbers_string).unwrap();
    let numbers = numbers_string
        .split(",")
        .map(|number| number.trim().parse::<u32>().unwrap())
        .collect();

    // Start parsing bingo boards
    let mut bingo_boards: Vec<BingoBoard> = Vec::new();
    let mut current_board: Vec<Vec<u32>> = Vec::new();
    let mut line_exists = file.read_line(&mut String::new()).unwrap();
    while line_exists != 0 {
        let mut current_line = String::new();
        line_exists = file.read_line(&mut current_line).unwrap();
        current_line = current_line.trim().to_string();
        if current_line.is_empty() {
            bingo_boards.push(BingoBoard::new(current_board));
            current_board = Vec::new();
        } else {
            current_board.push(
                current_line
                    .split_whitespace()
                    .map(|number| number.trim().parse::<u32>().unwrap())
                    .collect(),
            );
        }
    }

    return (bingo_boards, numbers);
}

struct BingoItem {
    value: u32,
    is_checked: bool,
}

impl BingoItem {
    fn new(value: u32) -> BingoItem {
        return BingoItem {
            value: value,
            is_checked: false,
        };
    }
}

struct BingoBoard {
    size: usize,
    board: Vec<Vec<BingoItem>>,
    is_winner: bool,
}

impl BingoBoard {
    fn new(values: Vec<Vec<u32>>) -> BingoBoard {
        let mut actual_board: Vec<Vec<BingoItem>> = Vec::new();
        let mut i = 0;
        let mut j;
        while i < values.len() {
            let mut row = Vec::new();

            j = 0;
            while j < values.len() {
                let value = values.get(i).unwrap().get(j).unwrap();
                row.push(BingoItem::new(*value));

                j += 1;
            }
            actual_board.push(row);

            i += 1;
        }

        // println!("Board!");
        // for row in actual_board.iter() {
        //     for cell in row {
        //         print!("{} ", cell.value);
        //     }

        //     println!();
        // }

        return BingoBoard {
            size: 5,
            board: actual_board,
            is_winner: false,
        };
    }

    fn sum_unmarked_numbers(&self) -> u32 {
        let mut sum = 0;
        for row in self.board.iter() {
            for cell in row {
                if !cell.is_checked {
                    sum += cell.value;
                }
            }
        }

        return sum;
    }

    fn mark_number(&mut self, value: u32) {
        let mut i = 0;
        let mut j;
        while i < self.size {
            j = 0;
            while j < self.size {
                let cell = self.board.get_mut(i).unwrap().get_mut(j).unwrap();
                if cell.value == value {
                    cell.is_checked = true;

                    self.check_winner(i, j);
                    return;
                }

                j += 1;
            }

            i += 1;
        }
    }

    fn check_winner(&mut self, i: usize, j: usize) {
        // Check Row
        let mut row_winner = true;
        for cell in self.board.get(i).unwrap() {
            if !cell.is_checked {
                row_winner = false;
            }
        }

        // Check Column
        let mut column_winner = true;
        for row in self.board.iter() {
            if !row.get(j).unwrap().is_checked {
                column_winner = false;
            }
        }

        self.is_winner = row_winner || column_winner;
    }
}
