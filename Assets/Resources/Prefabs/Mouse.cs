using UnityEngine;
using System.Collections;

public class Mouse : MonoBehaviour {
    public GameObject Plus;
    public float LifeTime = 100f;
    public int MaxHP = 5;

    private GameObject HP_image;
    private GameObject HP_gauge;
    private GameObject _game_system;
    private int _HP;
    private int _Random;
    private float _timer;

    private void Awake( ) {
        HP_gauge = GameObject.Find( "HPGauge" ).gameObject;
        HP_image = GameObject.Find( "HP" ).gameObject;
        _game_system = GameObject.Find( "GameSystem" ).gameObject;
    }

    void Start( ) {
        _Random = Random.Range( 0, 3 );
        _HP = MaxHP;
        HP_image.SetActive( false );
        transform.position = new Vector3( 0, -450f, 0 );
		transform.parent = GameObject.Find( "InsideBackground" ).transform;
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
		//Mouseが出る場所
        switch ( _Random ) {
            case 0:
                transform.position = new Vector3( 700, -339, 0 );
                break;
            case 1:
                transform.position = new Vector3( -350, 325, 0 );
                break;
            case 2:
                transform.position = new Vector3( 300, 440, 0 );
                break;
            case 3:
                transform.position = new Vector3( -600, 325, 0 );
                break;
        }
    }

    public void Attack( ) {
        /*if ( !HP_image.activeSelf ) {
            HP_image.SetActive( true );
        }*/
        _HP--;
        //HP_gauge.transform.localScale = new Vector3( ( 100f / MaxHP ) * _HP, 100, 100 );
    }
}
