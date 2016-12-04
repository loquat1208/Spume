using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class InsideCharacterFace : MonoBehaviour {
    [SerializeField]
    private Sprite Normal;
    [SerializeField]
    private Sprite Sick;
    [SerializeField]
    private Sprite Hungry;
    [SerializeField]
    private Sprite Thirsty;
    [SerializeField]
    private Sprite Defiance;
    [SerializeField]
    private Sprite Death;

	private GameSystem _game_system;

    void Start( ) {
		_game_system = GameObject.Find( "GameSystem" ).gameObject.GetComponent<GameSystem>( );
	}

	void Update ( ) {
        //Insideの時だけ動く。
		if ( _game_system.getLayer( ) != LAYER.INSIDE ) {
			return;
		}
        setFace( );
    }

    void setFace( ) {
        switch ( gameObject.GetComponent<Status>( ).getState( ) ) {
            case STATE.NORMAL:
                gameObject.GetComponent<Image>( ).sprite = Normal;
                break;
            case STATE.SICK:
                gameObject.GetComponent<Image>( ).sprite = Sick;
                break;
            case STATE.HUNGRY:
                gameObject.GetComponent<Image>( ).sprite = Hungry;
                break;
            case STATE.THIRSTY:
                gameObject.GetComponent<Image>( ).sprite = Thirsty;
                break;
            case STATE.DEFIANCE:
                gameObject.GetComponent<Image>( ).sprite = Defiance;
                break;
            case STATE.DEATH:
                gameObject.GetComponent<Image>( ).sprite = Death;
                break;
            default:
                gameObject.GetComponent<Image>( ).sprite = Normal;
                break;
        }
    }
}
