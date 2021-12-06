use std::fs::File;
use std::io::BufRead;
use std::io::BufReader;

fn main() {
    let mut all_fish: Vec<u32> = parse_input();

    let mut day = 0;
    while day < 80 {
        let mut new_fish_count = 0;

        // Fish age one day
        for fish in &mut all_fish {
            if *fish == 0 {
                *fish = 6;
                new_fish_count += 1;
            } else {
                *fish -= 1;
            }
        }

        // New fish
        while new_fish_count > 0 {
            all_fish.push(8);
            new_fish_count -= 1;
        }
        day += 1;
    }

    println!("Part One Answer: {}", all_fish.len());
}

fn parse_input() -> Vec<u32> {
    let mut file = BufReader::new(File::open("input.txt").unwrap());
    let mut input = String::new();
    file.read_line(&mut input).unwrap();
    return input
        .split(",")
        .map(|line| line.trim().parse::<u32>().unwrap())
        .collect();
}
