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
        [SerializeField] private float snakeinterval = 1f;

        private Slinklist<Snakebody> snake = new Slinklist<Snakebody>();
        private Pathfinder pathfinder;
        private List<Vector3> path = new List<Vector3>();

        private float eaten = 1f;
        private Vector3 food;

        private Foodcontroller foocon;

        public void Eat()
        {
            snake.Insert(1, new Snakebody(Instantiate(bodyfab, snake[0].transform.position, Quaternion.identity).transform));
            eaten++;

            List<Vector3> obs = Getobstacles();
            food = foocon.Spawn(obs);
            path = Pathfinder.Astar(snake[0].transform.position, food, obs, true);
        }

        private List<Vector3> Getobstacles()
        {
            List<Vector3> obs = new List<Vector3>();
            foreach (Snakebody each in snake)
                obs.Add(each.transform.position);

            return obs;
        }

        private IEnumerator Move()
        {
            Vector2 temp;
            while (true)
            {
                if (path.Count == 0)
                    path = Pathfinder.Astar(snake[0].transform.position, food, Getobstacles(), true);

                temp = path[0];
                path.RemoveAt(0);
                foreach (Snakebody each in snake)
                    (each.transform.position, temp) = (temp, each.transform.position);

                yield return new WaitForSeconds(snakeinterval);
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
            food = GameObject.Find("Foodprefab(Clone)").transform.position;
            path = Pathfinder.Astar(snake[0].transform.position, food);

            StartCoroutine(Move());
        }

        private void Update()
        {
            for (int c = 0; c < path.Count - 1; c++)
                Debug.DrawLine(new Vector3(path[c].x, path[c].y), new Vector3(path[c + 1].x, path[c + 1].y), Color.green);
        }
    }
}