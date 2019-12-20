using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class SceneContents : ScriptableObject
{
    public List<Enemy> Enemies { get => enemies;  private set => enemies = value;  }
    List<Enemy> enemies;
    public List<PlayerController> Players { get => players; private set => players = value; }
    List<PlayerController> players;

    private void OnEnable()
    {
        enemies = new List<Enemy>();
        players = new List<PlayerController>();
    }

    public void RegisterEnemy(Enemy enemy)
    {
        Enemies.Add(enemy);
    }

    public void UnregisterEnemy(Enemy enemy)
    {
        Enemies.Remove(enemy);
    }

    public void RegisterPlayer(PlayerController player)
    {
        Players.Add(player);
    }

    public void UnregisterPlayer(PlayerController player)
    {
        Players.Remove(player);
    }

}
