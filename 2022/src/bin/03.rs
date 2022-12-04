use std::collections::HashSet;

pub fn part_one(input: &str) -> Option<u32> {
    let rucksacks = map_input_to_models(input);

    let mut priority_sum = 0;
    for rucksack in rucksacks {
        priority_sum += get_item_priority(&rucksack.shared_item)
    }

    return Some(priority_sum);
}

pub fn part_two(input: &str) -> Option<u32> {
    let rucksacks = map_input_to_models(input);

    
    let mut priority_sum = 0;

    let mut i = 0;
    while i < rucksacks.len() {
        let container_one = &rucksacks[i].items;
        let container_two = &rucksacks[i + 1].items;
        let container_three = &rucksacks[i + 2].items;

        // Rust doesn't allow us to intersect three hashsets
        // The workaround places the results of intersecting the first two in a temporary hashset, and then intersecting
        // the third hashset with the temporary hashset
        let interim_intersection = container_one.intersection(&container_two);
        let mut interim_hashset = HashSet::new();
        for item in interim_intersection {
            interim_hashset.insert(*item);
        }
        let shared_item = interim_hashset.intersection(&container_three).next().unwrap();
        priority_sum += get_item_priority(shared_item);

        i += 3;
    }

    return Some(priority_sum);
}

fn main() {
    let input = &advent_of_code::read_file("inputs", 3);
    advent_of_code::solve!(1, part_one, input);
    advent_of_code::solve!(2, part_two, input);
}

fn map_input_to_models(input: &str) -> Vec<Rucksack> {
    let mut rucksacks: Vec<Rucksack> = Vec::new();
    for line in input.lines() {
        rucksacks.push(Rucksack::from(&line));
    }

    return rucksacks;
}

fn get_item_priority(item: &char) -> u32 {
    let code_point = *item as u32;
    if code_point < 97 {
        return code_point - 38;
    }

    return code_point - 96;
}

struct Rucksack {
    items: HashSet<char>,
    compartment_1: HashSet<char>,
    compartment_2: HashSet<char>,
    shared_item: char
}

impl Rucksack {
    fn from(input: &str) -> Rucksack {
        let (compartment_one_string, compartment_two_string) = input.split_at(input.len() / 2);

        let items = HashSet::from_iter(input.chars());
        let compartment_1 = HashSet::from_iter(compartment_one_string.chars());
        let compartment_2 = HashSet::from_iter(compartment_two_string.chars());

        let mut intersection = compartment_1.intersection(&compartment_2);
        let shared_item = *intersection.next().unwrap();

        return Rucksack {
            items: items,
            compartment_1: compartment_1,
            compartment_2: compartment_2,
            shared_item: shared_item
        };
    }
}

#[cfg(test)]
mod tests {
    use super::*;

    #[test]
    fn test_part_one() {
        let input = advent_of_code::read_file("examples", 3);
        assert_eq!(part_one(&input), None);
    }

    #[test]
    fn test_part_two() {
        let input = advent_of_code::read_file("examples", 3);
        assert_eq!(part_two(&input), None);
    }
}
