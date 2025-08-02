using UnityEngine;

public class ExitHole : AbstractInteractable
{
    [SerializeField] private Animator playerAnimator;
    
    private static readonly int ExitLevelTrigger = Animator.StringToHash("ExitLevelTrigger");
    public override async void Interact()
    {
        if (_interactInProgress) return;
        _interactInProgress = true;
        
        playerAnimator.SetTrigger(ExitLevelTrigger);
        await Awaitable.WaitForSecondsAsync(0.1f);
        //playerAnimator.gameObject.GetComponentInParent<Collider2D>().enabled = false;
        playerAnimator.gameObject.GetComponentInParent<PlayerController>().enabled = false;
        _ = ServiceLocator.Get<SceneTransitioner>().TransitionToNextLevelAsync();
        
    }
}