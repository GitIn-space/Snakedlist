using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FG
{
    public class Mygrid : MonoBehaviour
    {
        private class Tile
        {
            public Vector2Int loc;
            public int passable;
            public Tile[] neighbours { get; set; }

            public Tile(Vector2Int loc, int passable)
            {
                this.loc = loc;
                this.passable = passable;
            }
        }

        private Tile[,] grid;
        private Vector2Int dims { get; }

        public Mygrid(Vector2Int dims)
        {
            grid = new Tile[dims.x, dims.y];
            this.dims = dims;

            for (int c = 0; c < this.dims.x; c++)
                for (int q = 0; q < this.dims.y; q++)
                    grid[c, q] = new Tile(new Vector2Int(c, q), 1);

            for (int c = 0; c < this.dims.x; c++)
                for (int q = 0; q < this.dims.y; q++)
                    grid[c, q].neighbours = new[] { grid[(c - 1) % dims.x, q], grid[(c - 1) % dims.x, q], grid[c, (q + 1) % dims.y], grid[c, (q - 1) % dims.y]};
        }

        
    }
}