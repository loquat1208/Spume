using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class MovieManager : MonoBehaviour {
    [SerializeField]
    private float CameraSpeed; 
    [SerializeField]
    private float TimeScene0;
    [SerializeField]
    private Vector3 PosScene0;
    [SerializeField]
    private float TimeScene1;
    [SerializeField]
    private Vector3 PosScene1;
    [SerializeField]
    private float TimeScene2;
    [SerializeField]
    private Vector3 PosScene2;
    [SerializeField]
    private float TimeScene3;
    [SerializeField]
    private Vector3 PosScene3;
    [SerializeField]
    private float TimeScene4;
    [SerializeField]
    private Vector3 PosScene4;
    [SerializeField]
    private float TimeScene5;
    [SerializeField]
    private Vector3 PosScene5;
    [SerializeField]
    private float TimeScene6;
    [SerializeField]
    private Vector3 PosScene6;
    [SerializeField]
    private float TimeScene7;
    [SerializeField]
    private Vector3 PosScene7;
    [SerializeField]
    private float TimeScene8;
    [SerializeField]
    private Vector3 PosScene8;
    [SerializeField]
    private float TimeScene9;
    [SerializeField]
    private Vector3 PosScene9;
    [SerializeField]
    private float TimeScene10;
    [SerializeField]
    private Vector3 PosScene10;
    [SerializeField]
    private float TimeScene11;
    [SerializeField]
    private Vector3 PosScene11;
    [SerializeField]
    private float TimeScene12;
    [SerializeField]
    private Vector3 PosScene12;

	private GameObject icon_1_1;
	private GameObject icon_1_2;
	private GameObject icon_1_3;
	private GameObject icon_5_1;
	private GameObject icon_5_2;
	private GameObject icon_8_1;
	private GameObject icon_8_2;
	private GameObject cut9;
	private GameObject icon_9_1;
	private GameObject icon_9_2;
	private GameObject icon_10_1;
	private GameObject icon_10_2;
	private GameObject icon_12_1;
    private int _select;
    private float _timer;
    private int _scene_num;
    private GameObject _camera;

    // Use this for initialization
    void Start ( ) {
        _camera = GameObject.Find( "Main Camera" ).gameObject;
        _select = PlayerPrefs.GetInt( "Select" );
		icon_1_1 = GameObject.Find( "Icon1_1" ).gameObject;
		icon_1_2 = GameObject.Find( "Icon1_2" ).gameObject;
		icon_1_3 = GameObject.Find( "Icon1_3" ).gameObject;
		icon_5_1 = GameObject.Find( "Icon5_1" ).gameObject;
		icon_5_2 = GameObject.Find( "Icon5_2" ).gameObject;
		icon_8_1 = GameObject.Find( "Icon8_1" ).gameObject;
		icon_8_2 = GameObject.Find( "Icon8_2" ).gameObject;
		cut9 = GameObject.Find ("Cut9").gameObject;
		icon_9_1 = GameObject.Find( "Icon9_1" ).gameObject;
		icon_9_2 = GameObject.Find( "Icon9_2" ).gameObject;
		icon_10_1 = GameObject.Find( "Icon10_1" ).gameObject;
		icon_10_2 = GameObject.Find( "Icon10_2" ).gameObject;
		icon_12_1 = GameObject.Find( "Icon12_1" ).gameObject;
        
        _timer = 0.0f;
        _scene_num = 0;
        _camera.transform.position = PosScene0;
    }
	
	// Update is called once per frame
	void Update ( ) {
        playMovie( );
        _timer += Time.deltaTime;
        
    }

    void playMovie( ) {
        switch ( _scene_num ) {
		case 0:
			icon_1_1.GetComponent<SpriteRenderer>( ).color = new Color ( 255, 255, 255, _timer * 0.25f );
			icon_1_3.GetComponent<SpriteRenderer>( ).color = new Color ( 255, 255, 255, 1 - _timer * 0.15f );
			icon_1_2.GetComponent<SpriteRenderer>( ).color = new Color ( 255, 255, 255, 1 - _timer * 0.15f );
			icon_1_2.transform.position += new Vector3 ( -0.25f, 0.25f, 0 );
                if ( _timer >= TimeScene0 ) {
                    cameraMove( PosScene0, PosScene1 );
                }
                break;
            case 1:
                if ( _timer >= TimeScene1 ) {
                    cameraMove( PosScene1, PosScene2 );
                }
                break;
            case 2:
                if ( _timer >= TimeScene2 ) {
                    cameraMove( PosScene2, PosScene3 );
                }
                break;
            case 3:
                if ( _timer >= TimeScene3 ) {
                    cameraMove( PosScene3, PosScene4 );
                }
                break;
		case 4:
			if (_timer <= 0.5f) {
				icon_5_1.transform.position = new Vector3 (-723f, -124 + _timer * 146f, 200f);
			}
			if (_timer >= 1.0f) {
				icon_5_2.GetComponent<SpriteRenderer>( ).color = new Color ( 255, 255, 255, 1f );
			}
                if ( _timer >= TimeScene4 ) {
                    cameraMove( PosScene4, PosScene5 );
                }
                break;
            case 5:
                if ( _timer >= TimeScene5 ) {
                    cameraMove( PosScene5, PosScene6 );
                }
                break;
            case 6:
                if ( _timer >= TimeScene6 ) {
                    cameraMove( PosScene6, PosScene7 );
                }
                break;
		case 7:
			icon_8_1.GetComponent<SpriteRenderer> ().color = new Color (255, 255, 255, 1f);
			if ( _timer > TimeScene7 - 0.2f ) {
				icon_8_2.GetComponent<SpriteRenderer> ().color = new Color (255, 255, 255, 1f);
			}
                if ( _timer >= TimeScene7 ) {
                    cameraMove( PosScene7, PosScene8 );
                }
                break;
		case 8:
			if ( _timer <= 1.0f ) {
				cut9.transform.position += new Vector3 ( 0, Mathf.Sin ( _timer * 8 * Mathf.PI ), 0 );
			}
			if ( _timer > TimeScene8 - 3.0f ) {
				icon_9_1.GetComponent<SpriteRenderer> ().color = new Color (255, 255, 255, 1f);
			}
			if ( _timer > TimeScene8 - 2.0f ) {
				icon_9_2.GetComponent<SpriteRenderer> ().color = new Color (255, 255, 255, 1f);
			}
            if ( _timer >= TimeScene8 ) {
                cameraMove( PosScene8, PosScene9 );
            }
            break;
		case 9:
			if (_timer <= TimeScene9 - 2.0f) {
				icon_10_1.transform.position = new Vector3 (281, -110 - _timer * 10f, 200f);
				icon_10_1.transform.Rotate (new Vector3 (_timer / 10f, 0f, 0f));
			}
			if (_timer > TimeScene9 - 0.1f) {
				icon_10_2.GetComponent<SpriteRenderer> ().color = new Color (255, 255, 255, 1f);
			}
                if ( _timer >= TimeScene9 ) {
                    cameraMove( PosScene9, PosScene10 );
                }
                break;
            case 10:
                if ( _timer >= TimeScene10 ) {
                    cameraMove( PosScene10, PosScene11 );
                }
                break;
		case 11:
			float x = Input.mousePosition.x;
			float y = Input.mousePosition.y;
			float z = 200.0f;
			icon_12_1.transform.position = new Vector3 ( Camera.main.ScreenToWorldPoint( new Vector3( x, y, z ) ).x, 
														icon_12_1.transform.position.y, 
														icon_12_1.transform.position.z );
                if ( _timer >= TimeScene11 ) {
                    cameraMove( PosScene11, PosScene12 );
                }
                break;
            case 12:
                if ( _timer >= TimeScene12 ) {
                    cameraMove( PosScene12, PosScene12 );
                }
                break;
            default:
                break;
        }
    }

    void cameraMove( Vector3 start_pos, Vector3 end_pos ) {
        Vector3 dir = Vector3.Normalize( end_pos - start_pos );
        _camera.transform.position += dir * CameraSpeed * Time.deltaTime;
        if ( Vector3.Distance( _camera.transform.position, end_pos ) <= CameraSpeed / 20 ) {
            _scene_num++;
            _timer = 0;
        }
    }

    public int getSelect( ) {
        return _select;
    }

    public void setSelect( int select ) {
        _select = select;
    }
}
