using UnityEngine;

public class ResourceCell : MonoBehaviour
{
    private Resource _resource;

    public bool IsEmpty => _resource == null;
    public bool IsReserved = false;

    public void SetResource(Resource resource) => _resource = resource;

    public Resource GetResource()
    {   
        var resource = _resource;
        Clear();

        return resource;
    }

    public void Reserve() => IsReserved = true;

    private void Clear()
    {
        IsReserved = false;
        _resource = null;
    }
}