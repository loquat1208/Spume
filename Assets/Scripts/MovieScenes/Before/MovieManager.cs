using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;

public class MovieManager : MonoBehaviour {
    [SerializeField]
    private float CameraSpeed = 0;
    [SerializeField]
    private float TimeScene0 = 0;
    [SerializeField]
    private Vector3 PosScene0 = new Vector3( );
    [SerializeField]
    private float TimeScene1 = 0;
    [SerializeField]
    private Vector3 PosScene1 = new Vector3( );
    [SerializeField]
    private float TimeScene2 = 0;
    [SerializeField]
    private Vector3 PosScene2 = new Vector3( );
    [SerializeField]
    private float TimeScene3 = 0;
    [SerializeField]
    private Vector3 PosScene3 = new Vector3( );
    [SerializeField]
    private float TimeScene4 = 0;
    [SerializeField]
    private Vector3 PosScene4 = new Vector3( );
    [SerializeField]
    private float TimeScene5 = 0;
    [SerializeField]
    private Vector3 PosScene5 = new Vector3( );
    [SerializeField]
    private float TimeScene6 = 0;
    [SerializeField]
    private Vector3 PosScene6 = new Vector3( );
    [SerializeField]
    private float TimeScene7 = 0;
    [SerializeField]
    private Vector3 PosScene7 = new Vector3( );
    [SerializeField]
    private float TimeScene8 = 0;
    [SerializeField]
    private Vector3 PosScene8 = new Vector3( );
    [SerializeField]
    private float TimeScene9 = 0;
    [SerializeField]
    private Vector3 PosScene9 = new Vector3( );
    [SerializeField]
    private float TimeScene10 = 0;
    [SerializeField]
    private Vector3 PosScene10 = new Vector3( );
    [SerializeField]
    private float TimeScene11 = 0;
    [SerializeField]
    private Vector3 PosScene11 = new Vector3( );
    [SerializeField]
    private float TimeScene12 = 0;
    [SerializeField]
    private Vector3 PosScene12 = new Vector3( );
    [SerializeField]
    private float TimeScene13 = 0;
    [SerializeField]
    private Vector3 PosScene13 = new Vector3( );
    [SerializeField]
    private float TimeScene14 = 0;
    [SerializeField]
    private Vector3 PosScene14 = new Vector3( );
    [SerializeField]
    private Vector3 PosScene15 = new Vector3( );

    private AudioSource SE;
    private GameObject icon_1_1;
    private GameObject icon_1_2;
    private GameObject icon_1_3;
	private GameObject icon_5_0;
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
    private GameObject cut14_1;
    private GameObject cut14_2;
    private GameObject icon_14_1;
    private GameObject icon_14_2;
    private int _select;
    private float _timer;
    private int _scene_num;
    private GameObject _camera;
    private int _scene_select;

    // Use this for initialization
    void Start( ) {
        _scene_select = 0;
        _camera = GameObject.Find( "Main Camera" ).gameObject;
        _select = PlayerPrefs.GetInt( "Select" );
        icon_1_1 = GameObject.Find( "Icon1_1" ).gameObject;
        icon_1_2 = GameObject.Find( "Icon1_2" ).gameObject;
        icon_1_3 = GameObject.Find( "Icon1_3" ).gameObject;
		icon_5_0 = GameObject.Find( "Icon5_0" ).gameObject;
        icon_5_1 = GameObject.Find( "Icon5_1" ).gameObject;
        icon_5_2 = GameObject.Find( "Icon5_2" ).gameObject;
        icon_8_1 = GameObject.Find( "Icon8_1" ).gameObject;
        icon_8_2 = GameObject.Find( "Icon8_2" ).gameObject;
        cut9 = GameObject.Find( "Cut9" ).gameObject;
        icon_9_1 = GameObject.Find( "Icon9_1" ).gameObject;
        icon_9_2 = GameObject.Find( "Icon9_2" ).gameObject;
        icon_10_1 = GameObject.Find( "Icon10_1" ).gameObject;
        icon_10_2 = GameObject.Find( "Icon10_2" ).gameObject;
        icon_12_1 = GameObject.Find( "Icon12_1" ).gameObject;
        icon_14_1 = GameObject.Find( "Icon14_1" ).gameObject;
        icon_14_2 = GameObject.Find( "Icon14_2" ).gameObject;
        cut14_1 = GameObject.Find( "Cut14_1" ).gameObject;
        cut14_2 = GameObject.Find( "Cut14_2" ).gameObject;
        SE = GameObject.Find( "SE" ).gameObject.GetComponent<AudioSource>( );

        _timer = 0.0f;
        _scene_num = 0;
        _scene_select = 0;
        _camera.transform.position = PosScene0;
    }

