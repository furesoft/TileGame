namespace TileGame.Core;

public abstract class Component : IComponent
{
    public GameObject GameObject { get; private set; }

    public Component(GameObject gameObject)
    {
        GameObject = gameObject;
    }

    public virtual void Initialize() { }

    public virtual void Start() { }
}