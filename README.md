# Battleship State Tracker Documentation
By Andrea Ralletti

## Overview

The game starts with a menu that allows the player to:

-	Start a new game ( Reset boards)
-	View the boards ( View Player board and enemy board)
-	Add a ship (Select size, coordinates and orientation)
-	Attack (Fire on a location on the enemy board)
-	Exit


For usability, the “enemy board” is essentially a reflection of the player’s board. In the sense that the player will play against his own set of ships. Game is won once all ships have been destroyed. 
Player can deploy between 1 to 3 ships. A ship can have a size between 1 to 5.

## Validation

-	Player cannot attack if no ship has been deployed
-	Player cannot deploy more ships once he started attacking
-	Player cannot attack if game is finished

-	Ships cannot be deployed over other ships
-	Ships cannot be deployed out of the board boundaries (10x10)

-	Hits are not counted over coordinates that have been already hit

## Classes 

-	Cell class: Represent a cell on the board, has 3 attributes row, column and status
There are 5 types of status
On player board:
- = empty cell
S = the cell is allocated to a ship
On Enemy board:
M = Missed 
H = Hit
D = Destroyed (once a ship has been destroyed all H transform to D)

-	Ship class: represents a ship, it has several attributes such as its health, which is equal to the size given by the player. A flag to check if it is still alive. And a list that contains all cells that compose the ship position on the board.

-	Board class: represent the board, it has several attributes such as a list of cells containing 100 cells, a list of ships containing the ships deployed, the total number of ships alive and a maximum number of ships to deploy(to allow for different rules such as max number of ships available)

-	Program class: represents the main program, it contains the game menu and different methods to allow for menu options such as, reset the boards, view the boards, add a ship to a board and attack.

## Operations (Activity flow)

Start a new game: 
-	The boards are reset to empty cells
-	 Flags return to default state 
View Boards:
-	Print player board by looping through player board and print each cell status
-	Print enemy board by looping through enemy board and print each cell status
Deploy ship:
-	First check if player has already start attacking
-	Get player input for row, column, size and orientation
-	Validate ship against board boundaries
-	Validate ship by checking for other ships in the same locations
-	Deploy ship (Ship added to board, board cells status change)
Attack ship:
-	First check if  game has finished 
-	Second check if player has deployed a ship
-	Get player input for shot coordinates
-	Loop through all ships on the board, and check if ship locations match fire shot coordinates
-	If they match check if the ship was hit already, and then check if it was destroyed already
-	If it was not hit( or destroyed) already, update the board, and update ship health
-	Check how much health is left, if ship does not have any health left, destroy ship (update board H becomes D)
-	Check how many ships are left on the board, if there are no ships left player has won the game

## State

-	Board state is managed by a list containing 100 cells and their respective status
-	Board state is also managed by a list of ships on the board
-	Ship state is managed by the health of the ship
-	Shot state is managed by comparing the shot coordinates to the cells occupied by each ship on the board
-	Game state is managed by the number of ships left on the board
![image](https://user-images.githubusercontent.com/39337728/121800999-4a529500-cc78-11eb-87ff-80db6398e30c.png)
