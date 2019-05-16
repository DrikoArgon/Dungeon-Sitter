using UnityEngine;
using UnityEngine.UI;

public class ImageActivator : MonoBehaviour {

    // Use this for initialization

    private void Awake() {
        GetComponent<Image>().enabled = true;
    }

}
