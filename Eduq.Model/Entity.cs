using System;

namespace Eduq.Model
{
    public class Entity
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string DisplayName { get; set; }
        public string PluralName { get; set; }
        public string Code { get; set; }
    }
}
