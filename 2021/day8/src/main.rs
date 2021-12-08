use std::collections::HashMap;
use std::collections::HashSet;
use std::fs::File;
use std::io::BufRead;
use std::io::BufReader;
use std::iter::FromIterator;

fn main() {
    let entries = parse_input();

    let mut part_two_sum = 0;
    for entry in entries {
        let (input, output) = parse_entry(&entry);
        let decoder = Decoder::from(&input);

        let mut number_chars: Vec<char> = Vec::new();
        for digit_string in output {
            number_chars.push(decoder.decode(&digit_string));
        }

        let number_string = String::from_iter(number_chars);
        let number = number_string.parse::<u32>().unwrap();
        part_two_sum += number;
    }

    println!("Part One: {}", part_two_sum);
}

fn parse_input() -> Vec<String> {
    let file = BufReader::new(File::open("input.txt").unwrap());
    return file.lines().map(|l| l.unwrap()).collect();
}

fn parse_entry(entry: &str) -> (Vec<String>, Vec<String>) {
    let split: Vec<&str> = entry.split(" | ").collect();

    let mut input: Vec<String> = Vec::new();
    let mut output: Vec<String> = Vec::new();
    for item in split.get(0).unwrap().split(" ").collect::<Vec<&str>>() {
        input.push(item.to_string());
    }

    for item in split.get(1).unwrap().split(" ").collect::<Vec<&str>>() {
        output.push(item.to_string());
    }

    return (input, output);
}

// fn part_one(output: String) -> Option<u32> {
//     let output_len = output.len();
//     match output_len {
//         2 => {
//             return Option::from(1);
//         }
//         3 => {
//             return Option::from(7);
//         }
//         4 => {
//             return Option::from(4);
//         }
//         7 => {
//             return Option::from(8);
//         }
//         5 => {
//             // 2: acdef
//             // 3: acdfg
//             // 5: abdfg
//             return Option::None;
//         }
//         6 => {
//             // 0: abcefg
//             // 6: acdefg
//             // 9: abcdfg
//             return Option::None;
//         }
//         _ => {
//             panic!()
//         }
//     }
// }

struct Decoder {
    input: Vec<String>,
    number_map: HashMap<u32, String>,
    decode_map: HashMap<char, char>,
    encode_map: HashMap<char, char>,
}

/// Decoded Segments
///  aaaa
/// b    c
/// b    c
///  dddd
/// e    f
/// e    f
///  gggg
///
impl Decoder {
    fn from(input: &Vec<String>) -> Decoder {
        let mut decoder = Decoder {
            input: input.clone(),
            number_map: HashMap::new(),
            decode_map: HashMap::new(),
            encode_map: HashMap::new(),
        };

        decoder.deduction();
        return decoder;
    }

    /// Given an encoded string, decodes the value and returns the number
    /// it represents as a character (easier to join them in the invoking container)
    fn decode(&self, val: &str) -> char {
        let mut number = Vec::new();
        for segment in val.chars() {
            number.push(self.decode_map.get(&segment).unwrap());
        }

        number.sort();

        let number_string = String::from_iter(number);
        match number_string.as_ref() {
            "abcefg" => return '0',
            "cf" => return '1',
            "acdeg" => return '2',
            "acdfg" => return '3',
            "bcdf" => return '4',
            "abdfg" => return '5',
            "abdefg" => return '6',
            "acf" => return '7',
            "abcdefg" => return '8',
            "abcdfg" => return '9',
            _ => {
                println!(
                    "Number Map:\n\t0: {},\n\t1: {},\n\t2: {},\n\t3: {},\n\t4: {},\n\t5: {},\n\t6: {},\n\t7: {},\n\t8: {},\n\t9: {}",
                    self.number_map.get(&0).unwrap(),
                    self.number_map.get(&1).unwrap(),
                    self.number_map.get(&2).unwrap(),
                    self.number_map.get(&3).unwrap(),
                    self.number_map.get(&4).unwrap(),
                    self.number_map.get(&5).unwrap(),
                    self.number_map.get(&6).unwrap(),
                    self.number_map.get(&7).unwrap(),
                    self.number_map.get(&8).unwrap(),
                    self.number_map.get(&9).unwrap(),
                );

                println!(
                    "Encoding Map:\n\ta: {},\n\tb: {},\n\tc:{},\n\td:{},\n\te:{},\n\tf:{},\n\tg:{}",
                    self.encode_map.get(&'a').unwrap(),
                    self.encode_map.get(&'b').unwrap(),
                    self.encode_map.get(&'c').unwrap(),
                    self.encode_map.get(&'d').unwrap(),
                    self.encode_map.get(&'e').unwrap(),
                    self.encode_map.get(&'f').unwrap(),
                    self.encode_map.get(&'g').unwrap(),
                );

                println!(
                    "Decoding Map:\n\ta: {},\n\tb: {},\n\tc:{},\n\td:{},\n\te:{},\n\tf:{},\n\tg:{}",
                    self.decode_map.get(&'a').unwrap(),
                    self.decode_map.get(&'b').unwrap(),
                    self.decode_map.get(&'c').unwrap(),
                    self.decode_map.get(&'d').unwrap(),
                    self.decode_map.get(&'e').unwrap(),
                    self.decode_map.get(&'f').unwrap(),
                    self.decode_map.get(&'g').unwrap(),
                );
                panic!();
            }
        }
    }

