using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public enum OUTSIDE_STATE {
    NONE,
    FISHING,
    WATER,
	EXPLORE,
}

public class OutsideManager : MonoBehaviour {
	[SerializeField]
	private float WorkingTime;
    [SerializeField]
    private Sprite None;
    [SerializeField]
    private Sprite Island;
    [SerializeField]
    private Sprite Ship;
	[SerializeField]
	private GameObject Gauge;

	private bool is_event_update;
    private OUTSIDE_STATE _state;
	private float _event_update_time;
    private float _working_update_time;

    private Characters _characters;
    private Status selected;
    private List<Sprite> _sprite_normal = new List<Sprite>( );
    private List<Sprite> _sprite_fishing = new List<Sprite>( );
    private List<Sprite> _sprite_water = new List<Sprite>( );

    //private GameObject x_mark;
    private GameObject event_button;
    private GameObject event_object;
    private GameObject _outside_chara;
    private GameObject _chara_state_button;
	private GameObject _gauge;

    private LogManager log_manager;
    private GameSystem _game_system;
    private EventData _event_data;
    private ShipStatus ship_status;

    void Awake( ) {
        _outside_chara = GameObject.Find( "Character" ).gameObject;
        _chara_state_button = GameObject.Find( "StateButton" ).gameObject;
        event_button = GameObject.Find( "EventSelects" ).gameObject;
        event_object = GameObject.Find( "EventObject" ).gameObject;
        //x_mark = GameObject.Find( "XMark" ).gameObject;

        _characters = GameObject.Find( "Characters" ).gameObject.GetComponent<Characters>( );
        _game_system = GameObject.Find( "GameSystem" ).gameObject.GetComponent<GameSystem>( );
        _event_data = GameObject.Find( "EventSystem" ).gameObject.GetComponent<EventData>( );
        ship_status = GameObject.Find( "Ship" ).gameObject.GetComponent<ShipStatus>( );
        log_manager = GameObject.Find( "Log" ).gameObject.GetComponent<LogManager>( );
    }

    void Start( ) {
        //CharacterのSpriteをListで管理。
        _sprite_normal.Add( None );
        for ( int i = 1; i < 7; i++ ) {
            _sprite_normal.Add( _characters.getCharacter( i ).getOutsideImage( OUTSIDE_STATE.NONE ) );
            _sprite_fishing.Add( _characters.getCharacter( i ).getOutsideImage( OUTSIDE_STATE.FISHING ) );
            _sprite_water.Add( _characters.getCharacter( i ).getOutsideImage( OUTSIDE_STATE.WATER ) );
        }

        _outside_chara.GetComponent<Image>( ).sprite = _sprite_normal[ 0 ];
        event_button.SetActive( false );
        _chara_state_button.SetActive( false );

		is_event_update = false;
		_state = OUTSIDE_STATE.NONE;
		_event_update_time = 25 * 60;
		_working_update_time = 25 * 60;
    }

    void Update( ) {
        drawCharacter( _state );
        UpdateEvent( );
		updateWorking( );
    }

    void drawCharacter( OUTSIDE_STATE state ) {
        for ( int i = 1; i < 7; i++ ) {
            if ( !isOutside( _characters.getCharacter( i ) ) ) {
                continue;
            }
            selected = _characters.getCharacter( i );
            switch ( state ) {
                case OUTSIDE_STATE.NONE:
                    _outside_chara.GetComponent<Image>( ).sprite = _sprite_normal[ i ];
                    break;
                case OUTSIDE_STATE.FISHING:
                    _outside_chara.GetComponent<Image>( ).sprite = _sprite_fishing[ i - 1 ];
                    break;
                case OUTSIDE_STATE.WATER:
                    _outside_chara.GetComponent<Image>( ).sprite = _sprite_water[ i - 1 ];
                    break;
				case OUTSIDE_STATE.EXPLORE:
					_outside_chara.SetActive( false );
					break;
            }
        }
    }

    bool isOutside( Status character ) {
        if ( character.getStatus( ).place != LAYER.OUTSIDE ) {
            return false;
        }
        if ( character.getStatus( ).death ) {
            return false;
        }
        return true;
    }

