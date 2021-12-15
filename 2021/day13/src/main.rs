use std::fs::File;
use std::io::BufRead;
use std::io::BufReader;

fn main() {
    let (mut matrix, x_fold, y_fold) = parse_input();
    /*
    fold along x=655
    fold along y=447
    fold along x=327
    fold along y=223
    fold along x=163
    fold along y=111
    fold along x=81
    fold along y=55
    fold along x=40
    fold along y=27
    fold along y=13
    fold along y=6
    */
    matrix = fold_x(&matrix);
    matrix = fold_y(&matrix);
    matrix = fold_x(&matrix);
    matrix = fold_y(&matrix);
    matrix = fold_x(&matrix);
    matrix = fold_y(&matrix);
    matrix = fold_x(&matrix);
    matrix = fold_y(&matrix);
    matrix = fold_x(&matrix);
    matrix = fold_y(&matrix);
    matrix = fold_y(&matrix);
    matrix = fold_y(&matrix);
    println!("Dots: {:?}", matrix);
    let dots = count_dots(&matrix);
    println!("Dots: {}", dots);
}

fn fold_x(matrix: &Vec<Vec<bool>>) -> Vec<Vec<bool>> {
    let mut result: Vec<Vec<bool>> = Vec::new();

    let height = matrix.len();
    let width = matrix[0].len();
    println!("X: {}", width / 2);
    let mut i = 0;
    while i < height {
        result.push(Vec::new());
        let mut j = 0;
        while j < width / 2 {
            result[i].push(matrix[i][j] && matrix[i][width - 1 - j]);
            j += 1;
        }

        i += 1;
    }

    return result;
}

fn fold_y(matrix: &Vec<Vec<bool>>) -> Vec<Vec<bool>> {
    let mut result: Vec<Vec<bool>> = Vec::new();

    let height = matrix.len();
    let width = matrix[0].len();
    println!("Y: {}", height / 2);
    let mut i = 0;
    while i < height / 2 {
        result.push(Vec::new());
        let mut j = 0;
        while j < width {
            result[i].push(matrix[i][j] && matrix[height - 1 - i][j]);
            j += 1;
        }

        i += 1;
    }

    return result;
}

fn count_dots(matrix: &Vec<Vec<bool>>) -> u32 {
    let mut count = 0;

    let height = matrix.len();
    let width = matrix[0].len();
    let mut i = 0;
    while i < height {
        let mut j = 0;
        while j < width {
            if !matrix[i][j] {
                count += 1;
            }
            j += 1;
        }

        i += 1;
    }

    return count;
}

fn parse_input() -> (Vec<Vec<bool>>, usize, usize) {
    let file = BufReader::new(File::open("input.txt").unwrap());

    let mut y_fold = 0;
    let mut x_fold = 0;

    let mut pairs: Vec<(usize, usize)> = Vec::new();
    for line in file.lines().map(|l| l.unwrap()) {
        if line == "" {
        } else if line.contains(",") {
            let mut split = line.split(",");
            let x = split.next().unwrap().parse::<usize>().unwrap();
            let y = split.next().unwrap().parse::<usize>().unwrap();
            pairs.push((x, y));
        } else if line.contains("y") {
            let mut split = line.split("=");
            split.next();
            y_fold = split.next().unwrap().parse::<usize>().unwrap();
        } else if line.contains("x") {
            let mut split = line.split("=");
            split.next();
            x_fold = split.next().unwrap().parse::<usize>().unwrap();
        } else {
            panic!();
        }
    }

    let mut max_x = 0;
    let mut max_y = 0;
    for pair in &pairs {
        if max_x < pair.0 {
            max_x = pair.0;
        }
        if max_y < pair.1 {
            max_y = pair.1;
        }
    }

    println!("X: {}, Y: {}", max_x, max_y);

    let mut matrix: Vec<Vec<bool>> = Vec::new();
    let mut i = 0;
    while i <= max_y {
        matrix.push(Vec::new());
        let mut j = 0;
        while j <= max_x {
            matrix[i].push(true);
            j += 1;
        }

        i += 1;
    }

    for pair in &pairs {
        let x = pair.0;
        let y = pair.1;
        matrix[y][x] = false
    }
    return (matrix, x_fold, y_fold);
}