    fn deduction(&mut self) {
        // Easy to deduce, just count chars
        self.deduce_one();
        self.deduce_four();
        self.deduce_seven();
        self.deduce_eight();

        // 0, 6, and 9 all have 6 digits. We can deduce them in the following order
        // Don't change this order!
        self.deduce_six();
        self.deduce_zero();
        self.deduce_nine();

        // 2, 3, and 5 all have 5 digits. We can deduce them in the following order
        // Don't change this order!
        self.deduce_two();
        self.deduce_five();
        self.deduce_three();

        // Now that we know all of the numbers, we can deduce the character mappings in the following order
        // Don't change this order!
        self.set_c();
        self.set_f();
        self.set_d();
        self.set_b();
        self.set_a();
        self.set_g();
        self.set_e();
    }

    /// Given 4 and 6, we can differentiate between 0, 6, and 9 by counting the intersecting
    /// segments. 0 will have two segments that intersect with 4
    fn deduce_zero(&mut self) {
        let four = self.number_map.get(&4).unwrap();
        let four_set: HashSet<char> = HashSet::from_iter(four.chars());
        let six = self.number_map.get(&6).unwrap();

        let zero_six_or_nine = self
            .input
            .iter()
            .filter(|x| x.len() == 6)
            .map(|x| x.to_string());

        let zero_or_nine: Vec<_> = zero_six_or_nine.filter(|x| x != six).collect();
        for item in zero_or_nine {
            let item_set: HashSet<char> = HashSet::from_iter(item.chars());
            let intersection: HashSet<_> = item_set.intersection(&four_set).collect();

            // 0 intersection with 4 will not have matching segments
            if intersection.len() != 4 {
                self.number_map.insert(0, item);
            }
        }
    }

    /// Easily deduce-able
    fn deduce_one(&mut self) {
        let one = self
            .input
            .iter()
            .filter(|x| x.len() == 2)
            .map(|x| x.to_string())
            .next()
            .unwrap();
        self.number_map.insert(1, one);
    }

    /// Given 4, we can differentiate between 2, 3, and 5 by counting the intersecting
    /// segments. 2 will have two segments that intersect with 4
    fn deduce_two(&mut self) {
        let four = self.number_map.get(&4).unwrap();
        let four_set: HashSet<char> = HashSet::from_iter(four.chars());

        let two_three_or_five = self
            .input
            .iter()
            .filter(|x| x.len() == 5)
            .map(|x| x.to_string());

        for item in two_three_or_five {
            let item_set: HashSet<_> = HashSet::from_iter(item.chars());
            let intersection: HashSet<_> = item_set.intersection(&four_set).collect();
            if intersection.len() == 2 {
                self.number_map.insert(2, item);
            }
        }
    }

