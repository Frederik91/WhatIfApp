using Microsoft.AspNetCore.Components;
using WhatIf.Web.Components.Questions;

namespace WhatIf.Web.Components.QuestionAnswers
{
    public class QuestionReaderComponentBase : ComponentBase
    {
        [Parameter]
        public ReadQuestionModel Question { get; set; }

        protected override void OnParametersSet()
        {
            base.OnParametersSet();
        }
    }
}
