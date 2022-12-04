pub fn part_one(input: &str) -> Option<u32> {
    let assignment_pairs = map_input_to_models(input);
    let mut duplicate_sections = 0;

    for assignment_pair in assignment_pairs {
        if assignment_pair.0.section_id_start <= assignment_pair.1.section_id_start && assignment_pair.0.section_id_end >= assignment_pair.1.section_id_end {
            duplicate_sections += 1;
        }        
        else if assignment_pair.1.section_id_start <= assignment_pair.0.section_id_start && assignment_pair.1.section_id_end >= assignment_pair.0.section_id_end {
            duplicate_sections += 1;
        }
    }

    return Some(duplicate_sections);
}

pub fn part_two(input: &str) -> Option<u32> {
    let assignment_pairs = map_input_to_models(input);
    let mut overlapping_sections = 0;

    for assignment_pair in assignment_pairs {
        if assignment_pair.0.section_id_start <= assignment_pair.1.section_id_start && assignment_pair.0.section_id_end >= assignment_pair.1.section_id_start {
            overlapping_sections += 1;
        }        
        else if assignment_pair.1.section_id_start <= assignment_pair.0.section_id_start && assignment_pair.1.section_id_end >= assignment_pair.0.section_id_start {
            overlapping_sections += 1;
        }
    }

    return Some(overlapping_sections);
}

fn main() {
    let input = &advent_of_code::read_file("inputs", 4);
    advent_of_code::solve!(1, part_one, input);
    advent_of_code::solve!(2, part_two, input);
}

fn map_input_to_models(input: &str) -> Vec<(Assignment, Assignment)> {
    let mut assignment_pairs: Vec<(Assignment, Assignment)> = Vec::new();
    for line in input.lines() {
        let mut pairs = line.split(',');
        let pair_one = pairs.next().unwrap();
        let mut pair_one_section_ids = pair_one.split('-');
        let pair_one_section_start = pair_one_section_ids.next().unwrap().parse::<u32>().unwrap();
        let pair_one_section_end = pair_one_section_ids.next().unwrap().parse::<u32>().unwrap();
        let pair_two = pairs.next().unwrap();
        let mut pair_two_section_ids = pair_two.split('-');
        let pair_two_section_start = pair_two_section_ids.next().unwrap().parse::<u32>().unwrap();
        let pair_two_section_end = pair_two_section_ids.next().unwrap().parse::<u32>().unwrap();
        assignment_pairs.push((
            Assignment::from(pair_one_section_start, pair_one_section_end), 
            Assignment::from(pair_two_section_start, pair_two_section_end)
        ));
    }

    return assignment_pairs;
}

struct Assignment {
    section_id_start: u32,
    section_id_end: u32
}

impl Assignment {
    fn from(section_id_start: u32, section_id_end: u32) -> Assignment {
        return Assignment { section_id_start, section_id_end }
    }
}

#[cfg(test)]
mod tests {
    use super::*;

    #[test]
    fn test_part_one() {
        let input = advent_of_code::read_file("examples", 4);
        assert_eq!(part_one(&input), None);
    }

    #[test]
    fn test_part_two() {
        let input = advent_of_code::read_file("examples", 4);
        assert_eq!(part_two(&input), None);
    }
}
