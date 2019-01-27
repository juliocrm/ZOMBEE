using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IntroScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(_Run());
    }

    private IEnumerator _Run()
    {
        yield return new WaitForSeconds(35f);

        UnityEngine.SceneManagement.SceneManager.LoadScene(1);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.anyKey)
        {

            UnityEngine.SceneManagement.SceneManager.LoadScene(1);
        }
    }
}
