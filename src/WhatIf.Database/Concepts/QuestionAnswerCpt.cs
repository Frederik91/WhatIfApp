using WhatIf.Database.Tables;

namespace WhatIf.Database.Concepts
{
    public class QuestionAnswerCpt
    {
        public QuestionTbl Question { get; set; }
        public AnswerTbl Answer { get; set; }
    }
}
