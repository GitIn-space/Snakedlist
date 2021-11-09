using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;

namespace FG
{
    public class Pathfinder : MonoBehaviour
    {
        [SerializeField] private GameObject edge;
        [SerializeField] private Vector2Int dims = new Vector2Int(9, 6);

        private Mygrid grid;
        private Transform[] edges = new Transform[4];

        private float Getdistance(Tile subject, Tile target)
        {
            return Vector2Int.Distance(subject.loc, target.loc);
        }

        public List<Vector2Int> Astar(Tile start, Tile goal)
        { 
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
            path.Add(new Vector2Int(start.loc.x, start.loc.y));
            path.Reverse();
            return path;
        }

        private void Awake()
        {
            grid = new Mygrid(dims);

            dims += Vector2Int.one;

            edges[0] = Instantiate(edge, new Vector3(dims.x, 0, 0), Quaternion.identity).transform;
            edges[0].localScale = new Vector3(1, dims.y * 2, 1);
            edges[1] = Instantiate(edge, new Vector3(-dims.x, 0, 0), Quaternion.identity).transform;
            edges[1].localScale = new Vector3(1, -dims.y * 2, 1);
            edges[2] = Instantiate(edge, new Vector3(0, dims.y, 0), Quaternion.identity).transform;
            edges[2].localScale = new Vector3(dims.x * 2, 1, 1);
            edges[3] = Instantiate(edge, new Vector3(0, -dims.y, 0), Quaternion.identity).transform;
            edges[3].localScale = new Vector3(-dims.x * 2, 1, 1);

            dims -= Vector2Int.one;
        }
    }
}