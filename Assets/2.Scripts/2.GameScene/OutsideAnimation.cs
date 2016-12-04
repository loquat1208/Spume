using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class OutsideAnimation : MonoBehaviour {
    [SerializeField]
    private List<float> SeaAmpplitude = new List<float>( );
    [SerializeField]
    private float CharacterAmpplitude;
    [SerializeField]
    private float ShipAmpplitude;
    [SerializeField]
    private List<float> CludeSpeed = new List<float>( );
    [SerializeField]
    private float MoonRadius;

    private float _timer;
    private List<GameObject> _morning_sea = new List<GameObject>( );
    private List<GameObject> _sunset_sea = new List<GameObject>( );
    private List<GameObject> _night_sea = new List<GameObject>( );
    private List<GameObject> _morning_clude = new List<GameObject>( );
    private List<GameObject> _night_star = new List<GameObject>( );
    private List<GameObject> _sunset_clude = new List<GameObject>( );
    private GameObject _chara;
    private GameObject _ship;
    private GameObject _moon;
    private GameObject _b_morning_bg;
    private GameObject _b_night_bg;
    private GameObject _f_morning_bg;
    private GameObject _f_sunset_bg;
    private GameObject _f_night_bg;
    private GameSystem _game_system;
	private GameObject _event_object;
	private EventData _event_data;

    // Use this for initialization
    void Awake( ) {
        for ( int i = 1; i < 6; i++ ) {
            GameObject game_object = GameObject.Find( "MorningSea0" + i.ToString( ) ).gameObject;
            GameObject game_object1 = GameObject.Find( "SunsetSea0" + i.ToString( ) ).gameObject;
            GameObject game_object2 = GameObject.Find( "NightSea0" + i.ToString( ) ).gameObject;
            _morning_sea.Add( game_object );
            _sunset_sea.Add( game_object1 );
            _night_sea.Add( game_object2 );
        }
        for ( int i = 1; i < 5; i++ ) {
            GameObject game_object = GameObject.Find( "Morning_Cloud0" + i.ToString( ) ).gameObject;
            GameObject game_object1 = GameObject.Find( "Night_Star0" + i.ToString( ) ).gameObject;
            _morning_clude.Add( game_object );
            _night_star.Add( game_object1 );
        }

        for ( int i = 1; i < 3; i++ ) {
            GameObject game_object1 = GameObject.Find( "Sunset_Cloud0" + i.ToString( ) ).gameObject;
            _sunset_clude.Add( game_object1 );
        }

        _chara = GameObject.Find( "Character" ).gameObject;
        _ship = GameObject.Find( "Ship" ).gameObject;
        _moon = GameObject.Find( "Moon" ).gameObject;

        _b_morning_bg = GameObject.Find( "B_MorningBg" ).gameObject;
        _b_night_bg = GameObject.Find( "B_NightBg" ).gameObject;
        _f_morning_bg = GameObject.Find( "F_MorningBg" ).gameObject;
        _f_sunset_bg = GameObject.Find( "F_SunsetBg" ).gameObject;
        _f_night_bg = GameObject.Find( "F_NightBg" ).gameObject;

        _game_system = GameObject.Find( "GameSystem" ).gameObject.GetComponent<GameSystem>( );
		_event_data = GameObject.Find( "EventSystem" ).gameObject.GetComponent<EventData>( );
		_event_object = GameObject.Find( "EventObject" ).gameObject;
    }
	
	// Update is called once per frame
	void Update ( ) {
        if ( _game_system.getLayer( ) != LAYER.OUTSIDE ) {
            return;
        }
        _timer += Time.deltaTime;
        updateMove( );
        updateBg( );
		WaveEventObject( );
    }

    void updateMove( ) {
        for ( int i = 0; i < 5; i++ ) {
            WaveObject( _morning_sea[ i ], SeaAmpplitude[ i ], _timer, i + 1, -150 );
            WaveObject( _sunset_sea[ i ], SeaAmpplitude[ i ], _timer, i + 1, -150 );
            WaveObject( _night_sea[ i ], SeaAmpplitude[ i ], _timer, i + 1, -150 );
        }

        WaveObject( _chara, CharacterAmpplitude, _timer, 2, 40 );
        WaveObject( _ship, ShipAmpplitude, _timer, 2, -350 );

        for ( int i = 0; i < _morning_clude.Count; i++ ) {
            CludeMove( _morning_clude[ i ], CludeSpeed[ i ] );
        }
        for ( int i = 0; i < _sunset_clude.Count; i++ ) {
            CludeMove( _sunset_clude[ i ], CludeSpeed[ i ] );
        }

        float _moon_pos_x = -MoonRadius * Mathf.Sin( ( _game_system.getTime( ) * 0.5f ) * Mathf.PI / 360 );
        float _moon_pos_y = MoonRadius * Mathf.Cos( ( _game_system.getTime( ) * 0.5f ) * Mathf.PI / 360 ) - 450.0f;
        _moon.transform.position = new Vector3( _moon_pos_x, _moon_pos_y, 0 );
    }

    void updateBg( ) {
        if ( _game_system.getTime( ) <= 18 * 60 ) {
            float timer = ( _game_system.getTime( ) - 12 * 60 ) / ( 6 * 60 );
            _b_morning_bg.GetComponent<Image>( ).color = new Color( 255, 255, 255, 1 - timer );
            _f_morning_bg.GetComponent<Image>( ).color = new Color( 255, 255, 255, 1 - timer );
            _f_sunset_bg.GetComponent<Image>( ).color = new Color( 255, 255, 255, timer );
            _b_night_bg.GetComponent<Image>( ).color = new Color( 255, 255, 255, 0 );
            _f_night_bg.GetComponent<Image>( ).color = new Color( 255, 255, 255, 0 );

            for ( int i = 0; i < _morning_clude.Count; i++ ) {
                _morning_clude[ i ].GetComponent<Image>( ).color = new Color( 255, 255, 255, 1 - timer );
            }
            for ( int i = 0; i < _sunset_clude.Count; i++ ) {
                _sunset_clude[ i ].GetComponent<Image>( ).color = new Color( 255, 255, 255, timer );
            }
            for ( int i = 0; i < _morning_sea.Count; i++ ) {
                _morning_sea[ i ].GetComponent<Image>( ).color = new Color( 255, 255, 255, 1 - timer );
                _night_sea[ i ].GetComponent<Image>( ).color = new Color( 255, 255, 255, 0 );
            }
        }
        if ( _game_system.getTime( ) > 18 * 60 ) {
            float timer = ( _game_system.getTime( ) - 18 * 60 ) / ( 6 * 60 );
            _b_morning_bg.GetComponent<Image>( ).color = new Color( 255, 255, 255, 0 );
            _f_morning_bg.GetComponent<Image>( ).color = new Color( 255, 255, 255, 0 );
            _f_sunset_bg.GetComponent<Image>( ).color = new Color( 255, 255, 255, 1 - timer );
            _b_night_bg.GetComponent<Image>( ).color = new Color( 255, 255, 255, timer );
            _f_night_bg.GetComponent<Image>( ).color = new Color( 255, 255, 255, timer );

            for ( int i = 0; i < _morning_clude.Count; i++ ) {
                _morning_clude[ i ].GetComponent<Image>( ).color = new Color( 255, 255, 255, 0 );
            }
            for ( int i = 0; i < _sunset_clude.Count; i++ ) {
                _sunset_clude[ i ].GetComponent<Image>( ).color = new Color( 255, 255, 255, 1 - timer );
            }
            for ( int i = 0; i < _morning_sea.Count; i++ ) {
                _morning_sea[ i ].GetComponent<Image>( ).color = new Color( 255, 255, 255, 0 );
                _night_sea[ i ].GetComponent<Image>( ).color = new Color( 255, 255, 255, timer );
            }
        }

        /*if ( _game_system.getTime( ) >= 22 * 60 ) {
            float a0 = Mathf.Sin( ( _timer - 0.1f ) * Mathf.PI / 12.0f );
            float a1 = Mathf.Sin( ( _timer - 0.9f ) * Mathf.PI / 10.0f );
            float a2 = Mathf.Sin( ( _timer - 0.6f ) * Mathf.PI / 8.0f );
            float a3 = Mathf.Sin( ( _timer - 0.4f ) * Mathf.PI / 6.0f );
            _night_star[ 0 ].GetComponent<Image>( ).color = new Color( 255, 255, 255, a0 );
            _night_star[ 1 ].GetComponent<Image>( ).color = new Color( 255, 255, 255, a1 );
            _night_star[ 2 ].GetComponent<Image>( ).color = new Color( 255, 255, 255, a2 );
            _night_star[ 3 ].GetComponent<Image>( ).color = new Color( 255, 255, 255, a3 );
        }*/
    }

    void WaveObject( GameObject obj, float ampplitude, float timer, float speed, float s_pos ) {
        Vector3 pos = obj.transform.position;
        pos.y = ampplitude * Mathf.Sin( timer * Mathf.PI / speed ) + s_pos;
        obj.transform.position = pos;
    }

    void CludeMove( GameObject obj, float speed ) {
        Vector3 pos = obj.transform.position;
        pos.x += speed;
        if ( pos.x > 1600 ) {
            pos.x = -1600;
        }
        if ( pos.x < -1600 ) {
            pos.x = 1600;
        }
        obj.transform.position = pos;
    }

	void WaveEventObject( ) {
		switch ( _event_data.getContent( _game_system.randEvent( ) ) ) {
		case CONTENTS.ISLAND:
			//Magic Number;
			_event_object.transform.position = new Vector3( 400, -150, 0 );
			break;
		case CONTENTS.SHIP:
			WaveObject( _event_object, CharacterAmpplitude, _timer, 1, -150 );
			break;
		}
	}

    public void NextDay( ) {
        _moon.transform.position =  new Vector3( 0, -MoonRadius + 450.0f, 0 );
    }
}
