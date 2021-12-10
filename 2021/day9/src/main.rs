use std::fs::File;
use std::io::BufRead;
use std::io::BufReader;

fn main() {
    let input = parse_input();
    let part_one_answer = part_one(input);

    println!("Part One: {}", part_one_answer);
}

fn part_one(matrix: Vec<Vec<u32>>) -> u32 {
    let mut risk_level_sum = 0;

    let mut i = 0;
    while i < matrix.len() {
        let mut j = 0;
        while j < matrix[i].len() {
            let mut is_lowest = true;
            let current = matrix[i][j];

            // Check Top
            if i != 0 && matrix.get(i - 1).is_some() {
                is_lowest = is_lowest && current < matrix[i - 1][j];
            }

            // Check Bottom
            if matrix.get(i + 1).is_some() {
                is_lowest = is_lowest && current < matrix[i + 1][j];
            }
            // Check Left
            if j != 0 && matrix[i].get(j - 1).is_some() {
                is_lowest = is_lowest && current < matrix[i][j - 1];
            }

            // Check Right
            if matrix[i].get(j + 1).is_some() {
                is_lowest = is_lowest && current < matrix[i][j + 1];
            }

            if is_lowest {
                risk_level_sum += current + 1;
            }

            j += 1;
        }

        i += 1;
    }

    return risk_level_sum;
}

fn parse_input() -> Vec<Vec<u32>> {
    let file = BufReader::new(File::open("input.txt").unwrap());
    let vectors: Vec<Vec<u32>> = file
        .lines()
        .map(|l| {
            l.unwrap()
                .chars()
                .map(|c| c.to_digit(10).unwrap())
                .collect()
        })
        .collect();
    return vectors;
}
