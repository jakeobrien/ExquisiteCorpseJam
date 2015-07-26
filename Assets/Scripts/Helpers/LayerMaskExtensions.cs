using UnityEngine;

public static class LayerMaskExtensions
{
    public static bool ContainsLayer(this LayerMask mask, int layer)
    {
        return ((mask.value & (1 << layer)) != 0);
    }
}
