using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapManager : MonoBehaviour {


    //地图元素
    public GameObject[] outWallArray;
    public GameObject[] floorArray;
    public GameObject[] wallArray;
    public GameObject[] foodArray;
    public GameObject[] enemyArray;
    public GameObject exitPrefabs;
    public GameObject manPrefabs;


    //地图行、列
    public int rows = 12;
    public int cols = 12;


    //内部的元素位置（去除边框）
    //public int minCountWallC = 2;
    //public int maxCountWallC = 14;
    //public int minCountWallR = 2;
    //public int maxCpuntWallR = 12;
    public int minCountWall = 2;
    public int maxCountWall = 10;


    //创建地图文件夹，显示游戏地图所有元素
    private Transform mapHolder;

    private List<Vector2> positionList = new List<Vector2>();


    //获取GameManager
    private GameManager gameManager;

    
    //随机取得位置
    private Vector2 RandomPosition()
    {
        int positionIndex = Random.Range(0, positionList.Count);
        Vector2 pos = positionList[positionIndex];
        positionList.RemoveAt(positionIndex);
        return pos;
    }
    //随机获得当前需要放入的地图元素
    private GameObject RandomPrefabs(GameObject[] randomPrefabs)
    {
        int index = Random.Range(0, randomPrefabs.Length);
        return randomPrefabs[index];
    }


    //初始化地图方法
    private void InitMap()
    {
        mapHolder = new GameObject("map").transform;
        for(int x = 0;x<cols;x++)
        {
            for(int y = 0;y<rows;y++)
            {
                //
                if(x ==0||y==0||x==cols-1||y==rows-1)
                {
                    //外墙随机二选一
                    int index = Random.Range(0, outWallArray.Length);
                    GameObject outWall = GameObject.Instantiate(outWallArray[index], new Vector3(x, y, 0), Quaternion.identity) as GameObject;
                    outWall.transform.SetParent(mapHolder);
                }
                //floor
                else
                {
                    int index = Random.Range(0, floorArray.Length);
                    GameObject floor = GameObject.Instantiate(floorArray[index], new Vector3(x, y, 0), Quaternion.identity) as GameObject;
                    floor.transform.SetParent(mapHolder);
                }
            }
        }

        //清空PositionList
        positionList.Clear();
        for(int x= 2;x<cols-2;x++)
        {
            for(int y = 2;y<rows-2;y++)
            {
                positionList.Add(new Vector2(x, y));
            }
        }

  
        //障碍物
        int wallCount = Random.Range(minCountWall, maxCountWall + 1);//障碍物的个数
        for (int i = 0; i < wallCount; i++)
        {
            //随机获得位置
            Vector2 pos =  RandomPosition();
            //随即取得障碍物
            GameObject wallPrefabs = RandomPrefabs(wallArray);
            GameObject wall = GameObject.Instantiate(wallPrefabs, pos, Quaternion.identity) as GameObject;
            wall.transform.SetParent(mapHolder);
        }
        //食物  最小2，最大Level*2
        int foodCount = Random.Range(2, gameManager.level * 2 + 1);
        for(int i = 0;i<foodCount;i++)
        {
            Vector2 pos = RandomPosition();
            GameObject foodPrefabs = RandomPrefabs(foodArray);
            GameObject food = GameObject.Instantiate(foodPrefabs, pos, Quaternion.identity) as GameObject;
            food.transform.SetParent(mapHolder);
        }

        //敌人
        int enemyCount = gameManager.level / 2;
        for(int i = 0;i<enemyCount;i++)
        {
            Vector2 pos = RandomPosition();
            GameObject enemyPrefabs = RandomPrefabs(enemyArray);
            GameObject enemy = GameObject.Instantiate(enemyPrefabs, pos, Quaternion.identity) as GameObject;
            enemy.transform.SetParent(mapHolder);
        }

        //创建目的地
        GameObject target = Instantiate(exitPrefabs, new Vector2(cols - 2, rows - 2), Quaternion.identity) as GameObject;
        target.transform.SetParent(mapHolder);
    }


    // Use this for initialization 初始化
    void Awake () {

        gameManager = this.GetComponent<GameManager>();

        InitMap();
	}
	
	// Update is called once per frame
	void Update () {
		
	}



}
