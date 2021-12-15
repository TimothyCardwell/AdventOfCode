use std::collections::HashMap;
use std::collections::HashSet;
use std::fs::File;
use std::io::BufRead;
use std::io::BufReader;

fn main() {
    let mut cave_map = parse_input();
    for cave in cave_map.iter() {
        println!("Cave: {}, Neighbors: {:?}", cave.1.name, cave.1.neighbors);
    }

    let result = search_path("start", &mut cave_map, &mut HashSet::new(), false);

    println!("result: {:?}", result.unwrap().len());
}

fn search_path(
    current_cave_string: &str,
    all_caves: &mut HashMap<String, Cave>,
    visited_caves: &mut HashSet<String>,
    visited_small_cave: bool,
) -> Option<Vec<String>> {
    let current_cave = all_caves.get_mut(current_cave_string).unwrap();

    // Base case --> Found the exit!
    if current_cave.cave_type == CaveType::Exit {
        return Option::from(vec![current_cave.name.to_string()]);
    }

    // Base Case --> Cannot go back to entrance or re-visit small caves
    if visited_caves.contains(&current_cave.name) && current_cave.cave_type == CaveType::Entrance {
        return Option::None;
    }

    let mut visited_small_cave_copy = visited_small_cave;
    if visited_caves.contains(&current_cave.name) && current_cave.cave_type == CaveType::Small {
        if !visited_small_cave {
            visited_small_cave_copy = true;
        } else {
            return Option::None;
        }
    }

    visited_caves.insert(current_cave.name.to_string());

    // have to clone these objects due to ownership / borrowing issues... I must be missing something
    let neighbors = current_cave.neighbors.to_vec();
    let mut paths: Vec<String> = Vec::new();
    for neighbor in neighbors {
        let mut visited_caves_clone = visited_caves.clone();
        let result = search_path(
            &neighbor,
            all_caves,
            &mut visited_caves_clone,
            visited_small_cave_copy,
        );
        match result {
            Some(valid_paths) => {
                for valid_path in valid_paths {
                    paths.push(format!("{},{}", current_cave_string, valid_path));
                }
            }
            _ => {}
        }
    }

    return Option::from(paths);
}

#[derive(PartialEq)]
enum CaveType {
    Big,
    Entrance,
    Exit,
    Small,
}

struct Cave {
    name: String,
    cave_type: CaveType,
    neighbors: Vec<String>,
}

impl Cave {
    fn from(name: &str) -> Cave {
        let cave_type: CaveType;
        if name == "start" {
            cave_type = CaveType::Entrance;
        } else if name == "end" {
            cave_type = CaveType::Exit;
        } else if name.chars().next().unwrap().is_lowercase() {
            cave_type = CaveType::Small;
        } else {
            cave_type = CaveType::Big;
        }

        return Cave {
            name: String::from(name),
            cave_type: cave_type,
            neighbors: Vec::new(),
        };
    }

    fn add_neighbor(&mut self, neighbor: &str) {
        self.neighbors.push(neighbor.to_string());
    }
}

fn parse_input() -> HashMap<String, Cave> {
    let file = BufReader::new(File::open("input.txt").unwrap());

    let mut caves: HashMap<String, Cave> = HashMap::new();
    for line in file.lines().map(|l| l.unwrap()) {
        let mut line_split = line.split("-");
        let source_string = line_split.next().unwrap();
        let destination_string = line_split.next().unwrap();

        if !caves.contains_key(source_string) {
            caves.insert(String::from(source_string), Cave::from(source_string));
        }

        if !caves.contains_key(destination_string) {
            caves.insert(
                String::from(destination_string),
                Cave::from(destination_string),
            );
        }

        let source_cave = caves.get_mut(source_string).unwrap();
        source_cave.add_neighbor(destination_string);

        let destination_cave = caves.get_mut(destination_string).unwrap();
        destination_cave.add_neighbor(source_string);
    }

    return caves;
}
