using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameController : MonoBehaviour {
    int[,] stage = new int[10 + 2, 20 + 1];
    int[,,] block0 = new int[,,] {
        {{0,0}, {1,0}, {2,0}, {0,1}},
        {{0,0}, {1,0}, {1,1}, {1,2}},
        {{2,0}, {0,1}, {1,1}, {2,1}},
        {{0,0}, {0,1}, {0,2}, {1,2}}
    };
    int[,,] block1 = new int[,,] {
        {{0,0}, {1,0}, {2,0}, {2,1}},
        {{1,0}, {1,1}, {0,2}, {1,2}},
        {{0,0}, {0,1}, {1,1}, {2,1}},
        {{0,0}, {1,0}, {0,1}, {0,2}}
    };
    int[,,] block2 = new int[,,] {
        {{0,0}, {1,0}, {2,0}, {1,1}},
        {{1,0}, {0,1}, {1,1}, {1,2}},
        {{1,0}, {0,1}, {1,1}, {2,1}},
        {{0,0}, {0,1}, {1,1}, {0,2}}
    };
    int[,,] block3 = new int[,,] {
        {{0,0}, {0,1}, {1,1}, {1,2}},
        {{1,0}, {2,0}, {0,1}, {1,1}},
    };
    int[,,] block4 = new int[,,] {
        {{1,0}, {0,1}, {1,1}, {0,2}},
        {{0,0}, {1,0}, {1,1}, {2,1}},
    };
    int[,,] block5 = new int[,,] {
        {{0,0}, {0,1}, {0,2}, {0,3}},
        {{0,1}, {1,1}, {2,1}, {3,1}}
    };
    int[,,] block6 = new int[,,] {
        {{0,0}, {1,0}, {0,1}, {1,1}}
    };
    List<int[,,]> block_list = new List<int[,,]>();
    int blockIndex;
    int rotIndex;
    int[,,] block;
    Vector2 block_pos;
    float fall_time = 0.0f;
    bool is_ready = false;
    //private GameObject[,] data_stage = new GameObject[10,20];

    public GameObject view_block;
    public GameObject view_cube;
    public GameObject view_stage;
    public float fall_time_th = 0.1f;

    // Use this for initialization
    void Start() {
        InitStage();

        block_list.Add(block0);
        block_list.Add(block1);
        block_list.Add(block2);
        block_list.Add(block3);
        block_list.Add(block4);
        block_list.Add(block5);
        block_list.Add(block6);
    }

    // Update is called once per frame
    void Update() {
        float dt = Time.deltaTime;
        fall_time += dt;

        CheckKey();

        if (is_ready && fall_time > fall_time_th)
        {
            if (IsValidShift(0, 1))
            {
                view_block.transform.position += new Vector3(0.0f, -1.0f, 0.0f);
                fall_time = 0.0f;
            }
            else
            {
                UpdateStage();
                DeleteLine();
                GenBlock();
            }
        }
    }

    Vector2 View2DataPos(Vector2 v)
    {
        return new Vector2(v.x + 5 + 0.5f, -v.y + 20 - 0.5f);
    }

    Vector2 Data2ViewPos(int x, int y)
    {
        return new Vector2(x - 5 - 0.5f, 20 - y - 0.5f);
    }

    Vector2 Data2ViewPos(Vector2 v)
    {
        return new Vector2(v.x - 5 - 0.5f, 20 - v.y - 0.5f);
    }

    void UpdateStage()
    {
        int x, y;
        int i;

        for (i = 0; i < 4; i++)
        {
            x = (int)block_pos[0] + block[rotIndex, i, 0];
            y = (int)block_pos[1] + block[rotIndex, i, 1];
            stage[x, y] = 1;
        }

        i = 0;
        foreach (Transform obj in view_block.transform)
        {
            x = (int)block_pos[0] + block[rotIndex, i, 0];
            y = (int)block_pos[1] + block[rotIndex, i, 1];
            GameObject obj_block = Instantiate<GameObject>(obj.gameObject);
            obj_block.name = x.ToString() + "_" + y.ToString();
            obj_block.transform.parent = view_stage.transform;
            obj_block.transform.localPosition = Data2ViewPos(x, y); // new Vector3(x - 5 - 0.5f, 20 - y - 0.5f, 0);
            //data_stage[x-1, y] = obj_block;
            GameObject.Destroy(obj.gameObject);
            i++;
        }
    }

    void InitStage()
    {
        // init
        for (int y = 0; y < 20 + 1; y++)
            for (int x = 0; x < 10 + 2; x++)
                stage[x, y] = 0;
        // floor
        for (int x = 0; x < 10 + 2; x++)
            stage[x, 20] = 1;
        // wall
        for (int y = 0; y < 20 + 1; y++)
        {
            stage[0, y] = 1;
            stage[11, y] = 1;
        }
    }

    void CheckKey()
    {
        if (Input.GetKeyDown(KeyCode.W))
        {
        }
        else if (Input.GetKeyDown(KeyCode.S))
        {
            if (IsValidShift(0, -1))
                view_block.transform.position += new Vector3(-1.0f, 0.0f, 0.0f);
        }
        else if (Input.GetKeyDown(KeyCode.A))
        {
            if (IsValidShift(-1, 0))
                view_block.transform.position += new Vector3(-1.0f, 0.0f, 0.0f);
        }
        else if (Input.GetKeyDown(KeyCode.D))
        {
            if (IsValidShift(1, 0))
                view_block.transform.position += new Vector3(1.0f, 0.0f, 0.0f);
        }
        else if (Input.GetKeyDown(KeyCode.Comma))
        {
            if (IsValidRotation(-1))
            {
                //view_block.transform.Rotate(0.0f, 0.0f, -90.0f);
                Transform children = view_block.GetComponentInChildren<Transform>();
                int i = 0;
                foreach (Transform obj in children) {
                    obj.transform.localPosition = new Vector3(block[rotIndex, i, 0], -block[rotIndex, i, 1], 0.0f);
                    i++;

                }
            }
        }
        else if (Input.GetKeyDown(KeyCode.Period))
        {
            if (IsValidRotation(1)) {
                //view_block.transform.Rotate(0.0f, 0.0f, 90.0f);
                Transform children = view_block.GetComponentInChildren<Transform>();
                int i = 0;
                foreach (Transform obj in children)
                {
                    obj.transform.localPosition = new Vector3(block[rotIndex, i, 0], -block[rotIndex, i, 1], 0.0f);
                    i++;

                }
            }
        }
        else if (Input.GetKeyDown(KeyCode.Space))
        {
            is_ready = true;
            GenBlock();
        }
    }

    void DeleteLine()
    {
        List<int> deleted_line = new List<int>();

        for (int y = 0; y < 20; y++)
        {
            int cnt = 0;
            for (int x = 1; x < 10 + 1; x++)
            {
                cnt += stage[x, y];
            }
            if( cnt == 10 )
            {
                Debug.Log("delete line");
                deleted_line.Add(y);
                for (int x = 1; x < 10 + 1; x++)
                {
                    stage[x, y] = 0;
                    GameObject.Destroy(GameObject.Find(x.ToString() + "_" + y.ToString()));
                }
            }
        }

        foreach(int line in deleted_line)
        {
            for (int y = line; y >0; y--)
            {
                for(int x=1; x < 10 + 1; x++)
                {
                    stage[x, y] = stage[x, y-1];
                    GameObject obj = GameObject.Find(x.ToString() + "_" + (y-1).ToString());
                    if (obj)
                    {
                        obj.transform.Translate(new Vector3(0, -1, 0));
                        obj.name = x.ToString() + "_" + y.ToString();
                    }
                }
            }
        }
    }

    bool IsValidShift(int dx, int dy) {
        for (int i=0; i < 4; i++)
        {
            if (stage[(int)block_pos[0] + dx + block[rotIndex, i, 0], 
                      (int)block_pos[1] + dy + block[rotIndex, i, 1]] == 1)
                return false;
        }
        block_pos[0] += dx;
        block_pos[1] += dy;
        return true;
    }

    // dir
    //  1: clockwise
    //  -1: cclockwise
    bool IsValidRotation(int dir)
    {
        int n_rot_pattern = block.Length / 4 / 2;
        int new_rotIndex = (rotIndex + n_rot_pattern + dir) % n_rot_pattern;

        for (int i=0; i < 4; i++)
        {
            if (stage[(int)block_pos[0] + block[new_rotIndex, i, 0], 
                      (int)block_pos[1] + block[new_rotIndex, i, 1]] == 1)
                return false;
        }
        rotIndex = new_rotIndex;
        return true;
    }

    void GenBlock()
    {
        blockIndex = Random.Range(0, 7);
        rotIndex = 0;
        block = block_list[blockIndex];

        block_pos = new Vector2(5, 0);

        for (int i = 0; i < 4; i++)
        {
            if (stage[(int)block_pos[0] + block[rotIndex, i, 0],
                      (int)block_pos[1] + block[rotIndex, i, 1]] == 1)
            {
                is_ready = false;
                Debug.Log("game over");
                return;
            }
        }

        // [pattern(1~4), each_cube_block(4), xy(2)]
        for (int i=0; i < 4; i++)
        {
            //Debug.Log(new Vector3(block[rotIndex,i,0], block[rotIndex, i, 1], 0.0f));
            GameObject prefab = (GameObject)Resources.Load("Prefabs/Cube");
            view_cube = Instantiate(prefab);
            view_cube.transform.parent = view_block.transform;
            view_cube.transform.localPosition = new Vector3(block[rotIndex, i, 0], -block[rotIndex, i, 1], 0.0f);
        }

        view_block.transform.position = Data2ViewPos(block_pos); //new Vector3(0.0f - 0.5f, 20.0f - 0.5f, 0.0f);
        view_block.SetActive(true);


    }
}