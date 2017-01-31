using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public enum LAYER {
    INSIDE,
    OUTSIDE,
}

public class GameSystem : MonoBehaviour {
    public int MAIN_EVENT_INTERVAL = 10;
    public int MAIN_EVENT_MAX = 3;
    public float DefaultTimeSpeed = 1;
    public int RANDOM_OBJECT_APPEAR_MAX = 3;
    public List<GameObject> RandomObjects;

    private int max_days;
    private int days;
    private float time;
    private float _timer_speed;
    private LAYER _layer;
    private int rand_event;
    private List<float> _appear_time = new List<float>( );
    private int _random_object;

    private GameObject outside_layer;
    private GameObject fade_layer;
    private Characters _characters;
    private EventData _event_data;
    private ShipStatus _ship_status;
    private GameObject watch;
    private GameObject fade;
	private GameObject fade_text;
	private GameObject death_num;
    private GameObject death;
    private GameObject log;
    private GameObject _log_sub_select;

    void Awake( ) {
        max_days = MAIN_EVENT_INTERVAL * MAIN_EVENT_MAX + 1;

        outside_layer = GameObject.Find( "OutsideLayer" ).gameObject;
        fade_layer = GameObject.Find( "FadeLayer" ).gameObject;
        _characters = GameObject.Find( "Characters" ).gameObject.GetComponent<Characters>( );
        _event_data = GameObject.Find( "EventSystem" ).gameObject.GetComponent<EventData>( );
        rand_event = Random.Range( 0, ( int )_event_data.getMaxData( ) );
        fade = GameObject.Find( "Fade" ).gameObject;
		fade_text = GameObject.Find( "FadeText" ).gameObject;
		death_num = GameObject.Find( "DeathNum" ).gameObject;
		death = GameObject.Find( "Death" ).gameObject;
        watch = GameObject.Find( "Watch" ).gameObject;
        log = GameObject.Find( "Log" ).gameObject;
        _log_sub_select = GameObject.Find( "SubSelect" ).gameObject;
        _ship_status = gameObject.GetComponent<ShipStatus>( );
        init( );
    }

    void Start( ) {
        PlayerPrefs.SetInt( "Scene", SceneManager.GetActiveScene( ).buildIndex );
        PlayerPrefs.Save( );
        _timer_speed = DefaultTimeSpeed;
        SetAppearTime( );
    }

    void Update( ) {
		changeScene( );
        updateWatch( );
        RandomObject( );

        //24:00後は計算しない。
        if ( time >= 60 * 24 ) {
            return;
        }
        time += _timer_speed * Time.deltaTime;
    }

	//RandomObjectの登場時間を決める
    void SetAppearTime( ) {
		_random_object = PlayerPrefs.GetInt( "RandomObject" );//RandomObjectが出た回数をロード

        int appear_num = Random.Range( 1, RANDOM_OBJECT_APPEAR_MAX );
        for ( int i = 0; i < appear_num; i++ ) {
            float appear_time = Random.Range( 12 * 60, 24 * 60 - 10f );
            _appear_time.Add( appear_time );
        }
        _appear_time.Sort( );
    }

	//RandomObjectを出す
    void RandomObject( ) {
        for ( int i = 0; i < _appear_time.Count; i++ ) {
            if ( time > _appear_time[ i ] && _random_object.Equals( i ) ) {
                Instantiate( RandomObjects[ Random.Range( 0, RandomObjects.Count - 1 ) ] );
				//RandomObjectが出た回数をセーブ
                _random_object++;
                PlayerPrefs.SetInt( "RandomObject", _random_object );
                PlayerPrefs.Save( );
            }
        }
    }

    void init( ) {
		//新しいゲームの時の初期化
		if ( PlayerPrefs.GetInt( "Days" ).Equals( 0 ) ) {
			_characters.setNewGame( );
			_ship_status.setNewShip( );
			time = 12 * 60;
			PlayerPrefs.SetFloat( "Time", time );
			PlayerPrefs.SetInt( "Days", 1 );
			PlayerPrefs.Save( );
			GameObject.Find( "HelpButton" ).gameObject.GetComponent<Image>().material = Resources.Load( "Lim", typeof( Material ) ) as Material;
		} else {
			GameObject.Find( "Arrow" ).gameObject.SetActive( false );
		}
		//ロードゲームの時
        days = PlayerPrefs.GetInt( "Days" );
        StartCoroutine( "FadeIn" );
        time = PlayerPrefs.GetFloat( "Time" );
        _layer = LAYER.OUTSIDE;
    }

	//ゲームの時間のUpdate
    void updateWatch( ) {
        int hour = ( int )time / 60;
        int mint = ( int )time % 60;
        watch.GetComponent<Text>( ).text = " Day " + days.ToString( )
            + hour.ToString( "  00" ) + " : " + mint.ToString( "00" );
    }

