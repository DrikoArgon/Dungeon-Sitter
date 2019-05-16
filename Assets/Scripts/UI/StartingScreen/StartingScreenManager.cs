using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StartingScreenManager : MonoBehaviour {

    public Image fadeScreen;
    public float timeToFade = 1;
    private bool fading;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetMouseButtonDown(0) && !fading) {
            fading = true;
            StartCoroutine(FadeOut());
        }
	}

    IEnumerator FadeOut() {

        float elapsedTime = 0;

        while(elapsedTime < timeToFade) {

            elapsedTime += Time.deltaTime;

            fadeScreen.color = new Color(fadeScreen.color.r, fadeScreen.color.g, fadeScreen.color.b, elapsedTime / timeToFade);

            yield return null;
        }


        SceneManager.LoadScene(1);

    }
}
