using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace FG
{
    public class Controls : MonoBehaviour
    {
        [SerializeField] private GameObject text;
        [SerializeField] private Snakecontroller contr;

        public void Pause()
        {
            Time.timeScale = 0;
            text.SetActive(true);
        }
        private void OnUp()
        {
            contr.Input(new Vector2(0, 1));
        }
        private void OnDown()
        {
            contr.Input(new Vector2(0, -1));
        }
        private void OnLeft()
        {
            contr.Input(new Vector2(-1, 0));
        }
        private void OnRight()
        {
            contr.Input(new Vector2(1, 0));
        }

        private void OnReset()
        {
            if (Time.timeScale == 0)
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
                Time.timeScale = 1;
            }
        }
    }
}