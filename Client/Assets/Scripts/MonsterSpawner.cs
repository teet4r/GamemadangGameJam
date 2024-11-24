using System;
using System.Collections;
using UnityEngine;

public class MonsterSpawner : MonoBehaviour
{
    public enum MonsterName
    {
        Snake,
        Viper,
        Golem,
        Skeleton,
        Slime,
    }

    [System.Serializable]
    public class SpawnInfo
    {
        public MonsterName monsterName;
        public int count;
    }

    [System.Serializable]
    public class SpawnInfoArray
    {
        public SpawnInfo[] spawnInfoarray;
    }

    public static MonsterSpawner Instance => _instance;
    private static MonsterSpawner _instance;

    [SerializeField] private SpawnInfoArray[] _spawnInfoArrays;

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
        while (!Ingame.Instance.Hero.IsNull() && !Ingame.Instance.Hero.IsDead)
        {
            var arr = _spawnInfoArrays[Ingame.Instance.Hero.Level - 1].spawnInfoarray;
            for (int i = 0; i < arr.Length; ++i)
            {
                for (int j = 0; j < arr[i].count; ++j)
                {
                    float x = UnityEngine.Random.Range(-15f, 15f);
                    float y = (float)Math.Sqrt(225 - x * x) * (UnityEngine.Random.Range(0, 2) == 0 ? -1 : 1);

                    switch (arr[i].monsterName)
                    {
                        case MonsterName.Snake:
                            {
                                var snake = ObjectPoolManager.Instance.Get<Snake>();
                                var pos = snake.transform.position;
                                pos.x = x;
                                pos.y = y;
                                snake.transform.position = pos;
                                snake.Initialize();
                            }
                            break;
                        case MonsterName.Viper:
                            {
                                var viper = ObjectPoolManager.Instance.Get<Viper>();
                                var pos = viper.transform.position;
                                pos.x = x;
                                pos.y = y;
                                viper.transform.position = pos;
                                viper.Initialize();
                            }
                            break;
                        case MonsterName.Golem:
                            {
                                var golem = ObjectPoolManager.Instance.Get<Golem>();
                                var pos = golem.transform.position;
                                pos.x = x;
                                pos.y = y;
                                golem.transform.position = pos;
                                golem.Initialize();
                            }
                            break;
                        case MonsterName.Skeleton:
                            {
                                var skeleton = ObjectPoolManager.Instance.Get<Skeleton>();
                                var pos = skeleton.transform.position;
                                pos.x = x;
                                pos.y = y;
                                skeleton.transform.position = pos;
                                skeleton.Initialize();
                            }
                            break;
                        case MonsterName.Slime:
                            {
                                var slime = ObjectPoolManager.Instance.Get<Slime>();
                                var pos = slime.transform.position;
                                pos.x = x;
                                pos.y = y;
                                slime.transform.position = pos;
                                slime.Initialize();
                            }
                            break;
                    }
                }
            }

            yield return YieldCache.WaitForSeconds(3f);
        }
    }
}
