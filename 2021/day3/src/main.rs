use std::fs::File;
use std::io::BufRead;
use std::io::BufReader;

fn main() {
    let report = read_puzzle_input();
    let (gamma_rate, epsilon_rate) = part_one(report);

    println!("Gamma Rate: {}, Epsilon Rate: {}", gamma_rate, epsilon_rate);

    let gamma_rate_value = isize::from_str_radix(&gamma_rate, 2).unwrap();
    let epsilon_rate_value = isize::from_str_radix(&epsilon_rate, 2).unwrap();
    println!(
        "Power Consumption: {}",
        gamma_rate_value * epsilon_rate_value
    );
}

fn part_one(report: Vec<Vec<u32>>) -> (String, String) {
    let mut gamma_rate = Vec::new();
    let mut epsilon_rate = Vec::new();

    let mut zero_bit_count = 0;
    let mut one_bit_count = 0;

    let mut i = 0;
    let mut j = 0;

    // Iterate over the 2D array going column by column - count the frequency of 0s and 1s
    // and set the gamma/epsilon rates accordingly
    while i < report.get(0).unwrap().len() {
        while j < report.len() {
            let bit = report.get(j).unwrap().get(i).unwrap();
            if *bit == 1 {
                one_bit_count += 1;
            } else {
                zero_bit_count += 1;
            }
            j += 1;
        }

        if zero_bit_count > one_bit_count {
            gamma_rate.push(0.to_string());
            epsilon_rate.push(1.to_string());
        } else {
            gamma_rate.push(1.to_string());
            epsilon_rate.push(0.to_string());
        }

        zero_bit_count = 0;
        one_bit_count = 0;
        i += 1;
        j = 0;
    }

    return (gamma_rate.join(""), epsilon_rate.join(""));
}

/// Places the puzzle input into a 2D vector (allows us to iterate over columns later in an easier fashion)
fn read_puzzle_input() -> Vec<Vec<u32>> {
    let mut diagnostic_report = Vec::new();

    let file = BufReader::new(File::open("input.txt").unwrap());

    let mut index = 0;
    for line in file.lines() {
        diagnostic_report.push(Vec::new());

        let binary_number: Vec<u32> = line
            .unwrap()
            .chars()
            .map(|num| num.to_digit(2).unwrap())
            .collect();

        for number in binary_number {
            diagnostic_report.get_mut(index).unwrap().push(number)
        }

        index += 1;
    }
    return diagnostic_report;
}
