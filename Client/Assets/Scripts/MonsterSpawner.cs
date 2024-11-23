using System;
using System.Collections;
using UnityEngine;

public class MonsterSpawner : MonoBehaviour
{
    public static MonsterSpawner Instance => _instance;
    private static MonsterSpawner _instance;

    private void Awake()
    {
        _instance = this;
    }

    private void Start()
    {
        StartSpawn();
    }

    public void StartSpawn()
    {
        StartCoroutine(_StartSpawn());
    }

    private IEnumerator _StartSpawn()
    {
        var wfs = new WaitForSeconds(1f);

        while (true)
        {
            float x = UnityEngine.Random.Range(-5f, 5f);
            float y = (float)Math.Sqrt(25 - x * x) * (UnityEngine.Random.Range(0, 1) == 0 ? -1 : 1);

            var snake = ObjectPoolManager.Instance.Get<Snake>();
            var pos = snake.transform.position;
            pos.x = x;
            pos.y = y;
            snake.transform.position = pos;
            snake.Initialize();

            x = UnityEngine.Random.Range(-5f, 5f);
            y = (float)Math.Sqrt(25 - x * x) * (UnityEngine.Random.Range(0, 1) == 0 ? -1 : 1);

            var viper = ObjectPoolManager.Instance.Get<Viper>();
            pos = viper.transform.position;
            pos.x = x;
            pos.y = y;
            viper.transform.position = pos;
            viper.Initialize();

            x = UnityEngine.Random.Range(-5f, 5f);
            y = (float)Math.Sqrt(25 - x * x) * (UnityEngine.Random.Range(0, 1) == 0 ? -1 : 1);

            var golem = ObjectPoolManager.Instance.Get<Golem>();
            pos = golem.transform.position;
            pos.x = x;
            pos.y = y;
            golem.transform.position = pos;
            golem.Initialize();

            x = UnityEngine.Random.Range(-5f, 5f);
            y = (float)Math.Sqrt(25 - x * x) * (UnityEngine.Random.Range(0, 1) == 0 ? -1 : 1);

            var skeleton = ObjectPoolManager.Instance.Get<Skeleton>();
            pos = skeleton.transform.position;
            pos.x = x;
            pos.y = y;
            skeleton.transform.position = pos;
            skeleton.Initialize();

            x = UnityEngine.Random.Range(-5f, 5f);
            y = (float)Math.Sqrt(25 - x * x) * (UnityEngine.Random.Range(0, 1) == 0 ? -1 : 1);

            var slime = ObjectPoolManager.Instance.Get<Slime>();
            pos = slime.transform.position;
            pos.x = x;
            pos.y = y;
            slime.transform.position = pos;
            slime.Initialize();

            yield return wfs;
        }
    }
}
