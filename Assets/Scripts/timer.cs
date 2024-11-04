using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class timer : MonoBehaviour
{
    public float trueTime = 0.00f;

    void Start()
    {
        gameObject.GetComponent<TextMeshProUGUI>().text = "00:00.00";
    }

    // Update is called once per frame
    void Update()
    {
        if (!GameObject.Find("Player").GetComponent<Movement>().gameOver)
            trueTime += Time.deltaTime;

        gameObject.GetComponent<TextMeshProUGUI>().text = Mathf.Floor(trueTime/60).ToString("00") + ":" + (trueTime % 60).ToString("00.00");
    }
}
