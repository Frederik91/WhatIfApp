using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using WhatIf.Web.Components.Questions;

namespace WhatIf.Web.Components.QuestionAnswers
{
    public class AnswerReaderComponentBase : ComponentBase
    {
        [Parameter]
        public ReadAnswerModel Answer { get; set; }
    }
}
