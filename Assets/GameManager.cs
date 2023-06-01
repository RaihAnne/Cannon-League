using System;
using Unity.Netcode;
using UnityEngine;

public class GameManager : NetworkBehaviour
{
    public static GameManager Singleton;
    [SerializeField] private int MaximumPlayerCount = 2;
    [SerializeField] private GameCountdownHandler countdownHandler;
    [SerializeField] private Ball ball;
    [SerializeField] private int MaxRoundCount = 2;

    private int CurrentRound;

    public static Action OnGameStartEvent;
    public static Action OnGameEndEvent;

    private void Awake()
    {
        Singleton = this;
        NetworkManagerCustomEvents.OnClientConnectedEvent += OnClientConnect;
        Wall.OnBallGoalEvent += OnGoalEvent;
    }

    private void OnGoalEvent()
    {
        if (!IsServer)
        {
            return;
        }
        EndGameClientRpc();
    }

    private void OnClientConnect(ulong client)
    {
        if (!IsServer)
        {
            return;
        }

        if (NetworkManager.Singleton.ConnectedClientsList.Count == MaximumPlayerCount)
        {
            Invoke(nameof(StartGameClientRpc), 0.3f);
        }
    }

    [ClientRpc]
    public void StartGameClientRpc()
    {
        countdownHandler.StartCountdown();
        LaunchEvent(OnGameStartEvent);
    }

    [ClientRpc]
    public void EndGameClientRpc()
    {
        ball.transform.SetPositionAndRotation(Vector3.zero, Quaternion.identity);
        var ballBody = ball.GetComponent<Rigidbody2D>();
        ballBody.velocity = Vector2.zero;
        ballBody.angularVelocity = 0f;

        LaunchEvent(OnGameEndEvent);

        Invoke(nameof(StartGameClientRpc), 1.2f);
    }



    private void LaunchEvent(Action someAction)
    {
        if (someAction == null)
        {
            return;
        }

        someAction();
    }
}