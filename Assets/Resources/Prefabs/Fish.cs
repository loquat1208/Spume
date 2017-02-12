using UnityEngine;
using System.Collections;

public class Fish : MonoBehaviour {
    public GameObject Plus;
    public float LifeTime = 10f;
	public int _HP_max = 3;

    private GameObject HP_image;
    private GameObject HP_gauge;
    private GameObject _game_system;
    private int _HP;
    private float _timer;

    private void Awake( ) {
        HP_gauge = GameObject.Find( "HPGauge" ).gameObject;
        HP_image = GameObject.Find( "HP" ).gameObject;
        _game_system = GameObject.Find( "GameSystem" ).gameObject;
    }

    void Start( ) {
        _HP_max = 3;
        _HP = _HP_max;
        HP_image.SetActive( false );
        transform.position = new Vector3( 0, -450f, 0 );
		transform.parent = GameObject.Find( "Sea03" ).transform;
    }

	void Update( ) {
        _timer += Time.deltaTime * _game_system.GetComponent<GameSystem>( ).getTimerSpeed( );
		GetComponent<Animator>( ).speed = _game_system.GetComponent<GameSystem>( ).getTimerSpeed( );
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
        float pos_x = 300 * Mathf.Cos( ( _timer * 0.2f ) * Mathf.PI ) + 300f;
        float pos_y = 300 * Mathf.Sin( _timer * 0.2f * Mathf.PI ) - 400f;
        transform.position = new Vector3( pos_x, pos_y, 0 );
	}

	//魚のHPゲージが減ること（今は使わない）
    public void Attack( ) {
        if ( !HP_image.activeSelf ) {
            HP_image.SetActive( true );
        }
        _HP--;
        HP_gauge.transform.localScale = new Vector3( ( 100f / _HP_max ) * _HP, 100, 100 );
    }

    public void setMaxHP( int max_hp ) {
        _HP_max = max_hp;
        _HP = max_hp;
    }
}
