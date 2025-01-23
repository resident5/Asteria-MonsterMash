using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : Singleton<InputManager>
{
    [Header("Input Action")]
    [SerializeField] private InputActionAsset playerControls;

    [Header("Action Map Name References")]
    [SerializeField] private string actionMapName = "Player";
    [SerializeField] private string actionMenuMapName = "UI";

    [Header("Action Name Reference")]
    [SerializeField] private string move = "Movement";
    [SerializeField] private string jump = "Jump";
    [SerializeField] private string attack = "Attack";
    [SerializeField] private string interact = "Interact";
    [SerializeField] private string pause = "Pause";

    [Header("Action Menu Name Reference")]
    [SerializeField] private string cancel = "Cancel";

    private InputAction moveAction;
    private InputAction jumpAction;
    private InputAction interaction;
    private InputAction attackAction;
    private InputAction pauseAction;

    private InputAction cancelAction;

    public Vector2 MoveInput { get; private set; }
    public bool Jumped { get; private set; }
    public bool PauseInput { get; private set; }
    public bool Attacked { get; private set; }
    public bool Cancelled { get; private set; }
    public bool Interacted { get; private set; }

    //public static InputManager Instance { get; private set; }

    protected override void Awake()
    {
        base.Awake();
        //Put this on another gameobject besides the Game Manager
        //if(Instance == null)
        //{
        //    Instance = this;
        //    DontDestroyOnLoad(gameObject);
        //}
        //else
        //{
        //    Destroy(gameObject);
        //}

        moveAction = playerControls.FindActionMap(actionMapName).FindAction(move);
        jumpAction = playerControls.FindActionMap(actionMapName).FindAction(jump);
        attackAction = playerControls.FindActionMap(actionMapName).FindAction(attack);
        pauseAction = playerControls.FindActionMap(actionMapName).FindAction(pause);
        interaction = playerControls.FindActionMap(actionMapName).FindAction(interact);

        cancelAction = playerControls.FindActionMap(actionMenuMapName).FindAction(cancel);

        RegisterInputActions();
    }

    private void OnEnable()
    {
        moveAction.Enable();
        jumpAction.Enable();
        attackAction.Enable();
        pauseAction.Enable();
        interaction.Enable();

        cancelAction.Enable();
    }

    //private void OnDisable()
    //{
    //    moveAction.Disable();
    //    jumpAction.Disable();
    //    attackAction.Disable();
    //    pauseAction.Disable();
    //    interaction.Disable();

    //    cancelAction.Disable();
    //}

    void RegisterInputActions()
    {
        moveAction.performed += context => MoveInput = context.ReadValue<Vector2>();
        moveAction.canceled += context => MoveInput = Vector2.zero;

        jumpAction.performed += context => Jumped = true;
        jumpAction.canceled += context => Jumped = false;

        attackAction.performed += context => Attacked = true;
        attackAction.canceled += context => Attacked = false;

        pauseAction.performed += context => PauseInput = true;
        pauseAction.canceled += context => PauseInput = false;

        //interaction.started += context => Interacted = true;
        //interaction.canceled += context => Interacted = false;

        //cancelAction.started += context => Cancelled = true;
        //cancelAction.canceled += context => Cancelled = false;
    }

    private void Update()
    {
        if (interaction.WasCompletedThisFrame())
            Interacted = true;
        else
            Interacted = false;

        if(cancelAction.WasCompletedThisFrame())
            Cancelled = true;
        else
            Cancelled = false;
        
    }
}
