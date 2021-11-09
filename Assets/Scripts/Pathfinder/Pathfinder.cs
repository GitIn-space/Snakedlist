using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace FG
{
    public class Pathfinder : MonoBehaviour
    {
        private Mygrid grid;

        private float Getdistance(Vector2Int subject, Vector2Int target)
        {
            return Vector2Int.Distance(subject, target);
        }

        public List<Vector2Int> Astar(Vector2Int location, Vector2Int target)
        {
            Vector2Int start = new Vector2Int();
            Vector2Int goal = new Vector2Int();

            start.x = location.x;
            start.y = location.y;

            goal.x = target.x;
            goal.y = target.y;

            Vector2Int current = new Vector2Int();
            List<Vector2Int> open = new List<Vector2Int>();
            List<Vector2Int> closed = new List<Vector2Int>();
            int cost = 0;

            open.Add(start);

            while (open.Count > 0)
            {
                float lowest = open.Min(l => l.costdistance);
                current = open.First(l => l.costdistance == lowest);

                closed.Add(current);

                open.Remove(current);

                if (closed.FirstOrDefault(l => l.x == goal.x && l.y == goal.y) != null)
                    break;

                List<Vector2Int> neighbours = Getneighbours(current.x, current.y);
                cost++;

                foreach (Vector2Int neighbourtile in neighbours)
                {
                    if (closed.FirstOrDefault(l => l.x == neighbourtile.x && l.y == neighbourtile.y) != null)
                        continue;

                    if (open.FirstOrDefault(l => l.x == neighbourtile.x
                            && l.y == neighbourtile.y) == null)
                    {
                        neighbourtile.cost = cost;
                        neighbourtile.distance = Getdistance(neighbourtile, goal);
                        neighbourtile.costdistance = neighbourtile + neighbourtile.distance;
                        neighbourtile.Parent = current;

                        open.Insert(0, neighbourtile);
                    }
                    else
                    {
                        if (cost + neighbourtile.distance < neighbourtile.costdistance)
                        {
                            neighbourtile.cost = cost;
                            neighbourtile.costdistance = neighbourtile.cost + neighbourtile.distance;
                            neighbourtile.Parent = current;
                        }
                    }
                }
            }
            List<Vector2Int> path = new List<Vector2Int>();
            while (current != start)
            {
                path.Add(new Vector2Int(current.x, current.y));
                current = current.Parent;
            }
            path.Add(new Vector2Int(start.x, start.y));
            path.Reverse();
            return path;
        }

        private void Awake()
        {
            grid = new Mygrid(new Vector2Int(10, 10));
        }
    }
}