using SpaceGame.Enemy;
using SpaceGame.Player;
using System.Collections;
using UnityEngine;

public class EnemyFactory : MonoBehaviour
{
    [SerializeField] private EnemyShip _enemyShipPrefab;
    [SerializeField] private float _minPositionX;
    [SerializeField] private float _maxPositionX;
    [SerializeField] private float _positionY;
    [SerializeField] private float _spawnDelay = 10f;
    private WaitForSeconds _wait;
    private PlayerShip[] players;

    private void Start()
    {
        _wait = new WaitForSeconds(_spawnDelay);
        var firstPlayer = FindObjectOfType<MousePlayerShip>();
        var secondPlayer = FindObjectOfType<KeyBoardPlayerShip>();
        players = new PlayerShip[] { firstPlayer, secondPlayer };
        StartCoroutine(SpawnEnemyCoroutine());
    }

    private IEnumerator SpawnEnemyCoroutine()
    {
        while (true)
        {
            yield return _wait;
            var enemyShip = CreateEnemyShip();
            //var player = FindRandomPlayer();
            enemyShip.SetTargets(players);
        }
    }

    private EnemyShip CreateEnemyShip()
    {
        var positionX = Random.Range(_minPositionX, _maxPositionX);
        var position = new Vector3(positionX, _positionY, 0);
        var enemyShip = Instantiate(_enemyShipPrefab, position, Quaternion.identity);
        return enemyShip;
    }
}