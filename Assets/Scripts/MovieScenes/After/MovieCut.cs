using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
//using UnityEngine.UI;

public class MovieCut : MonoBehaviour {
    public List<AudioClip> Clip;
    public List<SpriteRenderer> Icon;

    private bool _play;
    private float _timer;
    private AudioSource _se;

    private void Awake( ) {
        _se = gameObject.GetComponent<AudioSource>( );
    }

    void Start( ) {
        _play = false;
    }

    void Update( ) {
        if ( !_play ) {
            _timer = 0;
            return;
        }
        _timer += Time.deltaTime;
        Cut( );
    } 

    void Cut(  ) {
        Cut1( transform.name );
        Cut2( transform.name );
    }

    void Cut2( string cut_name ) {
        if ( cut_name == "Cut2_0" ) {
            if ( !_se.isPlaying ) {
                _se.PlayOneShot( Clip[ 0 ], 0.5f );
            }
            if ( _timer > 1f ) {
                Icon[ 0 ].gameObject.GetComponent<SpriteRenderer>( ).color = new Color( 1, 1, 1, _timer / 4f - 1f );
            }
        }

        if ( cut_name == "Cut2_2" ) {
            if ( _timer > 2.9f ) {
                _se.Stop( );
            }
            if ( _timer > 1f && _timer < 2f ) {
                if ( !_se.isPlaying ) {
                    _se.PlayOneShot( Clip[ 0 ], 0.5f );
                }
                Vector3 cut2_1 = new Vector3( 0.1f * Mathf.Sin( _timer * 6f * Mathf.PI ), 0.1f * Mathf.Cos( _timer * 4f * Mathf.PI ), 0 );
                gameObject.transform.position += cut2_1;
            }
        }

        if ( cut_name == "Cut2_8" ) {
            if ( _timer > 1f ) {
                Icon[ 0 ].gameObject.GetComponent<SpriteRenderer>( ).color = new Color( 1, 1, 1, 1 );
            }
        }

        if ( cut_name == "Cut2_9" ) {
            if ( _timer > 1f ) {
                Icon[ 0 ].gameObject.GetComponent<SpriteRenderer>( ).color = new Color( 1, 1, 1, 1 );
            }
            if ( _timer > 3f ) {
                Icon[ 0 ].gameObject.GetComponent<SpriteRenderer>( ).color = new Color( 1, 1, 1, 0 );
                Icon[ 1 ].gameObject.GetComponent<SpriteRenderer>( ).color = new Color( 1, 1, 1, 1 );
            }
        }

        if ( cut_name == "Cut2_10" ) {
            if ( _timer > 1f ) {
                Icon[ 0 ].gameObject.GetComponent<SpriteRenderer>( ).color = new Color( 1, 1, 1, 1 );
            }
        }

        if ( cut_name == "Cut2_11" ) {
            if ( !_se.isPlaying ) {
                _se.PlayOneShot( Clip[ 0 ], 0.5f );
            }
        }

        if ( cut_name == "Cut2_18" ) {
            if ( !_se.isPlaying ) {
                _se.PlayOneShot( Clip[ 0 ], 0.5f );
            }
            if ( _timer < 1f ) {
                Vector3 cut2_18 = new Vector3( 0.01f * Mathf.Sin( _timer * 6f * Mathf.PI ), 0.01f * Mathf.Cos( _timer * 4f * Mathf.PI ), 0 );
                gameObject.transform.position += cut2_18;
            }
            if ( _timer > 1f ) {
                _se.Stop( );
                Icon[ 0 ].gameObject.GetComponent<SpriteRenderer>( ).color = new Color( 1, 1, 1, 1 );
            }
        }

        if ( cut_name == "Cut2_19" ) {
            if ( _timer > 1f ) {
                Icon[ 0 ].gameObject.GetComponent<SpriteRenderer>( ).color = new Color( 1, 1, 1, 1 );
            }
        }

        if ( cut_name == "Cut2_20" ) {
            //Select
            float x = Input.mousePosition.x - Screen.width / 2;
            Icon[ 0 ].gameObject.GetComponent<SpriteRenderer>( ).color = new Color( 1, 1, 1, 1 - x * 0.006f );
        }

        if ( cut_name == "Cut2_21" ) {
            if ( !_se.isPlaying ) {
                _se.PlayOneShot( Clip[ 0 ], 0.5f );
            }
            if ( _timer > 1f ) {
                _se.Stop( );
                Icon[ 0 ].gameObject.GetComponent<SpriteRenderer>( ).color = new Color( 1, 1, 1, 1 );
            }
        }

        if ( cut_name == "Cut2_22" ) {
            if ( !_se.isPlaying ) {
                _se.PlayOneShot( Clip[ 0 ], 0.5f );
            }
            if ( _timer > 1f ) {
                _se.Stop( );
                Icon[ 0 ].gameObject.GetComponent<SpriteRenderer>( ).color = new Color( 1, 1, 1, 1 );
            }
        }

        if ( cut_name == "Cut2_23" ) {
            if ( !_se.isPlaying ) {
                _se.PlayOneShot( Clip[ 0 ], 0.5f );
            }
            if ( _timer > 1f ) {
                Icon[ 0 ].gameObject.GetComponent<SpriteRenderer>( ).color = new Color( 1, 1, 1, 1 );
            }
        }

        if ( cut_name == "Cut2_25" ) {
            if ( !_se.isPlaying ) {
                _se.PlayOneShot( Clip[ 0 ], 1f );
            }
            if ( _timer > 6f ) {
                Icon[ 0 ].color = new Color( 0, 0, 0, _timer - 6f );
            }
            if ( _timer > 10f ) {
                SceneManager.LoadScene( "EndingScene" );
            }
        }
    }

