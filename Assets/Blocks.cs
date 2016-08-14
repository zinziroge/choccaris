using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Blocks : MonoBehaviour {
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
        {{0,1}, {0,2}, {0,3}, {0,4}},   
        {{0,1}, {1,1}, {2,1}, {3,1}}
    };
    int[,,] block6 = new int[,,] {
        {{0,0}, {1,0}, {0,1}, {1,1}}
    };
    List<int[,,]> block_list = new List<int[,,]>();
    int blockIndex;

    // Use this for initialization
    void Start () {
        block_list.Add(block0);
        block_list.Add(block1);
        block_list.Add(block2);
        block_list.Add(block3);
        block_list.Add(block4);
        block_list.Add(block5);
        block_list.Add(block6);
    }

    // Update is called once per frame
    void Update () {
	
	}
}
