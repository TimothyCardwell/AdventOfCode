use std::io::BufRead;
use std::io::BufReader;
use std::fs::File;
use std::convert::From;

fn main() {
    let puzzle_input = get_puzzle_input();
    match puzzle_input {
        Err(_err) => { println!("A lot of matching going on in Rust..."); }
        Ok(puzzle_input) => {
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

            println!("Increase Count: {}", increase_count)
        }
    }
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
