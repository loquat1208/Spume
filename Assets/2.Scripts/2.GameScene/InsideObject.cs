using UnityEngine;
using System.Collections;

public class InsideObject : MonoBehaviour {
    [SerializeField]
    private GameObject Light;

	// Use this for initialization
	void Start () {
	
	}

    public void OnOffLight( ) {
        if ( Light.activeSelf ) {
            Light.SetActive( false );
        } else {
            Light.SetActive( true );
        }
    }
}