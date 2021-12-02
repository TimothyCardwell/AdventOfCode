use std::io::BufRead;
use std::io::BufReader;
use std::fs::File;
use std::convert::From;

fn main() {
    //let puzzle_input = get_puzzle_input();
    let puzzle_input = get_puzzle_input_part_two();
    let mut increase_count = 0;

    let mut previous_depth: Option<u32> = Option::None;
    for depth in puzzle_input.iter() {
        match previous_depth {
            Some(x) => {
                if *depth > x { increase_count += 1; }
            }
            None => ()
        }

        previous_depth = Option::from(*depth);
    }

    println!("Increase Count: {}", increase_count);
}

fn get_puzzle_input() -> Result<Vec<u32>, String>
{
    let mut puzzle_input: Vec<u32> = Vec::new();

    let file = File::open("input.txt");
    match file {
        Ok(file) => {
            for line in BufReader::new(file).lines() {
                match line {
                    Ok(line) => {
                        match line.parse::<u32>() {
                            Ok(value) => {
                                puzzle_input.push(value);
                            }
                            Err(err) => {
                                println!("Error parsing value: {}", err);
                                return Err(String::from("Error parsing value!"));
                            }
                        }

                    }
                    Err(err) => {
                        println!("Error reading line: {}", err);
                        return Err(String::from("Error reading line!"));
                    }
                }
            }

            return Ok(puzzle_input);
        }
        Err(err) => {
            println!("Error reading file: {}", err);
            return Err(String::from("Error reading file!"));
        }
    }
}

fn get_puzzle_input_part_two() -> Vec<u32>
{
    let mut puzzle_input: Vec<u32> = Vec::new();

    let file = BufReader::new(File::open("input.txt").unwrap());
    let lines: Vec<u32> = file.lines().map(|line| { line.unwrap().parse::<u32>().unwrap() }).collect();
    //let lines = vec![199, 200, 208, 210, 200, 207, 240, 269, 260, 263];

    let mut index: usize = 0;
    let mut window_counter: u32 = 0;
    let mut window_value: u32 = 0;

    while lines.get(index).is_some() {
        if window_counter == 3 {
            puzzle_input.push(window_value);
            window_counter = 0;
            window_value = 0;
            index -= 2;
        }
        else {
            window_value += lines.get(index).unwrap();
            window_counter += 1;
            index += 1;
        }
    }

    puzzle_input.push(window_value);

    return puzzle_input;
}
