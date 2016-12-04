using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIButtonEvents : MonoBehaviour {
    private LogManager log_manager;
    private GameSystem game_system;
    private GameObject Ship_status_window;
    private GameObject status_window;
	private GameObject Help_window;
	private GameObject chara_status;
    private GameObject option_layer;

    void Awake( ) {
        log_manager = GameObject.Find( "Log" ).gameObject.GetComponent<LogManager>( );
        game_system = GameObject.Find( "GameSystem" ).gameObject.GetComponent<GameSystem>( );
        Ship_status_window = GameObject.Find( "ShipStatusWindow" ).gameObject;
        status_window = GameObject.Find( "StatusWindow" ).gameObject;
        option_layer = GameObject.Find( "OptionLayer" ).gameObject;
        Help_window = GameObject.Find( "HelpWindow" ).gameObject;

		chara_status = GameObject.Find( "StatusWindow" ).gameObject;
    }

    void Start( ) {
        Ship_status_window.SetActive( false );
        Help_window.SetActive( false );
        option_layer.SetActive( false );
    }

    public void NextDayButton( ) {
        if ( log_manager.isLogOpened(  ) ) {
            return;
        }
        game_system.NextDay( );
        game_system.setLayer( LAYER.OUTSIDE );
    }

    public void ChangeLayer( ) {
		log_manager.setLogOpen( false );
		chara_status.SetActive( false );
        LAYER layer = game_system.getLayer( );
        if ( layer == LAYER.OUTSIDE ) {
            Ship_status_window.SetActive( false );
            game_system.setLayer( LAYER.INSIDE );
        } else {
            game_system.setLayer( LAYER.OUTSIDE );
        }
    }

    public void OpenLog( ) {
        status_window.SetActive( false );
        if ( log_manager.isLogOpened( ) ) {
            log_manager.setLogOpen( false );
        }
        if ( !log_manager.isLogOpened( ) ) {
            log_manager.setLogOpen( true );
        }
    }

    public void GoToOption( ) {
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
            Help_window.transform.position = new Vector3( 0, 0, 0 );
            Help_window.SetActive( true );
        }
    }
}
