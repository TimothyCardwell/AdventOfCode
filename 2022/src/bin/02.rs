pub fn part_one(input: &str) -> Option<u32> {
    let rounds = map_input_to_models_part_one(&input);

    let mut total_score = 0;
    for round in rounds
    {
        let round_result = play_round(&round.1, &round.0);
        let round_score = get_points_for_round_result(round_result) + get_points_for_shape(&round.1);
        total_score += round_score;
    }

    return Some(total_score);
}

pub fn part_two(input: &str) -> Option<u32> {
    let rounds = map_input_to_models_part_two(&input);

    let mut total_score = 0;
    for round in rounds
    {
        let player_shape = get_player_shape_for_round_result(&round.0, &round.1);
        let round_score = get_points_for_round_result(round.1) + get_points_for_shape(&player_shape);
        total_score += round_score;
    }

    return Some(total_score);
}

fn main() {
    let input = &advent_of_code::read_file("inputs", 2);
    advent_of_code::solve!(1, part_one, input);
    advent_of_code::solve!(2, part_two, input);
}

fn map_input_to_models_part_one(input: &str) -> Vec<(Shape, Shape)> {
    let mut round_results: Vec<(Shape, Shape)> = Vec::new();
    for line in input.lines() {
        let mut iter = line.split_whitespace();
        let opponent_shape = map_letter_to_shape(iter.next().unwrap());
        let player_shape = map_letter_to_shape(iter.next().unwrap());
        round_results.push((opponent_shape, player_shape));
    }

    return round_results;
}


fn map_input_to_models_part_two(input: &str) -> Vec<(Shape, RoundResult)> {
    let mut round_results: Vec<(Shape, RoundResult)> = Vec::new();
    for line in input.lines() {
        let mut iter = line.split_whitespace();
        let opponent_shape = map_letter_to_shape(iter.next().unwrap());
        let round_result = map_letter_to_round_result(iter.next().unwrap());
        round_results.push((opponent_shape, round_result));
    }

    return round_results;
}

fn map_letter_to_shape(letter: &str) -> Shape
{
    return match letter {
        "A" => Shape::Rock,
        "B" => Shape::Paper,
        "C" => Shape::Scissors,
        "X" => Shape::Rock,
        "Y" => Shape::Paper,
        "Z" => Shape::Scissors,
        _ => panic!()
    };
}

fn map_letter_to_round_result(letter: &str) -> RoundResult
{
    return match letter {
        "X" => RoundResult::Loss,
        "Y" => RoundResult::Draw,
        "Z" => RoundResult::Win,
        _ => panic!()
    };
}

fn play_round(player_shape: &Shape, opponent_shape: &Shape) -> RoundResult
{
    return match player_shape {
        Shape::Rock => {
            match opponent_shape {
                Shape::Rock => RoundResult::Draw,
                Shape::Paper => RoundResult::Loss,
                Shape::Scissors => RoundResult::Win
            }
        },
        Shape::Paper => {
            match opponent_shape {
                Shape::Rock => RoundResult::Win,
                Shape::Paper => RoundResult::Draw,
                Shape::Scissors => RoundResult::Loss
            }
        },
        Shape::Scissors => {
            match opponent_shape {
                Shape::Rock => RoundResult::Loss,
                Shape::Paper => RoundResult::Win,
                Shape::Scissors => RoundResult::Draw
            }
        }
    }
}

fn get_points_for_shape(shape: &Shape) -> u32
{
    return match shape {
        Shape::Rock => 1,
        Shape::Paper => 2,
        Shape::Scissors => 3
    }
}

fn get_points_for_round_result(round_result: RoundResult) -> u32
{
    return match round_result {
        RoundResult::Win => 6,
        RoundResult::Draw => 3,
        RoundResult::Loss => 0
    }
}

fn get_player_shape_for_round_result(opponent_shape: &Shape, round_result: &RoundResult) -> Shape {
    return match opponent_shape {
        Shape::Rock => {
            match round_result {
                RoundResult::Win => Shape::Paper,
                RoundResult::Draw => Shape::Rock,
                RoundResult::Loss => Shape::Scissors
            }
        },
        Shape::Paper => {
            match round_result {
                RoundResult::Win => Shape::Scissors,
                RoundResult::Draw => Shape::Paper,
                RoundResult::Loss => Shape::Rock
            }
        },
        Shape::Scissors => {
            match round_result {
                RoundResult::Win => Shape::Rock,
                RoundResult::Draw => Shape::Scissors,
                RoundResult::Loss => Shape::Paper
            }
        }
    }
}

enum Shape {
    Rock,
    Paper,
    Scissors
}

enum RoundResult {
    Win,
    Draw,
    Loss
}

#[cfg(test)]
mod tests {
    use super::*;

    #[test]
    fn test_part_one() {
        let input = advent_of_code::read_file("examples", 2);
        assert_eq!(part_one(&input), None);
    }

    #[test]
    fn test_part_two() {
        let input = advent_of_code::read_file("examples", 2);
        assert_eq!(part_two(&input), None);
    }
}
