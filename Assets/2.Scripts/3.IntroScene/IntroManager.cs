using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class IntroManager : MonoBehaviour {
	float _timer;
	float _timer_switch;
	float _event_time;
	int _event_num;

	public int FIRST_EVENT;
	public int SECOND_EVENT;
	public int THIRD_EVENT;
	public int FOURTH_EVENT;
	public int FIFTH_EVENT;
	public int SIXTH_EVENT;
	public int SEVENTH_EVENT;

    GameObject _yes_text;
	GameObject _no_text;
	GameObject _button;

	GameObject _text;

	// Use this for initialization
	void Start( ) { 
        _timer = 0;
		_timer_switch = 1;
		_event_time = FIRST_EVENT;
        _event_num = 1;
		_yes_text = GameObject.Find( "YesText" ).gameObject;
		_no_text = GameObject.Find( "NoText" ).gameObject;
		_yes_text.GetComponent<Text>( ).text = "Yes 0";
		_no_text.GetComponent<Text>( ).text = "No 0";
		_button = GameObject.Find( "Button" ).gameObject;

		_text = GameObject.Find( "Text" ).gameObject;
	}
	
	// Update is called once per frame
	void Update( ) {
        if ( _event_num > 6 ) {
            SceneManager.LoadScene( "GameScene" );
        }

        if ( _event_num > 6 && allDeath( ) ) {
            PlayerPrefs.SetInt( "GameOver", 0 );
            PlayerPrefs.SetInt( "LoadGame", 0 );
            PlayerPrefs.Save( );
            SceneManager.LoadScene( "GameOverScene" );
        }

        if ( _timer > 3 ) {
            SceneManager.LoadScene( "GameScene" );
        }
        
        limitFourChara( );

        _timer += _timer_switch * Time.deltaTime;
		drawButton( );
		Select( _event_time );
		_text.GetComponent<Text>( ).text = "Character " + _event_num.ToString( ) + "  " 
            + PlayerPrefs.GetInt( "Chara1Alive" ).ToString( )
            + PlayerPrefs.GetInt( "Chara2Alive" ).ToString( )
            + PlayerPrefs.GetInt( "Chara3Alive" ).ToString( )
            + PlayerPrefs.GetInt( "Chara4Alive" ).ToString( )
            + PlayerPrefs.GetInt( "Chara5Alive" ).ToString( )
            + PlayerPrefs.GetInt( "Chara6Alive" ).ToString( );
    }

    void limitFourChara( ) {
        if ( PlayerPrefs.GetInt( "Chara1Alive" ) + 
             PlayerPrefs.GetInt( "Chara2Alive" ) +
             PlayerPrefs.GetInt( "Chara3Alive" ) +
             PlayerPrefs.GetInt( "Chara4Alive" ) +
             PlayerPrefs.GetInt( "Chara5Alive" ) +
             PlayerPrefs.GetInt( "Chara6Alive" ) >= 4 ) {
            _timer_switch = 1;
        }
    }

    bool allDeath( ) {
        if ( PlayerPrefs.GetInt( "Chara1Alive" ) +
             PlayerPrefs.GetInt( "Chara2Alive" ) +
             PlayerPrefs.GetInt( "Chara3Alive" ) +
             PlayerPrefs.GetInt( "Chara4Alive" ) +
             PlayerPrefs.GetInt( "Chara5Alive" ) +
             PlayerPrefs.GetInt( "Chara6Alive" ) == 0 ) {
            return true;
        }
        return false;
    }

    void drawButton( ) {
		if ( _timer_switch == 1 ) {
			_button.SetActive( false );
		}
        if ( _timer_switch == 0 ) {
            _button.SetActive( true );
        } 
	}

	void Select( float time ) {
		if ( _timer < time ) {
			return;
		}
		_timer_switch = 0;
		
		switch ( _event_num ) {
			case 0:
				_event_time = SECOND_EVENT;
				break;
			case 1:
                _event_time = THIRD_EVENT;
                _yes_text.GetComponent<Text>( ).text = "Yes 1";
				_no_text.GetComponent<Text>( ).text = "No 1";
				break;
			case 2:
                _event_time = FOURTH_EVENT;
                _yes_text.GetComponent<Text>( ).text = "Yes 2";
				_no_text.GetComponent<Text>( ).text = "No 2";
				break;
			case 3:
                _event_time = FIFTH_EVENT;
                _yes_text.GetComponent<Text>( ).text = "Yes 3";
				_no_text.GetComponent<Text>( ).text = "No 3";
				break;
			case 4:
                _event_time = SIXTH_EVENT;
                _yes_text.GetComponent<Text>( ).text = "Yes 4";
				_no_text.GetComponent<Text>( ).text = "No 4";
				break;
			case 5:
                _event_time = SEVENTH_EVENT;
                _yes_text.GetComponent<Text>( ).text = "Yes 5";
				_no_text.GetComponent<Text>( ).text = "No 5";
				break;
		}
	}

	public int getEventNum( ) {
		return _event_num;
	}

	public void setEventNum( int num ) {
		_event_num = num;
	}

	public void setTimerSwitch( int num ) {
		_timer = 0;
		_timer_switch = num;
	}
}