    /// Given 1, we can differentiate between 2, 3, and 5 by counting the intersecting
    /// segments. 3 will have two segments that intersect with 1
    fn deduce_three(&mut self) {
        let one = self.number_map.get(&1).unwrap();
        let one_set: HashSet<char> = HashSet::from_iter(one.chars());

        let two_three_or_five = self
            .input
            .iter()
            .filter(|x| x.len() == 5)
            .map(|x| x.to_string());

        for item in two_three_or_five {
            let item_set: HashSet<_> = HashSet::from_iter(item.chars());
            let intersection: HashSet<_> = item_set.intersection(&one_set).collect();
            if intersection.len() == 2 {
                self.number_map.insert(3, item);
            }
        }
    }

    /// Easily deduce-able
    fn deduce_four(&mut self) {
        let four = self
            .input
            .iter()
            .filter(|x| x.len() == 4)
            .map(|x| x.to_string())
            .next()
            .unwrap();
        self.number_map.insert(4, four);
    }

    /// Given 6, we can differentiate between 2, 3, and 5 by counting the intersecting
    /// segments. 5 will have 5 segments that intersect with 6
    fn deduce_five(&mut self) {
        let six = self.number_map.get(&6).unwrap();
        let six_set: HashSet<char> = HashSet::from_iter(six.chars());

        let two_three_or_five = self
            .input
            .iter()
            .filter(|x| x.len() == 5)
            .map(|x| x.to_string());

        for item in two_three_or_five {
            let item_set: HashSet<_> = HashSet::from_iter(item.chars());
            let intersection: HashSet<_> = item_set.intersection(&six_set).collect();
            if intersection.len() == 5 {
                self.number_map.insert(5, item);
            }
        }
    }

    /// Given 1 and 8, we can differentiate between 0, 6, and 9 by counting the intersecting
    /// segments. When intersected with 8, 6 will have a segment that also exists in 1
    fn deduce_six(&mut self) {
        let eight = self.number_map.get(&8).unwrap();
        let eight_set: HashSet<char> = HashSet::from_iter(eight.chars());
        let one = self.number_map.get(&1).unwrap();
        let one_set: HashSet<char> = HashSet::from_iter(one.chars());

        let zero_six_or_nine = self
            .input
            .iter()
            .filter(|x| x.len() == 6)
            .map(|x| x.to_string());

        for item in zero_six_or_nine {
            let item_set: HashSet<_> = HashSet::from_iter(item.chars());
            let missing_segments: HashSet<_> = item_set.symmetric_difference(&eight_set).collect();
            // Only ever one missing segment because 0, 6, and 9 all have six segments, where as 8 has seven segments
            let missing_segments = Vec::from_iter(missing_segments.clone());
            let missing_segment = missing_segments.get(0).unwrap().clone();

            // The segments for 1 do not overlap with 0 and 9, but does have an overlapping segment with 6. This is how
            // we identify 6
            if one_set.contains(missing_segment) {
                self.number_map.insert(6, item);
            }
        }
    }

    /// Easily deduce-able
    fn deduce_seven(&mut self) {
        let seven = self
            .input
            .iter()
            .filter(|x| x.len() == 3)
            .map(|x| x.to_string())
            .next()
            .unwrap();
        self.number_map.insert(7, seven);
    }

    /// Easily deduce-able
    fn deduce_eight(&mut self) {
        let eight = self
            .input
            .iter()
            .filter(|x| x.len() == 7)
            .map(|x| x.to_string())
            .next()
            .unwrap();
        self.number_map.insert(8, eight);
    }

    /// Given 0 and 6, we can find 9
    fn deduce_nine(&mut self) {
        let zero = self.number_map.get(&0).unwrap();
        let six = self.number_map.get(&6).unwrap();
        let zero_six_or_nine = self
            .input
            .iter()
            .filter(|x| x.len() == 6)
            .map(|x| x.to_string());

        let nines: Vec<_> = zero_six_or_nine.filter(|x| x != zero && x != six).collect();
        let nine = nines.get(0).unwrap().clone();
        self.number_map.insert(9, nine);
    }

    fn set_a(&mut self) {
        let seven = self.number_map.get(&7).unwrap();
        let seven_segments: Vec<char> = seven.chars().collect();

        let c_segment = self.encode_map.get(&'c').unwrap();
        let f_segment = self.encode_map.get(&'f').unwrap();

        let a: Vec<_> = seven_segments
            .iter()
            .filter(|x| x != &c_segment && x != &f_segment)
            .collect();

        self.encode_map.insert('a', **a.get(0).unwrap());
        self.decode_map.insert(**a.get(0).unwrap(), 'a');
    }

