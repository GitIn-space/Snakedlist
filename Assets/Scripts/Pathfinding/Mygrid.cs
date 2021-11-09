using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FG
{
    public class Mygrid
    {
        private Tile[,] grid;
        private Vector2Int dims;
        private Pathfinder pathfinder;

        public Mygrid(Vector2Int dims)
        {
            grid = new Tile[dims.x, dims.y];
            this.dims = dims;

            for (int c = 0; c < this.dims.x; c++)
                for (int q = 0; q < this.dims.y; q++)
                    grid[c, q] = new Tile(new Vector2Int(c, q), 1);

            int xpos = 0;
            int ypos = 0;
            for (int c = 0; c < this.dims.x; c++)
                for (int q = 0; q < this.dims.y; q++)
                {
                    xpos = c == 0 ? dims.x - 1 : c - 1;
                    ypos = q == 0 ? dims.y - 1 : q - 1;

                    grid[c, q].neighbours = new[] { grid[(c + 1) % dims.x, q], grid[xpos, q], grid[c, (q + 1) % dims.y], grid[c, ypos] };
                }
        }
    }
}