namespace GameStore.Domain.Models
{
    public class Game
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string ImageURL { get; set; } = string.Empty;
        public bool IsInstalled { get; set; } = false;
    }
}
