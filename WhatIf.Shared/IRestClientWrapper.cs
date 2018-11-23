using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using WhatIf.Shared.Services.Session;
using WhatIf.Shared.Services.User;

namespace WhatIf.Shared
{
    public interface IRestClientWrapper
    {
        Task<T> GetAsync<T>(string text, string urlEnding);
        Task<T> GetAsync<T>(Guid id, string urlEnding);
        Task<T> Post<T>(string urlEnding, object payload);
        Task Put(string urlEnding, object payload);
    }
}
