using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bird : MonoBehaviour {
	public GameObject Plus;
	public float Speed = 1f;
	public float LifeTime = 20f;
	public int _HP_max = 3;

	private GameObject _game_system;
	private int _HP;
	private float _timer;
	private float _game_speed;

	private void Awake( ) {
		_game_system = GameObject.Find( "GameSystem" ).gameObject;
	}

	void Start( ) {
		_HP_max = 3;
		_HP = _HP_max;
		transform.position = new Vector3( 1000, Random.Range( 0, 400 ), 0 );
		transform.parent = GameObject.Find( "OutsideLayer" ).transform;
	}

	void Update( ) {
		_game_speed = _game_system.GetComponent<GameSystem> ().getTimerSpeed ();
		_timer += Time.deltaTime * _game_speed;
		GetComponent<Animator>( ).speed = _game_speed;
		//HPが０より小さいと死ぬ
		if ( _HP <= 0 ) {
			ShipStatus _ship_status = _game_system.GetComponent<ShipStatus>( );
			_ship_status.setFoods( _ship_status.getResources( ).foods + 1 );
			Instantiate( Plus, transform.position, new Quaternion( ), GameObject.Find( "UILayer" ).gameObject.transform );
			Destroy( gameObject );
		}
		//一定時間がすぎると死ぬ
		if ( _timer > LifeTime ) {
			Destroy( gameObject );
		}
		//魚の動き
		transform.position -= new Vector3( _game_speed * Speed, 0, 0 );
	}

	//魚のHPゲージが減ること（今は使わない）
	public void Attack( ) {
		if ( _game_system.GetComponent<ShipStatus>( ).getResources( ).guns != 0 ) {
			_HP--;
		}
	}

	public void setMaxHP( int max_hp ) {
		_HP_max = max_hp;
		_HP = max_hp;
	}
}
