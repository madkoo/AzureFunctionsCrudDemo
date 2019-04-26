using System;

namespace CRUDFunctionAppDemo
{
    public class MarvelHero
    {
        public string Id { get; set; } = Guid.NewGuid().ToString("n");
        public DateTime CreatedTime { get; set; } = DateTime.UtcNow;
        public string Name { get; set; }
        public bool IsDead { get; set; }
    }
}