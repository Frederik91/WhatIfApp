using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;

namespace WhatIf.Web.Components
{
    public class AnswerCreatorComponentBase : ComponentBase
    {
        [Parameter]
        public AnswerModel Answer { get; set; }


    }
}
