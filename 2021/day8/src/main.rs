use std::fs::File;
use std::io::BufRead;
use std::io::BufReader;

fn main() {
    let entries = parse_input();

    let mut part_one_count = 0;
    for entry in entries {
        let (_, output) = parse_entry(&entry);
        println!("Entry: {}, Output: {:?}", entry, output);
        for digit_string in output {
            match get_int_from_output(digit_string) {
                Some(_) => {
                    part_one_count += 1;
                }
                None => {}
            }
        }
    }

    println!("Part One: {}", part_one_count);
}

fn parse_input() -> Vec<String> {
    let file = BufReader::new(File::open("input.txt").unwrap());
    return file.lines().map(|l| l.unwrap()).collect();
}

fn parse_entry(entry: &str) -> (Vec<String>, Vec<String>) {
    let split: Vec<&str> = entry.split(" | ").collect();

    let mut input: Vec<String> = Vec::new();
    let mut output: Vec<String> = Vec::new();
    for item in split.get(0).unwrap().split(" ").collect::<Vec<&str>>() {
        input.push(item.to_string());
    }

    for item in split.get(1).unwrap().split(" ").collect::<Vec<&str>>() {
        output.push(item.to_string());
    }

    return (input, output);
}

fn get_int_from_output(output: String) -> Option<u32> {
    let output_len = output.len();
    match output_len {
        2 => {
            return Option::from(1);
        }
        3 => {
            return Option::from(7);
        }
        4 => {
            return Option::from(4);
        }
        7 => {
            return Option::from(8);
        }
        5 => {
            // 2: acdef
            // 3: acdfg
            // 5: abdfg
            return Option::None;
        }
        6 => {
            // 0: abcefg
            // 6: acdefg
            // 9: abcdfg
            return Option::None;
        }
        _ => {
            panic!()
        }
    }
}

// fn get_int_from_output_demixed(output: String) -> u32 {
//     let mut chars: Vec<char> = output.chars().collect();
//     chars.sort();

//     let segment = chars
//         .iter()
//         .map(|x| x.to_string())
//         .collect::<Vec<String>>()
//         .join("");

//     match segment.as_ref() {
//         "abcefg" => return 0,
//         "cf" => return 1,
//         "acdef" => return 2,
//         "acdfg" => return 3,
//         "bcdf" => return 4,
//         "abdfg" => return 5,
//         "acdefg" => return 6,
//         "acf" => return 7,
//         "abcdefg" => return 8,
//         "abcdfg" => return 9,
//         _ => {
//             panic!();
//         }
//     }
// }

// 0 -> 6 digits
// 2 -> 5 digits
// 3 -> 5 digits
// 5 -> 5 digits
// 6 -> 6 digits
// 9 -> 6 digits
