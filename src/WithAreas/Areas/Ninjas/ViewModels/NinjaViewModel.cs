namespace WithAreas.Areas.Ninjas.ViewModels
{
    public class NinjaViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public override string ToString()
        {
            return $"{Name} ({Id})";
        }
    }
}