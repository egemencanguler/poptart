using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class StartButton : MonoBehaviour {

	// Use this for initialization
	void Start () {
        GetComponent<Button>().onClick.AddListener(OnButtonClick);
		
	}
    void OnButtonClick(){
        SceneManager.LoadScene("main");

    }
	// Update is called once per frame
	void Update () {
		
	}
}
