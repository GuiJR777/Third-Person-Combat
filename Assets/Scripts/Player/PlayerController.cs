using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [field: SerializeField] public InputReader InputReader {get; private set;}

    [field: Header("References")]
    [field: SerializeField] public PlayerSO Data {get; private set;}

    [field: Header("Collisions")]
    [field: SerializeField] public CapsuleColliderHandler capsuleColliderHandler { get; private set; }

    [field: Header("Layers")]
    [field: SerializeField] public PlayerLayerData layerData { get; private set; }
    [field: SerializeField] public Targeter Targeter;

    private PlayerStateMachine stateMachine;
    public FeetGrounder FeetGrounder;

    public Animator Animator;
    public Transform cameraTransform { get; private set; }
    public Rigidbody Body;

    private void Awake()
    {
        cameraTransform = Camera.main.transform;
        Animator = GetComponent<Animator>();
        FeetGrounder = GetComponent<FeetGrounder>();
        stateMachine = new PlayerStateMachine(this);
        stateMachine.Awake();
        capsuleColliderHandler.Initialize(gameObject);
        capsuleColliderHandler.CalculateCapsuleColliderDimensions();
    }

    void OnValidate()
    {
        capsuleColliderHandler.Initialize(gameObject);
        capsuleColliderHandler.CalculateCapsuleColliderDimensions();
    }

    private void Start()
    {
        Data.AnimationData.SetAnimator(Animator);
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