    void drawObject( CONTENTS contents ) {
        switch ( contents ) {
            case CONTENTS.ISLAND:
                event_object.GetComponent<Image>( ).sprite = Island;
                break;
            case CONTENTS.SHIP:
                event_object.GetComponent<Image>( ).sprite = Ship;
                break;
        }
    }

    void UpdateEvent( ) {
        //OUTSIDEに主人公がいるときはできない。
        if ( selected == null ) {
            return;
        }
        if ( _game_system.getTime( ) <= _event_update_time ) {
            return;
        }
        if ( is_event_update ) {
            return;
        }
        int active = ( int )_event_data.getData( _game_system.randEvent( ), EVENTDATA.ACTIVE );
        if ( active == 0 ) {
            if ( AllTrue( ) ) {
                changeStatusTrue( );
            }
            if ( !AllTrue( ) ) {
                changeStatusFalse( );
            }
        }
        is_event_update = true;
		_state = OUTSIDE_STATE.NONE;
        _outside_chara.SetActive( true );
        log_manager.setLogOpen( true );
    }

	void updateWorking( ) {
		if ( _game_system.getTime( ) >= 24 * 60 ) {
			_state = OUTSIDE_STATE.NONE;
			return;
		}
		if ( _game_system.getTime( ) <= _working_update_time ) {
			return;
		}
		if ( _state == OUTSIDE_STATE.FISHING ) {
			ship_status.setFoods( ship_status.getResources( ).foods + 1 );
			_working_update_time = 25 * 60;
		}
		if ( _state == OUTSIDE_STATE.WATER ) {
			ship_status.setWater( ship_status.getResources( ).water + 1 );
			_working_update_time = 25 * 60;
		}
		_state = OUTSIDE_STATE.NONE;
	}

	public OUTSIDE_STATE getOutsideState( ) {
		return _state;
	}

	public float getWorkingTime( ) {
		return WorkingTime;
	}

    public bool isEventUpdate( ) {
        return is_event_update;
    }

    public void NextDay( ) {
        gameObject.GetComponent<OutsideAnimation>( ).NextDay( );
        drawObject( _event_data.getContent( _game_system.randEvent( ) ) );
        _chara_state_button.SetActive( false );
        _outside_chara.SetActive( true );
        is_event_update = false;
        _event_update_time = 25 * 60;
		_working_update_time = 25 * 60;
        _state = OUTSIDE_STATE.NONE;
		if ( _gauge != null ) {
			Destroy( _gauge );
		}
    }

    public void fishingButton( ) {
		if ( _state != OUTSIDE_STATE.NONE ) {
			return;
		}
		//24時になるとできない。
		if ( _game_system.getTime( ) >= 24 * 60 ) {
			return;
		}
        if ( ship_status.getResources( ).rods <= 0 ) {
            return;
        }
		is_event_update = false;
		_event_update_time = 25 * 60;
		_state = OUTSIDE_STATE.FISHING;
		_working_update_time = _game_system.getTime( ) + WorkingTime * 60;

		Vector3 pos = _outside_chara.transform.position + new Vector3( 0, 50, 0 );
		_gauge = Instantiate( Gauge, pos, new Quaternion ( 0, 0, 0, 0 ) ) as GameObject;
		_gauge.transform.parent = gameObject.transform;
    }

    public void waterButton( ) {
		if ( _state != OUTSIDE_STATE.NONE ) {
			return;
		}
		//24時になるとできない。
		if ( _game_system.getTime( ) >= 24 * 60 ) {
			return;
		}
        if ( ship_status.getResources( ).pots <= 0 ) {
            return;
        }
		is_event_update = false;
		_event_update_time = 25 * 60;
        _state = OUTSIDE_STATE.WATER;
		_working_update_time = _game_system.getTime( ) + WorkingTime * 60;

		Vector3 pos = _outside_chara.transform.position + new Vector3( 0, 50, 0 );
		_gauge = Instantiate( Gauge, pos, new Quaternion ( 0, 0, 0, 0 ) ) as GameObject;
		_gauge.transform.parent = gameObject.transform;
    }

    public void normalButton( ) {
		Destroy( _gauge );
		is_event_update = false;
		_event_update_time = 25 * 60;
        _state = OUTSIDE_STATE.NONE;
    }

