using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class LayerChangeButton : MonoBehaviour {
    [SerializeField]
    private Sprite Inside;
    [SerializeField]
    private Sprite Outside;
    private GameObject game_system;

    // Use this for initialization
    void Start( ) {
        game_system = GameObject.Find( "GameSystem" ).gameObject;
    }

    public void changeImage( ) {
        LAYER layer = game_system.GetComponent<GameSystem>( ).getLayer( );
        if ( layer == LAYER.OUTSIDE ) {
            gameObject.GetComponent<Image>( ).sprite = Inside;
        } else {
            gameObject.GetComponent<Image>( ).sprite = Outside;
        }
    }

    public void setImageInside( ) {
        gameObject.GetComponent<Image>( ).sprite = Inside;
    }
}
