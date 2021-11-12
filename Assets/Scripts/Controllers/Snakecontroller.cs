using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FG
{
    public class Snakecontroller : MonoBehaviour
    {
        private class Snakebody
        {
            public Transform transform;

            public Snakebody(Transform transform)
            {
                this.transform = transform;
            }
        }

        [SerializeField] private GameObject bodyfab;
        [SerializeField] private GameObject snakefab;

        private Slinklist<Snakebody> snake = new Slinklist<Snakebody>();
        private Pathfinder pathfinder;
        private List<Vector2Int> path = new List<Vector2Int>();

        private float snakeinterval = 1f;
        private float eaten = 1f;

        private Foodcontroller foocon;

        public void Eat()
        {
            snake.Insert(1, new Snakebody(Instantiate(bodyfab, new Vector3(snake[0].transform.position.x, snake[0].transform.position.y), Quaternion.identity).transform));
            eaten++;

            List<Vector3> obs = new List<Vector3>();
            foreach (Snakebody each in snake)
                obs.Add(each.transform.position);

            path = Pathfinder.Astar(snake[0].transform.position, foocon.Spawn(obs), obs);
        }

        private IEnumerator Move()
        {
            Vector2 temp;
            while (true)
            {
                if (path.Count != 0)
                {
                    temp = path[0];
                    path.RemoveAt(0);
                    foreach (Snakebody each in snake)
                        (each.transform.position, temp) = (temp, each.transform.position);
                }
                else
                {
                    temp = -(snake[1].transform.position - snake[0].transform.position);
                    foreach (Snakebody each in snake)
                        (each.transform.position, temp) = (temp, each.transform.position);
                }

                yield return new WaitForSeconds(snakeinterval / (eaten > 0f ? eaten + 1f / eaten : 1f));
            }
        }

        private void Awake()
        {
            foocon = gameObject.GetComponent<Foodcontroller>();

            snake.Add(new Snakebody(Instantiate(snakefab, transform.position, Quaternion.identity, transform).transform));
            pathfinder = new Pathfinder();
        }

        private void Start()
        {
            path = Pathfinder.Astar(snake[0].transform.position, GameObject.Find("Foodprefab(Clone)").transform.position);

            StartCoroutine(Move());
        }

        private void Update()
        {
            for (int c = 0; c < path.Count - 1; c++)
                Debug.DrawLine(new Vector3(path[c].x, path[c].y), new Vector3(path[c + 1].x, path[c + 1].y), Color.green);
        }
    }
}