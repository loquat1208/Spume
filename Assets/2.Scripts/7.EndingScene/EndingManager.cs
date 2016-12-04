using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;

public class EndingManager : MonoBehaviour {
    private GameObject text;

	// Use this for initialization
	void Start () {
        text = GameObject.Find( "Text" ).gameObject;
        text.GetComponent<Text>( ).text = "Ending  " + PlayerPrefs.GetInt( "Select" ).ToString( );
    }
	
	// Update is called once per frame
	void Update () {
	    if ( Input.GetMouseButton( 0 ) ) {
            SceneManager.LoadScene( "CreditScene" );
        }
	}
}
