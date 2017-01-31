using UnityEngine;
using System.Collections;

public class TitleAnimationManager : MonoBehaviour {
    public float Layer1Ampplitude;
    public float Layer2Ampplitude;
    public float Layer3Ampplitude;
    public float Layer4Ampplitude;
    public float Layer5Ampplitude;
    public float Layer6Ampplitude;

    private GameObject layer1;
    private GameObject layer2;
    private GameObject layer3;
    private GameObject layer4;
    private GameObject layer5;
    private GameObject layer6;

    private GameObject star0;
    private GameObject star1;
    private GameObject star2;
    private GameObject star3;
    //private GameObject star4;

    private GameObject ship_light;

    private float _timer;

    // Use this for initialization
    void Start () {
        layer1 = GameObject.Find( "layer1" ).gameObject;
        layer2 = GameObject.Find( "layer2" ).gameObject;
        layer3 = GameObject.Find( "layer3" ).gameObject;
        layer4 = GameObject.Find( "layer4" ).gameObject;
        layer5 = GameObject.Find( "layer5" ).gameObject;
        layer6 = GameObject.Find( "layer6" ).gameObject;

        star0 = GameObject.Find( "Star0" ).gameObject;
        star1 = GameObject.Find( "Star1" ).gameObject;
        star2 = GameObject.Find( "Star2" ).gameObject;
        star3 = GameObject.Find( "Star3" ).gameObject;
        //star4 = GameObject.Find( "Star4" ).gameObject;

        ship_light = GameObject.Find( "ShipLight" ).gameObject;
    }
	
	// Update is called once per frame
	void Update () {
        _timer += Time.deltaTime;
        layerWave( layer1, Layer1Ampplitude, _timer, 1, 0 );
        layerWave( layer2, Layer2Ampplitude, _timer, 2, 0 );
        layerWave( layer3, Layer3Ampplitude, _timer, 3, 0 );
        layerWave( layer4, Layer4Ampplitude, _timer, 4, 0 );
        layerWave( layer5, Layer5Ampplitude, _timer, 3, 0 );
        layerWave( layer6, Layer6Ampplitude, _timer, 2, -30 );
        starTwingcle( star0, _timer, 12.0f, 0.1f );
        starTwingcle( star1, _timer, 10.0f, 0.9f );
        starTwingcle( star2, _timer, 8.0f, 0.6f );
        starTwingcle( star3, _timer, 6.0f, 0.4f );
        //starTwingcle( star4, _timer, 3.0f, 0.2f );

        starTwingcle( ship_light, _timer, 3.0f, 0.0f );
    }

    void layerWave( GameObject obj, float ampplitude, float timer, float speed, float s_pos ) {
        Vector3 pos = obj.transform.position;
        pos.y = ampplitude * Mathf.Sin( timer * Mathf.PI / speed ) + s_pos;
        obj.transform.position = pos;
    }

    void starTwingcle( GameObject obj, float timer, float speed, float delay ) {
        Color color = obj.GetComponent<SpriteRenderer>( ).material.color;
        color.a = Mathf.Sin( ( timer - delay ) * Mathf.PI / speed );
        obj.GetComponent<SpriteRenderer>( ).material.color = color;
    }
}
