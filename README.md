# Bob's Advent of Code 2022

It's Advent of Code time again! I probably won't document every day like I did [last year](https://github.com/mrdrbob/aoc-2021), but may write up some stuff as I get into the chunkier puzzles.

Everything is in C# again this year. I have no intention of keeping up with this every day, but may try to at least beat all the puzzles... eventually.

## Day 4

This one, for me, is notable mostly because it gave me an excuse to play with generic math to create a `Range<T>` type that can theoretically be used for different numeric types. I'm not 100% sure what I'm doing there, but it works for integers, so far. Also, I'm kind of abusing records here as a lazy way of destructuring what is effectively a list of values, since C# does not have array destructuring built-in. It had the added bonus of giving me the ability to apply a transformation to my soon-to-be-destructured values.
