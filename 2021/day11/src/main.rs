use std::fs::File;
use std::io::BufRead;
use std::io::BufReader;

fn main() {
    let mut octopuses = parse_input();
    let octopus_count = octopuses.len() * octopuses.get(0).unwrap().len();

    let mut all_flashed = false;
    let mut i = 0;
    while !all_flashed {
        increase_energy_levels(&mut octopuses);
        check_flash(&mut octopuses);
        let flash_count = reset_flashes(&mut octopuses) as usize;
        if flash_count == octopus_count {
            all_flashed = true;
        }
        i += 1;
    }

    print(&mut octopuses);

    println!("Part Two: {}", i);
}

fn increase_energy_levels(matrix: &mut Vec<Vec<Octopus>>) {
    let mut i = 0;
    while i < matrix.len() {
        let mut j = 0;
        while j < matrix[i].len() {
            matrix[i][j].value += 1;

            j += 1;
        }

        i += 1;
    }
}

fn check_flash(matrix: &mut Vec<Vec<Octopus>>) {
    let mut i = 0;
    while i < matrix.len() {
        let mut j = 0;
        while j < matrix[i].len() {
            if matrix[i][j].value > 9 {
                flash(matrix, i, j);
            }

            j += 1;
        }

        i += 1;
    }
}

fn reset_flashes(matrix: &mut Vec<Vec<Octopus>>) -> u32 {
    let mut flash_count = 0;
    let mut i = 0;
    while i < matrix.len() {
        let mut j = 0;
        while j < matrix[i].len() {
            let octopus = &mut matrix[i][j];
            if octopus.flashed {
                flash_count += 1;
                matrix[i][j].reset_flash();
            }

            j += 1;
        }

        i += 1;
    }

    return flash_count;
}

fn print(matrix: &mut Vec<Vec<Octopus>>) {
    let mut i = 0;
    while i < matrix.len() {
        let mut j = 0;
        while j < matrix[i].len() {
            let octopus = &matrix[i][j];
            print!("{}", octopus.value);

            j += 1;
        }

        println!();

        i += 1;
    }
}

struct Octopus {
    value: u32,
    flashed: bool,
}

impl Octopus {
    fn flash(&mut self) {
        self.value = 0;
        self.flashed = true
    }

    fn reset_flash(&mut self) {
        self.flashed = false;
    }
}

fn flash(matrix: &mut Vec<Vec<Octopus>>, i: usize, j: usize) {
    // Base Case - Octopus already flashed
    if matrix[i][j].flashed {
        return;
    }

    let octopus = &mut matrix[i][j];
    octopus.flash();

    let max_height = matrix.len() - 1;
    let max_width = matrix.get(0).unwrap().len() - 1;

    // Top Left
    if i > 0 && j > 0 {
        flashed(matrix, i - 1, j - 1);
    }

    // Top
    if i > 0 {
        flashed(matrix, i - 1, j);
    }

    // Top Right
    if i > 0 && j < max_width {
        flashed(matrix, i - 1, j + 1);
    }

    // Right
    if j < max_width {
        flashed(matrix, i, j + 1);
    }

    // Bottom Right
    if i < max_height && j < max_width {
        flashed(matrix, i + 1, j + 1);
    }

    // Bottom
    if i < max_height {
        flashed(matrix, i + 1, j);
    }

    // Bottom Left
    if i < max_height && j > 0 {
        flashed(matrix, i + 1, j - 1);
    }

    // Left
    if j > 0 {
        flashed(matrix, i, j - 1);
    }
}

fn flashed(matrix: &mut Vec<Vec<Octopus>>, i: usize, j: usize) {
    // Base Case - Octopus already flashed
    if matrix[i][j].flashed {
        return;
    }

    let octopus = &mut matrix[i][j];
    octopus.value += 1;
    if octopus.value > 9 {
        flash(matrix, i, j);
    }
}

fn parse_input() -> Vec<Vec<Octopus>> {
    let file = BufReader::new(File::open("input.txt").unwrap());
    let vectors: Vec<Vec<Octopus>> = file
        .lines()
        .map(|l| {
            l.unwrap()
                .chars()
                .map(|c| Octopus {
                    value: c.to_digit(10).unwrap(),
                    flashed: false,
                })
                .collect()
        })
        .collect();
    return vectors;
}
