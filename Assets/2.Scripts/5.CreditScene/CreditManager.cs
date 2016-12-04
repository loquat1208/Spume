using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class CreditManager : MonoBehaviour {
	// Update is called once per frame
	void Update () {
	    if ( Input.GetMouseButtonUp( 0 ) ) {
            SceneManager.LoadScene( "TitleScene" );
        }
	}
}
