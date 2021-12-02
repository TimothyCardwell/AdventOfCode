use std::fs::File;
use std::io::BufRead;
use std::io::BufReader;

struct Submarine {
    horizontal_position: i32,
    depth_position: i32,
}

impl Submarine {
    fn new() -> Submarine {
        return Submarine {
            horizontal_position: 0,
            depth_position: 0,
        };
    }

    fn increase_depth(&mut self, value: i32) {
        self.depth_position += value;
    }

    fn decrease_depth(&mut self, value: i32) {
        self.depth_position -= value;
    }

    fn increase_horizontal(&mut self, value: i32) {
        self.horizontal_position += value;
    }

    fn calculate_part_one(&self) -> i32 {
        return self.depth_position * self.horizontal_position;
    }
}

fn main() {
    let instructions = read_input();
    let mut sub = Submarine::new();

    for instruction in instructions {
        let pieces: Vec<&str> = instruction.split_whitespace().collect();
        let instruction = pieces.get(0).unwrap();
        let value = pieces.get(1).unwrap().parse::<i32>().unwrap();

        if *instruction == "forward" {
            sub.increase_horizontal(value);
        } else if *instruction == "down" {
            sub.increase_depth(value);
        } else if *instruction == "up" {
            sub.decrease_depth(value);
        } else {
            panic!();
        }
    }

    println!("Solution for Part One: {}", sub.calculate_part_one());
}

fn read_input() -> Vec<String> {
    let file = BufReader::new(File::open("input.txt").unwrap());
    return file.lines().map(|line| line.unwrap()).collect();
}