	//ゲームを閉じた時データを保存する
    void OnApplicationQuit( ) {
        dataSave( );
        PlayerPrefs.SetInt( "Scene", SceneManager.GetActiveScene( ).buildIndex );
		PlayerPrefs.SetFloat( "Time", time );
        PlayerPrefs.Save( );
    }

	//時間の速さのDefaultをゲットする
    public float getTimerSpeedDefault( ) {
        return DefaultTimeSpeed;
    }

	//ゲームの時間の速さをセットする
    public void setTimerSpeed( float speed ) {
        _timer_speed = speed;
    }

	//ゲームの時間の速さをゲットする
	public float getTimerSpeed( ) {
		return _timer_speed;
	}

	//Gameの現在時間をとる
    public float getTime( ) {
        return time;
    }

	//現在のランダムイベントがなんなのかとる
    public int randEvent( ) {
        return rand_event;
    }

	//GameOverのかを確認する
    bool gameOver( ) {
		//キャラが全部死ぬとゲームオーバー
        if ( _characters.allDeath( ) ) {
            return true;
        }
		//船に燃料がなくなるとゲームオーバー
        if ( _ship_status.getResources( ).fuels <= -1 ) {
            return true;
        }
		//船が壊れた状態なのに修理キットがないとゲームオーバー
		if ( _ship_status.getResources( ).ship_break && _ship_status.getResources( ).repair_tools <= 0 ) {
            return true;
        }
        return false;
    }

	//ゲームのSceneを変える
    void changeScene( ) {
        if ( gameOver( ) ) {
            _characters.setNewGame( );
            _ship_status.setNewShip( );
            PlayerPrefs.SetInt( "GameOver", 1 );
            PlayerPrefs.SetInt( "LoadGame", 0 );
            SceneManager.LoadScene( "GameOverScene" );
        }
        if ( days > max_days ) {
            SceneManager.LoadScene( "EndingScene" );
        }
        if ( days % MAIN_EVENT_INTERVAL == 0 ) {
            int event_num = days / MAIN_EVENT_INTERVAL;
            PlayerPrefs.SetInt( "EventNumber", event_num );
			PlayerPrefs.Save( );
            NextDay( );
            SceneManager.LoadScene( "GameLoading" );
        }
    }

	//DayとDayの間に暗転を入れる
	IEnumerator FadeIn( ) {
		//死んだキャラの数を表示
		int alive_num = 0;
		for ( int i = 1; i < 7; i++ ) {
			if( _characters.getCharacter( i ).getStatus( ).death ) {
				alive_num++;
			}
		}
		death_num.GetComponent<Text>( ).text = "X  " + alive_num.ToString( );
		//何日目なのかを表示
		fade_text.GetComponent<Text>( ).text = "Day " + days.ToString( );
		for ( float i = 0; i <= 180; i += 2f ) {
			//次の日のobjectを描画
			if ( i.Equals( 90 ) ) {
                outside_layer.GetComponent<OutsideManager>( ).NextDay( );
                log.GetComponent<LogManager>( ).setLogOpen( true );
			}
			//暗転
            fade.GetComponent<Image>( ).color = new Color( 0, 0, 0, Mathf.Sin( i * Mathf.PI / 180f ) );
			fade_text.GetComponent<Text>( ).color = new Color( 255, 255, 255, Mathf.Sin( i * Mathf.PI / 180f ) );
			death.GetComponent<Image>( ).color = new Color( 255, 255, 255, Mathf.Sin( i * Mathf.PI / 180f ) );
			death_num.GetComponent<Text>( ).color = new Color( 255, 255, 255, Mathf.Sin( i * Mathf.PI / 180f ) );
			yield return 0;
		}
        fade_layer.SetActive( false );
	} 

    public void NextDay( ) {
        //時間の管理
        days++;
        time = 60 * 12;
        _timer_speed = DefaultTimeSpeed;

        //FadeInOut
        fade_layer.SetActive( true );
        StartCoroutine( "FadeIn" );

        //新しいRandomEvevt
        rand_event = Random.Range( 0, ( int )_event_data.getMaxData( ) );

        //RandomObject管理
        SetAppearTime( );
        _random_object = 0;

        //各Status管理
        _ship_status.setFuels( _ship_status.getResources( ).fuels - 1 );
        _characters.nextDay( );
        dataSave( );
        
        //Logを閉める
        log.GetComponent<LogManager>( ).setSubSelect( false );
        _log_sub_select.SetActive( false );

        //OutsideEventの初期化
        outside_layer.GetComponent<OutsideManager>( ).setTodayEventDone( false );
    }
    
	//Dataの保存
    public void dataSave( ) {
        _ship_status.saveData( );
        PlayerPrefs.SetInt( "Days", days );
		PlayerPrefs.SetFloat( "Time", time );
		PlayerPrefs.SetInt( "TodayEvent", 0 );
        PlayerPrefs.Save( );
    }
	
    public LAYER getLayer( ) { return _layer; }
    public void setLayer( LAYER layer ) { _layer = layer; }
    public int getDays( ) { return days; }
}
