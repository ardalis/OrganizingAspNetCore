namespace WithFeatureFolders.Features.Samurai.Samurai
{
    public class SwordViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public override string ToString()
        {
            return $"{Name} ({Id})";
        }
    }
}