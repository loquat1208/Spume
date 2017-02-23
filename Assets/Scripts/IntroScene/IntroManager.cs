using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class IntroManager : MonoBehaviour {
	int _event_num;

	void Start( ) { 
        _event_num = 1;
	}
	
	void Update( ) {
        
    }

    public bool limitFourChara( ) {
        if ( PlayerPrefs.GetInt( "Chara1Alive" ) + 
             PlayerPrefs.GetInt( "Chara2Alive" ) +
             PlayerPrefs.GetInt( "Chara3Alive" ) +
             PlayerPrefs.GetInt( "Chara4Alive" ) +
             PlayerPrefs.GetInt( "Chara5Alive" ) +
             PlayerPrefs.GetInt( "Chara6Alive" ) >= 4 ) {
            return true;
        }
        return false;
    }

    public bool allDeath( ) {
        if ( PlayerPrefs.GetInt( "Chara1Alive" ) +
             PlayerPrefs.GetInt( "Chara2Alive" ) +
             PlayerPrefs.GetInt( "Chara3Alive" ) +
             PlayerPrefs.GetInt( "Chara4Alive" ) +
             PlayerPrefs.GetInt( "Chara5Alive" ) +
             PlayerPrefs.GetInt( "Chara6Alive" ) == 0 ) {
            return true;
        }
        return false;
    }

	public int getEventNum( ) {
		return _event_num;
	}

	public void setEventNum( int num ) {
		_event_num = num;
	}
}
