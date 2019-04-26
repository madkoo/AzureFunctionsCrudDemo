using Microsoft.WindowsAzure.Storage.Table;
using System;

namespace CRUDFunctionAppDemo
{
    public class MarvelHeroTableEntity : TableEntity
    {
        public DateTime CreatedTime { get; set; }
        public string Name { get; set; }
        public bool IsDead { get; set; }
    }

    public static class Mappings
    {
        public static MarvelHeroTableEntity ToTableEntity(this MarvelHero marvelHero)
        {
            return new MarvelHeroTableEntity()
            {
                PartitionKey = FunctionsSettings.PartitionKey,
                RowKey = marvelHero.Id,
                CreatedTime = marvelHero.CreatedTime,
                IsDead = marvelHero.IsDead,
                Name = marvelHero.Name
            };
        }

        public static MarvelHero ToMarvelHero(this MarvelHeroTableEntity marvelHero)
        {
            return new MarvelHero()
            {
                Id = marvelHero.RowKey,
                CreatedTime = marvelHero.CreatedTime,
                IsDead = marvelHero.IsDead,
                Name = marvelHero.Name
            };
        }
    }
}
