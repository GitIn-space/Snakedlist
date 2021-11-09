using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FG
{
    public class Snakecontroller : MonoBehaviour
    {
        private class Snakebody
        {
            public Rigidbody2D body;
            public Vector2 dir;

            public Snakebody(Rigidbody2D body, Vector2 dir)
            {
                this.body = body;
                this.dir = dir;
            }
        }

        [SerializeField] private GameObject bodyfab;
        [SerializeField] private GameObject foodfab;
        [SerializeField] private GameObject snakefab;

        private Slinklist<Snakebody> snake = new Slinklist<Snakebody>();

        private Vector2 dir = new Vector2(0, 0);
        private float snakeinterval = 1f;
        private float foodinterval = 5f;
        private float eaten = 1f;

        public void Input(Vector2 dir)
        {
            if(dir != this.dir * -1)
                this.dir = dir;
        }

        public void Eat()
        {
            snake.Insert(1, new Snakebody(Instantiate(bodyfab, new Vector3(Mathf.Round(snake[0].body.transform.position.x), Mathf.Round(snake[0].body.transform.position.y)), Quaternion.identity).GetComponent<Rigidbody2D>(), Vector2.zero));
            eaten++;
        }

        private IEnumerator Move()
        {
            while (true)
            {
                Vector2 tempdir = dir;
                foreach (Snakebody each in snake)
                {
                    each.body.velocity = new Vector3(tempdir.x, tempdir.y) * (eaten > 0f ? eaten + 1f / eaten : 1f);
                    (tempdir, each.dir) = (each.dir, tempdir);
                }

                yield return new WaitForSeconds(snakeinterval / (eaten > 0f ? eaten + 1f / eaten : 1f));
            }
        }

        private IEnumerator Food()
        {
            while (true)
            {
                Instantiate(foodfab, new Vector2(Random.Range(-9, 9), Random.Range(-6, 6)), Quaternion.identity);
                yield return new WaitForSeconds(foodinterval / (eaten > 0f ? eaten + 1f / eaten : 1f));
            }
        }

        private void Awake()
        {
            snake.Add(new Snakebody(Instantiate(snakefab, Vector2.zero, Quaternion.identity, transform).GetComponent<Rigidbody2D>(), Vector2.zero));

            StartCoroutine(Move());
            StartCoroutine(Food());
        }
    }
}