    fn set_b(&mut self) {
        let four = self.number_map.get(&4).unwrap();
        let four_segments: Vec<char> = four.chars().collect();

        let c_segment = self.encode_map.get(&'c').unwrap();
        let d_segment = self.encode_map.get(&'d').unwrap();
        let f_segment = self.encode_map.get(&'f').unwrap();

        let b: Vec<_> = four_segments
            .iter()
            .filter(|x| x != &c_segment && x != &d_segment && x != &f_segment)
            .collect();

        self.encode_map.insert('b', **b.get(0).unwrap());
        self.decode_map.insert(**b.get(0).unwrap(), 'b');
    }

    fn set_c(&mut self) {
        let one = self.number_map.get(&1).unwrap();
        let one_set: HashSet<char> = HashSet::from_iter(one.chars());
        let two = self.number_map.get(&2).unwrap();
        let two_set: HashSet<char> = HashSet::from_iter(two.chars());

        let intersection_set: HashSet<_> = one_set.intersection(&two_set).collect();
        let segment_itr: Vec<_> = intersection_set.iter().collect();
        let segment = segment_itr.get(0).unwrap();

        self.encode_map.insert('c', ***segment);
        self.decode_map.insert(***segment, 'c');
    }

    fn set_d(&mut self) {
        let zero = self.number_map.get(&0).unwrap();
        let zero_set: HashSet<char> = HashSet::from_iter(zero.chars());
        let eight = self.number_map.get(&8).unwrap();
        let eight_set: HashSet<char> = HashSet::from_iter(eight.chars());

        let segment_set: HashSet<_> = eight_set.symmetric_difference(&zero_set).collect();
        let segment_itr: Vec<_> = segment_set.iter().collect();
        let segment = segment_itr.get(0).unwrap();

        self.encode_map.insert('d', ***segment);
        self.decode_map.insert(***segment, 'd');
    }

    fn set_e(&mut self) {
        let eight = self.number_map.get(&8).unwrap();
        let eight_segments: Vec<char> = eight.chars().collect();

        let a_segment = self.encode_map.get(&'a').unwrap();
        let b_segment = self.encode_map.get(&'b').unwrap();
        let c_segment = self.encode_map.get(&'c').unwrap();
        let d_segment = self.encode_map.get(&'d').unwrap();
        let f_segment = self.encode_map.get(&'f').unwrap();
        let g_segment = self.encode_map.get(&'g').unwrap();

        let e: Vec<_> = eight_segments
            .iter()
            .filter(|x| {
                x != &a_segment
                    && x != &b_segment
                    && x != &c_segment
                    && x != &d_segment
                    && x != &f_segment
                    && x != &g_segment
            })
            .collect();

        self.encode_map.insert('e', **e.get(0).unwrap());
        self.decode_map.insert(**e.get(0).unwrap(), 'e');
    }

    fn set_f(&mut self) {
        let one = self.number_map.get(&1).unwrap();
        let one_set: HashSet<char> = HashSet::from_iter(one.chars());
        let c_segment = self.encode_map.get(&'c').unwrap();

        let segment_itr: Vec<_> = one_set.iter().filter(|x| x != &c_segment).collect();
        let segment = segment_itr.get(0).unwrap();

        self.encode_map.insert('f', **segment);
        self.decode_map.insert(**segment, 'f');
    }

    fn set_g(&mut self) {
        let three = self.number_map.get(&3).unwrap();
        let three_segments: Vec<char> = three.chars().collect();

        let a_segment = self.encode_map.get(&'a').unwrap();
        let c_segment = self.encode_map.get(&'c').unwrap();
        let d_segment = self.encode_map.get(&'d').unwrap();
        let f_segment = self.encode_map.get(&'f').unwrap();

        let g: Vec<_> = three_segments
            .iter()
            .filter(|x| x != &a_segment && x != &c_segment && x != &d_segment && x != &f_segment)
            .collect();

        self.encode_map.insert('g', **g.get(0).unwrap());
        self.decode_map.insert(**g.get(0).unwrap(), 'g');
    }
}
