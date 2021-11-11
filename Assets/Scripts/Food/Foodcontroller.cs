using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FG
{
    public class Foodcontroller : MonoBehaviour
    {
        [SerializeField] private GameObject foodfab;

        public void Respawn()
        {
            Instantiate(foodfab, new Vector2(Random.Range(0, 18), Random.Range(0, 12)), Quaternion.identity, transform);
        }

        private void Awake()
        {
            Instantiate(foodfab, new Vector2(Random.Range(0, 18), Random.Range(0, 12)), Quaternion.identity, transform);
        }
    }
}