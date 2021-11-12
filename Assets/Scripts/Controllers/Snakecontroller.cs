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

        public void Eat(Vector3 destination)
        {
            snake.Insert(1, new Snakebody(Instantiate(bodyfab, new Vector3(Mathf.Round(snake[0].transform.position.x), Mathf.Round(snake[0].transform.position.y)), Quaternion.identity).transform));
            eaten++;
            path = Pathfinder.Astar(snake[0].transform.position, destination);
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

                yield return new WaitForSeconds(snakeinterval / (eaten > 0f ? eaten + 1f / eaten : 1f));
            }
        }

        private void Awake()
        {
            snake.Add(new Snakebody(Instantiate(snakefab, transform.position, Quaternion.identity, transform).transform));
            pathfinder = new Pathfinder();
        }

        private void Start()
        {
            path = Pathfinder.Astar(snake[0].transform.position, GameObject.Find("Foodprefab(Clone)").transform.position);

            StartCoroutine(Move());
        }
    }
}