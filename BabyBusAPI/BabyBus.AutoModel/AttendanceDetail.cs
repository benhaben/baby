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
    
    public partial class AttendanceDetail
    {
        public int DetailId { get; set; }
        public System.DateTimeOffset CreateDate { get; set; }
        public int MasterId { get; set; }
        public int ChildId { get; set; }
        public Nullable<int> Status { get; set; }
        public Nullable<int> IsAskForLeave { get; set; }
    }
}