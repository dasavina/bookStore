﻿namespace DapperPart.Entities
{
    public class BaseEntity
    {
        public int Id { get; set; }
        public DateTime? CreationTime { get; set; }
        public bool IsRowActive { get; set; }
    }
}
