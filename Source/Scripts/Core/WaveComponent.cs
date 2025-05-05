using DCFApixels.DragonECS;

public struct WaveComponent : IEcsComponent
{
    public int WaveID;
}

public class WaveT : ComponentTemplate<WaveComponent>
{

}
