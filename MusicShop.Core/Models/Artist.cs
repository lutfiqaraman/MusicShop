﻿using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace MusicShop.Core.Models
{
    public class Artist
    {
        public Artist()
        {
            Musics = new Collection<Music>();
        }

        public string Id { get; set; }
        public string Name { get; set; }
        public ICollection<Music> Musics { get; set; }
    }
}