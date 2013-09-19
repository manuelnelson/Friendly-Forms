using System.Collections.Generic;

namespace BusinessLogic.Contracts
{
    public interface IConstantsService
    {
        Dictionary<string, string> GetConstants();
    }
}
