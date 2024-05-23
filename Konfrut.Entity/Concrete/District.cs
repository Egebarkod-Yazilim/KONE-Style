﻿using Konfrut.Shared.Entities.Abstract;

namespace KONE.Entity.Concrete
{
    public class District : EntityBase, IEntity
    {
        public Province Province { get; set; }
        public int ProvinceId { get; set; }
        public string Name { get; set; } = string.Empty;
        public int PropertyId { get; set; }
    }
}