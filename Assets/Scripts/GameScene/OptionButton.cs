using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class OptionButton : MonoBehaviour {

	private GameSystem game_system;
	private UIButtonEvents UI_button_event;

    private void Awake( ) {
		game_system = GameObject.Find( "GameSystem" ).gameObject.GetComponent<GameSystem>( );
		UI_button_event = GameObject.Find( "UIButtonEvent" ).gameObject.GetComponent<UIButtonEvents>( );
    }

    public void goToTitle( ) {
        game_system.dataSave( );
        PlayerPrefs.SetFloat( "Time", game_system.getTime( ) );
        PlayerPrefs.Save( );
        SceneManager.LoadScene( "TitleScene" );
    }

    public void gameExitButton( ) {
        Application.Quit( );
    }

	public void backButton( ) {
		game_system.setTimerSpeed( UI_button_event.getTimeSpeedSave( ) );
        gameObject.SetActive( false );
    }
}
