public class ExitHole : AbstractInteractable
{
    public override void Interact()
    {
        _ = ServiceLocator.Get<SceneTransitioner>().TransitionToNextLevelAsync();
    }
}