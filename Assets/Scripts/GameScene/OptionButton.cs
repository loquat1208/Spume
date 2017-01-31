using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class OptionButton : MonoBehaviour {

    private GameSystem game_system;

    private void Awake( ) {
        game_system = GameObject.Find( "GameSystem" ).gameObject.GetComponent<GameSystem>( );
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
        game_system.setTimerSpeed( game_system.getTimerSpeedDefault( ) );
        gameObject.SetActive( false );
    }
}
