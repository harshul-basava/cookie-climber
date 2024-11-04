using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class loadScene : MonoBehaviour
{
    GameObject[] transition;

    void Start()
    {
        transition = GameObject.FindGameObjectsWithTag("tile");
    }

    public void load()
    {
        StartCoroutine(exit());
    }

    IEnumerator exit() 
    {
        var k = 0f;
        while (k < 0.4f)
        {
            k += Time.deltaTime;
            foreach (var tran in transition) {
                var tile = tran.gameObject.GetComponent<RectTransform>();
                tile.localScale = new Vector3 (k, k, tile.localScale.z);
            }
            yield return 0;
        }

        foreach (var tran in transition)
        {
            var tile = tran.gameObject.GetComponent<RectTransform>();
            tile.localScale = new Vector3 (0.4f, 0.4f, tile.localScale.z);
        }     

        SceneManager.LoadScene(gameObject.name);
    }
}
