using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class ButtonManager : MonoBehaviour {
    MovieManager movie_manager;

    void Awake( ) {
        movie_manager = GameObject.Find( "MovieSystem" ).gameObject.GetComponent<MovieManager>( );
    }

    public void SkipMovie( ) {
		if ( movie_manager.getScene( ) > 10 ) { return; }
        movie_manager.setScene( 11 );
    }

    public void Select1( ) {
        int event_num = PlayerPrefs.GetInt( "EventNumber" );
        movie_manager.setSelect( movie_manager.getSelect( ) + ( int )Mathf.Pow( 2.0f, ( float )event_num - 1 ) );
        PlayerPrefs.SetInt( "Select", movie_manager.getSelect( ) );
        PlayerPrefs.Save( );
        movie_manager.nextScene( );
        movie_manager.setSceneSelect( 1 );
    }

    public void Select2( ) {
        movie_manager.nextScene( );
    }

    public void reset( ) {
        PlayerPrefs.DeleteAll( );
    }
}
