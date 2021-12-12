use std::fs::File;
use std::io::BufRead;
use std::io::BufReader;

fn main() {
    let lines = parse_input();
    let mut score = 0;
    for line in lines {
        score += part_one(&line);
    }

    println!("Score: {}", score);
}

fn part_one(line: &str) -> u32 {
    let open_chars = ['(', '[', '{', '<'];

    let mut stack: Vec<char> = Vec::new();
    for character in line.chars() {
        if open_chars.contains(&character) {
            stack.push(character)
        } else {
            let previous_close = stack.pop().unwrap();
            if get_index(previous_close) != get_index(character) {
                match character {
                    ')' => return 3,
                    ']' => return 57,
                    '}' => return 1197,
                    '>' => return 25137,
                    _ => {
                        continue;
                    }
                }
            }
        }
    }

    return 0;
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
