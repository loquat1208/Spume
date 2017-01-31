using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class OutsideAnimation : MonoBehaviour {
    [SerializeField]
    private List<Sprite> MorningSea = new List<Sprite>( );
    [SerializeField]
    private List<Sprite> SunsetSea = new List<Sprite>( );
    [SerializeField]
    private List<Sprite> NightSea = new List<Sprite>( );
    [SerializeField]
    private List<float> SeaAmpplitude = new List<float>( );
    [SerializeField]
    private float CharacterAmpplitude = 0;
    [SerializeField]
    private float ShipAmpplitude = 0;
    [SerializeField]
    private List<float> CludeSpeed = new List<float>( );
    [SerializeField]
    private float MoonRadius = 0;

    private float _timer;
    private List<GameObject> _sea = new List<GameObject>( );
    private List<GameObject> _morning_clude = new List<GameObject>( );
    private List<GameObject> _night_star = new List<GameObject>( );
    private List<GameObject> _sunset_clude = new List<GameObject>( );
    private GameObject _chara;
    private GameObject _ship;
    private GameObject _moon;
    private GameObject _sun;
    private GameObject _b_morning_bg;
    private GameObject _b_night_bg;
    private GameObject _f_morning_bg;
    private GameObject _f_sunset_bg;
    private GameObject _f_night_bg;
    private GameSystem _game_system;
	private GameObject _event_object;
	private EventData _event_data;

    void Awake( ) {
        for ( int i = 1; i < 6; i++ ) {
            GameObject game_object = GameObject.Find( "Sea0" + i.ToString( ) ).gameObject;
            _sea.Add( game_object );
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
        _sun = GameObject.Find( "Sun" ).gameObject;
        _moon.transform.position = new Vector3( 0, -450f, 0 );

        _b_morning_bg = GameObject.Find( "B_MorningBg" ).gameObject;
        _b_night_bg = GameObject.Find( "B_NightBg" ).gameObject;
        _f_morning_bg = GameObject.Find( "F_MorningBg" ).gameObject;
        _f_sunset_bg = GameObject.Find( "F_SunsetBg" ).gameObject;
        _f_night_bg = GameObject.Find( "F_NightBg" ).gameObject;

        _game_system = GameObject.Find( "GameSystem" ).gameObject.GetComponent<GameSystem>( );
		_event_data = GameObject.Find( "EventSystem" ).gameObject.GetComponent<EventData>( );
		_event_object = GameObject.Find( "EventObject" ).gameObject;
    }

	void Update ( ) {
        if ( _game_system.getLayer( ) != LAYER.OUTSIDE ) {
            return;
        }
        _timer += Time.deltaTime;
        updateMove( );
        updateBg( );
        updateSea( );
        WaveEventObject( );
    }

    void updateMove( ) {
        for ( int i = 0; i < 5; i++ ) {
            WaveObject( _sea[ i ], SeaAmpplitude[ i ], _timer, i + 1, -120 );
        }

        WaveObject( _chara, CharacterAmpplitude, _timer, 2, 30 );
        WaveObject( _ship, ShipAmpplitude, _timer, 2, -350 );

        for ( int i = 0; i < _morning_clude.Count; i++ ) {
            CludeMove( _morning_clude[ i ], CludeSpeed[ i ] );
        }
        for ( int i = 0; i < _sunset_clude.Count; i++ ) {
            CludeMove( _sunset_clude[ i ], CludeSpeed[ i ] );
        }

        float _moon_pos_x = -MoonRadius * Mathf.Sin( ( _game_system.getTime( ) * 0.5f ) * Mathf.PI / 360 );
        float _moon_pos_y = MoonRadius * Mathf.Cos( ( _game_system.getTime( ) * 0.5f ) * Mathf.PI / 360 );
        _moon.transform.position = new Vector3( _moon_pos_x, _moon_pos_y - 450.0f, 0 );
        _sun.transform.position = new Vector3( -_moon_pos_x, -_moon_pos_y - 450.0f, 0 );
    }

    void updateBg( ) {
        if ( _game_system.getTime( ) <= 18 * 60 ) {
            float timer = ( _game_system.getTime( ) - 12 * 60 ) / ( 6 * 60 );
            if ( !_b_morning_bg.activeSelf || !_f_morning_bg.activeSelf ) {
                _b_morning_bg.SetActive( true );
                _f_morning_bg.SetActive( true );
            }
            if ( _b_night_bg.activeSelf || _f_night_bg.activeSelf ) {
                _b_night_bg.SetActive( false );
                _f_night_bg.SetActive( false );
            }
            _b_morning_bg.GetComponent<Image>( ).color = new Color( 255, 255, 255, 1 - timer );
            _f_morning_bg.GetComponent<Image>( ).color = new Color( 255, 255, 255, 1 - timer );
            _f_sunset_bg.GetComponent<Image>( ).color = new Color( 255, 255, 255, timer );

            for ( int i = 0; i < _morning_clude.Count; i++ ) {
                _morning_clude[ i ].GetComponent<Image>( ).color = new Color( 255, 255, 255, 1 - timer );
            }
            for ( int i = 0; i < _sunset_clude.Count; i++ ) {
                _sunset_clude[ i ].GetComponent<Image>( ).color = new Color( 255, 255, 255, timer );
            }
        }
        if ( _game_system.getTime( ) > 18 * 60 ) {
            float timer = ( _game_system.getTime( ) - 18 * 60 ) / ( 6 * 60 );
            if ( _b_morning_bg.activeSelf || _f_morning_bg.activeSelf ) {
                _b_morning_bg.SetActive( false );
                _f_morning_bg.SetActive( false );
            }
            if ( !_b_night_bg.activeSelf || !_f_night_bg.activeSelf ) {
                _b_night_bg.SetActive( true );
                _f_night_bg.SetActive( true );
            }
            _f_sunset_bg.GetComponent<Image>( ).color = new Color( 255, 255, 255, 1 - timer );
            _b_night_bg.GetComponent<Image>( ).color = new Color( 255, 255, 255, timer );
            _f_night_bg.GetComponent<Image>( ).color = new Color( 255, 255, 255, timer );

            for ( int i = 0; i < _morning_clude.Count; i++ ) {
                _morning_clude[ i ].GetComponent<Image>( ).color = new Color( 255, 255, 255, 0 );
            }
            for ( int i = 0; i < _sunset_clude.Count; i++ ) {
                _sunset_clude[ i ].GetComponent<Image>( ).color = new Color( 255, 255, 255, 1 - timer );
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

    void updateSea( ) {
        float wait_time = 3f;
        float _timer = _game_system.getTime( ) / 60f - 12f;
        float interval = ( 6 - wait_time * 1.5f ) / _sea.Count;
        if ( _timer < wait_time ) {
            for ( int i = 0; i < _sea.Count; i++ ) {
                _sea[ i ].GetComponent<Image>( ).sprite = MorningSea[ i ];
            }
        }

        //後できれいに治す。
        if (_timer >= wait_time + interval * 1f ) {
            _sea[ _sea.Count - 1 ].GetComponent<Image>( ).sprite = SunsetSea[ _sea.Count - 1 ];
        }
        if ( _timer >= wait_time + interval * 2f ) {
            _sea[ _sea.Count - 2 ].GetComponent<Image>( ).sprite = SunsetSea[ _sea.Count - 2 ];
        }
        if ( _timer >= wait_time + interval * 3f ) {
            _sea[ _sea.Count - 3 ].GetComponent<Image>( ).sprite = SunsetSea[ _sea.Count - 3 ];
        }
        if ( _timer >= wait_time + interval * 4f ) {
            _sea[ _sea.Count - 4 ].GetComponent<Image>( ).sprite = SunsetSea[ _sea.Count - 4 ];
        }
        if ( _timer >= wait_time + interval * 5f ) {
            _sea[ _sea.Count - 5 ].GetComponent<Image>( ).sprite = SunsetSea[ _sea.Count - 5 ];
        }

        if ( _timer >= ( 6 + wait_time / 2 ) + interval * 1f ) {
            _sea[ 0 ].GetComponent<Image>( ).sprite = NightSea[ 0 ];
        }
        if ( _timer >= ( 6 + wait_time / 2 ) + interval * 2f ) {
            _sea[ 1 ].GetComponent<Image>( ).sprite = NightSea[ 1 ];
        }
        if ( _timer >= ( 6 + wait_time / 2 ) + interval * 3f ) {
            _sea[ 2 ].GetComponent<Image>( ).sprite = NightSea[ 2 ];
        }
        if ( _timer >= ( 6 + wait_time / 2 ) + interval * 4f ) {
            _sea[ 3 ].GetComponent<Image>( ).sprite = NightSea[ 3 ];
        }
        if ( _timer >= ( 6 + wait_time / 2 ) + interval * 5f ) {
            _sea[ 4 ].GetComponent<Image>( ).sprite = NightSea[ 4 ];
        }
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
        _moon.transform.position =  new Vector3( 0, MoonRadius + 450.0f, 0 );
    }
}
