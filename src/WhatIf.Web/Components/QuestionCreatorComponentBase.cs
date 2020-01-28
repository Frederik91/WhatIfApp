using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;

namespace WhatIf.Web.Components
{
    public class QuestionCreatorComponentBase : ComponentBase
    {
        [Parameter]
        public QuestionModel Question { get; set; }

    }
}
