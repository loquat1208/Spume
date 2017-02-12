using UnityEngine;
using System.Collections;

public class InsideCharacterMenu : MonoBehaviour {
    private GameObject inside_system;
    private InsideManager inside_manager;
    private GameObject menus;
	private GameObject status_window;
	//private GameObject log;

    void Awake( ) {
		inside_system = GameObject.Find( "InsideLayer" ).gameObject;
		menus = GameObject.Find( "InsideMenus" ).gameObject;
		status_window = GameObject.Find( "StatusWindow" ).gameObject;
		//log = GameObject.Find( "Log" ).gameObject;
	}

    void Start( ) {
        inside_manager = inside_system.GetComponent<InsideManager>( );
		status_window.SetActive( false );
    }

    public void CharacterClickEvent( ) {
		//If character what you clicked is dead, can't open menu
		if( gameObject.GetComponent<Status>( ).getStatus( ).death ) {
			return;
		}
        menus.transform.position = gameObject.GetComponent<Status>( ).getMenuPos( );
		status_window.SetActive( true );
		//どんなキャラが選ばれたのかセットする
        for ( int i = 1; i < 7; i++ ) {
            if ( gameObject.name == "Chara" + i.ToString( ) ) {
                inside_manager.setMenu( i );
            }
        }
    }

	/*public void StatusActive( ) {
		//Logが開いていると表示しない
		if ( log.GetComponent<LogManager>( ).isLogOpened( ) ) {
			return;
		}
		//死んでいるキャラは表示しない
		if ( gameObject.GetComponent<Status>( ).getStatus( ).death ) {
			return;
		}
		status_window.SetActive( true );
        //どんなキャラが選ばれたのかセットする
        for ( int i = 1; i < 7; i++ ) {
            if ( gameObject.name == "Chara" + i.ToString( ) ) {
                inside_manager.setStatusWindow( i );
            }
        }
    }*/
}
