using System;
using System.Collections.Generic;

namespace Battleship
{
    public class Ship
    {

        public int health { get; set; } // health of the ship represents also the size
        public string isAlive = "yes"; // ship alive or not
        public List<int> shipPositions = new List<int>(); // List to contain all coordinates of a ship


        /** 
         *  Constructor for Ship objects
         *  
         *  @param int health = the health of the ship, to track when it gets destroyed 
         *  (is equal to the size given by the player)
         *  
         *  @param List<int>positions = the index positions of the ship on the board
         *
         */


        public Ship(int health, List<int> positions)
        {
            this.health = health;
            for (int i = 0;i<positions.Count;i++)
            
            {
                //Console.WriteLine(positions[i]); DEBUG SHIP POSITION

                shipPositions.Add(positions[i]);
            }
        }

       

        
    }
}
