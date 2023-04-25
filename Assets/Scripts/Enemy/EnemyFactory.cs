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
    private MousePlayerShip _firstPlayer;
    private KeyBoardPlayerShip _secondPlayer;

    private void Start()
    {
        _wait = new WaitForSeconds(_spawnDelay);
        _firstPlayer = FindObjectOfType<MousePlayerShip>();
        _secondPlayer = FindObjectOfType<KeyBoardPlayerShip>();

        StartCoroutine(SpawnEnemyCoroutine());
    }

    private IEnumerator SpawnEnemyCoroutine()
    {
        while (true)
        {
            yield return _wait;
            var enemyShip = CreateEnemyShip();
            var player = FindRandomPlayer();
            enemyShip.SetTarget(player);
        }
    }

    private PlayerShip FindRandomPlayer()
    {
        var playerIndex = Random.Range(0, 2);

        switch (playerIndex)
        {
            case 0:
                return _firstPlayer != null
                            ? _firstPlayer
                            : _secondPlayer;

            case 1:
                return _secondPlayer != null
                            ? _secondPlayer
                            : _firstPlayer;

            default: throw new System.Exception();
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