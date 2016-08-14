using UnityEngine;
using System.Collections;

public class game_main : MonoBehaviour {
    private const int block_row = 20;
    private const int block_col = 10;
    // 0=no block, 1=fixed block
    private int[,] blocks_state = new int [block_row, block_col];
    private GameObject[,] blocks = new GameObject[block_row, block_col];
    private GameObject[] each_block = new GameObject[4];
    private GameObject block_prefab = null;
    private float fall_time = 0.0f;
    private bool exist_user_block = false;

    public GameObject user_block;
    public GameObject each_block_prefab;
    public float fall_time_th = 1.0f;

	// Use this for initialization
	void Start () {
        for(int i=0; i<20; i+=1)
            for(int j=0; j<10; j+=1)
                blocks_state[i, j] = 0;
	}
	
	// Update is called once per frame
	void Update () {
        float dt = Time.deltaTime;
        fall_time += dt;

        CheckKey();
        if( fall_time > fall_time_th)
        {
            block_prefab.transform.position += new Vector3(0.0f, -1.0f, 0.0f);
            fall_time = 0.0f;
            GenBlock();
        }
    }

    void CheckKey()
    {
        if(Input.GetKeyDown(KeyCode.W))
        {
            //user_block.transform.position += new Vector3(1.0f, 0.0f, 0.0f);
        }
        else if(Input.GetKeyDown(KeyCode.S))
        {
            user_block.transform.position += new Vector3(0.0f, -1.0f, 0.0f);
        }
        else if(Input.GetKeyDown(KeyCode.A))
        {
            user_block.transform.position += new Vector3(-1.0f, 0.0f, 0.0f);
        }
        else if(Input.GetKeyDown(KeyCode.D))
        {
            user_block.transform.position += new Vector3(1.0f, 0.0f, 0.0f);
        }
        else if(Input.GetKeyDown(KeyCode.Comma))
        {
            user_block.transform.Rotate(0.0f, 0.0f, -90.0f);
        }
        else if(Input.GetKeyDown(KeyCode.Period))
        {
            user_block.transform.rotation = Quaternion.Euler(user_block.transform.rotation.eulerAngles + new Vector3(0.0f, 0.0f, 90.0f));
        }
        else if (Input.GetKeyDown(KeyCode.Space))
        {
            if (block_prefab)
                DestroyObject(block_prefab);
            GenBlock();
        }
    }

    void GenBlock()
    {
        Debug.Log("gen block");
        /*for (int i = 0; i < 4; i += 1)
        {
            each_block[i] = Instantiate(each_block_prefab);
            each_block[i].transform.parent = user_block.transform;
            each_block[i].transform.position += new Vector3((float)i, 0.0f, 0.0f);
        }*/
        GameObject prefab = (GameObject)Resources.Load("Prefabs/Block_" + Random.Range(0, 7).ToString());
        block_prefab = Instantiate(prefab);
        user_block.transform.position = new Vector3(0.0f, 20.0f, 0.0f);
        block_prefab.transform.parent = user_block.transform;
        block_prefab.transform.localPosition = new Vector3(0.0f, 0.0f, 0.0f);
        user_block.SetActive(true);
    }

    void IsTouched()
    {

    }

    void CheckLine()
    {

    }
}
