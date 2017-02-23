using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LogIcon : MonoBehaviour {
	public Sprite Minus;
	public Sprite Zero;
	public Sprite Plus;

	private LogManager _log_manager;
	private EventData _event_data;
	private OutsideManager _outside_manager;
	private GameSystem _game_system;

	void Awake( ) {
		_log_manager = GameObject.Find( "Log" ).gameObject.GetComponent<LogManager>( );
		_event_data = GameObject.Find( "EventSystem" ).gameObject.GetComponent<EventData>( );
		_outside_manager = GameObject.Find( "OutsideLayer" ).gameObject.GetComponent<OutsideManager>( );
		_game_system = GameObject.Find( "GameSystem" ).gameObject.GetComponent<GameSystem>( );
	}

	void Update( ) {
		//Logが開いてないとUPDATEしない。
		if ( !_log_manager.isLogOpened( ) ){
			return;
		}
		changeImage( );
	}

	void changeImage( ) {
		//Eventに必要な時間が過ぎていないと表示いない。
		if ( !_outside_manager.isEventUpdate( )
			&& !_outside_manager.getTodayEventDone( )) {
			return;
		}

		int num = 0;
		switch ( gameObject.name ) {
			case "FuelResult":
				num = getNum( EVENTDATA.T_FUEL, EVENTDATA.F_FUEL );
				break;
			case "FoodResult":
				num = getNum( EVENTDATA.T_SHIP_FOODS, EVENTDATA.F_SHIP_FOODS );
				break;
			case "WaterResult":
				num = getNum( EVENTDATA.T_SHIP_WATER, EVENTDATA.F_SHIP_WATER );
				break;
			case "RodResult":
				num = getNum( EVENTDATA.T_RODS, EVENTDATA.F_RODS );
				break;
			case "PotResult":
				num = getNum( EVENTDATA.T_POTS, EVENTDATA.F_POTS );
				break;
			case "MediResult":
				num = getNum( EVENTDATA.T_MEDICAL_KITS, EVENTDATA.F_MEDICAL_KITS );
				break;
			case "HealthResult":
				num = getNum( EVENTDATA.T_HEALTH, EVENTDATA.F_HEALTH );
				break;
		}

		if ( num < 0 ) {
			gameObject.GetComponent<Image>( ).sprite = Minus;
		}
		if ( num == 0 ) {
			gameObject.GetComponent<Image>( ).sprite = Zero;
		}
		if ( num > 0 ) {
			gameObject.GetComponent<Image>( ).sprite = Plus;
		}
	}

	int getNum( EVENTDATA _true, EVENTDATA _false ) {
		int num;
		if ( _outside_manager.AllTrue( ) ) {
			num = ( int )_event_data.getData( _game_system.randEvent( ), _true );
		} else {
			num = ( int )_event_data.getData( _game_system.randEvent( ), _false );
		}
		return num;
	}
}