    public void drawEventSelectButton( ) {
        /*if ( is_event_update ) {
            x_mark.SetActive( true );
        } else {
            x_mark.SetActive( false );
        }*/
        if ( event_button.activeSelf ) {
            event_button.SetActive( false );
        } else {
            event_button.SetActive( true );
        }
    }

    public void drawOutsideStateButton( ) {
        if ( _outside_chara.GetComponent<Image>( ).sprite == _sprite_normal[ 0 ] ) {
            return;
        }
        if ( _chara_state_button.activeSelf ) {
            _chara_state_button.SetActive( false );
        } else {
            _chara_state_button.SetActive( true );
        }
    }

    public void EventYes( ) {
        //OUTSIDEに主人公がいるときはできない。
        if ( selected == null ) {
            return;
        }
        //キャラーが船で何かしているとできない。
        if ( _state != OUTSIDE_STATE.NONE ) {
            return;
        }
		//24時になるとできない。
		if ( _game_system.getTime( ) >= 24 * 60 ) {
			return;
		}

		Vector3 pos = event_object.transform.position + new Vector3( 0, -200, 0 );
		_gauge = Instantiate( Gauge, pos, new Quaternion ( 0, 0, 0, 0 ) ) as GameObject;
		_gauge.transform.parent = gameObject.transform;

		_state = OUTSIDE_STATE.EXPLORE;
        int event_end = ( int )_event_data.getData( _game_system.randEvent( ), EVENTDATA.TIME );
        _event_update_time = event_end * 60 + _game_system.getTime( );
        event_button.SetActive( false );
    }

    public void EventNo( ) {
		Destroy( _gauge );
        event_button.SetActive( false );
		_state = OUTSIDE_STATE.NONE;
        _outside_chara.SetActive( true );
        _event_update_time = 25 * 60;
    }

    public bool AllTrue( ) {
        if ( isStatus( ) && isFemale( ) && isNight( ) && isAfternoon( ) && isMale( ) ) {
            return true;
        }
        return false;
    }

    public void changeStatusFalse( ) {
        int f_fuels = ( int )_event_data.getData( _game_system.randEvent( ), EVENTDATA.F_FUEL );
        int f_ship_foods = ( int )_event_data.getData( _game_system.randEvent( ), EVENTDATA.F_SHIP_FOODS );
        int f_ship_water = ( int )_event_data.getData( _game_system.randEvent( ), EVENTDATA.F_SHIP_WATER );
        int f_guns = ( int )_event_data.getData( _game_system.randEvent( ), EVENTDATA.F_GUNS );
        int f_medical_kits = ( int )_event_data.getData( _game_system.randEvent( ), EVENTDATA.F_MEDICAL_KITS );
        int f_repair_tools = ( int )_event_data.getData( _game_system.randEvent( ), EVENTDATA.F_REPAIR_TOOLS );
        int f_rods = ( int )_event_data.getData( _game_system.randEvent( ), EVENTDATA.F_RODS );
        int f_pots = ( int )_event_data.getData( _game_system.randEvent( ), EVENTDATA.F_POTS );
        int f_ship_break = ( int )_event_data.getData( _game_system.randEvent( ), EVENTDATA.F_SHIP_BREAK );
        int f_health = ( int )_event_data.getData( _game_system.randEvent( ), EVENTDATA.F_HEALTH );
        int f_disease = ( int )_event_data.getData( _game_system.randEvent( ), EVENTDATA.F_DISEASE );
        ship_status.setFuels( ship_status.getResources( ).fuels + f_fuels );
        ship_status.setFoods( ship_status.getResources( ).foods + f_ship_foods );
        ship_status.setWater( ship_status.getResources( ).water + f_ship_water );
        ship_status.setGuns( ship_status.getResources( ).guns + f_guns );
        ship_status.setMedicalKits( ship_status.getResources( ).medical_kits + f_medical_kits );
        ship_status.setRepairTools( ship_status.getResources( ).repair_tools + f_repair_tools );
        ship_status.setRods( ship_status.getResources( ).rods + f_rods );
        ship_status.setPots( ship_status.getResources( ).pots + f_pots );
        ship_status.setShipBreak( f_ship_break );
        selected.setHealth( selected.getStatus( ).health + f_health );
        selected.setDisease( f_disease );
    }

