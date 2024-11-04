using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine;

public class finishScreen : MonoBehaviour
{
    float pr;
    float k;

    void Start()
    {
        k = 0;
    }

    void Update()
    {
        if (GameObject.Find("Player").GetComponent<Movement>().gameOver)
        {
            float trueTime = GameObject.Find("Timer").GetComponent<timer>().trueTime;
            GameObject.Find("gameTime").GetComponent<TextMeshProUGUI>().text = Mathf.Floor(trueTime/60).ToString("00") + ":" + (trueTime % 60).ToString("00.00");

            var scene = SceneManager.GetActiveScene().name;
            var key = scene + "pr";

            if (PlayerPrefs.HasKey(key))
            {
                pr = PlayerPrefs.GetFloat(key);

                if (trueTime < pr)
                {
                    PlayerPrefs.SetFloat(key, trueTime);
                    pr = trueTime;
                }

            } else {
                pr = trueTime;
                PlayerPrefs.SetFloat(key, trueTime);
            }

            GameObject.Find("bestTime").GetComponent<TextMeshProUGUI>().text = Mathf.Floor(pr/60).ToString("00") + ":" + (pr % 60).ToString("00.00");
            
            if (k < 1.1f) {
                k += 2.2f * Time.deltaTime;
                gameObject.transform.localScale = new Vector3 (Mathf.Sqrt(k), Mathf.Sqrt(k), gameObject.transform.localScale.z);
            } else {
                gameObject.transform.localScale = new Vector3 (1.1f, 1.1f, gameObject.transform.localScale.z);
            }
        }
    }
}
