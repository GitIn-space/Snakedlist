using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FG
{
    public class Bodycollision : MonoBehaviour
    {
        private void OnTriggerExit2D(Collider2D collision)
        {
            /*if (collision.CompareTag("Snake"))
                gameObject.GetComponent<CircleCollider2D>().isTrigger = false;*/
        }
    }
}