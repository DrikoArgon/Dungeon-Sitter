using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour {

    public static UIManager instance;

    public GameObject enlargedMinimap;
    public GameObject notificationPanel;

    public float notificationTime;

    private Coroutine notificationCoroutine;

    private void Awake() {
        if(instance == null) {
            instance = this;
        }
    }

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

        if (Input.GetKeyDown(KeyCode.Q)) {
            ShowEnlargedMinimap();
        }

        if (Input.GetKeyUp(KeyCode.Q)) {
            HideEnlargedMinimap();
        }

    }

    public void ShowEnlargedMinimap() {
        enlargedMinimap.SetActive(true);
    }

    public void HideEnlargedMinimap() {
        enlargedMinimap.SetActive(false);
    }

    public void ShowEnemyEnterNotification() {

        notificationPanel.GetComponent<Animator>().Play("Appear", -1, 0f);

        if(notificationCoroutine != null) {
            StopCoroutine(notificationCoroutine);
        }

        notificationCoroutine = StartCoroutine(KeepNotificationOpen());
    }

    IEnumerator KeepNotificationOpen() {

        float elapsedTime = 0;

        while(elapsedTime < notificationTime) {

            elapsedTime += Time.deltaTime;

            yield return null;
        }

        notificationPanel.GetComponent<Animator>().Play("Disappear");

    }
}