    public void changeStatusTrue( ) {
        int t_fuels = ( int )_event_data.getData( _game_system.randEvent( ), EVENTDATA.T_FUEL );
        int t_ship_foods = ( int )_event_data.getData( _game_system.randEvent( ), EVENTDATA.T_SHIP_FOODS );
        int t_ship_water = ( int )_event_data.getData( _game_system.randEvent( ), EVENTDATA.T_SHIP_WATER );
        int t_guns = ( int )_event_data.getData( _game_system.randEvent( ), EVENTDATA.T_GUNS );
        int t_medical_kits = ( int )_event_data.getData( _game_system.randEvent( ), EVENTDATA.T_MEDICAL_KITS );
        int t_repair_tools = ( int )_event_data.getData( _game_system.randEvent( ), EVENTDATA.T_REPAIR_TOOLS );
        int t_rods = ( int )_event_data.getData( _game_system.randEvent( ), EVENTDATA.T_RODS );
        int t_pots = ( int )_event_data.getData( _game_system.randEvent( ), EVENTDATA.T_POTS );
        int t_ship_break = ( int )_event_data.getData( _game_system.randEvent( ), EVENTDATA.T_SHIP_BREAK );
        int t_health = ( int )_event_data.getData( _game_system.randEvent( ), EVENTDATA.T_HEALTH );
        int t_disease = ( int )_event_data.getData( _game_system.randEvent( ), EVENTDATA.T_DISEASE );

        ship_status.setFuels( ship_status.getResources( ).fuels + t_fuels );
        ship_status.setFoods( ship_status.getResources( ).foods + t_ship_foods );
        ship_status.setWater( ship_status.getResources( ).water + t_ship_water );
        ship_status.setGuns( ship_status.getResources( ).guns + t_guns );
        ship_status.setMedicalKits( ship_status.getResources( ).medical_kits + t_medical_kits );
        ship_status.setRepairTools( ship_status.getResources( ).repair_tools + t_repair_tools );
        ship_status.setRods( ship_status.getResources( ).rods + t_rods );
        ship_status.setPots( ship_status.getResources( ).pots + t_pots );
        ship_status.setShipBreak( t_ship_break );
        selected.setHealth( selected.getStatus( ).health + t_health );
        selected.setDisease( t_disease );
    }

