using System;
namespace backend.src.GOD.Domain.Core
{
    public interface ITrackableEntity
    {   
        DateTime CreatedAt { get; set; }

        DateTime ModifiedAt { get; set; }
    }
}
