using UnityEngine;
using System.Collections;

public class UIButtonAnimations : MonoBehaviour {
	public GameObject Page;
    public GameObject Back;

    public void PointEnterUP( ) {
		Page.transform.position += new Vector3( 0, 10, 0 );
		Back.transform.position += new Vector3( 0, 10, 0 );
		transform.position += new Vector3( 0, 10, 0 );
    }

    public void PointExitDown( ) {
		Page.transform.position += new Vector3( 0, -10, 0 );
		Back.transform.position += new Vector3( 0, -10, 0 );
		transform.position += new Vector3( 0, -10, 0 );
    }

    public void PointEnterRight( ) {
		Page.transform.position += new Vector3( 10, 0, 0 );
		Back.transform.position += new Vector3( 10, 0, 0 );
		transform.position += new Vector3( 10, 0, 0 );
    }

    public void PointExitLeft( ) {
		Page.transform.position += new Vector3( -10, 0, 0 );
		Back.transform.position += new Vector3( -10, 0, 0 );
		transform.position += new Vector3( -10, 0, 0 );
    }
}
