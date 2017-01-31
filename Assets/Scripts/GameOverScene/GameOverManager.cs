using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;

public class GameOverManager : MonoBehaviour {
    public GameObject MainCamera;
    public float PlayTime = 5f;

    private GameObject _game_over1;
    private GameObject _game_over2;
    private GameObject _sea;
    private GameObject _ship;
    private GameObject _bubble;
    private GameObject _text;
    private float _timer;

    void Awake( ) {
        _game_over1 = GameObject.Find( "GameOver1" ).gameObject;
        _game_over2 = GameObject.Find( "GameOver2" ).gameObject;
        _sea = GameObject.Find( "Sea" ).gameObject;
        _ship = GameObject.Find( "Ship" ).gameObject;
        _bubble = GameObject.Find( "Bubble" ).gameObject;
        _text = GameObject.Find( "Text" ).gameObject;
    }

    private void Start( ) {
        if ( PlayerPrefs.GetInt( "GameOver" ) != 0 ) {
            MainCamera.transform.position = new Vector3( 0, 0, -1f );
            MainCamera.GetComponent<Camera>( ).orthographicSize = 450f;
            _sea.transform.position = new Vector3( 0, -800f, 0 );
            _ship.transform.position = new Vector3( 0, -180, 0 );
            _game_over1.SetActive( false );
            _game_over2.SetActive( true );
        } else {
            MainCamera.transform.position = new Vector3( 0, -60f, -1f );
            MainCamera.GetComponent<Camera>( ).orthographicSize = 40f;
            _game_over1.SetActive( true );
            _game_over2.SetActive( false );
        }
    }

    void Update( ) {
        _timer += Time.deltaTime;
		//各GameOverのAnimation
        switch ( PlayerPrefs.GetInt( "GameOver" ) ) {
            case 0:
                if ( _timer < PlayTime ) {
                    MainCamera.transform.position = new Vector3( 0, _timer / PlayTime * 60f - 60f, -1f );
                    MainCamera.GetComponent<Camera>( ).orthographicSize = _timer / PlayTime * 410f + 40f;
                }
                break;
            case 1:
                if ( _timer < PlayTime / 5f ) {
                    _ship.transform.localEulerAngles = new Vector3( 0, 0, _timer * 5f / PlayTime * -30f );
                } else if ( _timer < PlayTime ) {
                    float time = ( _timer - PlayTime / 5f ) * 1.25f / PlayTime;
                    _sea.transform.position = new Vector3( 0, time * 800f - 800f );
                }
                if ( _timer > PlayTime * 0.6f && _timer <= PlayTime ) {
                    float time = ( _timer - PlayTime * 0.6f ) * 2.5f / PlayTime;
                    _text.transform.position = new Vector3( 0, time * 800f - 800f );
                }
                if ( _timer > PlayTime * 0.6f && _timer <= PlayTime + 2f ) {
                    float time = ( _timer - PlayTime * 0.6f ) * 5f / PlayTime;
                    _bubble.transform.position = new Vector3( 0, time * 100f - 240f );
                }
                if ( _timer >= PlayTime ) {
                    float time = ( _timer - PlayTime ) * 1.25f / PlayTime;
                    _ship.transform.position = new Vector3( 0, -time * 100f - 180f );
                    _ship.GetComponent<Image>().color = new Color( 1, 1, 1, 1 - _timer * 0.7f + PlayTime );
                    if ( 1 - _timer * 0.7f + PlayTime <= 0 ) {
                        _ship.SetActive( false ); 
                    }
                }
                break;  
        }
		//Mouse ClickでTitleに戻る
        if ( Input.GetMouseButtonUp( 0 ) ) {
            SceneManager.LoadScene( "TitleScene" );
            PlayerPrefs.DeleteAll( );
        }
    }
}
