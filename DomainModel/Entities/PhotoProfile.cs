using DomainModel.Entities;
using DomainModel.ObjectValues.Hair;
using System;

namespace DomainModel.Entities
{
    public class PhotoProfile : EntityBase
    {
        public string Url { get; set; }
        public HairColor HairColor { get; set; }
        public HairStyle HairStyle { get; set; }
        public HairLength HairLength { get; set; }
    }
}