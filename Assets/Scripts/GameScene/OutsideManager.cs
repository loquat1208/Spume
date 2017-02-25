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
	private float WorkingTime = 3;
    [SerializeField]
    private Sprite None = new Sprite( );
    [SerializeField]
    private Sprite Island = new Sprite( );
    [SerializeField]
    private Sprite Ship = new Sprite( );
    [SerializeField]
	private GameObject Gauge;
    [SerializeField]
    private GameObject Speech;
    [SerializeField]
    private GameObject Plus1;

	//Event関連
    private bool is_event_update;
    private bool today_event_end;
	private float _event_update_time;
	private float _working_update_time;
	//character関連
    private Characters _characters;
	private Status selected;
	private OUTSIDE_STATE _state;
    private List<Sprite> _sprite_normal = new List<Sprite>( );
    private List<Sprite> _sprite_fishing = new List<Sprite>( );
    private List<Sprite> _sprite_water = new List<Sprite>( );
	//GameObject
    private GameObject _event_button;
    private GameObject _event_object;
    private GameObject _outside_chara;
    private GameObject _chara_state_button;
	private GameObject _gauge;
	//class
    private LogManager _log_manager;
    private GameSystem _game_system;
    private EventData _event_data;
    private ShipStatus _ship_status;

    void Awake( ) {
        _outside_chara = GameObject.Find( "Character" ).gameObject;
        _chara_state_button = GameObject.Find( "StateButton" ).gameObject;
        _event_button = GameObject.Find( "EventSelects" ).gameObject;
        _event_object = GameObject.Find( "EventObject" ).gameObject;
        _characters = GameObject.Find( "Characters" ).gameObject.GetComponent<Characters>( );
        _game_system = GameObject.Find( "GameSystem" ).gameObject.GetComponent<GameSystem>( );
        _ship_status = _game_system.GetComponent<ShipStatus>( );
        _event_data = GameObject.Find( "EventSystem" ).gameObject.GetComponent<EventData>( );
        _log_manager = GameObject.Find( "Log" ).gameObject.GetComponent<LogManager>( );
    }

    void Start( ) {
        //CharacterのSpriteをListで管理。
        _sprite_normal.Add( None );
        for ( int i = 1; i < 7; i++ ) {
            _sprite_normal.Add( _characters.getCharacter( i ).getOutsideImage( OUTSIDE_STATE.NONE ) );
            _sprite_fishing.Add( _characters.getCharacter( i ).getOutsideImage( OUTSIDE_STATE.FISHING ) );
            _sprite_water.Add( _characters.getCharacter( i ).getOutsideImage( OUTSIDE_STATE.WATER ) );
        }

        //初期の画面の状態
        _outside_chara.GetComponent<Image>( ).sprite = _sprite_normal[ 0 ];
        _event_button.SetActive( false );
        _chara_state_button.SetActive( false );
		_state = OUTSIDE_STATE.NONE;

        //変数の初期化
		is_event_update = false;
        if ( PlayerPrefs.GetInt( "TodayEvent" ) == 1 ) {
            today_event_end = true;
        } else {
            today_event_end = false;
        }
        _event_update_time = 25 * 60;
		_working_update_time = 25 * 60;
    }

    void Update( ) {
        drawCharacter( _state );
        UpdateEvent( );
		updateWorking( );

		//OutsideにいるCharacterが死ぬとOutsideにいるCharacterも消す
		if( selected == null ) {
			return;
		}
		if( selected.getStatus( ).death ) {
			_outside_chara.SetActive( false );
		}
    }

	//outsideに人がいるのかを確認
	public bool isNoPeopleOutside( ) {
		for ( int i = 1; i < 7; i++ ) {
			if ( isOutside( _characters.getCharacter( i ) ) ) {
				return false;
			} 
		}
		return true;
	}

    //キャラを描く
    void drawCharacter( OUTSIDE_STATE state ) {
        for ( int i = 1; i < 7; i++ ) {
			if ( !isOutside( _characters.getCharacter( i ) ) ) {
				continue;
			} 
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
			selected = _characters.getCharacter( i );
        }
		if ( isNoPeopleOutside( ) ) {
			_outside_chara.GetComponent<Image>( ).sprite = None;
		}
    }

    //各キャラが今Outsideにいるのかを判断
    bool isOutside( Status character ) {
        if ( !character.getStatus( ).place.Equals( LAYER.OUTSIDE ) ) {
            return false;
        }
        if ( character.getStatus( ).death ) {
            return false;
        }
        return true;
    }

	//RandomEventの描画
    void drawObject( CONTENTS contents ) {
        switch ( contents ) {
            case CONTENTS.ISLAND:
                _event_object.GetComponent<Image>( ).sprite = Island;
                break;
            case CONTENTS.SHIP:
                _event_object.GetComponent<Image>( ).sprite = Ship;
                break;
        }
    }

	//RandomEventのUpdate
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
        if ( today_event_end ) {
            return;
        }

        object active = _event_data.getData( _game_system.randEvent( ), EVENTDATA.ACTIVE );
        if ( active.Equals( 0 ) ) {
            if ( AllTrue( ) ) {
                changeStatusTrue( );
            }
            if ( !AllTrue( ) ) {
                changeStatusFalse( );
            }
        }
        _state = OUTSIDE_STATE.NONE;
		is_event_update = true;

		//UIを設定
        _outside_chara.SetActive( true );
        _log_manager.setLogOpen( true );

		//今日はイベントをやったのをデータにセーブ
		today_event_end = true;
        PlayerPrefs.SetInt( "TodayEvent", 1 );
        PlayerPrefs.Save( );
		Debug.Log( PlayerPrefs.GetInt( "TodayEvent" ) );
    }


	//釣り、浄水のUpdate
	void updateWorking( ) {
        //主人公の時はUPDATEしない
        if ( selected == null ) {
            return;
        }
        //24時後はUpdateしない
		if ( _game_system.getTime( ) >= 24 * 60 && selected.getStatus( ).place == LAYER.OUTSIDE ) {
            //キャラを船に戻す
            if ( !_outside_chara.activeSelf ) {
                _outside_chara.SetActive( true );
            }
            _state = OUTSIDE_STATE.NONE;
            return;
		}

		if ( _game_system.getTime( ) <= _working_update_time ) {
			return;
		}

        //Fishing Update
		if ( _state.Equals( OUTSIDE_STATE.FISHING ) ) {
            //成功確率（数字が高くなると確率があがる）
            const int WORKING_SUCCESS_PROBABILITY = 2;
            int probability = Random.Range( 0, WORKING_SUCCESS_PROBABILITY );
            if ( probability != 0 ) {
				//Plus1を出す
                Vector3 pos = _outside_chara.transform.position;
                GameObject _plus = Instantiate( Plus1, pos, new Quaternion( 0, 0, 0, 0 ) ) as GameObject;
                _plus.transform.parent = _outside_chara.transform;
                //ShipのStatus変更
                _ship_status.setFoods( _ship_status.getResources( ).foods + 1 );
            } else {
				//吹き出し
				Vector3 speech_pos = _outside_chara.transform.position + new Vector3( -40, 180, 0 );
				GameObject _speech = Instantiate( Speech, speech_pos, new Quaternion( 0, 0, 0, 0 ) ) as GameObject;
                _speech.GetComponent<Speech>( ).setSpeech( gameObject, "何も引っかからなかった。" );
            }

			//道具が壊れる。
			//成功確率（数字が高くなると確率が下がる）
			const int TOOL_BROKEN_PROBABILITY = 9;
			int tool_probability = Random.Range( 0, TOOL_BROKEN_PROBABILITY );
			if ( tool_probability == 0 ) {
				//吹き出し
				Vector3 speech_pos = _outside_chara.transform.position + new Vector3( -40, 180, 0 );
				GameObject _speech = Instantiate( Speech, speech_pos, new Quaternion( 0, 0, 0, 0 ) ) as GameObject;
				_speech.GetComponent<Speech>( ).setSpeech( gameObject, "釣竿が壊れてしまった。" );
				//ShipのStatus変更
				_ship_status.setRods( _ship_status.getResources( ).rods - 1 );
			}

			//workingのupdateができないように時間を設定する
			_working_update_time = 25 * 60;
		}

        //Water Update
        if ( _state.Equals( OUTSIDE_STATE.WATER ) ) {
			//成功確率（数字が高くなると確率があがる）
			const int WORKING_SUCCESS_PROBABILITY = 4;
			int probability = Random.Range( 0, WORKING_SUCCESS_PROBABILITY );
            if ( probability != 0 ) {
                //Plus1を出す
                Vector3 pos = _outside_chara.transform.position;
                GameObject _plus = Instantiate( Plus1, pos, new Quaternion( 0, 0, 0, 0 ) ) as GameObject;
				_plus.GetComponent<Plus1>( ).setColor( PLUSCOLOR.BLUE );
                _plus.transform.parent = _outside_chara.transform;
				//ShipのStatus変更
                _ship_status.setWater( _ship_status.getResources( ).water + 1 );
            } else {
                //吹き出し
                Vector3 speech_pos = _outside_chara.transform.position + new Vector3( -40, 180, 0 );
                GameObject _speech = Instantiate( Speech, speech_pos, new Quaternion( 0, 0, 0, 0 ) ) as GameObject;
                _speech.GetComponent<Speech>( ).setSpeech( gameObject, "汚くて飲める気がしない。" );
            }

			//道具が壊れる。
			//成功確率（数字が高くなると確率が下がる）
			const int TOOL_BROKEN_PROBABILITY = 7;
			int tool_probability = Random.Range( 0, TOOL_BROKEN_PROBABILITY );
			if ( tool_probability == 0 ) {
				//吹き出し
				Vector3 speech_pos = _outside_chara.transform.position + new Vector3( -40, 180, 0 );
				GameObject _speech = Instantiate( Speech, speech_pos, new Quaternion( 0, 0, 0, 0 ) ) as GameObject;
				_speech.GetComponent<Speech>( ).setSpeech( gameObject, "道具が壊れてしまった。" );
				//ShipのStatus変更
				_ship_status.setPots( _ship_status.getResources( ).pots - 1 );
			}

			//workingのupdateができないように時間を設定する
			_working_update_time = 25 * 60;
		}
		//characterの状態をReadyに戻す
		_state = OUTSIDE_STATE.NONE;
	}

	//CharacterのOutsideの状態をゲットする
	public OUTSIDE_STATE getOutsideState( ) {
		return _state;
	}

	//釣り、浄水にかかる時間をゲットする
	public float getWorkingTime( ) {
		return WorkingTime;
	}

	//RandomEventがUpdateされたのかをゲットする
    public bool isEventUpdate( ) {
        return is_event_update;
    }
    
	//その日イベント状態をセットする
    public void setTodayEventDone( bool flag ) {
        today_event_end = flag;
    }

	//その日イベントをやったのかをゲットする
	public bool getTodayEventDone( ) {
		return today_event_end;
	}

	//次の日になる
    public void NextDay( ) {
		if ( _gauge != null ) {
			Destroy( _gauge );
			_outside_chara.SetActive( true );
		}
        gameObject.GetComponent<OutsideAnimation>( ).NextDay( );
        drawObject( _event_data.getContent( _game_system.randEvent( ) ) );
        _chara_state_button.SetActive( false );
        is_event_update = false;
        _event_update_time = 25 * 60;
		_working_update_time = 25 * 60;
        _state = OUTSIDE_STATE.NONE;
		if ( selected != null ) {
			selected.setPlace( LAYER.INSIDE );
		}
    }

    public void fishingButton( ) {
        _chara_state_button.SetActive( false );
        //吹き出し
        Vector3 speech_pos = _outside_chara.transform.position + new Vector3( -40, 180, 0 );
        GameObject _speech = Instantiate( Speech, speech_pos, new Quaternion( 0, 0, 0, 0 ) ) as GameObject;
             //Readyの状態じゃないとやらない
        if ( !_state.Equals( OUTSIDE_STATE.NONE ) ) {
            if ( _state.Equals( OUTSIDE_STATE.WATER ) ) {
                _speech.GetComponent<Speech>( ).setSpeech( gameObject, "今は浄水中だよ。" );
            } else {
                _speech.GetComponent<Speech>( ).setSpeech( gameObject, "やっているよ。" );
            }
            return;
		}
		//24時になるとできない。
		if ( _game_system.getTime( ) >= 24 * 60 ) {
            _speech.GetComponent<Speech>( ).setSpeech( gameObject, "今日はもう終わったよ。" );
            return;
		}
        //命令を聞かない状態を確認
        if ( selected.getState( ).Equals( STATE.DEFIANCE ) ) {
            _speech.GetComponent<Speech>( ).setSpeech( gameObject, "お前の命令をなぜやらなければならない？" );
            return;
        }
        //道具があるのか
        if ( _ship_status.getResources( ).rods <= 0 ) {
            _speech.GetComponent<Speech>( ).setSpeech( gameObject, "釣り竿がない。" );
            return;
        }
		is_event_update = false;
		_event_update_time = 25 * 60;
		_state = OUTSIDE_STATE.FISHING;
		_working_update_time = _game_system.getTime( ) + WorkingTime * 60;

		Vector3 pos = _outside_chara.transform.position + new Vector3( -50, 50, 0 );
		_gauge = Instantiate( Gauge, pos, new Quaternion ( 0, 0, 0, 0 ) ) as GameObject;
		_gauge.transform.parent = gameObject.transform;
    }

    public void waterButton( ) {
        _chara_state_button.SetActive( false );
        //吹き出し
        Vector3 speech_pos = _outside_chara.transform.position + new Vector3( -40, 180, 0 );
        GameObject _speech = Instantiate( Speech, speech_pos, new Quaternion( 0, 0, 0, 0 ) ) as GameObject;
            //Readyの状態じゃないとやらない
		if ( _state != OUTSIDE_STATE.NONE ) {
            if ( _state.Equals( OUTSIDE_STATE.FISHING ) ) {
                _speech.GetComponent<Speech>( ).setSpeech( gameObject, "釣り中だから無理。" );
            } else {
                _speech.GetComponent<Speech>( ).setSpeech( gameObject, "やっているよ。" );
            }
            return;
		}
		//24時になるとできない。
		if ( _game_system.getTime( ) >= 24 * 60 ) {
            _speech.GetComponent<Speech>( ).setSpeech( gameObject, "今日はもう終わったよ。" );
            return;
		}
            //命令を聞かない状態を確認
        if ( selected.getState( ).Equals( STATE.DEFIANCE ) ) {
            _speech.GetComponent<Speech>( ).setSpeech( gameObject, "そんなにやりたくないけど" );
            return;
        }
        //道具があるのか
        if ( _ship_status.getResources( ).pots <= 0 ) {
            _speech.GetComponent<Speech>( ).setSpeech( gameObject, "道具がないんだ。" );
            return;
        }
		is_event_update = false;
		_event_update_time = 25 * 60;
        _state = OUTSIDE_STATE.WATER;
		_working_update_time = _game_system.getTime( ) + WorkingTime * 60;

		Vector3 pos = _outside_chara.transform.position + new Vector3( -50, 50, 0 );
		_gauge = Instantiate( Gauge, pos, new Quaternion ( 0, 0, 0, 0 ) ) as GameObject;
		_gauge.transform.parent = gameObject.transform;
    }

    public void normalButton( ) {
        _chara_state_button.SetActive( false );
        Destroy( _gauge );
		is_event_update = false;
		_event_update_time = 25 * 60;
        _state = OUTSIDE_STATE.NONE;
    }

    public void insideButton( ) {
        _chara_state_button.SetActive( false );
        //Readyの状態じゃないとやらない
        if ( _state != OUTSIDE_STATE.NONE ) {
            //吹き出し
            Vector3 speech_pos = _outside_chara.transform.position + new Vector3( -40, 180, 0 );
            GameObject _speech = Instantiate( Speech, speech_pos, new Quaternion( 0, 0, 0, 0 ) ) as GameObject;
            _speech.GetComponent<Speech>( ).setSpeech( gameObject, "仕事中だよ。" );
            return;
        }
        selected.setPlace( LAYER.INSIDE );
    }

    public void drawEventSelectButton( ) {
        if ( _event_button.activeSelf ) {
            _event_button.SetActive( false );
        } else {
            _event_button.SetActive( true );
        }
    }

    public void drawOutsideStateButton( ) {
        if ( _outside_chara.GetComponent<Image>( ).sprite == _sprite_normal[ 0 ] ) {
            Vector3 speech_pos = _outside_chara.transform.position + new Vector3( -40, 180, 0 );
            GameObject _speech = Instantiate( Speech, speech_pos, new Quaternion( 0, 0, 0, 0 ) ) as GameObject;
            _speech.GetComponent<Speech>( ).setSpeech( gameObject, "今日も俺たちは生きているな。" );
            return;
        }
        if ( _chara_state_button.activeSelf ) {
            _chara_state_button.SetActive( false );
        } else {
            _chara_state_button.SetActive( true );
        }
    }

    public void EventYes( ) {
        _event_button.SetActive( false );
		_chara_state_button.SetActive( false );
        //吹き出し
        Vector3 speech_pos = _outside_chara.transform.position + new Vector3( -40, 180, 0 );
        //Outsideに誰もいないときはできない。
		if ( isNoPeopleOutside( ) ) {
			GameObject _speech = Instantiate( Speech, speech_pos, new Quaternion( 0, 0, 0, 0 ) ) as GameObject;
			_speech.GetComponent<Speech>( ).setSpeech( gameObject, "俺は船から出ることはできない。" );
            return;
        }
        //キャラーが船で何かしているとできない。
        if ( !_state.Equals( OUTSIDE_STATE.NONE ) ) {
            if ( _state.Equals( OUTSIDE_STATE.EXPLORE ) ) {
                speech_pos = _event_object.transform.position + new Vector3( -40, 100, 0 );
                GameObject _speech = Instantiate( Speech, speech_pos, new Quaternion( 0, 0, 0, 0 ) ) as GameObject;
                _speech.GetComponent<Speech>( ).setSpeech( gameObject, "探査しているよ。" );
            } else {
                GameObject _speech = Instantiate( Speech, speech_pos, new Quaternion( 0, 0, 0, 0 ) ) as GameObject;
                _speech.GetComponent<Speech>( ).setSpeech( gameObject, "今は忙しいよ。" );
            }
            return;
        }
		//24時になるとできない。
		if ( _game_system.getTime( ) >= 24 * 60 ) {
            GameObject _speech = Instantiate( Speech, speech_pos, new Quaternion( 0, 0, 0, 0 ) ) as GameObject;
            _speech.GetComponent<Speech>( ).setSpeech( gameObject, "今日はもう終わったよ。" );
            return;
		}
        //Loyaltyが低いと命令を聞かない。
        if ( selected.getState( ).Equals( STATE.DEFIANCE ) ) {
            GameObject _speech = Instantiate( Speech, speech_pos, new Quaternion( 0, 0, 0, 0 ) ) as GameObject;
            _speech.GetComponent<Speech>( ).setSpeech( gameObject, "危ないかもしれないのになぜ行かなければならない？" );
            return;
        }
        //イベントをやっている途中じゃないときはイベントのUpdateを初期化する。
        if ( _gauge == null ) {
            is_event_update = false;
        }
        //イベントをやっている途中じゃないときはイベントのUpdateを初期化する。
        if ( today_event_end ) {
            GameObject _speech = Instantiate( Speech, speech_pos, new Quaternion( 0, 0, 0, 0 ) ) as GameObject;
            _speech.GetComponent<Speech>( ).setSpeech( gameObject, "もう行ってきた。" );
            return;
        }

        Vector3 pos = _event_object.transform.position + new Vector3( 0, -200, 0 );
		_gauge = Instantiate( Gauge, pos, new Quaternion ( 0, 0, 0, 0 ) ) as GameObject;
		_gauge.transform.parent = gameObject.transform;

		_state = OUTSIDE_STATE.EXPLORE;
        int event_end = ( int )_event_data.getData( _game_system.randEvent( ), EVENTDATA.TIME );
        _event_update_time = event_end * 60 + _game_system.getTime( );
    }

    public void EventNo( ) {
        _event_button.SetActive( false );
        //冒険中じゃないと反応しない。
        if ( !_state.Equals( OUTSIDE_STATE.EXPLORE ) ) {
            return;
        }
		Destroy( _gauge );
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
        _ship_status.setFuels( _ship_status.getResources( ).fuels + f_fuels );
        _ship_status.setFoods( _ship_status.getResources( ).foods + f_ship_foods );
        _ship_status.setWater( _ship_status.getResources( ).water + f_ship_water );
        _ship_status.setGuns( _ship_status.getResources( ).guns + f_guns );
        _ship_status.setMedicalKits( _ship_status.getResources( ).medical_kits + f_medical_kits );
        _ship_status.setRepairTools( _ship_status.getResources( ).repair_tools + f_repair_tools );
        _ship_status.setRods( _ship_status.getResources( ).rods + f_rods );
        _ship_status.setPots( _ship_status.getResources( ).pots + f_pots );
        _ship_status.setShipBreak( f_ship_break );
        selected.setHealth( selected.getStatus( ).health + f_health );
		if (f_disease == 1) {
			selected.setDisease (true);
		} else if (f_disease == 2) {
			selected.setDisease (false);
		}
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

        _ship_status.setFuels( _ship_status.getResources( ).fuels + t_fuels );
        _ship_status.setFoods( _ship_status.getResources( ).foods + t_ship_foods );
        _ship_status.setWater( _ship_status.getResources( ).water + t_ship_water );
        _ship_status.setGuns( _ship_status.getResources( ).guns + t_guns );
        _ship_status.setMedicalKits( _ship_status.getResources( ).medical_kits + t_medical_kits );
        _ship_status.setRepairTools( _ship_status.getResources( ).repair_tools + t_repair_tools );
        _ship_status.setRods( _ship_status.getResources( ).rods + t_rods );
        _ship_status.setPots( _ship_status.getResources( ).pots + t_pots );
        _ship_status.setShipBreak( t_ship_break );
        selected.setHealth( selected.getStatus( ).health + t_health );
		if (t_disease == 1) {
			selected.setDisease (true);
		} else if (t_disease == 2) {
			selected.setDisease (false);
		}
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
            bool fuels          = _ship_status.getResources( ).fuels         >= s_fuels;
            bool ship_food      = _ship_status.getResources( ).foods         >= s_ship_foods;
            bool ship_water     = _ship_status.getResources( ).water         >= s_ship_water;
            bool gun            = _ship_status.getResources( ).guns          >= s_guns;
            bool medical_kit    = _ship_status.getResources( ).medical_kits  >= s_medical_kits;
            bool repair_tool    = _ship_status.getResources( ).repair_tools  >= s_repair_tools;
            bool rod            = _ship_status.getResources( ).rods          >= s_rods;
            bool pot            = _ship_status.getResources( ).pots          >= s_pots;
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
            bool fuels          = s_fuels == 0          || _ship_status.getResources( ).fuels         <= s_fuels;
            bool ship_food      = s_ship_foods == 0     || _ship_status.getResources( ).foods         <= s_ship_foods;
            bool ship_water     = s_ship_water == 0     || _ship_status.getResources( ).water         <= s_ship_water;
            bool gun            = s_guns == 0           || _ship_status.getResources( ).guns          <= s_guns;
            bool medical_kit    = s_medical_kits == 0   || _ship_status.getResources( ).medical_kits  <= s_medical_kits;
            bool repair_tool    = s_repair_tools == 0   || _ship_status.getResources( ).repair_tools  <= s_repair_tools;
            bool rod            = s_rods == 0           || _ship_status.getResources( ).rods          <= s_rods;
            bool pot            = s_pots == 0           || _ship_status.getResources( ).pots          <= s_pots;
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
