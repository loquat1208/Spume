using UnityEngine;
using System.Collections;

public class TitleManager : MonoBehaviour {
    private GameObject load;
	private GameObject mark;
	// Use this for initialization
	void Start ( ) {
		mark = GameObject.Find( "Mark" ).gameObject;
        load = GameObject.Find( "LoadGame" ).gameObject;
    }
	
	// Update is called once per frame
	void Update ( ) {
        if ( PlayerPrefs.GetInt( "LoadGame" ) == 1 ) {
            load.SetActive( true );
        } else {
            load.SetActive( false );
        }
    }

	public void PointUp( ) {
		Vector3 pos = mark.transform.position;
		pos.y = transform.position.y;
		mark.transform.position = pos;
	}
}
