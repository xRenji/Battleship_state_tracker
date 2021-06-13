using System;
using System.Collections.Generic;

namespace Battleship
{
    public class Board
    {

        public List<Cell> Cells { get; set; } // Array to contain 100 board cells
        public int ships { get; set; } // Int ships defines Available ships to deploy
        public int shipsAlive { get; set; } // int shipsAlive defines ships on the enemy board
        public List<Ship> shipsDeployed = new List<Ship>();// Array to contain ships on the board


        /** 
         *  Constructor for board objects
         *  
         *  @param int ships = total number of ships available, set as a constant in program.cs
         *
         */
        public Board(int ships)
        {
            this.ships = ships;
            Cells = new List<Cell>(); 

            for (int i = 1; i <= 10; i++) // 10 rows
            {
                for (int j = 1; j <= 10; j++) // for 10 columns
                {
                    Cells.Add(new Cell(i, j, "-")); // Add cell with coordinates and status as args
                }
            }

            
        }

        

        
    }
}
