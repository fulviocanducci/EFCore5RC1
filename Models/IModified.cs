using System;

namespace EFCore5RC1.Models
{
    public interface IModified
    {
        DateTime CreatedAt { get; set; }
        DateTime UpdatedAt { get; set; }
    }
}
