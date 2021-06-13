using System;
namespace Battleship
{
    public class Cell
    {
        // Coordinates
        public int row { get; set; }
        public int column { get; set; }

        // To record status of cell, status of the cell: - = empty, S = Ship, M = Missed, H = Hit, D = Destroyed
        public string status { get; set; }

        /** 
        *  Constructor for Cell objects
        *  
        *  @param int row = row of the cell
        *  @param int col = col of the cell
        *  @param string status = the status of the cell
        *
        */
        public Cell(int row, int column, string status)
        {
            this.row = row;
            this.column = column;
            this.status = status;
        }

      
    }
}
