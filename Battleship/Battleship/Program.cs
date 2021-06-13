using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace Battleship
{
    class Program
    {
        const int NUMBEROFSHIPS = 3; // Constant to set Game Rules (number of ships available)
        const int MAXSHIPSIZE = 5; // Constant to set Game Rules (max size of a ship)
        static bool GAMEWON = false; // Flag to mark end of the game


        static void Main(string[] args)
        {

            Board playerBoard = new Board(NUMBEROFSHIPS); // Board to store player's ships
            Board enemyBoard = new Board(NUMBEROFSHIPS); // Board to store player's shots
            string choice = "0"; // variable to store user input on menu option
            bool attackTurn = false;

            /* Game Menu */


            while (!choice.Equals("5"))
            {
                Console.WriteLine("\n***** Game Menu *****\n");
                Console.WriteLine("Please Select an option:");
                Console.WriteLine("1. Start a New Game");
                Console.WriteLine("2. View Boards");
                Console.WriteLine("3. Add Ships");
                Console.WriteLine("4. Attack");
                Console.WriteLine("5. Exit");


                choice = Console.ReadLine(); // Read player input


                switch (choice)
                {
                    case "1":
                        /* Initialise Boards */
                        attackTurn = false;
                        playerBoard = InitialiseBoard();
                        enemyBoard = InitialiseBoard();
                        Console.WriteLine("\nBoards Initialised Successfully");

                        break;

                    case "2":
                        /* Print Boards and symbols */

                        Console.WriteLine("Your Board:\n");
                        PrintBoard(playerBoard);
                        Console.WriteLine("\n Ships Available = " + playerBoard.ships);
                        Console.WriteLine(" Ships Deployed = " + playerBoard.shipsDeployed.Count + "\n");
                        Console.WriteLine("\n - = empty \n");
                        Console.WriteLine("\n S = Ship \n");
                        Console.WriteLine("\nEnemy Board:\n");
                        PrintBoard(enemyBoard);
                        Console.WriteLine("\n M = Missed \n");
                        Console.WriteLine("\n H = Hit \n");
                        Console.WriteLine("\n D = Destroyed \n");

                        Console.WriteLine(" Number of enemy ships left: " + playerBoard.shipsAlive + "\n");



                        break;

                    case "3":
                        /* Add a ship */
                        if (!attackTurn) // check if player already start attacking
                        {
                            int row, col, size; // variables that store coordinates and size of the ship
                            int alignment; // variable to store alignment of the ship
                            bool isShipValid; // boolean to check if ship is in a valid position

                            /* Read player input for ship */
                            Console.WriteLine("\nPlease select size(5 MAX)");
                            size = int.Parse(Console.ReadLine());
                            Console.WriteLine("\nCOORDINATES:");
                            Console.WriteLine("\nPlease select row");
                            row = int.Parse(Console.ReadLine());
                            Console.WriteLine("\nPlease select column");
                            col = int.Parse(Console.ReadLine());

                            Console.WriteLine("\nPlease select alignment");
                            Console.WriteLine("\n1.Vertical  2.Horizontal");

                            alignment = int.Parse(Console.ReadLine());

                            isShipValid = validateShip(playerBoard, row, col, size, alignment); // check against boundaries of the board

                            if (isShipValid) // if ship pass validation
                            {
                                addShip(playerBoard, row, col, size, alignment);
                                Console.WriteLine("\nShip deployed");

                            }
                            else // if ship does not pass validation
                            {
                                Console.WriteLine("\nShip not valid");

                            }
                        }

                        else // if player already started attacking
                        {
                            Console.WriteLine("\nAttack turn already started");

                        }

                        break;

                    case "4":

                        if (playerBoard.shipsAlive == 0 && GAMEWON == true) // Check if game has finished
                        {
                            Console.WriteLine("\nGame won, please start a new game.");
                            break;
                        }
                        else if (playerBoard.shipsAlive == 0)
                        { // Check if player added a ship first
                            Console.WriteLine("\nNo ships deployed. Please add ship first.");
                            break;
                        }
                        else // if player has ships start attack turn
                        {
                            // Display start number of ships
                            if (attackTurn == false) { Console.WriteLine("\nStarting game with " + playerBoard.shipsAlive + " ships."); }

                            attackTurn = true;

                            FireShot(playerBoard, enemyBoard); // Get coordinates from player to fire
                        }
                        break;

                    case "5":
                        // exit game
                        Console.WriteLine("Game Exited Successfully");
                        break;

                    default:
                        /* Validate player Input */
                        Console.WriteLine("Option not valid\n");

                        break;
                }
            }
        }

        /** 
	     *  Method to reset a board.
	     *  
	     *  @return Board board = the board set to initial state
	     */
        public static Board InitialiseBoard()
        {
            GAMEWON = false;
            Board board = new Board(NUMBEROFSHIPS);
            return board;

        }

        /** 
         *  Method to print a board.
         *  @param Board board = the board to print
         *  
         */
        public static void PrintBoard(Board board)
        {

            for (int i = 1; i < 101; i++) // for 100 times
            {

                Console.Write(board.Cells[i - 1].status + " "); // print status of cell

                if (i % 10 == 0) // every 10 cells display new row
                {
                    Console.Write("\n");

                }

            }

        }

        /** 
       *  Method to validate ship deployment on the board.
       *  @param Board board = the board in which the ship is added
       *  @param int row = the starting row of the ship
       *  @param int col = the starting col of the ship
       *  @param int size = the size of the ship
       *  @param int face = the orientation of the ship
       *  
       *  @return bool isValid = to check against board state
       */
        public static bool validateShip(Board board, int row, int col, int size, int face)
        {
            bool isValid = true;

            //Check if player has finished ships to deploy
            if (board.ships == 0)
            {
                isValid = false;
                Console.WriteLine("\nAll Ships already deployed.");
                return isValid;

            }

            //Check ship size
            if (size > MAXSHIPSIZE)
            {
                isValid = false;
                Console.WriteLine("\nShip is too big.");
                return isValid;

            }

            if (face == 1) // if orientation is vertical
            {
                if (row - size < 0)
                {
                    isValid = false;
                }
            }

            else if (face == 2)
            { //if orientation is horizontal

                if (col + size > 10)
                {
                    isValid = false;
                }
            }
            else
            {
                Console.WriteLine("Alignment not valid.");

            }

            return isValid;


        }

        /** 
       *  Method to deploy ship on the board after checking if any cell is occupied by other ships.
       *  @param Board board = the board in which the ship is added
       *  @param int row = the starting row of the ship
       *  @param int col = the starting col of the ship
       *  @param int size = the size of the ship
       *  @param int face = the orientation of the ship
       *  
       *  @return bool isValid = to check against board state
       */
        public static void addShip(Board board, int row, int col, int size, int face)
        {
            int cell_index; //Cell index in the list of cells of the board
            bool thereIsAnotherShip = false;
            int shipsRemaining = board.ships;
            int shipsAlive = board.shipsAlive;
            List<int> shipPositions = new List<int>();


            cell_index = (row - 1) * 10 + col - 1;

            // ORIENTATION VERTICAL
            if (face == 1)
            {

                for (int i = cell_index; i > cell_index - size * 10; i = i - 10) //check if there is a ship already
                {
                    if (board.Cells[i].status == "S") // if there is a ship already
                    {
                        thereIsAnotherShip = true; // flag to true and print message
                        Console.WriteLine("\nShip not Valid");
                        Console.WriteLine("There is another ship already");
                        break;

                    }

                }

                if (thereIsAnotherShip == false) // if there is not another ship
                {
                    for (int i = cell_index; i > cell_index - size * 10; i = i - 10) //deploy ship facing north
                    {
                        board.Cells[i].status = "S"; // Update board cells
                        shipPositions.Add(i); // Record ship positions
                    }


                    Ship newShip = new Ship(size, shipPositions); // create ship
                    board.shipsDeployed.Add(newShip); // Add ship to board
                    board.ships = shipsRemaining - 1;//Update ships remaining
                    board.shipsAlive = shipsAlive + 1;//Update ships deployed
                }

            }

            // ORIENTATION HORIZONTAL

            else if (face == 2)
            {
                for (int i = cell_index; i < cell_index + size; i++) //check if there is a ship already
                {
                    if (board.Cells[i].status == "S") // if there is a ship already
                    {
                        thereIsAnotherShip = true; // flag to true and print message
                        Console.WriteLine("\nShip not Valid");
                        Console.WriteLine("There is another ship already");
                        break;

                    }

                }

                if (thereIsAnotherShip == false) // if there is not another ship
                {
                    for (int i = cell_index; i < cell_index + size; i++) //deploy ship facing east
                    {
                        board.Cells[i].status = "S";

                        shipPositions.Add(i);

                    }
                    Ship newShip = new Ship(size, shipPositions); // create ship
                    board.shipsDeployed.Add(newShip); // Add ship to board
                    board.ships = shipsRemaining - 1;//Update ships remained for deployment 
                    board.shipsAlive = shipsAlive + 1;//Update ships deployed
                }
            }

        }

        /** 
      *  Method to fire a shot to enemy, it records shots in enemy board (a copy of the player board)
      *  @param Board enemyBoard = for functionality, this is a copy of the player board
      *  @param Board playerBoard = the player board
      *
      *
      */
        public static void FireShot(Board playerBoard, Board enemyBoard)
        {
            int col, row, cellIndex; //Coordinates of the shot

            bool hit = false; // flag to mark if ship has been hit

            //Get coordinates from player input
            Console.WriteLine("\nPlease select row");
            row = int.Parse(Console.ReadLine());
            Console.WriteLine("\nPlease select column");
            col = int.Parse(Console.ReadLine());

            cellIndex = (row - 1) * 10 + col - 1; // calculate cell index

            // Check shot

            for (int i = 0; i < playerBoard.shipsDeployed.Count; i++) //loop all ships
            {
                for (int j = 0; j < playerBoard.shipsDeployed[i].shipPositions.Count; j++) //loop positions of each ship
                {

                    if (cellIndex == playerBoard.shipsDeployed[i].shipPositions[j]) // If shot hits the ship
                    {
                        if (playerBoard.shipsDeployed[i].isAlive == "no") // check if the ship has been destroyed already
                        {
                            Console.WriteLine("\nShip has been destroyed already!");
                            hit = true;
                            break;

                        }
                        if (enemyBoard.Cells[cellIndex].status == "H")
                        {
                            Console.WriteLine("\nShip was hit at this location already!");
                            hit = true;
                            break;
                        }

                        // if ship hasn't been destroyed or hit already continue

                        hit = true;
                        Console.WriteLine("\nShip has been hit");
                        playerBoard.shipsDeployed[i].health -= 1; // update health of the ship
                        enemyBoard.Cells[cellIndex].status = "H"; //update board

                        if (playerBoard.shipsDeployed[i].health == 0) // check if ship has no health left
                        {
                            Console.WriteLine("\nShip has been destroyed");
                            playerBoard.shipsDeployed[i].isAlive = "no";
                            hasBeenDestroyed(enemyBoard, playerBoard.shipsDeployed[i]); // update board with ship destroyed
                            playerBoard.shipsAlive -= 1; // update player's remaining ship

                            if (playerBoard.shipsAlive == 0) // if player does not have any ship left
                            {
                                GAMEWON = true;
                                Console.WriteLine("\nGame Won!");

                            }

                        }


                    }

                }

            }

            if (hit == false)
            {
                Console.WriteLine("Missed!");
                enemyBoard.Cells[cellIndex].status = "M"; //update board
            }




        }

        /** 
     *  Method that marks the ship has destroyed
     *  
     *  @param Board enemyBoard = the enemy board
     *  @param Ship ship = the ship that has been destroyed
     *
     */

        public static void hasBeenDestroyed(Board board, Ship ship)
        {
            for (int i = 0; i < ship.shipPositions.Count; i++)
            {
                board.Cells[ship.shipPositions[i]].status = "D";
            }
        }

    }
}
