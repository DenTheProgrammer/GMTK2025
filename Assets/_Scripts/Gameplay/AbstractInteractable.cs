using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public abstract class AbstractInteractable : MonoBehaviour, IInteractable
{
    [SerializeField] private KeyCode interactKey = KeyCode.E;
    [SerializeField] private Transform hint;

    internal bool _interactInProgress;
    private bool _inZone = false;
    
    public abstract void Interact();


    public virtual void Awake()
    {
        hint.gameObject.SetActive(false);
    }

    public virtual void OnCollisionEnter2D(Collision2D other)
    {
        OnTriggerEnter2D(other.collider);
    }

    public virtual void OnTriggerEnter2D(Collider2D other)
    {
        _inZone = true;
        ToggleHint(true);
    }

    public virtual void Update()
    {
        if (_interactInProgress)
        {
            ToggleHint(false);    
        }
        
        if (Input.GetKeyDown(interactKey))
        {
            if (_inZone)
            {
                Interact();
            }
        }
    }

    public virtual void OnCollisionExit2D(Collision2D other)
    {
        OnTriggerEnter2D(other.collider);
    }

    public virtual void OnTriggerExit2D(Collider2D other)
    {
        _inZone = false;
        ToggleHint(false);
    }

    private void ToggleHint(bool show)
    {
        hint.gameObject.SetActive(show);
    }
}