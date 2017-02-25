using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public struct STATUS {
    public LAYER place;
    public int foods;
    public int water;
    public int health;
    public int loyalty;
    public bool death;
    public bool disease;
}

public class Characters : MonoBehaviour {
	List<GameObject> _chara_obj = new List<GameObject>( );
	List<Status> _chara = new List<Status>( );

    //Came to next day, character decrease foods and wather. 
    public int DECREASE_FOODS_HEALTHY;
    public int DECREASE_WATER_HEALTHY;

    //If character's state is hunger, character decrease health. 
    public int DECREASE_HEALTH_HUNGER;
    
    //If character is sick, get more decrease value. 
	public int DECREASE_FOODS_SICK;
    public int DECREASE_WATER_SICK;
    public int DECREASE_HEALTH_SICK;

    //If character is healthy and no hunger, character increase health.
    private int FULL_HEALTH;
    public int INCREASE_HEALTH;

    //If character have foods and water more than this value, increase loyalty to next day.
    public int SATISFACTION_VALUE;
    public int INCREASE_LOYALTY;

    //If character have foods and water less than this value, increase loyalty to next day.
    public int DISSATISFACTION_VALUE;
    public int DECREASE_LOYALTY_SICK;
    public int DECREASE_LOYALTY_DISSATISFACTION;

    void Awake( ) {
        FULL_HEALTH = 10;
        for ( int i = 1; i < 7; i++ ) {
			_chara_obj.Add( GameObject.Find( "Chara" + i.ToString( ) ).gameObject );
			Status chara = _chara_obj[ i - 1 ].GetComponent<Status>( );
            _chara.Add( chara );
        }
		AliveCharacters( );
    }
	
	void Update( ) {
		drawCharacters( );
        for ( int i = 0; i < 6; i++ ) {
            isAllGood( _chara[ i ] );
            death( _chara[ i ] );
        }
    }
		
	void drawCharacters( ) {
		for( int i = 0; i < 6; i++ ) {
			_chara_obj[ i ].SetActive( true );
			if ( _chara[ i ].getStatus( ).place != LAYER.INSIDE ) {
				_chara_obj[ i ].SetActive( false );
			}
			if ( _chara[ i ].getStatus( ).death ) {
				_chara_obj[ i ].SetActive( false );
			}
		}
	}

	//check character's status
    void isAllGood( Status character ) {
        character.setFace( STATE.NORMAL );
        if ( character.getStatus( ).death ) {
            character.setFace( STATE.DEATH );
            return;
        }
        if ( character.getStatus( ).foods <= 0 ) {
            character.setFace( STATE.HUNGRY );
            return;
        }
        if ( character.getStatus( ).water <= 0 ) {
            character.setFace( STATE.THIRSTY );
            return;
        }
        if ( character.getStatus( ).disease ) {
            character.setFace( STATE.SICK );
            return;
        }
        if ( character.getStatus( ).loyalty <= 0 ) {
            character.setFace( STATE.DEFIANCE );
            return;
        }
    }

	//新しいゲームの時、各キャラのStatusを初期化する
    public void setNewGame( ) {
        for ( int i = 0; i < 6; i++ ) {
            _chara[ i ].setNew( );
        }
    }

	//Introの選択を適用する
    void AliveCharacters( ) {
        for ( int i = 1; i < 7; i++ ) {
            if ( PlayerPrefs.GetInt( "Chara" + i.ToString( ) + "Alive" ) == 0 ) {
                _chara[ i - 1 ].setDeath( true );
            }
        }
    }
		
	//体力が０より小さいとキャラを殺す
    void death( Status character ) {
        if ( character.getStatus( ).health <= 0 ) {
            character.setDeath( true );
        }
    }
		
    void decreaseFoods( Status character ) {
        character.setFoods( character.getStatus( ).foods - DECREASE_FOODS_HEALTHY );
		if ( character.getStatus( ).disease ) {
			character.setWater( character.getStatus( ).foods - DECREASE_FOODS_SICK );
		}
        if ( character.getStatus( ).foods <= 0 ) {
            character.setFoods( 0 );
            return;
        }
    }

    void decreaseWater( Status character ) {
        character.setWater( character.getStatus( ).water - DECREASE_WATER_HEALTHY );
        if ( character.getStatus( ).disease ) {
            character.setWater( character.getStatus( ).water - DECREASE_WATER_SICK );
        }
        if ( character.getStatus( ).water <= 0 ) {
            character.setWater( 0 );
            return;
        }
    }

    void variationHealth( Status character ) { 
        if ( character.getStatus( ).disease ) {
            character.setHealth( character.getStatus( ).health - DECREASE_HEALTH_SICK );
        }
        if ( !isHunger( character ) ) {
			//Health limit
            if ( character.getStatus( ).health >= FULL_HEALTH ) {
                return;
            }
            character.setHealth( character.getStatus( ).health + INCREASE_HEALTH );
        } else {
            character.setHealth( character.getStatus( ).health - DECREASE_HEALTH_HUNGER );
        }
    }

    void variationLoyality( Status character ) {
		//foodsとwaterが基準以上だったら、親密度が増える
        if ( character.getStatus( ).foods >= SATISFACTION_VALUE &&
             character.getStatus( ).water >= SATISFACTION_VALUE ) {
            character.setLoyalty( character.getStatus( ).loyalty + INCREASE_LOYALTY );
        }
		//foodsとwaterが基準以だったら、親密度が減る
        if ( character.getStatus( ).foods <= DISSATISFACTION_VALUE ||
             character.getStatus( ).water <= DISSATISFACTION_VALUE ) {
            character.setLoyalty( character.getStatus( ).loyalty - DECREASE_LOYALTY_DISSATISFACTION );
        }
		//病気の時は親密度が減る
        if ( character.getStatus( ).disease ) {
            character.setLoyalty( character.getStatus( ).loyalty - DECREASE_LOYALTY_SICK );
        }
        if ( character.getStatus( ).loyalty <= 0 ) {
            character.setLoyalty( 0 );
            return;
        }
    }

    bool isHunger( Status character ) {
        if ( character.getStatus( ).foods <= 0 ) {
            return true;
        }
        if ( character.getStatus( ).water <= 0 ) {
            return true;
        }
        return false;
    }

    void haveDisease( Status character ) {
        int isSick = 0;
        if ( character.getStatus( ).foods <= 0 &&
             character.getStatus( ).water <= 0 ) {
			isSick = Random.Range( 0, 3 );
        }
        if ( isSick != 0 ) {
            character.setDisease( true );
        }
    }
		
    public void nextDay( ) {
        for ( int i = 0; i < 6; i++ ) {
            decreaseFoods( _chara[ i ] );
            decreaseWater( _chara [ i ] );
            variationHealth( _chara [ i ] );
            variationLoyality( _chara [ i ] );
            haveDisease( _chara [ i ] );
            _chara[ i ].saveStatus( );
        }
    }

    public bool allDeath( ) {
        for ( int i = 0; i < 6; i++ ) {
            if ( !_chara[ i ].getStatus( ).death ) {
                return false;
            }
        }
        return true;
    }

    public Status getCharacter( int chara_num ) {
        return _chara[ chara_num - 1 ];
    }
}
