use std::array::IntoIter;
use std::collections::HashMap;
use std::fs::File;
use std::io::BufRead;
use std::io::BufReader;
use std::iter::FromIterator;

fn main() {
    // Use a map, rather than iterate over each fish
    // Key is the day, value is the number of fish 'on that day'
    let mut fish_map = HashMap::<i32, u64>::from_iter(IntoIter::new([
        (0, 0),
        (1, 0),
        (2, 0),
        (3, 0),
        (4, 0),
        (5, 0),
        (6, 0),
        (7, 0),
        (8, 0),
    ]));

    // Populate map with input
    for days_until_spawn in parse_input() {
        let count = fish_map.get_mut(&days_until_spawn).unwrap();
        *count += 1;
    }

    // For each day, we iterate over the 8 days in the map and swap
    // positions (i.e. all fish at day 7 now get moved to day 6, day 6
    // gets moved to day 5, and so on). At the end of the swap loop,
    // We merge the day 6 count with the day 0 count (indicating a
    // restart to their loop), and populate the day 8 count with
    // the day 0 count (indicated new fish)
    let mut day = 0;
    while day < 256 {
        if day % 10 == 0 {
            println!("Day: {}", day);
        }

        let mut index: i32 = 8;
        let mut previous_fish_group = 0;
        while index >= 0 {
            let temp = fish_map.remove(&index);
            fish_map.insert(index, previous_fish_group);
            previous_fish_group = temp.unwrap();

            index -= 1;
        }

        // Previous fish group is the group at day 0 here, indicating
        // each of them have given birth, so we add that many new fish
        // to the map at day 8
        fish_map.insert(8, previous_fish_group);

        // Again, previous fish group is the group at day 0 here, and they need
        // to be placed back into the map, starting over at day 6
        let sixth_day_count = fish_map.get_mut(&6).unwrap();
        *sixth_day_count += previous_fish_group;

        day += 1;
    }

    let mut part_two_answer = 0;
    for fish in fish_map.into_values() {
        part_two_answer += fish;
    }

    println!("Part Two Answer: {}", part_two_answer);
}

fn parse_input() -> Vec<i32> {
    let mut file = BufReader::new(File::open("input.txt").unwrap());
    let mut input = String::new();
    file.read_line(&mut input).unwrap();
    return input
        .split(",")
        .map(|line| line.trim().parse::<i32>().unwrap())
        .collect();
}
