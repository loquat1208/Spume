using UnityEngine;
using System.Collections;

public class GaugeBar : MonoBehaviour {
	private GameSystem _game_system;
	private EventData _event_data;
	private OutsideManager _outside_manager;
	private GameObject _gauge_bar;
	private float _gauge;
	private float _gauge_max;
	private float _speed;

	void Awake( ) {
		_game_system = GameObject.Find( "GameSystem" ).gameObject.GetComponent<GameSystem>( );
		_event_data = GameObject.Find( "EventSystem" ).gameObject.GetComponent<EventData>( );
		_outside_manager = GameObject.Find( "OutsideLayer" ).gameObject.GetComponent<OutsideManager>( );
		_gauge_bar = GameObject.Find( "GaugeBar" ).gameObject;
		_gauge = 0;
	}

	void Start( ) {
		_speed = _game_system.getTimerSpeed( );
		switch ( _outside_manager.getOutsideState( ) ) {
			case OUTSIDE_STATE.EXPLORE:
				_gauge_max = ( int )_event_data.getData( _game_system.randEvent( ), EVENTDATA.TIME );
				break;
			case OUTSIDE_STATE.FISHING:
				_gauge_max = _outside_manager.getWorkingTime( );
				break;
			case OUTSIDE_STATE.WATER:
				_gauge_max = _outside_manager.getWorkingTime( );
				break;
		}
	}

	void Update( ) {
		if ( _gauge > _gauge_max * 60 ) {
			Destroy( gameObject );
		}
		if ( _game_system.getTime( ) >= 24 * 60 ) {
			Destroy( gameObject );
		}
		_gauge += _speed * Time.deltaTime;
		_gauge_bar.transform.localScale = new Vector3 ( _gauge / ( _gauge_max * 60 ), 1, 1 );
	}
}
