use std::fs::File;
use std::io::BufRead;
use std::io::BufReader;

fn main() {
    let starting_positions = parse_input();

    let mut min_cost: Option<u32> = Option::None;
    let mut current_cost: Option<u32> = Option::None;
    let mut position = 0;

    // This algo is trash, but it suprisingly works. We'll see how part 2 goes...
    // Basically, starting at 0, just calculate the cost until the cost of the previous position
    // (min_cost) is less than the cost of the next position (current_cost). Due to the nature
    // of this problem, this should always work
    while min_cost.is_none() || min_cost.unwrap() >= current_cost.unwrap() {
        let mut cost: u32 = 0;
        for starting_position in &starting_positions {
            let negative_number: i32 = starting_position - position;
            cost += negative_number.abs() as u32;
        }

        match min_cost {
            Some(x) => {
                if x > cost {
                    min_cost = Option::from(cost)
                }
            }
            None => min_cost = Option::from(cost),
        }
        println!("Min Position: {}, Cost: {}", min_cost.unwrap(), cost);

        position += 1;
        current_cost = Option::from(cost);
    }

    println!("Position: {}, Min Cost: {}", position, min_cost.unwrap());
}

fn parse_input() -> Vec<i32> {
    let mut file = BufReader::new(File::open("input.txt").unwrap());

    let mut input = String::new();
    file.read_line(&mut input).unwrap();
    return input
        .split(",")
        .map(|x| x.trim().parse::<i32>().unwrap())
        .collect();
}
