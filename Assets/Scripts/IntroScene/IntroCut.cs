using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class IntroCut : MonoBehaviour {

    public List<GameObject> Icon;
    public AudioClip Sound;
    private IntroCutManager _cut_manager;
    private IntroManager _intro_manager;
    private float _timer;
    private float _timer_speed;
    private bool _play;
    private AudioSource _se;

    private void Start( ) {
        _timer_speed = 1f;
        _cut_manager = GameObject.Find( "CutManager" ).GetComponent<IntroCutManager>( );
        _intro_manager = GameObject.Find( "IntroSystem" ).GetComponent<IntroManager>( );
        _se = gameObject.GetComponent<AudioSource>( );
        _play = true;
    }

    private void Update( ) {
        if ( !_play ) {
            _timer = 0;
            return;
        }
        if ( gameObject.name != "Cut7" &&
             gameObject.name != "Cut8" &&
             gameObject.name != "Cut9" &&
             gameObject.name != "Cut10" &&
             gameObject.name != "Cut11" &&
             gameObject.name != "Cut12" ) {
            _timer_speed = _cut_manager.getSpeed( );
        } 
        _timer += _timer_speed * Time.deltaTime;
        Cut( );
    }

    void Cut( ) {
		//キャラの選択時に出る文章
		int select_num = PlayerPrefs.GetInt( "Chara1Alive" ) +
		                 PlayerPrefs.GetInt( "Chara2Alive" ) +
		                 PlayerPrefs.GetInt( "Chara3Alive" ) +
		                 PlayerPrefs.GetInt( "Chara4Alive" ) +
		                 PlayerPrefs.GetInt( "Chara5Alive" ) +
		                 PlayerPrefs.GetInt( "Chara6Alive" );
		string select = "一緒に生存する人物を選んでください\n現在：" + select_num + " / 4";
		
		//各CutのAnimation
        if ( gameObject.name == "Cut0" ) {
            Icon[ 1 ].transform.position += new Vector3( 1f, 0, 0 );
            Icon[ 2 ].transform.position += new Vector3( -1f, 0, 0 );
            Icon[ 3 ].transform.position += new Vector3( -1f, 0, 0 );
            float tree_time = 2.5f;
            if ( _timer > tree_time ) {
				Icon[ 0 ].GetComponentInChildren<Text>( ).text = "環境破壊・生態系の破壊・戦争";
                Icon[ 4 ].GetComponent<SpriteRenderer>( ).color = new Color( 1, 1, 1, 1 - ( _timer - tree_time ) );
            }
            if ( _timer > tree_time * 2 ) {
                Icon[ 0 ].GetComponentInChildren<Text>( ).text = "";
                Icon[ 5 ].GetComponent<SpriteRenderer>( ).color = new Color( 1, 1, 1, 1 - ( _timer - tree_time * 2 ) );
            }
            if ( _timer > tree_time * 3 ) {
				Icon[ 0 ].GetComponentInChildren<Text>( ).text = "人類は自然を破壊し続け、結果自然は我々人類を放棄した。";
                Icon[ 6 ].GetComponent<SpriteRenderer>( ).color = new Color( 1, 1, 1, 1 - ( _timer - tree_time * 3 ) );
            }
            if ( _timer > tree_time * 4 ) {
                Icon[ 7 ].GetComponent<SpriteRenderer>( ).color = new Color( 1, 1, 1, 1 - ( _timer - tree_time * 4 ) );
            }
            if ( _timer > tree_time * 5 ) {
                Icon[ 0 ].GetComponentInChildren<Text>( ).text = "";
                Icon[ 8 ].GetComponent<SpriteRenderer>( ).color = new Color( 1, 1, 1, 1 - ( _timer - tree_time * 5 ) );
            }
            if ( _timer > tree_time * 6 ) {
                stop( );
                _cut_manager.NextCut( );
            }
        }
        if ( gameObject.name == "Cut1" ) {
            Icon[ 1 ].transform.position += new Vector3( 1f, 0, 0 );
            Icon[ 2 ].transform.position += new Vector3( -1f, 0, 0 );
            Icon[ 3 ].transform.position += new Vector3( -1f, 0, 0 );
            float tree_time = 2.5f;
            if ( _timer > tree_time ) {
				if ( !_se.isPlaying ) {
					_se.PlayOneShot( Sound, 0.5f );
				}
				Icon[ 0 ].GetComponentInChildren<Text>( ).text = "我々が自然に対してしたように、自然も我々に容赦がなかった。";
                Icon[ 4 ].GetComponent<SpriteRenderer>( ).color = new Color( 1, 1, 1, 1 - ( _timer - tree_time ) );
            }
            if ( _timer > tree_time * 2 ) {
                Icon[ 6 ].transform.position += new Vector3( 0.6f, -1f, 0 );
            }
            if ( _timer > tree_time * 3 ) {
                Icon[ 7 ].transform.position += new Vector3( -0.6f, -1f, 0 );
                Icon[ 0 ].GetComponentInChildren<Text>( ).text = "";
            }
            if ( _timer > tree_time * 4 ) {
                Icon[ 5 ].transform.position += new Vector3( 0, -1f, 0 );
                if ( Icon[ 9 ].transform.position.y < 0 ) {
                    Icon[ 9 ].transform.position += new Vector3( 0, 2f, 1 );
                }
            }
            if ( _timer > tree_time * 6 ) {
				_se.Stop( );
                Icon[ 5 ].SetActive( false );
                Icon[ 6 ].SetActive( false );
                Icon[ 7 ].SetActive( false );
            }
            if ( _timer > tree_time * 7 ) {
                stop( );
                _cut_manager.NextCut( );
            }
        }
        if ( gameObject.name == "Cut2" ) {
            Icon[ 1 ].transform.position += new Vector3( 0.5f, 0, 0 );
            Icon[ 2 ].transform.position += new Vector3( -0.5f, 0, 0 );
            Icon[ 3 ].transform.position += new Vector3( -0.5f, 0, 0 );
            float tree_time = 2.5f;
            if ( _timer > tree_time ) {
				Icon[ 0 ].GetComponentInChildren<Text>( ).text = "空腹・喉の渇き・疫病・略奪・自殺・殺人";
                Icon[ 4 ].GetComponent<SpriteRenderer>( ).color = new Color( 1, 1, 1, _timer - tree_time );
            }
            if ( _timer > tree_time * 2 ) {
                Icon[ 4 ].SetActive( false );
                Icon[ 5 ].GetComponent<SpriteRenderer>( ).color = new Color( 1, 1, 1, _timer - tree_time * 2 );
            }
            if ( _timer > tree_time * 3 ) {
                Icon[ 6 ].GetComponent<SpriteRenderer>( ).color = new Color( 1, 1, 1, _timer - tree_time * 3 );
				Icon[ 0 ].GetComponentInChildren<Text>( ).text = "希望は海の泡のようにどんどん弾け続け";
            }
            if ( _timer > tree_time * 4 ) {
                Icon[ 7 ].GetComponent<SpriteRenderer>( ).color = new Color( 1, 1, 1, _timer - tree_time * 4 );
                if ( Icon[ 10 ].transform.position.y < 200f ) {
                    Icon[ 10 ].transform.position += new Vector3( 0, 2f, 0 );
                }
            }
            if ( _timer > tree_time * 5 ) {
				Icon[ 0 ].GetComponentInChildren<Text>( ).text = "しかし、自然は我々をあざ笑うように格段に美しく輝いていた。";
                Icon[ 8 ].GetComponent<SpriteRenderer>( ).color = new Color( 1, 1, 1, _timer - tree_time * 5 );
            }
            if ( _timer > tree_time * 7 ) {
                Icon[ 0 ].GetComponentInChildren<Text>( ).text = "";
                Icon[ 11 ].GetComponentInChildren<Image>( ).color = new Color( 1, 1, 1, _timer - tree_time * 6 );
                Icon[ 12 ].GetComponent<SpriteRenderer>( ).color = new Color( 1, 1, 1, _timer - tree_time * 6 );
            }
            if ( _timer > tree_time * 8 ) {
                Icon[ 12 ].GetComponent<SpriteRenderer>( ).color = new Color( 1, 1, 1, 1 - ( _timer - tree_time * 7 ) );
                Icon[ 13 ].GetComponent<SpriteRenderer>( ).color = new Color( 1, 1, 1, _timer - tree_time * 7 );
            }
            if ( _timer > tree_time * 9 ) {
                Icon[ 13 ].SetActive( false );
                //float time = 1 - ( _timer - tree_time * 8 );
                //Icon[ 11 ].GetComponentInChildren<Image>( ).color = new Color( time, time, time, 1 );
                Icon[ 11 ].GetComponentInChildren<Image>( ).color = new Color( 0, 0, 0, 1 );
            }
            if ( _timer > tree_time * 10 ) {
				if ( !_se.isPlaying ) {
					_se.PlayOneShot( Sound, 0.5f );
				}
                Icon[ 14 ].GetComponent<SpriteRenderer>( ).color = new Color( 1, 1, 1, 1 );
            }
            if ( _timer > tree_time * 12 ) {
                stop( );
                _cut_manager.NextCut( );
            }
        }
        if ( gameObject.name == "Cut3" ) {
            if ( !_se.isPlaying ) {
                _se.PlayOneShot( Sound, 0.5f );
            }
            if ( _timer > 1f ) {
                if ( Icon[ 2 ].transform.position.x > 20f ) {
                    Icon[ 2 ].transform.position += new Vector3( -2f, 0, 0 );
                }
            }
            if ( _timer > 9f ) {
                stop( );
                _cut_manager.NextCut( );
            }
        }
        if ( gameObject.name == "Cut4" ) {
            float e_t_1 = 2f;
            float e_t_2 = 5f;
            float e_t_3 = 9f;
            float e_t_4 = 11f;
            float end = 15f;

            if ( _timer > e_t_1 && _timer < e_t_2 ) {
                if ( Icon[ 1 ].transform.position.y > -300f ) {
                    Icon[ 1 ].transform.position += new Vector3( 0, -1.5f, 0 );
                }
            }
            if ( _timer > e_t_2 ) {
                if ( Icon[ 1 ].transform.position.y < 300f ) {
                    Icon[ 1 ].transform.position += new Vector3( 0, 1.5f, 0 );
                }
            }
            if ( _timer > e_t_3 ) {
				Icon[ 0 ].GetComponentInChildren<Text>( ).text = "";
                Icon[ 3 ].GetComponent<SpriteRenderer>( ).color = new Color( 1, 1, 1, 1 );
            }
            if ( _timer > e_t_4 ) {
                Icon[ 3 ].SetActive( false );
                Icon[ 1 ].GetComponent<SpriteRenderer>( ).color = new Color( 1, 1, 1, 1 - ( _timer - e_t_4 ) );
                Icon[ 2 ].GetComponent<SpriteRenderer>( ).color = new Color( 1, 1, 1, _timer - e_t_4 );
                Icon[ 4 ].GetComponent<SpriteRenderer>( ).color = new Color( 1, 1, 1, _timer - e_t_4 );
                if ( Icon[ 4 ].transform.position.y < -200f ) {
                    Icon[ 4 ].transform.position += new Vector3( -2f, 2f, 0 );
                }
            }
            if ( _timer > end ) {
                stop( );
                _cut_manager.NextCut( );
            }
        }
        if ( gameObject.name == "Cut5" ) {
            float e_t_1 = 2f;
            float e_t_2 = 3f;
            float e_t_3 = 4f;
            float e_t_4 = 5.5f;
            float e_t_5 = 7f;
            float e_t_6 = 9f;
            float end = 11f;
            if ( _timer > e_t_1 ) {
                Icon[ 0 ].SetActive( true );
                Icon[ 1 ].SetActive( true );
				if ( !_se.isPlaying ) {
					_se.PlayOneShot( Sound, 0.5f );
				}
            }
            if ( _timer > e_t_2 ) {
				Icon[ 7 ].SetActive( false );
                Icon[ 2 ].SetActive( true );
                Icon[ 0 ].GetComponent<SpriteRenderer>( ).color = new Color( 1, 1, 1, 1 - ( _timer - e_t_2 ) );
                Icon[ 1 ].GetComponent<SpriteRenderer>( ).color = new Color( 1, 1, 1, 1 - ( _timer - e_t_2 ) );
            }
            if ( _timer > e_t_3 ) {
                Icon[ 3 ].GetComponent<SpriteRenderer>( ).color = new Color( 1, 1, 1, _timer - e_t_3 );
            }
            if ( _timer > e_t_4 ) {
                Icon[ 3 ].GetComponent<SpriteRenderer>( ).color = new Color( 1, 1, 1, 1 - ( _timer - e_t_4 ) );
                Icon[ 4 ].GetComponent<SpriteRenderer>( ).color = new Color( 1, 1, 1, _timer - e_t_4 );
            }
            if ( _timer > e_t_5 ) {
                Icon[ 4 ].GetComponent<SpriteRenderer>( ).color = new Color( 1, 1, 1, 1 - ( _timer - e_t_5 ) );
                Icon[ 5 ].GetComponent<SpriteRenderer>( ).color = new Color( 1, 1, 1, _timer - e_t_5 );
            }
            if ( _timer > e_t_6 ) {
				_se.Stop( );
				Icon[ 6 ].GetComponent<SpriteRenderer>( ).color = new Color( 1, 1, 1, _timer - e_t_6 );
                Icon[ 8 ].GetComponent<Text>( ).color = new Color( 1, 1, 1, _timer - e_t_6 );
            }
            if ( _timer > end ) {
                stop( );
                _cut_manager.NextCut( );
            }
        }
        if ( gameObject.name == "Cut6" ) {
            float e_t_1 = 2f;
            float e_t_2 = 3f;
            float e_t_3 = 6f;
            float e_t_4 = 7f;
            float e_t_5 = 9f;
            float end = 12f;
            if ( _timer > e_t_1 ) {
				if ( !_se.isPlaying ) {
					_se.PlayOneShot( Sound, 0.3f );
				}
                Icon[ 0 ].SetActive( true );
                Icon[ 0 ].GetComponent<SpriteRenderer>( ).color = new Color( 1, 1, 1, _timer - e_t_1 );
            }
            if ( _timer > e_t_2 ) {
                Icon[ 1 ].SetActive( true );
            }
            if ( _timer > e_t_3 ) {
                Icon[ 2 ].SetActive( true );
            }
            if ( _timer > e_t_4 ) {
				_se.Stop( );
                Icon[ 3 ].SetActive( true );
            }
            if ( _timer > e_t_5 ) {
                Icon[ 4 ].SetActive( true );
                if ( Icon[ 4 ].transform.position.y > 0 ) {
                    Icon[ 4 ].transform.position -= new Vector3( 0, 8f, 0 );
                }
            }
            if ( _timer > end ) {
                stop( );
                _cut_manager.NextCut( );
            }
        }
        if ( gameObject.name == "Cut7" ) {
            float e_t_1 = 1f;
            float e_t_2 = 2f;
            float e_t_3 = 4f;
            float e_t_4 = 6f;
            float e_t_5 = 7f;
            float e_t_6 = 8f;
            float e_t_7 = 10f;
            float e_t_8 = 11f;
            float end = 12f;
			if ( !_se.isPlaying ) {
				_se.Play( );
			}
            if ( _timer > e_t_1 ) {
                Icon[ 0 ].GetComponent<SpriteRenderer>( ).color = new Color( 1, 1, 1, 1 - ( _timer - e_t_1 ) );
                Icon[ 1 ].GetComponent<SpriteRenderer>( ).color = new Color( 1, 1, 1, _timer - e_t_1 );
            }
            if ( _timer > e_t_2 ) {
                Icon[ 1 ].GetComponent<SpriteRenderer>( ).color = new Color( 1, 1, 1, 1 - ( _timer - e_t_2 ) );
                Icon[ 2 ].GetComponent<SpriteRenderer>( ).color = new Color( 1, 1, 1, _timer - e_t_2 );
            }
            if ( _timer > e_t_3 ) {
				_se.Stop( );
                Icon[ 3 ].SetActive( true );
                Icon[ 3 ].GetComponent<SpriteRenderer>( ).color = new Color( 1, 1, 1, _timer - e_t_3 );
            }
            if ( _timer > e_t_4 ) {
                Icon[ 4 ].SetActive( true );
                Icon[ 5 ].SetActive( true );
                //Icon[ 4 ].GetComponent<SpriteRenderer>( ).color = new Color( 1, 1, 1, _timer - e_t_4 );
            }
            if ( _timer > e_t_5 ) {
                Icon[ 6 ].SetActive( true );
            }
            if ( _timer > e_t_6 ) {
				Icon[ 5 ].GetComponent<AudioSource>( ).Stop( );
                _timer_speed = 0;
				Icon[ 7 ].SetActive( true );
				Icon[ 7 ].GetComponentInChildren<Text>( ).text = select;
            }
            if ( _timer > e_t_6 + 1f ) {
                _timer_speed = 1;
                Icon[ 7 ].SetActive( false );
                Icon[ 3 ].SetActive( false );
                Icon[ 4 ].SetActive( false );
                Icon[ 5 ].SetActive( false );
                Icon[ 6 ].SetActive( false );
                if ( PlayerPrefs.GetInt( "Chara1Alive" ) == 1 ) {
                    Icon[ 8 ].SetActive( true );
                }
            }
            if ( _timer > e_t_7 ) {
                Icon[ 2 ].GetComponent<SpriteRenderer>( ).color = new Color( 1, 1, 1, 1 - ( _timer - e_t_7 ) );
                Icon[ 1 ].GetComponent<SpriteRenderer>( ).color = new Color( 1, 1, 1, _timer - e_t_7 );
            }
            if ( _timer > e_t_8 ) {
                Icon[ 1 ].GetComponent<SpriteRenderer>( ).color = new Color( 1, 1, 1, 1 - ( _timer - e_t_8 ) );
                Icon[ 0 ].GetComponent<SpriteRenderer>( ).color = new Color( 1, 1, 1, _timer - e_t_8 );
            }
            if ( _timer > end ) {
                stop( );
                _cut_manager.NextCut( );
            }
        }
        if ( gameObject.name == "Cut8" ) {
            float e_t_1 = 2f;

            if ( _timer > e_t_1 ) {
                _timer_speed = 0;
                Icon[ 0 ].SetActive( true );
				Icon[ 0 ].GetComponentInChildren<Text>( ).text = select;
            }
            if ( _timer > e_t_1 + 1f ) {
                _timer_speed = 1;
                Icon[ 0 ].SetActive( false );
            }
            float end = 5f;
            if ( _timer > end ) {
                stop( );
                _cut_manager.NextCut( );
            }
        }
        if ( gameObject.name == "Cut9" ) {
            float e_t_1 = 1f;
            float e_t_2 = 2f;
            float e_t_3 = 3f;
            float e_t_4 = 4f;
            if ( PlayerPrefs.GetInt( "Chara1Alive" ) == 1 ) {
                Icon[ 3 ].SetActive( true );
            }
            if ( PlayerPrefs.GetInt( "Chara2Alive" ) == 1 ) {
                Icon[ 4 ].SetActive( true );
            }
            if ( _timer > e_t_1 ) {
                Icon[ 0 ].GetComponent<SpriteRenderer>( ).color = new Color( 1, 1, 1, 1 - ( _timer - e_t_1 ) );
                Icon[ 1 ].GetComponent<SpriteRenderer>( ).color = new Color( 1, 1, 1, _timer - e_t_1 );
            }
            if ( _timer > e_t_2 ) {
                Icon[ 1 ].GetComponent<SpriteRenderer>( ).color = new Color( 1, 1, 1, 1 - ( _timer - e_t_2 ) );
                Icon[ 2 ].GetComponent<SpriteRenderer>( ).color = new Color( 1, 1, 1, _timer - e_t_2 );
            }
            if ( _timer > e_t_3 ) {
                Icon[ 5 ].SetActive( true );
            }
            if ( _timer > e_t_4 ) {
                _timer_speed = 0;
                Icon[ 6 ].SetActive( true );
				Icon[ 6 ].GetComponentInChildren<Text>( ).text = select;
            }
            if ( _timer > e_t_4 + 1f ) {
                _timer_speed = 1;
                Icon[ 6 ].SetActive( false );
            }
            float end = 7f;
            if ( _timer > end ) {
                stop( );
                _cut_manager.NextCut( );
            }
        }
        if ( gameObject.name == "Cut10" ) {
            float e_t_1 = 2f;

            if ( PlayerPrefs.GetInt( "Chara3Alive" ) == 1 ) {
                Icon[ 0 ].SetActive( true );
            }
            if ( _timer > e_t_1 ) {
                _timer_speed = 0;
                Icon[ 1 ].SetActive( true );
				Icon[ 1 ].GetComponentInChildren<Text>( ).text = select;
            }
            if ( _timer > e_t_1 + 1f ) {
                _timer_speed = 1;
                Icon[ 1 ].SetActive( false );
            }
            float end = 5f;
            if ( _timer > end ) {
                stop( );
                _cut_manager.NextCut( );
            }
        }
        if ( gameObject.name == "Cut11" ) {
            float e_t_1 = 2f;
            float e_t_2 = 3f;
            float e_t_3 = 4f;
            float e_t_4 = 5f;
            float e_t_5 = 6f;

            if ( PlayerPrefs.GetInt( "Chara4Alive" ) == 1 ) {
                Icon[ 1 ].SetActive( false );
            } else {
                Icon[ 0 ].SetActive( false );
                Icon[ 1 ].SetActive( true );
            }

            if ( _timer > e_t_1 ) {
                Icon[ 4 ].SetActive( true );
            }
            if ( _timer > e_t_2 ) {
                if ( PlayerPrefs.GetInt( "Chara4Alive" ) == 1 ) {
                    Icon[ 2 ].SetActive( true );
                } else {
                    Icon[ 3 ].SetActive( true );
                }
            }
            if ( _timer > e_t_3 ) {
                Icon[ 5 ].SetActive( true );
            }
            if ( _timer > e_t_4 ) {
				if ( !_se.isPlaying ) {
					_se.PlayOneShot( Sound, 0.5f );
				}
                Icon[ 6 ].SetActive( true );
            }
            if ( _timer > e_t_5 ) {
				_se.Stop( );
                if ( !_intro_manager.limitFourChara( ) ) {
                    _timer_speed = 0;
                    Icon[ 7 ].SetActive( true );
					Icon[ 7 ].GetComponentInChildren<Text>( ).text = select;
                }
            }
            if ( _timer > e_t_5 + 1f ) {
                _timer_speed = 1;
                Icon[ 7 ].SetActive( false );
            }
            float end = 9f;
            if ( _timer > end ) {
                stop( );
                _cut_manager.NextCut( );
            }
        }
        if ( gameObject.name == "Cut12" ) {
            float e_t_1 = 2f;
            float e_t_2 = 3.5f;
            float e_t_3 = 5f;
            float e_t_4 = 6f;
            float e_t_5 = 8f;
            float e_t_6 = 10f;
            float e_t_7 = 12f;
            float end = 15f;
            if ( _timer > e_t_1 ) {
                Icon[ 5 ].GetComponent<SpriteRenderer>( ).color = new Color( 1, 1, 1, 1 - ( _timer - e_t_1 ) );
                Icon[ 6 ].GetComponent<SpriteRenderer>( ).color = new Color( 1, 1, 1, _timer - e_t_1 );
                Icon[ 0 ].transform.position += new Vector3( 0, -1.5f, 0 );
                Icon[ 10 ].transform.position += new Vector3( 0, 3f, 0 );
                if ( Icon[ 0 ].transform.position.y < -600f ) {
                    Icon[ 0 ].SetActive( false );
                    Icon[ 1 ].SetActive( false );
                    Icon[ 10 ].SetActive( false );
                }
                if ( Icon[ 2 ].transform.position.y > -200f ) {
                    Icon[ 2 ].transform.position += new Vector3( 0, -0.1f, 0 );
                }
            }
            if ( _timer > e_t_2 ) {
                Icon[ 6 ].GetComponent<SpriteRenderer>( ).color = new Color( 1, 1, 1, 1 - ( _timer - e_t_2 ) );
                Icon[ 5 ].GetComponent<SpriteRenderer>( ).color = new Color( 1, 1, 1, _timer - e_t_2 );
            }
            if ( _timer > e_t_3 ) {
                Icon[ 5 ].GetComponent<SpriteRenderer>( ).color = new Color( 1, 1, 1, 1 - ( _timer - e_t_3 ) );
                Icon[ 6 ].GetComponent<SpriteRenderer>( ).color = new Color( 1, 1, 1, _timer - e_t_3 );
                Icon[ 3 ].SetActive( true );
            }
            if ( _timer > e_t_4 ) {
                Icon[ 6 ].GetComponent<SpriteRenderer>( ).color = new Color( 1, 1, 1, 1 - ( _timer - e_t_4 ) );
                Icon[ 5 ].GetComponent<SpriteRenderer>( ).color = new Color( 1, 1, 1, _timer - e_t_4 );
                Icon[ 3 ].SetActive( false );
                Icon[ 4 ].SetActive( true );
            }
            if ( _timer > e_t_5 ) {
                Icon[ 7 ].SetActive( true );
            }
            if ( _timer > e_t_6 ) {
                Icon[ 8 ].SetActive( true );
            }

            if ( _timer > e_t_7 ) {
                if ( !_intro_manager.limitFourChara( ) ) {
                    _timer_speed = 0;
                    Icon[ 9 ].SetActive( true );
					Icon[ 9 ].GetComponentInChildren<Text>( ).text = select;
                }
            }
            if ( _timer > e_t_7 + 1f ) {
                _timer_speed = 1;
                Icon[ 9 ].SetActive( false );
            }

            if ( _timer > end ) {
                if ( _intro_manager.allDeath( ) ) {
                    PlayerPrefs.SetInt( "GameOver", 0 );
                    PlayerPrefs.SetInt( "LoadGame", 0 );
                    PlayerPrefs.Save( );
                    SceneManager.LoadScene( "GameOverScene" );
                } else {
                    SceneManager.LoadScene( "GameLoading" );
                }
            }
        }
    }

    public void TimerStart( ) {
        _timer += 1f;
    }

    public void play( ) {
        _play = true;
        gameObject.SetActive( true );
    }

    public void stop( ) {
        _play = false;
    }

    public bool isPlay( ) {
        return _play;
    }
}
