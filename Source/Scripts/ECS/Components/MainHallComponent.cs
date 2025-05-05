using DCFApixels.DragonECS;

[System.Serializable]
public struct MainHallComponent : IEcsComponent
{
    public int health;
}

public struct MainHallAttackRequest : IEcsComponent
{
    public int damage;
    public EcsEntityConnect connect;
}

public struct MainHallDefeate : IEcsComponent
{
}

public class MainHallT : ComponentTemplate<MainHallComponent>
{

}
