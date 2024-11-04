using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class sceneStart : MonoBehaviour
{
    GameObject[] transition;

    void Start()
    {
        transition = GameObject.FindGameObjectsWithTag("tile");
        StartCoroutine(enter());
    }

    IEnumerator enter() 
    {
        var k = 0f;
        while (k < 0.4f)
        {
            k += Time.deltaTime;
            foreach (var tran in transition) {
                var tile = tran.gameObject.GetComponent<RectTransform>();
                tile.localScale = new Vector3 (0.4f - k, 0.4f - k, tile.localScale.z);
            }
            yield return 0;
        }

        foreach (var tran in transition)
        {
            var tile = tran.gameObject.GetComponent<RectTransform>();
            tile.localScale = new Vector3 (0f, 0f, tile.localScale.z);
        }     
    }
}
