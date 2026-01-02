using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    public Player player;
    public List<Enemy> enemies;
    public Enemy enemyPrefab;

    public Vector2 enemyCount;

    public void RestartEnemyManager()
    {
        DeleteEnemies();
        GenerateEnemies();
    }

    private void GenerateEnemies()
    {
        var ramdomEnemyCount = UnityEngine.Random.Range(enemyCount.x, enemyCount.y);
        for (int i = 0; i < ramdomEnemyCount; i++)
        {
            var enemyXPos = UnityEngine.Random.Range(-2.56f, 2.44f);
            var newEnemy = Instantiate(enemyPrefab);
            newEnemy.transform.position = new Vector3(enemyXPos, 0, 3 + i * 1.5f);
            enemies.Add(newEnemy);
            newEnemy.StartEnemy(player);

        }
    }

    private void DeleteEnemies()
    {
        foreach (var e in enemies)
        {
            Destroy(e.gameObject);
        }
        enemies.Clear();
    }

    

    public void StopEnemies()
    {
        foreach (var e in enemies)
        {
            e.Stop();
        }
    }
}
 