    // Update is called once per frame
    void Update( ) {
        playMovie( );
        _timer += Time.deltaTime;
    }

    void OnApplicationQuit( ) {
        PlayerPrefs.SetInt( "Scene", SceneManager.GetActiveScene( ).buildIndex );
        PlayerPrefs.Save( );
    }

    void playMovie( ) {
        switch ( _scene_num ) {
            case 0:
                if ( !SE.isPlaying ) {
                    SE.PlayOneShot( ( AudioClip )Resources.Load( "Sounds/SE/MovieTitle", typeof( AudioClip ) ), 1f );
                }
                icon_1_1.GetComponent<SpriteRenderer>( ).color = new Color( 255, 255, 255, _timer * 0.25f );
                icon_1_3.GetComponent<SpriteRenderer>( ).color = new Color( 255, 255, 255, 1 - _timer * 0.15f );
                icon_1_2.GetComponent<SpriteRenderer>( ).color = new Color( 255, 255, 255, 1 - _timer * 0.15f );
                icon_1_2.transform.position += new Vector3( -0.25f, 0.25f, 0 );
                cameraMove( PosScene0, PosScene1, TimeScene0 );
                break;
            case 1:
                cameraMove( PosScene1, PosScene2, TimeScene1 );
                break;
            case 2:
                cameraMove( PosScene2, PosScene3, TimeScene2 );
                break;
            case 3:
                cameraMove( PosScene3, PosScene4, TimeScene3 );
                break;
			case 4:
				if ( _timer <= 0.5f ) {
					icon_5_0.transform.position = new Vector3( -700f - _timer * 146f, 50f, 200f );
					icon_5_0.GetComponent<SpriteRenderer>( ).color = new Color( 1, 1, 1, 1f - _timer * 2f );
				}
                if ( _timer <= 1.5f ) {
					icon_5_1.transform.position = new Vector3( -723f, -124 + ( _timer -  1f ) * 146f, 200f );
                }
                if ( _timer >= 2.0f ) {
                    icon_5_2.GetComponent<SpriteRenderer>( ).color = new Color( 255, 255, 255, 1f );
                }
                cameraMove( PosScene4, PosScene5, TimeScene4 );
                break;
            case 5:
                if ( !SE.isPlaying ) {
                    AudioClip clip = ( AudioClip )Resources.Load( "Sounds/SE/Walk", typeof( AudioClip ) );
                    SE.PlayOneShot( clip, 1f );
                }
                if ( _timer > 4f ) {
                    SE.Stop( );
                }
                cameraMove( PosScene5, PosScene6, TimeScene5 );
                break;
            case 6:
                if ( !SE.isPlaying ) {
                    AudioClip clip = ( AudioClip )Resources.Load( "Sounds/SE/Door", typeof( AudioClip ) );
                    SE.PlayOneShot( clip, 1f );
                }
                if ( _timer > 1f ) {
                    SE.Stop( );
                }
                cameraMove( PosScene6, PosScene7, TimeScene6 );
                break;
            case 7:
                icon_8_1.GetComponent<SpriteRenderer>( ).color = new Color( 255, 255, 255, 1f );
                if ( _timer > TimeScene7 - 0.2f ) {
                    icon_8_2.GetComponent<SpriteRenderer>( ).color = new Color( 255, 255, 255, 1f );
                }
                cameraMove( PosScene7, PosScene8, TimeScene7 );
                break;
            case 8:
                if ( !SE.isPlaying ) {
                    AudioClip clip = ( AudioClip )Resources.Load( "Sounds/SE/Hit2", typeof( AudioClip ) );
                    SE.PlayOneShot( clip, 1f );
                }
                if ( _timer > 1f ) {
                    SE.Stop( );
                }
                if ( _timer <= 1.0f ) {
                    cut9.transform.position += new Vector3( 0, Mathf.Sin( _timer * 8 * Mathf.PI ), 0 );
                }
                if ( _timer > TimeScene8 - 3.0f ) {
                    icon_9_1.GetComponent<SpriteRenderer>( ).color = new Color( 255, 255, 255, 1f );
                }
                if ( _timer > TimeScene8 - 2.0f ) {
                    icon_9_2.GetComponent<SpriteRenderer>( ).color = new Color( 255, 255, 255, 1f );
                }
                cameraMove( PosScene8, PosScene9, TimeScene8 );
                break;
            case 9:
                if ( _timer <= TimeScene9 - 2.0f ) {
                    icon_10_1.transform.position = new Vector3( 281, -110 - _timer * 10f, 200f );
                    icon_10_1.transform.Rotate( new Vector3( _timer / 10f, 0f, 0f ) );
                }
                if ( _timer > TimeScene9 - 0.1f ) {
                    icon_10_2.GetComponent<SpriteRenderer>( ).color = new Color( 255, 255, 255, 1f );
                }
                cameraMove( PosScene9, PosScene10, TimeScene9 );
                break;
            case 10:
                cameraMove( PosScene10, PosScene11, TimeScene10 );
                break;
            case 11:
                float x = Input.mousePosition.x - 450;
                if ( x < 0 ) {
                    x = 0;
                }
                if ( x > 100 ) {
                    x = 100;
                }
                icon_12_1.transform.position = new Vector3( 588 + x, -115, 200 );
                cameraMove( PosScene11, PosScene12, TimeScene11 );
                break;
            case 12:
                if ( !SE.isPlaying ) {
                    AudioClip clip = ( AudioClip )Resources.Load( "Sounds/SE/Shot", typeof( AudioClip ) );
                    SE.PlayOneShot( clip, 1f );
                }
                if ( _timer > 1f ) {
                    SE.Stop( );
                }
                cameraMove( PosScene12, PosScene13, TimeScene12 );
                if ( _scene_select == 0 ) {
                    cut14_1.SetActive( true );
                    cut14_2.SetActive( false );
                }
                if ( _scene_select == 1 ) {
                    cut14_1.SetActive( false );
                    cut14_2.SetActive( true );
                }
                break;
            case 13:
                if ( _scene_select == 0 ) {
                    if ( _timer <= 2.0f ) {
                        icon_14_1.GetComponent<SpriteRenderer>( ).color = new Color( 255, 255, 255, _timer / 2.0f );
                    }
                    icon_14_2.GetComponent<SpriteRenderer>( ).color = new Color( 255, 255, 255, 1 );
                    cameraMove( PosScene13, PosScene14, TimeScene13 );
                }
                if ( _scene_select == 1 ) {
                    cameraMove( PosScene13, PosScene14, TimeScene13 );
                }
                break;
            case 14:
                cameraMove( PosScene14, PosScene15, TimeScene14 );
                break;
            case 15:
                GameObject.Find( "Fade" ).gameObject.GetComponent<Image>( ).color = new Color( 0, 0, 0, _timer / 2f );
                if ( _timer > 3 ) {
                    SceneManager.LoadScene( "GameScene" );
                }
                break;
            default:
                break;
        }
    }

    void cameraMove( Vector3 start_pos, Vector3 end_pos, float end_time ) {
        _camera.transform.position = Vector3.Lerp( start_pos, end_pos, ( _timer - end_time ) * CameraSpeed );
        if ( ( _timer - end_time ) * CameraSpeed > 1 ) {
            _scene_num++;
            _timer = 0;
        }
    }

    public void nextScene( ) {
        _scene_num++;
        _timer = 0;
    }

    public void setScene( int scene ) {
        _scene_num = scene;
        _timer = 0;
    }

	public int getScene( ) {
		return _scene_num;
	}

    public void setSceneSelect( int select ) {
        _scene_select = select;
    }

    public int getSelect( ) {
        return _select;
    }

    public void setSelect( int select ) {
        _select = select;
    }
}
