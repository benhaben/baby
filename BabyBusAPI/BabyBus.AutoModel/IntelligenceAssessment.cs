//------------------------------------------------------------------------------
// <auto-generated>
//     此代码已从模板生成。
//
//     手动更改此文件可能导致应用程序出现意外的行为。
//     如果重新生成代码，将覆盖对此文件的手动更改。
// </auto-generated>
//------------------------------------------------------------------------------

namespace BabyBus.AutoModel
{
    using System;
    using System.Collections.Generic;
    
    public partial class IntelligenceAssessment
    {
        public int IntelligenceAssessmentId { get; set; }
        public int ChildId { get; set; }
        public Nullable<System.DateTimeOffset> CreateTime { get; set; }
        public int UserId { get; set; }
        public int IntelligenceAssessmentType { get; set; }
        public int IntelligenceQuestionId { get; set; }
        public int Score { get; set; }
    }
}
