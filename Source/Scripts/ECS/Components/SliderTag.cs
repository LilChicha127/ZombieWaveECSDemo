using DCFApixels.DragonECS;
using UnityEngine.UI;

[System.Serializable]
public struct SliderComponent : IEcsComponent
{
    public Slider view;
}

public class SliderT : ComponentTemplate<SliderComponent> { }