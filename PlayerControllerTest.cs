using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {
    //問題１
    //CanTrackをbooleanして、publicしている
    public bool CanTrack;
	public bool collided;
	private Vector3 position;
	private Vector3 WorldPointPos;

	void Update (){
		//問題２
        //CanTrackの判定
		if(CanTrack){
			Track();
		}
		//問題３
        //マウスの左クリックの判定
		if(Input.GetMouseButtonDown(0)){
			CanTrack = false;
			GetComponent<Rigidbody>().useGravity = true;
		}
		if(GetComponent<Rigidbody>().IsSleeping() && CanTrack == false){
			GetComponent<MeshRenderer>().material.color *= new Color(0.2f, 0.2f, 0.2f, 0.5f);
		}
	}
	void Track(){
		//問題４
        //マウスの位置座標を取得
		position = Input.mousePosition;
		position.z = -Camera.main.transform.position.z;
		WorldPointPos = Camera.main.ScreenToWorldPoint(position);

		float MoveRangeX = GameObject.Find("Base").transform.localScale.x/2;

		if (WorldPointPos.x <= -MoveRangeX) {
			WorldPointPos.x = -MoveRangeX;
		} else if (WorldPointPos.x >= MoveRangeX) {
			WorldPointPos.x = MoveRangeX;
		}

		WorldPointPos.y = GameObject.Find("SpawnPoint").transform.position.y;
		gameObject.transform.position = WorldPointPos;
	}
	//問題５
    //衝突したときに実行する関数
	void OnCollisionEnter(){
		if(collided == false && CanTrack == false){

			GameObject.Find("SpawnPoint").GetComponent<Spawner>().SpawnCube();
			collided = true;
		}
	}
}