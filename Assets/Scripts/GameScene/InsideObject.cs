using UnityEngine;
using System.Collections;

public class InsideObject : MonoBehaviour {
    [SerializeField]
    private GameObject Light;

	//insideにある電気
    public void OnOffLight( ) {
        if ( Light.activeSelf ) {
            Light.SetActive( false );
        } else {
            Light.SetActive( true );
        }
    }
}