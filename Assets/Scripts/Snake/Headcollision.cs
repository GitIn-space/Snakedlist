using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FG
{
    public class Headcollision : MonoBehaviour
    {
        private Controls contr;
        private Snakecontroller snacon;

        private void OnCollisionEnter2D(Collision2D collision)
        {
            if (collision.collider.CompareTag("Wall") || collision.collider.CompareTag("Snake"))
                contr.Pause();
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.CompareTag("Food"))
            {
                snacon.Eat();
                Destroy(collision.gameObject);
            }
        }

        private void Awake()
        {
            contr = gameObject.GetComponentInParent<Controls>();
            snacon = gameObject.GetComponentInParent<Snakecontroller>();
        }
    }
}