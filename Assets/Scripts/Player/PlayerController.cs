using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [field: SerializeField] public InputReader InputReader {get; private set;}
    [field: SerializeField] public PlayerSO Data {get; private set;}

    private PlayerStateMachine stateMachine;

    public Transform cameraTransform { get; private set; }
    public Rigidbody Body;

    private void Awake()
    {
        cameraTransform = Camera.main.transform;
        stateMachine = new PlayerStateMachine(this);
        stateMachine.Awake();
    }

    private void Start()
    {
        stateMachine.Start();
    }

    private void Update()
    {
        stateMachine.Tick(Time.deltaTime);
    }

    private void FixedUpdate()
    {
        stateMachine.PhysicsTick(Time.fixedDeltaTime);
    }
}
