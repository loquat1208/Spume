using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class LogText : MonoBehaviour {
    private OutsideManager _outside_manager;
    private LogManager _log_manager;
    private GameSystem _game_system;
    private EventData _event_data;
    private GameObject _log_sub_select;
	private bool _result_update;

    void Awake( ) {
        _game_system = GameObject.Find( "GameSystem" ).gameObject.GetComponent<GameSystem>( );
        _event_data = GameObject.Find( "EventSystem" ).gameObject.GetComponent<EventData>( );
        _outside_manager = GameObject.Find( "OutsideLayer" ).gameObject.GetComponent<OutsideManager>( );
        _log_manager = GameObject.Find( "Log" ).gameObject.GetComponent<LogManager>( );
        _log_sub_select = GameObject.Find( "SubSelect" ).gameObject;
    }

    void Start( ) {
        _log_sub_select.SetActive( false );
    }

    void Update( ) {
        //Logが開いてないとUPDATEしない。
        if ( !_log_manager.GetComponent<LogManager>( ).isLogOpened( ) ){
            return;
        }

        //各テキストの内容を書く。
        if ( gameObject.name == "LeftPage" ) {
            gameObject.GetComponent<Text>( ).text = subStory( _game_system.getDays( ) );
        }
        if ( gameObject.name == "LeftDownPage" ) {
            gameObject.GetComponent<Text>( ).text = writeRightPage( );
        }
        if ( gameObject.name == "RightPage" ) {
            updateRightUp( );
        }
        if ( gameObject.name == "RightDownPage" ) {
            updateRightDown( );
        }
		if ( gameObject.name == "ResultPage" ) {
			resultPage( );
		}

        //ACTIVEイベントの時、選択肢を出す。
        if ( _event_data.getData( _game_system.randEvent( ), EVENTDATA.ACTIVE ).ToString( ) == "1" ) {
			if ( !_outside_manager.isEventUpdate( ) ) {
                return;
            }
            _log_sub_select.SetActive( true );
        } else {
            _log_sub_select.SetActive( false );
        }
    }

	void updateRightUp( ) {
        //Eventに必要な時間が過ぎていないと表示いない。
        if ( !_outside_manager.isEventUpdate( )
			&& !_outside_manager.getTodayEventDone( )) {
            gameObject.GetComponent<Text>( ).text = "";
            return;
        }
        gameObject.GetComponent<Text>( ).text = _event_data.getData( _game_system.randEvent( ), EVENTDATA.STORY ).ToString( );
    }

    void updateRightDown( ) {
        //Eventに必要な時間が過ぎていないと表示いない。
		if ( !_outside_manager.isEventUpdate( ) 
			&& !_outside_manager.getTodayEventDone( ) ) {
            gameObject.GetComponent<Text>( ).text = "";
            return;
        }
        //イベント化Activeイベントだったら、表示しない。
        if ( ( int )_event_data.getData( _game_system.randEvent( ), EVENTDATA.ACTIVE ) == 1 ) {
            gameObject.GetComponent<Text>( ).text = "";
            return;
        }
        //イベントの条件を全部満足しないと表示しない。
        if ( !_outside_manager.AllTrue( ) ) {
            gameObject.GetComponent<Text>( ).text = "";
            return;
        }
        gameObject.GetComponent<Text>( ).text = _event_data.getData( _game_system.randEvent( ), EVENTDATA.SUBSTORY ).ToString( );
    }

	//イベントの結果を書く
	void resultPage( ) {
		//Eventに必要な時間が過ぎていないと表示いない。
		if ( !_outside_manager.isEventUpdate( )
			&& !_outside_manager.getTodayEventDone( )) {
			gameObject.GetComponent<Text>( ).text = "";
			return;
		}
		if ( _result_update ) {
			return;
		}
		string fuels;
		string foods;
		string water;
		string guns;
		string medi;
		string repair;
		string rods;
		string pots;
		string health;
		if ( _outside_manager.AllTrue( ) ) {
			fuels = _event_data.getData( _game_system.randEvent( ), EVENTDATA.T_FUEL).ToString( );
			foods = _event_data.getData( _game_system.randEvent( ), EVENTDATA.T_SHIP_FOODS ).ToString( );
			water = _event_data.getData( _game_system.randEvent( ), EVENTDATA.T_SHIP_WATER ).ToString( );
			guns = _event_data.getData( _game_system.randEvent( ), EVENTDATA.T_GUNS ).ToString( );
			medi = _event_data.getData( _game_system.randEvent( ), EVENTDATA.T_MEDICAL_KITS ).ToString( );
			repair = _event_data.getData( _game_system.randEvent( ), EVENTDATA.T_REPAIR_TOOLS ).ToString( );
			rods = _event_data.getData( _game_system.randEvent( ), EVENTDATA.T_RODS ).ToString( );
			pots = _event_data.getData( _game_system.randEvent( ), EVENTDATA.T_POTS ).ToString( );
			health = _event_data.getData( _game_system.randEvent( ), EVENTDATA.T_HEALTH ).ToString( );
		} else {
			fuels = _event_data.getData( _game_system.randEvent( ), EVENTDATA.F_FUEL).ToString( );
			foods = _event_data.getData( _game_system.randEvent( ), EVENTDATA.F_SHIP_FOODS ).ToString( );
			water = _event_data.getData( _game_system.randEvent( ), EVENTDATA.F_SHIP_WATER ).ToString( );
			guns = _event_data.getData( _game_system.randEvent( ), EVENTDATA.F_GUNS ).ToString( );
			medi = _event_data.getData( _game_system.randEvent( ), EVENTDATA.F_MEDICAL_KITS ).ToString( );
			repair = _event_data.getData( _game_system.randEvent( ), EVENTDATA.F_REPAIR_TOOLS ).ToString( );
			rods = _event_data.getData( _game_system.randEvent( ), EVENTDATA.F_RODS ).ToString( );
			pots = _event_data.getData( _game_system.randEvent( ), EVENTDATA.F_POTS ).ToString( );
			health = _event_data.getData( _game_system.randEvent( ), EVENTDATA.F_HEALTH ).ToString( );
		}
		gameObject.GetComponent<Text>( ).text = "FUELS : " + fuels
											+ " FOODS : " + foods
											+ " WATER : " + water + "\n"
                                            + "ROD : " + rods
                                            + " POT : " + pots
											+ " REPAIRTOOL : " + repair + "\n"
											+ "MEDIKIT : " + medi
                                            + " GUN : " + guns
											+ " HEALTH : " + health;
	}

	//RandomEventの説明
    string writeRightPage( ) {
        string article = _event_data.getData( _game_system.randEvent( ), EVENTDATA.ARTICLE ).ToString( );
        switch ( article ) {
            case "Island":
                return "今日は島が発見された。";
            case "Ship":
                return "今日は船が見える。";
            default:
                return "何も見つけませんでした。";
        }
    }

	//日記
    string subStory( int days ) {
        string sub_story;
        switch ( days ) {
            case 1:
                sub_story = "一体船に何事が起きたのか、最近ずっと食べることができなくて気力もあまりないと思うのによく脱出できたのか。そして俺たちは今からどうすればいいのか。";
                break;
			case 2:
				sub_story = "とりあえずみんなが持っている食料と水をまとまった。これではそんなに長い時間は生存できない。何かしないと・・・";
				break;
			case 3:
				sub_story = "氷河が溶けた後、どのぐらいの時間がすぎたのか。なぜ俺たちは生き残っているのか。こんなに辛い世界で・・・";
				break;
			case 4:
				sub_story = "俺たちは今、犬とオオカミの時間で暮らしている。希望も絶望も区別できない時間に・・・。なぜ自然はそんなに残酷なのか。";
				break;
			case 5:
				sub_story = "無意味な生存の続きでみんな疲れてしまった。でも、俺もまだ生き残る理由を見つけていないんだ。責任感？俺がそんなに素晴らしい人だったの？";
				break;
			case 6:
				sub_story = "手帳にはラジオの周波数と座標が書いてあった。そしてその下に書いてある日付と「失敗」の文字。あの人は何を聞いたのか、そしてこの座標がさしている場所には何かがあるのか。";
				break;
			case 7:
				sub_story = "同僚に手帳に書いてある内容を言った。この場所に行こうと意見が集まった。そして各自この場所にあるらしいことをいい始まった。";
				break;
			case 8:
				sub_story = "人生に目的がある、ないことで人の行動がこんなに変わるのか。みんなこの前に俺が知っている人と同じ人なのか。信じられない";
				break;
			case 9:
				sub_story = "もっと早く行けないのかむずかれている。手帳に書いてある場所に何かがあるのかは分からないのに、もういいものだと確信しているそうにみえた。";
				break;
			case 10:
				sub_story = "向こうに黒い雲が見える。明日は多い雨が降りそうだ。今日は寝らずに船を運転するしかないか・・・";
				break;
			case 11:
				sub_story = "海賊の死体は海に投げた。同僚の顔には人を殺した罪悪感は見えなかった。雨と一緒に洗った出されたのか。俺の手はまだ震えているのに・・・。";
				break;
			case 12:
				sub_story = "俺と戦った人は今生きているのか。あの人の最後の笑顔がまだ頭の中に残っている。なぜそんな時笑うことができるの・・・。";
				break;
			case 13:
				sub_story = "いつ雨が降ったのかそうないい天気の続きだ。同僚は言葉が少なくなった。音がなくなったこの空間にはどんな考えが浮かんでいるのか";
				break;
			case 14:
				sub_story = "座標に表示している場所に半分ぐらい届いた。残った半分の旅程は無事に行くことができるのか。神が存在するなら、俺達の道に祝福を・・・。";
				break;
			case 15:
				sub_story = "船のエンジン室からおかしい音を聞いた気がする。同僚にきいたけど、同僚は普通のエンジンの落としか聞こえなかったと言った。気のせいだったのか";
				break;
			case 16:
				sub_story = "名言";
				break;
			case 17:
				sub_story = "名言";
				break;
			case 18:
				sub_story = "名言";
				break;
			case 19:
				sub_story = "名言";
				break;
			case 20:
				sub_story = "名言";
				break;
			case 21:
				sub_story = "名言";
				break;
			case 22:
				sub_story = "名言";
				break;
			case 23:
				sub_story = "名言";
				break;
			case 24:
				sub_story = "名言";
				break;
			case 25:
				sub_story = "名言";
				break;
			case 26:
				sub_story = "名言";
				break;
			case 27:
				sub_story = "名言";
				break;
			case 28:
				sub_story = "名言";
				break;
			case 29:
				sub_story = "名言";
				break;
			case 30:
				sub_story = "名言";
				break;
            default:
                sub_story = "SubStory";
                break;
        }
        return sub_story;
    }
}
