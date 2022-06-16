
public class PlaneBase : Plane
{
    public Box Box { get; set; }

    internal override void Start()
    {
        base.Start();
    }

    public void RegistBox(Box box)
    {

    }

    public void OnBoxNotify(bool active)
    {
        print(active);
    }
}
