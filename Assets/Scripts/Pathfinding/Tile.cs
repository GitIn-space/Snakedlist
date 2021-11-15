using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FG
{
    public class Tile
    {
        public Vector3 loc;
        public bool passable;
        public Tile[] neighbours;

        public Tile parent;
        public float cost;
        public float distance;
        public float costdistance;

        public Tile()
        {
            this.loc = Vector3.zero;
            this.passable = false;
            this.neighbours = new Tile[4];

            parent = null;
            cost = 0f;
            distance = 0f;
            costdistance = 0f;
        }

        public Tile(Vector3 loc, bool passable)
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