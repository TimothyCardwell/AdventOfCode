use rusttype::Point;
use std::collections::HashMap;
use std::fs::File;
use std::io::BufRead;
use std::io::BufReader;

fn main() {
    let lines_of_vents = parse_input();
    let mut overlapping_points: HashMap<Point<u32>, u32> = HashMap::new();

    for line_segment in lines_of_vents {
        for point in line_segment.get_points() {
            match overlapping_points.get_mut(&point) {
                Some(x) => {
                    *x += 1;
                }
                None => {
                    overlapping_points.insert(point, 1);
                }
            }
        }
    }

    let part_one_answer = overlapping_points.iter().filter(|&(_k, v)| *v >= 2).count();
    println!("Part One Answer: {}", part_one_answer);
}

fn parse_input() -> Vec<PointToPoint> {
    let mut line_of_vents = Vec::new();

    let file = BufReader::new(File::open("input.txt").unwrap());
    for line in file.lines() {
        line_of_vents.push(PointToPoint::new(&line.unwrap()));
    }

    return line_of_vents;
}

struct PointToPoint {
    source_point: Point<u32>,
    target_point: Point<u32>,
}

impl PointToPoint {
    fn new(val: &str) -> PointToPoint {
        let no_whitespace = val.replace(" ", "");
        let mut thing = no_whitespace.split("->");

        let mut source = thing.next().unwrap().split(",");
        let source_x = source.next().unwrap().parse::<u32>().unwrap();
        let source_y = source.next().unwrap().parse::<u32>().unwrap();

        let mut target = thing.next().unwrap().split(",");
        let target_x = target.next().unwrap().parse::<u32>().unwrap();
        let target_y = target.next().unwrap().parse::<u32>().unwrap();

        return PointToPoint {
            source_point: Point {
                x: source_x,
                y: source_y,
            },
            target_point: Point {
                x: target_x,
                y: target_y,
            },
        };
    }

    fn get_points(&self) -> Vec<Point<u32>> {
        let mut points: Vec<Point<u32>> = Vec::new();
        // Horizontal
        if self.source_point.x == self.target_point.x {
            // To make iteration easy, we choose the source point to be the smaller y value
            let mut source_point = if self.source_point.y < self.target_point.y {
                self.source_point
            } else {
                self.target_point
            };
            let target_point = if self.source_point.y < self.target_point.y {
                self.target_point
            } else {
                self.source_point
            };

            while source_point.y <= target_point.y {
                points.push(Point {
                    x: source_point.x,
                    y: source_point.y,
                });

                source_point.y += 1;
            }
        }
        // Vertical
        else if self.source_point.y == self.target_point.y {
            // To make iteration easy, we choose the source point to be the smaller x value
            let mut source_point = if self.source_point.x < self.target_point.x {
                self.source_point
            } else {
                self.target_point
            };
            let target_point = if self.source_point.x < self.target_point.x {
                self.target_point
            } else {
                self.source_point
            };

            while source_point.x <= target_point.x {
                points.push(Point {
                    x: source_point.x,
                    y: source_point.y,
                });

                source_point.x += 1;
            }
        }
        // Diagonal
        else {
            // To make iteration easy, we choose the source point to be the smaller x value
            let mut source_point = if self.source_point.x < self.target_point.x {
                self.source_point
            } else {
                self.target_point
            };
            let target_point = if self.source_point.x < self.target_point.x {
                self.target_point
            } else {
                self.source_point
            };

            while source_point.x <= target_point.x {
                points.push(Point {
                    x: source_point.x,
                    y: source_point.y,
                });

                source_point.x += 1;
                if source_point.y < target_point.y {
                    source_point.y += 1;
                } else {
                    source_point.y -= 1;
                }
            }
        }

        return points;
    }
}
