using UnityEngine;
using System.Collections;

public enum STATE {
    NORMAL,
    SICK,
    HUNGRY,
    THIRSTY,
    DEFIANCE,
    DEATH,
}

public class Status : MonoBehaviour {
    private STATUS character;
    private STATE _state;

    [SerializeField]
    private int StartFoods = 10;
    [SerializeField]
    private int StartWater = 10;
    [SerializeField]
    private int StartHealth = 10;
    [SerializeField]
    private int StartLoyalty = 10;
    [SerializeField]
    private Sprite OutsideNormalImage = new Sprite( );
    [SerializeField]
    private Sprite OutsideFishingImage = new Sprite( );
    [SerializeField]
    private Sprite OutsideWaterImage = new Sprite( );
    [SerializeField]
    private Vector3 CharaMenuPos = new Vector3( );

    public void setNew( ) {
        PlayerPrefs.SetInt( gameObject.name + "Foods", StartFoods );
        PlayerPrefs.SetInt( gameObject.name + "Water", StartWater );
        PlayerPrefs.SetInt( gameObject.name + "Health", StartHealth );
        PlayerPrefs.SetInt( gameObject.name + "Loyalty", StartLoyalty );
        character.disease = false;
        PlayerPrefs.Save( );
    }

    void init( ) {
        character.place     = LAYER.INSIDE;
        character.foods     = PlayerPrefs.GetInt( gameObject.name + "Foods" );
        character.water     = PlayerPrefs.GetInt( gameObject.name + "Water" );
        character.health    = PlayerPrefs.GetInt( gameObject.name + "Health" );
        character.loyalty   = PlayerPrefs.GetInt( gameObject.name + "Loyalty" );
        if ( PlayerPrefs.GetInt( gameObject.name + "Alive" ) == 1 ) {
            character.death = false;
        } else {
            character.death = true;
        }
        if ( PlayerPrefs.GetInt( gameObject.name + "Disease" ) == 1 ) {
            character.disease = true;
        } else {
            character.disease = false;
        }
    }

    void Start( ) {
        init( );
    }

    public void saveStatus( ) {
        PlayerPrefs.SetInt( gameObject.name + "Foods", character.foods );
        PlayerPrefs.SetInt( gameObject.name + "Water", character.water );
        PlayerPrefs.SetInt( gameObject.name + "Health", character.health );
        PlayerPrefs.SetInt( gameObject.name + "Loyalty", character.loyalty );
        if ( !character.death ) {
            PlayerPrefs.SetInt( gameObject.name + "Alive", 1 );
        }
		if (character.disease) {
			PlayerPrefs.SetInt (gameObject.name + "Disease", 1);
		} else { 
			PlayerPrefs.SetInt (gameObject.name + "Disease", 0);
		}
        PlayerPrefs.Save( );
    }

    public void setFace( STATE state ) {
        _state = state;
    }

    public  STATE getState( ) {
        return _state;
    }

    public Vector3 getMenuPos( ) {
        return CharaMenuPos;
    }

    public Vector3 getPos( ) {
        return gameObject.transform.position;
    }
		
    public Sprite getOutsideImage( OUTSIDE_STATE state ) {
        switch ( state ) {
            case OUTSIDE_STATE.NONE:
                return OutsideNormalImage;
            case OUTSIDE_STATE.FISHING:
                return OutsideFishingImage;
            case OUTSIDE_STATE.WATER:
                return OutsideWaterImage;
            default:
                return OutsideNormalImage;
        }
    }

    public STATUS getStatus( ) { return character; }
    public void setPlace( LAYER place ) { character.place = place; }
    public void setFoods( int foods ) { character.foods = foods; }
    public void setWater( int water ) { character.water = water; }
    public void setHealth( int health ) { character.health = health; }
    public void setLoyalty( int loyalty ) { character.loyalty = loyalty; }
    public void setDeath( bool death ) { character.death = death; }
    public void setDisease( bool disease ) { character.disease = disease; }
    public void setDeath( int death ) {
        if ( death == 1 ) {
            character.death = true;
        } else {
            character.death = false;
        }
    }
    public void setDisease( int disease ) {
        if ( disease == 1 ) {
            character.disease = true;
        } else {
            character.disease = false;
        }
    }
}
