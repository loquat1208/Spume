using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bird : MonoBehaviour {
	public GameObject Plus;
	public float Speed = 2f;
	public float LifeTime = 15f;

	private GameObject _game_system;
	private int _HP;
	private int _HP_max;
	private float _timer;

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
		_timer += Time.deltaTime * _game_system.GetComponent<GameSystem>( ).getTimerSpeed( );
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
		transform.position -= new Vector3( Speed, 0, 0 );
	}

	//魚のHPゲージが減ること（今は使わない）
	public void Attack( ) {
		_HP--;
	}

	public void setMaxHP( int max_hp ) {
		_HP_max = max_hp;
		_HP = max_hp;
	}
}
