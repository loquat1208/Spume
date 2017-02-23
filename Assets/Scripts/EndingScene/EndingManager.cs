using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class EndingManager : MonoBehaviour {
    public List<GameObject> Scene;
    public List<Sprite> OlderScene;
    public float SceneChangeTime = 8;
    private GameObject _fade;
    private float _timer;
    private int _now_scene;
    private int _select; 

	void Start( ) {
        _select = PlayerPrefs.GetInt( "Select" );
        _timer = 0;
        _now_scene = 0;
        _fade = GameObject.Find( "Fade" ).gameObject;
    }

	void Update( ) {
        _timer += Time.deltaTime;
        playScene( _now_scene );
        if ( _timer > SceneChangeTime ) {
            _timer = 0;
            if ( _now_scene < Scene.Count - 1 ) {
                _now_scene++;
            }
            StartCoroutine( "FadeIn" );
        }
	}

    void playScene( int scene_num ) {
		//各Scene
        switch ( scene_num ) {
            case 0:
                if ( _timer > SceneChangeTime / 2 ) {
                    GameObject.Find( "Logo" ).gameObject.GetComponent<Image>( ).color = new Color( 1, 1, 1, 1 );
                }
                break;
            case 3:
                if ( _select % 2 == 1 ) {
                    Scene[ _now_scene ].GetComponent<Image>( ).sprite = OlderScene[ 0 ];
                }
                break;
            case 4:
                if ( ( _select >> 1 ) % 2 == 1 ) {
                    Scene[ _now_scene ].GetComponent<Image>( ).sprite = OlderScene[ 1 ];
                }
                break;
            case 5:
                if ( ( _select >> 2 ) % 2 == 1 ) {
                    Scene[ _now_scene ].GetComponent<Image>( ).sprite = OlderScene[ 2 ];
                }
                break;
            case 13:
                PlayerPrefs.DeleteAll( );
                SceneManager.LoadScene( "CreditScene" );
                break;
        }
    }

    IEnumerator FadeIn( ) {
        for ( float i = 0; i <= 180; i += 2f ) {
            if ( i == 90 ) {
                Scene[ _now_scene ].SetActive( true );
            }
            _fade.GetComponentInChildren<Image>( ).color = new Color( 0, 0, 0, Mathf.Sin( i * Mathf.PI / 180f ) );
            yield return 0;
        }
    }
}
