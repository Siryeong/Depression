using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleGenerator : MonoBehaviour
{
    [SerializeField] private Transform start_position = null;
    [SerializeField] private Transform end_position = null;

    [SerializeField] private float spawn_interval_min = 1f;
    [SerializeField] private float spawn_interval_max = 1f;
    [SerializeField] private float obstacle_scale_min = 1f;
    [SerializeField] private float obstacle_scale_max = 1f;
    [SerializeField] private GameObject[] obstacles = null;
    [SerializeField] private Transform[] gen_point = null;

    

    // Start is called before the first frame update
    void Start()
    {
        Initialization();
        StartCoroutine(SpawnSomething());
    }

    void Initialization()
    {
        string level = GameManager.Instance.GetLevel();
        switch (level)
        {
            case "Easy":
            spawn_interval_min *= 1.5f;
            spawn_interval_max *= 1.5f;
            break;

            case "Medium":
            break;

            case "Hard":
            spawn_interval_min *= 0.75f;
            spawn_interval_max *= 0.75f;
            break;

            default:
                break;
        }
    }

    IEnumerator SpawnSomething()
    {
        // start position init
        gameObject.SetActive(true);
        transform.position = start_position.position;
        transform.rotation = start_position.rotation;

        // random spawn of obstacles
        while(true)
        {
            // escape condition
            if(Vector3.Distance(transform.position, end_position.position) < 0.001f) break;

            // exception handling
            if(obstacles.Length == 0) 
            {
                Debug.LogError("obstacles no exist");
                transform.position = end_position.position;
                continue;
            }

            // init
            float spawn_distance = Random.Range(spawn_interval_min, spawn_interval_max);
            float obstacle_scale = Random.Range(obstacle_scale_min, obstacle_scale_max);
            GameObject prefab = obstacles[Random.Range(0, obstacles.Length)];
            Transform position = gen_point[Random.Range(0, gen_point.Length)];

            // move
            transform.position = Vector3.MoveTowards(transform.position, end_position.position, spawn_distance);

            // spawn
            GameObject spawn = GameObject.Instantiate<GameObject>(prefab);
            spawn.transform.position = position.position;
            spawn.transform.rotation = position.rotation;
            spawn.transform.localScale = new Vector3(obstacle_scale, obstacle_scale, obstacle_scale);
            spawn.transform.Rotate(new Vector3(Random.Range(1,360),Random.Range(1,360),Random.Range(1,360)), Space.World);
            if(spawn.GetComponent<Wall>() == null)
            {
                spawn.AddComponent<Wall>();
            }

            continue;
        }
        gameObject.SetActive(false);
        yield return false;
    }

}
