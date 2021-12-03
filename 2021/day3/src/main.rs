use std::fs::File;
use std::io::BufRead;
use std::io::BufReader;

fn main() {
    let report = read_puzzle_input();
    // let (gamma_rate, epsilon_rate) = part_one(report);

    // println!("Gamma Rate: {}, Epsilon Rate: {}", gamma_rate, epsilon_rate);

    // let gamma_rate_value = isize::from_str_radix(&gamma_rate, 2).unwrap();
    // let epsilon_rate_value = isize::from_str_radix(&epsilon_rate, 2).unwrap();
    // println!(
    //     "Power Consumption: {}",
    //     gamma_rate_value * epsilon_rate_value
    // );

    //println!("Gamma Rate: {}, Epsilon Rate: {}", oxygen_generator_rating, co2_scrubber_rating);

    let part_two_answer = part_two(report);
    println!("Life Support Rating {}", part_two_answer);
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

fn part_two(report: Vec<Vec<u32>>) -> isize {
    let oxygen_generator_rating = get_oxygen_generator_rating(&report);
    let co2_scrubber_rating = get_co2_scrubber_rating(&report);

    let oxygen_generator_rating_value = isize::from_str_radix(&oxygen_generator_rating, 2).unwrap();
    let co2_scrubber_rating_value = isize::from_str_radix(&co2_scrubber_rating, 2).unwrap();

    return oxygen_generator_rating_value * co2_scrubber_rating_value;
}

fn get_oxygen_generator_rating(report: &Vec<Vec<u32>>) -> String {
    let mut oxygen_generator_rating = String::new();

    // Oxygen Generator Rating
    let mut current_list = report.to_vec();
    let mut current_position = 0;
    while current_list.len() > 1 {
        let mut filtered_list_zeros: Vec<Vec<u32>> = Vec::new();
        let mut filtered_list_ones: Vec<Vec<u32>> = Vec::new();
        let mut zero_bit_count = 0;
        let mut one_bit_count = 0;

        let mut i = 0;
        while i < current_list.len() {
            let bit = current_list.get(i).unwrap().get(current_position).unwrap();
            if *bit == 1 {
                one_bit_count += 1;
                filtered_list_ones.push(current_list.get(i).unwrap().to_vec());
            } else {
                zero_bit_count += 1;
                filtered_list_zeros.push(current_list.get(i).unwrap().to_vec());
            }
            i += 1;
        }

        if zero_bit_count > one_bit_count {
            current_list = filtered_list_zeros;
        } else {
            current_list = filtered_list_ones;
        }

        current_position += 1;
    }

    println!("Final List Oxygen: {:?}", current_list.get(0).unwrap());

    // Holy shit why do I have to do this simply to go from vec to string?
    oxygen_generator_rating = current_list
        .get(0)
        .unwrap()
        .to_vec()
        .iter()
        .map(|x| x.to_string())
        .collect::<Vec<String>>()
        .join("");

    return oxygen_generator_rating;
}

fn get_co2_scrubber_rating(report: &Vec<Vec<u32>>) -> String {
    let mut co2_scrubber_rating = String::new();

    // Oxygen Generator Rating
    let mut current_list = report.to_vec();
    let mut current_position = 0;
    while current_list.len() > 1 {
        let mut filtered_list_zeros: Vec<Vec<u32>> = Vec::new();
        let mut filtered_list_ones: Vec<Vec<u32>> = Vec::new();
        let mut zero_bit_count = 0;
        let mut one_bit_count = 0;

        let mut i = 0;
        while i < current_list.len() {
            let bit = current_list.get(i).unwrap().get(current_position).unwrap();
            if *bit == 1 {
                one_bit_count += 1;
                filtered_list_ones.push(current_list.get(i).unwrap().to_vec());
            } else {
                zero_bit_count += 1;
                filtered_list_zeros.push(current_list.get(i).unwrap().to_vec());
            }
            i += 1;
        }

        if zero_bit_count <= one_bit_count {
            current_list = filtered_list_zeros;
        } else {
            current_list = filtered_list_ones;
        }

        current_position += 1; // 282
    }

    println!("Final List CO2: {:?}", current_list.get(0).unwrap());

    // Holy shit why do I have to do this simply to go from vec to string?
    co2_scrubber_rating = current_list
        .get(0)
        .unwrap()
        .to_vec()
        .iter()
        .map(|x| x.to_string())
        .collect::<Vec<String>>()
        .join("");

    return co2_scrubber_rating;
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
