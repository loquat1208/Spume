using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;

public enum LAYER {
    INSIDE,
    OUTSIDE,
}

public class GameSystem : MonoBehaviour {
    public int MAIN_EVENT_INTERVAL = 10;
    public int MAIN_EVENT_MAX = 4;
    public float DefaultTimeSpeed = 8;

    private int max_days;
    private int days;
    private float time;
    private float _timer_speed;
    private LAYER _layer;
    private int rand_event;

    private GameObject inside_layer;
    private GameObject outside_layer;
    private Characters _characters;
    private EventData _event_data;
    private ShipStatus ship_status;
    private GameObject watch;
    private GameObject fade;
    private GameObject fade_text;
    private GameObject log;
    private GameObject event_button;

    void Awake( ) {
        max_days = MAIN_EVENT_INTERVAL * ( MAIN_EVENT_MAX + 1 );

        event_button = GameObject.Find( "EventSelects" ).gameObject;
        inside_layer = GameObject.Find( "InsideLayer" ).gameObject;
        outside_layer = GameObject.Find( "OutsideLayer" ).gameObject;
        _characters = GameObject.Find( "Characters" ).gameObject.GetComponent<Characters>( );
        _event_data = GameObject.Find( "EventSystem" ).gameObject.GetComponent<EventData>( );
        ship_status = GameObject.Find( "Ship" ).gameObject.GetComponent<ShipStatus>( );
        rand_event = Random.Range( 0, ( int )_event_data.getMaxData( ) );
        fade = GameObject.Find( "Fade" ).gameObject;
        fade_text = GameObject.Find( "FadeText" ).gameObject;
        watch = GameObject.Find( "Watch" ).gameObject;
        log = GameObject.Find( "Log" ).gameObject;

        init( );
    }

    void Start( ) {
        _timer_speed = DefaultTimeSpeed;
    }

    void Update( ) {
        updateLayer( );
        changeScene( );
        updateWatch( );

        //24:00後は計算しない。
        if ( time >= 60 * 24 ) {
            return;
        }
        time += _timer_speed * Time.deltaTime;
    }

    void init( ) {
        if ( PlayerPrefs.GetInt( "Days" ) == 0 ) {
            _characters.setNewGame( );
            ship_status.setNewShip( );
            time = 12 * 60;
            PlayerPrefs.SetFloat( "Time", time );
            PlayerPrefs.SetInt( "Days", 1 );
            PlayerPrefs.Save( );
        }
        days = PlayerPrefs.GetInt( "Days" );
        StartCoroutine( "FadeIn" );
        time = PlayerPrefs.GetFloat( "Time" );
        _layer = LAYER.OUTSIDE;
    }

    void updateWatch( ) {
        int hour = ( int )time / 60;
        int mint = ( int )time % 60;
        watch.GetComponent<Text>( ).text = days.ToString( ) + " Days  "
            + hour.ToString( "00" ) + " : " + mint.ToString( "00" )
            + "  " + PlayerPrefs.GetInt( "Select" ).ToString( );

    }

    void OnApplicationQuit( ) {
        dataSave( );
        PlayerPrefs.SetFloat( "Time", time );
        PlayerPrefs.Save( );
    }

    public float getTimerSpeedDefault( ) {
        return DefaultTimeSpeed;
    }

    public void setTimerSpeed( float speed ) {
        _timer_speed = speed;
    }

	public float getTimerSpeed( ) {
		return _timer_speed;
	}

    public float getTime( ) {
        return time;
    }

    public int randEvent( ) {
        return rand_event;
    }

    void updateLayer( ) {
		Vector3 inside_layer_pos = inside_layer.transform.position;
        switch ( _layer ) {
            case LAYER.INSIDE:
				inside_layer_pos.x = 0;
				inside_layer.transform.position = inside_layer_pos;
                inside_layer.SetActive( true );
                outside_layer.SetActive( false );
                break;
            case LAYER.OUTSIDE:
                inside_layer.SetActive( false );
                outside_layer.SetActive( true );
                break;
        }
    }

    bool gameOver( ) {
        if ( _characters.allDeath( ) ) {
            return true;
        }
        if ( ship_status.getResources( ).fuels <= -1 ) {
            return true;
        }
		if ( ship_status.getResources( ).ship_break && ship_status.getResources( ).repair_tools <= 0 ) {
            return true;
        }
        return false;
    }

    void changeScene( ) {
        if ( gameOver( ) ) {
            _characters.setNewGame( );
            ship_status.setNewShip( );
            PlayerPrefs.SetInt( "GameOver", 1 );
            PlayerPrefs.SetInt( "LoadGame", 0 );
            PlayerPrefs.Save( );
            SceneManager.LoadScene( "GameOverScene" );
        }
        if ( days >= max_days ) {
            SceneManager.LoadScene( "EndingScene" );
        }
        if ( days % MAIN_EVENT_INTERVAL == 0 ) {
            int event_num = days / MAIN_EVENT_INTERVAL;
            PlayerPrefs.SetInt( "EventNumber", event_num );
            NextDay( );
            SceneManager.LoadScene( "MovieScene" + event_num.ToString( ) );
        }
    }

	IEnumerator FadeIn( ) {
		fade_text.GetComponent<Text> ().text = days.ToString( ) + " Days";
		for ( float i = 0; i <= 180; i += 2f ) {
			if ( i == 120 ) {
				log.GetComponent<LogManager>( ).setLogOpen( true );
                outside_layer.GetComponent<OutsideManager>( ).NextDay( );
			}
			fade.GetComponent<Image> ().color = new Color( 0, 0, 0, Mathf.Sin( i * Mathf.PI / 180f ) );
			fade_text.GetComponent<Text> ().color = new Color( 255, 255, 255, Mathf.Sin( i * Mathf.PI / 180f ) );
			yield return 0;
		}
	} 

    public void NextDay( ) {
        days++;
		StartCoroutine( "FadeIn" );
        time = 60 * 12;
        _timer_speed = DefaultTimeSpeed;
        rand_event = Random.Range( 0, ( int )_event_data.getMaxData( ) );
        log.GetComponent<LogManager>( ).setSubSelect( false );
        _characters.nextDay( );
        ship_status.setFuels( ship_status.getResources( ).fuels - 1 );
        dataSave( );
        event_button.SetActive( false );
    }
    
    public void dataSave( ) {
        ship_status.saveData( );
        PlayerPrefs.SetInt( "Days", days );
        PlayerPrefs.Save( );
    }
	
    public LAYER getLayer( ) { return _layer; }
    public void setLayer( LAYER layer ) { _layer = layer; }
    public int getDays( ) { return days; }
}
