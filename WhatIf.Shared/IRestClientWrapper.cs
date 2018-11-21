using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace WhatIf.Shared
{
    public interface IRestClientWrapper
    {
        Task<T> GetAsync<T>(string text, string urlEnding);
        Task<T> GetAsync<T>(Guid id, string urlEnding);
    }
}
