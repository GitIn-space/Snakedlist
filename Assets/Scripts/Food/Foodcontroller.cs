using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FG
{
    public class Foodcontroller : MonoBehaviour
    {
        [SerializeField] private GameObject foodfab;

        public Vector3 Respawn()
        {
            return Instantiate(foodfab, new Vector2(Random.Range(0, 18), Random.Range(0, 12)), Quaternion.identity, transform).transform.position;
        }

        private void Awake()
        {
            Instantiate(foodfab, new Vector2(Random.Range(0, 18), Random.Range(0, 12)), Quaternion.identity, transform);
        }
    }
}