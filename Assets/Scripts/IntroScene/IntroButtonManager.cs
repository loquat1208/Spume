using UnityEngine;
using System.Collections;

public class IntroButtonManager : MonoBehaviour {
	private IntroManager _intro;
    private int _event;

	void Start( ) {
		_intro = GameObject.Find( "IntroSystem" ).gameObject.GetComponent<IntroManager>( );
    }

    public void YesButton( ) {
        _event = _intro.getEventNum( );
        PlayerPrefs.SetInt( "Chara" + _event.ToString( ) + "Alive", 1 );
        PlayerPrefs.Save( );
        _intro.setEventNum( _intro.getEventNum( ) + 1 );
        gameObject.GetComponentInParent<IntroCut>( ).TimerStart( ); 
    }

    public void NoButton( ) {
        _event = _intro.getEventNum( );
        PlayerPrefs.SetInt( "Chara" + _event.ToString( ) + "Alive", 0 );
        PlayerPrefs.Save( );
        _intro.setEventNum( _intro.getEventNum( ) + 1 );
        gameObject.GetComponentInParent<IntroCut>( ).TimerStart( );
    }
}
