using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Loading : MonoBehaviour {
    private float _timer;
    private GameObject _ship;
    private GameObject _sea;

    void Awake( ) {
        _ship = GameObject.Find( "Ship" ).gameObject;
        _sea = GameObject.Find( "Sea" ).gameObject;
    }

    private void Start( ) {
        StartCoroutine( "LoadingScene" );
    }

    void Update( ) {
        _ship.transform.position += new Vector3( 2, 0, 0 );
        _sea.transform.position += new Vector3( 0, 0.5f * Mathf.Sin( _timer * 0.5f * Mathf.PI ), 0 );
    }

    IEnumerator LoadingScene( ) {
        switch ( PlayerPrefs.GetInt( "Scene" ) ) {
            case 0: //NewGame
                SceneManager.LoadSceneAsync( 2, LoadSceneMode.Single );
                break;
			case 2: //GameScene
				if ( PlayerPrefs.GetFloat( "Time" ) <= 12f * 60f ) {
					SceneManager.LoadSceneAsync( 2 + PlayerPrefs.GetInt( "EventNumber" ), LoadSceneMode.Single );
				} else {
					SceneManager.LoadSceneAsync( 2, LoadSceneMode.Single );
				}
                break;
            default:
                SceneManager.LoadSceneAsync( PlayerPrefs.GetInt( "Scene" ), LoadSceneMode.Single );
                break;
        }
        yield return 0;
    }
}
