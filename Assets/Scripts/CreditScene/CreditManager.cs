using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class CreditManager : MonoBehaviour {
	public float Speed = 1;
	public GameObject Credit;
	private float _timer;

	void Update( ) {
		if ( _timer <= 2200f ) {
			_timer += Time.deltaTime * Speed;
			Credit.transform.position = new Vector3( 0f, _timer, 600 );
		}
		if ( Input.GetMouseButtonUp( 0 ) ) {
            SceneManager.LoadScene( "TitleScene" );
        }
	}
}