    bool isStatus( ) {
        int up_down         = ( int )_event_data.getData( _game_system.randEvent( ), EVENTDATA.UPDOWN           );
        int s_fuels         = ( int )_event_data.getData( _game_system.randEvent( ), EVENTDATA.S_FUEL           );
        int s_ship_foods    = ( int )_event_data.getData( _game_system.randEvent( ), EVENTDATA.S_SHIP_FOODS     );
        int s_ship_water    = ( int )_event_data.getData( _game_system.randEvent( ), EVENTDATA.S_SHIP_WATER     );
        int s_guns          = ( int )_event_data.getData( _game_system.randEvent( ), EVENTDATA.S_GUNS           );
        int s_medical_kits  = ( int )_event_data.getData( _game_system.randEvent( ), EVENTDATA.S_MEDICAL_KITS   );
        int s_repair_tools  = ( int )_event_data.getData( _game_system.randEvent( ), EVENTDATA.S_REPAIR_TOOLS   );
        int s_rods          = ( int )_event_data.getData( _game_system.randEvent( ), EVENTDATA.S_RODS           );
        int s_pots          = ( int )_event_data.getData( _game_system.randEvent( ), EVENTDATA.S_POTS           );
        int s_chara_foods   = ( int )_event_data.getData( _game_system.randEvent( ), EVENTDATA.S_CHARA_FOODS    );
        int s_chara_water   = ( int )_event_data.getData( _game_system.randEvent( ), EVENTDATA.S_CHARA_WATER    );
        int s_health        = ( int )_event_data.getData( _game_system.randEvent( ), EVENTDATA.S_HEALTH         );
        int s_disease       = ( int )_event_data.getData( _game_system.randEvent( ), EVENTDATA.S_DISEASE        );
        if ( up_down == 1 ) {
            bool fuels          = ship_status.getResources( ).fuels         >= s_fuels;
            bool ship_food      = ship_status.getResources( ).foods         >= s_ship_foods;
            bool ship_water     = ship_status.getResources( ).water         >= s_ship_water;
            bool gun            = ship_status.getResources( ).guns          >= s_guns;
            bool medical_kit    = ship_status.getResources( ).medical_kits  >= s_medical_kits;
            bool repair_tool    = ship_status.getResources( ).repair_tools  >= s_repair_tools;
            bool rod            = ship_status.getResources( ).rods          >= s_rods;
            bool pot            = ship_status.getResources( ).pots          >= s_pots;
            bool chara_foods    = selected.getStatus( ).foods               >= s_chara_foods;
            bool chara_water    = selected.getStatus( ).water               >= s_chara_water;
            bool health         = selected.getStatus( ).health              >= s_health;
            bool disease        = selected.getStatus( ).disease && ( s_disease == 1 );

            if ( fuels && ship_food  && ship_water   && gun          && medical_kit  && repair_tool && 
                 rod   && pot        && chara_foods  && chara_water  && health       && disease         ) {
                return true;
            } else {
                return false;
            }
        } else {
            bool fuels          = s_fuels == 0          || ship_status.getResources( ).fuels         <= s_fuels;
            bool ship_food      = s_ship_foods == 0     || ship_status.getResources( ).foods         <= s_ship_foods;
            bool ship_water     = s_ship_water == 0     || ship_status.getResources( ).water         <= s_ship_water;
            bool gun            = s_guns == 0           || ship_status.getResources( ).guns          <= s_guns;
            bool medical_kit    = s_medical_kits == 0   || ship_status.getResources( ).medical_kits  <= s_medical_kits;
            bool repair_tool    = s_repair_tools == 0   || ship_status.getResources( ).repair_tools  <= s_repair_tools;
            bool rod            = s_rods == 0           || ship_status.getResources( ).rods          <= s_rods;
            bool pot            = s_pots == 0           || ship_status.getResources( ).pots          <= s_pots;
            bool chara_foods    = s_chara_foods == 0    || selected.getStatus( ).foods               <= s_chara_foods;
            bool chara_water    = s_chara_water == 0    || selected.getStatus( ).water               <= s_chara_water;
            bool health         = s_health == 0         || selected.getStatus( ).health              <= s_health;
            bool disease        = !selected.getStatus( ).disease && ( s_disease == 0 );
            if ( fuels && ship_food && ship_water && gun && medical_kit && repair_tool &&
                 rod && pot && chara_foods && chara_water && health && disease ) {
                return true;
            } else {
                return false;
            }
        }
    }

    bool isFemale( ) {
        int female = ( int )_event_data.getData( _game_system.randEvent( ), EVENTDATA.FEMALE );
        if ( female == 1 ) {
            if ( selected.transform.name == "Chara1" ) {
                return true;
            }
            if ( selected.transform.name == "Chara2" ) {
                return true;
            }
            return false;
        } else {
            return true;
        }
    }

    bool isMale( ) {
        int male = ( int )_event_data.getData( _game_system.randEvent( ), EVENTDATA.MALE );
        if ( male == 1 ) {
            if ( selected.transform.name == "Chara1" ) {
                return false;
            }
            if ( selected.transform.name == "Chara2" ) {
                return false;
            }
            return true;
        } else {
            return true;
        }
    }

    bool isNight( ) {
        int night = ( int )_event_data.getData( _game_system.randEvent( ), EVENTDATA.NIGHT );
        if ( night == 1 ) {
            if ( _game_system.getTime( ) >= 18 * 60 ) {
                return true;
            }
            return false;
        } else {
            return true;
        }
    }

    bool isAfternoon( ) {
        int afternoon = ( int )_event_data.getData( _game_system.randEvent( ), EVENTDATA.AFTERNOON );
        if ( afternoon == 1 ) {
            if ( _game_system.getTime( ) <= 18 * 60 ) {
                return true;
            }
            return false;
        } else {
            return true;
        }
    }
}
