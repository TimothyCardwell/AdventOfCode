use std::fs::File;
use std::io::BufRead;
use std::io::BufReader;

fn main() {
    let mut input = parse_input();
    let low_points = find_low_points(&input);

    // Part One
    // println!(
    //     "Part One: {}",
    //     low_points
    //         .iter()
    //         .map(|p| input[p.0][p.1].value + 1)
    //         .sum::<u32>()
    // );

    let mut basin_sizes = Vec::new();
    for low_point in low_points {
        let basin_size = get_basin_size(&mut input, low_point.0, low_point.1);
        basin_sizes.push(basin_size);
        reset_visited(&mut input);
    }

    basin_sizes.sort();
    basin_sizes.reverse();
    let part_two_answer = basin_sizes[0] * basin_sizes[1] * basin_sizes[2];

    //println!("Part One: {}", part_one_answer);
    println!("Part Two: {}", part_two_answer);
}

struct Node {
    value: u32,
    is_visited: bool,
}

fn find_low_points(matrix: &Vec<Vec<Node>>) -> Vec<(usize, usize)> {
    let mut low_points = Vec::new();

    let mut i = 0;
    while i < matrix.len() {
        let mut j = 0;
        while j < matrix[i].len() {
            let mut is_lowest = true;
            let current = &matrix[i][j];

            // Check Top
            if i != 0 && matrix.get(i - 1).is_some() {
                is_lowest = is_lowest && current.value < matrix[i - 1][j].value;
            }

            // Check Bottom
            if matrix.get(i + 1).is_some() {
                is_lowest = is_lowest && current.value < matrix[i + 1][j].value;
            }
            // Check Left
            if j != 0 && matrix[i].get(j - 1).is_some() {
                is_lowest = is_lowest && current.value < matrix[i][j - 1].value;
            }

            // Check Right
            if matrix[i].get(j + 1).is_some() {
                is_lowest = is_lowest && current.value < matrix[i][j + 1].value;
            }

            if is_lowest {
                low_points.push((i, j));
            }

            j += 1;
        }

        i += 1;
    }

    return low_points;
}

fn get_basin_size(matrix: &mut Vec<Vec<Node>>, i: usize, j: usize) -> u32 {
    // Base Case - Node doesn't exist
    if matrix.get(i).is_none() || matrix[i].get(j).is_none() {
        return 0;
    }

    // Base Case - Node already visited
    if matrix[i][j].is_visited {
        return 0;
    }

    let mut size = 1;
    matrix[i][j].is_visited = true;

    let current_value = matrix[i][j].value;

    // Check Top
    if i > 0
        && matrix.get(i - 1).is_some()
        && matrix[i - 1][j].value != 9
        && matrix[i - 1][j].value > current_value
    {
        size += get_basin_size(matrix, i - 1, j);
    }

    // Check Bottom
    if matrix.get(i + 1).is_some()
        && matrix[i + 1][j].value != 9
        && matrix[i + 1][j].value > current_value
    {
        size += get_basin_size(matrix, i + 1, j);
    }

    // Check Left
    if j > 0
        && matrix[i].get(j - 1).is_some()
        && matrix[i][j - 1].value != 9
        && matrix[i][j - 1].value > current_value
    {
        size += get_basin_size(matrix, i, j - 1);
    }

    // Check Right
    if matrix[i].get(j + 1).is_some()
        && matrix[i][j + 1].value != 9
        && matrix[i][j + 1].value > current_value
    {
        size += get_basin_size(matrix, i, j + 1);
    }

    return size;
}

fn reset_visited(matrix: &mut Vec<Vec<Node>>) {
    let mut i = 0;
    while i < matrix.len() {
        let mut j = 0;
        while j < matrix[i].len() {
            matrix[i][j].is_visited = false;

            j += 1;
        }

        i += 1;
    }
}

fn parse_input() -> Vec<Vec<Node>> {
    let file = BufReader::new(File::open("input.txt").unwrap());
    let vectors: Vec<Vec<Node>> = file
        .lines()
        .map(|l| {
            l.unwrap()
                .chars()
                .map(|c| Node {
                    value: c.to_digit(10).unwrap(),
                    is_visited: false,
                })
                .collect()
        })
        .collect();
    return vectors;
}
