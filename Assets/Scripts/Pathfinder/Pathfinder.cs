using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;

namespace FG
{
    public class Pathfinder
    {
        [SerializeField] private Vector2Int dims = new Vector2Int(9, 6);

        private Transform[] edges = new Transform[4];
        private static Mygrid grid;

        static Pathfinder()
        {
            grid = GameObject.Find("Controller").GetComponent<Mygrid>();
        }

        public static List<Vector2Int> Astar(Vector3 origin, Vector3 end)
        {
            Tile start = grid.Gettile(origin); 
            Tile goal = grid.Gettile(end);

            Tile current = new Tile();
            List<Tile> open = new List<Tile>();
            List<Tile> closed = new List<Tile>();
            int cost = 0;

            open.Add(start);

            while (open.Count > 0)
            {
                float lowest = open.Min(l => l.costdistance);
                current = open.First(l => l.costdistance == lowest);

                closed.Add(current);

                open.Remove(current);

                if (closed.FirstOrDefault(l => l.loc.x == goal.loc.x && l.loc.y == goal.loc.y) != null)
                    break;

                List<Tile> neighbours = current.neighbours.ToList();
                cost++;

                foreach (Tile neighbourtile in neighbours)
                {
                    if (closed.FirstOrDefault(l => l.loc.x == neighbourtile.loc.x && l.loc.y == neighbourtile.loc.y) != null)
                        continue;

                    if (open.FirstOrDefault(l => l.loc.x == neighbourtile.loc.x && l.loc.y == neighbourtile.loc.y) == null)
                    {
                        neighbourtile.cost = cost;
                        neighbourtile.distance = Getdistance(neighbourtile, goal);
                        neighbourtile.costdistance = neighbourtile.cost + neighbourtile.distance;
                        neighbourtile.parent = current;

                        open.Insert(0, neighbourtile);
                    }
                    else
                    {
                        if (cost + neighbourtile.distance < neighbourtile.costdistance)
                        {
                            neighbourtile.cost = cost;
                            neighbourtile.costdistance = neighbourtile.cost + neighbourtile.distance;
                            neighbourtile.parent = current;
                        }
                    }
                }
            }
            List<Vector2Int> path = new List<Vector2Int>();
            while (current != start)
            {
                path.Add(new Vector2Int(current.loc.x, current.loc.y));
                current = current.parent;
            }
            path.Reverse();

            grid.Resetastar();

            return path;
        }

        private static float Getdistance(Tile subject, Tile target)
        {
            return Vector2Int.Distance(subject.loc, target.loc);
        }
    }
}