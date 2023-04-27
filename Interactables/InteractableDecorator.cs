namespace StarterGame.Interactables
{
    public abstract class InteractableDecorator : Interactable
    {
        protected readonly Interactable Interactable;
        public InteractableDecorator(Interactable interactable, string name, string description) : base(name, description)
        {
            Interactable = interactable;
        }
    }
}