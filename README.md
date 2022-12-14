My Advent of Code 2022 solutions in c# and possibly c  
- [Advent](https://adventofcode.com/)

Some comments on more difficult days as they ramp up.  
I fell behind due to some family stuff, working on catching up.

Day 11: Easy enough problem when you create a containing class for each monkey. I did
end up getting stuck on the second part for a few minutes, because I was dividing by the LCM isntead
of taking the remainder through modulo, and didn't spot the error for longer than I'd care to admit.

Day 12: Implementing Dijkstra was a bit difficult, as examples online were not very helpful in understanding well
what was needed for it. Eventually I was able to get it done.

Day13: Extreme frustration as my solution was very close for the longest time, and I could not for the
life of me figure out what I was doing wrong. Until I finally spotted something that has been a running 
theme of these challenges so far for me. I had not considered 2 digit numbers in my solution. And reading 
the file character by character meant I was adding 1 and 0 from 10 separately.

Day 15: Brute force solution was easy enough to find, but with the large numbers involved it would have likely have taken at least an
hour to solve, so I spent some time trying to consider ways to find gaps in the signal another way. To keep it fast, I was not checking all spots, 
but only those less than the current location + manhattan distance in each cardinal direction. So I wanted a way to skip checking areas directly above and below
sensors, as well as some diagnal spots.

Day 16: Needed some help getting started here, I had planned on reusing the Dijkstra method to find the distance between each location to any other location, filtering
for valves with value, then brute forcing a solution. I got stuck after getting the Dijkstra method ran, on how to check each possible path for the best choice, so referenced other peoples solutions for guidance.

