using UnityEngine;
using System.Collections;
using UnityEngine.UI;

//UIのLayerChangeButtonのImage管理
public class LayerChangeButton : MonoBehaviour {
    [SerializeField]
    private Sprite Inside = new Sprite( );
    [SerializeField]
    private Sprite Outside = new Sprite( );
    private GameObject game_system;

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

    public void setImageOutside( ) {
        gameObject.GetComponent<Image>( ).sprite = Outside;
    }
}
