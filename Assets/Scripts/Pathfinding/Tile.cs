using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FG
{
    public class Tile
    {
        public Vector2Int loc;
        public int passable;
        public Tile[] neighbours;

        public Tile parent;
        public float cost;
        public float distance;
        public float costdistance;

        public Tile()
        {
            this.loc = Vector2Int.zero;
            this.passable = 0;
            this.neighbours = new Tile[4];

            parent = null;
            cost = 0f;
            distance = 0f;
            costdistance = 0f;
        }

        public Tile(Vector2Int loc, int passable)
        {
            this.loc = loc;
            this.passable = passable;
            this.neighbours = new Tile[4];

            parent = null;
            cost = 0f;
            distance = 0f;
            costdistance = 0f;
        }
    }
}