    void Cut1( string cut_name ) {
        if ( cut_name == "Cut1" ) {
            if ( !_se.isPlaying ) {
                _se.PlayOneShot( Clip[ 0 ], 0.5f );
            }
            Icon[ 1 ].gameObject.transform.position += new Vector3( 0.75f, 1f, 0 ) * 0.004f;
            if ( _timer > 1f ) {
                Icon[ 0 ].gameObject.GetComponent<SpriteRenderer>( ).color = new Color( 1, 1, 1, _timer / 4f - 1f );
                Icon[ 1 ].gameObject.GetComponent<SpriteRenderer>( ).color = new Color( 1, 1, 1, 2f - _timer / 4f );
            }
        }

        if ( cut_name == "Cut2" ) {
            gameObject.GetComponent<SpriteRenderer>( ).color = new Color( 1, 1, 1, _timer / 2f );
            Color Icon2_0 = new Color( 1, 1, 1, Mathf.Sin( _timer / 6f * Mathf.PI ) );
            Color Icon2_1 = new Color( 1, 1, 1, _timer / 3f - 2f );
            Icon[ 0 ].gameObject.GetComponent<SpriteRenderer>( ).color = Icon2_0;
            Icon[ 1 ].gameObject.GetComponent<SpriteRenderer>( ).color = Icon2_1;
        }

        if ( cut_name == "Cut3" ) {
            if ( !_se.isPlaying ) {
                _se.PlayOneShot( Clip[ 0 ], 0.1f );
            }
            Vector3 Icon3_0 = new Vector3( 0.0002f * Mathf.Sin( _timer * Mathf.PI ), 0.001f * Mathf.Cos( _timer * Mathf.PI ), 0 );
            Icon[ 0 ].gameObject.transform.position += Icon3_0;
            Color Icon3_1 = new Color( 1, 1, 1, Mathf.Sin( _timer / 6f * Mathf.PI ) );
            Color Icon3_2 = new Color( 1, 1, 1, _timer / 3f - 2f );
            Icon[ 1 ].gameObject.GetComponent<SpriteRenderer>( ).color = Icon3_1;
            Icon[ 2 ].gameObject.GetComponent<SpriteRenderer>( ).color = Icon3_2;
        }

        if ( cut_name == "Cut5" ) {
            if ( !_se.isPlaying ) {
                _se.PlayOneShot( Clip[ 0 ], 1f );
            }
            if ( _timer > 2f ) {
                Color Icon5_0 = new Color( 1, 1, 1, Mathf.Sin( 0.5f / _timer * Mathf.PI ) );
                Icon[ 0 ].gameObject.GetComponent<SpriteRenderer>( ).color = Icon5_0;
            }
        }

        if ( cut_name == "Cut6" ) {
            if ( !_se.isPlaying ) {
                _se.PlayOneShot( Clip[ 0 ], 1f );
            }
            if ( _timer > 2f ) {
                Color Icon6_0 = new Color( 1, 1, 1, Mathf.Sin( 0.5f / _timer * Mathf.PI ) );
                Icon[ 0 ].gameObject.GetComponent<SpriteRenderer>( ).color = Icon6_0;
            }
        }

        if ( cut_name == "Cut7" ) {
            if ( !_se.isPlaying ) {
                _se.PlayOneShot( Clip[ 0 ], 1f );
            }
            gameObject.GetComponent<SpriteRenderer>( ).color = new Color( 1, 1, 1, _timer );
            if ( _timer > 2f ) {
                Color Icon7_0 = new Color( 1, 1, 1, Mathf.Sin( 0.5f / _timer * Mathf.PI ) );
                Icon[ 0 ].gameObject.GetComponent<SpriteRenderer>( ).color = Icon7_0;
            }
        }

        if ( cut_name == "Cut8" ) {
            if ( !_se.isPlaying ) {
                _se.PlayOneShot( Clip[ 0 ], 1f );
            }
            gameObject.GetComponent<SpriteRenderer>( ).color = new Color( 1, 1, 1, _timer );
            if ( _timer > 2f ) {
                Color Icon8_0 = new Color( 1, 1, 1, Mathf.Sin( _timer / 2f * Mathf.PI ) );
                Icon[ 0 ].gameObject.GetComponent<SpriteRenderer>( ).color = Icon8_0;
            }
        }

        if ( cut_name == "Cut9" ) {
            if ( !_se.isPlaying ) {
                _se.PlayOneShot( Clip[ 0 ], 0.5f );
            }
            if ( _timer < 1f ) {
                Vector3 cut9 = new Vector3( 0.004f * Mathf.Sin( _timer * 6f * Mathf.PI ), 0.002f * Mathf.Cos( _timer * 4f * Mathf.PI ), 0 );
                gameObject.transform.position += cut9;
            }
            if ( _timer > 1.6f ) {
                _se.Stop( );
            }
        }

        if ( cut_name == "Cut10" ) {
            gameObject.GetComponent<SpriteRenderer>( ).color = new Color( 1 - _timer, 1 - _timer, 1 - _timer, 1 );
        }

        if ( cut_name == "Cut11" ) {
            float time1 = 2f;
            float time2 = 2.5f;
            float time3 = 3.5f;
            float time4 = 7f;
            if ( _timer < time1 ) {
                float color = _timer / 2f;
                gameObject.GetComponent<SpriteRenderer>( ).color = new Color( color, color, color, 1 );
            }
            if ( _timer > time1 && _timer < time2 ) {
                float color = 1 - 2 * ( _timer - time1 );
                gameObject.GetComponent<SpriteRenderer>( ).color = new Color( color, color, color, 1 );
            }
            if ( _timer > time2 && _timer < time3 ) {
                float color = _timer - time2;
                gameObject.GetComponent<SpriteRenderer>( ).color = new Color( color, color, color, 1 );
            }
            if ( _timer > time3 && _timer < time4 ) {
                float color = _timer - time3;
                Icon[ 0 ].color = new Color( 1, 1, 1, color );
            }
            if ( _timer > time4 ) {
                float color = _timer - time4;
                Icon[ 0 ].color = new Color( 1, 1, 1, 1f - color );
                Icon[ 1 ].color = new Color( 1, 1, 1, color - 1f );
            }
        }

        if ( cut_name == "Cut13" ) {
            if ( _timer < 1f ) {
                gameObject.GetComponent<SpriteRenderer>( ).color = new Color( _timer, _timer, _timer, 1 );
            }
        }

        if ( cut_name == "Cut14" ) {
            if ( !_se.isPlaying ) {
                _se.PlayOneShot( Clip[ 0 ], 0.5f );
            }
            if ( _timer < 1f ) {
                Vector3 cut14 = new Vector3( 0.004f * Mathf.Sin( _timer * 6f * Mathf.PI ), 0.002f * Mathf.Cos( _timer * 4f * Mathf.PI ), 0 );
                gameObject.transform.position += cut14;
            }
            if ( _timer > 1.6f ) {
                _se.Stop( );
            }
        }

        if ( cut_name == "Cut16" ) {
            float time1 = 4f;
            if ( _timer < time1 ) {
                Icon[ 0 ].color = new Color( 1, 1, 1, _timer );
            }
            if ( _timer > time1 ) {
                float color = _timer - time1;
                Icon[ 0 ].color = new Color( 1, 1, 1, 1f - color );
                Icon[ 1 ].color = new Color( 1, 1, 1, color - 1f );
            }
        }

        if ( cut_name == "Cut17" ) {
            gameObject.GetComponent<SpriteRenderer>( ).color = new Color( 1, 1, 1, 1 );
            if ( _timer > 0.2f ) {
                Icon[ 0 ].color = new Color( 1, 1, 1, 1 );
                Icon[ 1 ].color = new Color( 1, 1, 1, 1 );
            }
            if ( _timer < 0.5f ) {
                Icon[ 1 ].transform.position += new Vector3( 0.04f, 0.004f, 0f );
            }
        }

        if ( cut_name == "Cut18" ) {
            if ( !_se.isPlaying ) {
                _se.PlayOneShot( Clip[ 0 ], 0.5f );
            }
            if ( _timer > 0.2f ) {
                Icon[ 0 ].color = new Color( 1, 1, 1, 1 );
            }
            if ( _timer < 1f ) {
                Vector3 cut14 = new Vector3( 0.004f * Mathf.Sin( _timer * 6f * Mathf.PI ), 0.002f * Mathf.Cos( _timer * 4f * Mathf.PI ), 0 );
                gameObject.transform.position += cut14;
            }
            if ( _timer > 1.6f ) {
                _se.Stop( );
            }
        }

        if ( cut_name == "Cut19" ) {
            if ( !_se.isPlaying ) {
                _se.PlayOneShot( Clip[ 0 ], 0.5f );
            }
            if ( _timer < 1f ) {
                Vector3 cut14 = new Vector3( 0.004f * Mathf.Sin( _timer * 6f * Mathf.PI ), 0.002f * Mathf.Cos( _timer * 4f * Mathf.PI ), 0 );
                gameObject.transform.position += cut14;
            }
            if ( _timer > 1.5f ) {
                _se.Stop( );
            }
        }

        if ( cut_name == "Cut21" ) {
            if ( !_se.isPlaying ) {
                _se.PlayOneShot( Clip[ 0 ], 0.5f );
            }
            if ( _timer > 1.1f ) {
                _se.Stop( );
            }
            Icon[ 0 ].color = new Color( 1, 1, 1, 1 );
            if ( _timer > 0.2f && _timer < 0.3f ) {
                Icon[ 1 ].color = new Color( 1, 1, 1, 1 );
                float scale = ( _timer - 0.2f ) * 10f;
                Icon[ 2 ].transform.localScale = new Vector3( scale, scale, scale );
                Icon[ 2 ].transform.position += new Vector3( 0.6f, 0, 0 );
            }
        }

        if ( cut_name == "Cut23" ) {
            gameObject.GetComponent<SpriteRenderer>( ).color = new Color( 1, 1, 1, 1 );
            if ( _timer > 0.1f ) {
                Icon[ 0 ].color = new Color( 1, 1, 1, 1 );
            }
        }

        if ( cut_name == "Cut26" ) {
            if ( !_se.isPlaying ) {
                _se.PlayOneShot( Clip[ 0 ], 0.5f );
            }
            if ( _timer > 1.1f ) {
                _se.Stop( );
            }
            if ( _timer < 1f ) {
                Vector3 cut26 = new Vector3( 0.004f * Mathf.Sin( _timer * 6f * Mathf.PI ), 0.002f * Mathf.Cos( _timer * 4f * Mathf.PI ), 0 );
                gameObject.transform.position += cut26;
            }
        }

        if ( cut_name == "Cut27" ) {
            if ( !_se.isPlaying ) {
                _se.PlayOneShot( Clip[ 0 ], 1f );
                _se.time = 5f;
            }
            if ( _timer >= 2f ) {
                float color = 3f - _timer * 0.5f;
                gameObject.GetComponent<SpriteRenderer>( ).color = new Color( color, color, color, 1 );
            }
        }

        if ( cut_name == "Cut29" ) {
            if ( !_se.isPlaying ) {
                _se.PlayOneShot( Clip[ 0 ], 1f );
            }
            float x = Input.mousePosition.x - Screen.width / 2;
            Icon[ 0 ].transform.position = new Vector3( Icon[ 0 ].transform.position.x, -x * 0.005f + 6.5f, 0 );
        }

        if ( cut_name == "Cut30" ) {
            if ( !_se.isPlaying ) {
                _se.PlayOneShot( Clip[ 0 ], 1f );
            }
            if ( _timer > 1.6f ) {
                _se.Stop( );
            }
            if ( _timer < 1f ) {
                Icon[ 0 ].color = new Color( 1, 1, 1, 1 );
                Vector3 cut30 = new Vector3( 0.004f * Mathf.Sin( _timer * 6f * Mathf.PI ), 0.002f * Mathf.Cos( _timer * 4f * Mathf.PI ), 0 );
                gameObject.transform.position += cut30;
            }
            if ( _timer > 1f ) {
                Icon[ 0 ].color = new Color( 1, 1, 1, 0 );
                Icon[ 1 ].color = new Color( 1, 1, 1, 1 );
            }
            if ( _timer > 2f ) {
                Icon[ 2 ].color = new Color( 1, 1, 1, 1 );
            }
        }

        if ( cut_name == "Cut31" ) {
            float time1 = 4f;
            if ( _timer < time1 ) {
                Icon[ 0 ].color = new Color( 1, 1, 1, _timer );
            }
            if ( _timer > time1 ) {
                float color = _timer - time1;
                Icon[ 0 ].color = new Color( 1, 1, 1, 1f - color );
                Icon[ 1 ].color = new Color( 1, 1, 1, color - 1f );
            }
        }

        if ( cut_name == "Cut32" ) {
            if ( !_se.isPlaying ) {
                _se.PlayOneShot( Clip[ 0 ], 1f );
            }
            if ( _timer > 1.6f ) {
                _se.Stop( );
            }
        }

        if ( cut_name == "Cut33" ) {
            if ( !_se.isPlaying ) {
                _se.PlayOneShot( Clip[ 0 ], 1f );
            }
            if ( _timer > 1.1f ) {
                _se.Stop( );
            }
        }

        if ( cut_name == "Cut34" ) {
            if ( !_se.isPlaying ) {
                _se.PlayOneShot( Clip[ 0 ], 1f );
            }
            if ( _timer > 6f ) {
                Icon[ 0 ].color = new Color( 0, 0, 0, _timer - 6f );
            }
            if ( _timer > 10f ) {
                SceneManager.LoadScene( "GameScene" );
            }
        }
    }

    public bool isPlay( ) {
        return _play;
    }

    public void play( ) {
        _play = true;
    }

    public void stop( ) {
        _play = false;
        if ( _se.isPlaying ) {
            _se.Stop( );
        }
    }
}
