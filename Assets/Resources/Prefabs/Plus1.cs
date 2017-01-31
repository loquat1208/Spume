using UnityEngine;
using System.Collections;

public class Plus1 : MonoBehaviour {
    private float _timer;

	// Use this for initialization
	void Start () {
        _timer = 0;
    }
	
	// Update is called once per frame
	void Update () {
        _timer += Time.deltaTime;
        gameObject.transform.position += new Vector3( 0, _timer, 0 );

        if ( _timer >= 2.0f ) {
            Destroy( gameObject );
        }
    }
}
