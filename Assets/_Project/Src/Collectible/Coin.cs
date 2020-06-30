namespace Ransomink.Collectible
{
    public class Coin : Collectible
    {
        private void OnEnable()
        {
            if (group != null) transform.SetParent(group.transform);
        }
    }
}
