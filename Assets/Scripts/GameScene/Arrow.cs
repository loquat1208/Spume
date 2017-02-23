using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : MonoBehaviour {
	private int _arrow_num;
	private GameSystem _game_system;

	void Awake( ) {
		_game_system = GameObject.Find( "GameSystem" ).gameObject.GetComponent<GameSystem>( );
	}

	void Start( ) {
		_arrow_num = 0;
		//最初の日じゃないとDestroy
		if ( _game_system.getDays( ) > 1 ) {
			Destroy( gameObject );
		}
	}
		
	public void goToUI( ) {
		if ( _arrow_num != 0 ) {
			return;
		}
		Vector3 pos = GameObject.Find( "page2" ).transform.position;
		transform.position = new Vector3( pos.x + 50, transform.position.y, 0 );
		_arrow_num++;
	}

	public void goToOutside( ) {
		if ( _arrow_num != 1 ) {
			return;
		}
		Vector3 pos = GameObject.Find( "page3" ).transform.position;
		transform.position = new Vector3( pos.x + 50, transform.position.y, 0 );
		_arrow_num++;
	}

	public void goToInside( ) {
		if ( _arrow_num != 2 ) {
			return;
		}
		Vector3 pos = GameObject.Find( "page4" ).transform.position;
		transform.position = new Vector3( pos.x + 50, transform.position.y, 0 );
		_arrow_num++;
	}

	public void goToClose( ) {
		if ( _arrow_num != 3 ) {
			return;
		}
		Vector3 pos = GameObject.Find( "Close" ).transform.position;
		transform.position = new Vector3( pos.x + 50, transform.position.y - 30, 0 );
	}

	public void deleteArrow( ) {
		Destroy( gameObject );
	}
}
