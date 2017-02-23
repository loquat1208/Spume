using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIButtonEvents : MonoBehaviour {
    [SerializeField]
    private Sprite Fast = new Sprite( );
    [SerializeField]
    private Sprite Pause = new Sprite( );

    private LogManager _log_manager;
    private GameSystem game_system;
    private GameObject Ship_status_window;
    private GameObject status_window;
    private GameObject Help_window;
    private GameObject chara_status;
    private GameObject option_layer;
    private GameObject inside_layer;
    private GameObject outside_layer;
    private GameObject _event_button;
    private GameObject state_button;
    private GameObject fast_button;
	private float time_speed_save;

    void Awake( ) {
        _log_manager = GameObject.Find( "Log" ).gameObject.GetComponent<LogManager>( );
        game_system = GameObject.Find( "GameSystem" ).gameObject.GetComponent<GameSystem>( );
        Ship_status_window = GameObject.Find( "ShipStatusWindow" ).gameObject;
        status_window = GameObject.Find( "StatusWindow" ).gameObject;
        option_layer = GameObject.Find( "OptionLayer" ).gameObject;
        inside_layer = GameObject.Find( "InsideLayer" ).gameObject;
        outside_layer = GameObject.Find( "OutsideLayer" ).gameObject;
        Help_window = GameObject.Find( "HelpWindow" ).gameObject;

        _event_button = GameObject.Find( "EventSelects" ).gameObject;
        chara_status = GameObject.Find( "StatusWindow" ).gameObject;
        state_button = GameObject.Find( "StateButton" ).gameObject;
        fast_button = GameObject.Find( "FastButton" ).gameObject;    
    }

    void Start( ) {
        Ship_status_window.SetActive( false );
        option_layer.SetActive( false );
		time_speed_save = 0;
    }

	public float getTimeSpeedSave( ) {
		return time_speed_save;
	}

    public void NextDayButton( ) {
        if ( _log_manager.isLogOpened( ) ) {
            return;
        }
        if ( game_system.getTime( ) < 12 * 60 + 1 ) {
            return;
        }
        game_system.NextDay( );
        game_system.setLayer( LAYER.OUTSIDE );
        Ship_status_window.SetActive( false );
        _event_button.SetActive( false );
        chara_status.SetActive( false );
        fast_button.GetComponent<Image>( ).sprite = Fast;
    }

	public void shipStatusButton( ) {
		if ( Ship_status_window.activeSelf ) {
			Ship_status_window.SetActive( false );
		} else {
			Ship_status_window.SetActive( true );
		}
	}

    public void ChangeLayer( ) {
        _log_manager.setLogOpen( false );
        chara_status.SetActive( false );
        _event_button.SetActive( false );
        state_button.SetActive( false );
        LAYER layer = game_system.getLayer( );
        if ( layer == LAYER.OUTSIDE ) {
            Ship_status_window.SetActive( false );
            game_system.setLayer( LAYER.INSIDE );
            inside_layer.transform.position = new Vector3( 0, 0, 0 );
			outside_layer.transform.position = new Vector3( 1800, 0, 0 );
        } else {
            game_system.setLayer( LAYER.OUTSIDE );
            inside_layer.transform.position = new Vector3( 1800, 0, 0 );
            outside_layer.transform.position = new Vector3( 0, 0, 0 );
        }
    }

    public void OpenLog( ) {
        status_window.SetActive( false );
        if ( !_log_manager.isLogOpened( ) ) {
            _log_manager.setLogOpen( true );
            _log_manager.MoveLog( );
        }
    }

    public void GoToOption( ) {
		time_speed_save = game_system.getTimerSpeed( );
        game_system.setTimerSpeed( 0 );
        option_layer.transform.position = Vector3.zero;
        option_layer.SetActive( true );
        /*game_system.dataSave( );
        PlayerPrefs.SetFloat( "Time", game_system.getTime( ) );
        PlayerPrefs.Save( );
        SceneManager.LoadScene( "OptionScene" );*/
    }

    public void ClickedShip( ) {
        if ( Ship_status_window.activeSelf ) {
            Ship_status_window.SetActive( false );
        } else {
            Ship_status_window.transform.position = new Vector3( 30, -240, 0 );
            Ship_status_window.SetActive( true );
        }
    }

    public void HelpWindow( ) {
        if ( Help_window.activeSelf ) {
            Help_window.SetActive( false );
        } else {
            //Help_window.transform.position = new Vector3( 0, 0, 0 );
            Help_window.SetActive( true );
        }
    }

    public void FastButton( ) {
        if ( game_system.getTimerSpeed( ) != game_system.getTimerSpeedDefault( ) ) {
            game_system.setTimerSpeed( game_system.getTimerSpeedDefault( ) );
            fast_button.GetComponent<Image>( ).sprite = Fast;
        } else {
            game_system.setTimerSpeed( 8 );
            fast_button.GetComponent<Image>( ).sprite = Pause;
        }
    }
}
