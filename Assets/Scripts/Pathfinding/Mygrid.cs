using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FG
{
    public class Mygrid : MonoBehaviour
    {
        [SerializeField] private GameObject edge;
        [SerializeField] private Vector2Int dims;

        private Tile[,] grid;
        private Transform[] edges = new Transform[4];

        public Tile Gettile(Vector3 pos)
        {
            return grid[(int) pos.x, (int) pos.y];
        }

        public void Resetastar()
        {
            for(int c = 0; c < dims.x; c++)
                for(int q = 0; q < dims.y; q++)
                {
                    grid[c, q].cost = 0;
                    grid[c, q].costdistance = 0;
                    grid[c, q].parent = null;
                }
        }

        private void Awake()
        {
            dims += Vector2Int.one;

            grid = new Tile[dims.x, dims.y];

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

                    Debug.DrawLine(new Vector3(grid[c, q].loc.x, grid[c, q].loc.y), new Vector3(grid[(c + 1) % dims.x, q].loc.x, grid[(c + 1) % dims.x, q].loc.y), Color.cyan, 5f);
                    Debug.DrawLine(new Vector3(grid[c, q].loc.x, grid[c, q].loc.y), new Vector3(grid[xpos, q].loc.x, grid[xpos, q].loc.y), Color.green, 5f);
                    Debug.DrawLine(new Vector3(grid[c, q].loc.x, grid[c, q].loc.y), new Vector3(grid[c, (q + 1) % dims.y].loc.x, grid[c, (q + 1) % dims.y].loc.y), Color.red, 5f);
                    Debug.DrawLine(new Vector3(grid[c, q].loc.x, grid[c, q].loc.y), new Vector3(grid[c, ypos].loc.x, grid[c, ypos].loc.y), Color.blue, 5f);
                }

            gameObject.transform.position = new Vector3(dims.x / 2, dims.y / 2);

            dims += Vector2Int.one * 2;

            edges[0] = Instantiate(edge, new Vector3(dims.x / 2, 0, 0) + transform.position, Quaternion.identity, transform).transform;
            edges[0].localScale = new Vector3(1, dims.y, 1);
            edges[1] = Instantiate(edge, new Vector3(-dims.x / 2, 0, 0) + transform.position, Quaternion.identity, transform).transform;
            edges[1].localScale = new Vector3(1, -dims.y, 1);
            edges[2] = Instantiate(edge, new Vector3(0, dims.y / 2, 0) + transform.position, Quaternion.identity, transform).transform;
            edges[2].localScale = new Vector3(dims.x, 1, 1);
            edges[3] = Instantiate(edge, new Vector3(0, -dims.y / 2, 0) + transform.position, Quaternion.identity, transform).transform;
            edges[3].localScale = new Vector3(-dims.x, 1, 1);

            dims -= Vector2Int.one * 2;
        }
    }
}