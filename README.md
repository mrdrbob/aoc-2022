# Bob's Advent of Code 2022

It's Advent of Code time again! I probably won't document every day like I did [last year](https://github.com/mrdrbob/aoc-2021), but may write up some stuff as I get into the chunkier puzzles.

Everything is in C# again this year. I have no intention of keeping up with this every day, but may try to at least beat all the puzzles... eventually.

## Day 4

This one, for me, is notable mostly because it gave me an excuse to play with generic math to create a `Range<T>` type that can theoretically be used for different numeric types. I'm not 100% sure what I'm doing there, but it works for integers, so far. Also, I'm kind of abusing records here as a lazy way of destructuring what is effectively a list of values, since C# does not have array destructuring built-in. It had the added bonus of giving me the ability to apply a transformation to my soon-to-be-destructured values.

### Day 8

I think I write a basic Grid and Vector class every year. I suppose I could've used last year's implementation, but each time I write it gets... maybe not better, but different. Last year I really couldn't decide if my Grid API should take an `x` and `y` value, or a `Point`, or a `Vector`, or whatever. I had a lot of different ways of representing "two values". This year I think I'll mostly stick to my own `Vector2<T>` class. Re-inventing the wheel here, but it's all for fun. In retrospect, my `Range` type could probably have just been a `Vector2<T>` as well. Maybe I'll refactor at some point.
