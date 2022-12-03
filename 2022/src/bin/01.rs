pub fn part_one(input: &str) -> Option<u32> {
    let mut elves = map_input_to_models(input);
    elves.sort_by_key(|e| e.calorie_count);
    elves.reverse();
    return Some(elves[0].calorie_count);
}

pub fn part_two(input: &str) -> Option<u32> {
    let mut elves = map_input_to_models(input);
    elves.sort_by_key(|e| e.calorie_count);
    elves.reverse();

    let answer = elves[0].calorie_count + elves[1].calorie_count + elves[2].calorie_count;
    return Some(answer);
}

fn main() {
    let input = &advent_of_code::read_file("inputs", 1);
    advent_of_code::solve!(1, part_one, input);
    advent_of_code::solve!(2, part_two, input);
}

fn map_input_to_models(input: &str) -> Vec<Elf> {
    let mut puzzle_input: Vec<Elf> = Vec::new();

    let mut current_index = 0;
    let mut current_calorie_count = 0;
    for line in input.lines() {
        if line == "" {
            puzzle_input.push(Elf::from(current_index, current_calorie_count));
            current_index += 1;
            current_calorie_count = 0;
        }
        else {
            current_calorie_count += line.parse::<u32>().unwrap();
        }
    }

    return puzzle_input;
}

struct Elf {
    index: u32,
    calorie_count: u32
}

impl Elf {
    fn from(index: u32, calorie_count: u32) -> Elf {
        return Elf {
            index: index,
            calorie_count: calorie_count
        };
    }
}

#[cfg(test)]
mod tests {
    use super::*;

    #[test]
    fn test_part_one() {
        let input = advent_of_code::read_file("examples", 1);
        assert_eq!(part_one(&input), None);
    }

    #[test]
    fn test_part_two() {
        let input = advent_of_code::read_file("examples", 1);
        assert_eq!(part_two(&input), None);
    }
}
