The given Task:

The challenge is to program a simple version of the game Battleships (video). Create an application to allow a single human player to play a one-sided game of Battleships against ships placed by the computer.

The program should create a 10x10 grid, and place several ships on the grid at random with the following sizes:

1x Battleship (5 squares)

2x Destroyers (4 squares)

The player enters or selects coordinates of the form “A5”, where “A” is the column and “5” is the row, to specify a square to target. Shots result in hits, misses or sinks. The game ends when all ships are sunk.

You can write a console application or UI to complete the task.

Try to code the challenge as you would approach any typical work task; we are not looking for you to show knowledge of frameworks or unusual programming language features. Most importantly, keep it simple.

-------------------------------

Assumptions:
1. The algorithm used to place ships on the map is based on random values. As it is stated how big map will be and how many ships is going to be placed, this algorithm seemed to be the best to use. If there would be much more ships (higher density), it would be worth to consider another algorithm.
2. As the map and number of ships is small, basic data structures were used. Using more sophisticated would not be beneficial in this case.
3. The basic form of this game was implemented - just one shot per turn (not number of shoots = the number of ships)

------------------------------
Instruction:
Run it from Visual Studio. If you pass 'cheat' as argument to the app, it would show map uncovered.

![image](https://github.com/kamilzegadlo/Battleships/assets/17237889/751195f1-74b9-4ea1-bbd8-33262df0d352)

![image](https://github.com/kamilzegadlo/Battleships/assets/17237889/c1771527-d5e1-424a-a785-e559414f0437)

![image](https://github.com/kamilzegadlo/Battleships/assets/17237889/e0c7f6b1-eeac-4b60-92a1-ba16008baa33)


