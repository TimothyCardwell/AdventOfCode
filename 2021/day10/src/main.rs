use std::fs::File;
use std::io::BufRead;
use std::io::BufReader;

fn main() {
    let lines = parse_input();
    // let mut score = 0;
    // for line in lines {
    //     score += part_two(&line);
    // }
    // println!("Part One Score: {}", score);

    let mut scores: Vec<u64> = Vec::new();
    for line in lines {
        let score = part_two(&line);
        if score != 0 {
            scores.push(score);
        }
    }

    scores.sort();
    println!("{:?}", scores);

    println!("Score: {}", scores.get(scores.len() / 2).unwrap());
}

// fn part_one(line: &str) -> u32 {
//     let open_chars = ['(', '[', '{', '<'];

//     let mut stack: Vec<char> = Vec::new();
//     for character in line.chars() {
//         if open_chars.contains(&character) {
//             stack.push(character)
//         } else {
//             let previous_close = stack.pop().unwrap();
//             if get_index(previous_close) != get_index(character) {
//                 match character {
//                     ')' => return 3,
//                     ']' => return 57,
//                     '}' => return 1197,
//                     '>' => return 25137,
//                     _ => {
//                         continue;
//                     }
//                 }
//             }
//         }
//     }

//     return 0;
// }

fn part_two(line: &str) -> u64 {
    let open_chars = ['(', '[', '{', '<'];

    let mut stack: Vec<char> = Vec::new();
    let mut score = 0;
    for character in line.chars() {
        if open_chars.contains(&character) {
            stack.push(character)
        } else {
            let previous_open = stack.pop().unwrap();
            // Corrupted, we discard
            if get_index(previous_open) != get_index(character) {
                return 0;
            }
        }
    }

    stack.reverse();
    for character in stack {
        score *= 5;
        match character {
            '(' => score += 1,
            '[' => score += 2,
            '{' => score += 3,
            '<' => score += 4,
            _ => {
                panic!();
            }
        }
    }

    return score;
}

fn get_index(character: char) -> u32 {
    if character == '(' || character == ')' {
        return 0;
    } else if character == '[' || character == ']' {
        return 1;
    } else if character == '{' || character == '}' {
        return 2;
    } else if character == '<' || character == '>' {
        return 3;
    } else {
        panic!();
    }
}

fn parse_input() -> Vec<String> {
    let file = BufReader::new(File::open("input.txt").unwrap());
    return file.lines().map(|l| l.unwrap()).collect();
}
