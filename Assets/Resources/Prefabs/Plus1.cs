using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public enum PLUSCOLOR {
	GREEN,
	BLUE,
	RED,
}

public class Plus1 : MonoBehaviour {
	public Sprite Green;
	public Sprite Blue;
	public Sprite Red;

	private float _timer;
	private PLUSCOLOR _color;

	// Use this for initialization
	void Start () {
        _timer = 0;
    }
	
	// Update is called once per frame
	void Update () {
        _timer += Time.deltaTime;
        gameObject.transform.position += new Vector3( 0, _timer, 0 );

		switch (_color) {
		case PLUSCOLOR.GREEN:
			gameObject.GetComponent<Image> ().sprite = Green;
			break;
		case PLUSCOLOR.BLUE:
			gameObject.GetComponent<Image> ().sprite = Blue;
			break;
		case PLUSCOLOR.RED:
			gameObject.GetComponent<Image> ().sprite = Red;
			break;
		}

        if ( _timer >= 2.0f ) {
            Destroy( gameObject );
        }
    }

	public void setColor( PLUSCOLOR color ) {
		_color = color;
	}
}
